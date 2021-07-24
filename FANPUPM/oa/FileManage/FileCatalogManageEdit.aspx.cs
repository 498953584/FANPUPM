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
public partial class oa_FileManage_FileCatalogManageEdit : BasePage, IRequiresSessionState
{

	public OAFileCatalogAction amAction
	{
		get
		{
			return new OAFileCatalogAction();
		}
	}
	public string OperateType
	{
		get
		{
			object obj = this.ViewState["OPEARTETYPE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["OPEARTETYPE"] = value;
		}
	}
	public int RecordID
	{
		get
		{
			object obj = this.ViewState["RECORDID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["RECORDID"] = value;
		}
	}
	public string LibraryCode
	{
		get
		{
			object obj = this.ViewState["LIBRARYCODE"];
			if (obj != null)
			{
				return (string)obj;
			}
			return "";
		}
		set
		{
			this.ViewState["LIBRARYCODE"] = value;
		}
	}
	public int FileAge
	{
		get
		{
			object obj = this.ViewState["FILEAGE"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["FILEAGE"] = value;
		}
	}
	public int TimeLimit
	{
		get
		{
			object obj = this.ViewState["TIMELIMIT"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["TIMELIMIT"] = value;
		}
	}
	public int ClassID
	{
		get
		{
			object obj = this.ViewState["CLASSID"];
			if (obj != null)
			{
				return (int)obj;
			}
			return -1;
		}
		set
		{
			this.ViewState["CLASSID"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["t"] == null)
		{
			this.Page.RegisterStartupScript("", "<script>alert('参数错误!');window.close();</script>");
			return;
		}
		this.OperateType = base.Request["t"].ToString();
		if (base.Request["rc"] != null && base.Request["rc"].ToString() != "")
		{
			this.RecordID = int.Parse(base.Request["rc"].ToString());
		}
		if (base.Request["lc"] != null && base.Request["lc"].ToString() != "")
		{
			this.LibraryCode = base.Request["lc"].ToString();
		}
		if (base.Request["cid"] != null && base.Request["cid"].ToString() != "")
		{
			this.ClassID = int.Parse(base.Request["cid"].ToString());
		}
		if (base.Request["y"] != null && base.Request["y"].ToString() != "")
		{
			this.FileAge = int.Parse(base.Request["y"].ToString());
		}
		if (base.Request["l"] != null && base.Request["l"].ToString() != "")
		{
			this.TimeLimit = int.Parse(base.Request["l"].ToString());
		}
		if (!this.Page.IsPostBack)
		{
			if (this.OperateType == "add")
			{
				this.Add_Bind();
			}
			if (this.OperateType == "upd")
			{
				this.Edit_Bind();
			}
			if (this.OperateType == "view")
			{
				this.Edit_Bind();
				this.btnAdd.Width = 0;
			}
		}
		this.txtPageNumber.Attributes["onblur"] = "IsInteger(this);";
		this.txtFileAge.Attributes["onblur"] = "Datetimes(this);";
	}
	private void Add_Bind()
	{
		if (this.FileAge != -1)
		{
			this.txtFileAge.Text = this.FileAge.ToString();
			this.txtFileAge.Enabled = false;
		}
		if (this.TimeLimit != -1)
		{
			this.DDLTimeLimit.SelectedValue = this.TimeLimit.ToString();
			this.DDLTimeLimit.Enabled = false;
		}
		if (this.ClassID != -1)
		{
			this.DDLClassID.SelectedValue = this.ClassID.ToString();
			this.DDLClassID.Enabled = false;
		}
		this.txtPigeonholeDate.Text = DateTime.Now.ToShortDateString();
	}
	private void Edit_Bind()
	{
		this.txtFileAge.Enabled = false;
		this.DDLTimeLimit.Enabled = false;
		this.DDLClassID.Enabled = false;
		OAFileCatalog model = this.amAction.GetModel(this.RecordID);
		if (model != null)
		{
			this.txtBoxNumber.Text = model.BoxNumber.ToString();
			this.DDLClassID.SelectedValue = model.ClassID.ToString();
			this.txtContent.Text = model.Content;
			this.txtFileAge.Text = model.FileAge.ToString();
			this.txtFileCode.Text = model.FileCode.ToString();
			this.txtFileName.Text = model.FileName;
			this.txtFileNumber.Text = model.FileNumber.ToString();
			this.DDLFileType.SelectedValue = model.FileType;
			this.txtPageNumber.Text = model.PageNumber.ToString();
			this.txtPigeonholeDate.Text = model.PigeonholeDate.ToShortDateString();
			this.txtPrincipal.Text = model.Principal;
			this.txtSavePlace.Text = model.SavePlace;
			this.DDLSecretLevel.SelectedValue = model.SecretLevel.ToString();
			this.DDLTimeLimit.SelectedValue = model.TimeLimit.ToString();
			this.txtVolume.Text = model.Volume.ToString();
		}
	}
	private OAFileCatalog GetData()
	{
		return new OAFileCatalog
		{
			BoxNumber = this.txtBoxNumber.Text.Trim(),
			ClassID = (this.DDLClassID.Items.Count > 0) ? int.Parse(this.DDLClassID.SelectedValue) : 0,
			Content = this.txtContent.Text.Trim(),
			FileAge = (this.txtFileAge.Text.Trim() == "") ? DateTime.Now.Year : int.Parse(this.txtFileAge.Text.Trim()),
			FileCode = this.txtFileCode.Text.Trim(),
			FileName = this.txtFileName.Text.Trim(),
			FileNumber = this.txtFileNumber.Text.Trim(),
			FileType = this.DDLFileType.SelectedValue,
			IsValid = "1",
			LendState = "0",
			LibraryCode = this.LibraryCode,
			PageNumber = (this.txtPageNumber.Text.Trim() == "") ? 0 : int.Parse(this.txtPageNumber.Text.Trim()),
			PigeonholeDate = (this.txtPigeonholeDate.Text.Trim() == "") ? DateTime.Now : Convert.ToDateTime(this.txtPigeonholeDate.Text.Trim()),
			Principal = this.txtPrincipal.Text.Trim(),
			PrjCode = Guid.Empty,
			PrjName = "",
			RecordDate = DateTime.Now,
			RecordID = this.RecordID,
			SavePlace = this.txtSavePlace.Text.Trim(),
			SecretLevel = int.Parse(this.DDLSecretLevel.SelectedValue),
			TimeLimit = int.Parse(this.DDLTimeLimit.SelectedValue),
			UserCode = this.Session["yhdm"].ToString(),
			Volume = this.txtVolume.Text.Trim()
		};
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		OAFileCatalog data = this.GetData();
		if (this.OperateType == "add")
		{
			int num = this.amAction.Add(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
			}
			else
			{
				this.JS.Text = "没有相关数据可添加!";
			}
		}
		if (this.OperateType == "upd")
		{
			int num = this.amAction.Update(data);
			if (num > 0)
			{
				this.JS.Text = "returnValue=true;window.close();";
				return;
			}
			this.JS.Text = "没有相关数据可更新!";
		}
	}
}


