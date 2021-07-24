using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashClearEdit : NBasePage, IRequiresSessionState
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
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.txtStatetime.Text = System.DateTime.Now.ToString("yyyy-M-dd");
	}
	public void InitPettyCash()
	{
		if (!string.IsNullOrEmpty(this.pcId))
		{
			PCPettyCash byId = this.pcSer.GetById(this.pcId);
			byId.CleanDate = System.Convert.ToDateTime(this.txtStatetime.Text);
			byId.CleanNote = this.txtNote.Text.Trim();
			byId.State = "1";
			this.pcSer.Update(byId);
		}
		base.RegisterScript("top.ui.winSuccess({parentName:'_PClearedit'});");
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		this.InitPettyCash();
	}
}


