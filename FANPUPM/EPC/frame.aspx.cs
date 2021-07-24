using ASP;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
	public partial class Frame : Page, IRequiresSessionState
	{
		public string Type;
		public string Levels;
		public string _R = string.Empty;

		private PageType P_Type
		{
			get
			{
				return (PageType)this.ViewState["P_Type"];
			}
			set
			{
				this.ViewState["P_Type"] = value;
			}
		}
		public string Url
		{
			get
			{
				return this.ViewState["Url"].ToString();
			}
			set
			{
				this.ViewState["Url"] = value;
			}
		}
		public int PrjState
		{
			get
			{
				if (this.ViewState["PrjState"] != null)
				{
					return int.Parse(this.ViewState["PrjState"].ToString());
				}
				return 0;
			}
			set
			{
				this.ViewState["PrjState"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			this.GetPageValue();
			this.DataBind();
		}
		private void GetPageValue()
		{
			if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"].ToString() != "")
			{
				this.P_Type = (PageType)Enum.Parse(typeof(PageType), base.Request.QueryString["Type"].ToString());
				if (this.P_Type == PageType.Edit)
				{
					this.Type = "Edit";
				}
				else
				{
					if (this.P_Type == PageType.List)
					{
						this.Type = "List";
					}
					else
					{
						if (this.P_Type == PageType.ShenHe)
						{
							this.Type = "ShenHe";
						}
						else
						{
							if (this.P_Type == PageType.Borrow)
							{
								this.Type = "Borrow";
							}
							else
							{
								if (this.P_Type == PageType.Auditing)
								{
									this.Type = "Auditing";
								}
							}
						}
					}
				}
			}
			if (base.Request.QueryString["Url"] != null && base.Request.QueryString["Url"].ToString() != "")
			{
				this.Url = base.Request.QueryString["Url"].ToString();
			}
			if (base.Request["PrjState"] != null)
			{
				this.PrjState = int.Parse(base.Request.QueryString["PrjState"].ToString());
			}
			if (base.Request.Params["SchType"] != null)
			{
				this.Url = this.Url + "?SchType=" + base.Request.Params["SchType"].ToString();
			}
			if (base.Request["Levels"] != null)
			{
				this.Levels = base.Request["Levels"].ToString();
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


