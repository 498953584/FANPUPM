using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_ContractTaskQuery2 : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private string type = string.Empty;
	private string year = string.Empty;
	private string month = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		if (!string.IsNullOrEmpty(base.Request["month"]))
		{
			this.month = base.Request["month"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvBudget.DataKeys[e.Row.RowIndex]["TaskId"].ToString();
			string text2 = this.gvBudget.DataKeys[e.Row.RowIndex]["SubCount"].ToString();
			string text3 = this.gvBudget.DataKeys[e.Row.RowIndex]["OrderNumber"].ToString();
			int num = text3.Length / 3;
			e.Row.Attributes["id"] = text;
			e.Row.Attributes["orderNumber"] = text3;
			e.Row.Attributes["layer"] = num.ToString();
			e.Row.Attributes["subCount"] = text2;
			bool flag = text2 == "0";
			if (flag)
			{
				Image image = new Image();
				image.ImageUrl = "../../images/tree/more.gif";
				image.ToolTip = "详细信息";
				image.Attributes.Add("rowId", text);
				image.Attributes["onclick"] = "showInfo('" + text + "')";
				image.Style.Add("vertical-align", "middle");
				image.Style.Add("float", "right");
				image.Style.Add("cursor", "pointer");
				e.Row.Cells[1].Controls.Add(image);
			}
		}
	}
	public void BindGv()
	{
		if (this.month.Length == 1)
		{
			this.month = this.month.PadLeft(2, '0');
		}
		BudContractTask.GetTaskInfo(this.prjId, this.type, this.year, this.month);
		DataTable allTask = BudContractTask.GetAllTask(this.prjId, this.type, this.year, this.month);
		this.gvBudget.DataSource = allTask;
		this.gvBudget.DataBind();
	}
}


