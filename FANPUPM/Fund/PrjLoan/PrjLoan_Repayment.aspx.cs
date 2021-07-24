using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjLoan_Repayment : NBasePage, IRequiresSessionState
{
	private Fund_Loan_Repayment FA = new Fund_Loan_Repayment();

	protected Guid PrjGuid
	{
		get
		{
			object obj = this.ViewState["PRJGUID"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
		this.btnRepayment.Attributes.Add("onclick", "if( !confirm('此过程不可逆;\\r\\n确认归还该笔借款吗?')){ return false;}");
	}
	public void BindGv()
	{
		if (this.hfldPrjId.Value.ToString() == "" && base.Request["prjGuid"] != null)
		{
			this.hfldPrjId.Value = base.Request["prjGuid"].ToString();
		}
		if (this.hfldPrjId.Value.ToString() != "")
		{
			DataTable dataSource = new DataTable();
			dataSource = this.FA.GetRepaymentInfoByPrjGuid(this.hfldPrjId.Value.ToString());
			this.gvBudget.DataSource = dataSource;
		}
		this.gvBudget.DataBind();
	}
	protected void gvBudget_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			e.Row.Attributes["id"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString();
			string text = (dataRowView["ReturnFlowState"].ToString() == "1") ? "1" : "0";
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"clickRows('",
				this.gvBudget.DataKeys[e.Row.RowIndex]["LoanID"].ToString(),
				"',",
				text,
				",this)"
			});
			Label label = e.Row.Cells[2].FindControl("labLoanCode") as Label;
			label.Attributes["onclick"] = "shoView('" + this.gvBudget.DataKeys[e.Row.RowIndex].Value.ToString() + "')";
			int syday = (int)dataRowView["syday"];
			string text2 = this.FA.SetStatets(syday, text);
			e.Row.Cells[11].Text = text2;
			e.Row.Cells[11].Style.Add("color", this.FA.SetStateColor(text2));
		}
	}
	protected void btnRepayment_Click(object sender, EventArgs e)
	{
		string loanid = this.hdnLoanID.Value.ToString();
		int num = this.FA.SetLoanRepayment(loanid, base.UserCode);
		if (num == 1)
		{
			base.RegisterScript("alert('系统提示：\\n\\操作成功！');");
		}
		else
		{
			base.RegisterScript("alert('系统提示：\\n\\n操作失败！');");
		}
		this.hdnLoanID.Value = "";
		this.BindGv();
	}
}


