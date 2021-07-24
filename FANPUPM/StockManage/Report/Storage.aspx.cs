using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Report_Storage : NBasePage, IRequiresSessionState
{
	private StorageStock storageStock = new StorageStock();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.gvwStorage.PageSize = NBasePage.pagesize;
		if (!base.IsPostBack)
		{
			this.DataBindStorage(this.storageStock.GetReportDataSource(string.Empty));
		}
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable exportDataSource = this.GetExportDataSource();
		Common2 common = new Common2();
		common.ExportToExecelAll(exportDataSource, "入库信息报表.xls", base.Request.Browser.Browser);
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.DataBindStorage(this.GetStorageDataSource());
	}
	protected void gvwPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvwStorage.PageIndex = e.NewPageIndex;
		this.DataBindStorage(this.GetStorageDataSource());
	}
	private void DataBindStorage(DataTable table)
	{
		if (table.Rows.Count == 0)
		{
			table.Rows.Add(table.NewRow());
			this.gvwStorage.DataSource = table;
			this.gvwStorage.DataBind();
			this.gvwStorage.Rows[0].Visible = false;
			return;
		}
		this.gvwStorage.DataSource = table;
		this.gvwStorage.DataBind();
		string total = System.Convert.ToString(table.Compute("sum(total)", string.Empty));
		GridViewUtility.AddTotalRow(this.gvwStorage, total, 11);
	}
	private DataTable GetStorageDataSource()
	{
		System.DateTime? startDate = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			startDate = new System.DateTime?(System.Convert.ToDateTime(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? endDate = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			endDate = new System.DateTime?(System.Convert.ToDateTime(this.txtEndTime.Text.Trim()).AddDays(1.0));
		}
		return this.storageStock.GetReportDataSource(startDate, endDate, this.txtResourceName.Text.Trim(), this.txtResourceCode.Text.Trim(), this.txtPurchaseCode.Text.Trim(), this.txtCorpName.Text.Trim(), this.txtProjectName.Text.Trim(), this.txtTrea.Text.Trim(), this.dropIsFirst.SelectedValue, this.txtCategory.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
	}
	private DataTable GetExportDataSource()
	{
		DataTable storageDataSource = this.GetStorageDataSource();
		storageDataSource.Columns.Remove("sid");
		storageDataSource.Columns.Remove("ssid");
		storageDataSource.Columns.Remove("corp");
		storageDataSource.Columns.Remove("intype");
		storageDataSource.Columns.Remove("linkcode");
		storageDataSource.Columns.Remove("project");
		storageDataSource.Columns["RowNumber"].ColumnName = "序号";
		storageDataSource.Columns["ResourceCode"].ColumnName = "资源编号";
		storageDataSource.Columns["ResourceName"].ColumnName = "资源名称";
		storageDataSource.Columns["ResourceTypeName"].ColumnName = "资源类别";
		storageDataSource.Columns["资源类别"].SetOrdinal(3);
		storageDataSource.Columns["Specification"].ColumnName = "规格";
		storageDataSource.Columns["规格"].SetOrdinal(4);
		storageDataSource.Columns["Brand"].ColumnName = "品牌";
		storageDataSource.Columns["ModelNumber"].ColumnName = "型号";
		storageDataSource.Columns["TechnicalParameter"].ColumnName = "技术参数";
		storageDataSource.Columns["UnitName"].ColumnName = "单位";
		storageDataSource.Columns["number"].ColumnName = "数量";
		storageDataSource.Columns["sprice"].ColumnName = "单价";
		storageDataSource.Columns["total"].ColumnName = "小计";
		storageDataSource.Columns["CorpName"].ColumnName = "供应商";
		storageDataSource.Columns["intime"].ColumnName = "入库日期";
		storageDataSource.Columns["PrjName"].ColumnName = "项目名称";
		storageDataSource.Columns["tname"].ColumnName = "仓库名称";
		storageDataSource.Columns["StorageCode"].ColumnName = "单据号";
		storageDataSource.Columns["品牌"].SetOrdinal(5);
		storageDataSource.Columns["型号"].SetOrdinal(6);
		storageDataSource.Columns["技术参数"].SetOrdinal(7);
		storageDataSource.Columns["单价"].SetOrdinal(9);
		DataRow dataRow = storageDataSource.NewRow();
		dataRow["资源编号"] = "合计";
		string value = System.Convert.ToString(storageDataSource.Compute("sum(小计)", string.Empty));
		dataRow["小计"] = (string.IsNullOrEmpty(value) ? 0.0 : System.Convert.ToDouble(value));
		storageDataSource.Rows.Add(dataRow);
		return storageDataSource;
	}
}


