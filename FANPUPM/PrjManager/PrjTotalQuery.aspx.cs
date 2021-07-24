using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_PrjTotalQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			int[] prjTypeCodes = new int[]
			{
				5,
				17,
				7,
				8,
				9,
				10,
				11,
				12,
				13
			};
			TypeList.BindDrop(this.dropPrjState, prjTypeCodes, "", null);
			TypeList.BindDrop(this.dropPrjKindClass, "ProjectType", "", null, true);
			this.BindGv();
		}
	}
	public void BindGv()
	{
		int recordCount = 0;
		DataTable tenderPrjReport = ProjectInfo.GetTenderPrjReport(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, this.dropPrjSource.SelectedValue, ref recordCount);
		this.AspNetPager1.RecordCount = recordCount;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.gvPrjInfo.DataSource = tenderPrjReport;
		this.gvPrjInfo.DataBind();
	}
	protected void gvPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType != DataControlRowType.DataRow)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				int num = 0;
				e.Row.Cells[0].Text = "合计";
				DataTable tenderPrjReport = ProjectInfo.GetTenderPrjReport(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, 1, this.AspNetPager1.PageSize * this.AspNetPager1.RecordCount, this.dropPrjSource.SelectedValue, ref num);
				e.Row.Cells[6].Text = tenderPrjReport.Compute("SUM(PrjCost)", string.Empty).ToString();
				e.Row.Cells[6].Style.Add("text-align", "right");
			}
			return;
		}
		e.Row.Attributes["id"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Value.ToString();
		e.Row.Attributes["limit"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[2].ToString();
		e.Row.Attributes["child"] = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[3].ToString();
		string text = this.gvPrjInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
		e.Row.Attributes["typeCode"] = text;
		if (text.Length == 5)
		{
			e.Row.Attributes["isMainContract"] = "True";
			return;
		}
		e.Row.Attributes["isMainContract"] = "False";
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 0;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnExcel_Click(object sender, System.EventArgs e)
	{
		DataTable formatDataTable = this.GetFormatDataTable();
		System.Collections.Generic.List<ExcelHeader> list = new System.Collections.Generic.List<ExcelHeader>();
		foreach (DataColumn dataColumn in formatDataTable.Columns)
		{
			list.Add(ExcelHeader.Create(dataColumn.ColumnName, 1, 0, 0, 0));
		}
		ExcelHelper.ExportExcel(formatDataTable, "项目信息", "sheet1", "项目信息.xls", list, null, 3, base.Request.Browser.Browser);
	}
	private DataTable GetFormatDataTable()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("序号");
		dataTable.Columns.Add("项目状态");
		dataTable.Columns.Add("项目编号");
		dataTable.Columns.Add("项目名称");
		dataTable.Columns.Add("项目经理");
		dataTable.Columns.Add("建设单位");
		dataTable.Columns.Add("工程造价");
		dataTable.Columns.Add("项目来源");
		dataTable.Columns.Add("立项申请日期");
		dataTable.Columns.Add("开始日期");
		dataTable.Columns.Add("结束日期");
		int num = 0;
		int num2 = 0;
		DataTable tenderPrjReport = ProjectInfo.GetTenderPrjReport(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, 0, this.AspNetPager1.RecordCount, this.dropPrjSource.SelectedValue, ref num2);
		if (tenderPrjReport != null)
		{
			foreach (DataRow dataRow in tenderPrjReport.Rows)
			{
				num++;
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["序号"] = num.ToString();
				dataRow2["项目状态"] = dataRow["PrjStateName"];
				dataRow2["项目编号"] = dataRow["PrjCode"];
				dataRow2["项目名称"] = dataRow["PrjName"];
				dataRow2["项目经理"] = dataRow["PrjMangerName"];
				dataRow2["建设单位"] = dataRow["Owner"].ToString();
				dataRow2["工程造价"] = decimal.Parse(dataRow["PrjCost"].ToString()).ToString("#,##0.000");
				dataRow2["项目来源"] = (bool.Parse(dataRow["IsTender"].ToString()) ? "投标立项" : "手工立项");
				dataRow2["立项申请日期"] = Common2.GetTime(dataRow["InputDate"]);
				dataRow2["开始日期"] = Common2.GetTime(dataRow["StartDate"]);
				dataRow2["结束日期"] = Common2.GetTime(dataRow["EndDate"]);
				dataTable.Rows.Add(dataRow2);
			}
			if (tenderPrjReport.Rows.Count > 0)
			{
				DataRow dataRow3 = dataTable.NewRow();
				dataRow3["序号"] = "合 计";
				dataRow3["工程造价"] = System.Convert.ToDecimal(tenderPrjReport.Compute("SUM(PrjCost)", "1>0")).ToString("#,##0.000");
				dataTable.Rows.Add(dataRow3);
			}
		}
		return dataTable;
	}
	protected string ViewInfo(string prjGuid, bool isTender)
	{
		if (isTender)
		{
			return string.Format("viewopen('/TenderManage/InfoView.aspx?ic={0}')", prjGuid);
		}
		return string.Format("viewopen('/PrjManager/PrjInfoView.aspx?ic={0}')", prjGuid);
	}
}


