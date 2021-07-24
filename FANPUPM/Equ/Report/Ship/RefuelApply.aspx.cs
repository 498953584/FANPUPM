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
public partial class Equ_Report_Ship_RefuelApply : NBasePage, IRequiresSessionState
{
	private EquShipRefuelApplyService applySer = new EquShipRefuelApplyService();
	private string startDate = string.Empty;
	private string endDate = string.Empty;
	private string prjName = string.Empty;
	private string shipCode = string.Empty;
	private string[] total = new string[7];

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
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtShipCode.Text.Trim()))
		{
			this.shipCode = this.txtShipCode.Text.Trim();
		}
		this.AspNetPager1.RecordCount = this.applySer.GetApplyCount(this.startDate, this.endDate, this.shipCode);
		this.gvwRefuelApply.DataSource = this.applySer.GetApplyTable(this.startDate, this.endDate, this.shipCode, NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.gvwRefuelApply.DataBind();
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtShipCode.Text.Trim()))
		{
			this.shipCode = this.txtShipCode.Text.Trim();
		}
		DataTable applyTable = this.applySer.GetApplyTable(this.startDate, this.endDate, this.shipCode, 0, 0);
		if (applyTable.Rows.Count != 0)
		{
			this.total[0] = System.Convert.ToDecimal(applyTable.Compute("sum(TotalRefuel)", "1>0")).ToString("0.00");
			this.total[1] = System.Convert.ToDecimal(applyTable.Compute("sum(StockQuantity)", "1>0")).ToString("0.00");
			this.total[2] = System.Convert.ToDecimal(applyTable.Compute("sum(ApplyQuantity)", "1>0")).ToString("0.00");
			this.total[3] = System.Convert.ToDecimal(applyTable.Compute("sum(BudCompleteQuantity)", "1>0")).ToString("0.00");
			this.total[4] = System.Convert.ToDecimal(applyTable.Compute("sum(Ratio)", "1>0")).ToString("0.00");
			this.total[5] = System.Convert.ToDecimal(applyTable.Compute("sum(Sump)", "1>0")).ToString("0.00");
			this.total[6] = System.Convert.ToDecimal(applyTable.Compute("sum(RatifyQuantity)", "1>0")).ToString("0.00");
			return;
		}
		this.total[0] = "0.00";
		this.total[1] = "0.00";
		this.total[2] = "0.00";
		this.total[3] = "0.00";
		this.total[4] = "0.00";
		this.total[5] = "0.00";
		this.total[6] = "0.00";
	}
	protected void gvwRefuelApply_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].ColumnSpan = 10;
			cells[1].Text = "加油概况";
			cells.Add(new TableHeaderCell());
			cells[2].ColumnSpan = 3;
			cells[2].Text = "审核栏</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 1;
			cells[3].Text = "船机编号";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 1;
			cells[4].Text = "开工至今该项目<br />该船累计加油数量（吨）";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 1;
			cells[5].Text = "现有库存量（吨）";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 1;
			cells[6].Text = "本次申请数量(吨）";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 1;
			cells[7].Text = "本项目<br />该船预计完成工程量（m3)";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "开工至今该船累计完成<br />该项目工程量（m3)/累计施工时间";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "挖深（m)";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "申请加油地点";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "申请加油时间";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "是否委托采购";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "申请船主";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "本次批准加油数量<br />（吨，船机部复核）";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "船机部审核人员";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[2].Style.Add("text-align", "right");
			e.Row.Cells[2].Text = this.total[0];
			e.Row.Cells[3].Style.Add("text-align", "right");
			e.Row.Cells[3].Text = this.total[1];
			e.Row.Cells[4].Style.Add("text-align", "right");
			e.Row.Cells[4].Text = this.total[2];
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].Text = this.total[3];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].Text = this.total[4];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = this.total[5];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].Text = this.total[6];
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
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtShipCode.Text.Trim()))
		{
			this.shipCode = this.txtShipCode.Text.Trim();
		}
		DataTable dataTable = this.applySer.GetApplyTable(this.startDate, this.endDate, this.shipCode, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["EquipmentCode"] = "合计";
			dataRow["TotalRefuel"] = dataTable.Compute("sum(TotalRefuel)", "1>0");
			dataRow["StockQuantity"] = dataTable.Compute("sum(StockQuantity)", "1>0");
			dataRow["ApplyQuantity"] = dataTable.Compute("sum(ApplyQuantity)", "1>0");
			dataRow["BudCompleteQuantity"] = dataTable.Compute("sum(BudCompleteQuantity)", "1>0");
			dataRow["Ratio"] = dataTable.Compute("sum(Ratio)", "1>0");
			dataRow["Sump"] = dataTable.Compute("sum(Sump)", "1>0");
			dataRow["RatifyQuantity"] = dataTable.Compute("sum(RatifyQuantity)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("加油概况", 1, 10, 1, 0));
		list.Add(ExcelHeader.Create("审核栏", 1, 3, 11, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 1)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 1)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "加油申请单", "加油申请单", "加油申请单.xls", list, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "船机编号";
		}
		if (dt.Columns["TotalRefuel"] != null)
		{
			dt.Columns["TotalRefuel"].ColumnName = "开工至今该项目该船累计加油数量（吨）";
		}
		if (dt.Columns["StockQuantity"] != null)
		{
			dt.Columns["StockQuantity"].ColumnName = "现有库存量（吨）";
		}
		if (dt.Columns["ApplyQuantity"] != null)
		{
			dt.Columns["ApplyQuantity"].ColumnName = "本次申请数量（吨）";
		}
		if (dt.Columns["BudCompleteQuantity"] != null)
		{
			dt.Columns["BudCompleteQuantity"].ColumnName = "本项目该船预计完成工程量（m3）";
		}
		if (dt.Columns["Ratio"] != null)
		{
			dt.Columns["Ratio"].ColumnName = "开工至今该船累计完成该项目工程量（m3）/累计施工时间";
		}
		if (dt.Columns["Sump"] != null)
		{
			dt.Columns["Sump"].ColumnName = "挖深（m）";
		}
		if (dt.Columns["ApplyRefuelPlace"] != null)
		{
			dt.Columns["ApplyRefuelPlace"].ColumnName = "申请加油地点";
		}
		if (dt.Columns["ApplyRefuelDate"] != null)
		{
			dt.Columns["ApplyRefuelDate"].ColumnName = "申请加油时间";
		}
		if (dt.Columns["IsEntrustPurchase"] != null)
		{
			dt.Columns["IsEntrustPurchase"].ColumnName = "是否委托采购";
		}
		if (dt.Columns["ApplyMaster"] != null)
		{
			dt.Columns["ApplyMaster"].ColumnName = "申请船主";
		}
		if (dt.Columns["RatifyQuantity"] != null)
		{
			dt.Columns["RatifyQuantity"].ColumnName = "本次批准加油数量（吨，船机部复核）";
		}
		if (dt.Columns["OperatorName"] != null)
		{
			dt.Columns["OperatorName"].ColumnName = "船机部审核人员";
		}
		dt.AcceptChanges();
		return dt;
	}
}


