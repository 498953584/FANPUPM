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
public partial class ContractManage_ContractReport_BCorpNamePeport : NBasePage, IRequiresSessionState
{
	private PayoutBalance payoutBalance = new PayoutBalance();
	private PayoutContract payoutContract = new PayoutContract();
	private DataTable dtcontract = new DataTable();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
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
				8
			}
		}, reportData, "供应商台账.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	private void DataBindContract()
	{
		DataTable data = this.GetData();
		this.gvwContract.DataSource = data;
		this.gvwContract.DataBind();
		string[] value = new string[]
		{
			this.dtcontract.Compute("SUM(ModifiedMoney)", string.Empty).ToString(),
			this.dtcontract.Compute("SUM(BalanceMoney)", string.Empty).ToString(),
			this.dtcontract.Compute("SUM(PaymentMoney)", string.Empty).ToString(),
			this.dtcontract.Compute("SUM(NoPaymentMoney)", string.Empty).ToString(),
			this.dtcontract.Compute("SUM(Amount)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			2,
			3,
			4,
			5,
			6
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
	}
	private DataTable GetData()
	{
		this.dtcontract = this.payoutContract.GetBNameReportCount(this.txtBName.Text.Trim(), base.UserCode);
		this.ViewState["contract"] = this.dtcontract;
		this.AspNetPager1.RecordCount = this.dtcontract.Rows.Count;
		return this.payoutContract.GetBNameReport(this.txtBName.Text.Trim(), base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContract();
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = this.ViewState["contract"] as DataTable;
		if (dataTable.Rows.Count > 0)
		{
			string[] array = new string[]
			{
				dataTable.Compute("SUM(ModifiedMoney)", string.Empty).ToString(),
				dataTable.Compute("SUM(BalanceMoney)", string.Empty).ToString(),
				dataTable.Compute("SUM(PaymentMoney)", string.Empty).ToString(),
				dataTable.Compute("SUM(NoPaymentMoney)", string.Empty).ToString(),
				dataTable.Compute("SUM(Amount)", string.Empty).ToString()
			};
			DataRow dataRow = dataTable.NewRow();
			dataRow["CorpName"] = "合计";
			dataRow["ModifiedMoney"] = array[0];
			dataRow["BalanceMoney"] = array[1];
			dataRow["PaymentMoney"] = array[2];
			dataRow["NoPaymentMoney"] = array[3];
			dataRow["Amount"] = array[4];
			dataTable.Rows.Add(dataRow);
		}
		dataTable.Columns.Remove("CorpId");
		dataTable.Columns["Num"].ColumnName = "序号";
		dataTable.Columns["CorpName"].ColumnName = "供应商名称";
		dataTable.Columns["ModifiedMoney"].ColumnName = "合同金额";
		dataTable.Columns["BalanceMoney"].ColumnName = "累计结算金额";
		dataTable.Columns["PaymentMoney"].ColumnName = "累计已付款金额";
		dataTable.Columns["NoPaymentMoney"].ColumnName = "应付未付金额";
		dataTable.Columns["Amount"].ColumnName = "已提供发票金额";
		return dataTable;
	}
}


