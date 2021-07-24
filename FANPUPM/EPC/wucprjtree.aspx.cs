using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class WUCPrjTree1 : PageBase, IRequiresSessionState
	{
		public string Type;

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
			WUCPrjTree wUCPrjTree = (WUCPrjTree)this.Page.FindControl("WUCPrjTree3");
			this.GetPageValue();
			wUCPrjTree.UserCode = base.UserCode;
			wUCPrjTree.SubPrjUrl = this.Url;
			wUCPrjTree.TargetFrame = "mainFrame";
			wUCPrjTree.PrjState = this.PrjState;
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
						}
					}
				}
			}
			if (base.Request.QueryString["Url"] != null && base.Request.QueryString["Url"].ToString() != "")
			{
				this.Url = base.Request.QueryString["Url"].ToString();
				this.Url += "?Type=";
				this.Url += this.Type;
				if (base.Request["PrjState"] != null)
				{
					this.PrjState = int.Parse(base.Request.QueryString["PrjState"].ToString());
					this.Url += "&PrjState=";
					this.Url += this.PrjState;
				}
				if (base.Request["TypeID"] != null)
				{
					this.Url = this.Url + "&TypeId=" + base.Request["TypeID"].ToString();
				}
				if (base.Request["Flag"] != null)
				{
					this.Url = this.Url + "&Flag=" + base.Request["Flag"].ToString();
				}
				if (base.Request["conttype"] != null)
				{
					this.Url = this.Url + "&conttype=" + base.Request["conttype"].ToString();
				}
				if (base.Request.QueryString["CA"] != null)
				{
					this.Url = this.Url + "&CA=" + base.Request["CA"].ToString();
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


