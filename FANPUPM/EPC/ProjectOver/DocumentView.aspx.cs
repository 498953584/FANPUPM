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
	public partial class DocumentView : NBasePage, IRequiresSessionState
	{
		protected AnnexAction _AnnexAction = new AnnexAction();
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
				if (base.Request["dc"] == "00000000-0000-0000-0000-000000000000")
				{
					base.Response.End();
				}
				if (base.Request["dc"] == null)
				{
					this.JS.Text = "alert('参数错误！');";
					return;
				}
				this.DocumentCode = base.Request["dc"];
				this.RestoreDocumentInfo(new Guid(this.DocumentCode));
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
			if (docCode != Guid.Empty)
			{
				ProjectDocumentInfo oneDocumentInfo = ProjectOverAction.GetOneDocumentInfo(docCode);
				this.LblDocumentName.Text = oneDocumentInfo.DocumentName;
				this.LblFileCode.Text = oneDocumentInfo.FileCode;
				this.LblRollCode.Text = oneDocumentInfo.RollCode;
				this.LblStorage.Text = oneDocumentInfo.Storage;
				this.LblRemark.Text = oneDocumentInfo.Remark;
				this.LblClass.Text = com.jwsoft.pm.entpm.PageHelper.QueryBasicCode(this, "DocumentType", 20, oneDocumentInfo.ClassCode);
				this.dgdAnnex_Bind(this.DocumentCode, 0, 26);
			}
		}
		private void dgdAnnex_Bind(string recordCode, int annexType, int moduleID)
		{
			this.FileLink1.MID = 26;
			this.FileLink1.FID = recordCode;
			this.FileLink1.Type = 0;
		}
	}


