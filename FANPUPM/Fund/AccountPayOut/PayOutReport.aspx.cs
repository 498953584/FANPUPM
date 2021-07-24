using ASP;
using cn.justwin.BLL;
using cn.justwin.Fund.AccounPayout.BLL;
using cn.justwin.Fund.Account;
using cn.justwin.stockBLL.Fund.Account;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_AccountPayOut_PayOutReport : NBasePage, IRequiresSessionState
{
	private string accountid = string.Empty;
	private AccounPayoutBLL accountBLL = new AccounPayoutBLL();
	private decimal yingzhi = 0.00m;
	private decimal yizhi = 0.00m;

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			string a;
			if ((a = base.Request.QueryString["type"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (a == "1")
					{
						this.lblTitle.Text = "资金间接费用支出报表";
					}
				}
				else
				{
					this.lblTitle.Text = "资金直接费用支出报表";
				}
			}
			this.BindGv();
		}
		this.gvConract.PageSize = NBasePage.pagesize;
	}
	protected void btnexecl_Click(object sender, EventArgs e)
	{
		this.ExportToExecelAll(this.GetTitleByTable(this.GetTab()), this.lblTitle.Text + ".xls");
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		dt.Columns["PrjName"].ColumnName = "项目名称";
		dt.Columns["PayCode"].ColumnName = "费用名称";
		dt.Columns["PayTime"].ColumnName = "支付日期";
		dt.Columns["PayMoney"].ColumnName = "应支金额";
		dt.Columns["AccountMoney"].ColumnName = "已支金额";
		dt.Columns["BL"].ColumnName = "支付百分比";
		dt.Columns["Money"].ColumnName = "未支金额";
		dt.Columns.Remove(dt.Columns["ID"]);
		dt.Columns.Remove(dt.Columns["PrjGuid"]);
		return dt;
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	public DataTable GetTab()
	{
		string text = " and  PrjGuid in (select [PrjGuid] FROM [PT_PrjInfo] where  [Podepom] like '%" + base.UserCode + "%' ) ";
		if (!string.IsNullOrEmpty(this.txtPrjName.Text))
		{
			text = text + " and PrjName like '%" + this.txtPrjName.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			text = text + " and PayTime >='" + this.txtStartTime.Text.Trim() + "' ";
		}
		if (!string.IsNullOrEmpty(this.txtEndTime.Text))
		{
			text = text + " and PayTime <='" + this.txtEndTime.Text.Trim() + "' ";
		}
		if (!string.IsNullOrEmpty(this.hdfAccoun.Value))
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
		text += " order by PayTime desc ";
		DataTable reportByWhere = this.accountBLL.getReportByWhere(base.Request.QueryString["type"], text);
		for (int i = 0; i < reportByWhere.Rows.Count; i++)
		{
			this.yingzhi += Convert.ToDecimal(reportByWhere.Rows[i]["PayMoney"].ToString());
			this.yizhi += Convert.ToDecimal(reportByWhere.Rows[i]["AccountMoney"].ToString());
		}
		return reportByWhere;
	}
	public void BindGv()
	{
		this.gvConract.DataSource = this.GetTab();
		this.gvConract.DataBind();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		string a;
		if (e.Row.RowType == DataControlRowType.Header && (a = base.Request.QueryString["type"].ToString()) != null)
		{
			if (!(a == "0"))
			{
				if (a == "1")
				{
					e.Row.Cells[2].Text = "费用名称";
				}
			}
			else
			{
				e.Row.Cells[2].Text = "合同支付单号";
			}
		}
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Attributes.Add("Style", "text-align:right");
			e.Row.Cells[1].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[4].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[5].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[6].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[7].Attributes.Add("Style", "font-weight:bold");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[4].Text = this.yingzhi.ToString();
			e.Row.Cells[5].Text = this.yizhi.ToString();
			if (this.yingzhi.Equals(0.00m))
			{
				e.Row.Cells[6].Text = Math.Round(Convert.ToDecimal(this.yingzhi.ToString()), 2).ToString() + "%";
			}
			else
			{
				e.Row.Cells[6].Text = Math.Round(Convert.ToDecimal((this.yizhi / this.yingzhi * 100m).ToString()), 2).ToString() + "%";
			}
			e.Row.Cells[7].Text = (this.yingzhi - this.yizhi).ToString();
		}
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	private void ExportToExecelAll(DataTable dataSource, string TableName)
	{
		DataRow dataRow = dataSource.NewRow();
		dataRow[0] = "合计";
		dataRow[3] = this.yingzhi.ToString();
		dataRow[4] = this.yizhi.ToString();
		dataRow[5] = (this.yingzhi - this.yizhi).ToString();
		if (this.yingzhi.Equals(0.00m))
		{
			dataRow[6] = Math.Round(Convert.ToDecimal(this.yingzhi.ToString()), 2).ToString() + "%";
		}
		else
		{
			dataRow[6] = Math.Round(Convert.ToDecimal((this.yizhi / this.yingzhi * 100m).ToString()), 2).ToString() + "%";
		}
		dataSource.Rows.Add(dataRow);
		new Common2().ExportToExecelAll(dataSource, TableName, base.Request.Browser.Browser);
	}
}


