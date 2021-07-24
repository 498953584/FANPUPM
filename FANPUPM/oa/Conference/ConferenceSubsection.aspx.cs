using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConferenceSubsection : BasePage, IRequiresSessionState
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
				this.btnAdd.Attributes["onclick"] = "openSubsectionEdit(1,'" + this.MeetingInfoID + "')";
				this.btnEdit.Attributes["onclick"] = "openSubsectionEdit(2,'" + this.MeetingInfoID + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.gvSubsection.DataBind();
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.gvSubsection.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.gvSubsection.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnRecordID.Value);
			if (ConferenceManage.DelSubsection(recordId))
			{
				this.JS.Text = "alert('删除成功!');";
				this.gvSubsection.DataBind();
			}
		}
		protected void gvSubsection_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvSubsection.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
			}
		}
	}


