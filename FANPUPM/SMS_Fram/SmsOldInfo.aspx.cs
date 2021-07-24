using ASP;
using cn.justwin.BLL;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SMS_Fram_SmsOldInfo : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		this.BtnCancel.Attributes["onclick"] = "javascript:document.getElementById('divMySearch').style.display='none';";
	}
}


