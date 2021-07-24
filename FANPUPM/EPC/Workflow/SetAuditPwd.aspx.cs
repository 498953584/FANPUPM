using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_SetAuditPwd : NBasePage, IRequiresSessionState
{
	public string ImageName
	{
		get
		{
			object obj = this.ViewState["ImageName"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["ImageName"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			new userManageDb();
			this.tbLoginName.Text = userManageDb.GetCurrentUserInfo().UserName;
		}
	}
	protected void btMod_Click(object sender, System.EventArgs e)
	{
		string text = this.tbOldPwd.Text;
		string text2 = this.tbNewPwd1.Text;
		string text3 = this.tbNewPwd2.Text;
		userManageDb userManageDb = new userManageDb();
		string userLoginPwd = userManageDb.GetCurrentUserInfo().UserLoginPwd;
		if (!(text == userLoginPwd))
		{
			this.js.Text = "top.ui.alert('您输入的旧密码不正确，请重新输入！');";
			return;
		}
		if (!(text2 == text3))
		{
			this.js.Text = "top.ui.alert('您两次输入的新密码不相同，请重新输入！');";
			return;
		}
		if (userManageDb.updateUserAuditPwd(this.Page.Session["yhdm"].ToString(), text2.ToString()))
		{
			this.js.Text = "top.ui.show('审核密码修改成功，下次审核请使用新密码！');";
			return;
		}
		this.js.Text = "top.ui.alert('审核密码修改未成功，请重试！');";
	}
}


