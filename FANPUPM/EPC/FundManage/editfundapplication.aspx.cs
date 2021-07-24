using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EditFundApplication : PageBase, IRequiresSessionState
	{
		protected FundApplication fac = new FundApplication();
		
		public Guid PrjCode
		{
			get
			{
				object obj = this.ViewState["PrjCode"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["PrjCode"] = value;
			}
		}
		public Guid GuidFlow
		{
			get
			{
				object obj = this.ViewState["GuidFlow"];
				if (obj != null)
				{
					return (Guid)obj;
				}
				return Guid.Empty;
			}
			set
			{
				this.ViewState["GuidFlow"] = value;
			}
		}
		public string OpType
		{
			get
			{
				object obj = this.ViewState["OpType"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["OpType"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.txtDate.Text = DateTime.Today.Date.ToString();
				this.PrjCode = new Guid(base.Request.QueryString["PrjCode"].ToString());
				this.OpType = base.Request.QueryString["op"].ToString();
				if (this.OpType == "Mod")
				{
					this.GuidFlow = new Guid(base.Request.QueryString["id"].ToString());
					FundAppInfo fundAppInfo = new FundAppInfo();
					fundAppInfo = this.fac.getOnePrjAppRecord(this.GuidFlow);
					this.txtDate.Text = fundAppInfo.FundAppDate.Date.ToString();
					this.txtMoney.Text = fundAppInfo.FundNum.ToString();
					this.txtContent.Text = fundAppInfo.Content;
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
		protected void btnConfirm_Click(object sender, EventArgs e)
		{
			FundAppInfo fundAppInfo = new FundAppInfo();
			fundAppInfo.PrjCode = this.PrjCode;
			fundAppInfo.FundAppUser = this.Session["yhdm"].ToString();
			fundAppInfo.FundAppDate = Convert.ToDateTime(this.txtDate.Text.Trim());
			fundAppInfo.FundNum = Convert.ToDecimal(this.txtMoney.Text.Trim());
			fundAppInfo.Content = this.txtContent.Text.Trim();
			fundAppInfo.GuidFlow = this.GuidFlow;
			if (this.OpType == "Add")
			{
				if (this.fac.insFundRecord(fundAppInfo))
				{
					this.JS.Text = "alert('保存借款申请记录成功！');window.returnValue = true;window.close()";
					return;
				}
			}
			else
			{
				if (this.OpType == "Mod" && this.fac.updFundRecord(fundAppInfo))
				{
					this.JS.Text = "alert('更新借款申请记录成功！');window.returnValue = true;window.close()";
				}
			}
		}
	}


