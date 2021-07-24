using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_ShipElseReport : NBasePage, IRequiresSessionState
{
	private string[] total = new string[2];

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
	protected void gvwShipElse_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].Text = this.total[0];
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].Text = this.total[1];
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
		if (dt.Columns["ConstructionPlace"] != null)
		{
			dt.Columns["ConstructionPlace"].ColumnName = "施工区域";
		}
		if (dt.Columns["ShipCode"] != null)
		{
			dt.Columns["ShipCode"].ColumnName = "船舶编号";
		}
		if (dt.Columns["StartDate"] != null)
		{
			dt.Columns["StartDate"].ColumnName = "当天作业开始时间";
		}
		if (dt.Columns["EndDate"] != null)
		{
			dt.Columns["EndDate"].ColumnName = "当天作业完成时间";
		}
		if (dt.Columns["ConstructionDuration"] != null)
		{
			dt.Columns["ConstructionDuration"].ColumnName = "当天施工时间";
		}
		if (dt.Columns["Qty"] != null)
		{
			dt.Columns["Qty"].ColumnName = "当天施工产量";
		}
		if (dt.Columns["Captain"] != null)
		{
			dt.Columns["Captain"].ColumnName = "船长复核";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		return dt;
	}
}


