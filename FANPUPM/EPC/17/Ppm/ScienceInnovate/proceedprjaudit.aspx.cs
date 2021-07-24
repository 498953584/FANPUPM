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
	public partial class ProceedPrjAudit : BasePage, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["MainId"] != null)
				{
					this.hidMainId.Value = base.Request.Params["MainId"].ToString();
					this.BindDropDownList();
					this.BindData();
					return;
				}
				this.btnSave.Enabled = false;
			}
		}
		private void BindDropDownList()
		{
			this.ddlPPMAuditResult.Items.Add(new ListItem("待审", "0"));
			this.ddlPPMAuditResult.Items.Add(new ListItem("未通过", "1"));
			this.ddlPPMAuditResult.Items.Add(new ListItem("通过", "2"));
		}
		private void BindData()
		{
			ProgressProceedAction progressProceedAction = new ProgressProceedAction();
			ProgressProceedInfo proceInfo = progressProceedAction.GetProceInfo(int.Parse(this.hidMainId.Value));
			this.txtPPMSerialNumber.Text = proceInfo.PPMSerialNumber;
			this.txtPPMRemark.Text = proceInfo.PPMRemark;
			this.txtPPMGroupIdea.Text = proceInfo.PPMGroupIdea;
			this.txtPPMDeclareUnitIdea.Text = proceInfo.PPMDeclareUnitIdea;
			this.txtPPMCommitteeIdea.Text = proceInfo.PPMCommitteeIdea;
			this.ddlPPMAuditResult.SelectedValue = proceInfo.PPMAuditResult.ToString();
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
			if (ProgressProceedAction.PpmAuditProce(new ProgressProceedInfo
			{
				PPMSerialNumber = this.txtPPMSerialNumber.Text,
				PPMRemark = this.txtPPMRemark.Text,
				PPMGroupIdea = this.txtPPMGroupIdea.Text,
				PPMDeclareUnitIdea = this.txtPPMDeclareUnitIdea.Text,
				PPMCommitteeIdea = this.txtPPMCommitteeIdea.Text,
				PPMAuditResult = int.Parse(this.ddlPPMAuditResult.SelectedValue),
				MainID = int.Parse(this.hidMainId.Value)
			}))
			{
				this.js.Text = "alert(\"操作成功！\");";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
	}


