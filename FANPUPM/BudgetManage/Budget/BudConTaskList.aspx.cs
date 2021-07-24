using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Serialize;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudConTaskList : NBasePage, IRequiresSessionState
{
	private ISerializable ser = new JsonSerializer();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindBudget();
		}
	}
	protected void gvwResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string value2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text.Length / 3;
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["orderNumber"] = text;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = value2;
		}
	}
	private void DataBindBudget()
	{
		string text = base.Request["prjId"];
		if (!string.IsNullOrEmpty(text))
		{
			DataTable taskInfo = BudContractTask.GetTaskInfo(text, string.Empty, string.Empty, string.Empty);
			this.gvBudget.DataSource = taskInfo;
			this.gvBudget.DataBind();
		}
	}
}


