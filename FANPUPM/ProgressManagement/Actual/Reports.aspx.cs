using ASP;
using cn.justwin.BLL;
using cn.justwin.ProgressManagement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ProgressManagement_Actual_Reports : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindReports();
		}
	}
	protected void BindReports()
	{
		string proVerId = base.Request["id"];
		DataTable reports = Report.GetReports(proVerId, 2147483647, 1);
		this.gvwReports.DataSource = reports;
		this.gvwReports.DataBind();
	}
	protected void gvwReports_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["guid"] = this.gvwReports.DataKeys[e.Row.RowIndex]["Id"].ToString();
			e.Row.Attributes["id"] = this.gvwReports.DataKeys[e.Row.RowIndex]["Id"].ToString();
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldCheckedIds.Value;
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(value);
		}
		else
		{
			list.Add(value);
		}
		string str = "删除成功";
		try
		{
			Report.Del(list);
		}
		catch (System.Exception ex)
		{
			str = "删除失败，" + ex.Message;
		}
		base.RegisterScript("alert('" + str + "');");
		this.BindReports();
	}
}


