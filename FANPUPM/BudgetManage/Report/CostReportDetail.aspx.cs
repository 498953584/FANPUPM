using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_CostReportDetail : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvCost.PageSize = NBasePage.pagesize;
		this.BindGv();
	}
	public void BindGv()
	{
		this.ViewState["Cost"] = EReport.GetTargetCostDetail(this.prjId, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		DataTable dataTable = this.ViewState["Cost"] as DataTable;
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (dataTable.Rows.Count != 0)
		{
			string[] array = new string[8];
			array[0] = dataTable.Compute("SUM(MonthBud)", string.Empty).ToString();
			array[1] = dataTable.Compute("SUM(MonthAcc)", string.Empty).ToString();
			array[2] = (System.Convert.ToDecimal(array[0]) - System.Convert.ToDecimal(array[1])).ToString();
			if (System.Convert.ToDecimal(array[0]) != 0m)
			{
				decimal d = System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0]);
				array[3] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
			}
			else
			{
				array[3] = "0.00%";
			}
			array[4] = dataTable.Compute("SUM(Bud)", string.Empty).ToString();
			array[5] = dataTable.Compute("SUM(Acc)", string.Empty).ToString();
			array[6] = (System.Convert.ToDecimal(array[4]) - System.Convert.ToDecimal(array[5])).ToString();
			if (System.Convert.ToDecimal(array[4]) != 0m)
			{
				decimal d2 = System.Convert.ToDecimal(array[6]) / System.Convert.ToDecimal(array[4]);
				array[7] = (decimal.Floor(d2 * 10000m) / 100m).ToString() + "%";
			}
			else
			{
				array[7] = "0.00%";
			}
			this.ViewState["Total"] = array;
			this.gvCost.DataSource = EReport.GetPageDataTable(dataTable, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
			this.gvCost.DataBind();
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable reportTable = this.getReportTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 4, 3, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 4, 7, 0));
		foreach (DataColumn dataColumn in reportTable.Columns)
		{
			if (dataColumn.Ordinal >= 3)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
		}
		ExcelHelper.ExportExcel(reportTable, "目标成本执行情况明细表", "目标成本执行情况明细表", "目标成本执行情况明细表.xls", list, null, 2, base.Request.Browser.Browser);
	}
	private DataTable getReportTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("CBS编码");
		dataTable.Columns.Add("成本科目");
		dataTable.Columns.Add("目标成本");
		dataTable.Columns.Add("实际成本");
		dataTable.Columns.Add("降低额");
		dataTable.Columns.Add("降低率");
		dataTable.Columns.Add("目标成本 ");
		dataTable.Columns.Add("实际成本 ");
		dataTable.Columns.Add("降低额 ");
		dataTable.Columns.Add("降低率 ");
		int num = 0;
		foreach (DataRow dataRow in (this.ViewState["Cost"] as DataTable).Rows)
		{
			num++;
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = num.ToString();
			dataRow2["CBS编码"] = dataRow["CBSCode"];
			dataRow2["成本科目"] = dataRow["CBSName"];
			dataRow2["目标成本"] = dataRow["MonthBud"];
			dataRow2["实际成本"] = dataRow["MonthAcc"];
			dataRow2["降低额"] = (decimal.Parse(dataRow["MonthBud"].ToString()) - decimal.Parse(dataRow["MonthAcc"].ToString())).ToString();
			if (dataRow["MonthBud"].ToString() == "0.000")
			{
				dataRow2["降低率"] = "0.00%";
			}
			else
			{
				dataRow2["降低率"] = ((decimal.Parse(dataRow["MonthBud"].ToString()) - decimal.Parse(dataRow["MonthAcc"].ToString())) / decimal.Parse(dataRow["MonthBud"].ToString())).ToString("P2");
			}
			dataRow2["目标成本 "] = dataRow["Bud"];
			dataRow2["实际成本 "] = dataRow["Acc"];
			dataRow2["降低额 "] = (decimal.Parse(dataRow["Bud"].ToString()) - decimal.Parse(dataRow["Acc"].ToString())).ToString();
			if (dataRow["Bud"].ToString() == "0.000")
			{
				dataRow2["降低率 "] = "0.00%";
			}
			else
			{
				dataRow2["降低率 "] = ((decimal.Parse(dataRow["Bud"].ToString()) - decimal.Parse(dataRow["Acc"].ToString())) / decimal.Parse(dataRow["Bud"].ToString())).ToString("P2");
			}
			dataTable.Rows.Add(dataRow2);
		}
		string[] array = new string[8];
		array = (this.ViewState["Total"] as string[]);
		DataRow dataRow3 = dataTable.NewRow();
		dataRow3["序号"] = "合计";
		dataRow3["CBS编码"] = "";
		dataRow3["成本科目"] = "";
		dataRow3["目标成本"] = array[0];
		dataRow3["实际成本"] = array[1];
		dataRow3["降低额"] = array[2];
		dataRow3["降低率"] = array[3];
		dataRow3["目标成本 "] = array[4];
		dataRow3["实际成本 "] = array[5];
		dataRow3["降低额 "] = array[6];
		dataRow3["降低率 "] = array[7];
		dataTable.Rows.Add(dataRow3);
		return dataTable;
	}
	protected void gvCost_RowDataBound(object sender, GridViewRowEventArgs e)
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
			cells[0].CssClass = "header";
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].CssClass = "header";
			cells[1].RowSpan = 2;
			cells[1].Text = "CBS编码";
			cells.Add(new TableHeaderCell());
			cells[2].CssClass = "header";
			cells[2].RowSpan = 2;
			cells[2].Text = "成本科目";
			cells.Add(new TableHeaderCell());
			cells[3].CssClass = "header";
			cells[3].ColumnSpan = 4;
			cells[3].Text = "本月数";
			cells.Add(new TableHeaderCell());
			cells[4].CssClass = "header";
			cells[4].ColumnSpan = 4;
			cells[4].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 1;
			cells[5].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 1;
			cells[6].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 1;
			cells[7].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[7].CssClass = "tooltip";
			cells[7].Text = "降低额";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "降低率";
			cells[8].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[8].Text = "降低率";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[11].CssClass = "tooltip";
			cells[11].Text = "降低额";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[12].CssClass = "tooltip";
			cells[12].Text = "降低率";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[3].Text = array[0];
			e.Row.Cells[3].Style.Add("text-align", "right");
			e.Row.Cells[3].CssClass = "decimal_input";
			e.Row.Cells[4].Text = array[1];
			e.Row.Cells[4].Style.Add("text-align", "right");
			e.Row.Cells[4].CssClass = "decimal_input";
			e.Row.Cells[5].Text = array[2];
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].CssClass = "decimal_input";
			e.Row.Cells[6].Text = array[3];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[7].Text = array[4];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[5];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[6];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = array[7];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
		}
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


