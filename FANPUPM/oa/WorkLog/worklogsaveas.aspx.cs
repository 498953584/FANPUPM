using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class workLogSaveAs : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				DataTable dataSource = new DataTable();
				workLogDb workLogDb = new workLogDb();
				string yhdm = this.Page.Session["yhdm"].ToString();
				dataSource = workLogDb.getTable(yhdm);
				this.dg.DataSource = dataSource;
				this.dg.DataBind();
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


