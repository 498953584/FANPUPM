using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_eFileLock : BasePage, IRequiresSessionState
{

	private eFileInfoAction eff
	{
		get
		{
			return new eFileInfoAction();
		}
	}
	private eFileLendAction efa
	{
		get
		{
			return new eFileLendAction();
		}
	}
	private int rid
	{
		get
		{
			return Convert.ToInt32(this.ViewState["RID"]);
		}
		set
		{
			this.ViewState["RID"] = value;
		}
	}
	public Guid InstanceCode
	{
		get
		{
			object obj = this.ViewState["InstanceCode"];
			if (obj != null)
			{
				return (Guid)obj;
			}
			return Guid.Empty;
		}
		set
		{
			this.ViewState["InstanceCode"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["ic"] != null)
			{
				this.InstanceCode = new Guid(base.Request["ic"]);
				DataTable list = this.efa.GetList(" RecordID = '" + this.InstanceCode + "'");
				if (list.Rows.Count > 0)
				{
					this.rid = Convert.ToInt32(list.Rows[0]["FileRecordID"]);
				}
				else
				{
					this.rid = 0;
				}
				this.PageBind();
			}
		}
	}
	private void PageBind()
	{
		userManageDb userManageDb = new userManageDb();
		eFileLend model = this.efa.GetModel(this.InstanceCode);
		if (model.LendDate.ToString().Substring(0, 4) != "0001")
		{
			this.LbLendDate.Text = model.LendDate.ToString("yyyy-MM-dd");
		}
		this.LbPlanReturnDate.Text = model.PlanReturnDate.ToString("yyyy-MM-dd");
		this.LbBorrowMan.Text = userManageDb.GetUserName(model.BorrowMan);
		eFileInfo model2 = this.eff.GetModel(this.rid);
		this.LbUserName.Text = userManageDb.GetUserName(model2.UserCode);
		this.LbFileTitle.Text = model2.FileTitle;
		this.LbFileCode.Text = model2.FileCode;
		this.LbSaveLimit.Text = model2.SaveLimit;
		this.LbSubmitMan.Text = userManageDb.GetUserName(model2.SubmitMan);
		string secretLevel;
		if ((secretLevel = model2.SecretLevel) != null)
		{
			if (!(secretLevel == "1"))
			{
				if (!(secretLevel == "2"))
				{
					if (secretLevel == "3")
					{
						this.LbSecretLevel.Text = "绝密";
					}
				}
				else
				{
					this.LbSecretLevel.Text = "机密";
				}
			}
			else
			{
				this.LbSecretLevel.Text = "秘密";
			}
		}
		string fileCopy;
		if ((fileCopy = model2.FileCopy) != null)
		{
			if (!(fileCopy == "M"))
			{
				if (!(fileCopy == "U"))
				{
					if (fileCopy == "F")
					{
						this.LbFileCopy.Text = "正式电子文件";
					}
				}
				else
				{
					this.LbFileCopy.Text = "非正式电子文件";
				}
			}
			else
			{
				this.LbFileCopy.Text = "草稿性电子文件";
			}
		}
		string fileType;
		switch (fileType = model2.FileType)
		{
		case "T":
			this.LbFileType.Text = "文本文件";
			return;
		case "I":
			this.LbFileType.Text = "图像文件";
			return;
		case "G":
			this.LbFileType.Text = "图形文件";
			return;
		case "V":
			this.LbFileType.Text = "影像文件";
			return;
		case "A":
			this.LbFileType.Text = "声音文件";
			return;
		case "M":
			this.LbFileType.Text = "多媒体文件";
			return;
		case "P":
			this.LbFileType.Text = "计算机程序";
			return;
		case "D":
			this.LbFileType.Text = "数据文件";
			break;

			return;
		}
	}
}


