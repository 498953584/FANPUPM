using ASP;
using cn.justwin.stockBLL.TableTopBLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SysFrame2_DBSJ_DBSJTop : Page, IRequiresSessionState
{
	private DesktopBLL deskTop = new DesktopBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string userCode = this.Session["yhdm"].ToString();
			this.rptTask.DataSource = this.deskTop.GetDBInfo(userCode, 6);
			this.rptTask.DataBind();
		}
	}
}


