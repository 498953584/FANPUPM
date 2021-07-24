using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_PayoutContract_Permission : NBasePage, IRequiresSessionState
{
	private PayoutContract payoutContract = new PayoutContract();
	private readonly Purchase purchase = new Purchase();
	private readonly PurchaseStock purchaseStock = new PurchaseStock();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			System.Collections.Generic.List<PayoutContractModel> contracts = this.SearchContracts();
			this.DataBindContracts(contracts);
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		System.Collections.Generic.List<PayoutContractModel> contracts = this.SearchContracts();
		this.DataBindContracts(contracts);
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isMainContract"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["prjGuid"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["auditContent"] = "支出合同 ：" + this.gvwContract.DataKeys[e.Row.RowIndex].Values[3].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<PayoutContractModel> contracts = this.SearchContracts();
		this.DataBindContracts(contracts);
	}
	private void DataBindContracts(System.Collections.Generic.List<PayoutContractModel> contracts)
	{
		this.AspNetPager1.RecordCount = contracts.Count;
		decimal d = 0m;
		for (int i = 0; i < contracts.Count; i++)
		{
			d += System.Convert.ToDecimal(contracts[i].ModifiedMoney);
		}
		string[] value = new string[]
		{
			d.ToString()
		};
		int count = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize;
		this.gvwContract.DataSource = contracts.Skip(count).Take(this.AspNetPager1.PageSize);
		this.gvwContract.DataBind();
		int[] index = new int[]
		{
			4
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
	}
	private System.Collections.Generic.List<PayoutContractModel> SearchContracts()
	{
		System.DateTime startDate = string.IsNullOrEmpty(this.txtStartTime.Text.Trim()) ? new System.DateTime(1753, 1, 1) : System.Convert.ToDateTime(this.txtStartTime.Text.Trim());
		System.DateTime endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? new System.DateTime(9999, 12, 31) : System.Convert.ToDateTime(this.txtEndTime.Text.Trim());
		decimal startMoney = string.IsNullOrEmpty(this.txtStartMoney.Text.Trim()) ? new decimal(-999999999999999L) : System.Convert.ToDecimal(this.txtStartMoney.Text.Trim());
		decimal endMoney = string.IsNullOrEmpty(this.txtEndMoney.Text.Trim()) ? new decimal(999999999999999L) : System.Convert.ToDecimal(this.txtEndMoney.Text.Trim());
		return this.payoutContract.SelectLedger(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtProject.Text, string.Empty);
	}
}


