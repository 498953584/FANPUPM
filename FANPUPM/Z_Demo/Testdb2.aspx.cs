using ASP;
using cn.justwin.DAL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Z_Demo_Testdb2 : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.lbltest.Text = "欢迎进入DB2测试页面,请点击db2测试按钮进行测试。";
		}
	}
	protected void btndb2_Click(object sender, EventArgs e)
	{
		DB2SqlHelper.MiddleTableImport();
	}
}


