using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_EquLeaseReport : NBasePage, IRequiresSessionState
{
	private string total = string.Empty;

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
			e.Row.Cells[9].Style.Add("text-align", "right");
			e.Row.Cells[9].Text = this.total;
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
		if (dt.Columns["EquName"] != null)
		{
			dt.Columns["EquName"].ColumnName = "设备名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格型号";
		}
		if (dt.Columns["EquCode"] != null)
		{
			dt.Columns["EquCode"].ColumnName = "设备编号";
		}
		if (dt.Columns["ACorp"] != null)
		{
			dt.Columns["ACorp"].ColumnName = "使用部门";
		}
		if (dt.Columns["BCorp"] != null)
		{
			dt.Columns["BCorp"].ColumnName = "租赁公司、部门";
		}
		if (dt.Columns["StartDate"] != null)
		{
			dt.Columns["StartDate"].ColumnName = "起租时间";
		}
		if (dt.Columns["EndDate"] != null)
		{
			dt.Columns["EndDate"].ColumnName = "停租时间";
		}
		if (dt.Columns["ChargeMan"] != null)
		{
			dt.Columns["ChargeMan"].ColumnName = "负责人/ 使用人";
		}
		if (dt.Columns["Cost"] != null)
		{
			dt.Columns["Cost"].ColumnName = "租用费用";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		return dt;
	}
}


