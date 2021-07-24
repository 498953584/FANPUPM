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
	public partial class InputAuditOne : BasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack && base.Request.Params["MainId"] != null)
			{
				this.hidMainId.Value = base.Request.Params["MainId"].ToString();
				this.BindDropDownList();
				this.BindData();
			}
		}
		private void BindDropDownList()
		{
			this.ddlAuditResult.Items.Add(new System.Web.UI.WebControls.ListItem("审核通过", "1"));
			this.ddlAuditResult.Items.Add(new System.Web.UI.WebControls.ListItem("审核未通过", "0"));
		}
		private void BindData()
		{
			DevelopmentInputAction developmentInputAction = new DevelopmentInputAction();
			DevelopmentInputInfo mainInputInfo = developmentInputAction.GetMainInputInfo(int.Parse(this.hidMainId.Value));
			if (mainInputInfo.AuditPeople == "")
			{
				mainInputInfo.AuditPeople = userManageDb.GetCurrentUserInfo().UserName;
			}
			this.txtAuditView.Text = mainInputInfo.AuditIdea;
			this.txtAuditPeople.Text = mainInputInfo.AuditPeople;
			this.txtAuditDate.Text = mainInputInfo.AuditTime.ToShortDateString();
			if (this.txtAuditDate.Text == "1900-1-1")
			{
				this.txtAuditDate.Text = DateTime.Today.ToShortDateString();
			}
			this.ddlAuditResult.SelectedValue = (mainInputInfo.AuditResult ? "1" : "0");
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
			if (DevelopmentInputAction.AuditInputInfo(new DevelopmentInputInfo
			{
				AuditPeople = this.txtAuditPeople.Text,
				AuditResult = this.ddlAuditResult.SelectedValue == "1",
				AuditTime = DateTime.Parse(this.txtAuditDate.Text),
				AuditIdea = this.txtAuditView.Text,
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


