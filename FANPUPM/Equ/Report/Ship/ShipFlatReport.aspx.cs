using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_ShipFlatReport : NBasePage, IRequiresSessionState
{
	private string type = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["type"]))
		{
			this.type = base.Request["type"].ToString();
		}
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
			e.Row.Cells[10].Text = array[0];
			e.Row.Cells[10].Style.Add("text-align", "right");
			e.Row.Cells[10].CssClass = "decimal_input";
			e.Row.Cells[11].Text = array[1];
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[2];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
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
			dt.Columns["EquipmentCode"].ColumnName = "平板船编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "平板船名称";
		}
		if (dt.Columns["StartDate"] != null)
		{
			dt.Columns["StartDate"].ColumnName = "作业开始日期";
		}
		if (dt.Columns["EndDate"] != null)
		{
			dt.Columns["EndDate"].ColumnName = "作业完成日期";
		}
		if (dt.Columns["ShipCount"] != null)
		{
			dt.Columns["ShipCount"].ColumnName = "作业船数";
		}
		if (dt.Columns["Quantity"] != null)
		{
			dt.Columns["Quantity"].ColumnName = "每船产量";
		}
		if (dt.Columns["DeductQty"] != null)
		{
			dt.Columns["DeductQty"].ColumnName = "扣船方数";
		}
		if (dt.Columns["CompleteQty"] != null)
		{
			dt.Columns["CompleteQty"].ColumnName = "完成方数";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		dt.AcceptChanges();
		return dt;
	}
}


