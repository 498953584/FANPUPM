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
	public partial class ProgressImplementReport : NBasePage, IRequiresSessionState
	{
		protected string PlanId;
		protected string MainId;
		protected bool isNew;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["planId"] == null)
				{
					return;
				}
				this.PlanId = base.Request.Params["planId"].ToString();
				this.ViewState["PLANID"] = this.PlanId;
				if (base.Request.Params["MainId"] != null)
				{
					this.MainId = base.Request.Params["MainId"].ToString();
					this.ViewState["MAINID"] = this.MainId;
					this.BindData();
				}
				else
				{
					this.MainId = ProgressImplementAction.GetNewImplementId();
					this.ViewState["MAINID"] = this.MainId;
					this.isNew = true;
					this.ViewState["ISNEW"] = this.isNew.ToString();
					this.hdnProgressGuid.Value = Guid.NewGuid().ToString();
				}
				this.hidMainID.Value = this.MainId;
				this.FileLink1.MID = 1724;
				this.FileLink1.Type = 1;
				this.FileLink1.FID = this.hdnProgressGuid.Value.Trim();
			}
			this.MainId = this.ViewState["MAINID"].ToString();
			this.PlanId = this.ViewState["PLANID"].ToString();
			if (this.ViewState["ISNEW"] != null)
			{
				this.isNew = Convert.ToBoolean(this.ViewState["ISNEW"].ToString().ToLower());
			}
		}
		private void BindData()
		{
			ProgressImplementAction progressImplementAction = new ProgressImplementAction();
			ProgressImplementInfo implementInfo = progressImplementAction.GetImplementInfo(this.MainId);
			this.txtActualizeCircs.Text = implementInfo.ActualizeCircs;
			this.txtExaminePeople.Text = implementInfo.ExaminePeople;
			this.txtExamineTime.Text = implementInfo.ExamineTime.ToShortDateString();
			this.txtFillPeople.Text = implementInfo.FillPeople;
			this.txtFillTime.Text = implementInfo.FillTime.ToShortDateString();
			this.txtRemark.Text = implementInfo.Remark;
			this.hdnProgressGuid.Value = implementInfo.ProgressGuid;
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
			ProgressImplementInfo progressImplementInfo = new ProgressImplementInfo();
			progressImplementInfo.ActualizeCircs = this.txtActualizeCircs.Text;
			progressImplementInfo.ExaminePeople = this.txtExaminePeople.Text;
			if (this.txtExamineTime.Text.Trim() != "")
			{
				progressImplementInfo.ExamineTime = DateTime.Parse(this.txtExamineTime.Text);
			}
			progressImplementInfo.FillPeople = this.txtFillPeople.Text;
			if (this.txtFillTime.Text.Trim() != "")
			{
				progressImplementInfo.FillTime = DateTime.Parse(this.txtFillTime.Text);
			}
			progressImplementInfo.PlanId = this.PlanId;
			progressImplementInfo.MainID = this.MainId;
			progressImplementInfo.Remark = this.txtRemark.Text;
			progressImplementInfo.ProgressGuid = this.hdnProgressGuid.Value.Trim();
			if (this.isNew)
			{
				if (ProgressImplementAction.SaveImplement(progressImplementInfo))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_progressimplementitem' });");
				}
				else
				{
					base.RegisterScript("top.ui.alert('保存失败');");
				}
			}
			else
			{
				if (ProgressImplementAction.UpdateImplement(progressImplementInfo))
				{
					base.RegisterScript("top.ui.tabSuccess({ parentName: '_progressimplementitem' });");
				}
				else
				{
					base.RegisterScript("top.ui.alert('保存失败');");
				}
			}
			this.BindData();
		}
	}


