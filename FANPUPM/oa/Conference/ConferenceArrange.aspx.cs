using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConferenceArrange : BasePage, IRequiresSessionState
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

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UserCode = this.Session["yhdm"].ToString();
				this.btnAdd.Attributes["onclick"] = "openArrangeEdit(1,'" + this.UserCode + "')";
				this.btnEdit.Attributes["onclick"] = "openArrangeEdit(2,'" + this.UserCode + "')";
				this.btnPutOff.Attributes["onclick"] = "openArrangeEdit(3,'" + this.UserCode + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnFromTemplate.Attributes["onclick"] = "addFromTemplate();";
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.gvMeetinInfo.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.gvMeetinInfo.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnRecordID.Value);
			if (ConferenceManage.DelMeetingInfo(recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvMeetinInfo.DataBind();
			}
		}
		protected void btnFromTemplate_Click(object sender, EventArgs e)
		{
			this.gvMeetinInfo.DataBind();
		}
		protected void btnLaunch_Click(object sender, EventArgs e)
		{
			int meetingInfoId = Convert.ToInt32(this.hdnRecordID.Value);
			if (this.ConferenceLaunch(meetingInfoId, this.UserCode, "1"))
			{
				this.JS.Text = "alert('会议通知发起成功！');";
				this.gvMeetinInfo.DataBind();
			}
		}
		protected void btnPutOff_Click(object sender, EventArgs e)
		{
			this.gvMeetinInfo.DataBind();
		}
		protected void btnCancel_Click(object sender, EventArgs e)
		{
			int meetingInfoId = Convert.ToInt32(this.hdnRecordID.Value);
			if (this.ConferenceLaunch(meetingInfoId, this.UserCode, "0"))
			{
				this.JS.Text = "alert('会议取消，消息已发送！');";
				this.gvMeetinInfo.DataBind();
			}
		}
		protected void gvMeetinInfo_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.gvMeetinInfo.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				string text2 = ((DataRowView)e.Row.DataItem)["State"].ToString();
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow('",
					text,
					"',",
					text2,
					");"
				});
			}
		}
		protected bool ConferenceLaunch(int meetingInfoId, string sendUser, string flag)
		{
			DateTime sendTime = default(DateTime);
			sendTime = DateTime.Now;
			DataTable dataTable = ConferenceManage.QuerySubsectionList(meetingInfoId);
			if (dataTable.Rows.Count > 0)
			{
				foreach (DataRow dataRow in dataTable.Rows)
				{
					if (dataRow["IsSms"].ToString() == "1")
					{
						string message;
						string types;
						if (flag == "1")
						{
							message = string.Concat(new string[]
							{
								dataRow["CallHour"].ToString(),
								"时",
								dataRow["CallMinute"].ToString(),
								"分",
								dataRow["Topic"].ToString(),
								",请准时参加！"
							});
							types = "012";
						}
						else
						{
							message = string.Concat(new string[]
							{
								dataRow["CallHour"].ToString(),
								"时",
								dataRow["CallMinute"].ToString(),
								"分",
								dataRow["Topic"].ToString(),
								"已取消,请相互转告！"
							});
							types = "013";
						}
						string text = dataRow["AttendManCodes"].ToString();
						int i = text.IndexOf(",");
						if (i > 0)
						{
							while (i > 0)
							{
								string receiveUser = text.Substring(0, i);
								if (!this.SendSms(sendUser, receiveUser, message, sendTime, meetingInfoId, types))
								{
									bool result = false;
									return result;
								}
								text = text.Substring(i + 1, text.Length - i - 1);
								i = text.IndexOf(",");
							}
							if (!this.SendSms(sendUser, text, message, sendTime, meetingInfoId, types))
							{
								bool result = false;
								return result;
							}
						}
						else
						{
							if (!this.SendSms(sendUser, text, message, sendTime, meetingInfoId, types))
							{
								bool result = false;
								return result;
							}
						}
					}
				}
				return ConferenceManage.SetLaunchState(meetingInfoId, flag);
			}
			return false;
		}
		protected bool SendSms(string sendUser, string receiveUser, string message, DateTime sendTime, int recordId, string types)
		{
			return PublicInterface.SendSmsMsg(new SMSLog
			{
				SendUser = sendUser,
				ReceiveUser = receiveUser,
				Message = message,
				SendTime = sendTime,
				V_LXBM = types,
				I_XGID = recordId.ToString()
			}) == 1;
		}
	}


