using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Globalization;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Fund_MonthPlan_FundOverheadReport : NBasePage, IRequiresSessionState
{
	private PayoutBalance payoutBalance = new PayoutBalance();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtDate.Text = DateTime.Now.ToString("yyyy-MM", DateTimeFormatInfo.InvariantInfo);
			this.DBbind();
		}
	}
	public void DBbind()
	{
		DataTable dataTable = this.GetData();
		DataView defaultView = dataTable.DefaultView;
		defaultView.Sort = " prjName desc,NewAmount  DESC,agoAmount desc";
		dataTable = defaultView.ToTable();
		string text = "0.00";
		if (dataTable.Rows.Count > 0 && dataTable.Compute("SUM(agoAmount)", string.Empty).ToString() != "0.000")
		{
			text = dataTable.Compute("(SUM(payAmount)/SUM(agoAmount))*100", string.Empty).ToString();
			text = decimal.Parse(text).ToString("f4");
		}
		string[] value = new string[]
		{
			dataTable.Compute("SUM(agoAmount)", string.Empty).ToString(),
			dataTable.Compute("SUM(payAmount)", string.Empty).ToString(),
			dataTable.Compute("sum(NewAmount)", string.Empty).ToString(),
			text + "%",
			dataTable.Compute("SUM(balance)", string.Empty).ToString()
		};
		decimal num = 0m;
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal num2 = 0m;
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			num += Convert.ToDecimal(dataTable.Rows[i]["agoAmount"].ToString());
			d += Convert.ToDecimal(dataTable.Rows[i]["payAmount"].ToString());
			d3 += Convert.ToDecimal(dataTable.Rows[i]["balance"].ToString());
			d2 += Convert.ToDecimal(dataTable.Rows[i]["NewAmount"].ToString());
			if (dataTable.Rows[i][1].ToString() == "小计")
			{
				dataTable.Rows[i]["agoAmount"] = num.ToString();
				dataTable.Rows[i]["payAmount"] = d.ToString();
				dataTable.Rows[i]["balance"] = d3.ToString();
				dataTable.Rows[i]["NewAmount"] = d2.ToString();
				if (num != 0m)
				{
					num2 = d / num;
				}
				dataTable.Rows[i]["ratio"] = decimal.Parse(num2.ToString()).ToString("f4");
				num = 0m;
				d = 0m;
				d2 = 0m;
				d3 = 0m;
				num2 = 0m;
			}
		}
		this.gvOverhead.DataSource = dataTable;
		this.gvOverhead.DataBind();
		int[] index = new int[]
		{
			2,
			3,
			4,
			5,
			6
		};
		GridViewUtility.AddTotalRow(this.gvOverhead, value, index);
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.DBbind();
	}
	protected void gvPayoutPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvOverhead.PageIndex = e.NewPageIndex;
		this.DBbind();
	}
	protected void btnExcel_Click(object sender, EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				6
			}
		}, reportData, "支出计划报表.xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	private DataTable GetData()
	{
		Fund_Report fund_Report = new Fund_Report();
		int planYear = DateTime.Now.Year;
		int planMonth = DateTime.Now.Month;
		if (this.txtDate.Text.ToString() != "")
		{
			string[] array = this.txtDate.Text.ToString().Split(new char[]
			{
				'-'
			});
			int num = 0;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string value = array2[i];
				if (num == 0)
				{
					planYear = Convert.ToInt32(value);
				}
				else
				{
					if (num == 1)
					{
						planMonth = Convert.ToInt32(value);
					}
				}
				num++;
			}
		}
		return fund_Report.GetIndirectInfo(planYear, planMonth, this.hdnProjectCode.Value);
	}
	private DataTable GetData1()
	{
		DataTable dataTable = this.GetData();
		DataView defaultView = dataTable.DefaultView;
		defaultView.Sort = " prjName desc,NewAmount  DESC,agoAmount desc";
		dataTable = defaultView.ToTable();
		string text = "0.00";
		if (dataTable.Compute("SUM(agoAmount)", string.Empty).ToString() != "0.000")
		{
			text = dataTable.Compute("(SUM(payAmount)/SUM(agoAmount))*100", string.Empty).ToString();
			text = decimal.Parse(text).ToString("f4");
		}
		dataTable.Compute("SUM(agoAmount)", string.Empty).ToString();
		dataTable.Compute("SUM(payAmount)", string.Empty).ToString();
		dataTable.Compute("sum(NewAmount)", string.Empty).ToString();
		dataTable.Compute("SUM(balance)", string.Empty).ToString();
		decimal num = 0m;
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal num2 = 0m;
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			num += Convert.ToDecimal(dataTable.Rows[i]["agoAmount"].ToString());
			d += Convert.ToDecimal(dataTable.Rows[i]["payAmount"].ToString());
			d3 += Convert.ToDecimal(dataTable.Rows[i]["balance"].ToString());
			d2 += Convert.ToDecimal(dataTable.Rows[i]["NewAmount"].ToString());
			if (dataTable.Rows[i][1].ToString() == "小计")
			{
				dataTable.Rows[i]["agoAmount"] = num.ToString();
				dataTable.Rows[i]["payAmount"] = d.ToString();
				dataTable.Rows[i]["balance"] = d3.ToString();
				dataTable.Rows[i]["NewAmount"] = d2.ToString();
				if (num != 0m)
				{
					num2 = d / num;
				}
				dataTable.Rows[i]["ratio"] = decimal.Parse(num2.ToString()).ToString("f4");
				num = 0m;
				d = 0m;
				d2 = 0m;
				d3 = 0m;
				num2 = 0m;
			}
		}
		return dataTable;
	}
	private DataTable GetReportData()
	{
		DataTable data = this.GetData1();
		data.Columns["Name"].ColumnName = "所属科目";
		data.Columns["prjName"].ColumnName = "项目";
		data.Columns["agoAmount"].ColumnName = "上期计划金额";
		data.Columns["payAmount"].ColumnName = "上期实际发生";
		data.Columns["NewAmount"].ColumnName = "本期计划金额";
		data.Columns["ratio"].ColumnName = "上期计划执行完成情况";
		data.Columns["balance"].ColumnName = "上期实际数与计划数差异";
		data.Columns.Remove("CBSCode");
		data.Columns.Remove("prjGuid");
		data.Columns.Remove("xh");
		return data;
	}
	protected void gvOverhead_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvOverhead.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvOverhead.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void gvOverhead_DataBound(object sender, EventArgs e)
	{
		int index = 0;
		for (int i = 1; i < this.gvOverhead.Rows.Count; i++)
		{
			if (this.gvOverhead.Rows[i].Cells[0].Text == this.gvOverhead.Rows[i - 1].Cells[0].Text)
			{
				if (this.gvOverhead.Rows[index].Cells[0].RowSpan == 0)
				{
					this.gvOverhead.Rows[index].Cells[0].RowSpan++;
				}
				this.gvOverhead.Rows[index].Cells[0].RowSpan++;
				this.gvOverhead.Rows[i].Cells[0].Visible = false;
				if (this.gvOverhead.Rows[i].Cells[1].Text == "小计")
				{
					this.gvOverhead.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
				}
			}
			else
			{
				index = i;
			}
		}
	}
}


