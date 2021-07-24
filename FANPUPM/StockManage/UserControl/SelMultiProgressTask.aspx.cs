using ASP;
using cn.justwin.BLL;
using cn.justwin.ProgressManagement;
using cn.justwin.Serialize;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_UserControl_SelMultiProgressTask : NBasePage, IRequiresSessionState
{
	private ISerializable ser = new JsonSerializer();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindResource();
		}
	}
	protected void gvwResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["UID_"].ToString();
			string value2 = this.gvBudget.DataKeys[e.Row.RowIndex]["subCount"].ToString();
			int num = System.Convert.ToInt32(this.gvBudget.DataKeys[e.Row.RowIndex]["layer"].ToString());
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value2;
			e.Row.Attributes["projectId"] = base.Request["proVerId"];
		}
	}
	private void DataBindResource()
	{
		string text = base.Request["proVerId"];
		DataTable dataSource = null;
		if (!string.IsNullOrEmpty(text))
		{
			dataSource = ReportDetail.GetTreeTask(text);
		}
		this.gvBudget.DataSource = dataSource;
		this.gvBudget.DataBind();
	}
}


