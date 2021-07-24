using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_TenderStateReport : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			TypeList.BindDrop(this.ddlType, "ProjectType", "", null, true);
			TypeList.BindDrop(this.ddlPrjState, false);
			TypeList.BindDrop(this.dropPrjProperty, "ProjectProperty", "", null, true);
			this.ddlPrjState.Items.Insert(0, new ListItem("", ""));
			this.BindGv();
		}
	}
	protected void BindGv()
	{
		int tenderReportCount = TenderInfo.GetTenderReportCount(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.ddlType.SelectedValue.Trim(), this.txtManager.Value.Trim(), this.txtPrjFollowPeople.Value.Trim(), this.txtPrjPeople.Value.Trim(), this.ddlPrjState.SelectedValue.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text, this.txtOwnerName.Text.Trim(), this.txtProjPeopleDep.Text, this.dropPrjProperty.SelectedValue, this.txtPrincipal.Value.Trim(), base.UserCode, this.txtPrjStateChangeTimeStart.Text.Trim(), this.txtPrjStateChangeTimeEnd.Text.Trim());
		this.AspNetPager1.RecordCount = tenderReportCount;
		DataTable tenderReport = TenderInfo.GetTenderReport(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.ddlType.SelectedValue.Trim(), this.txtManager.Value.Trim(), this.txtPrjFollowPeople.Value.Trim(), this.txtPrjPeople.Value.Trim(), this.ddlPrjState.SelectedValue.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text, this.txtOwnerName.Text.Trim(), this.txtProjPeopleDep.Text, this.dropPrjProperty.SelectedValue, this.txtPrincipal.Value.Trim(), this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize, base.UserCode, this.txtPrjStateChangeTimeStart.Text.Trim(), this.txtPrjStateChangeTimeEnd.Text.Trim());
		this.gvTender.DataSource = tenderReport;
		this.gvTender.DataBind();
		this.ComputSum();
	}
	protected void ComputSum()
	{
		string text = "0";
		string text2 = "0";
		string text3 = "0";
		string text4 = "0";
		string text5 = "0";
		string text6 = "0";
		string text7 = "0";
		string text8 = "0";
		string text9 = "0";
		string text10 = "0";
		DataTable summarizingInfo = TenderInfo.GetSummarizingInfo(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.ddlType.SelectedValue.Trim(), this.txtManager.Value.Trim(), this.txtPrjFollowPeople.Value.Trim(), this.txtPrjPeople.Value.Trim(), this.ddlPrjState.SelectedValue.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text, this.txtOwnerName.Text.Trim(), this.txtProjPeopleDep.Text, this.dropPrjProperty.SelectedValue, this.txtPrincipal.Value.Trim(), base.UserCode, this.txtPrjStateChangeTimeStart.Text.Trim(), this.txtPrjStateChangeTimeEnd.Text.Trim());
		if (summarizingInfo != null && summarizingInfo.Rows.Count > 0)
		{
			text = summarizingInfo.Rows[0]["2"].ToString();
			text2 = summarizingInfo.Rows[0]["3"].ToString();
			text6 = summarizingInfo.Rows[0]["4"].ToString();
			text7 = summarizingInfo.Rows[0]["5"].ToString();
			text8 = summarizingInfo.Rows[0]["6"].ToString();
			text3 = summarizingInfo.Rows[0]["14"].ToString();
			text4 = summarizingInfo.Rows[0]["15"].ToString();
			text5 = summarizingInfo.Rows[0]["16"].ToString();
			text10 = summarizingInfo.Rows[0]["18"].ToString();
			text9 = summarizingInfo.Rows[0]["19"].ToString();
		}
		text = ((text == "") ? "0" : text);
		text2 = ((text2 == "") ? "0" : text2);
		text6 = ((text6 == "") ? "0" : text6);
		text7 = ((text7 == "") ? "0" : text7);
		text8 = ((text8 == "") ? "0" : text8);
		text3 = ((text3 == "") ? "0" : text3);
		text4 = ((text4 == "") ? "0" : text4);
		text5 = ((text5 == "") ? "0" : text5);
		text10 = ((text10 == "") ? "0" : text10);
		text9 = ((text9 == "") ? "0" : text9);
		string text11 = "<span style='margin-left:3px;margin-right:3px;'>";
		string text12 = "</span>";
		this.lblTotal.Text = string.Concat(new string[]
		{
			"汇总：信息立项",
			text11,
			text,
			text12,
			"项，报名通过",
			text11,
			text2,
			text12,
			"项,报名不通过",
			text11,
			text9,
			text12,
			"项，资格预审",
			text11,
			text3,
			text12,
			"项,预审通过",
			text11,
			text4,
			text12,
			"项,预审失败",
			text11,
			text5,
			text12,
			"项,投标",
			text11,
			text6,
			text12,
			"项,中标",
			text11,
			text7,
			text12,
			"项,落标",
			text11,
			text8,
			text12,
			"项,放弃",
			text11,
			text10,
			text12,
			"项"
		});
	}
	protected void gvTender_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType != DataControlRowType.DataRow)
		{
			if (e.Row.RowType == DataControlRowType.Footer)
			{
				e.Row.Cells[0].Text = "合计";
				DataTable tenderReport = TenderInfo.GetTenderReport(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.ddlType.SelectedValue.Trim(), this.txtManager.Value.Trim(), this.txtPrjFollowPeople.Value.Trim(), this.txtPrjPeople.Value.Trim(), this.ddlPrjState.SelectedValue.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text, this.txtOwnerName.Text.Trim(), this.txtProjPeopleDep.Text, this.dropPrjProperty.SelectedValue, this.txtPrincipal.Value.Trim(), 1, this.AspNetPager1.PageSize * this.AspNetPager1.RecordCount, base.UserCode, this.txtPrjStateChangeTimeStart.Text.Trim(), this.txtPrjStateChangeTimeEnd.Text.Trim());
				e.Row.Cells[8].Text = tenderReport.Compute("SUM(PrjCost)", string.Empty).ToString();
				e.Row.Cells[8].Style.Add("text-align", "right");
				e.Row.Cells[8].CssClass = "decimal_input";
				e.Row.Cells[9].Text = tenderReport.Compute("SUM(ProfessionalCost)", string.Empty).ToString();
				e.Row.Cells[9].Style.Add("text-align", "right");
				e.Row.Cells[9].CssClass = "decimal_input";
				e.Row.Cells[10].Text = tenderReport.Compute("SUM(SuccessBidPrice)", string.Empty).ToString();
				e.Row.Cells[10].Style.Add("text-align", "right");
				e.Row.Cells[10].CssClass = "decimal_input";
			}
			return;
		}
		string text = this.gvTender.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
		e.Row.Attributes["id"] = text;
		PTPrjInfoStateChange byPrjIdByOrder = new PTPrjInfoStateChangeService().GetByPrjIdByOrder(text);
		if (byPrjIdByOrder != null)
		{
			e.Row.Attributes["changed"] = "1";
			return;
		}
		e.Row.Attributes["changed"] = "0";
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	public string GetState(string state)
	{
		string result = "";
		if (state == "1")
		{
			result = "<span style='color:green; font-size:20px;'>●</span>";
		}
		else
		{
			if (state == "2")
			{
				result = "<span style='color:green;font-size:20px; font-weight:bold;'>√</span>";
			}
			else
			{
				if (state == "3")
				{
					result = "<span style='color:green;font-size:20px;font-weight:bold;'>×</span>";
				}
			}
		}
		return result;
	}
	protected void removeColumn(DataTable dtSource, System.Collections.Generic.List<string> columnNames)
	{
		if (columnNames != null && dtSource != null)
		{
			foreach (string current in columnNames)
			{
				if (dtSource.Columns.Contains(current))
				{
					dtSource.Columns.Remove(dtSource.Columns[current]);
				}
			}
		}
	}
	protected void btnexecl_Click(object sender, System.EventArgs e)
	{
		DataTable tenderReport = TenderInfo.GetTenderReport(this.txtCode.Text.Trim(), this.txtName.Text.Trim(), this.ddlType.SelectedValue.Trim(), this.txtManager.Value.Trim(), this.txtPrjFollowPeople.Value.Trim(), this.txtPrjPeople.Value.Trim(), this.ddlPrjState.SelectedValue.Trim(), this.txtStartTime.Text.Trim(), this.txtEndTime.Text, this.txtOwnerName.Text.Trim(), this.txtProjPeopleDep.Text, this.dropPrjProperty.SelectedValue, this.txtPrincipal.Value.Trim(), 1, 2147483647, base.UserCode, this.txtPrjStateChangeTimeStart.Text.Trim(), this.txtPrjStateChangeTimeEnd.Text.Trim());
		if (tenderReport.Rows.Count > 0)
		{
			DataRow dataRow = tenderReport.NewRow();
			dataRow["ItemName"] = "合计";
			dataRow["PrjCost"] = tenderReport.Compute("sum(PrjCost)", "1>0");
			dataRow["ProfessionalCost"] = tenderReport.Compute("sum(ProfessionalCost)", "1>0");
			dataRow["SuccessBidPrice"] = tenderReport.Compute("sum(SuccessBidPrice)", "1>0");
			tenderReport.Rows.Add(dataRow);
		}
		this.FormatTable(tenderReport);
		string value = this.hfldSummarizingInfo.Value;
		ExcelHelper.ExportFooterExcel(tenderReport, "项目市场状态一览", "项目市场状态一览", "项目市场状态一览.xls", null, null, 1, value, base.Request.Browser.Browser);
	}
	protected void FormatTable(DataTable dt)
	{
		dt.Columns["No"].SetOrdinal(0);
		dt.Columns["No"].ColumnName = "序号";
		dt.Columns["ItemName"].SetOrdinal(1);
		dt.Columns["ItemName"].ColumnName = "项目状态";
		dt.Columns["PrjManagerName"].SetOrdinal(2);
		dt.Columns["PrjManagerName"].ColumnName = "项目经理";
		dt.Columns["PrjDutyName"].SetOrdinal(3);
		dt.Columns["PrjDutyName"].ColumnName = "项目跟踪人";
		dt.Columns["WorkUnit"].SetOrdinal(4);
		dt.Columns["WorkUnit"].ColumnName = "主要负责人";
		dt.Columns["PrjCode"].SetOrdinal(5);
		dt.Columns["PrjCode"].ColumnName = "项目编号";
		dt.Columns["PrjName"].SetOrdinal(6);
		dt.Columns["PrjName"].ColumnName = "项目名称";
		dt.Columns["CorpName"].SetOrdinal(7);
		dt.Columns["CorpName"].ColumnName = "建设单位";
		dt.Columns["PrjCost"].SetOrdinal(8);
		dt.Columns["PrjCost"].ColumnName = "工程造价";
		dt.Columns["ProfessionalCost"].SetOrdinal(9);
		dt.Columns["ProfessionalCost"].ColumnName = "专业造价";
		dt.Columns["SuccessBidPrice"].SetOrdinal(10);
		dt.Columns["SuccessBidPrice"].ColumnName = "中标价";
		dt.Columns["PrjSetUp"].SetOrdinal(11);
		dt.Columns["PrjSetUp"].ColumnName = "信息立项";
		dt.Columns["PrjStart"].SetOrdinal(12);
		dt.Columns["PrjStart"].ColumnName = "报名通过";
		dt.Columns["PrjStartNo"].SetOrdinal(13);
		dt.Columns["PrjStartNo"].ColumnName = "报名不通过";
		dt.Columns["PrjYs"].SetOrdinal(14);
		dt.Columns["PrjYs"].ColumnName = "预审";
		dt.Columns["PrjTender"].SetOrdinal(15);
		dt.Columns["PrjTender"].ColumnName = "投标";
		dt.Columns["WinBid"].SetOrdinal(16);
		dt.Columns["WinBid"].ColumnName = "中标";
		dt.Columns["OutBid"].SetOrdinal(17);
		dt.Columns["OutBid"].ColumnName = "落标";
		dt.Columns["PrjGiveUp"].SetOrdinal(18);
		dt.Columns["PrjGiveUp"].ColumnName = "放弃";
		dt.Columns["InputDate"].SetOrdinal(19);
		dt.Columns["InputDate"].ColumnName = "立项申请日期";
		dt.Columns.Remove("PrjGuid");
		dt.Columns.Remove("CodeName");
	}
}


