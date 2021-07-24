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
public partial class BudgetManage_Report_MajorStuffDiffAnalysis : NBasePage, IRequiresSessionState
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
		DataTable majorStuffDiffAnalysis = ConstructReport.GetMajorStuffDiffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[9];
		if (majorStuffDiffAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentBudCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentConsCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentDiffPrice)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentReductionAmount)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(BudCost)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(ConsCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(DiffQuantity)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(DiffPrice)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetStuffCount(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), string.Empty, string.Empty, this.prjId);
		this.dt = ConstructReport.GetMajorStuffDiffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.prjId, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
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
			cells[7].ColumnSpan = 4;
			cells[7].Text = "本月数";
			cells.Add(new TableHeaderCell());
			cells[8].ColumnSpan = 5;
			cells[8].Text = "累计数</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "价差";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "差异金额";
			cells[12].Attributes.Add("title", "差异金额 = 目标成本 &ndash; 实际成本");
			cells[12].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "实际成本";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "量差";
			cells.Add(new TableHeaderCell());
			cells[16].RowSpan = 1;
			cells[16].Text = "价差";
			cells.Add(new TableHeaderCell());
			cells[17].RowSpan = 1;
			cells[17].Text = "差异金额";
			cells[17].Attributes.Add("title", "差异金额 = 目标成本 &ndash; 实际成本");
			cells[17].CssClass = "tooltip";
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
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable majorStuffDiffAnalysis = ConstructReport.GetMajorStuffDiffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[9];
		if (majorStuffDiffAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentBudCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentConsCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentDiffPrice)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(CurrentReductionAmount)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(BudCost)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(ConsCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(DiffQuantity)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(DiffPrice)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(majorStuffDiffAnalysis.Compute("sum(ReductionAmount)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
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
		dataTable = ConstructReport.GetMajorStuffDiffAnalysis(this.txtResourceCode.Text.Trim(), this.txtResourceName.Text.Trim(), this.prjId, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["CurrentBudCost"] = dataTable.Compute("sum(CurrentBudCost)", "1>0");
			dataRow["CurrentConsCost"] = dataTable.Compute("sum(CurrentConsCost)", "1>0");
			dataRow["CurrentDiffPrice"] = dataTable.Compute("sum(CurrentDiffPrice)", "1>0");
			dataRow["CurrentReductionAmount"] = dataTable.Compute("sum(CurrentReductionAmount)", "1>0");
			dataRow["BudCost"] = dataTable.Compute("sum(BudCost)", "1>0");
			dataRow["ConsCost"] = dataTable.Compute("sum(ConsCost)", "1>0");
			dataRow["DiffQuantity"] = dataTable.Compute("sum(DiffQuantity)", "1>0");
			dataRow["DiffPrice"] = dataTable.Compute("sum(DiffPrice)", "1>0");
			dataRow["ReductionAmount"] = dataTable.Compute("sum(ReductionAmount)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 4, 7, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 5, 11, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 7)
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
		ExcelHelper.ExportExcel(dataTable, "主材差异分析表", "主材差异分析表", "主材差异分析表.xls", list, null, 2, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ResourceId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ResourceId"]);
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
		if (dt.Columns["ModelNumber"] != null)
		{
			dt.Columns["ModelNumber"].ColumnName = "型号";
		}
		if (dt.Columns["TechnicalParameter"] != null)
		{
			dt.Columns["TechnicalParameter"].ColumnName = "技术参数";
		}
		if (dt.Columns["CurrentBudCost"] != null)
		{
			dt.Columns["CurrentBudCost"].ColumnName = " 目标成本 ";
		}
		if (dt.Columns["CurrentConsCost"] != null)
		{
			dt.Columns["CurrentConsCost"].ColumnName = " 实际成本 ";
		}
		if (dt.Columns["CurrentDiffPrice"] != null)
		{
			dt.Columns["CurrentDiffPrice"].ColumnName = " 价差 ";
		}
		if (dt.Columns["CurrentReductionAmount"] != null)
		{
			dt.Columns["CurrentReductionAmount"].ColumnName = " 差异金额 ";
		}
		if (dt.Columns["BudCost"] != null)
		{
			dt.Columns["BudCost"].ColumnName = "目标成本";
		}
		if (dt.Columns["ConsCost"] != null)
		{
			dt.Columns["ConsCost"].ColumnName = "实际成本";
		}
		if (dt.Columns["DiffQuantity"] != null)
		{
			dt.Columns["DiffQuantity"].ColumnName = "量差";
		}
		if (dt.Columns["DiffPrice"] != null)
		{
			dt.Columns["DiffPrice"].ColumnName = "价差";
		}
		if (dt.Columns["ReductionAmount"] != null)
		{
			dt.Columns["ReductionAmount"].ColumnName = "差异金额";
		}
		dt.AcceptChanges();
		return dt;
	}
}


