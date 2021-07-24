using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SendDataList : BasePage, IRequiresSessionState
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
				this.MeetingInfoID = Convert.ToInt32(base.Request["mid"]);
				this.btnAdd.Attributes["onclick"] = "openTopicEdit(1,'" + this.MeetingInfoID + "')";
				this.btnEdit.Attributes["onclick"] = "openTopicEdit(2,'" + this.MeetingInfoID + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.btnAnnex.Attributes["onclick"] = "UpFile(4,'" + this.MeetingInfoID.ToString() + "');";
				this.gvConferenceData.DataBind();
			}
		}
		protected void gvConferenceData_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvConferenceData.DataKeys[e.Row.RowIndex]["AnnexCode"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('0','" + str + "');";
			}
		}
		protected void gvTopic_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvTopic.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('1','" + str + "');";
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.gvTopic.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.gvTopic.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnTemplateID.Value);
			if (ConferenceManage.DelConferenceTopic(recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvTopic.DataBind();
			}
		}
		protected void btnAnnex_Click(object sender, EventArgs e)
		{
			this.gvConferenceData.DataBind();
		}
	}


