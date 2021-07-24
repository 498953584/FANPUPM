using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class AddRole : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			string text = this.tbJsmc.Text;
			roleManageDb roleManageDb = new roleManageDb();
			roleManageDb.roleAdd(base.Request.QueryString["rt"].ToString(), text);
			string script = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\ttop.ui.alert('保存成功!');\r\n\t\t\t\t\t\ttop.ui.closeWin();\r\n\t\t\t\t\t\ttop.ui.reloadTab();\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
			base.ClientScript.RegisterStartupScript(base.GetType(), "success", script);
		}
	}


