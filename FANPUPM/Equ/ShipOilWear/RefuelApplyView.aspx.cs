using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_ShipOilWear_RefuelApplyView : NBasePage, IRequiresSessionState
{
	private EquShipRefuelApplyService refuelApplySer = new EquShipRefuelApplyService();
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lblPrintDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.lblBllProducer.Text = WebUtil.GetUserNames(base.UserCode);
			EquShipRefuelApply byId = this.refuelApplySer.GetById(this.id);
		}
	}
}


