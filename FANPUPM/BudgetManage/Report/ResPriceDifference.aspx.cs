using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_ResPriceDifference : NBasePage, IRequiresSessionState
{
	private string prjId = string.Empty;
	private BudTaskResourceService budTaskResSer = new BudTaskResourceService();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
		this.AspNetPager1.RecordCount = this.budTaskResSer.GetResPriceDifferenceCount(this.prjId);
		this.gvwResPriceDiff.DataSource = this.budTaskResSer.GetResPriceDifference(this.prjId, NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.gvwResPriceDiff.DataBind();
	}
	protected void gvwResPriceDiff_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = this.budTaskResSer.GetResPriceDifference(this.prjId, 0, 0);
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 0));
		}
		ExcelHelper.ExportExcel(dataTable, "资源价差表", "资源价差表", "资源价差表.xls", list, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "资源编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "资源名称";
		}
		if (dt.Columns["UnIt"] != null)
		{
			dt.Columns["UnIt"].ColumnName = "单位";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["ResourceQuantity"] != null)
		{
			dt.Columns["ResourceQuantity"].ColumnName = "数量";
		}
		if (dt.Columns["ResourcePrice"] != null)
		{
			dt.Columns["ResourcePrice"].ColumnName = "单价";
		}
		if (dt.Columns["BudGetPrice"] != null)
		{
			dt.Columns["BudGetPrice"].ColumnName = "预算价";
		}
		if (dt.Columns["PriceDiff"] != null)
		{
			dt.Columns["PriceDiff"].ColumnName = "价差";
		}
		return dt;
	}
}


