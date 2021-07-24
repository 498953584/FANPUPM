using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TenderManage_ProjectState : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			System.Collections.Generic.IList<BasicCode> byType = BasicCode.GetByType("ProjectState");
			this.rptProjectState.DataSource = byType;
			this.rptProjectState.DataBind();
		}
	}
}


