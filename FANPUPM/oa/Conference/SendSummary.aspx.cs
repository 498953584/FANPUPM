using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SendSummary : BasePage, IRequiresSessionState
	{

		public int MeetingInfoID
		{
			get
			{
				object obj = this.ViewState["MeetingInfoID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["MeetingInfoID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.MeetingInfoID = Convert.ToInt32(base.Request.QueryString["mid"]);
				this.setData(this.MeetingInfoID);
				this.ImageBtn.Attributes["onclick"] = " return confirm('确定删除附件吗？');";
			}
		}
		protected void setData(int meetingInfoID)
		{
			DataTable dataTable = ConferenceManage.QueryMeetingInfo(meetingInfoID);
			if (dataTable.Rows.Count == 1)
			{
				this.txtSummary.Text = dataTable.Rows[0]["SummaryName"].ToString();
				this.txtOriginalName.Text = dataTable.Rows[0]["OriginalName"].ToString();
				this.annexName.InnerText = dataTable.Rows[0]["OriginalName"].ToString();
				this.hdnFilePath.Value = dataTable.Rows[0]["FilePath"].ToString();
			}
			if (this.hdnFilePath.Value.Length > 0)
			{
				this.tr_add.Visible = false;
				this.tr_edit.Visible = true;
				return;
			}
			this.tr_add.Visible = true;
			this.tr_edit.Visible = false;
		}
		protected void ImageBtn_Click(object sender, ImageClickEventArgs e)
		{
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string value = this.hdnFilePath.Value;
			if (fileUpload.Delete(value))
			{
				this.tr_add.Visible = true;
				this.tr_edit.Visible = false;
				Hashtable hashtable = new Hashtable();
				hashtable.Add("OriginalName", SqlStringConstructor.GetQuotedString(""));
				hashtable.Add("FilePath", SqlStringConstructor.GetQuotedString(""));
				hashtable.Add("State", "2");
				string where = " where RecordId = " + this.MeetingInfoID.ToString();
				if (ConferenceManage.UpdMeetingInfo(hashtable, where))
				{
					this.JS.Text = "alert('附件删除成功！');";
					return;
				}
				this.JS.Text = "alert('附件删除失败！');";
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			Hashtable hashtable = new Hashtable();
			hashtable.Add("SummaryName", SqlStringConstructor.GetQuotedString(this.txtSummary.Text));
			if (this.txtFilePath.HasFile)
			{
				HttpPostedFile postedFile = this.txtFilePath.PostedFile;
				com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
				string[] array = fileUpload.Upload(postedFile, 4);
				hashtable.Add("OriginalName", SqlStringConstructor.GetQuotedString(array[0]));
				hashtable.Add("FilePath", SqlStringConstructor.GetQuotedString(array[1]));
			}
			string where = " where RecordId = " + this.MeetingInfoID.ToString();
			if (ConferenceManage.UpdMeetingInfo(hashtable, where))
			{
				this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败！');";
		}
	}


