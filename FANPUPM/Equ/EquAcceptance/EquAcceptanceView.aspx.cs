using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_EquAcceptance_EquAcceptanceView : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.id = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	private void BindGvw()
	{
	}
	protected string GetCorpName(object corpId)
	{
		string result = string.Empty;
		if (corpId != null)
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId = xPMBasicContactCorpService.GetById(System.Convert.ToInt32(corpId));
			if (byId != null)
			{
				result = byId.CorpName;
			}
		}
		return result;
	}
}


