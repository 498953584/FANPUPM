using ASP;
using cn.justwin.Web;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Profile;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class UserControl_FileUpload : System.Web.UI.UserControl
{
	private string folder;
	private string recordCode;

	public string Type
	{
		set
		{
			this.hfldType.Value = value;
		}
	}
	public string Name
	{
		set
		{
			this.hfldName.Value = value;
		}
	}
	public string FileType
	{
		set
		{
			this.hfldFileType.Value = value;
		}
	}
	public string Class
	{
		set
		{
			this.folder = ConfigurationManager.AppSettings[value];
			this.hfldFolder.Value = ConfigurationManager.AppSettings[value];
		}
	}
	public List<MergerFolder> MergerFolder
	{
		set
		{
			string value2 = JsonConvert.SerializeObject(value);
			this.hfldMergerFolder.Value = value2;
		}
	}
	public string Folder
	{
		get
		{
			return this.folder;
		}
		set
		{
			this.folder = value;
			this.hfldFolder.Value = value;
		}
	}
	public string RecordCode
	{
		get
		{
			return this.recordCode;
		}
		set
		{
			this.recordCode = value;
			this.hfldRecordCode.Value = value;
		}
	}
	public string OnComplete
	{
		set
		{
			this.hfldOnComplete.Value = value;
		}
	}
	public string Script
	{
		set
		{
			this.hfldScript.Value = value;
		}
	}
	public string ScriptData
	{
		set
		{
			this.hfldScriptData.Value = value;
		}
	}
	public string OnClose
	{
		set
		{
			this.hfldOnClose.Value = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.txtFile.Attributes.Add("ReadOnly", "ReadOnly");
		}
	}
	private void InitTexFile()
	{
		string text = this.folder;
		if (!string.IsNullOrEmpty(this.RecordCode))
		{
			text = text + "\\" + this.RecordCode;
		}
		DirectoryUtility directoryUtility = new DirectoryUtility(base.Server.MapPath(text));
		this.txtFile.Text = directoryUtility.GetAnnexNames();
	}
	public void DataBindAnnex()
	{
	}
}


