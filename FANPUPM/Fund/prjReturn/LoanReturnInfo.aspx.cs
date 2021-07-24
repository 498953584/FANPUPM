using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Fund.prjReturn;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_prjReturn_LoanReturnInfo : NBasePage, IRequiresSessionState
{
	private prjReturenBLL returnBLL = new prjReturenBLL();
	private Common2 cm = new Common2();

	protected override void OnInit(EventArgs e)
	{
		this.gvLoanReturn.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public DataTable GetTable()
	{
		string text = " and  L.PrjGuid in (select [PrjGuid] FROM [PT_PrjInfo] where  [Podepom] like '%" + base.UserCode + "%' ) ";
		if (this.txtStartTime.Text.ToString() != "")
		{
			text = text + " and L.LoanDate >='" + this.txtStartTime.Text.ToString() + "'";
		}
		if (this.txtEndTime.Text.ToString() != "")
		{
			text = text + " and L.LoanDate<='" + this.txtEndTime.Text.ToString() + "'";
		}
		if (this.ddlReturnState.SelectedValue != "-1")
		{
			text = text + "  and L.ReturnState=" + this.ddlReturnState.SelectedValue.ToString();
		}
		if (this.hdnProjectCode.Value.ToString() != "" && this.txtPrjName.Value.ToString() != "")
		{
			text = text + "and L.prjGuid='" + this.hdnProjectCode.Value.ToString() + "'";
		}
		if (this.hdfAccoun.Value.ToString() != "" && this.txtAccoun.Value.ToString() != "")
		{
			text = text + " and a.acountName LIKE'%" + this.txtAccoun.Value.ToString() + "%'";
		}
		return this.returnBLL.GetLoanReturnInfo(text);
	}
	public void BindGv()
	{
		DataTable table = this.GetTable();
		this.gvLoanReturn.DataSource = table;
		this.gvLoanReturn.DataBind();
		string[] value = new string[]
		{
			table.Compute("SUM(LoanFund)", string.Empty).ToString(),
			table.Compute("SUM(returnMoney)", string.Empty).ToString(),
			table.Compute("SUM(returnInterest)", string.Empty).ToString(),
			table.Compute("SUM(returnDeduct)", string.Empty).ToString()
		};
		int[] index = new int[]
		{
			5,
			7,
			8,
			9
		};
		GridViewUtility.AddTotalRow(this.gvLoanReturn, value, index);
	}
	public void BindGv(DataTable gvLoanReturnSource)
	{
		if (gvLoanReturnSource.Rows.Count == 0)
		{
			gvLoanReturnSource.Rows.Add(gvLoanReturnSource.NewRow());
			this.gvLoanReturn.DataSource = gvLoanReturnSource;
			this.gvLoanReturn.DataBind();
			this.gvLoanReturn.HeaderRow.Cells[0].Visible = false;
			this.gvLoanReturn.Rows[0].Visible = false;
			return;
		}
		this.gvLoanReturn.DataSource = gvLoanReturnSource;
		this.gvLoanReturn.DataBind();
	}
	protected void gvLoanReturn_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvLoanReturn.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, EventArgs e)
	{
		DataTable table = this.GetTable();
		this.cm.ExportToExecelAll(this.GetTitleByTable(table), this.lblTitle.Text + ".xls", base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		string[] array = new string[]
		{
			dt.Compute("SUM(LoanFund)", string.Empty).ToString(),
			dt.Compute("SUM(returnMoney)", string.Empty).ToString(),
			dt.Compute("SUM(returnInterest)", string.Empty).ToString(),
			dt.Compute("SUM(returnDeduct)", string.Empty).ToString()
		};
		DataRow dataRow = dt.NewRow();
		dataRow["prjName"] = "合计";
		dataRow["LoanFund"] = array[0];
		dataRow["returnMoney"] = array[1];
		dataRow["returnInterest"] = array[2];
		dataRow["returnDeduct"] = array[3];
		dt.Rows.Add(dataRow);
		dt.Columns.Remove("LoanID");
		dt.Columns["prjName"].ColumnName = "项目名称";
		dt.Columns["LoanCode"].ColumnName = "借款单号";
		dt.Columns["LoanDate"].ColumnName = "借款日期";
		dt.Columns["LoanFund"].ColumnName = "借款金额";
		dt.Columns["LoanReturnState"].ColumnName = "是否还清";
		dt.Columns["returnMoney"].ColumnName = "归还本金";
		dt.Columns["returnInterest"].ColumnName = "归还利息";
		dt.Columns["returnDeduct"].ColumnName = "其他扣款";
		dt.Columns["Remark"].ColumnName = "备注";
		return dt;
	}
	protected void gvLoanReturn_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvLoanReturn.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvLoanReturn.DataKeys[e.Row.RowIndex].Value.ToString();
			Label label = e.Row.Cells[5].FindControl("labState") as Label;
			if (label.Text == "未还清")
			{
				label.Style.Add("color", "red");
				return;
			}
			if (label.Text == "已还清")
			{
				label.Style.Add("color", "#008B45");
			}
		}
	}
}


