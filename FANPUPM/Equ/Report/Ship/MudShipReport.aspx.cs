using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_MudShipReport : NBasePage, IRequiresSessionState
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
	protected void gvwMudShipReport_RowDataBound(object sender, GridViewRowEventArgs e)
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
			cells[1].Text = "上报日期";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "施工日期";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "项目名称";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "施工区域";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "泥驳船编号";
			cells.Add(new TableHeaderCell());
			cells[6].RowSpan = 2;
			cells[6].Text = "舱容";
			cells.Add(new TableHeaderCell());
			cells[7].ColumnSpan = 2;
			cells[7].Text = "装驳起止时间";
			cells.Add(new TableHeaderCell());
			cells[8].RowSpan = 2;
			cells[8].Text = "装驳时长（小时）";
			cells.Add(new TableHeaderCell());
			cells[9].RowSpan = 2;
			cells[9].Text = "扣方";
			cells.Add(new TableHeaderCell());
			cells[10].RowSpan = 2;
			cells[10].Text = "泥驳总方量";
			cells.Add(new TableHeaderCell());
			cells[11].RowSpan = 2;
			cells[11].Text = "开单员";
			cells.Add(new TableHeaderCell());
			cells[12].RowSpan = 2;
			cells[12].Text = "船长复核";
			cells.Add(new TableHeaderCell());
			cells[13].RowSpan = 2;
			cells[13].Text = "备注</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[14].RowSpan = 1;
			cells[14].Text = "起始时间";
			cells.Add(new TableHeaderCell());
			cells[15].RowSpan = 1;
			cells[15].Text = "终止时间";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].Text = this.total[0];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].Text = this.total[1];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].Text = this.total[2];
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
		if (dt.Columns["MudCode"] != null)
		{
			dt.Columns["MudCode"].ColumnName = "泥驳船编号";
		}
		if (dt.Columns["CabinCapacity"] != null)
		{
			dt.Columns["CabinCapacity"].ColumnName = "舱容";
		}
		if (dt.Columns["StartDate"] != null)
		{
			dt.Columns["StartDate"].ColumnName = "起始时间";
		}
		if (dt.Columns["EndDate"] != null)
		{
			dt.Columns["EndDate"].ColumnName = "终止时间";
		}
		if (dt.Columns["ConstructionDuration"] != null)
		{
			dt.Columns["ConstructionDuration"].ColumnName = "装驳时长（小时）";
		}
		if (dt.Columns["DeductQuantity"] != null)
		{
			dt.Columns["DeductQuantity"].ColumnName = "扣方";
		}
		if (dt.Columns["MudTotalQty"] != null)
		{
			dt.Columns["MudTotalQty"].ColumnName = "泥驳总方量";
		}
		if (dt.Columns["BillingUser"] != null)
		{
			dt.Columns["BillingUser"].ColumnName = "开单员";
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


