using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL.Domain;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_IndirectCostAnalysis : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private PTDUTYAction hrAction = new PTDUTYAction();
	private string prjId = string.Empty;
	private string year = string.Empty;
	private string prjName = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
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
			this.BindRpt();
		}
		this.hfldPrjid.Value = this.prjId;
		this.BindPrjName();
		this.hfldPrjName.Value = this.prjName;
	}
	public void BindRpt()
	{
		string cbsCode = this.txtTaskCode.Text.Trim();
		string cbsName = this.txtTaskName.Text.Trim();
		DataTable dataTable;
		if (this.year == "zzjg")
		{
			dataTable = EReport.GetOrganizationCosts(this.prjId, cbsCode, cbsName);
		}
		else
		{
			dataTable = EReport.GetIndirectCosts(this.prjId, cbsCode, cbsName);
		}
		this.ViewState["costs"] = dataTable;
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
		this.txtTaskCode.Text = string.Empty;
		this.txtTaskName.Text = string.Empty;
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindRpt();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable formatDataTable = this.GetFormatDataTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("本月数", 1, 4, 3, 0));
		list.Add(ExcelHeader.Create("累计数", 1, 4, 7, 0));
		foreach (DataColumn dataColumn in formatDataTable.Columns)
		{
			if (dataColumn.Ordinal <= 2)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(formatDataTable, this.prjName + "-间接费用分析报表", "sheet1", "间接费用分析.xls", list, null, 3, base.Request.Browser.Browser);
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
					if (current.Contains("Rate"))
					{
						(e.Item.FindControl("lbl" + current) as Label).Text = dictionary[current].ToString("P2");
					}
					else
					{
						(e.Item.FindControl("lbl" + current) as Label).Text = dictionary[current].ToString("F3");
					}
				}
			}
		}
	}
	protected void SumTotal()
	{
		DataTable dataTable = this.ViewState["costs"] as DataTable;
		if (dataTable.Rows.Count > 0)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = new System.Collections.Generic.Dictionary<string, decimal>();
			for (int i = dataTable.Columns.Count - 1; i >= dataTable.Columns.Count - 4; i--)
			{
				string columnName = dataTable.Columns[i].ColumnName;
				decimal value = decimal.Parse(dataTable.Compute("SUM(" + columnName + ")", "1=1").ToString());
				dictionary.Add(columnName, value);
			}
			dictionary.Add("MonthLower", dictionary["MonthTarget"] - dictionary["MonthReality"]);
			decimal value2 = (dictionary["MonthTarget"] == 0m) ? 0m : (dictionary["MonthLower"] / dictionary["MonthTarget"]);
			dictionary.Add("MonthLowerRate", value2);
			dictionary.Add("TotalLower", dictionary["TotalTarget"] - dictionary["TotalReality"]);
			value2 = ((dictionary["TotalTarget"] == 0m) ? 0m : (dictionary["TotalLower"] / dictionary["TotalTarget"]));
			dictionary.Add("TotalLowerRate", value2);
			this.ViewState["sum"] = dictionary;
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		if (this.ViewState["costs"] != null)
		{
			this.BindRpt();
		}
	}
	private DataTable GetFormatDataTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("CBS编码");
		dataTable.Columns.Add("成本科目");
		dataTable.Columns.Add("目标成本");
		dataTable.Columns.Add("实际成本");
		dataTable.Columns.Add("降低额");
		dataTable.Columns.Add("降低率");
		dataTable.Columns.Add("目标成本 ");
		dataTable.Columns.Add("实际成本 ");
		dataTable.Columns.Add("降低额 ");
		dataTable.Columns.Add("降低率 ");
		int num = 0;
		string cbsCode = this.txtTaskCode.Text.Trim();
		string cbsName = this.txtTaskName.Text.Trim();
		DataTable dataTable2;
		if (this.year == "zzjg")
		{
			dataTable2 = EReport.GetOrganizationCosts(this.prjId, cbsCode, cbsName);
		}
		else
		{
			dataTable2 = EReport.GetIndirectCosts(this.prjId, cbsCode, cbsName);
		}
		foreach (DataRow dataRow in dataTable2.Rows)
		{
			num++;
			DataRow dataRow2 = dataTable.NewRow();
			dataRow2["序号"] = num.ToString();
			dataRow2["CBS编码"] = dataRow["CBSCode"];
			dataRow2["成本科目"] = dataRow["CBSName"];
			dataRow2["目标成本"] = dataRow["MonthTarget"];
			dataRow2["实际成本"] = dataRow["MonthReality"];
			dataRow2["降低额"] = decimal.Parse(dataRow["MonthTarget"].ToString()) - decimal.Parse(dataRow["MonthReality"].ToString());
			string text = 0m.ToString("P2");
			text = ((decimal.Parse(dataRow["MonthTarget"].ToString()) == 0m) ? text : (decimal.Parse(dataRow2["降低额"].ToString()) / decimal.Parse(dataRow["MonthTarget"].ToString())).ToString("P2"));
			dataRow2["降低率"] = text;
			dataRow2["目标成本 "] = dataRow["TotalTarget"];
			dataRow2["实际成本 "] = dataRow["TotalReality"];
			dataRow2["降低额 "] = decimal.Parse(dataRow["TotalTarget"].ToString()) - decimal.Parse(dataRow["TotalReality"].ToString());
			text = 0m.ToString("P2");
			text = ((decimal.Parse(dataRow["TotalTarget"].ToString()) == 0m) ? text : (decimal.Parse(dataRow2["降低额 "].ToString()) / decimal.Parse(dataRow["TotalTarget"].ToString())).ToString("P2"));
			dataRow2["降低率 "] = text;
			dataTable.Rows.Add(dataRow2);
		}
		if (this.ViewState["sum"] != null)
		{
			System.Collections.Generic.Dictionary<string, decimal> dictionary = this.ViewState["sum"] as System.Collections.Generic.Dictionary<string, decimal>;
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["序号"] = "合 计";
			dataRow3["CBS编码"] = string.Empty;
			dataRow3["成本科目"] = string.Empty;
			dataRow3["目标成本"] = dictionary["MonthTarget"];
			dataRow3["实际成本"] = dictionary["MonthReality"];
			dataRow3["降低额"] = dictionary["MonthLower"];
			dataRow3["降低率"] = dictionary["MonthLowerRate"].ToString("P2");
			dataRow3["目标成本 "] = dictionary["TotalTarget"];
			dataRow3["实际成本 "] = dictionary["TotalReality"];
			dataRow3["降低额 "] = dictionary["TotalLower"];
			dataRow3["降低率 "] = dictionary["TotalLowerRate"].ToString("P2");
			dataTable.Rows.Add(dataRow3);
		}
		return dataTable;
	}
	protected string GetTableHeader()
	{
		return "  <h2 align='center'>" + this.prjName + "-间接费用分析</h2>\r\n                               <table style='width:100%; font-size:18px;' border='1'>\r\n                                    <tr>\r\n                                        <td rowspan='2' align='center'>序号 </td>\r\n                                        <td rowspan='2'align='center'>CBS编码 </td>\r\n                                         <td rowspan='2' align='center' >成本科目 </td>\r\n                                        <td colspan='4' align='center'>本月数</td>\r\n                                        <td colspan='4' align='center'>累计数</td>\r\n                                    </tr>\r\n                                    <tr >\r\n                                      \r\n                                        <td  align='center'>\r\n                                            目标成本\r\n                                        </td>\r\n                                        <td  align='center'>\r\n                                            实际成本\r\n                                        </td>\r\n                                        <td align='center'>\r\n                                            降低额\r\n                                        </td>\r\n                                        <td  align='center'>\r\n                                            降低率\r\n                                        </td>\r\n                                         <td  align='center'>\r\n                                            目标成本\r\n                                        </td>\r\n                                        <td  align='center'>\r\n                                            实际成本\r\n                                        </td>\r\n                                        <td  align='center'>\r\n                                            降低额\r\n                                        </td>\r\n                                        <td align='center'>\r\n                                            降低率\r\n                                        </td>\r\n                                    </tr>";
	}
	private void BindPrjName()
	{
        if (this.year != "zzjg" && this.prjId!="")
		{
			PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
			PTPrjInfo byId = pTPrjInfoService.GetById(this.prjId);
			this.prjName = byId.PrjName;
		}
	}
}


