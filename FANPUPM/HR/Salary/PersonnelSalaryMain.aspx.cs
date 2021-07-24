using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class HR_Salary_PersonnelSalaryMain : BasePage, IRequiresSessionState
{
	protected int Type
	{
		get
		{
			return Convert.ToInt32(this.ViewState["Type"]);
		}
		set
		{
			this.ViewState["Type"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["t"] != null)
		{
			this.Type = Convert.ToInt32(base.Request["t"]);
			switch (this.Type)
			{
			case 1:
				this.frmPage.Attributes["src"] = "PersonnelSalaryList.aspx?vb=";
				return;
			case 2:
				this.frmPage.Attributes["src"] = "SalaryIPIFrame.aspx?vb=";
				break;
			default:
				return;
			}
		}
	}
}


