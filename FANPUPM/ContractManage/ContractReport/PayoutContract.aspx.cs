using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Tender;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractReport_PayoutContract : NBasePage, IRequiresSessionState
{
	private DataTable contractCount = new DataTable();
	private PayoutContract payoutContract = new PayoutContract();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			this.DataBindContract();
		}
	}
	private void DataBindContract()
	{
		DataTable data = this.GetData();
		this.gvwContract.DataSource = data;
		this.gvwContract.DataBind();
		string[] value = new string[]
		{
			this.contractCount.Compute("SUM(ContractMoney)", string.Empty).ToString(),
			this.contractCount.Compute("SUM(ModifiedMoney)", string.Empty).ToString(),
			this.contractCount.Compute("SUM(ModifyTotal)", string.Empty).ToString(),
			this.contractCount.Compute("SUM(BalanceTotal)", string.Empty).ToString(),
			this.contractCount.Compute("SUM(PaymentTotal)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			5,
			6,
			7,
			9,
			11
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isMainContract"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[1].ToString();
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
		FileExport fileExport = new FileExport(exportable, reportData, "支出合同报表.xls");
		exportable.PercentColumns = new int[]
		{
			11,
			12,
			13,
			14,
			16
		};
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContract();
	}
	private DataTable GetData()
	{
		System.DateTime startDate = Common2.GetStartDate(this.txtStartDate);
		System.DateTime endDate = Common2.GetEndDate(this.txtEndDate);
		string reportCondition = this.payoutContract.GetReportCondition(startDate, endDate, this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), this.txtContractName.Text.Trim(), this.txtContractCode.Text.Trim(), this.txtBName.Text.Trim(), this.dropPrjKindClass.SelectedItem.Text, this.txtSignPersonName.Text.Trim());
		this.contractCount = this.payoutContract.Select(reportCondition, base.UserCode);
		this.ViewState["contract"] = this.contractCount;
		this.AspNetPager1.RecordCount = this.contractCount.Rows.Count;
		return this.payoutContract.SelectData(reportCondition, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = this.ViewState["contract"] as DataTable;
		dataTable.Columns.Remove("ContractID");
		dataTable.Columns.Remove("Version");
		dataTable.Columns.Remove("IsMainContract");
		dataTable.Columns.Remove("UserCodes");
		dataTable.Columns.Remove("Date");
		dataTable.Columns.Remove("BName");
		dataTable.Columns["Num"].ColumnName = "序号";
		dataTable.Columns["ContractCode"].ColumnName = "合同编号";
		dataTable.Columns["ContractName"].ColumnName = "合同名称";
		dataTable.Columns["CorpName"].ColumnName = "乙方";
		dataTable.Columns["ContractMoney"].ColumnName = "合同金额";
		dataTable.Columns["ModifiedMoney"].ColumnName = "变更后金额";
		dataTable.Columns["ModifyTotal"].ColumnName = "变更累计";
		dataTable.Columns["ModifyRate"].ColumnName = "变更比率";
		dataTable.Columns["BalanceTotal"].ColumnName = "结算累计";
		dataTable.Columns["BalanceRate"].ColumnName = "结算比率";
		dataTable.Columns["PaymentTotal"].ColumnName = "付款累计";
		dataTable.Columns["PaymentRate"].ColumnName = "付款比率";
		dataTable.Columns["PaymentBalanceRate"].ColumnName = "付款结算比率";
		dataTable.Columns["InvoiceSum"].ColumnName = "发票累计";
		dataTable.Columns["InvoiceRate"].ColumnName = "已开发票比率";
		dataTable.Columns.Add(new DataColumn("合同状态", typeof(string)));
		foreach (DataRow dataRow in dataTable.Rows)
		{
			dataRow["合同状态"] = WebUtil.GetConState(dataRow["conState"].ToString());
		}
		dataTable.Columns.Remove(dataTable.Columns["conState"]);
		dataTable.Columns["SignDate"].ColumnName = "签订时间";
		dataTable.Columns["TypeName"].ColumnName = "合同类型";
		dataTable.Columns["PrjName"].ColumnName = "项目";
		dataTable.Columns["CodeName"].ColumnName = "项目类型";
		dataTable.Columns["乙方"].SetOrdinal(3);
		dataTable.Columns["合同金额"].SetOrdinal(4);
		dataTable.Columns["变更后金额"].SetOrdinal(5);
		dataTable.Columns["变更累计"].SetOrdinal(6);
		dataTable.Columns["变更比率"].SetOrdinal(7);
		dataTable.Columns["结算累计"].SetOrdinal(8);
		dataTable.Columns["结算比率"].SetOrdinal(9);
		dataTable.Columns["付款累计"].SetOrdinal(10);
		dataTable.Columns["付款比率"].SetOrdinal(11);
		dataTable.Columns["付款结算比率"].SetOrdinal(12);
		dataTable.Columns["发票累计"].SetOrdinal(13);
		dataTable.Columns["已开发票比率"].SetOrdinal(14);
		dataTable.Columns["合同状态"].SetOrdinal(15);
		dataTable.Columns["签订时间"].SetOrdinal(16);
		dataTable.Columns["合同类型"].SetOrdinal(17);
		dataTable.Columns["项目"].SetOrdinal(18);
		dataTable.Columns["项目类型"].SetOrdinal(19);
		if (dataTable.Rows.Count > 0)
		{
			string[] array = new string[]
			{
				dataTable.Compute("SUM(合同金额)", string.Empty).ToString(),
				dataTable.Compute("SUM(变更后金额)", string.Empty).ToString(),
				dataTable.Compute("SUM(变更累计)", string.Empty).ToString(),
				dataTable.Compute("SUM(结算累计)", string.Empty).ToString(),
				dataTable.Compute("SUM(付款累计)", string.Empty).ToString()
			};
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["合同编号"] = "合计";
			dataRow2["合同金额"] = array[0];
			dataRow2["变更后金额"] = array[1];
			dataRow2["变更累计"] = array[2];
			dataRow2["结算累计"] = array[3];
			dataRow2["付款累计"] = array[4];
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
}


