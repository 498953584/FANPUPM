using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Land_RoadMixerReport : NBasePage, IRequiresSessionState
{
	private string[] total = new string[3];

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.ComputeTotal();
			this.BindGvw();
		}
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
	}
	private void BindGvw()
	{
	}
	protected void gvwMixerReport_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = this.total[0];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].Text = this.total[1];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].Text = this.total[2];
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.ComputeTotal();
		this.BindGvw();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
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
			dt.Columns["PrjName"].ColumnName = "项目名称";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "分部分项工程";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "搅拌车编号";
		}
		if (dt.Columns["Driver"] != null)
		{
			dt.Columns["Driver"].ColumnName = "搅拌车司机";
		}
		if (dt.Columns["TruckQty"] != null)
		{
			dt.Columns["TruckQty"].ColumnName = "车数";
		}
		if (dt.Columns["CubeQty"] != null)
		{
			dt.Columns["CubeQty"].ColumnName = "出库方数";
		}
		if (dt.Columns["ExworksUser"] != null)
		{
			dt.Columns["ExworksUser"].ColumnName = "出厂确认人";
		}
		if (dt.Columns["AffirmCubeQty"] != null)
		{
			dt.Columns["AffirmCubeQty"].ColumnName = "现场确认方数";
		}
		if (dt.Columns["Associater"] != null)
		{
			dt.Columns["Associater"].ColumnName = "现场交接人";
		}
		if (dt.Columns["ChargeMan"] != null)
		{
			dt.Columns["ChargeMan"].ColumnName = "现场负责人";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		return dt;
	}
}


