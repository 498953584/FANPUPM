using ASP;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.web;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class PickResource : PageBase, IRequiresSessionState
	{

		protected new string UserCode
		{
			get
			{
				return this.Session["yhdm"].ToString();
			}
		}
		protected Guid OpSession
		{
			get
			{
				object obj = this.ViewState["OPSESSION"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["OPSESSION"] = value;
			}
		}
		protected static ResourceItemAction ResItemAct
		{
			get
			{
				return new ResourceItemAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["ses"] == null || base.Request["rs"] == null)
				{
					this.js.Text = "alert('参数错误！');window.returnValue = false;window.close();";
					return;
				}
				this.OpSession = new Guid(base.Request["ses"]);
				string text = base.Request["rs"];
				string a;
				if ((a = text) != null)
				{
					if (a == "1")
					{
						this.FraHuman.Attributes["src"] = "ResourceTree.aspx?t=1&ses=" + this.OpSession.ToString();
						this.TabResource.SelectedIndex = 0;
						this.TabResource.Enabled = false;
						return;
					}
					if (a == "2")
					{
						this.FraMaterial.Attributes["src"] = "ResourceTree.aspx?t=2&ses=" + this.OpSession.ToString();
						this.TabResource.SelectedIndex = 1;
						this.TabResource.Enabled = false;
						return;
					}
					if (a == "3")
					{
						this.FraMachine.Attributes["src"] = "ResourceTree.aspx?t=3&ses=" + this.OpSession.ToString();
						this.TabResource.SelectedIndex = 2;
						this.TabResource.Enabled = false;
						return;
					}
				}
				this.FraHuman.Attributes["src"] = "ResourceTree.aspx?t=1&ses=" + this.OpSession.ToString();
				this.FraMaterial.Attributes["src"] = "ResourceTree.aspx?t=2&ses=" + this.OpSession.ToString();
				this.FraMachine.Attributes["src"] = "ResourceTree.aspx?t=3&ses=" + this.OpSession.ToString();
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
		private void Page_CustomInit()
		{
		}
		protected void BtnCancel_Click(object sender, EventArgs e)
		{
			PickResource.ResItemAct.UndoModifyTempResource(this.OpSession, this.UserCode);
			this.js.Text = " window.returnValue = false; window.close();";
		}
	}


