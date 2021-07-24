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
	public partial class ProgressImplementEvaluate : NBasePage, System.Web.SessionState.IRequiresSessionState
	{
		protected string mainId;
		protected string AppraiseId;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request.Params["mainId"] == null)
				{
					return;
				}
				this.mainId = base.Request.Params["mainId"].ToString();
				this.ViewState["MAINID"] = this.mainId;
				this.AppraiseId = ProgressImplementAction.GetNewAppraiseId();
				this.ViewState["APPRAISEID"] = this.AppraiseId;
				this.hidMainId.Value = this.mainId;
				this.BindData();
			}
			this.AppraiseId = this.ViewState["APPRAISEID"].ToString();
			this.mainId = this.ViewState["MAINID"].ToString();
		}
		private void BindData()
		{
			UserInfo currentUserInfo = userManageDb.GetCurrentUserInfo();
			ProgressImplementAction progressImplementAction = new ProgressImplementAction();
			ProgressEvaluateInfo evaluateInfo = progressImplementAction.GetEvaluateInfo(this.AppraiseId);
			this.txtAppraise.Text = evaluateInfo.Appraise;
			this.txtAppraiseTime.Text = evaluateInfo.AppraiseTime.ToShortDateString();
			this.txtAppraiseTime.ReadOnly = true;
			if (currentUserInfo != null)
			{
				this.txtAppraisePeople.Text = currentUserInfo.UserName;
				return;
			}
			this.js.Text = "alert(\"登录时间过期,请重新登陆!\");window.close();";
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
			ProgressEvaluateInfo progressEvaluateInfo = new ProgressEvaluateInfo();
			bool isEdit = this.ViewState["ISNEW"] != null && !Convert.ToBoolean(this.ViewState["ISNEW"].ToString().ToLower());
			progressEvaluateInfo.AppraisePeople = this.txtAppraisePeople.Text;
			progressEvaluateInfo.AppraiseTime = DateTime.Parse(this.txtAppraiseTime.Text);
			progressEvaluateInfo.ParentMainID = this.mainId;
			progressEvaluateInfo.MainID = this.AppraiseId;
			progressEvaluateInfo.Appraise = this.txtAppraise.Text;
			if (ProgressImplementAction.AddEvaluate(progressEvaluateInfo, isEdit))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_progressimplementitem' });");
			}
			else
			{
				base.RegisterScript("top.ui.alert('保存失败');");
			}
			this.BindData();
		}
	}


