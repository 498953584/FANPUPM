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
public partial class ContractManage_ContractForm_PaymentDetailReport : NBasePage, IRequiresSessionState
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
		DataTable dataTable = formBLL.PaymentDetailReport(this.txtPaymentCode.Text.Trim(), this.txtPerson.Text.Trim(), this.txtContractName.Text.Trim(), this.txtPrjName.Value, this.hldfIsExamineApprove.Value);
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		DataTable dataSource = formBLL.PaymentDetailReport(this.txtPaymentCode.Text.Trim(), this.txtPerson.Text.Trim(), this.txtContractName.Text.Trim(), this.txtPrjName.Value, this.hldfIsExamineApprove.Value, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwContract.DataSource = dataSource;
		this.gvwContract.DataBind();
		if (dataTable.Rows.Count > 0)
		{
			string[] value = new string[]
			{
				dataTable.Compute("SUM(PaymentAmount)", string.Empty).ToString()
			};
			int[] index = new int[]
			{
				4
			};
			GridViewUtility.AddTotalRow(this.gvwContract, value, index);
		}
	}
	private DataTable GetReportData()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("支付编号");
		dataTable.Columns.Add("支付日期");
		dataTable.Columns.Add("申请人");
		dataTable.Columns.Add("支付金额");
		dataTable.Columns.Add("合同名称");
		dataTable.Columns.Add("项目名称");
		DataTable dataTable2 = formBLL.PaymentDetailReport(this.txtPaymentCode.Text.Trim(), this.txtPerson.Text.Trim(), this.txtContractName.Text.Trim(), this.txtPrjName.Value, this.hldfIsExamineApprove.Value);
		if (dataTable2.Rows.Count > 0)
		{
			for (int i = 0; i < dataTable2.Rows.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["序号"] = (i + 1).ToString();
				dataRow["支付编号"] = dataTable2.Rows[i]["PaymentCode"].ToString();
				dataRow["支付日期"] = dataTable2.Rows[i]["PaymentDate"].ToString();
				dataRow["申请人"] = dataTable2.Rows[i]["PaymentPenson"].ToString();
				dataRow["支付金额"] = dataTable2.Rows[i]["PaymentAmount"].ToString();
				dataRow["合同名称"] = dataTable2.Rows[i]["ContractName"].ToString();
				dataRow["项目名称"] = dataTable2.Rows[i]["PrjName"].ToString();
				dataTable.Rows.Add(dataRow);
			}
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = "合计";
			dataRow2["支付编号"] = "";
			dataRow2["支付日期"] = "";
			dataRow2["申请人"] = "";
			dataRow2["支付金额"] = System.Convert.ToString(dataTable2.Compute("Sum(PaymentAmount)", string.Empty));
			dataRow2["合同名称"] = "";
			dataRow2["项目名称"] = "";
			dataTable.Rows.Add(dataRow2);
		}
		return dataTable;
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		Common2 common = new Common2();
		common.ExportToExecelAll(reportData, "项目资金收支明细表.xls", base.Request.Browser.Browser);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


