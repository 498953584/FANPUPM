using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_FileManage_SelectFile : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = "OnRecord(this);";
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			e.Row.ToolTip = "双击选择档案!";
			e.Row.Attributes["ondblclick"] = string.Concat(new string[]
			{
				"dblClickRow('",
				dataRowView["RecordID"].ToString(),
				"','",
				dataRowView["FileName"].ToString(),
				"','",
				dataRowView["FileCode"].ToString(),
				"','",
				dataRowView["PrjName"].ToString(),
				"','",
				dataRowView["SecretLevel"].ToString(),
				"');"
			});
		}
	}
}


