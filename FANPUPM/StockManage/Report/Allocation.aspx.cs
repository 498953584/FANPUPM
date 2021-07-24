using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Report_Allocation : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvwStorage.PageSize = NBasePage.pagesize;
		base.Response.Clear();
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.Bind_gvwStorage(string.Empty);
		}
	}
	protected void gvwStorage_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwStorage.PageIndex = e.NewPageIndex;
		this.Bind_gvwStorage(this.getSertchData());
	}
	protected void gvwStorage_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				AllocationBllAction allocationBllAction = new AllocationBllAction();
				DataRowView dataRowView = (DataRowView)e.Row.DataItem;
				e.Row.Cells[15].Text = allocationBllAction.GetTreasuryNameByCode(dataRowView["tcodea"].ToString()).Rows[0][0].ToString();
				e.Row.Cells[16].Text = allocationBllAction.GetTreasuryNameByCode(dataRowView["tcodeb"].ToString()).Rows[0][0].ToString();
			}
			catch
			{
			}
		}
	}
	protected void btnExcel_Command(object sender, CommandEventArgs e)
	{
		DataTable exportDataSource = this.GetExportDataSource();
		Common2 common = new Common2();
		if (e.CommandName == "excel")
		{
			common.ExportToExecelAll(exportDataSource, "调拨单报表.xls", base.Request.Browser.Browser);
			return;
		}
		common.ExportToWordAll(exportDataSource, "调拨单报表.doc");
	}
	protected DataTable ReportAlocationDataSource(string strWhere)
	{
		return AllocationAction.AllocationReportData(strWhere);
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.Bind_gvwStorage(this.getSertchData());
	}
	protected void Bind_gvwStorage(string strWhere)
	{
		Common2.BindGvTable(this.ReportAlocationDataSource(strWhere), this.gvwStorage, false);
	}
	protected string getSertchData()
	{
		string text = " and 1=1 ";
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			object obj = text;
			text = string.Concat(new object[]
			{
				obj,
				" and sa.intime>='",
				System.Convert.ToDateTime(this.txtStartTime.Text.Trim()),
				"' "
			});
		}
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			object obj2 = text;
			text = string.Concat(new object[]
			{
				obj2,
				" and sa.intime<'",
				System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0),
				"' "
			});
		}
		if (!string.IsNullOrEmpty(this.txtPurchaseCode.Text.Trim()))
		{
			text = text + " and sa.acode like '%" + this.txtPurchaseCode.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtCorp.Text.Trim()))
		{
			text = text + " and Bcorp.CorpName like '%" + this.txtCorp.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtTreaOut.Text.Trim()))
		{
			text = text + " and TCodeA.tname like '%" + this.txtTreaOut.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtTreaIn.Text.Trim()))
		{
			text = text + " and TCodeB.TName like '%" + this.txtTreaIn.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtCategory.Text.Trim()))
		{
			text = text + " and p9.ResourceTypeName like '%" + this.txtCategory.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtSpecification.Text.Trim()))
		{
			text = text + " and Specification like '%" + this.txtSpecification.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtBrand.Text.Trim()))
		{
			text = text + " and ress.Brand like '%" + this.txtBrand.Text.Trim() + "%' ";
		}
		if (!string.IsNullOrEmpty(this.txtModelNumber.Text.Trim()))
		{
			text = text + " and ress.ModelNumber like '%" + this.txtModelNumber.Text.Trim() + "%' ";
		}
		if (this.txtResourceCode.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("sas.scode", this.txtResourceCode.Text.Trim());
		}
		if (this.txtResourceName.Text.Trim() != "")
		{
			text += DBHelper.GetQuerySql("ress.ResourceName", this.txtResourceName.Text.Trim());
		}
		return text;
	}
	public override void VerifyRenderingInServerForm(Control control)
	{
	}
	private DataTable GetExportDataSource()
	{
		DataTable dataTable = this.ReportAlocationDataSource(this.getSertchData());
		dataTable.Columns.Remove("aid");
		dataTable.Columns.Remove("scode");
		dataTable.Columns.Remove("tcodea");
		dataTable.Columns.Remove("tcodeb");
		dataTable.Columns["acode"].ColumnName = "单据号";
		dataTable.Columns["ResourceCode"].ColumnName = "资源编号";
		dataTable.Columns["resourceName"].ColumnName = "资源名称";
		dataTable.Columns["ResourceTypeName"].ColumnName = "资源类别";
		dataTable.Columns["Specification"].ColumnName = "规格";
		dataTable.Columns["Brand"].ColumnName = "品牌";
		dataTable.Columns["ModelNumber"].ColumnName = "型号";
		dataTable.Columns["TechnicalParameter"].ColumnName = "技术参数";
		dataTable.Columns["Name"].ColumnName = "单位";
		dataTable.Columns["sprice"].ColumnName = "单价";
		dataTable.Columns["number"].ColumnName = "数量";
		dataTable.Columns["total"].ColumnName = "总价";
		dataTable.Columns["CorpName"].ColumnName = "供应商";
		dataTable.Columns["intime"].ColumnName = "调拨日期";
		dataTable.Columns["TAName"].ColumnName = "调出库";
		dataTable.Columns["TBName"].ColumnName = "接收库";
		dataTable.Columns.Add("num");
		for (int i = 0; i < dataTable.Rows.Count; i++)
		{
			dataTable.Rows[i]["num"] = i + 1;
		}
		dataTable.Columns["num"].ColumnName = "序号";
		dataTable.Columns["序号"].SetOrdinal(0);
		dataTable.Columns["品牌"].SetOrdinal(6);
		dataTable.Columns["型号"].SetOrdinal(7);
		dataTable.Columns["技术参数"].SetOrdinal(8);
		return dataTable;
	}
}


