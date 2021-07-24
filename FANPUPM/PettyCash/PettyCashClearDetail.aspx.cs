using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashClearDetail : NBasePage, IRequiresSessionState
{
	private string Id = string.Empty;
	private PCPettyCashService pcSer = new PCPettyCashService();
	private BudIndirectDiaryDetailsService indirDetailSer = new BudIndirectDiaryDetailsService();
	private BudIndirectDiaryCostService budInCostSer = new BudIndirectDiaryCostService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.Id = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		PCPettyCash byId = this.pcSer.GetById(this.Id);
		if (byId != null)
		{
			this.lblBllProducer.Text = PageHelper.QueryUser(this, base.UserCode);
			this.lblPrintDate.Text = System.DateTime.Now.ToShortDateString();
			this.lblDatetime.Text = byId.ApplicationDate.ToString("yyyy-MM-dd");
			this.lblMoney.Text = System.Convert.ToString(byId.Cash);
			this.lblMatter.Text = byId.Matter;
			this.lblCashDate.Text = byId.CashDate.ToString("yyyy-MM-dd");
			this.lblAmount.Text = this.GetAmountCash(this.Id).ToString();
			this.lblAuditAmount.Text = this.GetAuditAmount(this.Id).ToString();
			this.lblReportMoney.Text = (byId.Cash - this.GetAmountCash(this.Id)).ToString();
			this.lblOweMoney.Text = (byId.Cash - this.GetAuditAmount(this.Id)).ToString();
			this.lblReturnMoney.Text = this.lblOweMoney.Text;
			if (byId.Project != null)
			{
				this.lblProject.Text = byId.Project.PrjName;
			}
		}
	}
	public decimal GetAmountCash(string pId)
	{
		System.Collections.Generic.List<string> lstIds = new System.Collections.Generic.List<string>();
		System.Collections.Generic.List<BudIndirectDiaryCost> source = (
			from p in this.budInCostSer
			where p.PettyCashId == pId && p.FlowState == 1
			select p).ToList<BudIndirectDiaryCost>();
		lstIds = (
			from p in source
			select p.InDiaryId).Distinct<string>().ToList<string>();
		System.Collections.Generic.List<BudIndirectDiaryDetails> source2 = (
			from p in this.indirDetailSer
			where lstIds.Contains(p.InDiaryId) && p.PettyCashId == pId
			select p).ToList<BudIndirectDiaryDetails>();
		return (
			from p in source2
			select p.Amount).Sum();
	}
	public decimal GetAuditAmount(string pId)
	{
		System.Collections.Generic.List<string> lstIds = new System.Collections.Generic.List<string>();
		System.Collections.Generic.List<BudIndirectDiaryCost> source = (
			from p in this.budInCostSer
			where p.PettyCashId == pId && p.FlowState == 1
			select p).ToList<BudIndirectDiaryCost>();
		lstIds = (
			from p in source
			where p.IsAudit
			select p.InDiaryId).Distinct<string>().ToList<string>();
		System.Collections.Generic.List<BudIndirectDiaryDetails> source2 = (
			from p in this.indirDetailSer
			where lstIds.Contains(p.InDiaryId) && p.PettyCashId == pId
			select p).ToList<BudIndirectDiaryDetails>();
		return (
			from p in source2
			select p.AuditAmount).Sum();
	}
}


