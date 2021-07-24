using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserSet_PasswordSet_SetAuditPwd : BasePage, IRequiresSessionState
{

	public string ImageName
	{
		get
		{
			object obj = this.ViewState["ImageName"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["ImageName"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			userManageDb userManageDb = new userManageDb();
			this.tbLoginName.Text = userManageDb.GetCurrentUserInfo().UserName;
			DataTable userImageList = userManageDb.GetUserImageList(this.Session["yhdm"].ToString());
			if (userImageList.Rows[0]["AuditNameImagePath"].ToString() != "")
			{
				this.ImgName.ImageUrl = userImageList.Rows[0]["AuditNameImagePath"].ToString();
				this.ImageName = userImageList.Rows[0]["AuditNameImagePath"].ToString();
			}
		}
	}
	protected void btMod_Click(object sender, EventArgs e)
	{
		string text = this.tbNewPwd1.Text;
		string text2 = this.tbNewPwd2.Text;
		string text3;
		if (this.FUFilePath.HasFile)
		{
			HttpPostedFile postedFile = this.FUFilePath.PostedFile;
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string[] array = fileUpload.Upload(postedFile, 1);
			text3 = array[1].ToString();
		}
		else
		{
			text3 = this.ImageName;
		}
		userManageDb userManageDb = new userManageDb();
		if (!(text == text2))
		{
			this.js.Text = "alert('您两次输入的新密码不相同，请重新输入！');";
			return;
		}
		if (userManageDb.updateUserAuditPwd(this.Page.Session["yhdm"].ToString(), text.ToString(), text3))
		{
			this.js.Text = "alert('密码修改成功，下次审核请使用新密码！');";
			this.ImgName.ImageUrl = text3;
			return;
		}
		this.js.Text = "alert('密码修改未成功，请重试！');";
	}
}


