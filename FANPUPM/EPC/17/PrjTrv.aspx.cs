using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class PrjTrv : PageBase, IRequiresSessionState
	{
		public string Type;
		public string Levels;

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
				return int.Parse(this.ViewState["PrjState"].ToString());
			}
			set
			{
				this.ViewState["PrjState"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (this.Session["yhdm"] == null)
			{
				this.Page.RegisterClientScriptBlock("Close", "<script language='javascript'>alert(\"用户身份过期，请重新登录！\");window.parent.parent.close();</script>");
				return;
			}
			this.GetPageValue();
			this.WUCPrjTree2.UserCode = base.UserCode;
			this.WUCPrjTree2.SubPrjUrl = this.Url;
			this.WUCPrjTree2.TargetFrame = "mainFrame";
			this.WUCPrjTree2.PrjState = this.PrjState;
			if (base.Request["Levels"] != null)
			{
				this.WUCPrjTree2.Levels = base.Request["Levels"].ToString();
			}
		}
		private void GetPageValue()
		{
			if (base.Request.QueryString["Type"] != null && base.Request.QueryString["Type"].ToString() != "")
			{
				this.P_Type = (PageType)Enum.Parse(typeof(PageType), base.Request.QueryString["Type"].ToString());
				this.Type = this.P_Type.ToString();
			}
			if (base.Request.QueryString["Url"] != null && base.Request.QueryString["Url"].ToString() != "")
			{
				this.Url = base.Request.QueryString["Url"].ToString();
				if (this.Url.IndexOf("?") >= 0)
				{
					this.Url += "&Type=";
				}
				else
				{
					this.Url += "?Type=";
				}
				this.Url += this.Type;
				if (base.Request.QueryString["PrjState"] != null && base.Request.QueryString["PrjState"].ToString() != "")
				{
					this.PrjState = int.Parse(base.Request.QueryString["PrjState"].ToString());
					this.Url += "&PrjState=";
					this.Url += this.PrjState;
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
		}
	}


