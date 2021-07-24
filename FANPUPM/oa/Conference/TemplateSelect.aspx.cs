using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TemplateSelect : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
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
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			int templateId = 0;
			if (this.hdnTemplateID.Value != "")
			{
				templateId = Convert.ToInt32(this.hdnTemplateID.Value);
			}
			string userCode = this.Session["yhdm"].ToString();
			if (ConferenceManage.AddFromTemplate(templateId, userCode))
			{
				this.Page.RegisterStartupScript("ok", "<script>alert('从模板增加成功!');returnValue=true;top.window.close();</script>");
				return;
			}
			this.Page.RegisterStartupScript("ok", "<script>alert('从模板增加失败!');</script>");
		}
	}


