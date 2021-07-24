using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Analysis_ChartDetail : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
		if (byId != null)
		{
			base.RegisterScript("setIframe('" + byId.PrjName + "')");
		}
	}
}


