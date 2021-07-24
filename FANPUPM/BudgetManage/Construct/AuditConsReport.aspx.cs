using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_AuditConsReport : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindDrop();
			this.BindTree();
			this.hfldYear.Value = this.ddlYear.SelectedValue;
			this.BindGv();
			this.hfldPrjId.Value = this.tvBudget.SelectedValue;
		}
	}
	protected void BindGv()
	{
		System.Collections.Generic.IList<ConstructReport> byPrjAudit = ConstructReport.GetByPrjAudit(this.tvBudget.SelectedValue);
		this.gvConstruct.DataSource = byPrjAudit;
		this.gvConstruct.DataBind();
	}
	protected void gvConstruct_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string text = this.gvConstruct.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = text;
			e.Row.Style.Add("cursor", "default");
			Image image = new Image();
			image.ImageUrl = "../../images/tree/more.gif";
			image.ToolTip = "详细信息";
			image.ID = "imgMore";
			image.Attributes.Add("rowId", text);
			image.Attributes["onclick"] = "showInfo('" + text + "')";
			image.Style.Add("vertical-align", "middle");
			image.Style.Add("float", "right");
			image.Style.Add("cursor", "hand");
			e.Row.Cells[2].Controls.Add(image);
			string value = this.gvConstruct.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["state"] = value;
		}
	}
	protected void btnSaveCancelReportReason_Click(object sender, System.EventArgs e)
	{
	}
	protected void btnSaveCancelAuditReason_Click(object sender, System.EventArgs e)
	{
	}
	public void BindDrop()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindDlistYears(this.ddlYear, this.Session["PmSet"], base.UserCode, base.Request["year"]);
	}
	protected void BindTree()
	{
		ProjectTree projectTree = new ProjectTree();
		projectTree.BindTreeNodes(this.tvBudget, this.Session["PmSet"], base.UserCode, base.Request["prjId"], this.ddlYear.SelectedItem.Text, this.ddlYear.SelectedValue);
	}
	protected void ddlYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindTree();
		this.hfldYear.Value = this.ddlYear.SelectedValue;
		this.hfldPrjId.Value = this.tvBudget.SelectedValue;
		this.BindGv();
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
		this.hfldPrjId.Value = this.tvBudget.SelectedValue;
	}
}


