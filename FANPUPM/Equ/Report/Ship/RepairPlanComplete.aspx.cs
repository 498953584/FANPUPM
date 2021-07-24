using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_RepairPlanComplete : NBasePage, IRequiresSessionState
{
	private EquRepairReportService reportSer = new EquRepairReportService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindGv();
		}
	}
	public void BindGv()
	{
		string startDate = this.txtStartDate.Text.Trim();
		string endDate = this.txtEndDate.Text.Trim();
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		this.AspNetPager1.RecordCount = this.reportSer.GetRepairPlanCompleteCount("0", startDate, endDate, equCode, equName);
		DataTable repairPlanComplete = this.reportSer.GetRepairPlanComplete("0", startDate, endDate, equCode, equName, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvRepair.DataSource = repairPlanComplete;
		this.gvRepair.DataBind();
	}
	protected void gvRepair_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		string startDate = this.txtStartDate.Text.Trim();
		string endDate = this.txtEndDate.Text.Trim();
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		dataTable = this.reportSer.GetRepairPlanComplete("0", startDate, endDate, equCode, equName, 0, 0);
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "船机维修保养计划完成情况表", "船机维修保养计划完成情况表", "船机维修保养计划完成情况表.xls", null, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "设备编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "设备名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["PlanRepairContent"] != null)
		{
			dt.Columns["PlanRepairContent"].ColumnName = "计划维修内容";
		}
		if (dt.Columns["PlanRepairStartDate"] != null)
		{
			dt.Columns["PlanRepairStartDate"].ColumnName = "计划维修开始日期";
		}
		if (dt.Columns["PlanRepairEndDate"] != null)
		{
			dt.Columns["PlanRepairEndDate"].ColumnName = "计划维修结束日期";
		}
		if (dt.Columns["RepairContent"] != null)
		{
			dt.Columns["RepairContent"].ColumnName = "实际维修内容";
		}
		if (dt.Columns["RepairStartDate"] != null)
		{
			dt.Columns["RepairStartDate"].ColumnName = "实际维修开始日期";
		}
		if (dt.Columns["RepairEndDate"] != null)
		{
			dt.Columns["RepairEndDate"].ColumnName = "实际维修结束日期";
		}
		if (dt.Columns["Reason"] != null)
		{
			dt.Columns["Reason"].ColumnName = "原因分析";
		}
		if (dt.Columns["V_XM"] != null)
		{
			dt.Columns["V_XM"].ColumnName = "完成人";
		}
		dt.AcceptChanges();
		return dt;
	}
}


