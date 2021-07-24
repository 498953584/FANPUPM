using ASP;
using cn.justwin.BLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Land_DrillDetailReport : NBasePage, IRequiresSessionState
{
	private string equId = string.Empty;
	private string date = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["equId"]))
		{
			this.equId = base.Request["equId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["date"]))
		{
			this.date = base.Request["date"].ToString();
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
	}
	public void BindGv()
	{
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
			e.Row.Cells[6].Text = array[0];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = array[1];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
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
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["EquipmentId"] != null)
		{
			dt.Columns.Remove(dt.Columns["EquipmentId"]);
		}
		if (dt.Columns["ConstructionDate"] != null)
		{
			dt.Columns["ConstructionDate"].ColumnName = "施工日期";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "钻孔机编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "钻孔机名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["CorpName"] != null)
		{
			dt.Columns["CorpName"].ColumnName = "制造厂家";
		}
		if (dt.Columns["DrillCount"] != null)
		{
			dt.Columns["DrillCount"].ColumnName = "孔数";
		}
		if (dt.Columns["TotalLength"] != null)
		{
			dt.Columns["TotalLength"].ColumnName = "总长";
		}
		if (dt.Columns["PrjName"] != null)
		{
			dt.Columns["PrjName"].ColumnName = "项目";
		}
		if (dt.Columns["Location"] != null)
		{
			dt.Columns["Location"].ColumnName = "位置";
		}
		if (dt.Columns["Uom"] != null)
		{
			dt.Columns["Uom"].ColumnName = "计量单位";
		}
		if (dt.Columns["ConstructPlace"] != null)
		{
			dt.Columns["ConstructPlace"].ColumnName = "施工地点";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		dt.AcceptChanges();
		return dt;
	}
}


