using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using System;
using System.IO;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class upload : NBasePage, IRequiresSessionState
{
	private FileInfoBll fileInfoBll = new FileInfoBll();
	private string _fid = "";
	private int _ModuleID;
	private int _prjAge;
	private Guid _prjCode;

	protected void Page_Load(object sender, EventArgs e)
	{
		this._ModuleID = Convert.ToInt32(base.Request.Params["mid"]);
		this._fid = base.Request.Params["fid"].ToString();
		if (base.Request.Files.Count > 0)
		{
			string text = string.Empty;
			for (int i = 0; i < base.Request.Files.Count; i++)
			{
				HttpPostedFile httpPostedFile = base.Request.Files[i];
				string fileName = httpPostedFile.FileName;
				if (this.fileInfoBll.GetListArray(" where fileName='" + httpPostedFile.FileName + "'").Count > 0)
				{
					fileName = new Random().Next(100).ToString() + httpPostedFile.FileName;
				}
				if (httpPostedFile != null && httpPostedFile.ContentLength > 0)
				{
					try
					{
						string fileNewName;
						if (this._ModuleID == 2)
						{
							PersonalFileModel personalModel = this.GetPersonalModel(httpPostedFile, fileName);
							fileNewName = personalModel.FileNewName;
							new PersonalFileBll().Add(personalModel);
						}
						else
						{
							FileInfoModel fileModel = this.GetFileModel(httpPostedFile, fileName);
							fileNewName = fileModel.FileNewName;
							this.fileInfoBll.Add(fileModel);
						}
						text = base.Server.MapPath("~/UploadFiles/FileInfo/");
						if (!Directory.Exists(text))
						{
							Directory.CreateDirectory(text);
						}
						httpPostedFile.SaveAs(text + fileNewName);
					}
					catch (Exception)
					{
						throw;
					}
				}
			}
		}
	}
	public FileInfoModel GetFileModel(HttpPostedFile file, string fileName)
	{
		return new FileInfoModel
		{
			CreateTime = new DateTime?(DateTime.Now),
			FileOwner = base.UserCode,
			UserCodes = this.GetUserCode(),
			FileType = "0",
			Id = Guid.NewGuid().ToString(),
			ParentId = this._fid,
			FileName = fileName,
			FileNewName = this.GetNewName(file.FileName),
			FileSize = file.ContentLength.ToString()
		};
	}
	public PersonalFileModel GetPersonalModel(HttpPostedFile file, string fileName)
	{
		return new PersonalFileModel
		{
			CreateTime = new DateTime?(DateTime.Now),
			FileOwner = base.UserCode,
			FileType = "0",
			Id = Guid.NewGuid().ToString(),
			ParentId = this._fid,
			FileName = fileName,
			FileNewName = this.GetNewName(file.FileName),
			FileSize = file.ContentLength.ToString(),
			ShareUsers = JsonHelper.Serialize(new string[]
			{
				"00000000"
			})
		};
	}
	public string GetUserCode()
	{
		if (base.UserCode != "00000000")
		{
			return JsonHelper.Serialize(new string[]
			{
				base.UserCode,
				"00000000"
			});
		}
		return JsonHelper.Serialize(new string[]
		{
			"00000000"
		});
	}
	public string GetNewName(string oldName)
	{
		return DateTime.Now.ToString("yyyyMMddHHmmsss") + new Random().Next(100).ToString() + "," + oldName;
	}
	private string GetNameFromFullName(string fullName)
	{
		return fullName.Substring(fullName.LastIndexOf("\\") + 1, fullName.Length - fullName.LastIndexOf("\\") - 1);
	}
}


