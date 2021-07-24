using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class TaskAlterFrame : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UProjectList1.UserCode = base.UserCode;
				this.UProjectList1.SubPrjUrl = "../ProjectLayout/ProjectDistribute/WBSQuery2.aspx";
				this.UProjectList1.TargetFrame = "InfoList";
				this.Page.RegisterStartupScript("", "<script>InfoList.location.href ='WBSQuery2.aspx?pc=" + Guid.Empty + "&pn=';</script>");
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


