using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ZHY_BindBudget : PageBase, IRequiresSessionState
	{
		public string prjguid = string.Empty;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.prjguid = base.Request.QueryString["PrjGuid"].ToString();
		}
	}


