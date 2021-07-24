using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
	public partial class ItemDistribute : PageBase, IRequiresSessionState
	{
		public string jw = string.Empty;
		public string height = "1px";
		private string _Type = "";

		protected string ProjectName
		{
			get
			{
				return (string)this.ViewState["PROJECTNAME"];
			}
			set
			{
				this.ViewState["PROJECTNAME"] = value;
			}
		}
		protected Guid ProjectCode
		{
			get
			{
				return (Guid)this.ViewState["PROJECTCODE"];
			}
			set
			{
				this.ViewState["PROJECTCODE"] = value;
			}
		}
		protected ConstructWBSTaskAction bta
		{
			get
			{
				return new ConstructWBSTaskAction();
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["jw"] != null)
			{
				this.jw = base.Request["jw"].ToString().Trim();
				if (this.jw == "1")
				{
					this.height = "170px";
				}
			}
			this.UProjectList1.UserCode = base.UserCode;
			if (!base.IsPostBack)
			{
				this.ViewState["TYPE"] = base.Request.QueryString["Type"].ToString().Trim();
				this._Type = this.ViewState["TYPE"].ToString();
				if (this._Type == "gcfj")
				{
					if (base.Request.QueryString["open"].ToString() == "bind")
					{
						this.UProjectList1.SubPrjUrl = "/ZHY/costframeanalyze.aspx?type=see";
					}
					else
					{
						this.UProjectList1.SubPrjUrl = "/ZHY/wbsqueryReport.aspx";
					}
				}
				this.UProjectList1.TargetFrame = "InfoList";
				return;
			}
			this._Type = this.ViewState["TYPE"].ToString();
			if (base.Request.QueryString["open"].ToString() == "bind")
			{
				this.UProjectList1.SubPrjUrl = "/ZHY/costframeanalyze.aspx?type=see";
			}
			else
			{
				this.UProjectList1.SubPrjUrl = "/ZHY/wbsqueryReport.aspx";
			}
			this.UProjectList1.TargetFrame = "InfoList";
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


