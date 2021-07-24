using ASP;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class EPC_UserControl1_ExpBtn : System.Web.UI.UserControl
{
	public int Type = 1;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			if (this.Type == 0)
			{
				this.btnWord.Style["display"] = "none";
				this.btnexecl.Style["display"] = "none";
				return;
			}
			this.btnWord.Style["display"] = "";
			this.btnexecl.Style["display"] = "";
		}
	}
	protected void btnexecl_Click(object sender, EventArgs e)
	{
		this.ExportToExcel("application/ms-excel", this.Page.Title + ".xls");
	}
	protected void btnWord_Click(object sender, EventArgs e)
	{
		this.ExportToExcel("application/ms-excel", this.Page.Title + ".doc");
	}
	public void ExportToExcel(string FileType, string FileName)
	{
	}
}


