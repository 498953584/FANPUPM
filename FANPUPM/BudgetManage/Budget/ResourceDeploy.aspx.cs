using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Budget_ResourceDeploy : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
			this.hfldYear.Value = this.year;
		}
	}
	public void BindGv()
	{
		cn.justwin.Domain.BudTaskChange taskChange = cn.justwin.Domain.BudTaskChange.GetTaskChange(this.prjId);
		if (taskChange != null)
		{
			DataTable taskInfo = cn.justwin.Domain.BudTask.GetTaskInfo(this.prjId, this.hfldIsWBSRelevance.Value, string.Empty, string.Empty, string.Empty);
			this.gvBudget.DataSource = taskInfo;
			this.gvBudget.DataBind();
			if (!string.IsNullOrEmpty(this.prjId))
			{
				PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
				PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
				if (byId != null)
				{
					this.lblPrjName.Text = byId.PrjName;
				}
			}
			int wBSFlowState = Project.GetWBSFlowState(this.prjId);
			bool flag = cn.justwin.Domain.BudTaskChange.IsAllowBudget(this.prjId);
			this.hfldIsLocked.Value = (!flag).ToString();
			Label expr_BC = this.lblPrjName;
			expr_BC.Text = expr_BC.Text + "(" + Common2.GetStateNoColor(wBSFlowState.ToString()) + ")";
		}
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
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
				image.Style.Add("margin-right", "0");
				image.Style.Add("cursor", "pointer");
				e.Row.Cells[2].Controls.Add(image);
			}
		}
	}
}


