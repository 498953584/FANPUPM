using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_PostWeavePerson : BasePage, IRequiresSessionState
{

	public PTDUTYAction hrAction
	{
		get
		{
			return new PTDUTYAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.gvEmployeelist.DataBind();
		}
	}
	protected void gvEmployeelist_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
		}
	}
}


