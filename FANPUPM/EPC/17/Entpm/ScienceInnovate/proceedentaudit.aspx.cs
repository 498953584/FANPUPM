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
	public partial class ProceedEntAudit : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["MainId"] != null)
				{
					this.hidMainId.Value = base.Request.Params["MainId"].ToString();
					this.BindDropDown();
					this.BindData();
					return;
				}
				this.btnSave.Enabled = false;
			}
		}
		private void BindDropDown()
		{
			this.ddlEntAuditResult.Items.Add(new System.Web.UI.WebControls.ListItem("未通过", "0"));
			this.ddlEntAuditResult.Items.Add(new System.Web.UI.WebControls.ListItem("通过", "1"));
		}
		private void BindData()
		{
			ProgressProceedAction progressProceedAction = new ProgressProceedAction();
			ProgressProceedInfo proceInfo = progressProceedAction.GetProceInfo(int.Parse(this.hidMainId.Value));
			this.txtEntAuditDate.Text = proceInfo.EntAuditDate.ToShortDateString();
			this.txtEntAuditIdea.Text = proceInfo.EntAuditIdea;
			this.txtEntAuditPeople.Text = ((proceInfo.EntAuditPeople == "") ? userManageDb.GetCurrentUserInfo().UserName : proceInfo.EntAuditPeople);
			this.ddlEntAuditResult.SelectedValue = (proceInfo.EntAuditResult ? "1" : "0");
			this.txtAuditValue.Text = proceInfo.AuditValue.ToString();
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
			if (ProgressProceedAction.EntpmAuditProce(new ProgressProceedInfo
			{
				EntAuditDate = DateTime.Parse(this.txtEntAuditDate.Text),
				EntAuditIdea = this.txtEntAuditIdea.Text,
				EntAuditPeople = this.txtEntAuditPeople.Text,
				EntAuditResult = this.ddlEntAuditResult.SelectedValue == "1",
				MainID = int.Parse(this.hidMainId.Value),
				AuditValue = decimal.Parse(this.txtAuditValue.Text)
			}))
			{
				this.js.Text = "alert(\"操作成功！\");window.returnValue=true;window.close();";
			}
			else
			{
				this.js.Text = "alert(\"操作失败！\");";
			}
			this.BindData();
		}
	}


