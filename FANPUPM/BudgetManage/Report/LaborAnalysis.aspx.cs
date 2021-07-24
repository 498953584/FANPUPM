using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_LaborAnalysis : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private DataTable dt = new DataTable();
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
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
			this.hfldPrjId.Value = this.prjId;
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable laborAnalysis = ConstructReport.GetLaborAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[8];
		if (laborAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(CurrentLaborBudCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(CurrentLaborConsCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(CurrentReductionAmount)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(LaborBudCost)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(LaborConsCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
		}
		if (System.Convert.ToDecimal(array[0]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0]);
			array[3] = (decimal.Floor(d * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[3] = "0.00%";
		}
		if (System.Convert.ToDecimal(array[4]) != 0m)
		{
			decimal d2 = System.Convert.ToDecimal(array[6]) / System.Convert.ToDecimal(array[4]);
			array[7] = (decimal.Floor(d2 * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[7] = "0.00%";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetLaborAnalysisCount(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId);
		this.dt = ConstructReport.GetLaborAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvCost.DataSource = this.dt;
		this.gvCost.DataBind();
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
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 2;
			cells[1].Text = "任务编码";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "任务名称";
			cells.Add(new TableHeaderCell());
			cells[3].ColumnSpan = 4;
			cells[3].Text = "本月数";
			cells.Add(new TableHeaderCell());
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
			cells[7].Text = "降低额";
			cells[7].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[7].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "降低率";
			cells[8].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[8].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "降低额";
			cells[11].Attributes.Add("title", "降低额 = 目标成本 &ndash; 实际成本");
			cells[11].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "降低率";
			cells[12].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本");
			cells[12].CssClass = "tooltip";
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
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable laborAnalysis = ConstructReport.GetLaborAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[8];
		if (laborAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(CurrentLaborBudCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(CurrentLaborConsCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(CurrentReductionAmount)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(LaborBudCost)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(LaborConsCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(laborAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
		}
		if (System.Convert.ToDecimal(array[0]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0]);
			array[3] = (decimal.Floor(d * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[3] = "0.00%";
		}
		if (System.Convert.ToDecimal(array[4]) != 0m)
		{
			decimal d2 = System.Convert.ToDecimal(array[6]) / System.Convert.ToDecimal(array[4]);
			array[7] = (decimal.Floor(d2 * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[7] = "0.00%";
		}
		this.ViewState["Total"] = array;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = ConstructReport.GetLaborAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["CurrentLaborBudCost"] = dataTable.Compute("sum(CurrentLaborBudCost)", "1>0");
			dataRow["CurrentLaborConsCost"] = dataTable.Compute("sum(CurrentLaborConsCost)", "1>0");
			dataRow["CurrentReductionAmount"] = dataTable.Compute("sum(CurrentReductionAmount)", "1>0");
			dataRow["LaborBudCost"] = dataTable.Compute("sum(LaborBudCost)", "1>0");
			dataRow["LaborConsCost"] = dataTable.Compute("sum(LaborConsCost)", "1>0");
			dataRow["ReductionAmount"] = dataTable.Compute("sum(ReductionAmount)", "1>0");
			if (System.Convert.ToDecimal(dataRow["CurrentLaborBudCost"]) != 0m)
			{
				decimal d = System.Convert.ToDecimal(dataRow["CurrentReductionAmount"]) / System.Convert.ToDecimal(dataRow["CurrentLaborBudCost"]);
				dataRow["CurrentReductionRate"] = (decimal.Floor(d * 10000m) / 100m).ToString("0.00") + "%";
			}
			else
			{
				dataRow["CurrentReductionRate"] = "0.00%";
			}
			if (System.Convert.ToDecimal(dataRow["LaborBudCost"]) != 0m)
			{
				decimal d2 = System.Convert.ToDecimal(dataRow["ReductionAmount"]) / System.Convert.ToDecimal(dataRow["LaborBudCost"]);
				dataRow["ReductionRate"] = (decimal.Floor(d2 * 10000m) / 100m).ToString("0.00") + "%";
			}
			else
			{
				dataRow["ReductionRate"] = "0.00%";
			}
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 4, 3, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 4, 7, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 3)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 3)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "人工分析表", "人工分析表", "人工分析表.xls", list, null, 2, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["TaskCode"] != null)
		{
			dt.Columns["TaskCode"].ColumnName = "任务编码";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "任务名称";
		}
		if (dt.Columns["CurrentLaborBudCost"] != null)
		{
			dt.Columns["CurrentLaborBudCost"].ColumnName = " 目标成本 ";
		}
		if (dt.Columns["CurrentLaborConsCost"] != null)
		{
			dt.Columns["CurrentLaborConsCost"].ColumnName = " 实际成本 ";
		}
		if (dt.Columns["CurrentReductionAmount"] != null)
		{
			dt.Columns["CurrentReductionAmount"].ColumnName = " 降低额 ";
		}
		if (dt.Columns["CurrentReductionRate"] != null)
		{
			dt.Columns["CurrentReductionRate"].ColumnName = " 降低率 ";
		}
		if (dt.Columns["LaborBudCost"] != null)
		{
			dt.Columns["LaborBudCost"].ColumnName = "目标成本";
		}
		if (dt.Columns["LaborConsCost"] != null)
		{
			dt.Columns["LaborConsCost"].ColumnName = "实际成本";
		}
		if (dt.Columns["ReductionAmount"] != null)
		{
			dt.Columns["ReductionAmount"].ColumnName = "降低额";
		}
		if (dt.Columns["ReductionRate"] != null)
		{
			dt.Columns["ReductionRate"].ColumnName = "降低率";
		}
		dt.AcceptChanges();
		return dt;
	}
}


