using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class PickCorpSimple : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack && base.Request["ts"] != null && base.Request["ts"].Trim() != "")
			{
				this.FraCorpType.Attributes["src"] = "CorpTypeTree.aspx?ts=" + base.Request["ts"].Trim() + "&w=1";
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


