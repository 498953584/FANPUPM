using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
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
public partial class BudgetManage_Report_SubProjectCompare : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
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
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			DataTable subProject = this.GetSubProject(this.AspNetPager1.CurrentPageIndex, NBasePage.pagesize);
			this.DataBindGvwSubProject(subProject);
		}
	}
	protected void rptSubProject_OnItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Footer)
		{
			Literal literal = e.Item.FindControl("ltlPlanMonthTotal") as Literal;
			Literal literal2 = e.Item.FindControl("ltlActualMonthTotal") as Literal;
			Literal literal3 = e.Item.FindControl("ltlMonthDiffTotal") as Literal;
			Literal literal4 = e.Item.FindControl("ltlMonthDiffRate") as Literal;
			Literal literal5 = e.Item.FindControl("ltlPlanTotal") as Literal;
			Literal literal6 = e.Item.FindControl("ltlActualTotal") as Literal;
			Literal literal7 = e.Item.FindControl("ltlDiffTotal") as Literal;
			Literal literal8 = e.Item.FindControl("ltlDiffRate") as Literal;
			if (this.ViewState["TotalInfo"] == null)
			{
				DataTable subProject = this.GetSubProject(0, 0);
				this.SaveTotalInfo(subProject);
			}
			string[] array = (string[])this.ViewState["TotalInfo"];
			if (literal != null)
			{
				literal.Text = array[0];
			}
			if (literal2 != null)
			{
				literal2.Text = array[1];
			}
			if (literal3 != null)
			{
				literal3.Text = array[2];
			}
			if (literal4 != null)
			{
				literal4.Text = array[3];
			}
			if (literal5 != null)
			{
				literal5.Text = array[4];
			}
			if (literal6 != null)
			{
				literal6.Text = array[5];
			}
			if (literal7 != null)
			{
				literal7.Text = array[6];
			}
			if (literal8 != null)
			{
				literal8.Text = array[7];
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		DataTable subProject = this.GetSubProject(this.AspNetPager1.CurrentPageIndex, NBasePage.pagesize);
		this.DataBindGvwSubProject(subProject);
	}
	protected void btnSearch_OnClick(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable subProject = this.GetSubProject(this.AspNetPager1.CurrentPageIndex, NBasePage.pagesize);
		this.SaveTotalInfo(subProject);
		this.DataBindGvwSubProject(subProject);
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable exportData = this.GetExportData();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 4, 7, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 4, 3, 0));
		foreach (DataColumn dataColumn in exportData.Columns)
		{
			if (dataColumn.Ordinal <= 2)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(exportData, "分项工程成本对比表", "分项工程成本对比表", "分项工程成本对比表.xls", list, null, 1, base.Request.Browser.Browser);
	}
	private void DataBindGvwSubProject(DataTable table)
	{
		this.AspNetPager1.RecordCount = EReport.GetReport(this.prjId, 0, 0, this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldIsWBSRelevance.Value).Rows.Count;
		this.rptSubProject.DataSource = table;
		this.rptSubProject.DataBind();
		if (table.Rows.Count == 0)
		{
			base.RegisterScript("$('#tableSubProject tr:last-child').remove();");
		}
	}
	private DataTable GetSubProject(int pageIndex, int pageSize)
	{
		return EReport.GetReport(this.prjId, pageSize, pageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldIsWBSRelevance.Value);
	}
	private void SaveTotalInfo(DataTable table)
	{
		string[] array = new string[8];
		array[0] = Common2.FormatDecimal(table.Compute("SUM(PlanMonthTotal)", string.Empty));
		array[1] = Common2.FormatDecimal(table.Compute("SUM(ActualMonthTotal)", string.Empty));
		array[2] = Common2.FormatDecimal(table.Compute("SUM(MonthDiffTotal)", string.Empty));
		try
		{
			array[3] = Common2.FormatRate(System.Convert.ToDecimal(array[2]) / System.Convert.ToDecimal(array[0]));
		}
		catch
		{
			array[3] = Common2.FormatRate(0);
		}
		array[4] = Common2.FormatDecimal(table.Compute("SUM(PlanTotal)", string.Empty));
		array[5] = Common2.FormatDecimal(table.Compute("SUM(ActualTotal)", string.Empty));
		array[6] = Common2.FormatDecimal(table.Compute("SUM(DiffTotal)", string.Empty));
		try
		{
			array[7] = Common2.FormatRate(System.Convert.ToDecimal(array[6]) / System.Convert.ToDecimal(array[4]));
		}
		catch
		{
			array[7] = Common2.FormatRate(0);
		}
		this.ViewState["TotalInfo"] = array;
	}
	private DataTable GetExportData()
	{
		DataTable subProject = this.GetSubProject(0, 0);
		DataTable dataTable = subProject.Clone();
		dataTable.Columns["MonthDiffRate"].DataType = typeof(string);
		dataTable.Columns["DiffRate"].DataType = typeof(string);
		for (int i = 0; i < subProject.Rows.Count; i++)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["NUM"] = Common2.FormatDecimal(subProject.Rows[i]["NUM"]);
			dataRow["TaskCode"] = Common2.FormatDecimal(subProject.Rows[i]["TaskCode"]);
			dataRow["TaskName"] = Common2.FormatDecimal(subProject.Rows[i]["TaskName"]);
			dataRow["PlanTotal"] = Common2.FormatDecimal(subProject.Rows[i]["PlanTotal"]);
			dataRow["ActualTotal"] = Common2.FormatDecimal(subProject.Rows[i]["ActualTotal"]);
			dataRow["DiffTotal"] = Common2.FormatDecimal(subProject.Rows[i]["DiffTotal"]);
			dataRow["DiffRate"] = Common2.FormatRate(subProject.Rows[i]["DiffRate"]);
			dataRow["PlanMonthTotal"] = Common2.FormatDecimal(subProject.Rows[i]["PlanMonthTotal"]);
			dataRow["ActualMonthTotal"] = Common2.FormatDecimal(subProject.Rows[i]["ActualMonthTotal"]);
			dataRow["MonthDiffTotal"] = Common2.FormatDecimal(subProject.Rows[i]["MonthDiffTotal"]);
			dataRow["MonthDiffRate"] = Common2.FormatRate(subProject.Rows[i]["MonthDiffRate"]);
			dataTable.Rows.Add(dataRow);
		}
		DataRow dataRow2 = dataTable.NewRow();
		dataRow2["NUM"] = "合计";
		dataRow2["PlanMonthTotal"] = Common2.FormatDecimal(dataTable.Compute("SUM(PlanMonthTotal)", string.Empty));
		dataRow2["ActualMonthTotal"] = Common2.FormatDecimal(dataTable.Compute("SUM(ActualMonthTotal)", string.Empty));
		dataRow2["MonthDiffTotal"] = Common2.FormatDecimal(dataTable.Compute("SUM(MonthDiffTotal)", string.Empty));
		if ((decimal)dataRow2["PlanMonthTotal"] != 0m)
		{
			dataRow2["MonthDiffRate"] = Common2.FormatRate((decimal)dataRow2["MonthDiffTotal"] / (decimal)dataRow2["PlanMonthTotal"]);
		}
		else
		{
			dataRow2["MonthDiffRate"] = Common2.FormatRate(0);
		}
		dataRow2["PlanTotal"] = Common2.FormatDecimal(dataTable.Compute("SUM(PlanTotal)", string.Empty));
		dataRow2["ActualTotal"] = Common2.FormatDecimal(dataTable.Compute("SUM(ActualTotal)", string.Empty));
		dataRow2["DiffTotal"] = Common2.FormatDecimal(dataTable.Compute("SUM(DiffTotal)", string.Empty));
		if ((decimal)dataRow2["PlanTotal"] != 0m)
		{
			dataRow2["DiffRate"] = Common2.FormatRate((decimal)dataRow2["DiffTotal"] / (decimal)dataRow2["PlanTotal"]);
		}
		else
		{
			dataRow2["DiffRate"] = Common2.FormatRate(0);
		}
		dataTable.Rows.Add(dataRow2);
		dataTable.Columns.Remove("TaskId");
		dataTable.Columns.Remove("OrderNumber");
		dataTable.Columns["NUM"].ColumnName = "序号";
		dataTable.Columns["TaskCode"].ColumnName = "任务编码";
		dataTable.Columns["TaskName"].ColumnName = "任务名称";
		dataTable.Columns["PlanTotal"].ColumnName = " 目标成本 ";
		dataTable.Columns["ActualTotal"].ColumnName = " 实际成本 ";
		dataTable.Columns["DiffTotal"].ColumnName = " 降低额 ";
		dataTable.Columns["DiffRate"].ColumnName = " 降低率 ";
		dataTable.Columns["PlanMonthTotal"].ColumnName = "目标成本";
		dataTable.Columns["ActualMonthTotal"].ColumnName = "实际成本";
		dataTable.Columns["MonthDiffTotal"].ColumnName = "降低额";
		dataTable.Columns["MonthDiffRate"].ColumnName = "降低率";
		return dataTable;
	}
}


