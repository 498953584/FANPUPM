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
	public partial class TemplateList : BasePage, IRequiresSessionState
	{

		public int ClassID
		{
			get
			{
				object obj = this.ViewState["ClassID"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				this.ViewState["ClassID"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				this.btnAdd.Attributes["onclick"] = "openTemplateEdit(1,'" + this.ClassID + "')";
				this.btnEdit.Attributes["onclick"] = "openTemplateEdit(2,'" + this.ClassID + "')";
				this.btnDel.Attributes["onclick"] = " return confirm('确定删除当前记录数据吗？');";
				this.gvTemplatelist.DataBind();
			}
		}
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			this.gvTemplatelist.DataBind();
		}
		protected void btnEdit_Click(object sender, EventArgs e)
		{
			this.gvTemplatelist.DataBind();
		}
		protected void btnDel_Click(object sender, EventArgs e)
		{
			int templateId = Convert.ToInt32(this.hdnTemplateID.Value);
			if (ConferenceManage.DelMeetingTemplate(templateId))
			{
				this.JS.Text = "alert('删除成功！');";
				this.gvTemplatelist.DataBind();
			}
		}
		protected void gvTemplatelist_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if (e.Row.RowType == DataControlRowType.DataRow)
			{
				e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
				string str = this.gvTemplatelist.DataKeys[e.Row.RowIndex]["RecordID"].ToString();
				e.Row.Attributes["onclick"] = "OnRecord(this);ClickRow('" + str + "');";
			}
		}
	}


