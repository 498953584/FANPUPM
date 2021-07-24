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
public partial class StartStopReturnWork_StartWorkReportList : NBasePage, IRequiresSessionState
{
	private ProjectInfo proInfo = new ProjectInfo();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			int[] prjTypeCodes = new int[]
			{
				5,
				7,
				17
			};
			TypeList.BindDrop(this.dropPrjState, prjTypeCodes, "", null);
			this.BindFlowState();
			this.bindGv();
		}
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.bindGv();
	}
	protected void BindFlowState()
	{
		this.dropWFState.Items.Add(new ListItem("", ""));
		this.dropWFState.Items.Add(new ListItem("未提交", "-1"));
		this.dropWFState.Items.Add(new ListItem("审核中", "0"));
		this.dropWFState.Items.Add(new ListItem("已审核", "1"));
		this.dropWFState.Items.Add(new ListItem("重报", "-3"));
		this.dropWFState.Items.Add(new ListItem("驳回", "-4"));
	}
	protected void bindGv()
	{
		string prjCode = this.txtPrjCode.Text.Trim();
		string prjName = this.txtPrjName.Text.Trim();
		System.DateTime? start = null;
		if (!string.IsNullOrEmpty(this.txtStartTime.Text.Trim()))
		{
			start = new System.DateTime?(System.DateTime.Parse(this.txtStartTime.Text.Trim()));
		}
		System.DateTime? end = null;
		if (!string.IsNullOrEmpty(this.txtEndTime.Text.Trim()))
		{
			end = new System.DateTime?(System.DateTime.Parse(this.txtEndTime.Text.Trim()).AddDays(1.0));
		}
		string flowState = null;
		if (this.dropWFState.SelectedValue != "")
		{
			flowState = this.dropWFState.SelectedValue;
		}
		int? prjState = null;
		if (!string.IsNullOrEmpty(this.dropPrjState.SelectedValue.Trim()))
		{
			prjState = new int?(int.Parse(this.dropPrjState.SelectedValue.Trim()));
		}
		string prjManagerName = this.txtPrjManager.Text.Trim();
		this.AspNetPager1.RecordCount = this.proInfo.GetStartWorkPrjCount(prjName, prjCode, start, end, flowState, prjState, base.UserCode, prjManagerName);
		DataTable startWorkPrjInfos = this.proInfo.GetStartWorkPrjInfos(prjName, prjCode, start, end, flowState, prjState, base.UserCode, prjManagerName, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvwPrjInfo.DataSource = startWorkPrjInfos;
		this.gvwPrjInfo.DataBind();
	}
	protected void gvwPrjInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["PrjGuid"].ToString();
			e.Row.Attributes["guid"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["ReportId"].ToString();
			e.Row.Attributes["primit"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["Primit"].ToString();
			e.Row.Attributes["flowState"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
			e.Row.Attributes["prjState"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
			e.Row.Attributes["reportId"] = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["ReportId"].ToString();
			string text = this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["TypeCode"].ToString();
			this.gvwPrjInfo.DataKeys[e.Row.RowIndex]["PrjState"].ToString();
			if (text.Length == 5)
			{
				e.Row.Attributes["isMainContract"] = "True";
				return;
			}
			e.Row.Attributes["isMainContract"] = "False";
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


