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
public partial class ContractManage_ContractReport_IncometContractSum : NBasePage, IRequiresSessionState
{
	private string CorpName = string.Empty;
	private string CorpId = string.Empty;
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
		}, reportData, "收入合同汇总表.xls");
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
		DataTable data = this.GetData(this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwContract.DataSource = data;
		this.gvwContract.DataBind();
		string[] value = new string[]
		{
			this.dtcontracts.Compute("SUM(EndPrice)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(ClearingPrice)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(CllectionPrice)", string.Empty).ToString(),
			this.dtcontracts.Compute("SUM(NOCllectionPrice)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			8,
			9,
			10,
			11
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
	}
	public string GetParty(string party)
	{
		PayoutContract payoutContract = new PayoutContract();
		DataTable dataTable = new DataTable();
		string result = string.Empty;
		if (!string.IsNullOrEmpty(party))
		{
			dataTable = payoutContract.GetBName(party.Split(new char[]
			{
				','
			})[0]);
		}
		if (dataTable.Rows.Count > 0)
		{
			result = dataTable.Rows[0]["CorpName"].ToString();
		}
		return result;
	}
	private DataTable GetData(int pageIndex, int pageSize)
	{
		this.dtcontracts = this.payoutContract.GetIncometContractReportCount(base.UserCode, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), this.txtParty.Value.Trim(), this.txtSignPeople.Value.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim());
		this.AspNetPager1.RecordCount = this.dtcontracts.Rows.Count;
		DataTable dataTable = new DataTable();
		return this.payoutContract.GetIncometContractReport(base.UserCode, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), pageIndex, pageSize, this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), this.txtParty.Value.Trim(), this.txtSignPeople.Value.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim());
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindContract();
	}
	private DataTable GetReportData()
	{
		DataTable data = this.GetData(0, 0);
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("合同编号");
		dataTable.Columns.Add("合同名称");
		dataTable.Columns.Add("类型名称");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("甲方");
		dataTable.Columns.Add("签订人");
		dataTable.Columns.Add("签约时间");
		dataTable.Columns.Add("合同金额");
		dataTable.Columns.Add("累计结算金额");
		dataTable.Columns.Add("累计已收款金额");
		dataTable.Columns.Add("应收未收金额");
		if (data.Rows.Count != 0)
		{
			int num = 0;
			foreach (DataRow dataRow in data.Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = dataRow["Num"].ToString();
				dataRow2["合同编号"] = dataRow["ContractCode"];
				dataRow2["合同名称"] = dataRow["ContractName"];
				dataRow2["类型名称"] = dataRow["TypeName"];
				dataRow2["项目名称"] = dataRow["PrjName"];
				dataRow2["甲方"] = this.GetParty(dataRow["Party"].ToString());
				dataRow2["签订人"] = dataRow["SignName"];
				dataRow2["签约时间"] = dataRow["SignedTime"];
				dataRow2["合同金额"] = dataRow["EndPrice"];
				dataRow2["累计结算金额"] = dataRow["ClearingPrice"];
				dataRow2["累计已收款金额"] = dataRow["CllectionPrice"];
				dataRow2["应收未收金额"] = dataRow["NOCllectionPrice"];
				dataTable.Rows.Add(dataRow2);
			}
		}
		DataRow dataRow3 = dataTable.NewRow();
		dataRow3["序号"] = "合计";
		dataRow3["合同编号"] = "";
		dataRow3["合同名称"] = "";
		dataRow3["类型名称"] = "";
		dataRow3["项目名称"] = "";
		dataRow3["甲方"] = "";
		dataRow3["签订人"] = "";
		dataRow3["签约时间"] = "";
		dataRow3["合同金额"] = data.Compute("sum(EndPrice)", string.Empty).ToString();
		dataRow3["累计结算金额"] = data.Compute("sum(ClearingPrice)", string.Empty).ToString();
		dataRow3["累计已收款金额"] = data.Compute("sum(CllectionPrice)", string.Empty).ToString();
		dataRow3["应收未收金额"] = data.Compute("sum(NOCllectionPrice)", string.Empty).ToString();
		dataTable.Rows.Add(dataRow3);
		return dataTable;
	}
}


