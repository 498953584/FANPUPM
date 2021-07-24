using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class UpdatePriv : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			string roleCode = base.Request.QueryString["RoleCode"].ToString();
			string userCode = base.Request.QueryString["UserCode"].ToString();
			roleManageDb roleManageDb = new roleManageDb();
			DataTable dataTable = roleManageDb.roleQuary(roleCode);
			string scopeCode = dataTable.Rows[0]["v_bgfw"].ToString();
			string moduleCode = dataTable.Rows[0]["v_xtqx"].ToString();
			userManageDb userManageDb = new userManageDb();
			bool flag = userManageDb.updateRolePriv(scopeCode, moduleCode, userCode);
			if (flag)
			{
				this.js.Text = "alert(\"更新用户权限成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"更新用户权限失败！\");";
			}
			JavaScriptControl expr_C2 = this.js;
			expr_C2.Text += "window.close();";
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


