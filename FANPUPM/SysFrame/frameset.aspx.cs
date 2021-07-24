using ASP;
//using com.jwsoft.common.EncryptDogA;
using System;
using System.Configuration;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class FrameSet : Page, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (HttpContext.Current.Session["yhdm"] == null)
			{
				this.Page.Response.Redirect("/");
			}
            //EncryptDogA encryptDogA = new EncryptDogA();
            //encryptDogA.IsAuthorization();
			if (!base.IsPostBack)
			{
				this.SetHomePageTitle();
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
		protected void SetHomePageTitle()
		{
			string title = "建文工程项目管理系统" + ConfigurationManager.AppSettings["HomePageTitle"].ToString();
			base.Title = title;
		}
	}


