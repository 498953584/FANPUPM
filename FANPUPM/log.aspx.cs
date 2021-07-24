using ASP;
using cn.justwin.DAL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class log : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Init();
		}
	}
	private new void Init()
	{
		try
		{
			string cmdText = "SELECT * FROM UpdateLog ORDER BY UpdateDate DESC, Id DESC";
			this.gvwLog.DataSource = SqlHelper.ExecuteQuery(CommandType.Text, cmdText, new SqlParameter[0]);
			this.gvwLog.DataBind();
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
		}
	}
}


