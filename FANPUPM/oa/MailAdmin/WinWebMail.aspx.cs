using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_MailAdmin_WinWebMail : BasePage, IRequiresSessionState
{

	protected dzyjwbyjAction da
	{
		get
		{
			return new dzyjwbyjAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			DataTable list = this.da.GetList(" v_yhdm = '" + this.Session["yhdm"].ToString() + "'");
			if (list.Rows.Count > 0)
			{
				this.tb_yhdm.Value = list.Rows[0]["v_wbyjmc"].ToString();
				this.tb_pwd.Value = list.Rows[0]["v_password"].ToString();
				if (list.Rows[0]["v_wbyjmc"].ToString() != "" || list.Rows[0]["v_wbyjmc"].ToString() != "")
				{
					this.Page.ClientScript.RegisterStartupScript(base.GetType(), "ok", "validatorSpace()", true);
					return;
				}
				base.Response.Redirect("/WinWebMail/default.asp");
				return;
			}
			else
			{
				base.Response.Redirect("/WinWebMail/default.asp");
			}
		}
	}
}


