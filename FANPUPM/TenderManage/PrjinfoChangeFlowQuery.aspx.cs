using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class TenderManage_PrjinfoChangeFlowQuery : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.AspNetPager1.PageSize = NBasePage.pagesize;
			this.bindGv();
		}
	}
	private void bindGv()
	{
		PTPrjInfoStateChangeService pTPrjInfoStateChangeService = new PTPrjInfoStateChangeService();
		string prjGuid = base.Request["prjId"].ToString();
		this.AspNetPager1.RecordCount = pTPrjInfoStateChangeService.GetCountByPrjGuidAndCondition(prjGuid, this.txtStartTime.Text, this.txtEndTime.Text);
		this.gvDataInfo.DataSource = pTPrjInfoStateChangeService.GetLstByPrjGuid(prjGuid, this.txtStartTime.Text, this.txtEndTime.Text, this.AspNetPager1.CurrentPageIndex, this.AspNetPager1.PageSize);
		this.gvDataInfo.DataBind();
	}
	protected void gvDataInfo_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			string value = this.gvDataInfo.DataKeys[e.Row.RowIndex]["PrjId"].ToString();
			e.Row.Attributes["id"] = value;
			e.Row.Attributes["guid"] = value;
			e.Row.Attributes["flowState"] = this.gvDataInfo.DataKeys[e.Row.RowIndex]["FlowState"].ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
	protected string GetStateName(string itemCode)
	{
		int itemCode2 = int.Parse(itemCode);
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		return basicCodeListService.GetNameByTypeAndCode("ProjectState", itemCode2);
	}
	protected void brnQuery_Click(object sender, System.EventArgs e)
	{
		this.bindGv();
	}
}


