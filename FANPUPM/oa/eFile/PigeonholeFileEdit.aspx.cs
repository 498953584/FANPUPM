using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_PigeonholeFileEdit : BasePage, IRequiresSessionState
{

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
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			if (base.Request["rid"] != null)
			{
				this.rid = Convert.ToInt32(base.Request["rid"]);
				this.BtnSelect.Attributes["onclick"] = "return SelectClass();";
				this.PageBind();
				this.txtFileCode.Text = this.FileCode();
			}
		}
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
		userManageDb userManageDb = new userManageDb();
		this.txtSubmitMan.Text = userManageDb.GetUserName(model.SubmitMan);
		this.HdnSubmitMan.Value = model.SubmitMan;
	}
	private eFileInfo geteFileInfoModel()
	{
		return new eFileInfo
		{
			RecordID = this.rid,
			PrjGuid = Guid.Empty,
			RecordType = "1",
			CorpCode = this.Session["CorpCode"].ToString(),
			FileTitle = this.txtFileTitle.Text,
			SubmitMan = this.HdnSubmitMan.Value,
			SubmitDate = Convert.ToDateTime(this.DBSubmitDate.Text),
			Remark = this.txtRemark.Text,
			FileCode = this.FileCode(),
			ClassID = Convert.ToInt32(this.HdnClassID.Value),
			UserCode = this.Session["yhdm"].ToString(),
			RecordDate = Convert.ToDateTime(this.DBRecordDate.Text),
			SaveLimit = this.txtSaveLimit.Text,
			SecretLevel = this.DDLSecretLevel.SelectedValue,
			FileType = this.DDLFileType.SelectedValue,
			FileCopy = this.DDLFileCopy.SelectedValue,
			FilePath = "0",
			OriginalName = "0"
		};
	}
	protected void BtnSave_Click(object sender, EventArgs e)
	{
		if (this.eff.ReturnUpdate(this.geteFileInfoModel()) == 1)
		{
			this.JS.Text = "alert('保存成功!');returnValue=true;window.close();";
			return;
		}
		this.JS.Text = "alert('保存失败!');";
	}
	protected void DDLFileCopy_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.txtFileCode.Text = this.ReplaceCode(1, 1, this.txtFileCode.Text.ToString(), this.DDLFileCopy.SelectedValue.ToString());
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
	protected void DDLFileType_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.txtFileCode.Text = this.ReplaceCode(2, 1, this.txtFileCode.Text.ToString(), this.DDLFileType.SelectedValue.ToString());
	}
}


