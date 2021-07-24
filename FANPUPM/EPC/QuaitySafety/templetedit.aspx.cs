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
	public partial class TempletEdit : NBasePage, IRequiresSessionState
	{
		private TableAccountAction taa = new TableAccountAction();
		private TableAccountModelInfo tami = new TableAccountModelInfo();

		public string OpType
		{
			get
			{
				object obj = this.ViewState["OpType"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["OpType"] = value;
			}
		}
		public string Flag
		{
			get
			{
				object obj = this.ViewState["Flag"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				this.ViewState["Flag"] = value;
			}
		}
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!base.IsPostBack)
			{
				this.Flag = base.Request.QueryString["Flag"].ToString();
				this.OpType = base.Request.QueryString["OpType"].ToString();
				if (this.Flag == "Q")
				{
					this.LbName.Text = "质量台账模板维护";
				}
				else
				{
					if (this.Flag == "S")
					{
						this.LbName.Text = "安全资料模板维护";
					}
				}
				if (this.OpType == "Edit")
				{
					this.tami = this.taa.getOneAccountModel(Convert.ToInt32(base.Request.QueryString["RID"]));
					this.tbModelName.Text = this.tami.ModelName.ToString();
					this.FileModel.Visible = false;
					this.hlFile.Visible = true;
					this.hlFile.Text = this.tami.FilePath.Substring(this.tami.FilePath.LastIndexOf("/") + 1);
					this.hlFile.NavigateUrl = this.tami.FilePath.ToString();
				}
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
		protected void btnConfirm_Click(object sender, EventArgs e)
		{
			if (!(this.OpType == "Add"))
			{
				if (this.OpType == "Edit")
				{
					if (this.taa.updAccountModelRecord(this.tbModelName.Text.Trim(), Convert.ToInt32(base.Request.QueryString["RID"])))
					{
						base.RegisterScript("top.ui.tabSuccess({ parentName: '_templetview' });");
						return;
					}
					this.JS.Text = "alert('保存模板记录失败!')";
				}
				return;
			}
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string text = this.FileModel.PostedFile.FileName.Substring(this.FileModel.PostedFile.FileName.LastIndexOf("\\") + 1);
			string filePath = "";
			if (this.Flag == "Q")
			{
				fileUpload.UploadPath = "QualityReport\\";
				filePath = "QualityReport/" + text;
			}
			else
			{
				if (this.Flag == "S")
				{
					fileUpload.UploadPath = "SafetyReport\\";
					filePath = "SafetyReport/" + text;
				}
			}
			if (!fileUpload.Upload(this.FileModel.PostedFile, text, false))
			{
				this.JS.Text = "alert('上传文件失败!');return false;";
				return;
			}
			if (this.taa.insAccountModelRecord(this.tbModelName.Text.Trim(), filePath, this.Flag))
			{
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_templetview' });");
				return;
			}
			this.JS.Text = "alert('保存模板记录失败!')";
		}
	}


