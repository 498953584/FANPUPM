using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class ContractManage_PayoutContract_PayoutContract : NBasePage, IRequiresSessionState
{
	private System.Collections.Generic.List<PayoutContractModel> contracts;
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
			DataTable dtcontract = this.SearchContracts();
			this.DataBindContracts(dtcontract);
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		DataTable dtcontract = this.SearchContracts();
		this.DataBindContracts(dtcontract);
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable dtcontract = this.SearchContracts();
		this.DataBindContracts(dtcontract);
	}
	private DataTable SearchContracts()
	{
		System.DateTime startDate = string.IsNullOrEmpty(this.txtStartTime.Text.Trim()) ? new System.DateTime(1753, 1, 1) : System.Convert.ToDateTime(this.txtStartTime.Text);
		System.DateTime endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? new System.DateTime(9999, 12, 31) : System.Convert.ToDateTime(this.txtEndTime.Text.Trim());
		decimal startMoney = string.IsNullOrEmpty(this.txtStartMoney.Text.Trim()) ? new decimal(-999999999999999L) : System.Convert.ToDecimal(this.txtStartMoney.Text.Trim());
		decimal endMoney = string.IsNullOrEmpty(this.txtEndMoney.Text.Trim()) ? new decimal(999999999999999L) : System.Convert.ToDecimal(this.txtEndMoney.Text.Trim());
		string text = string.Format(" AND (Con_Payout_Contract.BName like '%{0}%' OR CorpName like '%{1}%' ) \n", this.txtBName.Text.Trim(), this.txtBName.Text.Trim());
		if (!string.IsNullOrEmpty(this.txtSignPersonName.Text.Trim()))
		{
			text += string.Format(" AND YH.v_xm LIKE '%{0}%' \n", this.txtSignPersonName.Text.Trim());
		}
		this.contracts = this.payoutContract.SelectLedger(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), base.UserCode, text);
		if (this.contracts == null || this.contracts.Count < 1)
		{
			this.AspNetPager1.RecordCount = 1;
		}
		else
		{
			this.AspNetPager1.RecordCount = this.contracts.Count;
		}
		return this.payoutContract.SelectLedger(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtConType.Text.Trim(), this.txtProject.Text.Trim(), base.UserCode, text, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
	}
	private void DataBindContracts(DataTable dtcontract)
	{
		this.gvwContract.DataSource = dtcontract;
		this.gvwContract.DataBind();
		decimal d = 0m;
		for (int i = 0; i < this.contracts.Count; i++)
		{
			d += System.Convert.ToDecimal(this.contracts[i].ModifiedMoney);
		}
		string[] value = new string[]
		{
			d.ToString()
		};
		int[] index = new int[]
		{
			4
		};
		GridViewUtility.AddTotalRow(this.gvwContract, value, index);
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
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldContract.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldContract.Value);
		}
		else
		{
			list.Add(this.hfldContract.Value);
		}
		try
		{
			foreach (string current in list)
			{
				System.Collections.Generic.List<PayoutContractModel> list2 = this.payoutContract.GetList(string.Format(" Con_Payout_Contract.MainContractID = '{0}'", current));
				if (list2.Count > 0)
				{
					base.RegisterScript("top.ui.alert('请先删除补充协议！');");
					return;
				}
				if (this.payoutContract.IsReferenced(current))
				{
					base.RegisterScript("top.ui.alert('请先删除此合同的关联数据！');");
					return;
				}
			}
			PayoutTarget payoutTarget = new PayoutTarget();
			payoutTarget.DelByContractId(list);
			this.payoutContract.Delete(list);
			ConConfigContractService conConfigContractService = new ConConfigContractService();
			conConfigContractService.Deltes(list);
			base.RegisterScript("window.location = window.location;");
		}
		catch
		{
			base.RegisterScript("top.ui.alert('删除失败！');");
		}
	}
}


