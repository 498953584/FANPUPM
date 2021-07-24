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
public partial class BudgetManage_Report_MajorStuffAnalysis : NBasePage, IRequiresSessionState
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
		DataTable majorStuffAnalysis = ConstructReport.GetMajorStuffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[12];
		if (majorStuffAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentBudQuantity)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentBudCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentConsQuantity)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentConsCost)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentReductionAmount)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(BudQuantity)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(BudCost)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(ConsQuantity)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(ConsCost)", "1>0")).ToString("0.000");
			array[10] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
			array[9] = "0.000";
			array[10] = "0.000";
		}
		if (System.Convert.ToDecimal(array[1]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[4]) / System.Convert.ToDecimal(array[1]);
			array[5] = (decimal.Floor(d * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[5] = "0.00%";
		}
		if (System.Convert.ToDecimal(array[7]) != 0m)
		{
			decimal d2 = System.Convert.ToDecimal(array[10]) / System.Convert.ToDecimal(array[7]);
			array[11] = (decimal.Floor(d2 * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[11] = "0.00%";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetStuffCount(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId);
		this.dt = ConstructReport.GetMajorStuffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvMajorStuff.DataSource = this.dt;
		this.gvMajorStuff.DataBind();
	}
	protected void gvMajorStuff_RowDataBound(object sender, GridViewRowEventArgs e)
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
			cells[1].Text = "材料编号";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "材料名称";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "规格";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "品牌";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "型号";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 2;
			cells[6].Text = "技术参数";
			cells.Add(new TableHeaderCell());
			cells[7].ColumnSpan = 6;
			cells[7].Text = "本月数";
			cells.Add(new TableHeaderCell());
			cells[8].ColumnSpan = 6;
			cells[8].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "目标成本数量";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "目标成本金额";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "实际成本数量";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "实际成本金额";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "降低额";
			cells[13].Attributes.Add("title", "降低额 = 目标成本金额 &ndash; 实际成本金额");
			cells[13].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "降低率";
			cells[14].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本金额");
			cells[14].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "目标成本数量";
			cells.Add(new TableHeaderCell());
			cells[16].RowSpan = 1;
			cells[16].Text = "目标成本金额";
			cells.Add(new TableHeaderCell());
			cells.Add(new TableHeaderCell());
			cells[17].RowSpan = 1;
			cells[17].Text = "实际成本数量";
			cells[18].RowSpan = 1;
			cells[18].Text = "实际成本金额";
			cells.Add(new TableHeaderCell());
			cells[19].RowSpan = 1;
			cells[19].Text = "降低额";
			cells[19].Attributes.Add("title", "降低额 = 目标成本金额 &ndash; 实际成本金额");
			cells[19].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[20].RowSpan = 1;
			cells[20].Text = "降低率";
			cells[20].Attributes.Add("title", "降低率 = 降低额 &divide; 目标成本金额");
			cells[20].CssClass = "tooltip";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[7].Text = array[0];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[1];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[2];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = array[3];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[4];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[5];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[6];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
			e.Row.Cells[14].Text = array[7];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].CssClass = "decimal_input";
			e.Row.Cells[15].Text = array[8];
			e.Row.Cells[15].Style.Add("text-align", "right");
			e.Row.Cells[15].CssClass = "decimal_input";
			e.Row.Cells[16].Text = array[9];
			e.Row.Cells[16].Style.Add("text-align", "right");
			e.Row.Cells[16].CssClass = "decimal_input";
			e.Row.Cells[17].Text = array[10];
			e.Row.Cells[17].Style.Add("text-align", "right");
			e.Row.Cells[17].CssClass = "decimal_input";
			e.Row.Cells[18].Text = array[11];
			e.Row.Cells[18].Style.Add("text-align", "right");
			e.Row.Cells[18].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable majorStuffAnalysis = ConstructReport.GetMajorStuffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[12];
		if (majorStuffAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentBudQuantity)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentBudCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentConsQuantity)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentConsCost)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(CurrentReductionAmount)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(BudQuantity)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(BudCost)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(ConsQuantity)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(ConsCost)", "1>0")).ToString("0.000");
			array[10] = System.Convert.ToDecimal(majorStuffAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
			array[9] = "0.000";
			array[10] = "0.000";
		}
		if (System.Convert.ToDecimal(array[1]) != 0m)
		{
			decimal d = System.Convert.ToDecimal(array[4]) / System.Convert.ToDecimal(array[1]);
			array[5] = (decimal.Floor(d * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[5] = "0.00%";
		}
		if (System.Convert.ToDecimal(array[7]) != 0m)
		{
			decimal d2 = System.Convert.ToDecimal(array[10]) / System.Convert.ToDecimal(array[7]);
			array[11] = (decimal.Floor(d2 * 10000m) / 100m).ToString("0.00") + "%";
		}
		else
		{
			array[11] = "0.00%";
		}
		int[] value = new int[]
		{
			7,
			8,
			9,
			10,
			11,
			12,
			13,
			14,
			15,
			16,
			17,
			18
		};
		this.ViewState["Total"] = array;
		this.ViewState["index"] = value;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = ConstructReport.GetMajorStuffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.prjId, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["CurrentBudQuantity"] = dataTable.Compute("sum(CurrentBudQuantity)", "1>0");
			dataRow["CurrentBudCost"] = dataTable.Compute("sum(CurrentBudCost)", "1>0");
			dataRow["CurrentConsQuantity"] = dataTable.Compute("sum(CurrentConsQuantity)", "1>0");
			dataRow["CurrentConsCost"] = dataTable.Compute("sum(CurrentConsCost)", "1>0");
			dataRow["CurrentReductionAmount"] = dataTable.Compute("sum(CurrentReductionAmount)", "1>0");
			dataRow["BudQuantity"] = dataTable.Compute("sum(BudQuantity)", "1>0");
			dataRow["BudCost"] = dataTable.Compute("sum(BudCost)", "1>0");
			dataRow["ConsQuantity"] = dataTable.Compute("sum(ConsQuantity)", "1>0");
			dataRow["ConsCost"] = dataTable.Compute("sum(ConsCost)", "1>0");
			dataRow["ReductionAmount"] = dataTable.Compute("sum(ReductionAmount)", "1>0");
			if (System.Convert.ToDecimal(dataRow["CurrentBudCost"]) != 0m)
			{
				decimal d = System.Convert.ToDecimal(dataRow["CurrentReductionAmount"]) / System.Convert.ToDecimal(dataRow["CurrentBudCost"]);
				dataRow["CurrentReductionRate"] = (decimal.Floor(d * 10000m) / 100m).ToString("0.00") + "%";
			}
			else
			{
				dataRow["CurrentReductionRate"] = "0.00%";
			}
			if (System.Convert.ToDecimal(dataRow["BudCost"]) != 0m)
			{
				decimal d2 = System.Convert.ToDecimal(dataRow["ReductionAmount"]) / System.Convert.ToDecimal(dataRow["BudCost"]);
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
		list.Add(ExcelHeader.Create("本月数", 1, 6, 7, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 6, 13, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 3)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 7)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "主材分析表", "主材分析表", "主材分析表.xls", list, null, 2, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "材料编码";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "材料名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["Brand"] != null)
		{
			dt.Columns["Brand"].ColumnName = "品牌";
		}
		dt.Columns["品牌"].SetOrdinal(4);
		if (dt.Columns["ModelNumber"] != null)
		{
			dt.Columns["ModelNumber"].ColumnName = "型号";
		}
		dt.Columns["型号"].SetOrdinal(5);
		if (dt.Columns["TechnicalParameter"] != null)
		{
			dt.Columns["TechnicalParameter"].ColumnName = "技术参数";
		}
		if (dt.Columns["CurrentBudQuantity"] != null)
		{
			dt.Columns["CurrentBudQuantity"].ColumnName = " 目标成本数量 ";
		}
		if (dt.Columns["CurrentBudCost"] != null)
		{
			dt.Columns["CurrentBudCost"].ColumnName = " 目标成本金额 ";
		}
		if (dt.Columns["CurrentConsQuantity"] != null)
		{
			dt.Columns["CurrentConsQuantity"].ColumnName = " 实际成本数量 ";
		}
		if (dt.Columns["CurrentConsCost"] != null)
		{
			dt.Columns["CurrentConsCost"].ColumnName = " 实际成本金额 ";
		}
		if (dt.Columns["CurrentReductionAmount"] != null)
		{
			dt.Columns["CurrentReductionAmount"].ColumnName = " 降低额 ";
		}
		if (dt.Columns["CurrentReductionRate"] != null)
		{
			dt.Columns["CurrentReductionRate"].ColumnName = " 降低率 ";
		}
		if (dt.Columns["BudQuantity"] != null)
		{
			dt.Columns["BudQuantity"].ColumnName = "目标成本数量";
		}
		if (dt.Columns["BudCost"] != null)
		{
			dt.Columns["BudCost"].ColumnName = "目标成本金额";
		}
		if (dt.Columns["ConsQuantity"] != null)
		{
			dt.Columns["ConsQuantity"].ColumnName = "实际成本数量";
		}
		if (dt.Columns["ConsCost"] != null)
		{
			dt.Columns["ConsCost"].ColumnName = "实际成本金额";
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


