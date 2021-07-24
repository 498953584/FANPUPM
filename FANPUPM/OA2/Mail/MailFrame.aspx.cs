using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class OA2_MailFrame : NBasePage, IRequiresSessionState
{
	private MailService mailService = new MailService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && !string.IsNullOrEmpty(base.Request["mailId"]))
		{
			this.hfldEmailID.Value = base.Request["mailId"];
		}
	}
	public new void Init()
	{
		int count = this.mailService.GetCount("", "", "", WebUtil.GetUserNames(base.UserCode), null, null, "I", new bool?(true), new bool?(false));
		int count2 = this.mailService.GetCount("", "", WebUtil.GetUserNames(base.UserCode), "", null, null, "D", new bool?(true), null);
		int count3 = this.mailService.GetCount("", "", WebUtil.GetUserNames(base.UserCode), "", null, null, "R", new bool?(true), new bool?(false));
		int count4 = this.mailService.GetCount("", "", WebUtil.GetUserNames(base.UserCode), "", null, null, "O", new bool?(true), null);
		int count5 = this.mailService.GetCount("", "", "", WebUtil.GetUserNames(base.UserCode), null, null, "", new bool?(false), null);
		this.inbox.Value = "收件箱(" + count.ToString() + ")";
		this.draft.Value = "草稿箱(" + count2.ToString() + ")";
		this.repeal.Value = "撤回邮件(" + count3.ToString() + ")";
		this.outbox.Value = "已发送邮件(" + count4.ToString() + ")";
		this.deleted.Value = "已删除邮件(" + count5.ToString() + ")";
	}
}


