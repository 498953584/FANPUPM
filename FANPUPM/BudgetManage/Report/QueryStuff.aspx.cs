using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Report_QueryStuff : NBasePage, IRequiresSessionState
{
	private PT_PrjInfo prjInfo = new PT_PrjInfo();
	private DataTable dtStuff = new DataTable();
	private string prjId = string.Empty;
	private DataTable dt = new DataTable();
	private string isWBSRelevance = ConfigHelper.Get("IsWBSRelevance");

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		PTPrjInfoBll pTPrjInfoBll = new PTPrjInfoBll();
		new PrjInfoModel();
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			pTPrjInfoBll.GetModelByPrjGuid(this.prjId);
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldIsWBSRelevance.Value = this.isWBSRelevance;
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.ComputeTotal();
			this.BindGv();
		}
	}
	protected void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable stuffInfo = ConstructReport.GetStuffInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtUnitName.Text.Trim(), this.prjId, 0, 0, this.hfldIsWBSRelevance.Value);
		string[] array = new string[6];
		if (stuffInfo.Rows.Count != 0)
		{
			array[0] = System.Convert.ToDecimal(stuffInfo.Compute("sum(BudQuantity)", "1>0")).ToString("0.000");
			array[1] = System.Convert.ToDecimal(stuffInfo.Compute("sum(ConsQuantity)", "1>0")).ToString("0.000");
			array[2] = System.Convert.ToDecimal(stuffInfo.Compute("sum(RealityNumber)", "1>0")).ToString("0.000");
			array[3] = System.Convert.ToDecimal(stuffInfo.Compute("sum(BudTotal)", "1>0")).ToString("0.000");
			array[4] = System.Convert.ToDecimal(stuffInfo.Compute("sum(ConsTotal)", "1>0")).ToString("0.000");
			array[5] = System.Convert.ToDecimal(stuffInfo.Compute("sum(RealityTotal)", "1>0")).ToString("0.000");
		}
		else
		{
			array[0] = "0.000";
			array[1] = "0.000";
			array[2] = "0.000";
			array[3] = "0.000";
			array[4] = "0.000";
			array[5] = "0.000";
		}
		this.ViewState["Total"] = array;
	}
	protected void BindGv()
	{
		this.AspNetPager1.RecordCount = ConstructReport.GetStuffCount(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtUnitName.Text.Trim(), this.prjId, this.hfldIsWBSRelevance.Value);
		this.dtStuff = ConstructReport.GetStuffInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtUnitName.Text.Trim(), this.prjId, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, this.hfldIsWBSRelevance.Value);
		this.gvStuff.DataSource = this.dtStuff;
		this.gvStuff.DataBind();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvStuff_RowDataBound(object sender, GridViewRowEventArgs e)
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
			cells[1].Text = "材料编号";
			cells.Add(new TableHeaderCell());
			cells[2].RowSpan = 2;
			cells[2].Text = "材料名称";
			cells.Add(new TableHeaderCell());
			cells[3].RowSpan = 2;
			cells[3].Text = "规格";
			cells.Add(new TableHeaderCell());
			cells[4].RowSpan = 2;
			cells[4].Text = "品牌";
			cells.Add(new TableHeaderCell());
			cells[5].RowSpan = 2;
			cells[5].Text = "单位";
			cells.Add(new TableHeaderCell());
			cells[6].ColumnSpan = 3;
			cells[6].Text = "材料数量";
			cells.Add(new TableHeaderCell());
			cells[7].ColumnSpan = 3;
			cells[7].Text = "材料价格";
			cells.Add(new TableHeaderCell());
			cells[8].ColumnSpan = 3;
			cells[8].Text = "材料金额</th></tr><tr class='header'>";
			cells.Add(new TableHeaderCell());
			cells[9].Text = "目标数量";
			cells.Add(new TableHeaderCell());
			cells[10].Text = "报量数量";
			cells.Add(new TableHeaderCell());
			cells[11].Text = "实际数量";
			cells[11].Attributes.Add("title", "  实际数量 = 出库数量 &ndash; 退库数量 ");
			cells[11].CssClass = "tooltip";
			cells.Add(new TableHeaderCell());
			cells[12].Text = "目标价格";
			cells.Add(new TableHeaderCell());
			cells[13].Text = "报量价格";
			cells.Add(new TableHeaderCell());
			cells[14].Text = "实际价格";
			cells.Add(new TableHeaderCell());
			cells[15].Text = "目标金额";
			cells.Add(new TableHeaderCell());
			cells[16].Text = "报量金额";
			cells.Add(new TableHeaderCell());
			cells[17].Text = "实际金额";
			cells[17].Attributes.Add("title", " 实际数量 = 出库中成本归集于材料的资源金额 &ndash; 退库中成本归集于材料的资源金额 ");
			cells[17].CssClass = "tooltip";
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Text = "合计";
			string[] array = (string[])this.ViewState["Total"];
			e.Row.Cells[6].Text = array[0];
			e.Row.Cells[6].Style.Add("text-align", "right");
			e.Row.Cells[6].CssClass = "decimal_input";
			e.Row.Cells[7].Text = array[1];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].CssClass = "decimal_input";
			e.Row.Cells[8].Text = array[2];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].CssClass = "decimal_input";
			e.Row.Cells[12].Text = array[3];
			e.Row.Cells[12].Style.Add("text-align", "right");
			e.Row.Cells[12].CssClass = "decimal_input";
			e.Row.Cells[13].Text = array[4];
			e.Row.Cells[13].Style.Add("text-align", "right");
			e.Row.Cells[13].CssClass = "decimal_input";
			e.Row.Cells[14].Text = array[5];
			e.Row.Cells[14].Style.Add("text-align", "right");
			e.Row.Cells[14].CssClass = "decimal_input";
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ComputeTotal();
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable dataTable = new DataTable();
		dataTable = ConstructReport.GetStuffInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.txtSpecification.Text.Trim(), this.txtBrand.Text.Trim(), this.txtUnitName.Text.Trim(), this.prjId, 0, 0, this.hfldIsWBSRelevance.Value);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["Num"] = "合计";
			dataRow["BudQuantity"] = dataTable.Compute("sum(BudQuantity)", "1>0");
			dataRow["ConsQuantity"] = dataTable.Compute("sum(ConsQuantity)", "1>0");
			dataRow["RealityNumber"] = dataTable.Compute("sum(RealityNumber)", "1>0");
			dataRow["BudTotal"] = dataTable.Compute("sum(BudTotal)", "1>0");
			dataRow["ConsTotal"] = dataTable.Compute("sum(ConsTotal)", "1>0");
			dataRow["RealityTotal"] = dataTable.Compute("sum(RealityTotal)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		list.Add(ExcelHeader.Create("材料数量", 1, 3, 6, 0));
		list.Add(ExcelHeader.Create("材料价格", 1, 3, 9, 0));
		list.Add(ExcelHeader.Create("材料金额", 1, 3, 12, 0));
		System.Collections.Generic.List<int> list2 = new System.Collections.Generic.List<int>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			if (dataColumn.Ordinal >= 6)
			{
				list2.Add(dataColumn.Ordinal);
			}
			if (dataColumn.Ordinal < 6)
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 2));
			}
			else
			{
				list.Add(ExcelHeader.Create(dataColumn.ColumnName, 2, 0, 0, 0));
			}
		}
		ExcelHelper.ExportExcel(dataTable, "材料明细", "材料明细", "材料明细.xls", list, null, 3, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["ResourceId"] != null)
		{
			dt.Columns.Remove(dt.Columns["ResourceId"]);
		}
		if (dt.Columns["ResourceCode"] != null)
		{
			dt.Columns["ResourceCode"].ColumnName = "材料编号";
		}
		if (dt.Columns["ResourceName"] != null)
		{
			dt.Columns["ResourceName"].ColumnName = "材料名称";
		}
		if (dt.Columns["Specification"] != null)
		{
			dt.Columns["Specification"].ColumnName = "规格";
		}
		if (dt.Columns["Brand"] != null)
		{
			dt.Columns["Brand"].ColumnName = "品牌";
		}
		if (dt.Columns["Unitname"] != null)
		{
			dt.Columns["Unitname"].ColumnName = "单位";
		}
		if (dt.Columns["BudQuantity"] != null)
		{
			dt.Columns["BudQuantity"].ColumnName = "目标数量";
		}
		if (dt.Columns["ConsQuantity"] != null)
		{
			dt.Columns["ConsQuantity"].ColumnName = "报量数量";
		}
		if (dt.Columns["RealityNumber"] != null)
		{
			dt.Columns["RealityNumber"].ColumnName = "实际数量";
		}
		if (dt.Columns["BudPrice"] != null)
		{
			dt.Columns["BudPrice"].ColumnName = "目标价格";
		}
		if (dt.Columns["ConsPrice"] != null)
		{
			dt.Columns["ConsPrice"].ColumnName = "报量价格";
		}
		if (dt.Columns["RealityPrice"] != null)
		{
			dt.Columns["RealityPrice"].ColumnName = "实际价格";
		}
		if (dt.Columns["BudTotal"] != null)
		{
			dt.Columns["BudTotal"].ColumnName = "目标金额";
		}
		if (dt.Columns["ConsTotal"] != null)
		{
			dt.Columns["ConsTotal"].ColumnName = "报量金额";
		}
		if (dt.Columns["RealityTotal"] != null)
		{
			dt.Columns["RealityTotal"].ColumnName = "实际金额";
		}
		dt.AcceptChanges();
		return dt;
	}
}


