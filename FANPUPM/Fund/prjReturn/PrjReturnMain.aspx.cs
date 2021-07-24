using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Fund.prjReturn;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_prjReturn_PrjReturnMain : NBasePage, IRequiresSessionState
{
	private prjReturenBLL prBLL = new prjReturenBLL();

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.DataBindAccount();
			this.setTitle();
		}
	}
	private void setTitle()
	{
		this.lblTitle.Text = "项目还款管理";
		if (this.gvwAccount.Rows.Count > 0)
		{
			this.framAccount.Attributes["src"] = string.Concat(new object[]
			{
				"../prjReturn/prjReturnList.aspx?LoanID=",
				this.gvwAccount.DataKeys[0].Values[0],
				"&returnState=",
				this.gvwAccount.DataKeys[0].Values[1]
			});
			return;
		}
		this.framAccount.Attributes["src"] = "../prjReturn/prjReturnList.aspx?LoanID=&returnState=";
	}
	private void DataBindAccount()
	{
		string text = "  and  l.PrjGuid in (select [PrjGuid] FROM [PT_PrjInfo] where  [Podepom] like '%" + base.UserCode + "%' ) ";
		if (this.txtCode.Text.Trim() != "" && this.txtCode.Text.Trim() != null)
		{
			text = text + " and LoanCode like '%" + this.txtCode.Text.Trim().ToString() + "%' ";
		}
		if (this.hdnProjectCode.Value != "" && this.hdnProjectCode.Value != null)
		{
			text = text + " and l.prjGuid='" + this.hdnProjectCode.Value.Trim() + "' ";
		}
		if (this.txtMoney.Text.Trim() != "" && this.txtMoney.Text.Trim() != null)
		{
			text = text + " and LoanFund=" + Convert.ToDecimal(this.txtMoney.Text.Trim());
		}
		if (this.txtPeople.Value.Trim() != "" && this.txtPeople.Value.Trim() != null)
		{
			text = text + " and LoanMan='" + this.hfdPeople.Value.Trim() + "'";
		}
		if (this.txtBeginTime.Text.Trim() != "" && this.txtBeginTime.Text.Trim() != null)
		{
			if (this.txtEndTime.Text.Trim() == "" || this.txtEndTime.Text.Trim() == null)
			{
				text = text + " and (PlanReturnDate >='" + this.txtBeginTime.Text.Trim() + "')";
			}
			else
			{
				string text2 = text;
				text = string.Concat(new string[]
				{
					text2,
					" and (PlanReturnDate between '",
					this.txtBeginTime.Text.Trim(),
					"' and '",
					this.txtEndTime.Text.Trim(),
					"')"
				});
			}
		}
		else
		{
			if (this.txtEndTime.Text.Trim() != "" && this.txtEndTime.Text.Trim() != null)
			{
				text = text + " and (PlanReturnDate <='" + this.txtEndTime.Text.Trim() + "')";
			}
		}
		string accounID = this.hdfAccoun.Value.ToString();
		this.prBLL.GetLoadInfo(base.UserCode, text, accounID);
		this.gvwAccount.DataSource = this.prBLL.GetLoadInfo(base.UserCode, text, accounID);
		this.gvwAccount.DataBind();
	}
	protected void gvwAccount_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvwAccount.DataKeys[e.Row.RowIndex].Values[0].ToString();
			e.Row.Attributes["prjguid"] = dataRowView["PrjGuid"].ToString();
			string text = "0";
			if (this.gvwAccount.DataKeys[e.Row.RowIndex].Values[1].ToString() != "")
			{
				text = this.gvwAccount.DataKeys[e.Row.RowIndex].Values[1].ToString();
			}
			Label label = e.Row.Cells[4].FindControl("labRetState") as Label;
			if (text == "0")
			{
				label.Style.Add("color", "red");
			}
			else
			{
				if (text == "1")
				{
					label.Style.Add("color", "#008B45");
				}
			}
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				e.Row.Attributes["id"],
				"',",
				text,
				")"
			});
		}
	}
	protected void gvwAccount_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwAccount.PageIndex = e.NewPageIndex;
		this.DataBindAccount();
	}
	protected void btnSearch_Click(object sender, EventArgs e)
	{
		this.gvwAccount.PageIndex = 1;
		this.DataBindAccount();
	}
}


