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
	public partial class FileAdd : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				this.ViewState["DATUMCODE"] = base.Request.QueryString["DatumCode"];
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
		protected void btnAdd_Click(object sender, EventArgs e)
		{
			KnowledgeFileModel knowledgeFileModel = new KnowledgeFileModel();
			knowledgeFileModel.AnnexCode = Guid.NewGuid();
			knowledgeFileModel.ProjectCode = new Guid("{00000000-0000-0000-0000-000000000000}");
			knowledgeFileModel.ModuleID = 17;
			knowledgeFileModel.RecordCode = new Guid(this.ViewState["DATUMCODE"].ToString());
			knowledgeFileModel.AnnexType = 0;
			knowledgeFileModel.FileCode = "";
			knowledgeFileModel.AddDate = DateTime.Now;
			string[] array = new string[]
			{
				"",
				"",
				"",
				"",
				""
			};
			KnowledgeFileUp knowledgeFileUp = new KnowledgeFileUp();
			string[] array2 = knowledgeFileUp.UploadSingleFile(this.FileKnowledge.PostedFile, 17);
			knowledgeFileModel.OriginalName = array2[0];
			knowledgeFileModel.AnnexName = array2[4];
			knowledgeFileModel.FileSize = Convert.ToInt32(array2[3]);
			knowledgeFileModel.FilePath = array2[1];
			knowledgeFileModel.State = 0;
			knowledgeFileModel.HumanCode = this.Session["yhdm"].ToString();
			knowledgeFileModel.Remark = this.txtRemark.Text.Trim();
			if (KnowledgeFileAction.AddNewFile(knowledgeFileModel) != 1)
			{
				this.js.Text = "alert('增加失败！');";
				return;
			}
			this.js.Text = "alert('增加成功！');window.returnValue=true;window.close();";
		}
	}


