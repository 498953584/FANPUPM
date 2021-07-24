using ASP;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Help_Tohtm : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void Button1_Click(object sender, EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = publicDbOpClass.DataTableQuary("SELECT * FROM  PT_help ");
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			string str = dataTable.Rows[i]["txt_help"].ToString().Replace("helpSel.aspx?id=", "").Replace("&amp;mc=", ".html");
			string str2 = dataTable.Rows[i]["C_MKDM"].ToString();
			string str3 = dataTable.Rows[i]["v_MKMC"].ToString();
			string str4 = "<div    align=left style='color:#000099;font-weight:bold;padding-left:10px;height:27px;width:100%;font-size:14px;background-color: #E9F3FC;'><span >" + str3 + "</span></div>";
			File.WriteAllText("d:\\htm\\" + str2 + ".html", str4 + str, Encoding.UTF8);
		}
	}
	protected void Button2_Click(object sender, EventArgs e)
	{
		base.Response.Redirect("help.aspx?op=");
	}
}


