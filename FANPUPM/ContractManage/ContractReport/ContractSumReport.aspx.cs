using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractReport_ContractSumReport : NBasePage, IRequiresSessionState
{
	private static string CorpName = string.Empty;
	private static string CorpId = string.Empty;
	private string ic = string.Empty;
	private PayoutBalance payoutBalance = new PayoutBalance();
	private PayoutContract payoutContract = new PayoutContract();
	private DataTable dtcontracts = new DataTable();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ic"]))
		{
			ContractManage_ContractReport_ContractSumReport.CorpId = base.Request["ic"];
			DataTable bName = this.payoutContract.GetBName(ContractManage_ContractReport_ContractSumReport.CorpId);
			ContractManage_ContractReport_ContractSumReport.CorpName = bName.Rows[0][0].ToString();
		}
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
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				11
			}
		}, reportData, ContractManage_ContractReport_ContractSumReport.CorpName + "明细表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isMainContract"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[1].ToString();
		}
	}
	private void DataBindContract()
	{
		DataTable data = this.GetData();
		this.gvwContract.DataSource = data;
		this.gvwContract.DataBind();
		string[] value = new string[]
		{
			this.dtcontracts.Compute("SUM(ModifiedMoney)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(BalanceMoney)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(PaymentMoney)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(NoPaymentMoney)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(Amount)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			6,
			7,
			8,
			9,
			10
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
	}
	private DataTable GetData()
	{
		this.dtcontracts = this.payoutContract.GetContractReportCount(ContractManage_ContractReport_ContractSumReport.CorpName, base.UserCode, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtProject.Text.Trim(), this.txtConType.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim());
		this.AspNetPager1.RecordCount = this.dtcontracts.Rows.Count;
		this.ViewState["contractdata"] = this.dtcontracts;
		return this.payoutContract.GetContractReport(ContractManage_ContractReport_ContractSumReport.CorpName, base.UserCode, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtProject.Text.Trim(), this.txtConType.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContract();
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = this.ViewState["contractdata"] as DataTable;
		dataTable.Columns.Remove("UserCodes");
		dataTable.Columns.Remove("IsMainContract");
		dataTable.Columns.Remove("CorpName");
		dataTable.Columns["Num"].ColumnName = "序号";
		dataTable.Columns["ContractCode"].ColumnName = "合同编号";
		dataTable.Columns["ContractName"].ColumnName = "合同名称";
		dataTable.Columns["PrjName"].ColumnName = "项目名称";
		dataTable.Columns["SignDate"].ColumnName = "签订时间";
		dataTable.Columns["TypeName"].ColumnName = "合同类型";
		dataTable.Columns["ModifiedMoney"].ColumnName = "合同金额";
		dataTable.Columns["BalanceMoney"].ColumnName = "累计结算金额";
		dataTable.Columns["PaymentMoney"].ColumnName = "累计已付款金额";
		dataTable.Columns["NoPaymentMoney"].ColumnName = "应付未付金额";
		dataTable.Columns["Amount"].ColumnName = "已提供发票金额";
		string[] array = new string[]
		{
			dataTable.Compute("SUM(合同金额)", string.Empty).ToString(),
			dataTable.Compute("SUM(累计结算金额)", string.Empty).ToString(),
			dataTable.Compute("SUM(累计已付款金额)", string.Empty).ToString(),
			dataTable.Compute("SUM(应付未付金额)", string.Empty).ToString(),
			dataTable.Compute("SUM(已提供发票金额)", string.Empty).ToString()
		};
		DataRow dataRow = dataTable.NewRow();
		dataRow["合同编号"] = "总计";
		dataRow["合同金额"] = array[0].Trim();
		dataRow["累计结算金额"] = array[1].Trim();
		dataRow["累计已付款金额"] = array[2].Trim();
		dataRow["应付未付金额"] = array[3].Trim();
		dataRow["已提供发票金额"] = array[4].Trim();
		dataTable.Rows.Add(dataRow);
		return dataTable;
	}
}


