using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_MailAdmin_ReclaimMail : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.btnDel.Attributes["onclick"] = "javascript:if(!confirm('确认要收回选定邮件么？')){return false;}";
		}
	}
	protected void gvReclaim_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		DataRowView dataRowView = (DataRowView)e.Row.DataItem;
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["onclick"] = string.Concat(new string[]
			{
				"OnRecord(this);ClickRow('",
				this.gvReclaim.DataKeys[e.Row.RowIndex]["I_YJ_ID"].ToString(),
				"','",
				dataRowView["I_SJID"].ToString(),
				"');"
			});
		}
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		MailManage mailManage = new MailManage();
		mailManage.ReclaimMain(Convert.ToInt32(this.hdnKeyField.Value));
		mailManage.RecdeskTookip(this.HdnSJID.Value.ToString());
		this.gvReclaim.DataBind();
	}
}


