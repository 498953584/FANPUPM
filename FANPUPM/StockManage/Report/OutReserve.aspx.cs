using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_Report_OutReserve : NBasePage, IRequiresSessionState
{
	private OutStockBll outStockBll = new OutStockBll();
	private Common2 cm = new Common2();

	protected override void OnInit(System.EventArgs e)
	{
		this.gvPurchaseplan.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindGv();
		}
	}
	public DataTable GetTable()
	{
		return this.outStockBll.GetTableByParam(this.txtStartTime.Text, this.txtEndTime.Text, this.txtCode.Text.Trim(), this.txtResourceNames.Text.Split(new char[]
		{
			','
		}), this.txtResourceCodes.Text.Split(new char[]
		{
			','
		}), this.txtProjectName.Value.Trim(), this.txtCorp.Text.Trim(), this.txtCategory.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtModelNumber.Text.Trim());
	}
	public void BindGv()
	{
		this.BindGv(this.GetTable());
	}
	public void BindGv(DataTable storageDataSource)
	{
		if (storageDataSource.Rows.Count == 0)
		{
			storageDataSource.Rows.Add(storageDataSource.NewRow());
			this.gvPurchaseplan.DataSource = storageDataSource;
			this.gvPurchaseplan.DataBind();
			this.gvPurchaseplan.HeaderRow.Cells[0].Visible = false;
			this.gvPurchaseplan.Rows[0].Visible = false;
			return;
		}
		this.gvPurchaseplan.DataSource = storageDataSource;
		this.gvPurchaseplan.DataBind();
		GridViewUtility.AddTotalRow(this.gvPurchaseplan, System.Convert.ToDecimal(storageDataSource.Compute("sum(xjsprice)", "")).ToString("0.000"), 13);
	}
	protected void gvPurchaseplan_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvPurchaseplan.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable table = this.GetTable();
		Common2 common = new Common2();
		common.ExportToExecelAll(this.GetTitleByTable(table), "出库报表.xls", base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dtSource)
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("仓库名称");
		dataTable.Columns.Add("出库编号");
		dataTable.Columns.Add("资源编号");
		dataTable.Columns.Add("资源名称");
		dataTable.Columns.Add("资源类别");
		dataTable.Columns.Add("规格");
		dataTable.Columns.Add("品牌");
		dataTable.Columns.Add("型号");
		dataTable.Columns.Add("技术参数");
		dataTable.Columns.Add("单位");
		dataTable.Columns.Add("单价");
		dataTable.Columns.Add("数量");
		dataTable.Columns.Add("小计");
		dataTable.Columns.Add("供应商");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("申请日期");
		if (dtSource.Rows.Count != 0)
		{
			int num = 0;
			foreach (DataRow dataRow in dtSource.Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["仓库名称"] = dataRow["tname"];
				dataRow2["出库编号"] = dataRow["orcode"];
				dataRow2["资源编号"] = dataRow["ResourceCode"];
				dataRow2["资源名称"] = dataRow["ResourceName"];
				dataRow2["资源类别"] = dataRow["ResourceTypeName"];
				dataRow2["规格"] = dataRow["Specification"];
				dataRow2["品牌"] = dataRow["Brand"];
				dataRow2["型号"] = dataRow["ModelNumber"];
				dataRow2["技术参数"] = dataRow["TechnicalParameter"];
				dataRow2["单位"] = dataRow["Name"];
				dataRow2["单价"] = dataRow["sprice"];
				dataRow2["数量"] = dataRow["number"];
				dataRow2["小计"] = dataRow["xjsprice"];
				dataRow2["供应商"] = dataRow["corpName"];
				dataRow2["项目名称"] = dataRow["prjName"];
				dataRow2["申请日期"] = Common2.GetTime(dataRow["intime"]);
				dataTable.Rows.Add(dataRow2);
			}
			DataRow dataRow3 = dataTable.NewRow();
			dataRow3["序号"] = "合计";
			dataRow3["仓库名称"] = "";
			dataRow3["出库编号"] = "";
			dataRow3["资源编号"] = "";
			dataRow3["资源名称"] = "";
			dataRow3["资源类别"] = "";
			dataRow3["规格"] = "";
			dataRow3["品牌"] = "";
			dataRow3["型号"] = "";
			dataRow3["技术参数"] = "";
			dataRow3["单位"] = "";
			dataRow3["单价"] = "";
			dataRow3["数量"] = "";
			dataRow3["小计"] = dtSource.Compute("sum(xjsprice)", string.Empty).ToString();
			dataRow3["供应商"] = "";
			dataRow3["项目名称"] = "";
			dataRow3["申请日期"] = "";
			dataTable.Rows.Add(dataRow3);
		}
		return dataTable;
	}
}


