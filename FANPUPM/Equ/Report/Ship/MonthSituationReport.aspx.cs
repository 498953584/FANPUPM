using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_MonthSituationReport : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equipmentSer = new EquEquipmentService();

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.BindYearMonth();
			this.ComputeTotal();
			this.BindGv();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		string date = this.ddlYear.SelectedValue + this.ddlMonth.SelectedValue + "01";
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		DataTable shipReport = this.equipmentSer.GetShipReport(equCode, equName, date, 0, 0);
		string[] array = new string[9];
		if (shipReport.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(shipReport.Compute("sum(Qty)", "1>0")).ToString();
			array[1] = System.Convert.ToDecimal(shipReport.Compute("sum(Amount)", "1>0")).ToString();
			array[2] = System.Convert.ToDecimal(shipReport.Compute("sum(TotalCost)", "1>0")).ToString();
			array[3] = System.Convert.ToDecimal(shipReport.Compute("sum(FixedCost)", "1>0")).ToString();
			array[4] = System.Convert.ToDecimal(shipReport.Compute("sum(RefuelQty)", "1>0")).ToString();
			array[5] = System.Convert.ToDecimal(shipReport.Compute("sum(RefuelAmount)", "1>0")).ToString();
			array[6] = System.Convert.ToDecimal(shipReport.Compute("sum(SalaryCost)", "1>0")).ToString();
			array[7] = System.Convert.ToDecimal(shipReport.Compute("sum(InterestExpense)", "1>0")).ToString();
			array[8] = System.Convert.ToDecimal(shipReport.Compute("sum(Profitloss)", "1>0")).ToString();
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
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		string date = this.ddlYear.SelectedValue + this.ddlMonth.SelectedValue + "01";
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		this.AspNetPager1.RecordCount = this.equipmentSer.GetShipReportCount(equCode, equName, date);
		this.gvReport.DataSource = this.equipmentSer.GetShipReport(equCode, equName, date, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvReport.DataBind();
	}
	protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[4].Text = array[0];
			e.Row.Cells[4].Style.Add("text-align", "right");
			e.Row.Cells[4].CssClass = "decimal_input";
			e.Row.Cells[5].Text = array[1];
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].CssClass = "decimal_input";
			e.Row.Cells[7].Text = array[2];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[3];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[9].Text = array[4];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = array[5];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[6];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[7];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[19].Text = array[8];
			e.Row.Cells[19].Style.Add("text-align", "right");
			e.Row.Cells[19].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		string date = this.ddlYear.SelectedValue + this.ddlMonth.SelectedValue + "01";
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		dataTable = this.equipmentSer.GetShipReport(equCode, equName, date, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["Qty"] = System.Convert.ToDecimal(dataTable.Compute("sum(Qty)", "1>0")).ToString();
			dataRow["Amount"] = System.Convert.ToDecimal(dataTable.Compute("sum(Amount)", "1>0")).ToString();
			dataRow["TotalCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(TotalCost)", "1>0")).ToString();
			dataRow["FixedCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(FixedCost)", "1>0")).ToString();
			dataRow["RefuelQty"] = System.Convert.ToDecimal(dataTable.Compute("sum(RefuelQty)", "1>0")).ToString();
			dataRow["RefuelAmount"] = System.Convert.ToDecimal(dataTable.Compute("sum(RefuelAmount)", "1>0")).ToString();
			dataRow["SalaryCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(SalaryCost)", "1>0")).ToString();
			dataRow["InterestExpense"] = System.Convert.ToDecimal(dataTable.Compute("sum(InterestExpense)", "1>0")).ToString();
			dataRow["Profitloss"] = System.Convert.ToDecimal(dataTable.Compute("sum(Profitloss)", "1>0")).ToString();
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "船机月度经营情况报表", "船机月度经营情况报表", "船机月度经营情况报表.xls", null, null, 0, base.Request.Browser.Browser);
	}
	private void BindYearMonth()
	{
		for (int i = 1; i <= 12; i++)
		{
			this.ddlMonth.Items.Add(new ListItem(i.ToString() + "月", i.ToString("00")));
		}
		for (int j = System.DateTime.Now.Year - 4; j <= System.DateTime.Now.Year; j++)
		{
			this.ddlYear.Items.Add(new ListItem(j.ToString() + "年", j.ToString("")));
		}
		this.ddlYear.SelectedValue = System.DateTime.Now.Year.ToString();
		this.ddlMonth.SelectedValue = System.DateTime.Now.Month.ToString("00");
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["VariableCost"] != null)
		{
			dt.Columns.Remove(dt.Columns["VariableCost"]);
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "船舶编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "船舶名称";
		}
		if (dt.Columns["PrjNames"] != null)
		{
			dt.Columns["PrjNames"].ColumnName = "施工项目";
		}
		if (dt.Columns["Qty"] != null)
		{
			dt.Columns["Qty"].ColumnName = "总产量(m³)";
		}
		if (dt.Columns["Amount"] != null)
		{
			dt.Columns["Amount"].ColumnName = "总产值(元)";
		}
		if (dt.Columns["BudPrice"] != null)
		{
			dt.Columns["BudPrice"].ColumnName = "加权单价(元/m³)";
		}
		if (dt.Columns["TotalCost"] != null)
		{
			dt.Columns["TotalCost"].ColumnName = "总成本(元)";
		}
		if (dt.Columns["FixedCost"] != null)
		{
			dt.Columns["FixedCost"].ColumnName = "固定成本(元)";
		}
		if (dt.Columns["RefuelQty"] != null)
		{
			dt.Columns["RefuelQty"].ColumnName = "油耗(KG)";
		}
		if (dt.Columns["RefuelAmount"] != null)
		{
			dt.Columns["RefuelAmount"].ColumnName = "油耗(元)";
		}
		if (dt.Columns["SalaryCost"] != null)
		{
			dt.Columns["SalaryCost"].ColumnName = "员工工资(元)";
		}
		if (dt.Columns["InterestExpense"] != null)
		{
			dt.Columns["InterestExpense"].ColumnName = "利息支出(元)";
		}
		if (dt.Columns["FixedRate"] != null)
		{
			dt.Columns["FixedRate"].ColumnName = "固定成本占总成本比重(%)";
		}
		if (dt.Columns["SalaryCostRate"] != null)
		{
			dt.Columns["SalaryCostRate"].ColumnName = "员工工资占总成本比重(%)";
		}
		if (dt.Columns["InterestCostRate"] != null)
		{
			dt.Columns["InterestCostRate"].ColumnName = "利息支出占总成本比重(%)";
		}
		if (dt.Columns["UnitVariableCost"] != null)
		{
			dt.Columns["UnitVariableCost"].ColumnName = "单位变动成本(元/m³)";
		}
		if (dt.Columns["UnitRefuelAmount"] != null)
		{
			dt.Columns["UnitRefuelAmount"].ColumnName = "单位产量油耗(元/m³)";
		}
		if (dt.Columns["UnitTotalCost"] != null)
		{
			dt.Columns["UnitTotalCost"].ColumnName = "单位产量成本(元/m³)";
		}
		if (dt.Columns["Profitloss"] != null)
		{
			dt.Columns["Profitloss"].ColumnName = "整体盈亏(元)";
		}
		if (dt.Columns["EquilibriumPoint"] != null)
		{
			dt.Columns["EquilibriumPoint"].ColumnName = "盈亏平衡点";
		}
		dt.AcceptChanges();
		return dt;
	}
}


