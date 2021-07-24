using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_ApplicationLock : BasePage, IRequiresSessionState
{
	private ApplicationAction aa
	{
		get
		{
			return new ApplicationAction();
		}
	}
	private Guid rid
	{
		get
		{
			return (Guid)this.ViewState["RID"];
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["ic"] != null)
			{
				this.rid = new Guid(base.Request["ic"]);
				this.GetPageData();
			}
		}
	}
	protected void GetPageData()
	{
		HRLeaveApplication model = this.aa.GetModel(this.rid);
		if (model != null)
		{
			this.LbBeginDate.Text = model.BeginDate.ToString("yyyy-MM-dd");
			this.LbDays.Text = model.Days.ToString();
			this.LbReason.Text = model.Reason.ToString();
			this.LbBackDate.Text = model.BackDate.ToString();
			this.LbLeaveDays.Text = model.LeaveDays.ToString();
			string key;
			switch (key = model.LeaveType.ToString())
			{
			case "1":
				this.LbLeaveType.Text = "事假";
				break;
			case "2":
				this.LbLeaveType.Text = "婚假";
				break;
			case "3":
				this.LbLeaveType.Text = "年休假";
				break;
			case "4":
				this.LbLeaveType.Text = "工伤";
				break;
			case "5":
				this.LbLeaveType.Text = "病假";
				break;
			case "6":
				this.LbLeaveType.Text = "产假";
				break;
			case "7":
				this.LbLeaveType.Text = "丧假";
				break;
			}
			userManageDb userManageDb = new userManageDb();
			this.LbUserName.Text = userManageDb.GetUserName(model.UserCode);
		}
	}
}


