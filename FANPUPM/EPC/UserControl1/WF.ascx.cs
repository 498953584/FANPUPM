using ASP;
using cn.justwin.Domain.Services;
using System;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_UserControl1_WF : System.Web.UI.UserControl
{
	public int ISdel = 1;
	public int Type = 1;
	public string BusiCode
	{
		set
		{
			this.BusinessCode.Value = value;
		}
	}
	public string BusiClass
	{
		set
		{
			this.BusinessClass.Value = value;
		}
	}
	public string URLParameter
	{
		set
		{
			this.hfldURLParameter.Value = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			BasicConfigService basicConfigService = new BasicConfigService();
			this.hfldPwd.Value = basicConfigService.GetValue("TheDeletePwd");
			if (this.Type == 0)
			{
				this.btnViewWF.Style["display"] = "none";
				this.btnWFPrint.Style["display"] = "none";
				return;
			}
			int arg_71_0 = this.ISdel;
			this.btnViewWF.Style["display"] = "";
			this.btnWFPrint.Style["display"] = "";
		}
	}
}


