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
public partial class BudgetManage_Cost_CostBudgetDetails : NBasePage, IRequiresSessionState
{
	private BudPreReimburseApplyService budiApplySer = new BudPreReimburseApplyService();
	private BudPreReimburseApplyDetailService budApplyDetailSer = new BudPreReimburseApplyDetailService();
	private BudPreReimburseApplyDetail indirApplyDetail = new BudPreReimburseApplyDetail();
	private string Ic = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.BindDetails();
			this.BindRptContrast();
		}
	}
	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.Ic = base.Request["ic"];
		}
		base.OnInit(e);
	}
	public void InitPage()
	{
		if (!string.IsNullOrEmpty(this.Ic))
		{
			this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			BudPreReimburseApply byId = this.budiApplySer.GetById(this.Ic);
			if (byId.CostType == "P")
			{
				this.lblPrjName.Text = this.GetPrjName(byId.PrjId);
				this.lblPrjCode.Text = this.GetPrjCode(byId.PrjId);
			}
			else
			{
				this.lblPrjNameTitle.Text = "部门名称";
				this.lblPrjCodeTitle.Text = "部门全称";
				this.lblPrjName.Text = this.GetDepName(byId.PrjId);
				this.lblPrjCode.Text = this.GetDepFullName(byId.PrjId);
			}
			this.lblName.Text = byId.Name;
			this.lblCode.Text = byId.Code;
			this.lblInputDate.Text = byId.ApplyDate.ToString("yyyy-M-dd");
			this.lblInputUser.Text = byId.RptUser;
		}
	}
	protected void BindDetails()
	{
		System.Collections.Generic.List<BudPreReimburseApplyDetail> list = (
			from p in this.budApplyDetailSer
			where p.ApplyId == this.Request["ic"]
			select p).ToList<BudPreReimburseApplyDetail>();
		if (list.Count > 0)
		{
			base.RegisterScript("fillTotalAmount('" + list.Sum((BudPreReimburseApplyDetail m) => m.Cost).ToString("F3") + "')");
		}
		this.gvDiaryDetails.DataSource = list;
		this.gvDiaryDetails.DataBind();
	}
	protected void BindRptContrast()
	{
		DataTable contrast = CostApply.GetContrast(base.Request["ic"]);
		this.rptContrast.DataSource = contrast;
		this.rptContrast.DataBind();
		if (contrast.Rows.Count == 0)
		{
			base.RegisterScript("$('#rptContrast tr:last-child').remove();");
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
	protected void gvDiaryDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvDiaryDetails.DataKeys[e.Row.RowIndex]["Id"].ToString();
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
	public void SumTotal()
	{
		DataTable contrast = CostApply.GetContrast(base.Request["ic"]);
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
			decimal value2 = decimal.Parse((dataTable.Compute("SUM(MonthAmount)", "1=1") == null) ? 0m.ToString() : dataTable.Compute("SUM(MonthAmount)", "1=1").ToString());
			dictionary.Add("MonthAmount", value2);
			decimal value3 = decimal.Parse(dataTable.Compute("SUM(PrjAmount)", "1=1").ToString());
			dictionary.Add("PrjAmount", value3);
			decimal value4 = decimal.Parse(dataTable.Compute("SUM(PrjAlreadyAmount)", "1=1").ToString());
			dictionary.Add("PrjAlreadyAmount", value4);
			this.ViewState["sum"] = dictionary;
		}
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
}


