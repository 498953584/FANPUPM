using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class UpdateUserPriv : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string text = base.Request.QueryString["RoleCode"].ToString();
			string strPriv = base.Request.QueryString["Priv"].ToString();
			roleManageDb roleManageDb = new roleManageDb();
			bool flag = roleManageDb.userPrivSet(text, strPriv);
			if (flag)
			{
				this.js.Text = "alert(\"更新用户权限成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"更新用户权限失败！\");";
			}
			JavaScriptControl expr_72 = this.js;
			expr_72.Text = expr_72.Text + "top.FraOperate.location = 'UpdateRolePriv.aspx?id=" + text + "';";
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


