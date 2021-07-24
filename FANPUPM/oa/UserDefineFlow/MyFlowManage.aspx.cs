using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_UserDefineFlow_myflowmanage : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack && this.Session["yhdm"] != null)
		{
			this.DisplayFlowClass(this.Session["yhdm"].ToString());
		}
	}
	private void DisplayFlowClass(string usercode)
	{
		string sqlString = "select * from PT_YHMC_Privilege where V_yhdm='" + usercode + "' and C_MKDM='4815'";
		DataTable dataTable = publicDbOpClass.DataTableQuary(sqlString);
		if (dataTable.Rows.Count > 0)
		{
			this.tableClass.Visible = true;
		}
	}
}


