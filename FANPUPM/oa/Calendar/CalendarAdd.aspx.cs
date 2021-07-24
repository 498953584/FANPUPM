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
public partial class oa_Calendar_CalendarAdd : BasePage, IRequiresSessionState
{

	protected string yhdm
	{
		get
		{
			return this.ViewState["YHDM"].ToString();
		}
		set
		{
			this.ViewState["YHDM"] = value;
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
	private CalendarInfoAction cia
	{
		get
		{
			return new CalendarInfoAction();
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
	private string Type
	{
		get
		{
			return this.ViewState["YTPE"].ToString();
		}
		set
		{
			this.ViewState["YTPE"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && (base.Request["t"] != null || base.Request["dt"] != null || base.Request["rid"] != null || base.Request["ig"] != null))
		{
			this.Type = base.Request["t"].ToString();
			this.RecordID = Convert.ToInt32(base.Request["rid"]);
			this.TxtRecordDate.Text = base.Request["dt"].ToString();
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			this.TxtUserName.Text = currentUserInfo.UserName;
			this.HdnUserCode.Value = currentUserInfo.UserCode;
			this.InfoGuid = Guid.NewGuid();
			if (this.Type != "Add")
			{
				this.PageBind();
				this.InfoGuid = new Guid(base.Request["ig"]);
				return;
			}
			this.CBIsSms.Checked = false;
			this.CBIsMessage.Checked = false;
		}
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.TxtContent.Text.Length > 1000)
		{
			this.JS.Text = "top.ui.alert('详细内容不能大于1000字!');";
			return;
		}
		if (!this.Page.IsValid)
		{
			return;
		}
		if (this.Type == "Add")
		{
			if (this.cia.Add(this.getOACalendarInfoModel(), this.GetCalendarRemindSet()) == 1)
			{
				this.JS.Text = "top.ui.winSuccess({ parentName: '_calendaradd' });";
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败!');";
			return;
		}
		else
		{
			if (this.cia.Update(this.getOACalendarInfoModel(), this.GetCalendarRemindSet()) == 1)
			{
				this.JS.Text = "top.ui.winSuccess({ parentName: '_calendaradd' });";
				return;
			}
			this.JS.Text = "top.ui.alert('保存失败!');";
			return;
		}
	}
	private OACalendarInfo getOACalendarInfoModel()
	{
		return new OACalendarInfo
		{
			UserCode = this.HdnUserCode.Value.ToString(),
			RecordDate = Convert.ToDateTime(this.TxtRecordDate.Text),
			Title = this.TxtTitle.Text,
			Content = this.TxtContent.Text,
			IsRemind = this.CBIsRemind.Checked ? "1" : "0",
			InfoGuid = this.InfoGuid
		};
	}
	private OACalendarRemindSet GetCalendarRemindSet()
	{
		return new OACalendarRemindSet
		{
			InfoGuid = this.InfoGuid,
			IsSms = this.CBIsSms.Checked ? "1" : "0",
			IsMessage = this.CBIsMessage.Checked ? "1" : "0",
			RemindType = 1,
			RemindHour = Convert.ToInt32(this.TxtHours.Text),
			RemindMinute = Convert.ToInt32(this.TxtMinutes.Text),
			EndDate = Convert.ToDateTime(this.TxtRecordDate.Text)
		};
	}
	private void PageBind()
	{
		OACalendarInfo model = this.cia.GetModel(this.RecordID);
		this.TxtRecordDate.Text = Convert.ToDateTime(model.RecordDate).ToString("yyyy-MM-dd");
		this.TxtContent.Text = model.Content;
		this.TxtTitle.Text = model.Title;
		userManageDb userManageDb = new userManageDb();
		this.TxtUserName.Text = userManageDb.GetUserName(model.UserCode);
		this.CBIsRemind.Checked = (model.IsRemind == "1");
		if (!this.CBIsRemind.Checked)
		{
			this.CBIsSms.Enabled = false;
			this.CBIsMessage.Enabled = false;
		}
		this.InfoGuid = model.InfoGuid;
		CalendarRemindSetaction calendarRemindSetaction = new CalendarRemindSetaction();
		OACalendarRemindSet model2 = calendarRemindSetaction.GetModel(this.InfoGuid);
		if (model2 != null)
		{
			this.TxtHours.Text = model2.RemindHour.ToString();
			this.TxtMinutes.Text = model2.RemindMinute.ToString();
			this.CBIsMessage.Checked = (model2.IsMessage == "1");
			this.CBIsSms.Checked = (model2.IsSms == "1");
			return;
		}
		this.CBIsMessage.Checked = false;
		this.CBIsSms.Checked = false;
	}
	protected void CBIsRemind_CheckedChanged(object sender, EventArgs e)
	{
		if (!this.CBIsRemind.Checked)
		{
			this.CBIsSms.Enabled = false;
			this.CBIsMessage.Enabled = false;
			this.CBIsSms.Checked = false;
			this.CBIsMessage.Checked = false;
			return;
		}
		this.CBIsSms.Enabled = true;
		this.CBIsMessage.Enabled = true;
	}
}


