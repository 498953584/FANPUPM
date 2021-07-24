using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Calendar_CalendarRemindSetAdd : BasePage, IRequiresSessionState
{

	private CalendarRemindSetaction crs
	{
		get
		{
			return new CalendarRemindSetaction();
		}
	}
	protected int RecordID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RECORDID"].ToString());
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	private Guid InfoGuid
	{
		get
		{
			return (Guid)this.ViewState["INFOGUID"];
		}
		set
		{
			this.ViewState["INFOGUID"] = value;
		}
	}
	private DateTime RDate
	{
		get
		{
			return Convert.ToDateTime(this.ViewState["RDATE"]);
		}
		set
		{
			this.ViewState["RDATE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && (base.Request["rid"] != null || base.Request["ig"] != null || base.Request["dt"] != null))
		{
			this.InfoGuid = new Guid(base.Request["ig"]);
			this.RecordID = Convert.ToInt32(base.Request["rid"].ToString());
			this.RDate = Convert.ToDateTime(base.Request["dt"]);
			this.PageBind();
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.crs.Add(this.GetCalendarRemindSet(), this.GetOACalendarInfo(), this.RDate) == 1)
		{
			this.JS.Text = "alert('保存成功!'); window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
	private OACalendarRemindSet GetCalendarRemindSet()
	{
		return new OACalendarRemindSet
		{
			InfoGuid = this.InfoGuid,
			IsSms = this.CBIsSms.Checked ? "1" : "0",
			IsMessage = this.CBIsMessage.Checked ? "1" : "0",
			RemindType = Convert.ToInt32(this.DDLRemindType.SelectedValue),
			RemindHour = Convert.ToInt32(this.TxtHours.Text),
			RemindMinute = Convert.ToInt32(this.TxtMinutes.Text),
			EndDate = Convert.ToDateTime(this.DBEndDate.Text)
		};
	}
	private OACalendarInfo GetOACalendarInfo()
	{
		CalendarInfoAction calendarInfoAction = new CalendarInfoAction();
		return calendarInfoAction.GetModel(this.RecordID);
	}
	private void PageBind()
	{
		CalendarRemindSetaction calendarRemindSetaction = new CalendarRemindSetaction();
		OACalendarRemindSet model = calendarRemindSetaction.GetModel(this.InfoGuid);
		if (model != null)
		{
			this.CBIsMessage.Checked = (model.IsMessage == "1");
			this.CBIsSms.Checked = (model.IsSms == "1");
			this.TxtHours.Text = model.RemindHour.ToString();
			this.TxtMinutes.Text = model.RemindMinute.ToString();
			if (model.RemindType > 1)
			{
				this.DDLRemindType.SelectedValue = model.RemindType.ToString();
			}
			this.DBEndDate.Text = model.EndDate.ToString("yyyy-MM-dd");
		}
	}
}


