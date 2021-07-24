using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class UpdateRole : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				string roleCode = this.Page.Request.QueryString["RecordID"].Trim();
				roleManageDb roleManageDb = new roleManageDb();
				DataTable dataTable = roleManageDb.roleQuary(roleCode);
				this.tbJsmc.Text = dataTable.Rows[0]["RoleName"].ToString();
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
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			string roleCode = base.Request.QueryString["RecordID"].ToString();
			string text = this.tbJsmc.Text;
			roleManageDb roleManageDb = new roleManageDb();
			bool flag = roleManageDb.roleMod(roleCode, text);
			if (flag)
			{
				string script = "\r\n\t\t\t\t\t<script>\r\n\t\t\t\t\t\ttop.ui.alert('修改成功!');\r\n\t\t\t\t\t\ttop.ui.closeWin();\r\n\t\t\t\t\t\ttop.ui.reloadTab();\r\n\t\t\t\t\t</script>\r\n\t\t\t\t";
				base.ClientScript.RegisterStartupScript(base.GetType(), "success", script);
				return;
			}
			if (!flag)
			{
				base.ClientScript.RegisterStartupScript(base.GetType(), "error", "<script>alert('数据库操作不成功！');</script>");
			}
		}
	}


