using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Personnel_DutyList : BasePage, IRequiresSessionState
{
	public int DeptID
	{
		get
		{
			object obj = this.ViewState["DeptID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return 0;
		}
		set
		{
			this.ViewState["DeptID"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		bool arg_0B_0 = this.Page.IsPostBack;
	}
	protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			string text = this.GridView1.DataKeys[e.Row.RowIndex]["I_DUTYID"].ToString();
			string text2 = this.GridView1.DataKeys[e.Row.RowIndex]["i_bmdm"].ToString();
			string text3 = ((DataRowView)e.Row.DataItem)["DutyName"].ToString();
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				text,
				"','",
				text2,
				"','",
				text3,
				"')"
			});
		}
	}
}


