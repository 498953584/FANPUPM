using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ProjectTree : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.UProjectList1.UserCode = base.UserCode;
				this.UProjectList1.TargetFrame = "List";
				if (base.Request["pt"].ToString() != "")
				{
					this.UProjectList1.SubPrjUrl = "../FundManage/FundManageTab.aspx?pt=" + base.Request["pt"].ToString() + "&Type=" + base.Request.QueryString["Type"].ToString();
					return;
				}
				this.UProjectList1.SubPrjUrl = "../FundManage/FundApplicationList.aspx?Type=" + base.Request.QueryString["Type"].ToString();
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


