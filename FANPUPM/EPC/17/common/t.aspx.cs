using ASP;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
public partial class EPC_ScienceInnovate_common_Default : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable.TableName = "Ta";
		dataTable.Columns.Add("a", typeof(decimal));
		dataTable.Columns.Add("b", typeof(decimal));
		dataTable.Columns.Add("c", typeof(decimal), "a*b");
	}
}


