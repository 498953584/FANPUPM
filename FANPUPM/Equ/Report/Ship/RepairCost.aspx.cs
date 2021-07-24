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
public partial class Equ_Report_Ship_RepairCost : NBasePage, IRequiresSessionState
{
	private EquRepairReportService reportSer = new EquRepairReportService();
	private string equipmentType = "0";

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["equipmentType"]))
		{
			this.equipmentType = base.Request["equipmentType"].ToString();
		}
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
		DataTable repairCost = this.reportSer.GetRepairCost(this.equipmentType, startDate, endDate, equCode, equName, 0, 0);
		string[] array = new string[2];
		if (repairCost.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(repairCost.Compute("sum(TotalPrice)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(repairCost.Compute("sum(RepairCost)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		string startDate = this.txtStartDate.Text.Trim();
		string endDate = this.txtEndDate.Text.Trim();
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		this.AspNetPager1.RecordCount = this.reportSer.GetRepairPlanCompleteCount(this.equipmentType, startDate, endDate, equCode, equName);
		DataTable repairCost = this.reportSer.GetRepairCost(this.equipmentType, startDate, endDate, equCode, equName, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvRepair.DataSource = repairCost;
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
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[2].Text = array[0];
			e.Row.Cells[2].Style.Add("text-align", "right");
			e.Row.Cells[2].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[1];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
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
		dataTable = this.reportSer.GetRepairCost(this.equipmentType, startDate, endDate, equCode, equName, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["ReportDate"] = "合计";
			dataRow["TotalPrice"] = dataTable.Compute("sum(TotalPrice)", "1>0");
			dataRow["RepairCost"] = dataTable.Compute("sum(RepairCost)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		string text = "船机维修保养费一览表";
		if (this.equipmentType == "1")
		{
			text = "设备维修保养费一览表";
		}
		ExcelHelper.ExportExcel(dataTable, text, text, text + ".xls", null, null, 0, base.Request.Browser.Browser);
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
		if (dt.Columns["TotalPrice"] != null)
		{
			dt.Columns["TotalPrice"].ColumnName = "配件费用合计";
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
		if (dt.Columns["RepairCost"] != null)
		{
			dt.Columns["RepairCost"].ColumnName = "维修金额";
		}
		if (dt.Columns["RepairPerson"] != null)
		{
			dt.Columns["RepairPerson"].ColumnName = "维修人";
		}
		if (dt.Columns["RepairType"] != null)
		{
			dt.Columns["RepairType"].ColumnName = "维修方式";
		}
		if (dt.Columns["OutSubContractor"] != null)
		{
			dt.Columns["OutSubContractor"].ColumnName = "委外分包商";
		}
		dt.AcceptChanges();
		return dt;
	}
}


