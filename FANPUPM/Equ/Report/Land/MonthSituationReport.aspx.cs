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
public partial class Equ_Report_Land_MonthSituationReport : NBasePage, IRequiresSessionState
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
		DataTable roadReport = this.equipmentSer.GetRoadReport(equCode, equName, date, 0, 0);
		string[] array = new string[5];
		if (roadReport.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(roadReport.Compute("sum(SumQty)", "1>0")).ToString();
			array[1] = System.Convert.ToDecimal(roadReport.Compute("sum(TeamQty)", "1>0")).ToString();
			array[2] = System.Convert.ToDecimal(roadReport.Compute("sum(Amount)", "1>0")).ToString();
			array[3] = System.Convert.ToDecimal(roadReport.Compute("sum(TotalCost)", "1>0")).ToString();
			array[4] = System.Convert.ToDecimal(roadReport.Compute("sum(Profitloss)", "1>0")).ToString();
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	public void BindGv()
	{
		string date = this.ddlYear.SelectedValue + this.ddlMonth.SelectedValue + "01";
		string equCode = this.txtEquCode.Text.Trim();
		string equName = this.txtEquName.Text.Trim();
		this.AspNetPager1.RecordCount = this.equipmentSer.GetRoadReportCount(equCode, equName, date);
		this.gvReport.DataSource = this.equipmentSer.GetRoadReport(equCode, equName, date, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
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
			e.Row.Cells[9].Text = array[0];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].CssClass = "decimal_input";
			e.Row.Cells[10].Text = array[1];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[2];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[3];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[26].Text = array[4];
			e.Row.Cells[26].Style.Add("text-align", "right");
			e.Row.Cells[26].CssClass = "decimal_input";
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
		dataTable = this.equipmentSer.GetRoadReport(equCode, equName, date, 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["SumQty"] = System.Convert.ToDecimal(dataTable.Compute("sum(SumQty)", "1>0")).ToString();
			dataRow["TeamQty"] = System.Convert.ToDecimal(dataTable.Compute("sum(TeamQty)", "1>0")).ToString();
			dataRow["Amount"] = System.Convert.ToDecimal(dataTable.Compute("sum(Amount)", "1>0")).ToString();
			dataRow["TotalCost"] = System.Convert.ToDecimal(dataTable.Compute("sum(TotalCost)", "1>0")).ToString();
			dataRow["Profitloss"] = System.Convert.ToDecimal(dataTable.Compute("sum(Profitloss)", "1>0")).ToString();
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "陆上设备月度经营情况报表", "陆上设备月度经营情况报表", "陆上设备月度经营情况报表.xls", null, null, 0, base.Request.Browser.Browser);
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
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "设备编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "设备名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["PrjNames"] != null)
		{
			dt.Columns["PrjNames"].ColumnName = "施工项目";
		}
		if (dt.Columns["UserNames"] != null)
		{
			dt.Columns["UserNames"].ColumnName = "设备人员";
		}
		if (dt.Columns["PurchasePrice"] != null)
		{
			dt.Columns["PurchasePrice"].ColumnName = "设备原值";
		}
		if (dt.Columns["UnitName"] != null)
		{
			dt.Columns["UnitName"].ColumnName = "计量单位";
		}
		if (dt.Columns["BudPrice"] != null)
		{
			dt.Columns["BudPrice"].ColumnName = "单价";
		}
		if (dt.Columns["SumQty"] != null)
		{
			dt.Columns["SumQty"].ColumnName = "产量上报量";
		}
		if (dt.Columns["TeamQty"] != null)
		{
			dt.Columns["TeamQty"].ColumnName = "台班上报量";
		}
		if (dt.Columns["Amount"] != null)
		{
			dt.Columns["Amount"].ColumnName = "收入合计金额";
		}
		if (dt.Columns["TotalCost"] != null)
		{
			dt.Columns["TotalCost"].ColumnName = "支出合计金额";
		}
		if (dt.Columns["ConstructionDates"] != null)
		{
			dt.Columns["ConstructionDates"].ColumnName = "设备月实作天数";
		}
		if (dt.Columns["ConstructionDatesRate"] != null)
		{
			dt.Columns["ConstructionDatesRate"].ColumnName = "设备利用率(%)";
		}
		if (dt.Columns["InGoodDays"] != null)
		{
			dt.Columns["InGoodDays"].ColumnName = "设备月完好天数";
		}
		if (dt.Columns["MonthTotalDates"] != null)
		{
			dt.Columns["MonthTotalDates"].ColumnName = "设备月制度台日数";
		}
		if (dt.Columns["InGoodDaysRate"] != null)
		{
			dt.Columns["InGoodDaysRate"].ColumnName = "设备完好率(%)";
		}
		if (dt.Columns["UnitRefuelQty"] != null)
		{
			dt.Columns["UnitRefuelQty"].ColumnName = "单位产量油耗(L/单位)";
		}
		if (dt.Columns["UnitRefuelAmount"] != null)
		{
			dt.Columns["UnitRefuelAmount"].ColumnName = "单位产量油耗(元/单位)";
		}
		if (dt.Columns["UnitTotalCost"] != null)
		{
			dt.Columns["UnitTotalCost"].ColumnName = "单位产量成本(元/单位)";
		}
		if (dt.Columns["UnitVariableCost"] != null)
		{
			dt.Columns["UnitVariableCost"].ColumnName = "单位变动成本(元/单位)";
		}
		if (dt.Columns["SalaryCostRate"] != null)
		{
			dt.Columns["SalaryCostRate"].ColumnName = "员工工资占总成本比重(%)";
		}
		if (dt.Columns["FixedRate"] != null)
		{
			dt.Columns["FixedRate"].ColumnName = "固定成本占总成本比重(%)";
		}
		if (dt.Columns["VariableRate"] != null)
		{
			dt.Columns["VariableRate"].ColumnName = "变动成本占总成本比重(%)";
		}
		if (dt.Columns["InterestCostRate"] != null)
		{
			dt.Columns["InterestCostRate"].ColumnName = "利息支出占总成本比重(%)";
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


