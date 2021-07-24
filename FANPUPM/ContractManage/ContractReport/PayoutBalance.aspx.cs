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
public partial class ContractManage_ContractReport_PayoutBalance : NBasePage, IRequiresSessionState
{
	private DataTable contractCount = new DataTable();
	private PayoutBalance payoutBalance = new PayoutBalance();

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
			this.contractCount.Compute("SUM(ModifiedMoney)", string.Empty).ToString(),
			this.contractCount.Compute("SUM(BalanceTotal)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			5,
			6
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
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				8
			}
		}, reportData, "支出合同结算报表.xls");
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
		PayoutContract payoutContract = new PayoutContract();
		string reportCondition = payoutContract.GetReportCondition(startDate, endDate, this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), this.txtContractName.Text.Trim(), this.txtContractCode.Text.Trim(), this.txtBName.Text.Trim(), this.dropPrjKindClass.SelectedItem.Text, this.txtSignPersonName.Text.Trim());
		this.contractCount = this.payoutBalance.Select(reportCondition, base.UserCode);
		this.ViewState["contract"] = this.contractCount;
		this.AspNetPager1.RecordCount = this.contractCount.Rows.Count;
		return this.payoutBalance.SelectData(reportCondition, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
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
		dataTable.Columns["ModifiedMoney"].ColumnName = "最终金额";
		dataTable.Columns["SignDate"].ColumnName = "签订时间";
		dataTable.Columns["TypeName"].ColumnName = "合同类型";
		dataTable.Columns["PrjName"].ColumnName = "项目";
		dataTable.Columns["BalanceTotal"].ColumnName = "结算累计";
		dataTable.Columns["Rate"].ColumnName = "百分比";
		dataTable.Columns["CorpName"].ColumnName = "乙方";
		dataTable.Columns["CodeName"].ColumnName = "项目类型";
		dataTable.Columns.Add(new DataColumn("合同状态", typeof(string)));
		foreach (DataRow dataRow in dataTable.Rows)
		{
			dataRow["合同状态"] = WebUtil.GetConState(dataRow["conState"].ToString());
		}
		dataTable.Columns.Remove(dataTable.Columns["conState"]);
		dataTable.Columns["乙方"].SetOrdinal(3);
		dataTable.Columns["最终金额"].SetOrdinal(4);
		dataTable.Columns["结算累计"].SetOrdinal(5);
		dataTable.Columns["百分比"].SetOrdinal(6);
		dataTable.Columns["合同状态"].SetOrdinal(7);
		dataTable.Columns["签订时间"].SetOrdinal(8);
		dataTable.Columns["合同类型"].SetOrdinal(9);
		dataTable.Columns["项目"].SetOrdinal(10);
		dataTable.Columns["项目类型"].SetOrdinal(11);
		if (this.contractCount.Rows.Count > 0)
		{
			string[] array = new string[]
			{
				dataTable.Compute("SUM(最终金额)", string.Empty).ToString(),
				dataTable.Compute("SUM(结算累计)", string.Empty).ToString()
			};
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["合同编号"] = "合计";
			dataRow2["最终金额"] = array[0];
			dataRow2["结算累计"] = array[1];
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
}


