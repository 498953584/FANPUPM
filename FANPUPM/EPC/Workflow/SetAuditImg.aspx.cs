using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_SetAuditImg : NBasePage, IRequiresSessionState
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
			return "/images/defaultaudit.gif";
		}
		set
		{
			this.ViewState["ImageName"] = value;
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
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
	protected void SaveBt_Click(object sender, System.EventArgs e)
	{
		userManageDb userManageDb = new userManageDb();
		string text;
		if (this.FUFilePath.HasFile)
		{
			HttpPostedFile postedFile = this.FUFilePath.PostedFile;
			com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
			string[] array = fileUpload.Upload(postedFile, 1);
			text = array[1].ToString();
		}
		else
		{
			text = this.ImageName;
		}
		if (userManageDb.updateUserAuditImg(this.Page.Session["yhdm"].ToString(), text))
		{
			this.js.Text = "alert('审核签名设置成功！');";
			this.ImgName.ImageUrl = text;
			return;
		}
		this.js.Text = "alert('审核签名设置失败，请重试！');";
	}
}


