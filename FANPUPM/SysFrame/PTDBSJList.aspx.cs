using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SysFrame_PTDBSJList : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindDbsj();
		}
	}
	protected void GVDBSJ_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.GVDBSJ.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1) + "、";
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			string str = "/" + dataRowView["V_DBLJ"].ToString().Replace("ModuleSet", "Epc");
			LinkButton linkButton = (LinkButton)e.Row.Cells[1].FindControl("lblContent");
			if (linkButton != null)
			{
				linkButton.Text = dataRowView["V_Content"].ToString() + "&nbsp;" + Convert.ToDateTime(dataRowView["DTM_DBSJ"]).ToString("yyyy-MM-dd");
				linkButton.Attributes["class"] = "firstpage";
				linkButton.Attributes["style"] = " color:blue;";
			}
			linkButton.Attributes["onclick"] = "toolbox_oncommand('" + str + "','待办工作查看')";
			this.GVDBSJ.PageSize = 15;
		}
	}
	private void DataBindDbsj()
	{
		string spName = "\r\n\t\t\tSELECT [I_DBSJ_ID], [I_XGID], [V_LXBM], [V_YHDM], [DTM_DBSJ], [C_OpenFlag], [V_DBLJ], [V_TPLJ], [V_Content] \r\n\t\t\tFROM [PT_DBSJ] \r\n\t\t\tWHERE [V_YHDM] = @V_YHDM AND IsOpened = 0\r\n\t\t\tUNION\r\n\t\t\tSELECT [I_DBSJ_ID], [I_XGID], [V_LXBM], [V_YHDM], [DTM_DBSJ], [C_OpenFlag], [V_DBLJ], [V_TPLJ], [V_Content] \r\n\t\t\tFROM [PT_DBSJ_Today] \r\n\t\t\tWHERE [V_YHDM] = @V_YHDM AND DTM_DBSJ <= GetDate() AND IsOpened = 0\r\n\t\t\tORDER BY [DTM_DBSJ] desc\r\n\t\t";
		SqlParameter[] commandParameters = new SqlParameter[]
		{
			new SqlParameter("@V_YHDM", base.UserCode)
		};
		DataTable dataSource = publicDbOpClass.ExecuteDataTable(CommandType.Text, spName, commandParameters);
		this.GVDBSJ.DataSource = dataSource;
		this.GVDBSJ.DataBind();
	}
	protected void GVDBSJ_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVDBSJ.PageIndex = e.NewPageIndex;
		this.DataBindDbsj();
	}
}


