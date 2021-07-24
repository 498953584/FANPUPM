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
	public partial class TopicEdit : BasePage, IRequiresSessionState
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
				this.MeetingInfoID = Convert.ToInt32(base.Request.QueryString["mid"]);
				this.RecordID = Convert.ToInt32(base.Request.QueryString["rid"]);
				if (this.RecordID != 0)
				{
					this.setData(this.RecordID);
				}
			}
		}
		protected void setData(int recordId)
		{
			DataTable dataTable = ConferenceManage.QueryConferenceTopic(recordId);
			if (dataTable.Rows.Count == 1)
			{
				this.txtTopic.Text = dataTable.Rows[0]["Topic"].ToString();
				this.txtContent.Text = dataTable.Rows[0]["Content"].ToString();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			string pStr = "%";
			DataTable dataTable = DocumentAction.QueryCorpCode(this.UserCode);
			if (dataTable.Rows.Count > 0)
			{
				pStr = dataTable.Rows[0]["CorpCode"].ToString();
			}
			DateTime dateTime = default(DateTime);
			dateTime = DateTime.Now;
			Hashtable hashtable = new Hashtable();
			hashtable.Add("MeetingInfoID", this.MeetingInfoID.ToString());
			hashtable.Add("UserCode", SqlStringConstructor.GetQuotedString(this.UserCode));
			hashtable.Add("RecordDate", SqlStringConstructor.GetQuotedString(dateTime.ToString("yyyy-MM-dd")));
			hashtable.Add("CorpCode", SqlStringConstructor.GetQuotedString(pStr));
			hashtable.Add("Topic", SqlStringConstructor.GetQuotedString(this.txtTopic.Text));
			hashtable.Add("Content", SqlStringConstructor.GetQuotedString(this.txtContent.Text));
			if (this.RecordID == 0)
			{
				if (ConferenceManage.AddConferenceTopic(hashtable))
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
				if (ConferenceManage.UpdConferenceTopic(hashtable, where))
				{
					this.JS.Text = "alert('保存成功！');window.returnValue=true;window.close();";
					return;
				}
				this.JS.Text = "alert('保存失败！');";
				return;
			}
		}
	}


