using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class PrjManager_TenderInfoQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			int[] prjTypeCodes = new int[]
			{
				5,
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
			this.bindGv();
		}
	}
	public void bindGv()
	{
		int recordCount = 0;
		DataTable tenderPrjReport = ProjectInfo.GetTenderPrjReport(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, "1", ref recordCount);
		this.AspNetPager1.RecordCount = recordCount;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.gvDataInfo.DataSource = tenderPrjReport;
		this.gvDataInfo.DataBind();
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType != DataControlRowType.DataRow)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				int num = 0;
				e.Row.Cells[0].Text = "合计";
				DataTable tenderPrjReport = ProjectInfo.GetTenderPrjReport(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, 1, this.AspNetPager1.PageSize * this.AspNetPager1.RecordCount, "1", ref num);
				e.Row.Cells[6].Text = tenderPrjReport.Compute("SUM(PrjCost)", string.Empty).ToString();
				e.Row.Cells[6].Style.Add("text-align", "right");
			}
			return;
		}
		e.Row.Attributes["id"] = this.gvDataInfo.DataKeys[e.Row.RowIndex].Value.ToString();
		string text = this.gvDataInfo.DataKeys[e.Row.RowIndex].Values[1].ToString();
		if (text.Length == 5)
		{
			e.Row.Attributes["isMainContract"] = "True";
			return;
		}
		e.Row.Attributes["isMainContract"] = "False";
	}
	protected void btnExport_Click(object sender, System.EventArgs e)
	{
		int num = 0;
		DataTable tenderPrjReport = ProjectInfo.GetTenderPrjReport(this.txtPrjCode.Text.Trim(), this.txtPrjName.Text.Trim(), this.txtName.Text.Trim(), this.dropPrjKindClass.SelectedValue, this.txtStartTime.Text.Trim(), this.txtEndTime.Text.Trim(), this.txtOwner.Text.Trim(), this.dropPrjState.SelectedValue, base.UserCode, 1, 2147483647, "1", ref num);
		string[] headerName = new string[]
		{
			"序号",
			"项目状态",
			"项目编号",
			"项目名称",
			"项目经理",
			"建设单位",
			"工程造价",
			"立项申请日期",
			"开始日期",
			"结束日期"
		};
		string[] fieldName = new string[]
		{
			"",
			"PrjStateName",
			"PrjCode",
			"PrjName",
			"PrjMangerName",
			"Owner",
			"PrjCost",
			"InputDate",
			"StartDate",
			"EndDate"
		};
		string[] totalField = new string[]
		{
			"PrjCost"
		};
		ExcelHelper.ExportExcel(tenderPrjReport, headerName, fieldName, totalField, "招投标项目一览.xls", base.Request.Browser.Browser);
	}
}


