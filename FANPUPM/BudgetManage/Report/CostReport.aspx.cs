using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_CostReport : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
		this.gvCost.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.bindGv();
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable reportTable = this.getReportTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 6, 3, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 6, 9, 0));
		list.Add(ExcelHeader.Create("目标成本", 2, 2, 3, 0));
		list.Add(ExcelHeader.Create("实际成本", 2, 2, 5, 0));
		list.Add(ExcelHeader.Create("目标成本 ", 2, 2, 9, 0));
		list.Add(ExcelHeader.Create("实际成本 ", 2, 2, 11, 0));
		foreach (DataColumn dataColumn in reportTable.Columns)
		{
			if ((dataColumn.Ordinal > 2 && dataColumn.Ordinal <= 6) || (dataColumn.Ordinal >= 9 && dataColumn.Ordinal <= 12))
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 3, 0, 0, 0));
			}
			else
			{
				if (dataColumn.Ordinal <= 2)
				{
					list.Add(ExcelHeader.Create(dataColumn.ColumnName, 3, 0, 0, 3));
				}
				else
				{
					list.Add(ExcelHeader.Create(dataColumn.ColumnName, 3, 0, 0, 2));
				}
			}
		}
		ExcelHelper.ExportExcel(reportTable, "目标成本执行情况分析", "目标成本执行情况分析", "目标成本执行情况分析.xls", list, null, 1, base.Request.Browser.Browser);
	}
	private DataTable getReportTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目编码");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("直接成本预算");
		dataTable.Columns.Add("间接成本预算");
		dataTable.Columns.Add("直接成本");
		dataTable.Columns.Add("间接成本");
		dataTable.Columns.Add("降低额");
		dataTable.Columns.Add("降低率");
		dataTable.Columns.Add("直接成本预算 ");
		dataTable.Columns.Add("间接成本预算 ");
		dataTable.Columns.Add("直接成本 ");
		dataTable.Columns.Add("间接成本 ");
		dataTable.Columns.Add("降低额 ");
		dataTable.Columns.Add("降低率 ");
		if ((this.ViewState["Cost"] as DataTable).Rows.Count != 0)
		{
			int num = 0;
			foreach (DataRow dataRow in (this.ViewState["Cost"] as DataTable).Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["项目编码"] = dataRow["PrjCode"];
				dataRow2["项目名称"] = dataRow["PrjName"];
				dataRow2["直接成本预算"] = dataRow["MonthRealitybud"];
				dataRow2["间接成本预算"] = dataRow["Monthbud"];
				dataRow2["直接成本"] = dataRow["MonthReality"];
				dataRow2["间接成本"] = dataRow["Monthacc"];
				dataRow2["降低额"] = dataRow["MonthChazhi"];
				dataRow2["降低率"] = dataRow["MonthBilu"];
				dataRow2["直接成本预算 "] = dataRow["Target"];
				dataRow2["间接成本预算 "] = dataRow["IndirBud"];
				dataRow2["直接成本 "] = dataRow["Reality"];
				dataRow2["间接成本 "] = dataRow["IndirAcc"];
				dataRow2["降低额 "] = dataRow["Chazhi"];
				dataRow2["降低率 "] = dataRow["Bilu"];
				dataTable.Rows.Add(dataRow2);
			}
			string[] array = new string[12];
			array = (this.ViewState["Total"] as string[]);
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["序号"] = "合计";
			dataRow3["项目名称"] = "";
			dataRow3["直接成本预算"] = array[0];
			dataRow3["间接成本预算"] = array[1];
			dataRow3["直接成本"] = array[2];
			dataRow3["间接成本"] = array[3];
			dataRow3["降低额"] = array[4];
			dataRow3["降低率"] = array[5];
			dataRow3["直接成本预算 "] = array[6];
			dataRow3["间接成本预算 "] = array[7];
			dataRow3["直接成本 "] = array[8];
			dataRow3["间接成本 "] = array[9];
			dataRow3["降低额 "] = array[10];
			dataRow3["降低率 "] = array[11];
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
			cells[0].RowSpan = 3;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].CssClass = "header";
			cells[1].RowSpan = 3;
			cells[1].Text = "项目编码";
			cells.Add(new TableHeaderCell());
			cells[2].CssClass = "header";
			cells[2].RowSpan = 3;
			cells[2].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[3].CssClass = "header";
			cells[3].ColumnSpan = 6;
			cells[3].Text = "本月数";
			cells.Add(new TableHeaderCell());
			cells[4].CssClass = "header";
			cells[4].ColumnSpan = 6;
			cells[4].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[5].ColumnSpan = 2;
			cells[5].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[6].ColumnSpan = 2;
			cells[6].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 2;
			cells[7].Text = "降低额";
			cells[7].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[7].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 2;
			cells[8].Text = "降低率";
			cells[8].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[8].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[9].ColumnSpan = 2;
			cells[9].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[10].ColumnSpan = 2;
			cells[10].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 2;
			cells[11].Text = "降低额";
			cells[11].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[11].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 2;
			cells[12].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[12].CssClass = "tooltip";
			cells[12].Text = "降低率</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "直接成本预算";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "间接成本预算";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "直接成本";
			cells.Add(new TableHeaderCell());
			cells[16].RowSpan = 1;
			cells[16].Text = "间接成本";
			cells.Add(new TableHeaderCell());
			cells[17].RowSpan = 1;
			cells[17].Text = "直接成本预算";
			cells.Add(new TableHeaderCell());
			cells[18].RowSpan = 1;
			cells[18].Text = "间接成本预算";
			cells.Add(new TableHeaderCell());
			cells[19].RowSpan = 1;
			cells[19].Text = "直接成本";
			cells.Add(new TableHeaderCell());
			cells[20].RowSpan = 1;
			cells[20].Text = "间接成本";
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
			e.Row.Cells[11].Text = array[8];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[9];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[10];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
			e.Row.Cells[14].Text = array[11];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].CssClass = "decimal_input";
		}
	}
	private void bindGv()
	{
		this.ViewState["Cost"] = EReport.GetBudTaskCost(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), base.UserCode, this.hfldIsWBSRelevance.Value);
		DataTable dataTable = this.ViewState["Cost"] as DataTable;
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (dataTable.Rows.Count != 0)
		{
			string[] array = new string[12];
			array[0] = dataTable.Compute("SUM(MonthRealitybud)", string.Empty).ToString();
			array[1] = dataTable.Compute("SUM(Monthbud)", string.Empty).ToString();
			array[2] = dataTable.Compute("SUM(MonthReality)", string.Empty).ToString();
			array[3] = dataTable.Compute("SUM(Monthacc)", string.Empty).ToString();
			array[4] = dataTable.Compute("SUM(MonthChazhi)", string.Empty).ToString();
			if (System.Convert.ToDecimal(array[0]) + System.Convert.ToDecimal(array[1]) != 0m)
			{
				decimal d = System.Convert.ToDecimal(array[4]) / (System.Convert.ToDecimal(array[0]) + System.Convert.ToDecimal(array[1]));
				array[5] = (decimal.Floor(d * 10000m) / 100m).ToString() + "%";
			}
			else
			{
				array[5] = "0.00%";
			}
			array[6] = dataTable.Compute("SUM(Target)", string.Empty).ToString();
			array[7] = dataTable.Compute("SUM(IndirBud)", string.Empty).ToString();
			array[8] = dataTable.Compute("SUM(Reality)", string.Empty).ToString();
			array[9] = dataTable.Compute("SUM(IndirAcc)", string.Empty).ToString();
			array[10] = dataTable.Compute("SUM(Chazhi)", string.Empty).ToString();
			if (System.Convert.ToDecimal(array[6]) + System.Convert.ToDecimal(array[7]) != 0m)
			{
				decimal d2 = System.Convert.ToDecimal(array[10]) / (System.Convert.ToDecimal(array[6]) + System.Convert.ToDecimal(array[7]));
				array[11] = (decimal.Floor(d2 * 10000m) / 100m).ToString() + "%";
			}
			else
			{
				array[11] = "0.00%";
			}
			this.ViewState["Total"] = array;
		}
		this.gvCost.DataSource = EReport.GetPageDataTable(dataTable, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvCost.DataKeyNames = new string[]
		{
			"PrjGuid"
		};
		this.gvCost.DataBind();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


