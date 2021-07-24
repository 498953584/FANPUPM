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
	public partial class DocumentEdit : NBasePage, IRequiresSessionState
	{

		protected string DocumentCode
		{
			get
			{
				object obj = this.ViewState["DocumentCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["DocumentCode"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["dc"] == null)
				{
					this.JS.Text = "alert('参数错误！');";
				}
				else
				{
					this.DocumentCode = base.Request["dc"];
				}
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
				this.RestoreDocumentInfo(new Guid(this.DocumentCode));
				this.BtnAnnex.Attributes["onclick"] = "UpFile();return false;";
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
		private void RestoreDocumentInfo(Guid docCode)
		{
			ProjectDocumentInfo oneDocumentInfo = ProjectOverAction.GetOneDocumentInfo(docCode);
			this.HdnDocCode.Value = oneDocumentInfo.DocumentCode.ToString();
			this.HdnProjectCode.Value = oneDocumentInfo.ProjectCode.ToString();
			this.TxtDocumentName.Text = oneDocumentInfo.DocumentName;
			this.TxtFileCode.Text = oneDocumentInfo.FileCode;
			this.TxtRollCode.Text = oneDocumentInfo.RollCode;
			this.TxtStorage.Text = oneDocumentInfo.Storage;
			this.TxtRemark.Text = oneDocumentInfo.Remark;
			this.DDTClass.SelectedValue = oneDocumentInfo.ClassCode.ToString();
		}
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			if (ProjectOverAction.UpdDocument(new ProjectDocumentInfo
			{
				DocumentCode = new Guid(this.DocumentCode),
				ProjectCode = new Guid(this.HdnProjectCode.Value),
				ClassCode = Convert.ToInt32(this.DDTClass.SelectedValue.Trim()),
				DocumentName = this.TxtDocumentName.Text.Trim(),
				FileCode = this.TxtFileCode.Text.Trim(),
				RollCode = this.TxtRollCode.Text.Trim(),
				Storage = this.TxtStorage.Text.Trim(),
				Remark = this.TxtRemark.Text.Trim(),
				State = 0,
				AddDate = DateTime.Today
			}) == 1)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_documentlist' });");
				return;
			}
			base.RegisterScript("top.ui.alert('添加失败！');");
		}
		protected void BtnAnnex_Click(object sender, EventArgs e)
		{
		}
	}


