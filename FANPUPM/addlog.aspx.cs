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
public partial class addlog : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void btnOk_Click(object sender, EventArgs e)
	{
		try
		{
			if (!string.IsNullOrWhiteSpace(this.dropLogType.SelectedValue) && !string.IsNullOrWhiteSpace(this.txtLog.Text) && !string.IsNullOrWhiteSpace(this.txtDate.Text))
			{
				string cmdText = string.Format("INSERT INTO UpdateLog (Title, Log, UpdateDate) VALUES('{0}', '{1}','{2}');", this.dropLogType.SelectedValue, this.txtLog.Text, this.txtDate.Text);
				SqlHelper.ExecuteNonQuery(CommandType.Text, cmdText, new SqlParameter[0]);
				this.dropLogType.SelectedValue = "";
				this.txtLog.Text = "";
				this.txtDate.Text = "";
			}
		}
		catch (Exception ex)
		{
			base.Response.Write(ex.Message);
		}
	}
}


