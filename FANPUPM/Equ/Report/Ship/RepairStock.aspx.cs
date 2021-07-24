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
public partial class Equ_Report_Ship_RepairStock : NBasePage, IRequiresSessionState
{
	private EquRepairStockService stockSer = new EquRepairStockService();
	private string reportId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["reportId"]))
		{
			this.reportId = base.Request["reportId"].ToString();
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
		DataTable repairStock = this.stockSer.GetRepairStock(this.reportId, this.txtResCode.Text.Trim(), this.txtResName.Text.Trim(), 0, 0);
		string value = string.Empty;
		if (repairStock.Rows.Count != 0)
		{
			value = System.Convert.ToDecimal(repairStock.Compute("sum(Total)", "1>0")).ToString("0.000");
		}
		else
		{
			value = "0.000";
		}
		this.ViewState["Total"] = value;
	}
	public void BindGv()
	{
		string resCode = this.txtResCode.Text.Trim();
		string resName = this.txtResName.Text.Trim();
		this.AspNetPager1.RecordCount = this.stockSer.GetRepairStocksCount(this.reportId, resCode, resName);
		this.gvRepair.DataSource = this.stockSer.GetRepairStock(this.reportId, resCode, resName, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvRepair.DataBind();
	}
	protected void gvRepair_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			string text = (string)this.ViewState["Total"];
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[11].Text = text;
			e.Row.Cells[11].Style.Add("text-align", "right");
			e.Row.Cells[11].CssClass = "decimal_input";
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
		dataTable = this.stockSer.GetRepairStock(this.reportId, this.txtResCode.Text.Trim(), this.txtResName.Text.Trim(), 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["ResourceCode"] = "合计";
			dataRow["Total"] = dataTable.Compute("sum(Total)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		ExcelHelper.ExportExcel(dataTable, "维修保养配件表", "维修保养配件表", "维修保养配件表.xls", null, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["Id"] != null)
		{
			dt.Columns.Remove(dt.Columns["Id"]);
		}
		if (dt.Columns["ReportId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ReportId"]);
		}
		if (dt.Columns["ResourceId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ResourceId"]);
		}
		if (dt.Columns["ReceivePerson"] != null)
		{
			dt.Columns.Remove(dt.Columns["ReceivePerson"]);
		}
		if (dt.Columns["CorpId"] != null)
		{
			dt.Columns.Remove(dt.Columns["CorpId"]);
		}
		if (dt.Columns["ReceiveDate"] != null)
		{
			dt.Columns["ReceiveDate"].ColumnName = "领用日期";
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "配件编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "配件名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["Brand"] != null)
		{
			dt.Columns["Brand"].ColumnName = "品牌";
		}
		if (dt.Columns["ModelNumber"] != null)
		{
			dt.Columns["ModelNumber"].ColumnName = "型号";
		}
		if (dt.Columns["TechnicalParameter"] != null)
		{
			dt.Columns["TechnicalParameter"].ColumnName = "技术参数";
		}
		if (dt.Columns["Name"] != null)
		{
			dt.Columns["Name"].ColumnName = "单位";
		}
		if (dt.Columns["Quantity"] != null)
		{
			dt.Columns["Quantity"].ColumnName = "数量";
		}
		if (dt.Columns["UnitPrice"] != null)
		{
			dt.Columns["UnitPrice"].ColumnName = "单价";
		}
		if (dt.Columns["Total"] != null)
		{
			dt.Columns["Total"].ColumnName = "合价";
		}
		if (dt.Columns["Total"] != null)
		{
			dt.Columns["Total"].ColumnName = "合价";
		}
		if (dt.Columns["ReceivePersonName"] != null)
		{
			dt.Columns["ReceivePersonName"].ColumnName = "领用人";
		}
		if (dt.Columns["CorpName"] != null)
		{
			dt.Columns["CorpName"].ColumnName = "供应商";
		}
		dt.AcceptChanges();
		return dt;
	}
}


