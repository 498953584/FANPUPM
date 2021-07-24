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
		protected bool isView;
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
				if (base.Request.Params["target"] != null)
				{
					this.SetControlEnabled();
				}
			}
			this.planId = this.ViewState["PLANID"].ToString();
		}
		private void SetControlEnabled()
		{
			this.btnSave.Visible = false;
			this.txtRemark.Enabled = false;
			this.txtPlanCode.Enabled = false;
			this.txtPanelView.Enabled = false;
			this.txtDeclareUnitView.Enabled = false;
			this.txtVettingCommitteeView.Enabled = false;
			this.ddlAuditState.Enabled = false;
		}
		private void BindDropDownList()
		{
			this.ddlAuditState.Items.Add(new System.Web.UI.WebControls.ListItem("通过", "2"));
			this.ddlAuditState.Items.Add(new System.Web.UI.WebControls.ListItem("不通过", "1"));
			this.ddlAuditState.Items.Add(new System.Web.UI.WebControls.ListItem("待审", "0"));
		}
		private void BindData()
		{
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			ProgressPlanInfo onePlanInfo = progressPlanAction.GetOnePlanInfo(this.planId);
			this.txtDeclareUnitView.Text = onePlanInfo.DeclareUnitView;
			this.txtPanelView.Text = onePlanInfo.PanelView;
			this.txtPlanCode.Text = onePlanInfo.PlanCode;
			this.txtRemark.Text = onePlanInfo.Remark;
			this.txtVettingCommitteeView.Text = onePlanInfo.VettingCommitteeView;
			if (onePlanInfo.AuditState < 3)
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
			if (progressPlanAction.PpmAuditPlan(new ProgressPlanInfo
			{
				PlanId = this.planId,
				DeclareUnitView = this.txtDeclareUnitView.Text,
				PanelView = this.txtPanelView.Text,
				PlanCode = this.txtPlanCode.Text,
				Remark = this.txtRemark.Text,
				VettingCommitteeView = this.txtVettingCommitteeView.Text,
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


