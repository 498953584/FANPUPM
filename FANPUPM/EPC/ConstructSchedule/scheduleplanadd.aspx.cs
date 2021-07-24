using ASP;
using com.jwsoft.common.baseclass;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class SchedulePlanAdd : PageBase, IRequiresSessionState
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
				this.UCSchedulePlan.TaskList_Bind("Edit");
				this.UCTaskRelation.ProjectCode = this.ProjectCode;
				this.UCTaskRelation.TaskCode = this.TaskCode;
				this.UCTaskRelation.Bind("Edit");
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
		public int Update()
		{
			if (this.UCSchedulePlan.UpdateTaskList() == 1 && this.UCTaskRelation.Update() == 1)
			{
				return 1;
			}
			return 0;
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (this.UCSchedulePlan.UpdateTaskList() == 1 && this.UCTaskRelation.Update() == 1)
			{
				this.RegisterStartupScript("成功", "<script>alert('保存成功！');</script>");
				return;
			}
			this.RegisterStartupScript("失败", "<script>alert('保存失败！');</script>");
		}
		protected void BtnCancel_Click(object sender, EventArgs e)
		{
			this.UCSchedulePlan.ProjectCode = this.ProjectCode;
			this.HdnProjectCode.Value = this.ProjectCode.ToString();
			this.UCSchedulePlan.TaskCode = this.TaskCode;
			this.UCSchedulePlan.TaskList_Bind("Edit");
			this.UCTaskRelation.ProjectCode = this.ProjectCode;
			this.UCTaskRelation.TaskCode = this.TaskCode;
			this.UCTaskRelation.Bind("Edit");
		}
		private void BtnClose_Click(object sender, EventArgs e)
		{
		}
	}


