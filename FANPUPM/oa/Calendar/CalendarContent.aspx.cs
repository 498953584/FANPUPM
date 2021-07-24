using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_Calendar_CalendarContent : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		CalendarInfoAction calendarInfoAction = new CalendarInfoAction();
		int recordID = Convert.ToInt32(this.id);
		this.content.InnerText = calendarInfoAction.GetModel(recordID).Content;
	}
}


