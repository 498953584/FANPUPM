using ASP;
using cn.justwin.BLL;
using System;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class TableTop_UploadLogo : NBasePage, IRequiresSessionState
{
	public string imaLoginStr = string.Empty;
	public string imaLoginedStr = string.Empty;
	private string logoPath = "/UploadFiles/UserLogo/logo.jpg";
	private string logoBakPath = "/UploadFiles/UserLogo/logobak.jpg";
	private string logoLastPath = "/UploadFiles/UserLogo/logolast.jpg";
	private string logoTempPath = "/UploadFiles/UserLogo/logotemp.jpg";
	private string logo3Path = "/UploadFiles/UserLogo/logo3.jpg";
	private string logo3BakPath = "/UploadFiles/UserLogo/logo3bak.jpg";
	private string logo3LastPath = "/UploadFiles/UserLogo/logo3last.jpg";
	private string logo3TempPath = "/UploadFiles/UserLogo/logo3temp.jpg";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindImg();
		}
	}
	protected void BindImg()
	{
		this.imaLoginStr = DateTime.Now.ToString();
		this.imaLoginedStr = DateTime.Now.ToString();
	}
	protected void btnSaveLogin_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.fplLoginLogo.PostedFile.FileName))
		{
			string contentType = this.fplLoginLogo.PostedFile.ContentType;
			if (contentType == "image/bmp" || contentType == "image/gif" || contentType == "image/x-png" || contentType == "image/pjpeg")
			{
				string arg_72_0 = this.fplLoginLogo.PostedFile.FileName;
				string text = base.Server.MapPath(this.logo3Path);
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Exists)
				{
					fileInfo.IsReadOnly = false;
					FileInfo fileInfo2 = new FileInfo(base.Server.MapPath(this.logo3LastPath));
					if (fileInfo2.Exists)
					{
						fileInfo2.IsReadOnly = false;
						fileInfo2.Delete();
					}
					fileInfo.MoveTo(base.Server.MapPath(this.logo3LastPath));
				}
				this.fplLoginLogo.SaveAs(text);
				base.RegisterScript("top.ui.show('上传成功')");
			}
			else
			{
				base.RegisterScript("top.ui.alert('上传的登录页面的logo格式不正确')");
			}
		}
		else
		{
			base.RegisterScript("top.ui.alert('请选择图片')");
		}
		this.BindImg();
	}
	protected void btnSaveLogined_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.fplLoginedLogo.PostedFile.FileName))
		{
			string contentType = this.fplLoginedLogo.PostedFile.ContentType;
			if (contentType == "image/bmp" || contentType == "image/gif" || contentType == "image/x-png" || contentType == "image/pjpeg")
			{
				string arg_72_0 = this.fplLoginedLogo.PostedFile.FileName;
				string text = base.Server.MapPath(this.logoPath);
				FileInfo fileInfo = new FileInfo(text);
				if (fileInfo.Exists)
				{
					fileInfo.IsReadOnly = false;
					FileInfo fileInfo2 = new FileInfo(base.Server.MapPath(this.logoLastPath));
					if (fileInfo2.Exists)
					{
						fileInfo2.IsReadOnly = false;
						fileInfo2.Delete();
					}
					fileInfo.MoveTo(base.Server.MapPath(this.logoLastPath));
				}
				this.fplLoginedLogo.SaveAs(text);
				base.RegisterScript("top.ui.show('上传成功')");
			}
			else
			{
				base.RegisterScript("top.ui.alert('上传的登录后页面的logo格式不正确')");
			}
		}
		else
		{
			base.RegisterScript("top.ui.alert('请选择图片')");
		}
		this.BindImg();
	}
	protected void btnReturnLastLogin_Click(object sender, EventArgs e)
	{
		FileInfo fileInfo = new FileInfo(base.Server.MapPath(this.logo3LastPath));
		FileInfo fileInfo2 = new FileInfo(base.Server.MapPath(this.logo3Path));
		if (fileInfo.Exists)
		{
			if (fileInfo2.Exists)
			{
				fileInfo2.MoveTo(base.Server.MapPath(this.logo3TempPath));
			}
			fileInfo.MoveTo(base.Server.MapPath(this.logo3Path));
			FileInfo fileInfo3 = new FileInfo(base.Server.MapPath(this.logo3TempPath));
			fileInfo3.MoveTo(base.Server.MapPath(this.logo3LastPath));
			base.RegisterScript("top.ui.show('还原成功')");
		}
		this.BindImg();
	}
	protected void btnReturnBakLogin_Click(object sender, EventArgs e)
	{
		FileInfo fileInfo = new FileInfo(base.Server.MapPath(this.logo3BakPath));
		FileInfo fileInfo2 = new FileInfo(base.Server.MapPath(this.logo3LastPath));
		FileInfo fileInfo3 = new FileInfo(base.Server.MapPath(this.logo3Path));
		if (fileInfo3.Exists)
		{
			if (fileInfo2.Exists)
			{
				fileInfo2.IsReadOnly = false;
				fileInfo2.Delete();
			}
			fileInfo3.MoveTo(base.Server.MapPath(this.logo3LastPath));
			fileInfo.CopyTo(base.Server.MapPath(this.logo3Path));
			base.RegisterScript("top.ui.show('还原成功')");
		}
		this.BindImg();
	}
	protected void btnReturnLastLogined_Click(object sender, EventArgs e)
	{
		FileInfo fileInfo = new FileInfo(base.Server.MapPath(this.logoLastPath));
		FileInfo fileInfo2 = new FileInfo(base.Server.MapPath(this.logoPath));
		if (fileInfo.Exists)
		{
			if (fileInfo2.Exists)
			{
				fileInfo2.MoveTo(base.Server.MapPath(this.logoTempPath));
			}
			fileInfo.MoveTo(base.Server.MapPath(this.logoPath));
			FileInfo fileInfo3 = new FileInfo(base.Server.MapPath(this.logoTempPath));
			fileInfo3.MoveTo(base.Server.MapPath(this.logoLastPath));
			base.RegisterScript("top.ui.show('还原成功')");
		}
		this.BindImg();
	}
	protected void btnReturnBakLogined_Click(object sender, EventArgs e)
	{
		FileInfo fileInfo = new FileInfo(base.Server.MapPath(this.logoBakPath));
		FileInfo fileInfo2 = new FileInfo(base.Server.MapPath(this.logoLastPath));
		FileInfo fileInfo3 = new FileInfo(base.Server.MapPath(this.logoPath));
		if (fileInfo3.Exists)
		{
			if (fileInfo2.Exists)
			{
				fileInfo2.IsReadOnly = false;
				fileInfo2.Delete();
			}
			fileInfo3.MoveTo(base.Server.MapPath(this.logoLastPath));
			fileInfo.CopyTo(base.Server.MapPath(this.logoPath));
			base.RegisterScript("top.ui.show('还原成功！')");
		}
		this.BindImg();
	}
}


