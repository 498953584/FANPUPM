using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProductValueFrame : PageBase, IRequiresSessionState
	{

		public int Type
		{
			get
			{
				object obj = this.ViewState["TYPE"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
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
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			try
			{
				this.Type = int.Parse(base.Request["t"].ToString().Trim());
			}
			catch
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			if (!this.Page.IsPostBack)
			{
				this.Data_Bind(this.Type);
				this.UCProjectList.UserCode = base.UserCode;
				this.UCProjectList.TargetFrame = "InfoList";
			}
			if (this.UCProjectList.SubPrjUrl != "")
			{
				if (this.UCProjectList.SubPrjUrl.IndexOf("?") > 0)
				{
					this.Page.RegisterStartupScript("", string.Concat(new object[]
					{
						"<script language=\"javascript\">InfoList.location = '",
						this.UCProjectList.SubPrjUrl,
						"&PrjCode=&PrjGuid=",
						Guid.Empty,
						"&pc=",
						Guid.Empty,
						"&pn=&cc=';</script>"
					}));
					return;
				}
				this.Page.RegisterStartupScript("", string.Concat(new object[]
				{
					"<script language=\"javascript\">InfoList.location = '",
					this.UCProjectList.SubPrjUrl,
					"?PrjCode=&PrjGuid=",
					Guid.Empty,
					"&pc=",
					Guid.Empty,
					"&pn=&cc=';</script>"
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
		private void Data_Bind(int intType)
		{
			switch (intType)
			{
			case 1:
				this.LblMsg.Text = "产值计量上报";
				this.UCProjectList.SubPrjUrl = "ProductValueMonthReport.aspx?t=1";
				return;
			case 2:
				this.LblMsg.Text = "产值计量上报审核";
				this.UCProjectList.SubPrjUrl = "ProductValueMonthReport.aspx?t=2";
				return;
			case 3:
				this.LblMsg.Text = "产值统计";
				this.UCProjectList.SubPrjUrl = "ProductValueMonthStat.aspx";
				return;
			case 4:
			case 5:
				break;
			case 6:
				this.LblMsg.Text = "产值计划";
				this.UCProjectList.SubPrjUrl = "ProductValuePlanFrm.aspx?t=1";
				return;
			case 7:
				this.LblMsg.Text = "产值计划审核";
				this.UCProjectList.SubPrjUrl = "ProductValuePlanFrm.aspx?t=2";
				break;
			default:
				return;
			}
		}
	}


