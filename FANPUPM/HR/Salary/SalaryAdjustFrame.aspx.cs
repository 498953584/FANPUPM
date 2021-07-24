using ASP;
using com.jwsoft.common.baseclass;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class HR_Salary_SalaryAdjustFrame : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
	}
}


