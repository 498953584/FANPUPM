using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SchedulePlan : PageBase, IRequiresSessionState
	{

		public Guid ProjectCode
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
		public string ProjectName
		{
			get
			{
				object obj = this.ViewState["PROJECTNAME"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["PROJECTNAME"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["pc"] == null || base.Request["pn"] == null || base.Request["t"] == null)
				{
					this.JS.Text = "alert('参数有误！');";
					return;
				}
				this.ProjectCode = new Guid(base.Request["pc"].ToString());
				this.ProjectName = base.Request["pn"].ToString();
				this.HdnType.Value = base.Request["t"].ToString();
				this.HdnProjectName.Value = this.ProjectName;
				this.LblProjectName.Text = this.ProjectName;
				this.UCTaskTreeTable.ProjectCode = this.ProjectCode;
				this.UCTaskTreeTable.Cols = "1,3,4,5,6";
				this.UCTaskTreeTable.CreateTable();
				if (base.Request["t"].ToLower() == "resource")
				{
					this.Page.RegisterStartupScript("", "<script language=\"javascript\">InfoList.location = '../ProjectLayout/ProjectDistribute/ResourceDistribute.aspx?pc=" + Guid.Empty + "&tc=&pn=&wbs=1';</script>");
				}
				if (base.Request["t"].ToLower() == "plan")
				{
					this.Page.RegisterStartupScript("", "<script language=\"javascript\">InfoList.location = '../../EPC/UserControl1/webTreeTS.aspx?fj=fxrw';</script>");
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
	}


