using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class CorpSearch : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			base.Response.Cache.SetNoStore();
			base.Response.Clear();
			if (!this.Page.IsPostBack)
			{
				this.hdnmyuser.Value = base.UserCode;
				this.DDL_CorpKind.DataTextField = "CodeName";
				this.DDL_CorpKind.DataValueField = "CodeID";
				this.DDL_CorpKind.DataSource = CodingAction.getTypebyID("1");
				this.DDL_CorpKind.DataBind();
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


