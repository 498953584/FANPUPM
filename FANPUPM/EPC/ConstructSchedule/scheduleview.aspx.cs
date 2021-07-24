using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ScheduleView : PageBase, IRequiresSessionState
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
		public string TaskCode
		{
			get
			{
				return (string)this.ViewState["TASKCODE"];
			}
			set
			{
				this.ViewState["TASKCODE"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["pc"] == null || base.Request["pc"].ToString() == "" || base.Request["tc"] == null)
			{
				this.RegisterStartupScript("错误提示", "<script>alert('参数错误！');</script>");
				return;
			}
			this.ProjectCode = new Guid(base.Request["pc"].ToString());
			this.TaskCode = base.Request["tc"].ToString();
			this.HdnProjectName.Value = base.Request["pn"].ToString();
			if (!this.Page.IsPostBack)
			{
				this.UCSchedulePlan.ProjectCode = this.ProjectCode;
				this.HdnProjectCode.Value = this.ProjectCode.ToString();
				this.UCSchedulePlan.TaskCode = this.TaskCode;
				this.UCSchedulePlan.TaskList_Bind("View");
				this.UCTaskRelation.ProjectCode = this.ProjectCode;
				this.UCTaskRelation.TaskCode = this.TaskCode;
				this.UCTaskRelation.Bind("View");
			}
			this.BtnResInfo.Attributes["onclick"] = string.Concat(new object[]
			{
				"javascript:return OpenScheduleResource('",
				this.ProjectCode,
				"','",
				this.TaskCode,
				"');"
			});
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void Button1_ServerClick(object sender, EventArgs e)
		{
		}
	}


