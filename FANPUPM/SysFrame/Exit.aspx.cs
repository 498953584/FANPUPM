using ASP;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
public partial class SysFrame_Exit : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		DataTable dataTable = (DataTable)base.Application["UserCodeCollection"];
		string b = this.Session["yhdm"].ToString();
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			if (dataTable.Rows[i]["userCode"].ToString() == b)
			{
				dataTable.Rows.Remove(dataTable.Rows[i]);
			}
		}
		base.Response.Write("<script>top.window.opener=null;top.window.open('','_self');top.window.close(); window.open('../default.aspx'); </script>");
	}
}


