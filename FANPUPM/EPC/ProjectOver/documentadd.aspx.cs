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
	public partial class DocumentAdd : NBasePage, IRequiresSessionState
	{
		protected string ProjectCode
		{
			get
			{
				object obj = this.ViewState["ProjectCode"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["ProjectCode"] = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["pc"] == null)
				{
					this.JS.Text = "alert('参数错误！');";
				}
				else
				{
					this.ProjectCode = base.Request["pc"];
				}
				if (base.Request["allowAdd"].Trim() == "0")
				{
					this.BtnSave.Enabled = false;
				}
				com.jwsoft.pm.entpm.PageHelper.BindDropDownTree(this.DDTClass, 20, true);
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
		protected void BtnSave_Click(object sender, EventArgs e)
		{
			ProjectDocumentInfo projectDocumentInfo = new ProjectDocumentInfo();
			projectDocumentInfo.DocumentCode = Guid.NewGuid();
			projectDocumentInfo.ProjectCode = new Guid(this.ProjectCode);
			projectDocumentInfo.ClassCode = Convert.ToInt32(this.DDTClass.SelectedValue.Trim());
			projectDocumentInfo.DocumentName = this.TxtDocumentName.Text.Trim();
			projectDocumentInfo.FileCode = this.TxtFileCode.Text.Trim();
			projectDocumentInfo.RollCode = this.TxtRollCode.Text.Trim();
			projectDocumentInfo.Storage = this.TxtStorage.Text.Trim();
			projectDocumentInfo.Remark = this.TxtRemark.Text.Trim();
			projectDocumentInfo.State = 0;
			projectDocumentInfo.AddDate = DateTime.Today;
			if (ProjectOverAction.AddDocument(projectDocumentInfo) == 1)
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_documentlist' });");
			}
			else
			{
				base.RegisterScript("top.ui.alert('添加失败！');");
			}
			this.HdnDocCode.Value = projectDocumentInfo.DocumentCode.ToString();
		}
	}


