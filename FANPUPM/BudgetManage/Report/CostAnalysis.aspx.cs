using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_CostAnalysis : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGV();
			this.lblTabulator.Text = PageHelper.QueryUser(this, base.UserCode);
			this.lblDate.Text = string.Concat(new object[]
			{
				System.DateTime.Now.Year,
				"年",
				System.DateTime.Now.Month,
				"月",
				System.DateTime.Now.Day,
				"日"
			});
		}
	}
	protected void BindGV()
	{
		if (this.ViewState["cost"] == null)
		{
			string code = base.Request["code"];
			string name = base.Request["name"];
			this.ViewState["cost"] = EReport.GetCostAnalysis(name, code);
		}
		DataTable dataTable = this.ViewState["cost"] as DataTable;
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		this.gvBudget.DataSource = BudgetManage_Report_CostAnalysis.GetPageDataTable(dataTable, currentPageIndex, NBasePage.pagesize);
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGV();
	}
	public static DataTable GetPageDataTable(DataTable dtAll, int pageIndex, int pageSize)
	{
		DataTable dataTable = dtAll.Clone();
		int num = (pageIndex - 1) * pageSize;
		int num2 = num;
		while (num2 < dtAll.Rows.Count && num2 != num + pageSize)
		{
			DataRow dataRow = dtAll.Rows[num2];
			dataTable.Rows.Add(dataRow.ItemArray);
			num2++;
		}
		return dataTable;
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable data = this.GetData();
		IExportable exporter = new ExcelExporter();
		FileExport fileExport = new FileExport(exporter, data, "项目成本分析报表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	private DataTable GetData()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("合同金额");
		dataTable.Columns.Add("预算人工费");
		dataTable.Columns.Add("预算材料费");
		dataTable.Columns.Add("预算机械费");
		dataTable.Columns.Add("预算间接成本");
		dataTable.Columns.Add("实际人工费");
		dataTable.Columns.Add("实际材料费");
		dataTable.Columns.Add("实际机械费");
		dataTable.Columns.Add("实际间接成本");
		int num = 0;
		if (this.ViewState["cost"] != null)
		{
			foreach (DataRow dataRow in (this.ViewState["cost"] as DataTable).Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["项目编号"] = dataRow["PrjCode"];
				dataRow2["项目名称"] = dataRow["PrjName"];
				dataRow2["合同金额"] = dataRow["Contract"];
				dataRow2["预算人工费"] = dataRow["BudgetRen"];
				dataRow2["预算材料费"] = dataRow["BudgetCai"];
				dataRow2["预算机械费"] = dataRow["BudgetJi"];
				dataRow2["预算间接成本"] = dataRow["Budget"];
				dataRow2["实际人工费"] = dataRow["AccountRen"];
				dataRow2["实际材料费"] = dataRow["AccountCai"];
				dataRow2["实际机械费"] = dataRow["AccountJi"];
				dataRow2["实际间接成本"] = dataRow["Account"];
				dataTable.Rows.Add(dataRow2);
			}
		}
		return dataTable;
	}
}


