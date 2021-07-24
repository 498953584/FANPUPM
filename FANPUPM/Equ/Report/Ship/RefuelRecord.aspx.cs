using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_Report_Ship_RefuelRecord : NBasePage, IRequiresSessionState
{
	private EquShipRefuelApplyService applySer = new EquShipRefuelApplyService();
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
	private void BindGvw()
	{
		this.AspNetPager1.RecordCount = this.applySer.GetRefuelrecordCount(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.txtShipCode.Text.Trim());
		this.gvwRefuelRecord.DataSource = this.applySer.GetRefuelRecord(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.txtShipCode.Text.Trim(), NBasePage.pagesize, this.AspNetPager1.CurrentPageIndex);
		this.gvwRefuelRecord.DataBind();
	}
	private void ComputeTotal()
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		DataTable refuelRecord = this.applySer.GetRefuelRecord(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.txtShipCode.Text.Trim(), 0, 0);
		if (refuelRecord.Rows.Count != 0)
		{
			this.total[0] = System.Convert.ToDecimal(refuelRecord.Compute("sum(StockQuantity)", "1>0")).ToString("0.00");
			this.total[1] = System.Convert.ToDecimal(refuelRecord.Compute("sum(ApplyQuantity)", "1>0")).ToString("0.00");
			this.total[2] = System.Convert.ToDecimal(refuelRecord.Compute("sum(AfterQuantity)", "1>0")).ToString("0.00");
			return;
		}
		this.total[0] = "0.00";
		this.total[1] = "0.00";
		this.total[2] = "0.00";
	}
	protected void gvwRefuelRecord_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = e.Row.RowIndex.ToString();
		}
		if (e.Row.RowType == DataControlRowType.Footer)
		{
			e.Row.Cells[1].Style.Add("text-align", "right");
			e.Row.Cells[1].Text = "合计";
			e.Row.Cells[4].Style.Add("text-align", "right");
			e.Row.Cells[4].Text = this.total[0];
			e.Row.Cells[7].Style.Add("text-align", "right");
			e.Row.Cells[7].Text = this.total[1];
			e.Row.Cells[8].Style.Add("text-align", "right");
			e.Row.Cells[8].Text = this.total[2];
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
		DataTable dataTable = this.applySer.GetRefuelRecord(this.txtStartDate.Text.Trim(), this.txtEndDate.Text.Trim(), this.txtShipCode.Text.Trim(), 0, 0);
		if (dataTable.Rows.Count > 0)
		{
			DataRow dataRow = dataTable.NewRow();
			dataRow["EquipmentCode"] = "合计";
			dataRow["StockQuantity"] = dataTable.Compute("sum(StockQuantity)", "1>0");
			dataRow["ApplyQuantity"] = dataTable.Compute("sum(ApplyQuantity)", "1>0");
			dataRow["AfterQuantity"] = dataTable.Compute("sum(AfterQuantity)", "1>0");
			dataTable.Rows.Add(dataRow);
		}
		dataTable = this.GetTitleByTable(dataTable);
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		foreach (DataColumn dataColumn in dataTable.Columns)
		{
			list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 0));
		}
		ExcelHelper.ExportExcel(dataTable, "单船加油量记录表", "单船加油量记录表", "单船加油量记录表.xls", list, null, 0, base.Request.Browser.Browser);
	}
	public DataTable GetTitleByTable(DataTable dt)
	{
		if (dt.Columns["Num"] != null)
		{
			dt.Columns["Num"].ColumnName = "序号";
		}
		if (dt.Columns["EquipmentCode"] != null)
		{
			dt.Columns["EquipmentCode"].ColumnName = "船机编号";
		}
		if (dt.Columns["ApplyRefuelDate"] != null)
		{
			dt.Columns["ApplyRefuelDate"].ColumnName = "日期";
		}
		if (dt.Columns["LastDate"] != null)
		{
			dt.Columns["LastDate"].ColumnName = "上次加油时间";
		}
		if (dt.Columns["StockQuantity"] != null)
		{
			dt.Columns["StockQuantity"].ColumnName = "加油前存量（T）";
		}
		if (dt.Columns["PrjName"] != null)
		{
			dt.Columns["PrjName"].ColumnName = "项目名称";
		}
		if (dt.Columns["TaskName"] != null)
		{
			dt.Columns["TaskName"].ColumnName = "分项工程";
		}
		if (dt.Columns["ApplyQuantity"] != null)
		{
			dt.Columns["ApplyQuantity"].ColumnName = "加油量（T）";
		}
		if (dt.Columns["AfterQuantity"] != null)
		{
			dt.Columns["AfterQuantity"].ColumnName = "加油后存量（T）";
		}
		if (dt.Columns["OilsType"] != null)
		{
			dt.Columns["OilsType"].ColumnName = "油品种类";
		}
		if (dt.Columns["Fueler"] != null)
		{
			dt.Columns["Fueler"].ColumnName = "供油船名";
		}
		if (dt.Columns["ApplyRefuelPlace"] != null)
		{
			dt.Columns["ApplyRefuelPlace"].ColumnName = "加油地点";
		}
		if (dt.Columns["FuelerOwner"] != null)
		{
			dt.Columns["FuelerOwner"].ColumnName = "船东";
		}
		if (dt.Columns["LeaderName"] != null)
		{
			dt.Columns["LeaderName"].ColumnName = "现场负责人";
		}
		if (dt.Columns["Note"] != null)
		{
			dt.Columns["Note"].ColumnName = "备注";
		}
		dt.AcceptChanges();
		return dt;
	}
}


