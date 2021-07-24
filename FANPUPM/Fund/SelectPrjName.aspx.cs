using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_SelectPrjName : NBasePage, IRequiresSessionState
{
	private string _IC = string.Empty;
	public string IC
	{
		get
		{
			return this._IC;
		}
		set
		{
			this._IC = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request.QueryString["ic"] != null)
		{
			this._IC = base.Request.QueryString["ic"].ToString();
			this.bindItem(this._IC);
		}
	}
	private void bindItem(string _IC)
	{
		DataTable dataTable = new DataTable();
		Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
		dataTable = fund_Prj_Accoun.GetPrjNameByAccountID(_IC);
		if (dataTable.Rows.Count > 0)
		{
			this.GridView9.DataSource = dataTable;
			this.GridView9.DataBind();
		}
	}
	protected void GridView9_DataBound(object sender, EventArgs e)
	{
	}
	protected void GridView9_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = dataRowView["PrjGuid"].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"clickRow('",
				dataRowView["PrjGuid"].ToString(),
				"','",
				dataRowView["PrjName"].ToString(),
				"')"
			});
			e.Row.Attributes["dbClickRow"] = string.Concat(new string[]
			{
				"dbClickRow('",
				dataRowView["PrjGuid"].ToString(),
				"','",
				dataRowView["PrjName"].ToString(),
				"')"
			});
		}
	}
}


