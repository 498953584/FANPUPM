using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using UserControl;
	public partial class ProgressPlanOneEdit : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected EditDropDownList ddlPlanSort = new EditDropDownList();
		protected string prjCode = "";
		protected string planId = "0";

		protected void Page_Load(object sender, EventArgs e)
		{
			this.BindDropDownList();
			if (this.Page.IsPostBack)
			{
				this.planId = this.ViewState["PLANID"].ToString();
				this.prjCode = this.ViewState["PRJCODE"].ToString();
				return;
			}
			base.Response.Cache.SetNoStore();
			if (base.Request.Params["prjCode"] != null)
			{
				this.ViewState["PRJCODE"] = base.Request.Params["prjCode"].ToString();
				this.prjCode = this.ViewState["PRJCODE"].ToString();
				if (base.Request.Params["planId"] != null)
				{
					this.ViewState["PLANID"] = base.Request.Params["planId"].ToString();
					this.planId = this.ViewState["PLANID"].ToString();
					this.BindData();
				}
				else
				{
					this.ViewState["PLANID"] = ProgressPlanAction.GetNewPlanId();
					this.hidIsSave.Value = "false";
					this.planId = this.ViewState["PLANID"].ToString();
					this.hdnProgressGuid.Value = Guid.NewGuid().ToString();
				}
				this.hidPlanId.Value = this.planId;
				this.hidPrjCode.Value = this.prjCode;
				this.FileLink1.MID = 1747;
				this.FileLink1.Type = 1;
				this.FileLink1.FID = this.hdnProgressGuid.Value.Trim();
				return;
			}
		}
		private void BindDropDownList()
		{
			this.ddlPlanSort.ID = "ddlPlanSort";
			foreach (DataRow dataRow in ProgressPlanAction.GetPlanSortList().Rows)
			{
				this.ddlPlanSort.Items.Add(new System.Web.UI.WebControls.ListItem(dataRow[0].ToString(), dataRow[0].ToString()));
			}
			this.lbPlanSort.Controls.Add(this.ddlPlanSort);
		}
		private void BindData()
		{
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			ProgressPlanInfo onePlanInfo = progressPlanAction.GetOnePlanInfo(this.planId);
			this.txtResultUint.Text = onePlanInfo.ResultsHolders;
			this.txtCompletedDate.Text = onePlanInfo.CompletedDate.ToShortDateString();
			this.txtApplication.Text = onePlanInfo.ApplicationName;
			this.txtPlanCode.Text = onePlanInfo.PlanCode;
			this.txtResultName.Text = onePlanInfo.ResultsName;
			this.txtNotes.Text = onePlanInfo.Remark;
			this.txtPlanSort.Text = ((onePlanInfo.PlanSort != null) ? onePlanInfo.PlanSort.Trim() : "");
			this.hdnProgressGuid.Value = onePlanInfo.ProgressGuid;
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
			ProgressPlanInfo progressPlanInfo = new ProgressPlanInfo();
			ProgressPlanAction progressPlanAction = new ProgressPlanAction();
			progressPlanInfo.PlanCode = this.txtPlanCode.Text;
			progressPlanInfo.PlanId = this.planId;
			progressPlanInfo.ApplicationName = this.txtApplication.Text;
			if (this.txtCompletedDate.Text.Trim() != "")
			{
				progressPlanInfo.CompletedDate = DateTime.Parse(this.txtCompletedDate.Text);
			}
			progressPlanInfo.ResultsName = this.txtResultName.Text;
			progressPlanInfo.ResultsHolders = this.txtResultUint.Text;
			progressPlanInfo.PrjCode = this.prjCode;
			progressPlanInfo.PlanSort = ((EditDropDownList)this.lbPlanSort.FindControl("ddlPlanSort")).Text;
			progressPlanInfo.PlanSort = this.txtPlanSort.Text.Trim();
			progressPlanInfo.Remark = this.txtNotes.Text.Trim().ToString();
			progressPlanInfo.ProgressGuid = this.hdnProgressGuid.Value.Trim();
			if (progressPlanAction.UpdatePlan(progressPlanInfo))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_progressplanlist' });");
				return;
			}
			this.js.Text = "alert(\"操作失败！\");";
		}
	}


