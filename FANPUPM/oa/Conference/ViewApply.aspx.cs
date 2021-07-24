using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ViewApply : BasePage, IRequiresSessionState
	{
		public int RecordId
		{
			get
			{
				object obj = this.ViewState["RecordId"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RecordId"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.RecordId = Convert.ToInt32(base.Request.QueryString["mid"]);
				this.setData(this.RecordId);
				this.hdnRecordId.Value = this.RecordId.ToString();
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryApplyInfo(recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtMeetingRoom.Text = dataTable.Rows[0]["MeetingRoom"].ToString();
				this.hdnMeetinRoomID.Value = dataTable.Rows[0]["MeetingRoomID"].ToString();
				this.txtHumanNumber.Text = dataTable.Rows[0]["HumanNumber"].ToString();
				this.txtTitle.Text = dataTable.Rows[0]["Title"].ToString();
				this.dbUserDate.Text = dataTable.Rows[0]["UserDate"].ToString();
				this.ddltBeginHour.SelectedValue = dataTable.Rows[0]["BeginHour"].ToString();
				this.ddltBeginMinute.SelectedValue = dataTable.Rows[0]["BeginMinute"].ToString();
				this.ddltEndHour.SelectedValue = dataTable.Rows[0]["EndHour"].ToString();
				this.ddltEndMinute.SelectedValue = dataTable.Rows[0]["EndMinute"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (ConferenceManage.SetApplyState(2, this.RecordId))
			{
				this.JS.Text = "alert('会议室已安排!');window.returnValue='" + this.RecordId.ToString() + "';window.close();";
				return;
			}
			this.JS.Text = "alert('会议室安排失败!');";
		}
	}


