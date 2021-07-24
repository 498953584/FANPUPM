using ASP;
using cn.justwin.BLL;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class CorpAddressList : NBasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request.QueryString["flag"] == null)
			{
				this.DeptList.Attributes["src"] = "DeptList.aspx?flag=Manage";
				this.LinkmanList.Attributes["src"] = "LinkmanList.aspx";
				return;
			}
			this.DeptList.Attributes["src"] = "DeptList.aspx?flag=Search";
			this.LinkmanList.Attributes["src"] = "LinkmanSearch.aspx";
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


