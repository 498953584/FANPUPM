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
public partial class Equ_Report_Ship_ShipOilWearAnalysis : NBasePage, IRequiresSessionState
{
	private EquShipRefuelApplyService applySer = new EquShipRefuelApplyService();
	private string startDate = string.Empty;
	private string endDate = string.Empty;
	private string prjName = string.Empty;
	private string shipCode = string.Empty;
	private string[] total = new string[5];

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ComputeTotal();
			this.BindGvw();
		}
	}
	private void BindGvw()
	{
		if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
		{
			this.prjName = this.txtProjectName.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtShipCode.Text.Trim()))
		{
			this.shipCode = this.txtShipCode.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
		this.AspNetPager1.RecordCount = this.applySer.GetAnalysisCount(this.startDate, this.endDate, this.prjName, this.shipCode);
		this.gvwOilWear.DataSource = this.applySer.GetAnalysisTable(this.startDate, this.endDate, this.prjName, this.shipCode, NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.gvwOilWear.DataBind();
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
		{
			this.prjName = this.txtProjectName.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtShipCode.Text.Trim()))
		{
			this.shipCode = this.txtShipCode.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
		DataTable analysisTable = this.applySer.GetAnalysisTable(this.startDate, this.endDate, this.prjName, this.shipCode, 0, 0);
		if (analysisTable.Rows.Count != 0)
		{
			this.total[0] = System.Convert.ToDecimal(analysisTable.Compute("sum(Qty)", "1>0")).ToString("0.00");
			this.total[1] = System.Convert.ToDecimal(analysisTable.Compute("sum(Sump)", "1>0")).ToString("0.00");
			this.total[2] = System.Convert.ToDecimal(analysisTable.Compute("sum(BudOilWear)", "1>0")).ToString("0.00");
			this.total[3] = System.Convert.ToDecimal(analysisTable.Compute("sum(ApplyQuantity)", "1>0")).ToString("0.00");
			this.total[4] = System.Convert.ToDecimal(analysisTable.Compute("sum(Diff)", "1>0")).ToString("0.00");
			return;
		}
		this.total[0] = "0.00";
		this.total[1] = "0.00";
		this.total[2] = "0.00";
		this.total[3] = "0.00";
		this.total[4] = "0.00";
	}
	protected void gvwOilWear_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].Text = this.total[0];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = this.total[1];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].Text = this.total[2];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].Text = this.total[3];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].Text = this.total[4];
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.BindGvw();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = this.applySer.GetAnalysisTable(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.txtProjectName.Text.Trim(), this.txtShipCode.Text.Trim(), 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["PrjName"] = "合计";
			dataRow["Qty"] = System.Convert.ToDecimal(dataTable.Compute("sum(Qty)", "1>0")).ToString("0.00");
			dataRow["Sump"] = System.Convert.ToDecimal(dataTable.Compute("sum(Sump)", "1>0")).ToString("0.00");
			dataRow["BudOilWear"] = System.Convert.ToDecimal(dataTable.Compute("sum(BudOilWear)", "1>0")).ToString("0.00");
			dataRow["ApplyQuantity"] = System.Convert.ToDecimal(dataTable.Compute("sum(ApplyQuantity)", "1>0")).ToString("0.00");
			dataRow["Diff"] = System.Convert.ToDecimal(dataTable.Compute("sum(Diff)", "1>0")).ToString("0.00");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetFormatDataTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 0));
		}
		ExcelHelper.ExportExcel(dataTable, "单船油耗分析", "单船油耗分析", "单船油耗分析.xls", list, null, 3, base.Request.Browser.Browser);
	}
	private DataTable GetFormatDataTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["PrjName"] != null)
		{
			dt.Columns["PrjName"].ColumnName = "项目";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "分部分项";
		}
		if (dt.Columns["ConstructionPlace"] != null)
		{
			dt.Columns["ConstructionPlace"].ColumnName = "施工地点";
		}
		if (dt.Columns["QuotaOilWear"] != null)
		{
			dt.Columns["QuotaOilWear"].ColumnName = "定额油耗";
		}
		if (dt.Columns["ApplyRefuelDate"] != null)
		{
			dt.Columns["ApplyRefuelDate"].ColumnName = "施工日期";
		}
		if (dt.Columns["Qty"] != null)
		{
			dt.Columns["Qty"].ColumnName = "挖泥数量";
		}
		if (dt.Columns["Sump"] != null)
		{
			dt.Columns["Sump"].ColumnName = "挖深（m）";
		}
		if (dt.Columns["BudOilWear"] != null)
		{
			dt.Columns["BudOilWear"].ColumnName = "预算油耗";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "施工船只编号";
		}
		if (dt.Columns["ApplyQuantity"] != null)
		{
			dt.Columns["ApplyQuantity"].ColumnName = "实际加油数量";
		}
		if (dt.Columns["Diff"] != null)
		{
			dt.Columns["Diff"].ColumnName = "差异数量";
		}
		if (dt.Columns["Reason"] != null)
		{
			dt.Columns["Reason"].ColumnName = "原因分析";
		}
		dt.AcceptChanges();
		return dt;
	}
}


