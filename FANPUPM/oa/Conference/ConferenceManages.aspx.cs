using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConferenceManages : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.btnSendSummary.Attributes["onclick"] = "sendsummary();";
				this.gvMeetinInfo.DataBind();
			}
		}
		protected void gvMeetinInfo_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType != DataControlRowType.Pager)
			{
				e.Row.Cells[0].Attributes["style"] = "display:none";
			}
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.gvMeetinInfo.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				string text2 = ((DataRowView)e.Row.DataItem)["State"].ToString();
				string text3 = ((DataRowView)e.Row.DataItem)["PigeonholeState"].ToString();
				string text4 = ((DataRowView)e.Row.DataItem)["OriginalName"].ToString();
				string text5 = ((DataRowView)e.Row.DataItem)["FilePath"].ToString();
				e.Row.Cells[8].Attributes["onclick"] = string.Concat(new string[]
				{
					"download('",
					text5,
					"','",
					text4,
					"');"
				});
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow('",
					text,
					"',",
					text2,
					",'",
					text3,
					"');this.cells[0].childNodes[0].click();"
				});
			}
		}
		protected void btnSendSummary_Click(object sender, EventArgs e)
		{
			this.gvMeetinInfo.DataBind();
		}
		protected void btnPigeOnHole_Click(object sender, EventArgs e)
		{
			string corpCode = "%";
			string userCode = this.Session["yhdm"].ToString();
			DataTable dataTable = DocumentAction.QueryCorpCode(userCode);
			if (dataTable.Rows.Count > 0)
			{
				corpCode = dataTable.Rows[0]["corpCode"].ToString();
			}
			int meetingInfoId = Convert.ToInt32(this.hdnRecordID.Value);
			if (ConferenceManage.PigeOnHole(userCode, corpCode, meetingInfoId))
			{
				this.JS.Text = "alert('会议纪要已归档！');";
				this.gvMeetinInfo.DataBind();
			}
		}
		protected void gvMeetinInfo_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.gvConferenceData.DataBind();
		}
		protected void gvConferenceData_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				this.gvConferenceData.DataKeys[e.Row.RowIndex]["AnnexCode"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);";
			}
		}
	}


