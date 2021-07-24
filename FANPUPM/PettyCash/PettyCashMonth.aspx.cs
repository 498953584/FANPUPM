using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCashMonth : NBasePage, IRequiresSessionState
{
	private string userCode = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["userCode"]))
		{
			this.userCode = base.Request["userCode"];
		}
		else
		{
			this.userCode = base.UserCode;
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitTitle(System.DateTime.Now.Year);
			this.BindDropYear();
			this.BindGvwMonthCash(System.DateTime.Now.Year, this.userCode);
		}
	}
	protected void gvwMonthCash_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			PCPettyCashService pCPettyCashService = new PCPettyCashService();
			int year = System.Convert.ToInt32(this.dropYear.SelectedValue);
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[3].Text = pCPettyCashService.GetCashTotal(year, this.userCode).ToString("#0.000");
			e.Row.Cells[3].Style.Add("text-align", "right");
			e.Row.Cells[3].CssClass = "decimal_input";
		}
	}
	protected void btnExportExcel_Click(object sender, System.EventArgs e)
	{
		int year = System.Convert.ToInt32(this.dropYear.SelectedValue);
		PCPettyCashService pCPettyCashService = new PCPettyCashService();
		DataTable monthCash = pCPettyCashService.GetMonthCash(year, this.userCode);
		for (int i = 0; i < monthCash.Rows.Count; i++)
		{
			DataRow dataRow = monthCash.Rows[i];
			dataRow["Matter"] = dataRow["Matter"].ToString().Replace("<br />", ";\t");
		}
		string[] headerName = new string[]
		{
			"序号",
			"日期",
			"事项",
			"借款金额"
		};
		string[] fieldName = new string[]
		{
			"No",
			"Date",
			"Matter",
			"Cash"
		};
		string[] totalField = new string[]
		{
			"Cash"
		};
		ExcelHelper.ExportExcel(monthCash, headerName, fieldName, totalField, this.lblTitle.Text + ".xls", base.Request.Browser.Browser);
	}
	protected void dropYear_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		int year = System.Convert.ToInt32(this.dropYear.SelectedValue);
		this.InitTitle(year);
		this.BindGvwMonthCash(year, this.userCode);
	}
	private void InitTitle(int year)
	{
		PTYhmcService pTYhmcService = new PTYhmcService();
		string v_xm = pTYhmcService.GetById(this.userCode).v_xm;
		this.lblTitle.Text = string.Format("{0}年{1}借款金额汇总", year, v_xm);
	}
	private void BindDropYear()
	{
		this.dropYear.DataSource = this.GetMonthCash();
		this.dropYear.DataBind();
	}
	private DataTable GetMonthCash()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("name");
		dataTable.Columns.Add("value");
		for (int i = System.DateTime.Now.Year; i >= 2000; i--)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["name"] = i;
			dataRow["value"] = i;
			dataTable.Rows.Add(dataRow);
		}
		return dataTable;
	}
	private void BindGvwMonthCash(int year, string userCode)
	{
		PCPettyCashService pCPettyCashService = new PCPettyCashService();
		this.gvwMonthCash.DataSource = pCPettyCashService.GetMonthCash(year, userCode);
		this.gvwMonthCash.DataBind();
	}
}


