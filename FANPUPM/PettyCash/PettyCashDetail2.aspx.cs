using ASP;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashDetail2 : NBasePage, IRequiresSessionState
{
	private string pcId = string.Empty;
	private PCPettyCashService pcSer = new PCPettyCashService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.GetPattyCash();
			this.InitRptAudit();
			this.GetNamePath(base.UserCode);
		}
	}
	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.pcId = base.Request["ic"];
		}
		base.OnInit(e);
	}
	public void GetPattyCash()
	{
		PTdbmService pTdbmService = new PTdbmService();
		PTYhmcService pTYhmcService = new PTYhmcService();
		BasicSerialNumberService basicSerialNumberService = new BasicSerialNumberService();
		PTdbm byId = pTdbmService.GetById(1);
		this.lblTitle.Text = byId.V_BMMC + "备用金申请表";
		this.lblPayMentDate.Text = System.DateTime.Now.ToShortDateString();
		if (!string.IsNullOrEmpty(this.pcId))
		{
			PCPettyCash byId2 = this.pcSer.GetById(this.pcId);
			if (byId2 != null)
			{
				PTyhmc byId3 = pTYhmcService.GetById(byId2.Applicant);
				PTdbm byId4 = pTdbmService.GetById(byId3.i_bmdm);
				this.lblDepartMent.Text = byId4.V_BMMC;
				this.lblOperator.Text = byId3.v_xm;
				this.lblContent.Text = byId2.Matter;
				this.lblMoney.Text = byId2.Cash.ToString();
				this.lblTotalMoney.Text = this.lblMoney.Text;
				this.lblDrawPeople.Text = byId3.v_xm;
				this.lblpayee.Text = byId2.Payee;
				this.lblBank.Text = byId2.Bank;
				this.lblAccount.Text = byId2.Account;
				if (byId2.Project != null)
				{
					this.lblProjectCode.Text = byId2.Project.PrjCode;
				}
				this.lblFileNumber.Text = basicSerialNumberService.GetNo("PC_PettyCash", this.pcId);
			}
		}
	}
	public void InitRptAudit()
	{
		WFInstanceMainService wFInstanceMainService = new WFInstanceMainService();
		WFInstanceService source = new WFInstanceService();
		System.Collections.Generic.IList<WFInstanceMain> byInstanceCode = wFInstanceMainService.GetByInstanceCode(this.pcId);
		System.Collections.Generic.List<WFInstance> list = new System.Collections.Generic.List<WFInstance>();
		foreach (WFInstanceMain im in byInstanceCode)
		{
			System.Collections.Generic.List<WFInstance> list2 = (
				from i in source
				where i.ID == (int?)im.ID && i.AuditResult == (int?)1
				orderby i.AuditDate descending
				select i).ToList<WFInstance>();
			foreach (WFInstance current in list2)
			{
				if ((im.ID == byInstanceCode.Last<WFInstanceMain>().ID || current.Sing.Value != -1) && !string.IsNullOrEmpty(this.GetNamePath(current.Operator)))
				{
					list.Add(current);
				}
			}
		}
		if (list.Count > 0)
		{
			this.rptAudit.DataSource = list;
			this.rptAudit.DataBind();
		}
	}
	public string GetNamePath(object userCode)
	{
		string result = string.Empty;
		PTLOGINService pTLOGINService = new PTLOGINService();
		PTLOGIN byUserCode = pTLOGINService.GetByUserCode(userCode.ToString());
		if (byUserCode != null && !string.IsNullOrEmpty(byUserCode.AuditNameImagePath))
		{
			result = ".." + byUserCode.AuditNameImagePath;
		}
		else
		{
			result = "";
		}
		return result;
	}
}


