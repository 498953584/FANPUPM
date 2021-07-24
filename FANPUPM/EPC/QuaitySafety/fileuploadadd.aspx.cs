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
	public partial class FileUploadAdd : PageBase, IRequiresSessionState
	{
		private QualityGoalAction qa = new QualityGoalAction();
		private string modelkey;

		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["PrjCode"].ToString() == "")
			{
				this.JS.Text = "alert('请选择项目!');window.close();";
			}
			if (base.Request["modelkey"] != null)
			{
				this.modelkey = base.Request.QueryString["modelkey"];
			}
			if (!this.Page.IsPostBack)
			{
				if (base.Request.QueryString["Flag"].ToString() == "Q")
				{
					this.lblTitle.Text = "质量台账";
					return;
				}
				this.lblTitle.Text = "安全台账";
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
		protected void BtnOK_Click(object sender, EventArgs e)
		{
			QualityFileInfo qualityFileInfo = new QualityFileInfo();
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string[] array = fileUpload.Upload(this.FileUp.PostedFile, AnnexAssistAction.GetQSTable().ModuleID);
			qualityFileInfo.urlpath = array[1];
			if (array[2].ToString() != "")
			{
				this.JS.Text = "alert('" + array[2].ToString() + "');";
				return;
			}
			qualityFileInfo.title = this.txtName.Text;
			qualityFileInfo.bz = this.txtbz.Text;
			qualityFileInfo.modelkey = int.Parse(this.modelkey);
			qualityFileInfo.prjcode = base.Request.QueryString["PrjCode"];
			int num = this.qa.InsertFile(qualityFileInfo);
			if (num == 1)
			{
				this.JS.Text = "alert('上传成功!');window.close();";
				return;
			}
			this.JS.Text = "alert('上传失败!');";
		}
	}


