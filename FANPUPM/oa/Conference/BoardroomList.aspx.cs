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
	public partial class BoardroomList : BasePage, IRequiresSessionState
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
				this.btnAdd.Attributes["onclick"] = "javascript:return openWin('add','" + this.CorpCode + "');";
				this.btnEdit.Attributes["onclick"] = "javascript:return openWin('edit','" + this.CorpCode + "');";
				this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要删除选定会议室信息吗？')){return false;}";
				this.GridView1.DataBind();
			}
		}
		protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.GridView1.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int recordId = Convert.ToInt32(this.hdnRecordID.Value);
			if (ConferenceManage.DelBoardroom(recordId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.GridView1.DataBind();
			}
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
		}
		protected void btnRefresh_Click(object sender, EventArgs e)
		{
			this.GridView1.DataBind();
		}
	}


