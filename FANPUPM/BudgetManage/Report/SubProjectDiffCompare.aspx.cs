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
public partial class BudgetManage_Report_SubProjectDiffCompare : NBasePage, IRequiresSessionState
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
			Literal literal2 = e.Item.FindControl("ltlActualMonthQty") as Literal;
			Literal literal3 = e.Item.FindControl("ltlActualMonthTotal") as Literal;
			Literal literal4 = e.Item.FindControl("ltlPlanMonthQty") as Literal;
			Literal literal5 = e.Item.FindControl("ltlMonthDiffQty") as Literal;
			Literal literal6 = e.Item.FindControl("ltlMonthDiffPrice") as Literal;
			Literal literal7 = e.Item.FindControl("ltlPlanTotal") as Literal;
			Literal literal8 = e.Item.FindControl("ltlQuantity") as Literal;
			Literal literal9 = e.Item.FindControl("ltlUnitPrice") as Literal;
			Literal literal10 = e.Item.FindControl("ltlActualTotal") as Literal;
			Literal literal11 = e.Item.FindControl("ltlActualQty") as Literal;
			Literal literal12 = e.Item.FindControl("ltlActualPrice") as Literal;
			Literal literal13 = e.Item.FindControl("ltlDiffQuantity") as Literal;
			Literal literal14 = e.Item.FindControl("ltlDiffPrice") as Literal;
			Literal literal15 = e.Item.FindControl("ltlDiffTotal") as Literal;
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
			if (literal9 != null)
			{
				literal9.Text = array[8];
			}
			if (literal10 != null)
			{
				literal10.Text = array[9];
			}
			if (literal11 != null)
			{
				literal11.Text = array[10];
			}
			if (literal12 != null)
			{
				literal12.Text = array[11];
			}
			if (literal13 != null)
			{
				literal13.Text = array[12];
			}
			if (literal14 != null)
			{
				literal14.Text = array[13];
			}
			if (literal15 != null)
			{
				literal15.Text = array[14];
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
		list.Add(ExcelHeader.Create("本月数", 1, 9, 9, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 6, 3, 0));
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
		ExcelHelper.ExportExcel(exportData, "分项工程成本-工程量差异分析表", "分项工程成本-工程量差异分析表", "分项工程成本-工程量差异分析表.xls", list, null, 1, base.Request.Browser.Browser);
	}
	private void DataBindGvwSubProject(DataTable table)
	{
		this.AspNetPager1.RecordCount = EReport.GetDiffReport(this.prjId, 0, 0, this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldIsWBSRelevance.Value).Rows.Count;
		this.rptSubProject.DataSource = table;
		this.rptSubProject.DataBind();
		if (table.Rows.Count == 0)
		{
			base.RegisterScript("$('#tableSubProject tr:last-child').remove();");
		}
	}
	private DataTable GetSubProject(int pageIndex, int pageSize)
	{
		return EReport.GetDiffReport(this.prjId, pageSize, pageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.hfldIsWBSRelevance.Value);
	}
	private void SaveTotalInfo(DataTable table)
	{
		string[] value = new string[]
		{
			Common2.FormatDecimal(table.Compute("SUM(PlanMonthTotal)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(ActualMonthQty)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(ActualMonthTotal)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(PlanMonthQty)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(MonthDiffQty)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(MonthDiffPrice)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(PlanTotal)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(Quantity)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(UnitPrice)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(ActualTotal)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(ActualQty)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(ActualPrice)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(DiffQuantity)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(DiffPrice)", string.Empty)),
			Common2.FormatDecimal(table.Compute("SUM(DiffTotal)", string.Empty))
		};
		this.ViewState["TotalInfo"] = value;
	}
	private DataTable GetExportData()
	{
		DataTable subProject = this.GetSubProject(0, 0);
		for (int i = 0; i < subProject.Rows.Count; i++)
		{
			subProject.Rows[i]["PlanMonthTotal"] = Common2.FormatDecimal(subProject.Rows[i]["PlanMonthTotal"]);
			subProject.Rows[i]["ActualMonthQty"] = Common2.FormatDecimal(subProject.Rows[i]["ActualMonthQty"]);
			subProject.Rows[i]["ActualMonthTotal"] = Common2.FormatDecimal(subProject.Rows[i]["ActualMonthTotal"]);
			subProject.Rows[i]["PlanMonthQty"] = Common2.FormatDecimal(subProject.Rows[i]["PlanMonthQty"]);
			subProject.Rows[i]["MonthDiffQty"] = Common2.FormatDecimal(subProject.Rows[i]["MonthDiffQty"]);
			subProject.Rows[i]["MonthDiffPrice"] = Common2.FormatDecimal(subProject.Rows[i]["MonthDiffPrice"]);
			subProject.Rows[i]["PlanTotal"] = Common2.FormatDecimal(subProject.Rows[i]["PlanTotal"]);
			subProject.Rows[i]["Quantity"] = Common2.FormatDecimal(subProject.Rows[i]["Quantity"]);
			subProject.Rows[i]["UnitPrice"] = Common2.FormatDecimal(subProject.Rows[i]["UnitPrice"]);
			subProject.Rows[i]["ActualTotal"] = Common2.FormatDecimal(subProject.Rows[i]["ActualTotal"]);
			subProject.Rows[i]["ActualQty"] = Common2.FormatDecimal(subProject.Rows[i]["ActualQty"]);
			subProject.Rows[i]["ActualPrice"] = Common2.FormatDecimal(subProject.Rows[i]["ActualPrice"]);
			subProject.Rows[i]["DiffQuantity"] = Common2.FormatDecimal(subProject.Rows[i]["DiffQuantity"]);
			subProject.Rows[i]["DiffPrice"] = Common2.FormatDecimal(subProject.Rows[i]["DiffPrice"]);
			subProject.Rows[i]["DiffTotal"] = Common2.FormatDecimal(subProject.Rows[i]["DiffTotal"]);
		}
		DataRow dataRow = subProject.NewRow();
		dataRow["NUM"] = "合计";
		dataRow["PlanMonthTotal"] = Common2.FormatDecimal(subProject.Compute("SUM(PlanMonthTotal)", string.Empty));
		dataRow["ActualMonthQty"] = Common2.FormatDecimal(subProject.Compute("SUM(ActualMonthQty)", string.Empty));
		dataRow["ActualMonthTotal"] = Common2.FormatDecimal(subProject.Compute("SUM(ActualMonthTotal)", string.Empty));
		dataRow["PlanMonthQty"] = Common2.FormatDecimal(subProject.Compute("SUM(PlanMonthQty)", string.Empty));
		dataRow["MonthDiffPrice"] = Common2.FormatDecimal(subProject.Compute("SUM(MonthDiffPrice)", string.Empty));
		dataRow["PlanTotal"] = Common2.FormatDecimal(subProject.Compute("SUM(PlanTotal)", string.Empty));
		dataRow["Quantity"] = Common2.FormatDecimal(subProject.Compute("SUM(Quantity)", string.Empty));
		dataRow["UnitPrice"] = Common2.FormatDecimal(subProject.Compute("SUM(UnitPrice)", string.Empty));
		dataRow["ActualTotal"] = Common2.FormatDecimal(subProject.Compute("SUM(ActualTotal)", string.Empty));
		dataRow["ActualQty"] = Common2.FormatDecimal(subProject.Compute("SUM(ActualQty)", string.Empty));
		dataRow["DiffQuantity"] = Common2.FormatDecimal(subProject.Compute("SUM(DiffQuantity)", string.Empty));
		dataRow["DiffTotal"] = Common2.FormatDecimal(subProject.Compute("SUM(DiffTotal)", string.Empty));
		subProject.Rows.Add(dataRow);
		subProject.Columns.Remove("OrderNumber");
		subProject.Columns.Remove("TaskId");
		subProject.Columns["NUM"].ColumnName = "序号";
		subProject.Columns["TaskCode"].ColumnName = "任务编码";
		subProject.Columns["TaskName"].ColumnName = "任务名称";
		subProject.Columns["PlanMonthTotal"].ColumnName = "目标成本";
		subProject.Columns["ActualMonthQty"].ColumnName = "预算量";
		subProject.Columns["ActualMonthTotal"].ColumnName = "实际成本";
		subProject.Columns["PlanMonthQty"].ColumnName = "实际量";
		subProject.Columns["MonthDiffQty"].ColumnName = "量差";
		subProject.Columns["MonthDiffPrice"].ColumnName = "价差";
		subProject.Columns["PlanTotal"].ColumnName = "目标成本 ";
		subProject.Columns["Quantity"].ColumnName = " 预算量";
		subProject.Columns["UnitPrice"].ColumnName = "目标成本价";
		subProject.Columns["ActualTotal"].ColumnName = " 实际成本 ";
		subProject.Columns["ActualQty"].ColumnName = " 实际量 ";
		subProject.Columns["ActualPrice"].ColumnName = "实际价 ";
		subProject.Columns["DiffQuantity"].ColumnName = " 量差 ";
		subProject.Columns["DiffPrice"].ColumnName = " 价差 ";
		subProject.Columns["DiffTotal"].ColumnName = "成本差额 ";
		return subProject;
	}
}


