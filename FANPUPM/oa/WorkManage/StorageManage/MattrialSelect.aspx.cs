using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_StorageManage_MattrialSelect : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
		this.btnAdd.Attributes["onclick"] = "javascript:MaterialSelect();";
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["ResName"].ToString(),
				"','",
				dataRowView["UseType"].ToString(),
				"','",
				dataRowView["GetType"].ToString(),
				"','",
				dataRowView["Unit"].ToString(),
				"','",
				dataRowView["PlanFee"].ToString(),
				"','",
				dataRowView["RecordID"].ToString(),
				"');"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
			string a;
			if ((a = dataRowView["UseType"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (a == "1")
					{
						e.Row.Cells[2].Text = "以旧换新";
					}
				}
				else
				{
					e.Row.Cells[2].Text = "不回收";
				}
			}
			string a2;
			if ((a2 = dataRowView["GetType"].ToString()) != null)
			{
				if (a2 == "0")
				{
					e.Row.Cells[3].Text = "个人领用";
					return;
				}
				if (!(a2 == "1"))
				{
					return;
				}
				e.Row.Cells[3].Text = "部门公共";
			}
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		SqlDataSource expr_06 = this.SQLDataSource;
		expr_06.SelectCommand = expr_06.SelectCommand + " and ResName like '%" + this.txtKey.Text.Trim() + "%'";
		this.GVBook.DataBind();
	}
}


