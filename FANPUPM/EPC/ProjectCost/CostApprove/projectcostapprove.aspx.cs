using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProjectCostApprove : PageBase, IRequiresSessionState
	{
		protected string Type
		{
			get
			{
				object obj = this.ViewState["TYPE"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["TYPE"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["t"] == null)
			{
				this.RegisterStartupScript("", "<script>alert('参数错误!');</script>");
				return;
			}
			this.Type = base.Request["t"].ToString();
			if (!this.Page.IsPostBack)
			{
				this.UCProjectList.UserCode = base.UserCode;
				if (this.Type == "Approve")
				{
					this.UCProjectList.SubPrjUrl = "../ProjectCost/CostApprove/CostApproveGather.aspx";
					this.Page.RegisterStartupScript("", string.Concat(new object[]
					{
						"<script language=\"javascript\">InfoList.location = 'CostApproveGather.aspx?PrjCode=&PrjGuid=",
						Guid.Empty,
						"&pc=",
						Guid.Empty,
						"&pn=&cc=';</script>"
					}));
				}
				if (this.Type == "MonthSta")
				{
					this.UCProjectList.SubPrjUrl = "../ProjectCost/CostApprove/MonthCostApprove.aspx";
					this.Page.RegisterStartupScript("", string.Concat(new object[]
					{
						"<script language=\"javascript\">InfoList.location = 'MonthCostApprove.aspx?PrjCode=&PrjGuid=",
						Guid.Empty,
						"&pc=",
						Guid.Empty,
						"&pn=&cc=';</script>"
					}));
				}
				this.UCProjectList.TargetFrame = "InfoList";
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


