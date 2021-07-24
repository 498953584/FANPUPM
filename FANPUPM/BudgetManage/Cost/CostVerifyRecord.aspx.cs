using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
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
public partial class BudgetManage_Cost_CostVerifyRecord : NBasePage, IRequiresSessionState
{
	private BudIndirectDiaryDetailsService IndiDetailSer = new BudIndirectDiaryDetailsService();
	private BudIndirectDiaryDetails indirDetails = new BudIndirectDiaryDetails();
	private BudIndirectDiaryCostService IndiCostSer = new BudIndirectDiaryCostService();

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
			this.hfldPurchaseChecked.Value = base.Request["ic"];
			BudIndirectDiaryCost byId = this.IndiCostSer.GetById(base.Request["ic"]);
			if (byId.CostType == "P")
			{
				this.lblPrjName.Text = this.GetPrjName(byId.ProjectId);
				this.lblPrjCode.Text = this.GetPrjCode(byId.ProjectId);
			}
			else
			{
				this.lblPrjNameTitle.Text = "部门名称";
				this.lblPrjCodeTitle.Text = "部门全称";
				this.lblPrjName.Text = this.GetDepName(byId.ProjectId);
				this.lblPrjCode.Text = this.GetDepFullName(byId.ProjectId);
			}
			this.lblCode.Text = byId.IndireCode;
			this.lblName.Text = byId.Name;
			this.lblDeparment.Text = byId.Department;
			this.lblInputDate.Text = byId.InputDate.ToString("yyyy-M-dd");
			this.lblInputUser.Text = byId.InputUser;
			this.lblPeople.Text = byId.IssuedBy;
		}
	}
	protected void BindDetails()
	{
		System.Collections.Generic.List<BudIndirectDiaryDetails> list = (
			from p in this.IndiDetailSer
			where p.InDiaryId == this.Request["ic"]
			orderby p.No
			select p).ToList<BudIndirectDiaryDetails>();
		if (list.Count > 0)
		{
			base.RegisterScript("fillTotalAmount('" + list.Sum((BudIndirectDiaryDetails m) => m.Amount).ToString("F3") + "')");
		}
		this.gvDiaryDetails.DataSource = list;
		this.gvDiaryDetails.DataBind();
	}
	protected void gvDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["InDiaryDetailsId"].ToString();
		}
	}
	protected void BindRptContrast()
	{
		DataTable contrast = CostDiary.GetContrast(base.Request["ic"]);
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
							(e.Item.FindControl("lbl" + current) as Label).Text = dictionary[current].ToString("F2") + "%";
						}
					}
				}
			}
		}
	}
	protected void SumTotal()
	{
		DataTable contrast = CostDiary.GetContrast(base.Request["ic"]);
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
				dictionary.Add("ChaLv", decimal.Round(dictionary["MonthCha"] * 100m / dictionary["MonthAmount"], 3));
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
	private string GetPrjName(string prjId)
	{
		string result = string.Empty;
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(prjId);
		if (byId != null)
		{
			result = byId.PrjName;
		}
		else
		{
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(prjId);
			if (byId2 != null)
			{
				result = byId2.PrjName;
			}
		}
		return result;
	}
	private string GetPrjCode(string prjId)
	{
		string result = string.Empty;
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(prjId);
		if (byId != null)
		{
			result = byId.PrjCode;
		}
		else
		{
			PTPrjInfoZTBService pTPrjInfoZTBService = new PTPrjInfoZTBService();
			PTPrjInfoZTB byId2 = pTPrjInfoZTBService.GetById(prjId);
			if (byId2 != null)
			{
				result = byId2.PrjCode;
			}
		}
		return result;
	}
	public string CBSName(string CBSCode)
	{
		CostAccounting byCode = CostAccounting.GetByCode(CBSCode);
		if (byCode == null)
		{
			return string.Empty;
		}
		return byCode.Name;
	}
	private string GetDepName(string depid)
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTdbm byId = pTdbmService.GetById(depid);
		if (byId != null)
		{
			return byId.V_BMMC;
		}
		return string.Empty;
	}
	private string GetDepFullName(string depid)
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTdbm byId = pTdbmService.GetById(depid);
		if (byId != null)
		{
			return byId.V_bmqc;
		}
		return string.Empty;
	}
}


