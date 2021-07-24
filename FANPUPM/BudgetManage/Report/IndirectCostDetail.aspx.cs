using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_IndirectCostDetail : NBasePage, IRequiresSessionState
{
	private static string prjId = string.Empty;
	private static string cBSCode = string.Empty;
	private static string cBSName = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			BudgetManage_Report_IndirectCostDetail.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["cbsCode"]))
		{
			BudgetManage_Report_IndirectCostDetail.cBSCode = base.Request["cbsCode"];
		}
		if (!string.IsNullOrEmpty(base.Request["cbsName"]))
		{
			BudgetManage_Report_IndirectCostDetail.cBSName = base.Request["cbsName"];
		}
		this.AspNetPager1.CurrentPageIndex = 1;
		if (!base.IsPostBack)
		{
			this.DataBindContract();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.DataBindContract();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		IExportable exportable = new ExcelExporter();
		exportable.PercentColumns = new int[]
		{
			8
		};
		string fileName = this.GetPrjName() + "的" + BudgetManage_Report_IndirectCostDetail.cBSName + "明细表.xls";
		FileExport fileExport = new FileExport(exportable, reportData, fileName);
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContract();
	}
	private void DataBindContract()
	{
		DataTable indirectCostDetails = EReport.GetIndirectCostDetails(BudgetManage_Report_IndirectCostDetail.prjId, BudgetManage_Report_IndirectCostDetail.cBSCode, this.txtDiaryName.Text.Trim(), this.txtIssuedBy.Text.Trim(), this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim());
		this.ViewState["CostDetail"] = indirectCostDetails;
		this.AspNetPager1.RecordCount = indirectCostDetails.Rows.Count;
		int num = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize;
		DataTable dataTable;
		if (indirectCostDetails.Rows.Count <= this.AspNetPager1.PageSize)
		{
			dataTable = indirectCostDetails;
		}
		else
		{
			dataTable = indirectCostDetails.Clone();
			dataTable.Clear();
			int num2 = num;
			while (num2 < num + this.AspNetPager1.PageSize && indirectCostDetails.Rows.Count > num2)
			{
				dataTable.Rows.Add(indirectCostDetails.Rows[num2].ItemArray);
				num2++;
			}
		}
		string total = indirectCostDetails.Compute("SUM(Amount)", string.Empty).ToString();
		this.gvwCostDetail.DataSource = dataTable;
		this.gvwCostDetail.DataBind();
		BudgetManage_Report_IndirectCostDetail.AddTotalRow(this.gvwCostDetail, total, 6, indirectCostDetails.Columns.Count);
	}
	public static void AddTotalRow(GridView gvw, string total, int totalColumnIndex, int columnNum)
	{
		GridViewRow gridViewRow = new GridViewRow(0, 0, DataControlRowType.DataRow, DataControlRowState.Normal);
		TableCell tableCell = new TableCell();
		tableCell.Attributes["align"] = "center";
		Label label = new Label();
		label.Text = "合计";
		tableCell.Controls.Add(label);
		TableCell tableCell2 = new TableCell();
		tableCell2.Style.Add(HtmlTextWriterStyle.TextAlign, TextAlign.Right.ToString());
		Label label2 = new Label();
		label2.CssClass = "_total_";
		label2.Text = total;
		tableCell2.Controls.Add(label2);
		gridViewRow.Cells.Add(tableCell);
		for (int i = 1; i < columnNum; i++)
		{
			if (i == totalColumnIndex)
			{
				gridViewRow.Cells.Add(tableCell2);
			}
			else
			{
				gridViewRow.Cells.Add(new TableCell());
			}
		}
		if (gvw.Rows.Count > 0)
		{
			gvw.Controls[0].Controls.AddAt(gvw.Rows.Count + 1, gridViewRow);
		}
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = this.ViewState["CostDetail"] as DataTable;
		dataTable.Columns["Num"].ColumnName = "序号";
		dataTable.Columns["CostName"].ColumnName = "名称";
		dataTable.Columns["IssuedBy"].ColumnName = "经手人";
		dataTable.Columns["InputUser"].ColumnName = "录入人";
		dataTable.Columns["DepartMent"].ColumnName = "发生单位";
		dataTable.Columns["InputDate"].ColumnName = "发生时间";
		dataTable.Columns["Amount"].ColumnName = "金额";
		dataTable.Columns["Note"].ColumnName = "备注";
		dataTable.Compute("SUM(金额)", string.Empty).ToString();
		DataRow dataRow = dataTable.NewRow();
		dataRow["名称"] = "合计";
		dataRow["金额"] = dataTable.Compute("SUM(金额)", string.Empty).ToString();
		dataTable.Rows.Add(dataRow);
		return dataTable;
	}
	private string GetPrjName()
	{
		PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
		PTPrjInfo byId = pTPrjInfoService.GetById(BudgetManage_Report_IndirectCostDetail.prjId);
		return byId.PrjName;
	}
}


