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
public partial class Fund_MonthPlan_FundPayPlanReport : NBasePage, IRequiresSessionState
{
	private PayoutBalance payoutBalance = new PayoutBalance();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtDate.Text = DateTime.Now.ToString("yyyy-MM", DateTimeFormatInfo.InvariantInfo);
			this.DBbind();
			if (base.Request["PlanType"].ToString() == "payout")
			{
				this.labTitle.Text = "支出计划报表";
			}
			else
			{
				this.labTitle.Text = "收入计划报表";
			}
			this.hfldPrjName.Value = base.UserCode;
		}
	}
	private void DBbind()
	{
		DataTable dataTable = this.GetData();
		DataView defaultView = dataTable.DefaultView;
		defaultView.Sort = " PrjName desc,PlanMoney  DESC, conPayMoney desc";
		dataTable = defaultView.ToTable();
		string text = "0.00";
		if (dataTable.Rows.Count > 0 && dataTable.Compute("SUM(beforePlanMoney)", string.Empty).ToString() != "0.000")
		{
			text = dataTable.Compute("(SUM(conPayMoney)/SUM(beforePlanMoney))*100", string.Empty).ToString();
			text = decimal.Parse(text).ToString("f2");
		}
		string[] value = new string[]
		{
			dataTable.Compute("SUM(beforePlanMoney)", string.Empty).ToString(),
			dataTable.Compute("SUM(PlanMoney)", string.Empty).ToString(),
			dataTable.Compute("sum(conPayMoney)", string.Empty).ToString(),
			text + "%",
			dataTable.Compute("SUM(ExecuteVariation)", string.Empty).ToString()
		};
		decimal num = 0m;
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal num2 = 0m;
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			string a = dataTable.Rows[i][0].ToString();
			if (a == "")
			{
				dataTable.Rows.Remove(dataTable.Rows[i]);
			}
			num += Convert.ToDecimal(dataTable.Rows[i]["beforePlanMoney"].ToString());
			d += Convert.ToDecimal(dataTable.Rows[i]["conPayMoney"].ToString());
			d2 += Convert.ToDecimal(dataTable.Rows[i]["ExecuteVariation"].ToString());
			d3 += Convert.ToDecimal(dataTable.Rows[i]["PlanMoney"].ToString());
			if (dataTable.Rows[i][2].ToString() == "小计")
			{
				dataTable.Rows[i]["beforePlanMoney"] = num.ToString();
				dataTable.Rows[i]["conPayMoney"] = d.ToString();
				dataTable.Rows[i]["ExecuteVariation"] = d2.ToString();
				dataTable.Rows[i]["PlanMoney"] = d3.ToString();
				if (num != 0m)
				{
					num2 = d / num * 100m;
				}
				dataTable.Rows[i]["ExecuteRatio"] = decimal.Parse(num2.ToString()).ToString("f2") + "%";
				num = 0m;
				d = 0m;
				d2 = 0m;
				d3 = 0m;
				num2 = 0m;
			}
		}
		this.gvPayoutPlan.DataSource = dataTable;
		this.gvPayoutPlan.DataBind();
		int[] index = new int[]
		{
			2,
			3,
			4,
			5,
			6
		};
		GridViewUtility.AddTotalRow(this.gvPayoutPlan, value, index);
	}
	protected void btn_Search_Click(object sender, EventArgs e)
	{
		this.DBbind();
	}
	protected void gvPayoutPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvPayoutPlan.PageIndex = e.NewPageIndex;
		this.DBbind();
	}
	protected void btnExcel_Click(object sender, EventArgs e)
	{
		DataTable reportData = this.GetReportData();
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				5
			}
		}, reportData, this.labTitle.Text.ToString() + ".xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	private DataTable GetData()
	{
		string planType = base.Request["PlanType"].ToString();
		Fund_Report fund_Report = new Fund_Report();
		string arg_27_0 = this.txtProject.Text;
		string arg_33_0 = this.txtContract.Text;
		int planYear = DateTime.Now.Year;
		int planMonth = DateTime.Now.Month;
		if (this.txtDate.Text != "")
		{
			string[] array = this.txtDate.Text.Split(new char[]
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
		return fund_Report.getPayoutPlanInfo(base.UserCode, planYear, planMonth, this.txtProject.Text, this.hfldContract.Value, planType);
	}
	private DataTable GetData1()
	{
		string planType = base.Request["PlanType"].ToString();
		Fund_Report fund_Report = new Fund_Report();
		string arg_27_0 = this.txtProject.Text;
		string arg_33_0 = this.txtContract.Text;
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
		DataTable payoutPlanInfo = fund_Report.getPayoutPlanInfo(base.UserCode, planYear, planMonth, this.txtProject.Text, this.hfldContract.Value, planType);
		decimal d = 0m;
		decimal d2 = 0m;
		decimal d3 = 0m;
		decimal d4 = 0m;
		for (int j = 0; j < payoutPlanInfo.Rows.Count; j++)
		{
			d += Convert.ToDecimal(payoutPlanInfo.Rows[j]["beforePlanMoney"].ToString());
			d2 += Convert.ToDecimal(payoutPlanInfo.Rows[j]["conPayMoney"].ToString());
			d3 += Convert.ToDecimal(payoutPlanInfo.Rows[j]["ExecuteVariation"].ToString());
			d4 += Convert.ToDecimal(payoutPlanInfo.Rows[j]["PlanMoney"].ToString());
			if (payoutPlanInfo.Rows[j][2].ToString() == "小计")
			{
				payoutPlanInfo.Rows[j]["beforePlanMoney"] = d.ToString();
				payoutPlanInfo.Rows[j]["conPayMoney"] = d2.ToString();
				payoutPlanInfo.Rows[j]["ExecuteVariation"] = d3.ToString();
				payoutPlanInfo.Rows[j]["PlanMoney"] = d4.ToString();
				d = 0m;
				d2 = 0m;
				d3 = 0m;
				d4 = 0m;
			}
		}
		return payoutPlanInfo;
	}
	private DataTable GetReportData()
	{
		DataTable data = this.GetData1();
		data.Columns["ContractName"].ColumnName = "合同名称";
		data.Columns["prjName"].ColumnName = "所属项目";
		data.Columns["beforePlanMoney"].ColumnName = "上月计划金额";
		data.Columns["conPayMoney"].ColumnName = "本月实际发生";
		data.Columns["planMoney"].ColumnName = "本月计划金额";
		data.Columns["ExecuteRatio"].ColumnName = "计划执行完成情况";
		data.Columns["ExecuteVariation"].ColumnName = "计划执行差异";
		data.Columns["ReMark"].ColumnName = "说明";
		data.Columns.Remove("ContractID");
		data.Columns.Remove("prjGuid");
		data.Columns.Remove("xh");
		return data;
	}
	protected void gvPayoutPlan_DataBound(object sender, EventArgs e)
	{
		int index = 0;
		for (int i = 1; i < this.gvPayoutPlan.Rows.Count; i++)
		{
			Label label = this.gvPayoutPlan.Rows[i].Cells[0].FindControl("labPrjName") as Label;
			Label label2 = this.gvPayoutPlan.Rows[i - 1].Cells[0].FindControl("labPrjName") as Label;
			if (label2.Text.ToString() == label.Text.ToString())
			{
				if (this.gvPayoutPlan.Rows[index].Cells[0].RowSpan == 0)
				{
					this.gvPayoutPlan.Rows[index].Cells[0].RowSpan++;
				}
				this.gvPayoutPlan.Rows[index].Cells[0].RowSpan++;
				this.gvPayoutPlan.Rows[i].Cells[0].Visible = false;
				if (this.gvPayoutPlan.Rows[i].Cells[1].Text == "小计")
				{
					this.gvPayoutPlan.Rows[i].Cells[1].HorizontalAlign = HorizontalAlign.Center;
				}
			}
			else
			{
				index = i;
			}
		}
	}
	protected void gvPayoutPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvPayoutPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvPayoutPlan.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
}


