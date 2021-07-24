using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Report_CostSummarylist : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	protected DataTable dtCostSum = new DataTable();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvCost.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.BindDrop();
			this.BindPrjInfo();
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		this.dtCostSum = ConstructReport.GetCostSummary(this.ddlPrject.SelectedValue, 1);
		this.ViewState["dtCostSum"] = this.dtCostSum;
		this.gvCost.DataSource = this.dtCostSum;
		this.gvCost.DataBind();
		string[] value = new string[]
		{
			System.Convert.ToDecimal(this.dtCostSum.Compute("sum(BudAmount)", "1>0")).ToString("0.000"),
			System.Convert.ToDecimal(this.dtCostSum.Compute("sum(Cost)", "1>0")).ToString("0.000"),
			System.Convert.ToDecimal(this.dtCostSum.Compute("sum(Disparity)", "1>0")).ToString("0.000")
		};
		int[] index = new int[]
		{
			2,
			3,
			4
		};
		GridViewUtility.AddTotalRow(this.gvCost, value, index);
	}
	private void BindDrop()
	{
		System.Collections.Generic.List<PT_PrjInfo> project = this.prjInfo.GetProject(base.UserCode);
		this.ddlPrject.DataSource = project;
		this.ddlPrject.DataTextField = "PrjName";
		this.ddlPrject.DataValueField = "PrjGuid";
		this.ddlPrject.DataBind();
		this.ddlPrject.SelectedIndex = this.ddlPrject.Items.Count - 1;
	}
	protected void gvCost_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvCost.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void ddlPrject_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.BindPrjInfo();
		this.BindGv();
	}
	private void BindPrjInfo()
	{
		PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
		PrjInfoModel prjInfoModel = new PrjInfoModel();
		if (this.ddlPrject.Items.Count > 0)
		{
			prjInfoModel = pTPrjInfoBll.GetModelByPrjGuid(this.ddlPrject.SelectedValue.Trim());
			string prjManager = prjInfoModel.PrjManager;
			this.lblPrjName.Text = prjInfoModel.PrjName;
			this.lblPrjManager.Text = prjManager.Substring(prjManager.IndexOf('-') + 1, prjManager.Length - (prjManager.IndexOf('-') + 1));
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		new Common2().ExportToExecelAll(this.GetTitleByTable((DataTable)this.ViewState["dtCostSum"]), this.ddlPrject.SelectedItem.Text + ".xls", base.Request.Browser.Browser);
	}
	protected void btnWord_Click(object sender, System.EventArgs e)
	{
		new Common2().ExportToWordAll(this.GetTitleByTable((DataTable)this.ViewState["dtCostSum"]), this.ddlPrject.SelectedItem.Text + ".doc");
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Rows.Count > 0)
		{
			if (dt.Columns["Name"] != null)
			{
				dt.Columns["Name"].ColumnName = "项目";
			}
			if (dt.Columns["BudAmount"] != null)
			{
				dt.Columns["BudAmount"].ColumnName = "目标成本";
			}
			if (dt.Columns["Cost"] != null)
			{
				dt.Columns["Cost"].ColumnName = "实际使用的陈本";
			}
			if (dt.Columns["Disparity"] != null)
			{
				dt.Columns["Disparity"].ColumnName = "差额";
			}
		}
		return dt;
	}
}


