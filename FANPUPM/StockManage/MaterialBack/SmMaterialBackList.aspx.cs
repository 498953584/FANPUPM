using ASP;
using cn.justwin.BLL;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_SmMaterialBackList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.hdnProname.Value = base.Request.QueryString["pn"];
			this.hndProCode.Value = base.Request.QueryString["PrjGuid"];
			this.lblProjectName.Text = base.Request.QueryString["pn"];
		}
	}
}


