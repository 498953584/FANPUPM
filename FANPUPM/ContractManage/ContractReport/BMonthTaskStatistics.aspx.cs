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
public partial class ContractManage_ContractReport_BMonthTaskStatistics : NBasePage, IRequiresSessionState
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
		PayoutContract payoutContract = new PayoutContract();
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
			dataTable = payoutContract.GetMonthStatistics(text, beginTime, endTime, text2);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					try
					{
						if (dataRow["OrderNumber"].ToString().Length / 3 == 1)
						{
							d += System.Convert.ToDecimal(dataRow["Total"]);
							d2 += System.Convert.ToDecimal(dataRow["mTotal2"]);
						}
						d3 += System.Convert.ToDecimal(dataRow["cTotal"]);
						d4 += System.Convert.ToDecimal(dataRow["tTotal"]);
					}
					catch (System.Exception)
					{
					}
				}
			}
		}
		this.hfldTotal2.Value = d.ToString();
		this.hfldMTotal.Value = d2.ToString();
		this.hfldCTotal.Value = d3.ToString();
		this.hfldTTotal.Value = d4.ToString();
		this.ViewState["dt"] = dataTable;
		this.gvwPrjBudgetStatistics.DataSource = dataTable;
		this.gvwPrjBudgetStatistics.DataBind();
	}
	protected void gvwPrjBudgetStatistics_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string text = this.gvwPrjBudgetStatistics.DataKeys[e.Row.RowIndex].Values["ModifyType"].ToString();
			string value = this.gvwPrjBudgetStatistics.DataKeys[e.Row.RowIndex].Values["TaskId"].ToString();
			string value2 = this.gvwPrjBudgetStatistics.DataKeys[e.Row.RowIndex].Values["SubCount"].ToString();
			string value3 = this.gvwPrjBudgetStatistics.DataKeys[e.Row.RowIndex].Values["OrderNumber"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["ModifyType"] = text;
			e.Row.Attributes["SubCount"] = value2;
			e.Row.Attributes["OrderNumber"] = value3;
			if (text == "0")
			{
				e.Row.CssClass = "tr-waring";
			}
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[0].Text = "合计";
			e.Row.Cells[6].Text = this.hfldTotal2.Value;
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[8].Text = this.hfldMTotal.Value;
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[10].Text = this.hfldCTotal.Value;
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[12].Text = this.hfldTTotal.Value;
			e.Row.Cells[12].CssClass = "decimal_input";
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
		string[] values = new string[12];
		foreach (DataRow dataRow in dataTable.Rows)
		{
			string text = dataRow["Num"].ToString();
			string text2 = dataRow["TaskCode"].ToString();
			string text3 = dataRow["TaskName"].ToString();
			string text4 = dataRow["Unit"].ToString();
			string text5 = dataRow["UnitPrice"].ToString();
			string text6 = dataRow["Quantity"].ToString();
			string text7 = dataRow["Total"].ToString();
			string text8 = dataRow["mtQty"].ToString();
			string text9 = dataRow["mTotal2"].ToString();
			string text10 = dataRow["cQty"].ToString();
			string text11 = dataRow["cTotal"].ToString();
			string text12 = dataRow["tQty"].ToString();
			string text13 = dataRow["tTotal"].ToString();
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
				text11,
				text12,
				text13
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
			this.hfldTotal2.Value,
			"",
			this.hfldMTotal.Value,
			"",
			this.hfldCTotal.Value,
			"",
			this.hfldTTotal.Value
		};
		dataTable2.Rows.Add(values2);
		string text14 = "目标成本月完成预算统计表.xls";
		if (base.Request.Browser.Browser != "Safari")
		{
			text14 = HttpUtility.UrlEncode(text14, System.Text.Encoding.UTF8);
		}
		ExcelHelper.ExportDataTableToExcel(dataTable2, text14, "sheet1");
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


