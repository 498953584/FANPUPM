using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL.Domain;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_BudgetAnalysis : NBasePage, IRequiresSessionState
{
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.BindRpt();
		}
	}
	protected void BindRpt()
	{
		string prjCode = this.txtPrjCode.Text.Trim();
		string prjName = this.txtPrjName.Text.Trim();
		string text = this.txtStartDate.Text;
		string text2 = this.txtEndDate.Text;
		this.ViewState["analysis"] = EReport.GetBudAnalysis(prjCode, prjName, text, text2, base.UserCode, this.hfldIsWBSRelevance.Value);
		DataTable dataTable = this.ViewState["analysis"] as DataTable;
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.rptBudget.DataSource = EReport.GetPageDataTable(dataTable, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.rptBudget.DataBind();
		if (this.rptBudget.Items.Count == 0)
		{
			base.RegisterScript("$('#rptBudget tr:last-child').remove();");
		}
	}
	protected void SumTotal()
	{
		DataTable dataTable = this.ViewState["analysis"] as DataTable;
		if (dataTable == null || dataTable.Rows.Count == 0)
		{
			return;
		}
		System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
		for (int i = dataTable.Columns.Count - 3; i >= dataTable.Columns.Count - 7; i--)
		{
			string columnName = dataTable.Columns[i].ColumnName;
			decimal value = decimal.Parse(dataTable.Compute("sum(" + columnName + ")", "1=1").ToString());
			dictionary.Add(columnName, value);
		}
		dictionary.Add("kljhlr", dictionary["Tender"] - dictionary["IndirBud"] - dictionary["Target"]);
		dictionary.Add("klsjyk", dictionary["Tender"] - dictionary["IndirAcc"] - dictionary["Reality"]);
		dictionary.Add("klxmlr", dictionary["IndirBud"] + dictionary["Target"] - dictionary["IndirAcc"] - dictionary["Reality"]);
		decimal value2 = 0m;
		decimal num = System.Convert.ToDecimal(dictionary["IndirBud"] + dictionary["Target"]);
		decimal d = System.Convert.ToDecimal(dictionary["IndirAcc"] + dictionary["Reality"]);
		if (num != 0m)
		{
			value2 = d / num;
		}
		dictionary.Add("percentCompleted", value2);
		this.ViewState["sum"] = dictionary;
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindRpt();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable formatDataTable = this.GetFormatDataTable(true);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("目标成本", 1, 2, 5, 0));
		list.Add(ExcelHeader.Create("实际成本", 1, 2, 7, 0));
		foreach (DataColumn dataColumn in formatDataTable.Columns)
		{
			if (dataColumn.Ordinal <= 4 || dataColumn.Ordinal >= 9)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(formatDataTable, "三算分析总表", "sheet1", "三算分析总表.xls", list, null, 4, base.Request.Browser.Browser);
	}
	private DataTable GetFormatDataTable(bool isAddTotal)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("计划开始日期");
		dataTable.Columns.Add("合同预算");
		dataTable.Columns.Add("直接成本预算");
		dataTable.Columns.Add("间接成本预算");
		dataTable.Columns.Add("直接成本");
		dataTable.Columns.Add("间接成本");
		dataTable.Columns.Add("开累计划利润");
		dataTable.Columns.Add("开累实际盈亏");
		dataTable.Columns.Add("开累项目部利润");
		dataTable.Columns.Add("完成百分比");
		int num = 0;
		DataTable dataTable2 = this.ViewState["analysis"] as DataTable;
		foreach (DataRow dataRow in dataTable2.Rows)
		{
			num++;
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = num.ToString();
			dataRow2["项目编号"] = dataRow["PrjCode"];
			dataRow2["项目名称"] = dataRow["PrjName"];
			dataRow2["计划开始日期"] = Common2.GetTime(dataRow["StartDate"]);
			dataRow2["合同预算"] = decimal.Parse(dataRow["Tender"].ToString()).ToString("#,##0.000");
			dataRow2["直接成本预算"] = decimal.Parse(dataRow["Target"].ToString()).ToString("#,##0.000");
			dataRow2["间接成本预算"] = decimal.Parse(dataRow["IndirBud"].ToString()).ToString("#,##0.000");
			dataRow2["直接成本"] = decimal.Parse(dataRow["Reality"].ToString()).ToString("#,##0.000");
			dataRow2["间接成本"] = decimal.Parse(dataRow["IndirAcc"].ToString()).ToString("#,##0.000");
			dataRow2["开累计划利润"] = (decimal.Parse(dataRow["Tender"].ToString()) - decimal.Parse(dataRow["Target"].ToString()) - decimal.Parse(dataRow["IndirBud"].ToString())).ToString("#,##0.000");
			dataRow2["开累实际盈亏"] = (decimal.Parse(dataRow["Tender"].ToString()) - decimal.Parse(dataRow["Reality"].ToString()) - decimal.Parse(dataRow["IndirAcc"].ToString())).ToString("#,##0.000");
			dataRow2["开累项目部利润"] = (decimal.Parse(dataRow["Target"].ToString()) + decimal.Parse(dataRow["IndirBud"].ToString()) - decimal.Parse(dataRow["Reality"].ToString()) - decimal.Parse(dataRow["IndirAcc"].ToString())).ToString("#,##0.000");
			dataRow2["完成百分比"] = dataRow["PercentCompleted"];
			dataTable.Rows.Add(dataRow2);
		}
		if (isAddTotal && this.ViewState["sum"] != null)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = this.ViewState["sum"] as System.Collections.Generic.Dictionary<string, decimal>;
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["序号"] = "合 计";
			dataRow3["项目编号"] = string.Empty;
			dataRow3["项目名称"] = string.Empty;
			dataRow3["计划开始日期"] = string.Empty;
			dataRow3["合同预算"] = dictionary["Tender"].ToString("#,###.000");
			dataRow3["直接成本预算"] = dictionary["Target"].ToString("#,###.000");
			dataRow3["间接成本预算"] = dictionary["IndirBud"].ToString("#,###.000");
			dataRow3["直接成本"] = dictionary["Reality"].ToString("#,###.000");
			dataRow3["间接成本"] = dictionary["IndirAcc"].ToString("#,###.000");
			dataRow3["开累计划利润"] = dictionary["kljhlr"].ToString("#,###.000");
			dataRow3["开累实际盈亏"] = dictionary["klsjyk"].ToString("#,###.000");
			dataRow3["开累项目部利润"] = dictionary["klxmlr"].ToString("#,###.000");
			dataRow3["完成百分比"] = (decimal.Floor(dictionary["percentCompleted"] * 10000m) / 100m).ToString("0.00") + "%";
			dataTable.Rows.Add(dataRow3);
		}
		return dataTable;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		if (this.ViewState["analysis"] != null)
		{
			DataTable dtAll = this.ViewState["analysis"] as DataTable;
			this.rptBudget.DataSource = EReport.GetPageDataTable(dtAll, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
			this.rptBudget.DataBind();
		}
	}
	protected void rptBudget_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Footer)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
			this.SumTotal();
			dictionary = (this.ViewState["sum"] as System.Collections.Generic.Dictionary<string, decimal>);
			if (dictionary == null)
			{
				return;
			}
			(e.Item.FindControl("lblSumTender") as Label).Text = dictionary["Tender"].ToString();
			(e.Item.FindControl("lblSumIndirBud") as Label).Text = dictionary["IndirBud"].ToString();
			(e.Item.FindControl("lblSumTarget") as Label).Text = dictionary["Target"].ToString();
			(e.Item.FindControl("lblSumIndirAcc") as Label).Text = dictionary["IndirAcc"].ToString();
			(e.Item.FindControl("lblSumReality") as Label).Text = dictionary["Reality"].ToString();
			(e.Item.FindControl("lblSumkljhlr") as Label).Text = dictionary["kljhlr"].ToString();
			(e.Item.FindControl("lblSumklsjyk") as Label).Text = dictionary["klsjyk"].ToString();
			(e.Item.FindControl("lblSumklxmlr") as Label).Text = dictionary["klxmlr"].ToString();
			(e.Item.FindControl("lblPercentCompleted") as Label).Text = (decimal.Floor(dictionary["percentCompleted"] * 10000m) / 100m).ToString("0.00") + "%";
		}
	}
}


