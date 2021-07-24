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
	public partial class AnnexAdd : BasePage, IRequiresSessionState
	{
		private AnnexAction _AnnexAction = new AnnexAction();
		private int _AnnexType;
		private int _ModuleID;

		private void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["at"] == null || base.Request["mid"] == null)
			{
				this.js.Text = "alert('参数错误！');window.returnValue = false; window.close();";
				return;
			}
			this._AnnexType = int.Parse(base.Request["at"]);
			this._ModuleID = int.Parse(base.Request["mid"]);
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
			base.Load += new EventHandler(this.Page_Load);
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			AnnexInfo annexInfo = new AnnexInfo();
			annexInfo.AnnexCode = Guid.NewGuid();
			annexInfo.ModuleID = this._ModuleID;
			annexInfo.AnnexType = this._AnnexType;
			annexInfo.FileCode = this.txtFileCode.Text;
			annexInfo.HumanCode = this.Session["yhdm"].ToString();
			annexInfo.Remark = this.txtRemark.Text;
			AnnexModuleSettingsInfo settingsInfo = new AnnexModuleSettingsInfo();
			settingsInfo = AnnexAssistAction.GetAnnexModelInfo(this._ModuleID);
			if (this._AnnexAction.AddAnnex(this.fileAnnex.PostedFile, annexInfo, settingsInfo) != 1)
			{
				this.js.Text = "alert('上传文件失败！');";
				return;
			}
			this.js.Text = "window.returnValue=true;alert('上传文件成功！');window.close();";
		}
	}


