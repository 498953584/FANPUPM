using ASP;
using cn.justwin.AccountManage.accBaise;
using cn.justwin.AccountManage.prjaccount;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL.AccountManage.acc_Manage;
using cn.justwin.stockBLL.AccountManage.accBaise;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSelectMyContrt : NBasePage, IRequiresSessionState
{
	private PayoutContract payoutContract = new PayoutContract();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvwContract.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			string value = base.Request["typevalue"].ToString();
			this.ViewState["type"] = value;
			System.Collections.Generic.List<PayoutContractModel> contracts = this.SearchContracts();
			this.DataBindContracts(contracts);
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<PayoutContractModel> contracts = this.SearchContracts();
		this.DataBindContracts(contracts);
	}
	private System.Collections.Generic.List<PayoutContractModel> SearchContracts()
	{
		string where = " and flowstate=1 and ContractID in (select convert(uniqueidentifier,contractNum) as dd from dbo.fund_Prjaccount where contractNum!='' and   authorizer like '%" + this.Session["yhdm"].ToString() + "%'  )";
		if (!(this.Session["yhdm"].ToString() != "00000000"))
		{
			where = " and ContractID in (select convert(uniqueidentifier,contractNum) as dd from dbo.fund_Prjaccount where contractNum!='')";
		}
		System.DateTime startDate = string.IsNullOrEmpty(this.txtStartTime.Text.Trim()) ? new System.DateTime(1753, 1, 1) : System.Convert.ToDateTime(this.txtStartTime.Text);
		System.DateTime endDate = string.IsNullOrEmpty(this.txtEndTime.Text.Trim()) ? new System.DateTime(9999, 12, 31) : System.Convert.ToDateTime(this.txtEndTime.Text.Trim());
		decimal startMoney = string.IsNullOrEmpty(this.txtStartMoney.Text.Trim()) ? 0m : System.Convert.ToDecimal(this.txtStartMoney.Text.Trim());
		decimal endMoney = string.IsNullOrEmpty(this.txtEndMoney.Text.Trim()) ? new decimal(999999999999999L) : System.Convert.ToDecimal(this.txtEndMoney.Text.Trim());
		return this.payoutContract.SelectAndLedgerWhere(startDate, endDate, startMoney, endMoney, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.ContractType1.TypeID, this.hdnProjectCode.Value, base.UserCode, where);
	}
	private void DataBindContracts(System.Collections.Generic.List<PayoutContractModel> contracts)
	{
		this.gvwContract.DataSource = contracts;
		this.gvwContract.DataBind();
	}
	protected void gvwContract_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		accBaise accBaise = new accBaise();
		basieModel model = accBaise.GetModel(1);
		string value;
		if (model != null)
		{
			value = model.fundRatio.ToString();
		}
		else
		{
			value = "0";
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = ((Label)e.Row.FindControl("lblname")).Text;
			string value2 = ((HiddenField)e.Row.FindControl("hdfacid")).Value;
			string text2 = ((Label)e.Row.FindControl("lblmony")).Text;
			string a = this.ViewState["type"].ToString();
			decimal num;
			if (a == "0")
			{
				num = System.Convert.ToDecimal(text2) * (System.Convert.ToDecimal(value) / 100m);
			}
			else
			{
				if (a == "1")
				{
					num = System.Convert.ToDecimal(text2);
				}
				else
				{
					if (a == "addoper")
					{
						num = System.Convert.ToDecimal(text2);
					}
					else
					{
						num = System.Convert.ToDecimal(text2);
					}
				}
			}
			e.Row.Attributes["ondblclick"] = string.Concat(new object[]
			{
				"dbClickRow(this,'",
				this.gvwContract.DataKeys[e.Row.RowIndex].Values[0].ToString(),
				"','",
				this.gvwContract.DataKeys[e.Row.RowIndex].Values[4].ToString(),
				"','",
				text,
				"','",
				value2,
				"','",
				num,
				"', true)"
			});
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
					base.RegisterScript("alert('系统提示：\\n\\n请先删除补充协议！');");
					return;
				}
				if (this.payoutContract.IsReferenced(current))
				{
					base.RegisterScript("alert('系统提示：\\n\\n请先删除此合同的关联数据！');");
					return;
				}
			}
			this.payoutContract.Delete(list);
			base.RegisterScript("window.location = window.location;");
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n删除失败！');");
		}
	}
	protected void gvwContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwContract.PageIndex = e.NewPageIndex;
		System.Collections.Generic.List<PayoutContractModel> contracts = this.SearchContracts();
		this.DataBindContracts(contracts);
	}
	protected string GetBankName(string contenum)
	{
		new prjaccountModel();
		accountMange accountMange = new accountMange();
		DataTable table = accountMange.GetTable(" contractNum='" + contenum + "'");
		if (table.Rows.Count == 1)
		{
			return table.Rows[0]["accountNum"].ToString();
		}
		return string.Empty;
	}
	protected string GetBankNum(string contenum)
	{
		new prjaccountModel();
		accountMange accountMange = new accountMange();
		DataTable table = accountMange.GetTable(" contractNum='" + contenum + "'");
		if (table.Rows.Count == 1)
		{
			return table.Rows[0]["accountID"].ToString();
		}
		return string.Empty;
	}
}


