using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using cn.justwin.PrjManager;
using cn.justwin.Tender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public class ReportInfo
{
    public string ReportTypeId
    {
        get;
        set;
    }
    public string ReportTypeName
    {
        get;
        set;
    }
    public string ReportId
    {
        get;
        set;
    }
    public System.DateTime? ReportDate
    {
        get;
        set;
    }
    public string ConstUnit
    {
        get;
        set;
    }
    public string InputUser
    {
        get;
        set;
    }
    public System.DateTime InputDate
    {
        get;
        set;
    }
    public int FlowState
    {
        get;
        set;
    }
}
public partial class StartStopReturnWork_QueryReportList : NBasePage, IRequiresSessionState
{
	private PTStartReportService startReportServer = new PTStartReportService();
	private PTStopMsgService stopMsgServer = new PTStopMsgService();
	private PTRetMsgService RetMsgServer = new PTRetMsgService();
	private string prjId = string.Empty;
	private string year = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["prjId"]))
		{
			this.prjId = base.Request["prjId"];
		}
		if (!string.IsNullOrEmpty(base.Request["year"]))
		{
			this.year = base.Request["year"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hfldPrjId.Value = this.prjId;
			this.GetProjectInfo();
			this.BindGv();
		}
	}
	private void BindGv()
	{
		System.Collections.Generic.List<ReportInfo> list2 = new System.Collections.Generic.List<ReportInfo>();
		if (string.IsNullOrEmpty(this.dropReportType.SelectedValue))
		{
			list2 = this.GetStartReport(list2);
			list2 = this.GetStopMsg(list2);
			list2 = this.GetRetMsg(list2);
			list2 = (
				from list in list2
				orderby list.InputDate
				select list).ToList<ReportInfo>();
		}
		else
		{
			if (this.dropReportType.SelectedValue == "1")
			{
				list2 = this.GetStartReport(list2);
			}
			else
			{
				if (this.dropReportType.SelectedValue == "2")
				{
					list2 = this.GetStopMsg(list2);
				}
				else
				{
					if (this.dropReportType.SelectedValue == "3")
					{
						list2 = this.GetRetMsg(list2);
					}
				}
			}
		}
		this.gvReportList.DataSource = list2;
		this.gvReportList.DataBind();
	}
	private System.Collections.Generic.List<ReportInfo> GetStartReport(System.Collections.Generic.List<ReportInfo> reportList)
	{
		PTStartReport byPrjGuid = this.startReportServer.GetByPrjGuid(this.hfldPrjId.Value);
		if (byPrjGuid != null)
		{
			ReportInfo reportInfo = new ReportInfo();
			reportInfo.ReportTypeId = "1";
			reportInfo.ReportTypeName = "开工报告";
			reportInfo.ReportId = byPrjGuid.ReportId;
			if (byPrjGuid.RealityStartDate.HasValue)
			{
				reportInfo.ReportDate = new System.DateTime?(byPrjGuid.RealityStartDate.Value);
			}
			else
			{
				reportInfo.ReportDate = null;
			}
			reportInfo.ConstUnit = byPrjGuid.ConstructionUnit;
			reportInfo.InputDate = byPrjGuid.InputDate;
			reportInfo.InputUser = byPrjGuid.InputUser;
			reportInfo.FlowState = byPrjGuid.FlowState.Value;
			reportList.Add(reportInfo);
		}
		return reportList;
	}
	private System.Collections.Generic.List<ReportInfo> GetStopMsg(System.Collections.Generic.List<ReportInfo> reportList)
	{
		System.Collections.Generic.List<PTStopMsg> list = (
			from stop in this.stopMsgServer
			where stop.PrjGuid == this.hfldPrjId.Value
			select stop into list0
			orderby list0.InputDate
			select list0).ToList<PTStopMsg>();
		foreach (PTStopMsg current in list)
		{
			reportList.Add(new ReportInfo
			{
				ReportTypeId = "2",
				ReportTypeName = "停工通知单",
				ReportId = current.StopMsgId,
				ReportDate = new System.DateTime?(current.StopDate),
				ConstUnit = current.ConstUnit,
				InputDate = current.InputDate,
				InputUser = current.InputUser,
				FlowState = current.FlowState.Value
			});
		}
		return reportList;
	}
	private System.Collections.Generic.List<ReportInfo> GetRetMsg(System.Collections.Generic.List<ReportInfo> reportList)
	{
		System.Collections.Generic.List<PTRetMsg> list = (
			from ret in this.RetMsgServer
			where ret.PrjGuid == this.hfldPrjId.Value
			select ret into list0
			orderby list0.InputDate
			select list0).ToList<PTRetMsg>();
		foreach (PTRetMsg current in list)
		{
			reportList.Add(new ReportInfo
			{
				ReportTypeId = "3",
				ReportTypeName = "复工通知单",
				ReportId = current.RetMsgId,
				ReportDate = new System.DateTime?(current.RetDate),
				ConstUnit = current.ConstUnit,
				InputDate = current.InputDate,
				InputUser = current.InputUser,
				FlowState = current.FlowState.Value
			});
		}
		return reportList;
	}
	protected void gvReportList_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			string value = this.gvReportList.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["id"] = value;
			string value2 = this.gvReportList.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["typeId"] = value2;
		}
	}
	private void GetProjectInfo()
	{
		if (!string.IsNullOrEmpty(this.hfldPrjId.Value))
		{
			ProjectInfo byId = ProjectInfo.GetById(this.hfldPrjId.Value);
			if (byId != null)
			{
				this.lblProject.Text = byId.PrjName;
				Label expr_40 = this.lblProject;
				expr_40.Text = expr_40.Text + "(" + Common2.GetPrjState(byId.PrjState.ToString(), true) + ")";
				this.hfldPrjState.Value = byId.PrjState.ToString();
				return;
			}
			TenderInfo byId2 = TenderInfo.GetById(this.hfldPrjId.Value);
			if (byId2 != null)
			{
				this.lblProject.Text = byId2.PrjName;
				Label expr_BF = this.lblProject;
				expr_BF.Text = expr_BF.Text + "(" + Common2.GetPrjState(byId2.PrjState.ToString(), false) + ")";
				this.hfldPrjState.Value = byId2.PrjState.ToString();
			}
		}
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


