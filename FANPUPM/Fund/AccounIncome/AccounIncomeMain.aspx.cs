using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Fund.Account;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccounIncome_AccounIncomeMain : NBasePage, IRequiresSessionState
{
	public AccountLogic AccountLogic = new AccountLogic();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.setTitle();
			this.DataBindAccount();
		}
	}
	private void setTitle()
	{
		if (!string.IsNullOrEmpty(base.Request["Sub"]))
		{
			this.hdnSub.Value = base.Request.QueryString["Sub"].ToString();
		}
		this.hdnType.Value = base.Request.QueryString["type"].ToLower();
		string text = this.hdnType.Value.ToString();
		this.gvwAccount.Columns[0].Visible = false;
		string a;
		if ((a = text) != null)
		{
			if (a == "income")
			{
				this.lblTitle.Text = "收入记账管理";
				this.framAccount.Attributes["src"] = "/Fund/AccounIncome/AccounIncome.aspx";
				return;
			}
			if (a == "payout")
			{
				this.lblTitle.Text = "支出记账管理";
				this.framAccount.Attributes["src"] = "/Fund/AccountPayOut/AccountPayOut.aspx?Sub=" + this.hdnSub.Value;
				return;
			}
			if (a == "loan")
			{
				this.lblTitle.Text = "项目借款管理";
				this.framAccount.Attributes["src"] = "../PrjLoan/PrjLoan.aspx";
				return;
			}
			if (a == "repayment")
			{
				this.lblTitle.Text = "项目还款管理";
				this.framAccount.Attributes["src"] = "../PrjLoan/PrjLoan_Repayment.aspx";
				return;
			}
			if (!(a == "view"))
			{
				return;
			}
			this.lblTitle.Text = "项目账户信息";
			this.gvwAccount.PageSize = 15;
			this.gvwAccount.Columns[0].Visible = false;
		}
	}
	private void DataBindAccount()
	{
		string text = " and authorizer like '%" + base.UserCode + "%' ";
		if (this.txtCode.Text.Trim() != "" && this.txtCode.Text.Trim() != null)
		{
			text = text + " and accountNum like '%" + this.txtCode.Text.Trim().ToString() + "%' ";
		}
		if (this.hdnProjectCode.Value != "" && this.hdnProjectCode.Value != null)
		{
			text = text + " and PrjGuid like '%" + this.hdnProjectCode.Value + "%' ";
		}
		if (this.txtPrjName.Text.Trim() != "" && this.txtPrjName.Text.Trim() != null)
		{
			text = text + " and acountName like '%" + this.txtPrjName.Text.Trim().ToString() + "%' ";
		}
		Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
		this.gvwAccount.DataSource = fund_Prj_Accoun.getAccounSumInfo(text);
		this.gvwAccount.DataBind();
	}
	protected void gvwAccount_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			e.Row.Cells.Clear();
			string text = "序号</td>  <td colspan='3' Class = 'header' align='center'>账户信息</td>  <td colspan='4' Class = 'header' align='center' >账户资金来源（+）</td>  <td colspan='4' Class = 'header' align='center' >账户资金支出（-）</td>  <td rowspan='2' Class = 'header'  width='80'>账户可用余额</td>  <td colspan='2' Class = 'header' align='center' >应支未支的（+）</td>  <td rowspan='2' Class = 'header'>账户余额</td>  <td rowspan='2' Class = 'header'  >操作</td>   </tr>   <tr>   <td width='80' Class = 'header' >账户编号</td>  <td width='80' Class = 'header' >账户名称</td>  <td width='80' Class = 'header' >包含项目</td>  <td width='80' Class = 'header' >启动资金</td>  <td width='80' Class = 'header' >其他收入</td>  <td width='80' Class = 'header' >合同回款入账</td>  <td width='80' Class = 'header' >项目借款</td>  <td width='80' Class = 'header' >归还本金</td>  <td width='80' Class = 'header' >归还利息及其他</td>  <td width='80' Class = 'header' >应支合同额</td>  <td width='80' Class = 'header' >应支间接费用</td>  <td width='80' Class = 'header' >未支合同额</td>  <td width='80' Class = 'header' >未支间接费用";
			TableCell tableCell = new TableCell();
			tableCell.RowSpan = 2;
			tableCell.CssClass = "header";
			tableCell.Text = text;
			e.Row.Cells.Add(tableCell);
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView arg_75_0 = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + e.Row.Attributes["id"] + "')";
		}
	}
	protected void gvwAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwAccount.PageIndex = e.NewPageIndex;
		this.DataBindAccount();
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.DataBindAccount();
	}
}


