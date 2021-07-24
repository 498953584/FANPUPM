using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_ProjectFileManageEdit : BasePage, IRequiresSessionState
{

	private string Type
	{
		get
		{
			return this.ViewState["YTPE"].ToString();
		}
		set
		{
			this.ViewState["YTPE"] = value;
		}
	}
	private eFileInfoAction eff
	{
		get
		{
			return new eFileInfoAction();
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
	private int ClassID
	{
		get
		{
			return Convert.ToInt32(this.ViewState["CLASSID"].ToString());
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	private string RecordType
	{
		get
		{
			return this.ViewState["RECORDTYPE"].ToString();
		}
		set
		{
			this.ViewState["RECORDTYPE"] = value;
		}
	}
	private Guid PrjGuid
	{
		get
		{
			return (Guid)this.ViewState["PRJGUID"];
		}
		set
		{
			this.ViewState["PRJGUID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["rid"] != null || base.Request["t"] != null || base.Request["cid"] != null || base.Request["rt"] != null || base.Request["prj"] != null)
			{
				if (base.Request["sl"] != null && base.Request["sl"].ToString() != "")
				{
					this.DDLSecretLevel.Items.Clear();
					this.DDLSecretLevel.Items.Add(new ListItem("绝密", "3"));
				}
				this.rid = Convert.ToInt32(base.Request["rid"]);
				this.Type = base.Request["t"].ToString();
				this.ClassID = Convert.ToInt32(base.Request["cid"]);
				this.RecordType = base.Request["rt"].ToString();
				this.PrjGuid = new Guid(base.Request["prj"]);
				this.DBSubmitDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				this.DBRecordDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
				userManageDb userManageDb = new userManageDb();
				this.txtSubmitMan.Text = userManageDb.GetUserName(this.Session["yhdm"].ToString());
				this.HdnSubmitMan.Value = this.Session["yhdm"].ToString();
				this.BtnSave.Attributes["onclick"] = "return IsNullValue();";
				if (this.Type != "Add")
				{
					this.PageBind();
					this.Td_Look.Visible = true;
					this.Td_Up.Visible = false;
					this.RBSelectPath.Enabled = false;
					return;
				}
				this.Td_Look.Visible = false;
				this.Td_Up.Visible = true;
				this.txtFileCode.Text = this.FileCode();
			}
		}
	}
	private eFileInfo geteFileInfoModel()
	{
		eFileInfo eFileInfo = new eFileInfo();
		eFileInfo.RecordID = this.rid;
		eFileInfo.PrjGuid = this.PrjGuid;
		eFileInfo.RecordType = this.RecordType;
		eFileInfo.CorpCode = this.Session["CorpCode"].ToString();
		eFileInfo.FileTitle = this.txtFileTitle.Text;
		eFileInfo.SubmitMan = this.HdnSubmitMan.Value;
		eFileInfo.SubmitDate = Convert.ToDateTime(this.DBSubmitDate.Text);
		eFileInfo.Remark = this.txtRemark.Text;
		eFileInfo.FileCode = this.txtFileCode.Text;
		eFileInfo.ClassID = this.ClassID;
		eFileInfo.UserCode = this.Session["yhdm"].ToString();
		eFileInfo.RecordDate = Convert.ToDateTime(this.DBRecordDate.Text);
		eFileInfo.SaveLimit = this.txtSaveLimit.Text;
		eFileInfo.SecretLevel = this.DDLSecretLevel.SelectedValue;
		eFileInfo.FileType = this.DDLFileType.SelectedValue;
		eFileInfo.FileCopy = this.DDLFileCopy.SelectedValue;
		if (this.Td_Up.Visible)
		{
			if (this.RBSelectPath.SelectedValue == "1")
			{
				if (this.FUFilePath.HasFile)
				{
					HttpPostedFile postedFile = this.FUFilePath.PostedFile;
					com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
					string[] array = fileUpload.Upload(postedFile, 1);
					eFileInfo.FilePath = array[1].ToString();
					eFileInfo.OriginalName = array[0].ToString();
				}
			}
			else
			{
				eFileInfo.FilePath = this.txtFilePath.Text;
				eFileInfo.OriginalName = this.txtFilePath.Text;
			}
		}
		else
		{
			eFileInfo.FilePath = "0";
			eFileInfo.OriginalName = "0";
		}
		return eFileInfo;
	}
	private void PageBind()
	{
		eFileInfo model = this.eff.GetModel(this.rid);
		this.txtFileTitle.Text = model.FileTitle;
		this.DBSubmitDate.Text = model.SubmitDate.ToString("yyyy-MM-dd");
		this.txtRemark.Text = model.Remark;
		this.txtFileCode.Text = model.FileCode;
		this.txtSaveLimit.Text = model.SaveLimit;
		this.DDLSecretLevel.SelectedValue = model.SecretLevel;
		this.DDLFileType.SelectedValue = model.FileType;
		this.DDLFileCopy.SelectedValue = model.FileCopy;
		this.HL_Annex.Text = model.OriginalName;
		this.HL_Annex.NavigateUrl = "#";
		this.HL_Annex.Attributes["onclick"] = "javascript:download('" + model.FilePath + "');";
		userManageDb userManageDb = new userManageDb();
		this.txtSubmitMan.Text = userManageDb.GetUserName(model.SubmitMan);
		this.HdnSubmitMan.Value = model.SubmitMan;
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.Type == "Add")
		{
			if (this.eff.Add(this.geteFileInfoModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
		else
		{
			if (this.eff.Update(this.geteFileInfoModel()) == 1)
			{
				this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "alert('保存失败!');";
			return;
		}
	}
	protected void IBtn_DelAnnex_Click(object sender, ImageClickEventArgs e)
	{
		com.jwsoft.pm.entpm.action.FileUpload fileUpload = new com.jwsoft.pm.entpm.action.FileUpload();
		fileUpload.DeleteFile(this.HL_Annex.NavigateUrl.ToString());
		this.Td_Look.Visible = false;
		this.Td_Up.Visible = true;
		this.RBSelectPath.Enabled = true;
	}
	private string FileCode()
	{
		string text;
		if (this.Session["CorpCode"].ToString() == "00")
		{
			text = "G";
		}
		else
		{
			text = "S";
		}
		text = text + this.DDLFileCopy.SelectedValue.ToString() + this.DDLFileType.SelectedValue.ToString();
		text += DateTime.Now.ToString("yyyy-MM-dd").Replace("-", "");
		return text + this.eff.GetFileCode(text);
	}
	private string ReplaceCode(int strarIndex, int length, string strOld, string strNew)
	{
		string text = strOld.Remove(strarIndex, length);
		return text.Insert(strarIndex, strNew);
	}
	protected void DDLFileCopy_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.txtFileCode.Text = this.ReplaceCode(1, 1, this.txtFileCode.Text.ToString(), this.DDLFileCopy.SelectedValue.ToString());
	}
	protected void DDLFileType_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.txtFileCode.Text = this.ReplaceCode(2, 1, this.txtFileCode.Text.ToString(), this.DDLFileType.SelectedValue.ToString());
	}
}


