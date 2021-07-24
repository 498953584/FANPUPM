using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ArrangeEdit : BasePage, IRequiresSessionState
	{
		public new string UserCode
		{
			get
			{
				object obj = this.ViewState["UserCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["UserCode"] = value;
			}
		}
		public int RecordID
		{
			get
			{
				object obj = this.ViewState["RecordID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["RecordID"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = base.Request.QueryString["code"].ToString();
				this.RecordID = Convert.ToInt32(base.Request.QueryString["rid"]);
				string a = base.Request.QueryString["t"].ToString();
				if (a == "3")
				{
					this.pageTile.InnerText = "会议推迟";
					this.txtMeetingTitle.Enabled = false;
					this.img2.Visible = false;
					this.BtnSave.Text = "推迟确认";
					this.Page.Header.Title = "会议推迟确认";
				}
				else
				{
					this.pageTile.InnerText = "会议信息登记";
					this.txtMeetingTitle.Enabled = true;
					this.img2.Visible = true;
					this.BtnSave.Text = "保 存";
					this.Page.Header.Title = "会议信息维护";
				}
				if (this.RecordID != 0)
				{
					this.setData(this.RecordID);
				}
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryMeetingInfo(recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtMeetingTitle.Text = dataTable.Rows[0]["MeetingTitle"].ToString();
				this.hdnMeetingTitle.Value = dataTable.Rows[0]["MeetingTitle"].ToString();
				this.txtMeetingPlace.Text = dataTable.Rows[0]["MeetingPlace"].ToString();
				this.hdnMeetingRoom.Value = dataTable.Rows[0]["MeetingPlace"].ToString();
				this.txtCallDate.Text = ((DateTime)dataTable.Rows[0]["CallDate"]).ToShortDateString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
				this.ddltCallTime.SelectedValue = dataTable.Rows[0]["CallTime"].ToString();
				this.ddltCallMinute.SelectedValue = dataTable.Rows[0]["CallMinute"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			DateTime dateTime = default(DateTime);
			dateTime = DateTime.Now;
			DateTime dateTime2 = default(DateTime);
			dateTime2 = Convert.ToDateTime(this.txtCallDate.Text);
			Hashtable hashtable = new Hashtable();
			hashtable.Add("UserCode", SqlStringConstructor.GetQuotedString(this.UserCode));
			hashtable.Add("RecordDate", SqlStringConstructor.GetQuotedString(dateTime.ToString("yyyy-MM-dd")));
			hashtable.Add("MeetingTitle", SqlStringConstructor.GetQuotedString(this.txtMeetingTitle.Text));
			hashtable.Add("MeetingPlace", SqlStringConstructor.GetQuotedString(this.txtMeetingPlace.Text));
			hashtable.Add("CallDate", SqlStringConstructor.GetQuotedString(dateTime2.ToString("yyyy-MM-dd")));
			hashtable.Add("CallTime", this.ddltCallTime.SelectedValue.ToString());
			hashtable.Add("CallMinute", this.ddltCallMinute.SelectedValue.ToString());
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			hashtable.Add("State", "0");
			hashtable.Add("PigeonholeState", SqlStringConstructor.GetQuotedString("0"));
			if (this.RecordID == 0)
			{
				if (ConferenceManage.AddMeetingInfo(hashtable))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
			else
			{
				string where = " where RecordID = " + this.RecordID.ToString();
				if (ConferenceManage.UpdMeetingInfo(hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


