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
public partial class Equ_Report_Land_OutRepairCost : NBasePage, IRequiresSessionState
{
	private EquRepairReportService reportSer = new EquRepairReportService();

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		string startDate = this.txtStartDate.Text.Trim();
		string endDate = this.txtEndDate.Text.Trim();
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		DataTable roadOutRepairCost = this.reportSer.GetRoadOutRepairCost(startDate, endDate, equCode, equName, 0, 0);
		string[] array = new string[3];
		if (roadOutRepairCost.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(roadOutRepairCost.Compute("sum(LaborCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(roadOutRepairCost.Compute("sum(StuffCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(roadOutRepairCost.Compute("sum(RepairCost)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		string startDate = this.txtStartDate.Text.Trim();
		string endDate = this.txtEndDate.Text.Trim();
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		this.AspNetPager1.RecordCount = this.reportSer.GetRoadOutRepairCostCount(startDate, endDate, equCode, equName);
		DataTable roadOutRepairCost = this.reportSer.GetRoadOutRepairCost(startDate, endDate, equCode, equName, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvRepair.DataSource = roadOutRepairCost;
		this.gvRepair.DataBind();
	}
	protected void gvRepair_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[11].Text = array[0];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[1];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[2];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
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
		dataTable = this.reportSer.GetRoadOutRepairCost(startDate, endDate, equCode, equName, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["EquipmentCode"] = "合计";
			dataRow["LaborCost"] = dataTable.Compute("sum(LaborCost)", "1>0");
			dataRow["StuffCost"] = dataTable.Compute("sum(StuffCost)", "1>0");
			dataRow["RepairCost"] = dataTable.Compute("sum(RepairCost)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "设备委外维修报表", "设备委外维修报表", "设备委外维修报表.xls", null, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ReportId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ReportId"]);
		}
		if (dt.Columns["ReportDate"] != null)
		{
			dt.Columns["ReportDate"].ColumnName = "上报日期";
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
		if (dt.Columns["DepName"] != null)
		{
			dt.Columns["DepName"].ColumnName = "申请部门";
		}
		if (dt.Columns["FaultDescription"] != null)
		{
			dt.Columns["FaultDescription"].ColumnName = "故障简介";
		}
		if (dt.Columns["OutCompany"] != null)
		{
			dt.Columns["OutCompany"].ColumnName = "委外维修公司";
		}
		if (dt.Columns["OutDepartment"] != null)
		{
			dt.Columns["OutDepartment"].ColumnName = "委外维修部门";
		}
		if (dt.Columns["RepairStartDate"] != null)
		{
			dt.Columns["RepairStartDate"].ColumnName = "实际维修开始日期";
		}
		if (dt.Columns["RepairEndDate"] != null)
		{
			dt.Columns["RepairEndDate"].ColumnName = "实际维修结束日期";
		}
		if (dt.Columns["LaborCost"] != null)
		{
			dt.Columns["LaborCost"].ColumnName = "人工费";
		}
		if (dt.Columns["StuffCost"] != null)
		{
			dt.Columns["StuffCost"].ColumnName = "材料费";
		}
		if (dt.Columns["RepairCost"] != null)
		{
			dt.Columns["RepairCost"].ColumnName = "维修总费用";
		}
		if (dt.Columns["RepairPerson"] != null)
		{
			dt.Columns["RepairPerson"].ColumnName = "维修人";
		}
		if (dt.Columns["AcceptorName"] != null)
		{
			dt.Columns["AcceptorName"].ColumnName = "验收人";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		dt.AcceptChanges();
		return dt;
	}
}


