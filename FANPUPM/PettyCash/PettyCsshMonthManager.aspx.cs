using ASP;
using cn.justwin.Domain.EasyUI;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PettyCash_PettyCsshMonthManager : Page, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			DepEmployee depEmployee = new DepEmployee();
			string json = depEmployee.GetJson(2);
			this.hfldDepEmployeeJson.Value = json;
		}
	}
}


