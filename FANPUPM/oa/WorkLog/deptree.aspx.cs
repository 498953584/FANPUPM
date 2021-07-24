using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.sysManage.common;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class depTree : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			new CreatDepTree(this.tv.Nodes)
			{
				EnabledLink = true,
				Target = "rFrame",
				NavigationURL = "userList.aspx"
			}.BuildTreeView(this.Page.Session["yhdm"].ToString(), 0);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
	}


