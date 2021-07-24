using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Land_DumpReport : NBasePage, IRequiresSessionState
{

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.txtStartDate.Text.Trim();
		this.txtEndDate.Text.Trim();
		this.txtEquCode.Text.Trim();
		this.txtEquName.Text.Trim();
		this.txtPrjName.Text.Trim();
	}
	public void BindGv()
	{
		this.txtStartDate.Text.Trim();
		this.txtEndDate.Text.Trim();
		this.txtEquCode.Text.Trim();
		this.txtEquName.Text.Trim();
		this.txtPrjName.Text.Trim();
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
			e.Row.Cells[11].Text = array[0];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[1];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[2];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
			e.Row.Cells[14].Text = array[3];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].CssClass = "decimal_input";
			e.Row.Cells[15].Text = array[4];
			e.Row.Cells[15].Style.Add("text-align", "right");
			e.Row.Cells[15].CssClass = "decimal_input";
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
		new DataTable();
		this.txtStartDate.Text.Trim();
		this.txtEndDate.Text.Trim();
		this.txtEquCode.Text.Trim();
		this.txtEquName.Text.Trim();
		this.txtPrjName.Text.Trim();
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ReportDate"] != null)
		{
			dt.Columns["ReportDate"].ColumnName = "上报日期";
		}
		if (dt.Columns["ConstructionDate"] != null)
		{
			dt.Columns["ConstructionDate"].ColumnName = "施工日期";
		}
		if (dt.Columns["PrjName"] != null)
		{
			dt.Columns["PrjName"].ColumnName = "项目";
		}
		if (dt.Columns["ConstructionPlace"] != null)
		{
			dt.Columns["ConstructionPlace"].ColumnName = "施工区域";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "自卸车编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "自卸车名称";
		}
		if (dt.Columns["WeighbridgeRoom"] != null)
		{
			dt.Columns["WeighbridgeRoom"].ColumnName = "地磅房";
		}
		if (dt.Columns["Sn"] != null)
		{
			dt.Columns["Sn"].ColumnName = "流水号";
		}
		if (dt.Columns["TruckNo"] != null)
		{
			dt.Columns["TruckNo"].ColumnName = "车号";
		}
		if (dt.Columns["ShipEquCode"] != null)
		{
			dt.Columns["ShipEquCode"].ColumnName = "抛石船编号";
		}
		if (dt.Columns["CargoNo"] != null)
		{
			dt.Columns["CargoNo"].ColumnName = "货名";
		}
		if (dt.Columns["GrossWeigh"] != null)
		{
			dt.Columns["GrossWeigh"].ColumnName = "毛重";
		}
		if (dt.Columns["BareWeigh"] != null)
		{
			dt.Columns["BareWeigh"].ColumnName = "空重";
		}
		if (dt.Columns["NetWeigh"] != null)
		{
			dt.Columns["NetWeigh"].ColumnName = "净重";
		}
		if (dt.Columns["CubeQty"] != null)
		{
			dt.Columns["CubeQty"].ColumnName = "方数";
		}
		if (dt.Columns["TruckQty"] != null)
		{
			dt.Columns["TruckQty"].ColumnName = "车数";
		}
		if (dt.Columns["WeighbridgeUserName"] != null)
		{
			dt.Columns["WeighbridgeUserName"].ColumnName = "过磅员";
		}
		if (dt.Columns["GrossWeighDate"] != null)
		{
			dt.Columns["GrossWeighDate"].ColumnName = "毛重日期";
		}
		if (dt.Columns["GrossWeighTime"] != null)
		{
			dt.Columns["GrossWeighTime"].ColumnName = "毛重时间";
		}
		if (dt.Columns["DiscardPlace"] != null)
		{
			dt.Columns["DiscardPlace"].ColumnName = "抛石地点";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		dt.AcceptChanges();
		return dt;
	}
}


