using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_ContractExecution_ContractArchives : NBasePage, IRequiresSessionState
{
	private System.Collections.Generic.List<PayoutContractModel> contracts;
	private PayoutContract payoutContract = new PayoutContract();

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = 5;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindContract();
		}
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwContract.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["isMainContract"] = this.gvwContract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			CheckBox checkBox = e.Row.FindControl("chk") as CheckBox;
			try
			{
				checkBox.Attributes.Add("onclick", "Change(" + checkBox.ClientID + ")");
			}
			catch
			{
			}
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes.Add("onmouseover", "currentcolor=this.style.backgroundColor;this.style.backgroundColor ='#EAF4FD'");
			e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor =currentcolor");
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		DataTable dataTable = this.SearchContracts();
		this.DataBindContracts(dataTable);
	}
	private void DataBindContract()
	{
		System.Collections.Generic.List<PayoutContractModel> list = this.payoutContract.GetList(" Con_Payout_Contract.IsArchived = '1' ", base.UserCode);
		if (list == null || list.Count < 1)
		{
			this.AspNetPager1.RecordCount = 1;
		}
		else
		{
			this.AspNetPager1.RecordCount = list.Count;
		}
		DataTable list2 = this.payoutContract.GetList(" Con_Payout_Contract.IsArchived = '1' ", base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvwContract.DataSource = list2;
		this.gvwContract.DataBind();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.hfldContractId.Value = "";
		DataTable dataTable = this.SearchContracts();
		this.DataBindContracts(dataTable);
	}
	private void DataBindContracts(DataTable contracts)
	{
		this.gvwContract.DataSource = contracts;
		this.gvwContract.DataBind();
	}
	private DataTable SearchContracts()
	{
		System.DateTime startDate = string.IsNullOrEmpty(this.txtStartTime.Text.Trim()) ? new System.DateTime(1753, 1, 1) : System.Convert.ToDateTime(this.txtStartTime.Text.Trim());
		System.DateTime endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? new System.DateTime(9999, 12, 31) : System.Convert.ToDateTime(this.txtEndTime.Text.Trim());
		decimal startMoney = string.IsNullOrEmpty(this.txtStartMoney.Text.Trim()) ? new decimal(-999999999999999L) : System.Convert.ToDecimal(this.txtStartMoney.Text.Trim());
		decimal endMoney = string.IsNullOrEmpty(this.txtEndMoney.Text.Trim()) ? new decimal(999999999999999L) : System.Convert.ToDecimal(this.txtEndMoney.Text.Trim());
		this.contracts = this.payoutContract.SelectArchives(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), base.UserCode);
		if (this.contracts == null || this.contracts.Count < 1)
		{
			this.AspNetPager1.RecordCount = 1;
		}
		else
		{
			this.AspNetPager1.RecordCount = this.contracts.Count;
		}
		return this.payoutContract.SelectArchives(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
	}
}


