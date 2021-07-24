using ASP;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class EntStandardFrame : System.Web.UI.Page, System.Web.SessionState.IRequiresSessionState
	{
		public string type
		{
			get
			{
				return this.ViewState["type"].ToString();
			}
			set
			{
				this.ViewState["type"] = value;
			}
		}
		public string PrjCode
		{
			get
			{
				return this.ViewState["PrjCode"].ToString();
			}
			set
			{
				this.ViewState["PrjCode"] = value;
			}
		}
		public string Levels
		{
			get
			{
				return this.ViewState["Levels"].ToString();
			}
			set
			{
				this.ViewState["Levels"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				if (base.Request["type"] != null)
				{
					this.type = base.Request.QueryString["type"].ToString().Trim();
				}
				if (base.Request["PrjCode"] != null)
				{
					this.PrjCode = base.Request.QueryString["PrjCode"].ToString().Trim();
				}
				else
				{
					this.PrjCode = "";
				}
				if (base.Request["Levels"] != null)
				{
					this.Levels = base.Request.QueryString["Levels"].ToString().Trim();
				}
				else
				{
					this.Levels = "";
				}
				this.DataBind();
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


