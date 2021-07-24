using ASP;
using cn.justwin.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Land_OilWearSummary : NBasePage, IRequiresSessionState
{
	private string startDate = string.Empty;
	private string endDate = string.Empty;
	private string prjName = string.Empty;
	private string equName = string.Empty;
	private string[] total = new string[11];

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
		if (!string.IsNullOrEmpty(this.txtEquipmentCode.Text.Trim()))
		{
			this.equName = this.txtEquipmentCode.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
		{
			this.prjName = this.txtProjectName.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEquipmentCode.Text.Trim()))
		{
			this.equName = this.txtEquipmentCode.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
	}
	protected void gvwOilWear_RowDataBound(object sender, GridViewRowEventArgs e)
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
			cells[1].RowSpan = 2;
			cells[1].Text = "项目";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "设备";
			cells.Add(new TableHeaderCell());
			cells[3].ColumnSpan = 4;
			cells[3].Text = "入库";
			cells.Add(new TableHeaderCell());
			cells[4].ColumnSpan = 2;
			cells[4].Text = "出库（安港）小计";
			cells.Add(new TableHeaderCell());
			cells[5].ColumnSpan = 2;
			cells[5].Text = "出库（外租）小计";
			cells.Add(new TableHeaderCell());
			cells[6].ColumnSpan = 2;
			cells[6].Text = "出库合计";
			cells.Add(new TableHeaderCell());
			cells[7].ColumnSpan = 2;
			cells[7].Text = "库存合计";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 2;
			cells[8].Text = "仓管员";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 2;
			cells[9].Text = "签收人</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "入库日期";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "单价（元/L）";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "数量(L)";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "金额（元）";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "数量(L) ";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "金额（元）";
			cells.Add(new TableHeaderCell());
			cells[16].RowSpan = 1;
			cells[16].Text = "数量(L) ";
			cells.Add(new TableHeaderCell());
			cells[17].RowSpan = 1;
			cells[17].Text = "金额（元）";
			cells.Add(new TableHeaderCell());
			cells[18].RowSpan = 1;
			cells[18].Text = "数量(L) ";
			cells.Add(new TableHeaderCell());
			cells[19].RowSpan = 1;
			cells[19].Text = "金额（元）";
			cells.Add(new TableHeaderCell());
			cells[20].RowSpan = 1;
			cells[20].Text = "数量(L) ";
			cells.Add(new TableHeaderCell());
			cells[21].RowSpan = 1;
			cells[21].Text = "金额（元）";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].Text = this.total[0];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].Text = this.total[1];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = this.total[2];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].Text = this.total[3];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].Text = this.total[4];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].Text = this.total[5];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].Text = this.total[6];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].Text = this.total[7];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].Text = this.total[8];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].Text = this.total[9];
			e.Row.Cells[15].Style.Add("text-align", "right");
			e.Row.Cells[15].Text = this.total[10];
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.BindGvw();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = this.GetFormatDataTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("入库", 1, 4, 3, 0));
		list.Add(ExcelHeader.Create("出库（安港）小计", 1, 2, 7, 0));
		list.Add(ExcelHeader.Create("出库（外租）小计", 1, 2, 9, 0));
		list.Add(ExcelHeader.Create("出库合计", 1, 2, 11, 0));
		list.Add(ExcelHeader.Create("库存合计", 1, 2, 13, 0));
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal < 3 || dataColumn.Ordinal >= 15)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
	}
	public DataTable GetFormatDataTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目");
		dataTable.Columns.Add("设备");
		dataTable.Columns.Add("入库日期");
		dataTable.Columns.Add("单价（元/L）");
		dataTable.Columns.Add("数量（L）");
		dataTable.Columns.Add("金额（元）");
		dataTable.Columns.Add("数量（L） ");
		dataTable.Columns.Add("金额（元） ");
		dataTable.Columns.Add("数量（L）  ");
		dataTable.Columns.Add("金额（元）  ");
		dataTable.Columns.Add("数量（L）   ");
		dataTable.Columns.Add("金额（元）   ");
		dataTable.Columns.Add("数量（L）    ");
		dataTable.Columns.Add("金额（元）    ");
		dataTable.Columns.Add("仓管员");
		dataTable.Columns.Add("签收人");
		if (!string.IsNullOrEmpty(this.txtProjectName.Text.Trim()))
		{
			this.prjName = this.txtProjectName.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEquipmentCode.Text.Trim()))
		{
			this.equName = this.txtEquipmentCode.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtStartDate.Text.Trim()))
		{
			this.startDate = this.txtStartDate.Text.Trim();
		}
		if (!string.IsNullOrEmpty(this.txtEndDate.Text.Trim()))
		{
			this.endDate = this.txtEndDate.Text.Trim();
		}
		return null;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
}


