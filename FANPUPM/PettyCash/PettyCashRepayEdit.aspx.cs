using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashRepayEdit : NBasePage, IRequiresSessionState
{
	private string pcId = string.Empty;
	private PCPettyCashService pcSer = new PCPettyCashService();
	private BudIndirectDiaryDetailsService indirDetailSer = new BudIndirectDiaryDetailsService();
	private BudIndirectDiaryCostService budInCostSer = new BudIndirectDiaryCostService();
	private PTYhmcService yhmcSer = new PTYhmcService();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.pcId = base.Request["ic"];
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
	protected void InitPage()
	{
		PCPettyCash byId = this.pcSer.GetById(this.pcId);
		if (byId != null)
		{
			PTyhmc byId2 = this.yhmcSer.GetById(byId.Applicant);
			this.txtApplicant.Text = byId2.v_xm;
			this.txtDatetime.Text = byId.ApplicationDate.ToString("yyyy-MM-dd");
			this.txtMoney.Text = System.Convert.ToString(byId.Cash);
			this.txtMatter.Text = byId.Matter;
			this.txtCashDate.Text = byId.CashDate.ToString("yyyy-MM-dd");
			this.txtAmount.Text = this.GetAmountCash(this.pcId).ToString();
			this.txtAuditAmount.Text = this.GetAuditAmount(this.pcId).ToString();
			this.txtReportMoney.Text = (byId.Cash - this.GetAmountCash(this.pcId)).ToString();
			this.txtOweMoney.Text = (byId.Cash - this.GetAuditAmount(this.pcId)).ToString();
			if (byId.Project != null)
			{
				this.txtProject.Text = byId.Project.PrjName;
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.SaveRepayCash();
	}
	public void SaveRepayCash()
	{
		if (!string.IsNullOrEmpty(this.pcId))
		{
			PCPettyCash byId = this.pcSer.GetById(this.pcId);
			byId.RepayCash = (string.IsNullOrEmpty(this.txtReturnMoney.Text) ? 0m : System.Convert.ToDecimal(this.txtReturnMoney.Text));
			decimal d = decimal.Parse(this.txtOweMoney.Text);
			if (byId.RepayCash < d)
			{
				base.RegisterScript("top.ui.alert('还款金额不能小于欠款金额');");
				return;
			}
			byId.RepayFlowState = -1;
			byId.IsRepay = true;
			this.pcSer.Update(byId);
		}
		base.RegisterScript("top.ui.winSuccess({parentName:'_PcReadyedit'});");
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


