using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_DeployPlanReport : NBasePage, IRequiresSessionState
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
	protected void gvwDeployPlan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[5].Style.Add("text-align", "right");
			e.Row.Cells[5].Text = this.total[0];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].Text = this.total[1];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = this.total[2];
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
		if (dt.Columns["ApplyDate"] != null)
		{
			dt.Columns["ApplyDate"].ColumnName = "申请日期";
		}
		if (dt.Columns["EquCode"] != null)
		{
			dt.Columns["EquCode"].ColumnName = "设备编号";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格型号";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "分项工程";
		}
		if (dt.Columns["Sump"] != null)
		{
			dt.Columns["Sump"].ColumnName = "挖深";
		}
		if (dt.Columns["BudQuantity"] != null)
		{
			dt.Columns["BudQuantity"].ColumnName = "本月预算工程量";
		}
		if (dt.Columns["BudOilWear"] != null)
		{
			dt.Columns["BudOilWear"].ColumnName = "本月预算油耗";
		}
		if (dt.Columns["EnterDate"] != null)
		{
			dt.Columns["EnterDate"].ColumnName = "进场时间";
		}
		if (dt.Columns["EnterArea"] != null)
		{
			dt.Columns["EnterArea"].ColumnName = "进场地点";
		}
		if (dt.Columns["OutDate"] != null)
		{
			dt.Columns["OutDate"].ColumnName = "出场时间";
		}
		if (dt.Columns["OutArea"] != null)
		{
			dt.Columns["OutArea"].ColumnName = "出场地点";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		return dt;
	}
}


