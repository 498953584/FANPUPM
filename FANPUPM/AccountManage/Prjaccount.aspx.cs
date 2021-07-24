using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class AccountManage_Prjaccount : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_06_0 = base.IsPostBack;
	}
	public void gvBind(string sql)
	{
		DataTable dataSource = new DataTable();
		this.gvwAccount.DataSource = dataSource;
	}
}


