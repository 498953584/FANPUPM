using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.AccounIncome.BLL;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL.Fund.Account;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccounIncome_InComeReport : NBasePage, IRequiresSessionState
{
	private string accountid = string.Empty;
	private AccounIncome incomebll = new AccounIncome();
	private decimal inmoney = 0.00m;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
		this.gvConract.PageSize = NBasePage.pagesize;
	}
	protected void btnexecl_Click(object sender, EventArgs e)
	{
		new Common2().ExportToExecelAll(this.GetTitleByTable(this.GetTab()), this.lblTitle.Text + ".xls", base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		DataRow dataRow = dt.NewRow();
		dataRow["prjname"] = "合计";
		dataRow["Inmoney"] = this.inmoney;
		dt.Rows.Add(dataRow);
		dt.Columns["prjname"].ColumnName = "项目名称";
		dt.Columns["acountName"].ColumnName = "账户名称";
		dt.Columns["cllectionCode"].ColumnName = "回款单号";
		dt.Columns["ContName"].ColumnName = "合同名称";
		dt.Columns["Subject"].ColumnName = "入账类型";
		dt.Columns["getdate"].ColumnName = "发生日期";
		dt.Columns["people"].ColumnName = "经手人";
		dt.Columns["Inmoney"].ColumnName = "金额";
		dt.Columns["Remark"].ColumnName = "备注";
		dt.Columns.Remove(dt.Columns["Inuid"]);
		dt.Columns.Remove(dt.Columns["PrjGuid"]);
		dt.Columns.Remove(dt.Columns["FlowState"]);
		dt.Columns.Remove(dt.Columns["accountNum"]);
		return dt;
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	public DataTable GetTab()
	{
		string text = " and  PrjGuid in (select [PrjGuid] FROM [PT_PrjInfo] where  [Podepom] like '%" + base.UserCode + "%' ) ";
		if (!string.IsNullOrEmpty(this.txtPrjName.Text.Trim()))
		{
			text = text + " and prjname like '%" + this.txtPrjName.Text.Trim() + "%' ";
		}
		if (this.txtStartTime.Text.Trim().ToString() != "")
		{
			text = text + " and getdate >='" + this.txtStartTime.Text.Trim().ToString() + "' ";
		}
		if (this.txtEndTime.Text.Trim().ToString() != "")
		{
			text = text + " and getdate <='" + this.txtEndTime.Text.Trim().ToString() + "' ";
		}
		if (this.ddlSubject.SelectedValue != "-1")
		{
			text = text + " and Subject='" + this.ddlSubject.SelectedItem.Text.Trim().ToString() + "' ";
		}
		if (this.hdfAccoun.Value.Trim().ToString() != "")
		{
			AccounModel accounModel = new AccounModel();
			AccountLogic accountLogic = new AccountLogic();
			accounModel = accountLogic.GetModel(this.hdfAccoun.Value.ToString());
			string str;
			if (accounModel.PrjGuid.StartsWith("'"))
			{
				str = accounModel.PrjGuid.ToString();
			}
			else
			{
				str = "'" + accounModel.PrjGuid.ToString() + "'";
			}
			text = text + " and PrjGuid in (" + str + ")";
		}
		text += " and FlowState='1' ";
		text += " order by getdate desc ";
		DataTable list = this.incomebll.GetList(text);
		for (int i = 0; i < list.Rows.Count; i++)
		{
			this.inmoney += Convert.ToDecimal(list.Rows[i]["Inmoney"].ToString());
		}
		return list;
	}
	public void BindGv()
	{
		this.gvConract.DataSource = this.GetTab();
		this.gvConract.DataBind();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
			if (e.Row.Cells[8].Text.Length > 20)
			{
				e.Row.Cells[8].ToolTip = e.Row.Cells[8].Text;
				e.Row.Cells[8].Text = e.Row.Cells[8].Text.Substring(0, 19) + "...";
			}
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Attributes.Add("Style", "text-align:right");
			e.Row.Cells[1].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[8].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[8].Text = this.inmoney.ToString();
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
}


