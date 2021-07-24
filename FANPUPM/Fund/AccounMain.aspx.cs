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
public partial class Fund_AccounMain : NBasePage, IRequiresSessionState
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
		this.hdnType.Value.ToString();
		this.gvwAccount.Columns[0].Visible = false;
		this.GVAccount.Columns[0].Visible = false;
		this.lblTitle.Text = "项目基本账户信息";
		this.tdAccout.Style.Add("height", "85%");
		this.gvwAccount.PageSize = NBasePage.pagesize;
		this.GVAccount.PageSize = NBasePage.pagesize;
	}
	private void DataBindAccount()
	{
		string text = " ";
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
		if (base.UserCode != "00000000")
		{
			string text2 = text;
			text = string.Concat(new string[]
			{
				text2,
				" AND authorizer LIKE '%",
				base.UserCode,
				"%' OR [createMan]=",
				base.UserCode,
				" "
			});
		}
		Fund_Prj_Accoun fund_Prj_Accoun = new Fund_Prj_Accoun();
		if (this.DDSelType.SelectedValue == "2")
		{
			this.GVAccount.Style.Add("display", "none");
			this.gvwAccount.Style.Add("display", "");
			this.gvwAccount.DataSource = fund_Prj_Accoun.getAccounSumInfo(text);
			this.gvwAccount.DataBind();
			return;
		}
		this.GVAccount.Style.Add("display", "");
		this.gvwAccount.Style.Add("display", "none");
		this.GVAccount.DataSource = fund_Prj_Accoun.getAccounSumInfo(text);
		this.GVAccount.DataBind();
	}
	protected void gvwAccount_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Header)
		{
			e.Row.Cells.Clear();
			string text = "序号</td>  <td colspan='3' Class = 'header' align='center'>账户基本信息</td>  <td colspan='4' Class = 'header' align='center' >账户资金来源（+）</td>  <td colspan='4' Class = 'header' align='center' >账户资金支出（-）</td>  <td rowspan='2' Class = 'header'  width='80'>账户可用余额</td>  <td colspan='2' Class = 'header' align='center' >应支未支的（+）</td>  <td rowspan='2' Class = 'header' >账户余额</td>  <td rowspan='2' Class = 'header'  >操作</td>   </tr>   <tr>   <td width='80' Class = 'header' >账户编号</td>  <td width='80' Class = 'header' >账户名称</td>  <td width='80' Class = 'header' >项目</td>  <td width='80' Class = 'header' >启动资金</td>  <td width='80' Class = 'header' >其他收入</td>  <td width='80' Class = 'header' >合同回款入账</td>  <td width='80' Class = 'header' >项目借款</td>  <td width='80' Class = 'header' >归还本金</td>  <td width='80' Class = 'header' >归还利息及其他</td>  <td width='80' Class = 'header' >应支合同额</td>  <td width='80' Class = 'header' >应支间接费用</td>  <td width='80' Class = 'header' >未支合同额</td>  <td width='80' Class = 'header' >未支间接费用";
			TableCell tableCell = new TableCell();
			tableCell.RowSpan = 2;
			tableCell.CssClass = "header";
			tableCell.Text = text;
			e.Row.Cells.Add(tableCell);
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + dataRowView["accountid"] + "')";
		}
	}
	protected void GVAccount_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView arg_21_0 = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.GVAccount.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + e.Row.Attributes["id"] + "')";
		}
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.DataBindAccount();
	}
	protected void gvwAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwAccount.PageIndex = e.NewPageIndex;
		this.DataBindAccount();
	}
	protected void GVAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GVAccount.PageIndex = e.NewPageIndex;
		this.DataBindAccount();
	}
}


