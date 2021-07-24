using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class SysFrame_Nav_right : BasePage, IRequiresSessionState
{
	public string strSkinPath = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		this.strSkinPath = "skin" + this.Session["SkinID"].ToString();
	}
}


