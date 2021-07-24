using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_IncometInvoice_InvoiceLedger : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();
	private IncometInvoiceBll incometPaymentBll = new IncometInvoiceBll();

	protected override void OnInit(EventArgs e)
	{
		this.gvConract.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public void BindGv()
	{
		this.BindGv(this.GetTable(""));
	}
	public DataTable GetTable(string strWhere)
	{
		strWhere = strWhere + " and p1.UserCodes like '%" + base.UserCode + "%'";
		return this.incometContractBll.GetReportTb(this.txtConType.Text.Trim(), this.txtPrjName.Value, this.txtContractCode.Text.Trim(), this.txtContractName.Text.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.hdParty.Value + "," + this.txtParty.Value.Trim(), "Invoice", strWhere, null);
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvConract.DataSource = storageDataSource;
			this.gvConract.DataBind();
			this.gvConract.HeaderRow.Cells[0].Visible = false;
			this.gvConract.Rows[0].Visible = false;
			return;
		}
		this.gvConract.DataSource = storageDataSource;
		this.gvConract.DataBind();
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal d4 = 0m;
		for (int i = 0; i < storageDataSource.Rows.Count; i++)
		{
			d2 += Convert.ToDecimal(storageDataSource.Rows[i]["EndPrice"].ToString());
			d += Convert.ToDecimal(WebUtil.GetPaymentSum(storageDataSource.Rows[i]["ContractId"].ToString(), "Con_Incomet_Payment", "CllectionPrice"));
			d3 += Convert.ToDecimal(storageDataSource.Rows[i]["InvoicePrice"].ToString());
			d4 += Convert.ToDecimal(this.GetBlInvoice(storageDataSource.Rows[i]["ContractId"].ToString()));
		}
		string[] value = new string[]
		{
			d2.ToString(),
			d.ToString(),
			d3.ToString(),
			d4.ToString()
		};
		int[] index = new int[]
		{
			7,
			8,
			9,
			10
		};
		GridViewUtility.AddTotalRow(this.gvConract, value, index);
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvConract.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvConract.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["isMainContract"] = this.gvConract.DataKeys[e.Row.RowIndex].Values[1].ToString();
			}
			catch
			{
			}
		}
	}
	public string GetParty(string party)
	{
		if (!string.IsNullOrEmpty(party))
		{
			return party.Split(new char[]
			{
				','
			})[1];
		}
		return "";
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, EventArgs e)
	{
		DataTable table = this.GetTable("");
		new Common2().ExportToExecelAll(this.GetTitleByTable(table), "发票台帐.xls", base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		dt = WebUtil.UpdateDataTable(dt, "Invoice", "Party");
		dt.Columns.Add(new DataColumn("累计收款金额"));
		dt.Columns.Add(new DataColumn("累计未开发票额"));
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal d4 = 0m;
		for (int i = 0; i < dt.Rows.Count; i++)
		{
			dt.Rows[i]["累计收款金额"] = Convert.ToDecimal(WebUtil.GetPaymentSum(dt.Rows[i]["ContractId"].ToString(), "Con_Incomet_Payment", "CllectionPrice"));
			dt.Rows[i]["累计未开发票额"] = Convert.ToDecimal(this.GetBlInvoice(dt.Rows[i]["ContractId"].ToString()));
			d2 += Convert.ToDecimal(dt.Rows[i]["EndPrice"].ToString());
			d += Convert.ToDecimal(WebUtil.GetPaymentSum(dt.Rows[i]["ContractId"].ToString(), "Con_Incomet_Payment", "CllectionPrice"));
			d3 += Convert.ToDecimal(dt.Rows[i]["InvoicePrice"].ToString());
			d4 += Convert.ToDecimal(this.GetBlInvoice(dt.Rows[i]["ContractId"].ToString()));
		}
		string[] array = new string[]
		{
			d2.ToString(),
			d.ToString(),
			d3.ToString(),
			d4.ToString()
		};
		DataRow dataRow = dt.NewRow();
		dataRow["prjName"] = "总计";
		dataRow["EndPrice"] = array[0];
		dataRow["累计收款金额"] = array[1];
		dataRow["InvoicePrice"] = array[2];
		dataRow["累计未开发票额"] = array[3];
		dt.Rows.Add(dataRow);
		dt.Columns["Num"].ColumnName = "序号";
		dt.Columns["prjName"].ColumnName = "项目名称";
		dt.Columns["ContractCode"].ColumnName = "合同编号";
		dt.Columns["ContractName"].ColumnName = "合同名称";
		dt.Columns["EndPrice"].ColumnName = "总合同金额";
		dt.Columns["TypeName"].ColumnName = "合同类型";
		dt.Columns["SignedTime"].ColumnName = "签约日期";
		dt.Columns["Party"].ColumnName = "甲方";
		dt.Columns["InvoicePrice"].ColumnName = "累计已开发票额";
		dt.Columns["CodeName"].ColumnName = "付款方式";
		dt.Columns["Notes"].ColumnName = "备注";
		dt.Columns.Remove(dt.Columns["ContractId"]);
		dt.Columns.Remove(dt.Columns["FCode"]);
		dt.Columns.Remove(dt.Columns["Version"]);
		dt.Columns.Remove(dt.Columns["isFContract"]);
		dt.Columns.Remove("conState");
		dt.Columns.Remove("SignPeopleName");
		return dt;
	}
	public string GetBlInvoice(string contractID)
	{
		decimal d = Convert.ToDecimal(WebUtil.GetPaymentSum(contractID, "Con_Incomet_Payment", "CllectionPrice"));
		decimal d2 = Convert.ToDecimal(WebUtil.GetPaymentSum(contractID, "Con_Incomet_Invoice", "Amount"));
		return (d - d2).ToString("0.000");
	}
}


