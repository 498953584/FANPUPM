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
public partial class Fund_MonthPlan_FundPayPlanALL : NBasePage, IRequiresSessionState
{
	private PayoutBalance payoutBalance = new PayoutBalance();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DBbind();
			if (base.Request["PlanType"].ToString() == "payout")
			{
				this.labTitle.Text = "支出计划总报表";
			}
			else
			{
				this.labTitle.Text = "收入计划总报表";
			}
			if (base.Request["PlanType"].ToString() == "allplan")
			{
				this.labTitle.Text = "资金计划汇总表";
			}
			this.txtDate.Text = DateTime.Now.ToString("yyyy-MM", DateTimeFormatInfo.InvariantInfo);
		}
	}
	private void DBbind()
	{
		DataTable data = this.GetData();
		if (base.Request["PlanType"].ToString() == "allplan")
		{
			this.GridView1.DataSource = data;
			this.GridView1.DataBind();
			return;
		}
		DataView defaultView = data.DefaultView;
		defaultView.Sort = " NewPlanMoney  DESC";
		DataTable dataTable = defaultView.ToTable();
		string text = "0.00";
		if (dataTable.Compute("SUM(conPayMoney)", string.Empty).ToString() != "0.000")
		{
			text = dataTable.Compute("(SUM(conPayMoney)/SUM(planMoney))*100", string.Empty).ToString();
			if (!string.IsNullOrEmpty(text))
			{
				text = decimal.Parse(text).ToString("f2");
			}
			else
			{
				text = "0.00";
			}
		}
		string[] value = new string[]
		{
			dataTable.Compute("SUM(planMoney)", string.Empty).ToString(),
			dataTable.Compute("SUM(conPayMoney)", string.Empty).ToString(),
			dataTable.Compute("sum(NewPlanMoney)", string.Empty).ToString(),
			text + "%"
		};
		this.gvPayoutPlan.DataSource = dataTable;
		this.gvPayoutPlan.DataBind();
		int[] index = new int[]
		{
			2,
			3,
			4,
			5
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
	protected void btnExcela_Click(object sender, EventArgs e)
	{
		DataTable reportData = this.GetReportData1();
		FileExport fileExport = new FileExport(new ExcelExporter
		{
			PercentColumns = new int[]
			{
				9
			}
		}, reportData, this.labTitle.Text.ToString() + ".xls");
		fileExport.Export(base.Request.Browser.Browser);
	}
	private DataTable GetData()
	{
		string text = base.Request["PlanType"].ToString();
		Fund_Report fund_Report = new Fund_Report();
		string arg_27_0 = this.txtProject.Text;
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
		DataTable dataTable = new DataTable();
		if (text == "allplan")
		{
			dataTable.Columns.Add("prjguid");
			dataTable.Columns.Add("prjname");
			dataTable.Columns.Add("lastplanincome");
			dataTable.Columns.Add("lastplanpayout");
			dataTable.Columns.Add("lastactuallyincome");
			dataTable.Columns.Add("lastincomecpl");
			dataTable.Columns.Add("lastactuallypayout");
			dataTable.Columns.Add("lastpayoutcpl");
			dataTable.Columns.Add("thisincome");
			dataTable.Columns.Add("thispayout");
			DataTable planInfo = fund_Report.GetPlanInfo(base.UserCode, planYear, planMonth, "income", this.txtProject.Text);
			DataTable planInfo2 = fund_Report.GetPlanInfo(base.UserCode, planYear, planMonth, "payout", this.txtProject.Text);
			foreach (DataRow dataRow in planInfo.Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["prjguid"] = dataRow["PrjGuid"];
				dataRow2["prjname"] = dataRow["PrjName"];
				dataRow2["lastplanincome"] = dataRow["planMoney"];
				dataRow2["lastactuallyincome"] = dataRow["conPayMoney"];
				dataRow2["lastincomecpl"] = dataRow["ratio"];
				dataRow2["thisincome"] = dataRow["NewPlanMoney"];
				DataRow[] array3 = planInfo2.Select(" prjguid='" + dataRow["prjguid"] + "' ");
				if (array3.Length != 0)
				{
					dataRow2["lastplanpayout"] = array3[0]["planMoney"];
					dataRow2["lastactuallypayout"] = array3[0]["conPayMoney"];
					dataRow2["lastpayoutcpl"] = array3[0]["ratio"];
					dataRow2["thispayout"] = array3[0]["NewPlanMoney"];
					dataTable.Rows.Add(dataRow2);
				}
			}
			return dataTable;
		}
		dataTable = fund_Report.GetPlanInfo(base.UserCode, planYear, planMonth, text, this.txtProject.Text);
		return dataTable;
	}
	private DataTable GetReportData()
	{
		DataTable data = this.GetData();
		data.Columns["prjName"].ColumnName = "所属项目";
		data.Columns["planMoney"].ColumnName = "上期计划金额";
		data.Columns["conPayMoney"].ColumnName = "上期实际发生金额";
		data.Columns["NewPlanMoney"].ColumnName = "本期计划金额";
		data.Columns["ratio"].ColumnName = "上期计划执行完成情况";
		data.Columns["Remark"].ColumnName = "说明";
		data.Columns.Remove("prjGuid");
		return data;
	}
	private DataTable GetReportData1()
	{
		DataTable data = this.GetData();
		data.Columns["prjname"].ColumnName = "所属项目";
		data.Columns["lastplanincome"].ColumnName = "上期计划收入金额";
		data.Columns["lastplanpayout"].ColumnName = "上期计划支出金额";
		data.Columns["lastactuallyincome"].ColumnName = "上期实际收入金额";
		data.Columns["lastincomecpl"].ColumnName = "上期实际收入完成情况";
		data.Columns["lastactuallypayout"].ColumnName = "上期实际支出金额";
		data.Columns["lastpayoutcpl"].ColumnName = "上期实际支出完成情况";
		data.Columns["thisincome"].ColumnName = "本期计划收入金额";
		data.Columns["thispayout"].ColumnName = "本期计划支出金额";
		data.Columns.Remove("prjguid");
		return data;
	}
	protected void gvPayoutPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (base.Request["PlanType"].ToString() == "allplan" && e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].Attributes.Add("rowspan", "2");
			cells[0].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[1].Attributes.Add("colspan", "2");
			cells[1].Text = "上期计划金额";
			cells.Add(new TableHeaderCell());
			cells[2].Attributes.Add("colspan", "4");
			cells[2].Text = "上期实际金额";
			cells.Add(new TableHeaderCell());
			cells[3].Attributes.Add("colspan", "2");
			cells[3].Text = "本期计划金额</th></tr><tr>";
			cells.Add(new TableHeaderCell());
			cells[4].Text = "收入";
			cells.Add(new TableHeaderCell());
			cells[5].Text = "支出";
			cells.Add(new TableHeaderCell());
			cells[6].Text = "收入";
			cells.Add(new TableHeaderCell());
			cells[7].Text = "完成情况";
			cells.Add(new TableHeaderCell());
			cells[8].Text = "支出";
			cells.Add(new TableHeaderCell());
			cells[9].Text = "完成情况";
			cells.Add(new TableHeaderCell());
			cells[10].Text = "收入";
			cells.Add(new TableHeaderCell());
			cells[11].Text = "支出";
			cells[4].Attributes["style"] = "background-color:#eef2f5";
			cells[5].Attributes["style"] = "background-color:#eef2f5";
			cells[6].Attributes["style"] = "background-color:#eef2f5";
			cells[7].Attributes["style"] = "background-color:#eef2f5";
			cells[8].Attributes["style"] = "background-color:#eef2f5";
			cells[9].Attributes["style"] = "background-color:#eef2f5";
			cells[10].Attributes["style"] = "background-color:#eef2f5";
			cells[11].Attributes["style"] = "background-color:#eef2f5";
		}
		if (e.Row.RowIndex > -1)
		{
			if (base.Request["PlanType"].ToString() == "allplan")
			{
				e.Row.Attributes["id"] = this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
				e.Row.Attributes["guid"] = this.GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
				return;
			}
			e.Row.Attributes["id"] = this.gvPayoutPlan.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvPayoutPlan.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.GridView1.PageIndex = e.NewPageIndex;
		this.DBbind();
	}
}


