using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
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
public partial class BudgetManage_Report_BudgetDetail : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string prjName = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			if (byId != null)
			{
				this.prjName = byId.PrjName;
			}
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.BindRpt();
		}
	}
	public void BindRpt()
	{
		string taskCode = this.txtTaskCode.Text.Trim();
		string taskName = this.txtTaskName.Text.Trim();
		this.ViewState["detail"] = EReport.GetBudAnalysisDetail(this.prjId, taskCode, taskName, this.hfldIsWBSRelevance.Value);
		DataTable dataTable = this.ViewState["detail"] as DataTable;
		if (dataTable == null)
		{
			return;
		}
		this.AspNetPager1.RecordCount = dataTable.Rows.Count;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.rptBudget.DataSource = EReport.GetPageDataTable(dataTable, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.rptBudget.DataBind();
		if (this.rptBudget.Items.Count == 0)
		{
			base.RegisterScript("$('#rptBudget tr:last-child').remove();");
		}
	}
	protected void ClearSearchCondition()
	{
		if (this.ViewState["sum"] != null)
		{
			this.ViewState["sum"] = null;
		}
		this.txtTaskCode.Text = string.Empty;
		this.txtTaskName.Text = string.Empty;
		this.AspNetPager1.CurrentPageIndex = 1;
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
		list.Add(ExcelHeader.Create("合同预算", 1, 2, 3, 0));
		list.Add(ExcelHeader.Create("目标成本", 1, 2, 5, 0));
		list.Add(ExcelHeader.Create("实际成本", 1, 2, 7, 0));
		foreach (DataColumn dataColumn in formatDataTable.Columns)
		{
			if (dataColumn.Ordinal <= 2 || dataColumn.Ordinal >= 9)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(formatDataTable, this.prjName + "三算分析明细表", "sheet1", "三算分析明细表.xls", list, null, 3, base.Request.Browser.Browser);
	}
	protected void rptBudget_ItemDataBound(object sender, RepeaterItemEventArgs e)
	{
		if (e.Item.ItemType == ListItemType.Footer)
		{
			this.SumTotal();
			System.Collections.Generic.Dictionary<string, decimal> dictionary = this.ViewState["sum"] as System.Collections.Generic.Dictionary<string, decimal>;
			if (dictionary != null)
			{
				foreach (string current in dictionary.Keys)
				{
					(e.Item.FindControl("lbl" + current) as Label).Text = dictionary[current].ToString();
				}
			}
		}
	}
	protected void SumTotal()
	{
		DataTable dataTable = this.ViewState["detail"] as DataTable;
		if (dataTable.Rows.Count > 0)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
			for (int i = dataTable.Columns.Count - 1; i >= dataTable.Columns.Count - 6; i--)
			{
				string columnName = dataTable.Columns[i].ColumnName;
				decimal value = 0m;
				if (columnName == "TenderTotal")
				{
					string text = dataTable.Compute("sum(" + columnName + ")", "1=1").ToString();
					if (!string.IsNullOrEmpty(text))
					{
						value = decimal.Parse(text);
					}
				}
				else
				{
					if (columnName == "TargetTotal")
					{
						if (this.isWBSRelevance == "0")
						{
							string text2 = dataTable.Compute("sum(" + columnName + ")", "len(OrderNumber)=3").ToString();
							if (!string.IsNullOrEmpty(text2))
							{
								value = decimal.Parse(text2);
							}
						}
						else
						{
							string text3 = dataTable.Compute("sum(" + columnName + ")", "1=1").ToString();
							if (!string.IsNullOrEmpty(text3))
							{
								value = decimal.Parse(text3);
							}
						}
					}
					else
					{
						string text4 = dataTable.Compute("sum(" + columnName + ")", "1=1").ToString();
						if (!string.IsNullOrEmpty(text4))
						{
							value = decimal.Parse(text4);
						}
					}
				}
				if (columnName != "RealityPrice" && columnName != "TargetPrice" && columnName != "TenderPrice")
				{
					dictionary.Add(columnName, value);
				}
			}
			dictionary.Add("kljhlr", dictionary["TenderTotal"] - dictionary["TargetTotal"]);
			dictionary.Add("klsjyk", dictionary["TenderTotal"] - dictionary["RealityTotal"]);
			dictionary.Add("klxmlr", dictionary["TargetTotal"] - dictionary["RealityTotal"]);
			this.ViewState["sum"] = dictionary;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		if (this.ViewState["detail"] != null)
		{
			this.BindRpt();
		}
	}
	private DataTable GetFormatDataTable(bool isAddTotal)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("任务编码");
		dataTable.Columns.Add("任务名称");
		dataTable.Columns.Add("总价");
		dataTable.Columns.Add("单价");
		dataTable.Columns.Add("总价 ");
		dataTable.Columns.Add("单价 ");
		dataTable.Columns.Add("总价  ");
		dataTable.Columns.Add("单价  ");
		dataTable.Columns.Add("开累计划利润");
		dataTable.Columns.Add("开累实际盈亏");
		dataTable.Columns.Add("开累项目部利润");
		int num = 0;
		DataTable dataTable2 = this.ViewState["detail"] as DataTable;
		if (dataTable2 != null)
		{
			foreach (DataRow dataRow in dataTable2.Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["任务编码"] = dataRow["TaskCode"];
				dataRow2["任务名称"] = dataRow["TaskName"];
				dataRow2["总价"] = decimal.Parse(dataRow["TenderTotal"].ToString()).ToString("#,##0.000");
				dataRow2["单价"] = decimal.Parse(dataRow["TenderPrice"].ToString()).ToString("#,##0.000");
				dataRow2["总价 "] = decimal.Parse(dataRow["TargetTotal"].ToString()).ToString("#,##0.000");
				dataRow2["单价 "] = decimal.Parse(dataRow["TargetPrice"].ToString()).ToString("#,##0.000");
				dataRow2["总价  "] = decimal.Parse(dataRow["RealityTotal"].ToString()).ToString("#,##0.000");
				dataRow2["单价  "] = decimal.Parse(dataRow["RealityPrice"].ToString()).ToString("#,##0.000");
				dataRow2["开累计划利润"] = (decimal.Parse(dataRow["TenderTotal"].ToString()) - decimal.Parse(dataRow["TargetTotal"].ToString())).ToString("#,##0.000");
				dataRow2["开累实际盈亏"] = (decimal.Parse(dataRow["TenderTotal"].ToString()) - decimal.Parse(dataRow["RealityTotal"].ToString())).ToString("#,##0.000");
				dataRow2["开累项目部利润"] = (decimal.Parse(dataRow["TargetTotal"].ToString()) - decimal.Parse(dataRow["RealityTotal"].ToString())).ToString("#,##0.000");
				dataTable.Rows.Add(dataRow2);
			}
			if (isAddTotal && this.ViewState["sum"] != null)
			{
				System.Collections.Generic.Dictionary<string, decimal> dictionary = this.ViewState["sum"] as System.Collections.Generic.Dictionary<string, decimal>;
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["序号"] = "合 计";
				dataRow3["任务编码"] = string.Empty;
				dataRow3["任务名称"] = string.Empty;
				dataRow3["总价"] = dictionary["TenderTotal"].ToString("#,##0.000");
				dataRow3["总价 "] = dictionary["TargetTotal"].ToString("#,##0.000");
				dataRow3["总价  "] = dictionary["RealityTotal"].ToString("#,##0.000");
				dataRow3["开累计划利润"] = dictionary["kljhlr"].ToString("#,##0.000");
				dataRow3["开累实际盈亏"] = dictionary["klsjyk"].ToString("#,##0.000");
				dataRow3["开累项目部利润"] = dictionary["klxmlr"].ToString("#,##0.000");
				dataTable.Rows.Add(dataRow3);
			}
		}
		return dataTable;
	}
}


