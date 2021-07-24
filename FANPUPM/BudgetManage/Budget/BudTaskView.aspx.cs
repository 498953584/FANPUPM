using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_BudTaskView : NBasePage, IRequiresSessionState
{
	private string taskChangeId = "";
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			this.taskChangeId = base.Request["ic"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
		this.BindData();
	}
	public void BindData()
	{
		BudTaskChange model = BudTaskChange.GetModel(this.taskChangeId);
		if (model != null)
		{
			this.lblPrjName.Text = model.PrjName;
			this.lblVersionCode.Text = model.VersionCode;
			this.lblUserCode.Text = WebUtil.GetUserNames(model.InputUser);
			this.lblNote.Text = model.Note;
			this.hfldPrjId.Value = model.PrjId;
			DataTable taskInfo = BudTask.GetTaskInfo(model.PrjId, this.hfldIsWBSRelevance.Value, string.Empty, string.Empty, string.Empty);
			this.gvBudget.DataSource = taskInfo;
			this.gvBudget.DataBind();
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
			string value = this.gvBudget.DataKeys[e.Row.RowIndex]["Modified"].ToString();
			if (!string.IsNullOrEmpty(value))
			{
				e.Row.Style.Add("color", "red");
				HyperLink hyperLink = e.Row.FindControl("hlinkShowNote") as HyperLink;
				if (hyperLink != null)
				{
					hyperLink.Style.Add("color", "red");
				}
			}
			bool flag = text2 == "0";
			if (flag && this.hfldIsWBSRelevance.Value == "1")
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
}


