using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class PwdSetting : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (this.Session["pttest"] == null || this.Session["pttest"].ToString() != "notest")
				{
					base.Response.Write("<font color=\"red\">对不起，DEMO版无此权限！</font>");
					base.Response.End();
				}
				this.Label1.Text = "用户密码设置";
				userManageDb userManageDb = new userManageDb();
				this.tbLoginName.Text = userManageDb.getUserDlid(this.Session["yhdm"].ToString());
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void btMod_Click(object sender, EventArgs e)
		{
			string text = this.tbOldPwd.Text;
			string text2 = this.tbNewPwd1.Text;
			string text3 = this.tbNewPwd2.Text;
			userManageDb userManageDb = new userManageDb();
			bool flag = userManageDb.comparePwd(this.Page.Session["yhdm"].ToString(), text);
			if (flag)
			{
				if (userManageDb.chkDlid(this.tbLoginName.Text.Trim(), this.Session["yhdm"].ToString()) == 1)
				{
					this.js.Text = "top.ui.alert('您新修改的登录名已经被别人使用！请更换。');";
					return;
				}
				if (userManageDb.chkDlid(this.tbLoginName.Text.Trim(), this.Session["yhdm"].ToString()) == 0)
				{
					if (!(text2 == text3))
					{
						this.js.Text = "top.ui.alert('您两次输入的新密码不相同，请重新输入！');";
						return;
					}
					if (userManageDb.updateUserPwd(this.tbLoginName.Text.Trim(), this.Page.Session["yhdm"].ToString(), text2.ToString()))
					{
						this.js.Text = "top.ui.show('密码修改成功，下次登录请使用新密码！');";
						return;
					}
					this.js.Text = "top.ui.alert('密码修改未成功，请重试！');";
					return;
				}
			}
			else
			{
				this.js.Text = "top.ui.alert('输入的旧密码不正确！');";
			}
		}
	}


