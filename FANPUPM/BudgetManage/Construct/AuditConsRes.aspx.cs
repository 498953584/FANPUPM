using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Construct_AuditConsRes : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string consTaskId = string.Empty;
	private string taskId = string.Empty;
	private System.Collections.Generic.List<CostAccounting> costAccountList = new System.Collections.Generic.List<CostAccounting>();

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["consTaskId"] != null)
		{
			this.consTaskId = base.Request.QueryString["consTaskId"];
		}
		if (base.Request.QueryString["taskId"] != null)
		{
			this.taskId = base.Request.QueryString["taskId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
			this.hfldConsTaskId.Value = this.consTaskId;
		}
	}
	protected void BindGv()
	{
		this.GetCBS();
		DataTable resInfo = ConstructResource.GetResInfo(this.consTaskId);
		this.gvResource.DataSource = resInfo;
		this.gvResource.DataBind();
	}
	public void GetCBS()
	{
		this.costAccountList = CostAccounting.GetByD();
		CostAccounting item = CostAccounting.Create(" ", " ", " ", " ", " ");
		this.costAccountList.Insert(0, item);
	}
	protected void gvResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvResource.DataKeys[e.Row.RowIndex]["Id"].ToString();
			DropDownList dropDownList = e.Row.FindControl("ddlCBS") as DropDownList;
			dropDownList.DataSource = this.costAccountList;
			dropDownList.DataTextField = "Name";
			dropDownList.DataValueField = "Code";
			dropDownList.DataBind();
			string text = this.gvResource.DataKeys[e.Row.RowIndex].Values["CBSCode"].ToString();
			dropDownList.SelectedValue = text.Trim();
		}
	}
}


