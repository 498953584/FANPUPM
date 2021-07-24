using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_PayoutInvoice_InvoiceLedger : NBasePage, IRequiresSessionState
{
	private PayoutInvoice invoice = new PayoutInvoice();
	private PayoutContract payoutContract = new PayoutContract();

	protected void Page_Load(object sender, EventArgs e)
	{
		this.gvwContract.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.DataBindContract();
		}
	}
	protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwContract.PageIndex = e.NewPageIndex;
		this.DataBindContract();
	}
	private void DataBindContract()
	{
		DataTable data = this.GetData();
		this.gvwContract.DataSource = data;
		this.gvwContract.DataBind();
		string[] value = new string[]
		{
			data.Compute("SUM(ModifiedMoney)", string.Empty).ToString(),
			data.Compute("SUM(PaymentTotal)", string.Empty).ToString(),
			data.Compute("SUM(InvoiceTotal)", string.Empty).ToString(),
			data.Compute("SUM(InvoiceDiff)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			7,
			8,
			9,
			10
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
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.DataBindContract();
	}
	protected void btnExcel_Click(object sender, EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		IExportable exportable = new ExcelExporter();
		FileExport fileExport = new FileExport(exportable, reportData, "支出合同发票台帐.xls");
		exportable.PercentColumns = new int[]
		{
			10
		};
		fileExport.Export(base.Request.Browser.Browser);
	}
	private DataTable GetData()
	{
		DateTime startDate = Common2.GetStartDate(this.txtStartDate);
		DateTime endDate = Common2.GetEndDate(this.txtEndDate);
		string reportCondition = this.payoutContract.GetReportCondition(startDate, endDate, this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), this.txtContractName.Text.Trim(), this.txtContractCode.Text.Trim(), this.txtBName.Text.Trim(), null, "");
		return this.invoice.GetLedger(reportCondition, base.UserCode);
	}
	private DataTable GetReportData()
	{
		DataTable data = this.GetData();
		data.Columns.Add(new DataColumn("序号"));
		for (int i = 0; i < data.Rows.Count; i++)
		{
			data.Rows[i]["序号"] = i + 1;
		}
		data.Columns["序号"].SetOrdinal(0);
		data.Columns.Remove("ContractID");
		data.Columns.Remove("Version");
		data.Columns.Remove("IsMainContract");
		data.Columns.Remove("UserCodes");
		data.Columns.Remove("Date");
		data.Columns.Remove("BName");
		data.Columns["ContractCode"].ColumnName = "合同编号";
		data.Columns["ContractName"].ColumnName = "合同名称";
		data.Columns["CorpName"].ColumnName = "乙方";
		data.Columns["ModifiedMoney"].ColumnName = "总合同金额";
		data.Columns["SignDate"].ColumnName = "签约日期";
		data.Columns["TypeName"].ColumnName = "合同类型";
		data.Columns["PrjName"].ColumnName = "项目名称";
		data.Columns["CodeName"].ColumnName = "付款方式";
		data.Columns["PaymentTotal"].ColumnName = "累计付款金额";
		data.Columns["InvoiceTotal"].ColumnName = "累计已到发票额";
		data.Columns["InvoiceDiff"].ColumnName = "累计未到发票额";
		data.Columns["InvoiceRate"].ColumnName = "已开发票付款比率";
		data.Columns["Notes"].ColumnName = "备注";
		data.Columns["已开发票付款比率"].SetOrdinal(10);
		string[] array = new string[]
		{
			data.Compute("SUM(总合同金额)", string.Empty).ToString(),
			data.Compute("SUM(累计付款金额)", string.Empty).ToString(),
			data.Compute("SUM(累计已到发票额)", string.Empty).ToString(),
			data.Compute("SUM(累计未到发票额)", string.Empty).ToString()
		};
		DataRow dataRow = data.NewRow();
		dataRow["项目名称"] = "总计";
		dataRow["总合同金额"] = array[0];
		dataRow["累计付款金额"] = array[1];
		dataRow["累计已到发票额"] = array[2];
		dataRow["累计未到发票额"] = array[3];
		data.Rows.Add(dataRow);
		return data;
	}
}


