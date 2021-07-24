using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Cost_CostPreReimburseModifyDetails : NBasePage, IRequiresSessionState
{
	private BudPreReimburseModifyService budModifySer = new BudPreReimburseModifyService();
	private BudPreReimburseModifyDetailService budModifyDetailSer = new BudPreReimburseModifyDetailService();
	private string Ic = string.Empty;

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
			this.BindModifyDetail();
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
			BudPreReimburseModify byId = this.budModifySer.GetById(this.Ic);
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
	public void BindModifyDetail()
	{
		System.Collections.Generic.List<BudPreReimburseModifyDetail> list = (
			from p in this.budModifyDetailSer
			where p.ModifyId == this.Request["ic"]
			select p).ToList<BudPreReimburseModifyDetail>();
		if (list.Count > 0)
		{
			decimal num = list.Sum((BudPreReimburseModifyDetail m) => m.BeginCost);
			decimal num2 = list.Sum((BudPreReimburseModifyDetail m) => m.AfterCost);
			base.RegisterScript(string.Concat(new string[]
			{
				"fillTotalAmount('",
				num.ToString("F3"),
				"','",
				num2.ToString("F3"),
				"')"
			}));
		}
		this.gvModifyDetails.DataSource = list;
		this.gvModifyDetails.DataBind();
	}
	protected void gvModifyDetails_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvModifyDetails.DataKeys[e.Row.RowIndex]["Id"].ToString();
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


