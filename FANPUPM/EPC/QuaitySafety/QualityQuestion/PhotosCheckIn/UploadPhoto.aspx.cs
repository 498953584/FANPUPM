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
public partial class CommonWindow_Annex_UploadPhoto : BasePage, IRequiresSessionState
{
	private IntendancePhotoListAction _IntendancePhotoListAction = new IntendancePhotoListAction();

	public Guid IntendanceGuid
	{
		get
		{
			return (Guid)this.ViewState["IntendanceGuid"];
		}
		set
		{
			this.ViewState["IntendanceGuid"] = value;
		}
	}
	public int PhotoType
	{
		get
		{
			return (int)this.ViewState["PhotoType"];
		}
		set
		{
			this.ViewState["PhotoType"] = value;
		}
	}
	private void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["p"] == null && base.Request["ty"] == null)
		{
			this.js.Text = "alert('参数错误！');window.returnValue = false; window.close();";
			return;
		}
		this.IntendanceGuid = new Guid(base.Request.QueryString["p"]);
		this.PhotoType = Convert.ToInt32(base.Request.QueryString["ty"]);
	}
	protected override void OnInit(EventArgs e)
	{
		this.InitializeComponent();
		base.OnInit(e);
	}
	private void InitializeComponent()
	{
		this.btnAd.Click += new EventHandler(this.btnAdd_Click);
		base.Load += new EventHandler(this.Page_Load);
	}
	private void btnAdd_Click(object sender, EventArgs e)
	{
		IntendancePhotoList intendancePhotoList = new IntendancePhotoList();
		intendancePhotoList.NoteId = Guid.NewGuid();
		intendancePhotoList.InfoGuid = this.IntendanceGuid;
		intendancePhotoList.PhotoNumber = this.txtFileCode.Text;
		intendancePhotoList.PhotoExplain = this.txtRemark.Text;
		intendancePhotoList.UserCode = base.UserCode;
		intendancePhotoList.PhotoType = this.PhotoType;
		MakeThumbnail makeThumbnail = new MakeThumbnail();
		if (makeThumbnail.AddIntendancePhotoListAction(this.fileAnnex.PostedFile, intendancePhotoList) != 1)
		{
			this.js.Text = "alert('上传文件失败！');";
			return;
		}
		this.js.Text = "window.returnValue=true;alert('上传文件成功！');window.close();";
	}
}


