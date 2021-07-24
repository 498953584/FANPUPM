using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashDetail : NBasePage, IRequiresSessionState
{
	private PCPettyCashService pcSer = new PCPettyCashService();
	private string pcId = string.Empty;

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
			this.Initial();
		}
	}
	private void Initial()
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTYhmcService pTYhmcService = new PTYhmcService();
		PTdbm byId = pTdbmService.GetById(1);
		this.lblTitle.Text = byId.V_BMMC + "备用金申请表";
		if (!string.IsNullOrEmpty(this.pcId))
		{
			PCPettyCash byId2 = this.pcSer.GetById(this.pcId);
			if (byId2 != null)
			{
				this.hfldPettyCashIds.Value = this.pcId;
				PTyhmc byId3 = pTYhmcService.GetById(byId2.Applicant);
				PTdbm byId4 = pTdbmService.GetById(byId3.i_bmdm);
				this.lblPayer.Text = byId2.Payer;
				this.lblApplicationDate.Text = byId2.ApplicationDate.ToString("yyyy-MM-dd");
				this.lblDepart.Text = byId4.V_BMMC;
				this.lblApplicant.Text = byId3.v_xm;
				this.lblAccount.Text = byId2.Account;
				this.lblBank.Text = byId2.Bank;
				this.lblMatter.Text = byId2.Matter;
				if (byId2.Project != null)
				{
					this.lblProject.Text = byId2.Project.PrjName;
				}
				this.lblCash.Text = byId2.Cash.ToString();
				this.lblpayee.Text = byId2.Payee;
				this.lblCashDate.Text = byId2.CashDate.ToString("yyyy-MM-dd");
				this.lblApplicationReason.Text = StringUtility.ReplaceTxt(byId2.ApplicationReason);
			}
		}
	}
}


