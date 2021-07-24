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
public partial class BudgetManage_Report_LSMDetailAnalysis : NBasePage, IRequiresSessionState
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
		DataTable lSMTaskAnalysis = ConstructReport.GetLSMTaskAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0);
		string[] array = new string[9];
		if (lSMTaskAnalysis.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(LaborBudCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(StuffBudCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(MachineBudCost)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(BudTotal)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(LaborConsCost)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(StuffConsCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(MachineConsCost)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(ConsTotal)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(lSMTaskAnalysis.Compute("sum(DiffTotal)", "1>0")).ToString("0.000");
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
		this.AspNetPager1.RecordCount = ConstructReport.GetLSMTaskCount(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId);
		this.dt = ConstructReport.GetLSMTaskAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
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
			cells[3].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[4].ColumnSpan = 4;
			cells[4].Text = "报量成本";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "差额</th></tr><tr class='header'>";
			cells[5].Attributes.Add("title", "差额 = 目标成本.小计 &ndash; 报量成本.小计");
			cells[5].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 1;
			cells[6].Text = "人工";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 1;
			cells[7].Text = "材料";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 1;
			cells[8].Text = "机械";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 1;
			cells[9].Text = "小计";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 1;
			cells[10].Text = "人工";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 1;
			cells[11].Text = "材料";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 1;
			cells[12].Text = "机械";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 1;
			cells[13].Text = "小计";
		}
		if (e.Row.RowType == DataControlRowType.Footer && this.ViewState["Total"] != null)
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
		dataTable = ConstructReport.GetLSMTaskAnalysis(this.txtTaskCode.Text.Trim(), this.txtTaskName.Text.Trim(), this.prjId, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["LaborBudCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(LaborBudCost)", "1>0")).ToString("0.000");
			dataRow["StuffBudCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(StuffBudCost)", "1>0")).ToString("0.000");
			dataRow["MachineBudCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(MachineBudCost)", "1>0")).ToString("0.000");
			dataRow["BudTotal"] = System.Convert.ToDecimal(dataTable.Compute("sum(BudTotal)", "1>0")).ToString("0.000");
			dataRow["LaborConsCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(LaborConsCost)", "1>0")).ToString("0.000");
			dataRow["StuffConsCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(StuffConsCost)", "1>0")).ToString("0.000");
			dataRow["MachineConsCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(MachineConsCost)", "1>0")).ToString("0.000");
			dataRow["ConsTotal"] = System.Convert.ToDecimal(dataTable.Compute("sum(ConsTotal)", "1>0")).ToString("0.000");
			dataRow["DiffTotal"] = System.Convert.ToDecimal(dataTable.Compute("sum(DiffTotal)", "1>0")).ToString("0.000");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("目标成本", 1, 4, 3, 0));
		list.Add(ExcelHeader.Create("报量成本", 1, 4, 7, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 3 && dataColumn.Ordinal < 11)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 3)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				if (dataColumn.Ordinal > 10)
				{
					list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
				}
				else
				{
					list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
				}
			}
		}
		ExcelHelper.ExportExcel(dataTable, "人材机成本明细表", "人材机成本明细表", "人材机成本明细表.xls", list, null, 2, base.Request.Browser.Browser);
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
		if (dt.Columns["LaborBudCost"] != null)
		{
			dt.Columns["LaborBudCost"].ColumnName = "人工";
		}
		if (dt.Columns["StuffBudCost"] != null)
		{
			dt.Columns["StuffBudCost"].ColumnName = "材料";
		}
		if (dt.Columns["MachineBudCost"] != null)
		{
			dt.Columns["MachineBudCost"].ColumnName = "机械";
		}
		if (dt.Columns["BudTotal"] != null)
		{
			dt.Columns["BudTotal"].ColumnName = "小计";
		}
		if (dt.Columns["LaborConsCost"] != null)
		{
			dt.Columns["LaborConsCost"].ColumnName = " 人工 ";
		}
		if (dt.Columns["StuffConsCost"] != null)
		{
			dt.Columns["StuffConsCost"].ColumnName = " 材料 ";
		}
		if (dt.Columns["MachineConsCost"] != null)
		{
			dt.Columns["MachineConsCost"].ColumnName = " 机械 ";
		}
		if (dt.Columns["ConsTotal"] != null)
		{
			dt.Columns["ConsTotal"].ColumnName = " 小计 ";
		}
		if (dt.Columns["DiffTotal"] != null)
		{
			dt.Columns["DiffTotal"].ColumnName = "差额";
		}
		dt.AcceptChanges();
		return dt;
	}
}


