using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.Web;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractForm_PaymentReport : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hldfIsExamineApprove.Value = ConfigHelper.Get("IsIncomeContractApprove");
			this.BindGv();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	private void BindGv()
	{
		DataTable dataTable = new DataTable();
		dataTable = formBLL.PaymentReport(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtPrjName.Value, this.hldfIsExamineApprove.Value);
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		DataTable dataSource = formBLL.PaymentReport(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtPrjName.Value, this.hldfIsExamineApprove.Value, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwContract.DataSource = dataSource;
		this.gvwContract.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value = new string[]
			{
				dataTable.Compute("SUM(ContractAmount)", string.Empty).ToString(),
				dataTable.Compute("SUM(PayAmount)", string.Empty).ToString(),
				dataTable.Compute("SUM(ApplyAmount)", string.Empty).ToString(),
				dataTable.Compute("SUM(DiffAmount)", string.Empty).ToString()
			};
			int[] index = new int[]
			{
				3,
				4,
				5,
				6
			};
			GridViewUtility.AddTotalRow(this.gvwContract, value, index);
		}
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("合同编码");
		dataTable.Columns.Add("合同名称");
		dataTable.Columns.Add("合同金额");
		dataTable.Columns.Add("收款累计");
		dataTable.Columns.Add("挂靠付款累计");
		dataTable.Columns.Add("差额");
		dataTable.Columns.Add("合同类型");
		dataTable.Columns.Add("项目名称");
		DataTable dataTable2 = formBLL.PaymentReport(this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.hfldConType.Value, this.txtPrjName.Value, this.hldfIsExamineApprove.Value);
		if (dataTable2.Rows.Count > 0)
		{
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["序号"] = (i + 1).ToString();
				dataRow["合同编码"] = dataTable2.Rows[i]["ContractCode"].ToString();
				dataRow["合同名称"] = dataTable2.Rows[i]["ContractName"].ToString();
				dataRow["合同金额"] = dataTable2.Rows[i]["ContractAmount"].ToString();
				dataRow["收款累计"] = dataTable2.Rows[i]["PayAmount"].ToString();
				dataRow["挂靠付款累计"] = dataTable2.Rows[i]["ApplyAmount"].ToString();
				dataRow["差额"] = dataTable2.Rows[i]["DiffAmount"].ToString();
				dataRow["合同类型"] = dataTable2.Rows[i]["TypeName"].ToString();
				dataRow["项目名称"] = dataTable2.Rows[i]["PrjName"].ToString();
				dataTable.Rows.Add(dataRow);
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = "合计";
			dataRow2["合同编码"] = "";
			dataRow2["合同名称"] = "";
			dataRow2["合同金额"] = System.Convert.ToString(dataTable2.Compute("Sum(ContractAmount)", string.Empty));
			dataRow2["收款累计"] = System.Convert.ToString(dataTable2.Compute("Sum(PayAmount)", string.Empty));
			dataRow2["挂靠付款累计"] = System.Convert.ToString(dataTable2.Compute("Sum(ApplyAmount)", string.Empty));
			dataRow2["差额"] = System.Convert.ToString(dataTable2.Compute("Sum(DiffAmount)", string.Empty));
			dataRow2["合同类型"] = "";
			dataRow2["项目名称"] = "";
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		Common2 common = new Common2();
		common.ExportToExecelAll(reportData, "项目资金收支总表.xls", base.Request.Browser.Browser);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


