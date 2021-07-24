using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ConferenceDataSend : BasePage, IRequiresSessionState
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
				return "";
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
				this.SqlMeetingInfo.DataBind();
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
		protected void btnSearch_Click(object sender, EventArgs e)
		{
			this.SqlMeetingInfo.DataBind();
			this.gvMeetinInfo.DataBind();
		}
		protected string GetSelectCommandText()
		{
			string text = "SELECT [RecordID], [UserCode], [RecordDate], [MeetingTitle], [MeetingPlace], [CallDate], [CallTime], [CallMinute], [Content], [State],(case State when 0 then '未发起' when 1 then '未召开' when 2 then '已召开' end) as StateName, [PigeonholeState], [SummaryName], [FilePath] ";
			text = text + " FROM [OA_Meeting_Info] WHERE ([State] > 0) AND([UserCode] = '" + this.UserCode + "') ";
			string selectedValue = this.ddlClass.SelectedValue;
			string text2 = this.tbKey.Text;
			if (selectedValue != "")
			{
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					" AND (",
					selectedValue,
					" like '%",
					text2.ToString(),
					"%') "
				});
			}
			if (this.txtBeginDate.Text != "")
			{
				text = text + " AND ( [CallDate] >= '" + this.txtBeginDate.Text + "' ";
				if (this.txtEndDate.Text != "")
				{
					text = text + " AND [CallDate] < '" + this.txtEndDate.Text + "') ";
				}
				else
				{
					text += ") ";
				}
			}
			else
			{
				if (this.txtEndDate.Text != "")
				{
					text = text + " AND ( [CallDate] < '" + this.txtEndDate.Text + "')";
				}
			}
			return text + " order by CallDate desc ";
		}
	}


