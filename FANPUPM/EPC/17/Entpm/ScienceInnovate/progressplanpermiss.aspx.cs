using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class ProgressPlanPermiss : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected string planId;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["planId"] == null)
				{
					return;
				}
				this.ViewState["PLANID"] = base.Request.Params["planId"].ToString();
				this.planId = this.ViewState["PLANID"].ToString();
				this.BindDropDownList();
				this.BindData();
			}
			this.planId = this.ViewState["PLANID"].ToString();
		}
		private void BindDropDownList()
		{
			this.ddlAuditState.Items.Add(new System.Web.UI.WebControls.ListItem("审核通过", "4"));
			this.ddlAuditState.Items.Add(new System.Web.UI.WebControls.ListItem("审核未通过", "3"));
		}
		private void BindData()
		{
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			ProgressPlanInfo onePlanInfo = progressPlanAction.GetOnePlanInfo(this.planId);
			this.txtPermissionPeople.Text = onePlanInfo.PermissionPeople;
			this.txtPermissionView.Text = onePlanInfo.PermissionView;
			if (onePlanInfo.AuditState > 2)
			{
				this.ddlAuditState.SelectedValue = onePlanInfo.AuditState.ToString();
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
		protected void btnSave_Click(object sender, EventArgs e)
		{
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			if (progressPlanAction.EntAuditPlan(new ProgressPlanInfo
			{
				PlanId = this.planId,
				PermissionPeople = this.txtPermissionPeople.Text,
				PermissionView = this.txtPermissionView.Text,
				AuditState = int.Parse(this.ddlAuditState.SelectedValue)
			}))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_progressplanlist' });");
			}
			else
			{
				base.RegisterScript("top.ui.alert('操作失败！');");
			}
			this.BindData();
		}
	}


