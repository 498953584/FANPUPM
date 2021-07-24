using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ConstructTaskBill : PageBase, IRequiresSessionState
	{

		protected Guid ProjectCode
		{
			get
			{
				object obj = this.ViewState["PROJECTCODE"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.UCProjectList.SubPrjUrl = "../ProjectCost/Construct/TaskBillList.aspx?tc=" + string.Empty + "&t=1";
				this.UCProjectList.UserCode = base.UserCode;
				this.UCProjectList.TargetFrame = "InfoList";
				this.Page.RegisterStartupScript("Material", string.Concat(new object[]
				{
					"<script language=\"javascript\">InfoList.location = 'TaskBillList.aspx?pc=",
					this.ProjectCode,
					"&tc=",
					string.Empty,
					"&t=1';</script>"
				}));
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


