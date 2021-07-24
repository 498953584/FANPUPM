using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using System;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractReport_BMonthCompletedStatistics : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.gvwPrjBudgetStatistics.PageSize = NBasePage.pagesize;
			this.DataBindList();
		}
	}
	private void DataBindList()
	{
		IncometContract incometContract = new IncometContract();
		string text = base.Request.QueryString["prjCode"];
		DataTable dataTable = null;
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal d4 = 0m;
		if (!string.IsNullOrEmpty(text))
		{
			string text2 = string.Empty;
			if (!string.IsNullOrEmpty(this.txtCode.Text.Trim()))
			{
				text2 = text2 + " AND taskCode LIKE '%" + this.txtCode.Text.Trim() + "%' ";
			}
			if (!string.IsNullOrEmpty(this.txtName.Text.Trim()))
			{
				text2 = text2 + " AND taskName LIKE '%" + this.txtName.Text.Trim() + "%' ";
			}
			string beginTime = System.DateTime.Now.ToString("yyyy-MM") + "-01";
			string endTime = System.DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
			dataTable = incometContract.GetMonthStatistics(text, beginTime, endTime, text2);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					d += System.Convert.ToDecimal(dataRow["Total"]);
					d2 += System.Convert.ToDecimal(dataRow["MTotal"]);
					d3 += System.Convert.ToDecimal(dataRow["CAmount"]);
					d4 += System.Convert.ToDecimal(dataRow["TAmount"]);
					if (dataRow["ModifyType"].ToString() == "0")
					{
						dataRow["Total"] = 0;
						dataRow["Quantity"] = 0;
					}
				}
			}
		}
		this.hfldPreTotal.Value = d.ToString();
		this.hfldCurTotal.Value = d2.ToString();
		this.hfldCueMonthTotal.Value = d3.ToString();
		this.hfldAggregateTotal.Value = d4.ToString();
		this.ViewState["dt"] = dataTable;
		this.gvwPrjBudgetStatistics.DataSource = dataTable;
		this.gvwPrjBudgetStatistics.DataBind();
	}
	protected void gvwPrjBudgetStatistics_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvwPrjBudgetStatistics.DataKeys[e.Row.RowIndex]["ModifyType"].ToString();
			string value = this.gvwPrjBudgetStatistics.DataKeys[e.Row.RowIndex]["TaskID"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["ModifyType"] = text;
			if (text == "0")
			{
				e.Row.CssClass = "tr-waring";
			}
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			e.Row.Cells[6].Text = this.hfldPreTotal.Value;
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[8].Text = this.hfldCurTotal.Value;
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = this.hfldCueMonthTotal.Value;
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = this.hfldAggregateTotal.Value;
			e.Row.Cells[10].CssClass = "decimal_input";
		}
	}
	protected void btnExecl_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = this.ViewState["dt"] as DataTable;
		if (dataTable == null || dataTable.Rows.Count < 1)
		{
			return;
		}
		DataTable dataTable2 = new DataTable();
		for (int i = 0; i < this.gvwPrjBudgetStatistics.Columns.Count; i++)
		{
			dataTable2.Columns.Add(this.gvwPrjBudgetStatistics.HeaderRow.Cells[i].Text);
		}
		string[] values = new string[10];
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text = dataRow["NUM"].ToString();
			string text2 = dataRow["TaskCode"].ToString();
			string text3 = dataRow["TaskName"].ToString();
			string text4 = dataRow["Unit"].ToString();
			string text5 = dataRow["UnitPrice"].ToString();
			string text6 = dataRow["Quantity"].ToString();
			string text7 = dataRow["Total"].ToString();
			string text8 = dataRow["MQuantity"].ToString();
			string text9 = dataRow["MTotal"].ToString();
			string text10 = dataRow["CAmount"].ToString();
			string text11 = dataRow["TAmount"].ToString();
			values = new string[]
			{
				text,
				text2,
				text3,
				text4,
				text5,
				text6,
				text7,
				text8,
				text9,
				text10,
				text11
			};
			dataTable2.Rows.Add(values);
		}
		string[] values2 = new string[]
		{
			"合计",
			"",
			"",
			"",
			"",
			"",
			this.hfldPreTotal.Value,
			"",
			this.hfldCurTotal.Value,
			this.hfldCueMonthTotal.Value,
			this.hfldAggregateTotal.Value
		};
		dataTable2.Rows.Add(values2);
		string text12 = "合同预算月完成预算统计表.xls";
		if (base.Request.Browser.Browser != "Safari")
		{
			text12 = HttpUtility.UrlEncode(text12, System.Text.Encoding.UTF8);
		}
		ExcelHelper.ExportDataTableToExcel(dataTable2, text12, "sheet1");
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.DataBindList();
	}
	protected void gvwPrjBudgetStatistics_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwPrjBudgetStatistics.PageIndex = e.NewPageIndex;
		this.DataBindList();
	}
}


