using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashEdit : NBasePage, IRequiresSessionState
{
	private PCPettyCashService pcSer = new PCPettyCashService();
	private string type = string.Empty;
	private string pcId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.pcId = base.Request["id"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Initial();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		if (string.Compare(this.type, "Add", true) == 0)
		{
			PCPettyCash item = this.CreatePettyCash();
			try
			{
				this.pcSer.Add(item);
				base.RegisterScript("top.ui.tabSuccess({parentName: '_pettycashlist'});");
				return;
			}
			catch (System.Exception)
			{
				base.RegisterScript("top.ui.alert('新增失败');");
				return;
			}
		}
		if (string.Compare(this.type, "Edit", true) == 0)
		{
			PCPettyCash byId = this.pcSer.GetById(this.pcId);
			this.UpdatePettyCash(byId);
			try
			{
				this.pcSer.Update(byId);
				base.RegisterScript("top.ui.tabSuccess({parentName: '_pettycashlist'});");
			}
			catch (System.Exception)
			{
				base.RegisterScript("top.ui.alert('编辑失败');");
			}
		}
	}
	private void Initial()
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTYhmcService pTYhmcService = new PTYhmcService();
		PTdbm byId = pTdbmService.GetById(1);
		this.lblTitle.Text = byId.V_BMMC + "备用金申请表";
		if (string.Compare(this.type, "Add", true) == 0)
		{
			this.txtPayer.Text = byId.V_BMMC;
			this.txtApplicationDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.txtCashDate.Text = System.DateTime.Now.AddDays(3.0).ToString("yyyy-MM-dd");
			PTyhmc byId2 = pTYhmcService.GetById(base.UserCode);
			this.txtApplicant.Text = byId2.v_xm;
			PTdbm byId3 = pTdbmService.GetById(byId2.i_bmdm);
			this.txtDepart.Text = byId3.V_BMMC;
		}
		if (!string.IsNullOrEmpty(this.pcId))
		{
			PCPettyCash byId4 = this.pcSer.GetById(this.pcId);
			if (byId4 != null)
			{
				PTyhmc byId5 = pTYhmcService.GetById(byId4.Applicant);
				PTdbm byId6 = pTdbmService.GetById(byId5.i_bmdm);
				this.txtPayer.Text = byId4.Payer;
				this.txtApplicationDate.Text = byId4.ApplicationDate.ToString("yyyy-MM-dd");
				this.txtDepart.Text = byId6.V_BMMC;
				this.txtApplicant.Text = byId5.v_xm;
				this.txtAccount.Text = byId4.Account;
				this.txtBank.Text = byId4.Bank;
				this.txtMatter.Text = byId4.Matter;
				if (byId4.Project != null)
				{
					this.txtProject.Text = byId4.Project.PrjName;
					this.hfldPrjTypeCode.Value = byId4.Project.TypeCode;
				}
				this.txtCash.Text = byId4.Cash.ToString();
				this.txtCashDate.Text = byId4.CashDate.ToString("yyyy-MM-dd");
				this.txtApplicationReason.Text = byId4.ApplicationReason;
				this.txtpayee.Text = byId4.Payee;
			}
		}
	}
	private PCPettyCash CreatePettyCash()
	{
		return new PCPettyCash
		{
			Id = System.Guid.NewGuid().ToString(),
			Payer = this.txtPayer.Text.Trim(),
			ApplicationDate = System.Convert.ToDateTime(this.txtApplicationDate.Text),
			Applicant = base.UserCode,
			Account = this.txtAccount.Text.Trim(),
			Bank = this.txtBank.Text.Trim(),
			Matter = this.txtMatter.Text.Trim(),
			PrjTypeCode = this.hfldPrjTypeCode.Value,
			Cash = System.Convert.ToDecimal(this.txtCash.Text),
			Payee = this.txtpayee.Text.Trim(),
			CashDate = System.Convert.ToDateTime(this.txtCashDate.Text),
			ApplicationReason = this.txtApplicationReason.Text.Trim(),
			FlowState = -1,
			InputUser = base.UserCode,
			InputDate = System.DateTime.Now,
			ModifyUser = base.UserCode,
			ModifyDate = System.DateTime.Now,
			RepayCash = 0m,
			RepayFlowState = -1,
			State = "0",
			CleanDate = System.DateTime.Now
		};
	}
	private void UpdatePettyCash(PCPettyCash pc)
	{
		if (pc != null)
		{
			pc.Payer = this.txtPayer.Text.Trim();
			pc.ApplicationDate = System.Convert.ToDateTime(this.txtApplicationDate.Text);
			pc.Applicant = base.UserCode;
			pc.Account = this.txtAccount.Text.Trim();
			pc.Bank = this.txtBank.Text.Trim();
			pc.Matter = this.txtMatter.Text.Trim();
			pc.PrjTypeCode = this.hfldPrjTypeCode.Value;
			pc.Cash = System.Convert.ToDecimal(this.txtCash.Text);
			pc.Payee = this.txtpayee.Text.Trim();
			pc.CashDate = System.Convert.ToDateTime(this.txtCashDate.Text);
			pc.ApplicationReason = this.txtApplicationReason.Text.Trim();
			pc.ModifyUser = base.UserCode;
			pc.ModifyDate = System.DateTime.Now;
		}
	}
}


