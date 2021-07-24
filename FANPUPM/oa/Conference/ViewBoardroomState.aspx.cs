using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ViewBoardroomState : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			if (!this.Page.IsPostBack)
			{
				this.ddltYear.DataBind();
				this.ddltMonth.DataBind();
				this.ddltMeetingRoom.DataBind();
				this.ddltMonth.SelectedValue = DateTime.Now.Month.ToString();
				if (this.ddltYear.Items.Count > 0)
				{
					int meetingRoomId = Convert.ToInt32(this.ddltMeetingRoom.SelectedValue);
					int year = Convert.ToInt32(this.ddltYear.SelectedValue);
					int month = Convert.ToInt32(this.ddltMonth.SelectedValue);
					ConferenceManage.CreateStateTable(this.tbState, meetingRoomId, year, month);
				}
			}
		}
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			int meetingRoomId = Convert.ToInt32(this.ddltMeetingRoom.SelectedValue);
			int year = Convert.ToInt32(this.ddltYear.SelectedValue);
			int month = Convert.ToInt32(this.ddltMonth.SelectedValue);
			ConferenceManage.CreateStateTable(this.tbState, meetingRoomId, year, month);
		}
	}


