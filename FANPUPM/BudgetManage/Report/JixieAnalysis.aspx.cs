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
public partial class BudgetManage_Report_JixieAnalysis : NBasePage, IRequiresSessionState
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
		this.BindGv();
	}
	public void BindGv()
	{
		this.ViewState["Jixie"] = EReport.GetJixieAnalysis(this.prjId, this.txtName.Text.Trim());
		DataTable dataTable = this.ViewState["Jixie"] as DataTable;
		if (dataTable.Rows.Count != 0)
		{
			string[] array = new string[8];
			array[0] = dataTable.Compute("SUM(MonthTotalBud)", string.Empty).ToString();
			array[1] = dataTable.Compute("SUM(MonthTotalAcc)", string.Empty).ToString();
			array[2] = dataTable.Compute("SUM(MonthChazhi)", string.Empty).ToString();
			if (System.Convert.ToDecimal(array[0]) != 0m)
			{
				array[3] = (System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0])).ToString("P2");
			}
			else
			{
				array[3] = "0.00%";
			}
			array[4] = dataTable.Compute("SUM(TotalBud)", string.Empty).ToString();
			array[5] = dataTable.Compute("SUM(TotalAcc)", string.Empty).ToString();
			array[6] = dataTable.Compute("SUM(Chazhi)", string.Empty).ToString();
			if (System.Convert.ToDecimal(array[4]) != 0m)
			{
				array[7] = (System.Convert.ToDecimal(array[6]) / System.Convert.ToDecimal(array[4])).ToString("P2");
			}
			else
			{
				array[7] = "0.00%";
			}
			this.ViewState["Total"] = array;
		}
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.gvCost.DataSource = EReport.GetPageDataTable(dataTable, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvCost.DataBind();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable reportTable = this.getReportTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 4, 2, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 4, 6, 0));
		foreach (DataColumn dataColumn in reportTable.Columns)
		{
			if (dataColumn.Ordinal >= 2)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
		}
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		list2.Add(2);
		list2.Add(3);
		list2.Add(6);
		list2.Add(7);
		ExcelHelper.ExportExcel(reportTable, "机械费分析表", "机械费分析表", "机械费分析表.xls", list, null, 2, base.Request.Browser.Browser);
	}
	private DataTable getReportTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("机械名称");
		dataTable.Columns.Add("目标成本");
		dataTable.Columns.Add("实际成本");
		dataTable.Columns.Add("降低额");
		dataTable.Columns.Add("降低率");
		dataTable.Columns.Add("目标成本 ");
		dataTable.Columns.Add("实际成本 ");
		dataTable.Columns.Add("降低额 ");
		dataTable.Columns.Add("降低率 ");
		if ((this.ViewState["Jixie"] as DataTable).Rows.Count != 0)
		{
			int num = 0;
			foreach (DataRow dataRow in (this.ViewState["Jixie"] as DataTable).Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["机械名称"] = dataRow["ResourceName"];
				dataRow2["目标成本"] = dataRow["MonthTotalBud"];
				dataRow2["实际成本"] = dataRow["MonthTotalAcc"];
				dataRow2["降低额"] = dataRow["MonthChazhi"];
				dataRow2["降低率"] = dataRow["Monthbilu"];
				dataRow2["目标成本 "] = dataRow["TotalBud"];
				dataRow2["实际成本 "] = dataRow["TotalAcc"];
				dataRow2["降低额 "] = dataRow["Chazhi"];
				dataRow2["降低率 "] = dataRow["Bilu"];
				dataTable.Rows.Add(dataRow2);
			}
			string[] array = new string[8];
			array = (this.ViewState["Total"] as string[]);
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["序号"] = "合计";
			dataRow3["机械名称"] = "";
			dataRow3["目标成本"] = array[0];
			dataRow3["实际成本"] = array[1];
			dataRow3["降低额"] = array[2];
			dataRow3["降低率"] = array[3];
			dataRow3["目标成本 "] = array[4];
			dataRow3["实际成本 "] = array[5];
			dataRow3["降低额 "] = array[6];
			dataRow3["降低率 "] = array[7];
			dataTable.Rows.Add(dataRow3);
		}
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
			cells[1].Text = "机械名称";
			cells.Add(new TableHeaderCell());
			cells[2].CssClass = "header";
			cells[2].ColumnSpan = 4;
			cells[2].Text = "本月数";
			cells.Add(new TableHeaderCell());
			cells[3].CssClass = "header";
			cells[3].ColumnSpan = 4;
			cells[3].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 1;
			cells[4].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 1;
			cells[5].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 1;
			cells[6].Text = "降低额";
			cells[6].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[6].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 1;
			cells[7].Text = "降低率";
			cells[7].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[7].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "降低额";
			cells[10].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[10].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "降低率";
			cells[11].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[11].CssClass = "tooltip";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[2].Text = array[0];
			e.Row.Cells[2].Style.Add("text-align", "right");
			e.Row.Cells[2].CssClass = "decimal_input";
			e.Row.Cells[3].Text = array[1];
			e.Row.Cells[3].Style.Add("text-align", "right");
			e.Row.Cells[3].CssClass = "decimal_input";
			e.Row.Cells[4].Text = array[2];
			e.Row.Cells[4].Style.Add("text-align", "right");
			e.Row.Cells[4].CssClass = "decimal_input";
			e.Row.Cells[5].Text = array[3];
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].CssClass = "decimal_input";
			e.Row.Cells[6].Text = array[4];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[7].Text = array[5];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[6];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[7];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
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


