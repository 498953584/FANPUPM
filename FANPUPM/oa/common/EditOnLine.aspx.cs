using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class EditOnLine : BasePage, IRequiresSessionState
	{
		public string docid;

		public string filename
		{
			get
			{
				object obj = this.ViewState["filename"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["filename"] = value;
			}
		}
		public string FilePath
		{
			get
			{
				object obj = this.ViewState["FilePath"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["FilePath"] = value;
			}
		}
		public string URL
		{
			get
			{
				object obj = this.ViewState["URL"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["URL"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.URL = base.Request.QueryString["filepath"];
				if (this.URL != null && this.URL.Length != 0)
				{
					this.Session["uploadpath"] = this.URL.Substring(0, this.URL.LastIndexOf("/"));
					this.FilePath = base.Server.MapPath(this.URL);
					this.filename = this.FilePath.Substring(this.FilePath.LastIndexOf('\\') + 1);
					this.docid = "";
				}
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.ID = "myForm";
		}
	}


