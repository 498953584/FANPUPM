using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.Tender;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_ProfitStatistics : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private DataTable dtProfit = new DataTable();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");
	private int[] types = new int[]
	{
		1,
		2,
		3,
		4,
		5,
		6,
		7,
		8,
		9,
		10,
		11,
		12,
		13,
		14,
		15,
		16,
		17,
		18,
		19
	};

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.dropPrjState, this.types, "", null);
			this.ComputeTotal();
			this.BindGv();
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable profitInfo = ConstructReport.GetProfitInfo(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), base.UserCode, this.dropPrjState.SelectedValue, this.txtPrjPeopleName.Text.Trim(), this.txtPrjStartDate.Text.Trim(), this.txtPrjEndDate.Text.Trim(), 0, 0, this.hfldIsWBSRelevance.Value);
		string[] array = new string[12];
		if (profitInfo.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(profitInfo.Compute("sum(ContractCost)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(profitInfo.Compute("sum(BudCost)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(profitInfo.Compute("sum(ConsTotal)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(profitInfo.Compute("sum(StuffCost)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(profitInfo.Compute("sum(LaborCost)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(profitInfo.Compute("sum(MachineCost)", "1>0")).ToString("0.000");
			array[6] = System.Convert.ToDecimal(profitInfo.Compute("sum(OutSourceCost)", "1>0")).ToString("0.000");
			array[7] = System.Convert.ToDecimal(profitInfo.Compute("sum(ElseCBSCost)", "1>0")).ToString("0.000");
			array[8] = System.Convert.ToDecimal(profitInfo.Compute("sum(DirectCost)", "1>0")).ToString("0.000");
			array[9] = System.Convert.ToDecimal(profitInfo.Compute("sum(IndirectCost)", "1>0")).ToString("0.000");
			array[10] = System.Convert.ToDecimal(profitInfo.Compute("sum(TargetProfit)", "1>0")).ToString("0.000");
			array[11] = System.Convert.ToDecimal(profitInfo.Compute("sum(RealityProfit)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
			array[6] = "0.000";
			array[7] = "0.000";
			array[8] = "0.000";
			array[9] = "0.000";
			array[10] = "0.000";
			array[11] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetProfitCount(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), base.UserCode, this.dropPrjState.SelectedValue, this.txtPrjPeopleName.Text.Trim(), this.txtPrjStartDate.Text.Trim(), this.txtPrjEndDate.Text.Trim());
		this.dtProfit = ConstructReport.GetProfitInfo(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), base.UserCode, this.dropPrjState.SelectedValue, this.txtPrjPeopleName.Text.Trim(), this.txtPrjStartDate.Text.Trim(), this.txtPrjEndDate.Text.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, this.hfldIsWBSRelevance.Value);
		this.SetPrjStates(this.dtProfit);
		this.gvProfit.DataSource = this.dtProfit;
		this.gvProfit.DataBind();
	}
	private void SetPrjStates(DataTable dbInfo)
	{
		System.Collections.Generic.Dictionary<string, string> dictionary = TypeList.GetCodeList(this.types).ToDictionary((TypeList type) => type.Value, (TypeList type) => type.Text);
		if (dbInfo != null)
		{
			dbInfo.Columns.Add("PrjStateName", System.Type.GetType("System.String")).SetOrdinal(7);
			foreach (DataRow dataRow in dbInfo.Rows)
			{
				if (dictionary.ContainsKey(dataRow["PrjState"].ToString()))
				{
					dataRow["PrjStateName"] = dictionary[dataRow["PrjState"].ToString()];
				}
				else
				{
					dataRow["PrjStateName"] = string.Empty;
				}
			}
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvProfit_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Header)
		{
			TableCellCollection cells = e.Row.Cells;
			cells.Clear();
			cells.Add(new TableHeaderCell());
			cells[0].RowSpan = 2;
			cells[0].Text = "序号";
			cells.Add(new TableHeaderCell());
			cells[1].RowSpan = 2;
			cells[1].Text = "项目编号";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "开始日期";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "结束日期";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "项目状态";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 2;
			cells[6].Text = "项目申请人";
			cells.Add(new TableHeaderCell());
			cells[7].RowSpan = 2;
			cells[7].Text = "投标预算";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 2;
			cells[8].Text = "目标成本";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 2;
			cells[9].Text = "报量成本";
			cells.Add(new TableHeaderCell());
			cells[10].ColumnSpan = 6;
			cells[10].Text = "直接费用";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 2;
			cells[11].Text = "间接费用";
			cells.Add(new TableHeaderCell());
			cells[12].ColumnSpan = 2;
			cells[12].Text = "利润</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[13].Text = "直接材料费";
			cells[13].Attributes.Add("title", " 直接材料费 = 出库物资金额 &ndash; 退库物资金额 ");
			cells[13].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[14].Text = "公司项目<br />人员费用";
			cells[14].Attributes.Add("title", " 公司项目人员费用 = 合同类型中成本归集于人工的合同金额 ");
			cells[14].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[15].Text = "公司项目<br />机械设备费用";
			cells[15].Attributes.Add("title", " 公司项目机械设备费用 = 合同类型中成本归集于机械的合同金额 ");
			cells[15].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[16].Text = "外包费用";
			cells[16].Attributes.Add("title", " 外包费用 = 合同类型中成本归集于外包费用的合同金额 ");
			cells[16].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[17].Text = "其他";
			cells[17].Attributes.Add("title", " 其他 = 合同类型中成本归集于其他的合同金额 ");
			cells[17].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[18].Text = "实际直接费用";
			cells[18].Attributes.Add("title", "  实际直接费用 = 直接材料费 + 公司项目人员费用 + 公司项目机械设备费用 + 外包费用 + 其他 ");
			cells[18].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[19].Text = "目标利润";
			cells[19].Attributes.Add("title", "  目标利润 = 投标预算金额 &ndash; 目标成本 ");
			cells[19].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[20].Text = "实际利润";
			cells[20].Attributes.Add("title", "  实际利润 = 投标预算金额 &ndash; 实际直接成本 &ndash; 间接费用 ");
			cells[20].CssClass = "tooltip";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[7].Text = array[0];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[1];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[2];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = array[3];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[4];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[5];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[6];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
			e.Row.Cells[14].Text = array[7];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].CssClass = "decimal_input";
			e.Row.Cells[15].Text = array[8];
			e.Row.Cells[15].Style.Add("text-align", "right");
			e.Row.Cells[15].CssClass = "decimal_input";
			e.Row.Cells[16].Text = array[9];
			e.Row.Cells[16].Style.Add("text-align", "right");
			e.Row.Cells[16].CssClass = "decimal_input";
			e.Row.Cells[17].Text = array[10];
			e.Row.Cells[17].Style.Add("text-align", "right");
			e.Row.Cells[17].CssClass = "decimal_input";
			e.Row.Cells[18].Text = array[11];
			e.Row.Cells[18].Style.Add("text-align", "right");
			e.Row.Cells[18].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = ConstructReport.GetProfitInfo(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), base.UserCode, this.dropPrjState.SelectedValue, this.txtPrjPeopleName.Text.Trim(), this.txtPrjStartDate.Text.Trim(), this.txtPrjEndDate.Text.Trim(), 0, 0, this.hfldIsWBSRelevance.Value);
		this.SetPrjStates(dataTable);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["ContractCost"] = dataTable.Compute("sum(ContractCost)", "1>0");
			dataRow["BudCost"] = dataTable.Compute("sum(BudCost)", "1>0");
			dataRow["ConsTotal"] = dataTable.Compute("sum(ConsTotal)", "1>0");
			dataRow["StuffCost"] = dataTable.Compute("sum(StuffCost)", "1>0");
			dataRow["LaborCost"] = dataTable.Compute("sum(LaborCost)", "1>0");
			dataRow["MachineCost"] = dataTable.Compute("sum(MachineCost)", "1>0");
			dataRow["OutSourceCost"] = dataTable.Compute("sum(OutSourceCost)", "1>0");
			dataRow["ElseCBSCost"] = dataTable.Compute("sum(ElseCBSCost)", "1>0");
			dataRow["DirectCost"] = dataTable.Compute("sum(DirectCost)", "1>0");
			dataRow["IndirectCost"] = dataTable.Compute("sum(IndirectCost)", "1>0");
			dataRow["TargetProfit"] = dataTable.Compute("sum(TargetProfit)", "1>0");
			dataRow["RealityProfit"] = dataTable.Compute("sum(RealityProfit)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("直接费用", 1, 6, 10, 0));
		list.Add(ExcelHeader.Create("利润", 1, 2, 17, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 10)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 10 || dataColumn.Ordinal == 16)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "利润分析", "利润分析", "利润分析.xls", list, null, 3, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["prjGuid"] != null)
		{
			dt.Columns.Remove(dt.Columns["prjGuid"]);
		}
		if (dt.Columns["prjCode"] != null)
		{
			dt.Columns["prjCode"].ColumnName = "项目编号";
		}
		if (dt.Columns["prjName"] != null)
		{
			dt.Columns["prjName"].ColumnName = "项目名称";
		}
		if (dt.Columns["StartDate"] != null)
		{
			dt.Columns["StartDate"].ColumnName = "开始日期";
		}
		if (dt.Columns["EndDate"] != null)
		{
			dt.Columns["EndDate"].ColumnName = "结束日期";
		}
		if (dt.Columns["PrjState"] != null)
		{
			dt.Columns.Remove(dt.Columns["PrjState"]);
		}
		if (dt.Columns["PrjStateName"] != null)
		{
			dt.Columns["PrjStateName"].ColumnName = "项目状态";
		}
		if (dt.Columns["ProjPeopleName"] != null)
		{
			dt.Columns["ProjPeopleName"].ColumnName = "项目申请人";
		}
		if (dt.Columns["ContractCost"] != null)
		{
			dt.Columns["ContractCost"].ColumnName = "投标预算金额";
		}
		if (dt.Columns["BudCost"] != null)
		{
			dt.Columns["BudCost"].ColumnName = "目标成本";
		}
		if (dt.Columns["ConsTotal"] != null)
		{
			dt.Columns["ConsTotal"].ColumnName = "报量成本";
		}
		if (dt.Columns["StuffCost"] != null)
		{
			dt.Columns["StuffCost"].ColumnName = "直接材料费";
		}
		if (dt.Columns["LaborCost"] != null)
		{
			dt.Columns["LaborCost"].ColumnName = "公司项目人员费用";
		}
		if (dt.Columns["MachineCost"] != null)
		{
			dt.Columns["MachineCost"].ColumnName = "公司项目机械设备费用";
		}
		if (dt.Columns["OutSourceCost"] != null)
		{
			dt.Columns["OutSourceCost"].ColumnName = "外包费用";
		}
		if (dt.Columns["ElseCBSCost"] != null)
		{
			dt.Columns["ElseCBSCost"].ColumnName = "其他";
		}
		if (dt.Columns["DirectCost"] != null)
		{
			dt.Columns["DirectCost"].ColumnName = "实际直接成本";
		}
		if (dt.Columns["TargetProfit"] != null)
		{
			dt.Columns["TargetProfit"].ColumnName = "目标利润";
		}
		if (dt.Columns["IndirectCost"] != null)
		{
			dt.Columns["IndirectCost"].ColumnName = "间接费用";
		}
		if (dt.Columns["RealityProfit"] != null)
		{
			dt.Columns["RealityProfit"].ColumnName = "实际利润";
		}
		dt.AcceptChanges();
		return dt;
	}
}


