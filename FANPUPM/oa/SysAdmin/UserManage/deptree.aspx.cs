using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
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
			if (!this.Page.IsPostBack)
			{
				CreatDepTree creatDepTree = new CreatDepTree(this.tvDept.Nodes);
				creatDepTree.EnabledLink = true;
				creatDepTree.Target = "rFrame";
				creatDepTree.NavigationURL = base.Request.QueryString["DestUrl"].ToString();
				userManageDb userManageDb = new userManageDb();
				string a = userManageDb.manageDept(this.Session["yhdm"].ToString()).Trim();
				if (a == "1")
				{
					creatDepTree.BuildTreeView(this.Page.Session["yhdm"].ToString(), 0);
				}
				int arg_BA_0 = this.tvDept.Nodes.Count;
			}
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


