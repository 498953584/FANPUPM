using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_OrgVerifyRecord : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.BindDetails();
			this.BindRptContrast();
		}
	}
	protected void InitPage()
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			OrganizationDiary byId = OrganizationDiary.GetById(base.Request["ic"]);
			if (byId != null)
			{
				this.lblName.Text = byId.Name;
				this.lblCode.Text = byId.Code;
				this.lblDeparment.Text = byId.Department;
				this.lblInputDate.Text = byId.InputDate.ToString("yyyy-M-dd");
				this.lblInputUser.Text = byId.InputUser;
				this.lblPeople.Text = byId.IssuedBy;
			}
		}
	}
	protected void BindDetails()
	{
		System.Collections.Generic.List<OrganizationDiaryDetails> allByInDiaryId = OrganizationDiaryDetails.GetAllByInDiaryId(base.Request["ic"]);
		if (allByInDiaryId.Count > 0)
		{
			base.RegisterScript("fillTotalAmount('" + allByInDiaryId.Sum((OrganizationDiaryDetails m) => m.Amount).ToString("F3") + "')");
		}
		this.gvDiaryDetails.DataSource = allByInDiaryId;
		this.gvDiaryDetails.DataBind();
	}
	protected void gvDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["id"].ToString();
		}
	}
	protected void BindRptContrast()
	{
		DataTable contrast = OrganizationDiary.GetContrast(base.Request["ic"]);
		this.rptContrast.DataSource = contrast;
		this.rptContrast.DataBind();
		if (contrast.Rows.Count == 0)
		{
			base.RegisterScript("$('#rptContrast tr:last-child').remove();");
		}
	}
	protected void rptContrast_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Footer)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
			if (this.rptContrast.Items.Count > 0)
			{
				if (this.ViewState["sum"] == null)
				{
					this.SumTotal();
				}
				dictionary = (this.ViewState["sum"] as System.Collections.Generic.Dictionary<string, decimal>);
				foreach (string current in dictionary.Keys)
				{
					if (!(current == "MonthAlreadyAmount"))
					{
						(e.Item.FindControl("lbl" + current) as Label).Text = dictionary[current].ToString();
						if (current == "ChaLv")
						{
							(e.Item.FindControl("lbl" + current) as Label).Text = dictionary[current].ToString("P2");
						}
					}
				}
			}
		}
	}
	protected void SumTotal()
	{
		DataTable contrast = OrganizationDiary.GetContrast(base.Request["ic"]);
		if (contrast.Rows.Count > 0)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
			string text = "ThisAmount";
			decimal value = decimal.Parse(contrast.Compute("sum(" + text + ")", "1=1").ToString());
			dictionary.Add(text, value);
			DataTable dataTable = contrast.Clone();
			int num = -1;
			foreach (DataRow dataRow in contrast.Rows)
			{
				num++;
				if (num <= 0 || !(dataRow["CBSCode"].ToString() == contrast.Rows[num - 1]["CBSCode"].ToString()))
				{
					DataRow dataRow2 = dataTable.NewRow();
					dataRow2.ItemArray = dataRow.ItemArray;
					dataTable.Rows.Add(dataRow2);
				}
			}
			decimal num2 = decimal.Parse((dataTable.Compute("SUM(MonthAmount)", "1=1") == null) ? 0m.ToString() : dataTable.Compute("SUM(MonthAmount)", "1=1").ToString());
			decimal d = decimal.Parse((dataTable.Compute("SUM(MonthAlreadyAmount)", "1=1") == null) ? 0m.ToString() : dataTable.Compute("SUM(MonthAlreadyAmount)", "1=1").ToString());
			dictionary["MonthAmount"] = num2;
			dictionary["MonthCha"] = num2 - d;
			if (dictionary["MonthAmount"] > 0m)
			{
				dictionary.Add("ChaLv", decimal.Round(dictionary["MonthCha"] / dictionary["MonthAmount"], 3));
			}
			else
			{
				dictionary.Add("ChaLv", 0m);
			}
			decimal value2 = decimal.Parse(dataTable.Compute("SUM(PrjAmount)", "1=1").ToString());
			dictionary.Add("PrjAmount", value2);
			decimal value3 = decimal.Parse(dataTable.Compute("SUM(PrjAlreadyAmount)", "1=1").ToString());
			dictionary.Add("PrjAlreadyAmount", value3);
			this.ViewState["sum"] = dictionary;
		}
	}
}


