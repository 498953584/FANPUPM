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
	public partial class SelectRoomList : BasePage, IRequiresSessionState
	{
		public string CorpCode
		{
			get
			{
				object obj = this.ViewState["CorpCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["CorpCode"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.CorpCode = base.Request.QueryString["code"];
				this.BtnSave.Attributes["onclick"] = "javascript:confirmselect();";
				this.GridView1.DataBind();
			}
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
		}
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string text = this.GridView1.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				string text2 = ((DataRowView)e.Row.DataItem)["MeetingRoom"].ToString();
				e.Row.Attributes["onclick"] = string.Concat(new string[]
				{
					"OnRecord(this);ClickRow('",
					text,
					"','",
					text2,
					"');"
				});
			}
		}
	}


