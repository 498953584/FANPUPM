<%@ WebHandler Language="C#" Class="FileUpload2" %>

using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using System;
using System.IO;
using System.Web;
public class FileUpload2 : IHttpHandler
{
	private FileInfoBll fileInfoBll = new FileInfoBll();
	private string _fid = "";
	private int _type;
	private string _userCode;
	public bool IsReusable
	{
		get
		{
			return false;
		}
	}
	public void ProcessRequest(HttpContext context)
	{
		context.Response.ContentType = "text/plain";
		context.Response.Charset = "utf-8";
		this._type = Convert.ToInt32(context.Request["type"]);
		this._fid = context.Request["fid"].ToString();
		this._userCode = context.Request["ucode"].ToString();
		HttpPostedFile httpPostedFile = context.Request.Files["Filedata"];
		string text = HttpContext.Current.Server.MapPath(context.Request["folder"]) + "\\";
		if (httpPostedFile != null)
		{
			try
			{
				string fileNewName;
				if (this._type == 2)
				{
					PersonalFileModel personalModel = this.GetPersonalModel(httpPostedFile);
					fileNewName = personalModel.FileNewName;
					new PersonalFileBll().Add(personalModel);
				}
				else
				{
					FileInfoModel fileModel = this.GetFileModel(httpPostedFile);
					fileNewName = fileModel.FileNewName;
					this.fileInfoBll.Add(fileModel);
				}
				if (!Directory.Exists(text))
				{
					Directory.CreateDirectory(text);
				}
				httpPostedFile.SaveAs(text + fileNewName);
				context.Response.Write("1");
				return;
			}
			catch (Exception)
			{
				return;
			}
		}
		context.Response.Write("0");
	}
	public FileInfoModel GetFileModel(HttpPostedFile file)
	{
		return new FileInfoModel
		{
			CreateTime = new DateTime?(DateTime.Now),
			FileOwner = this._userCode,
			UserCodes = this.GetUserCode(),
			FileType = "0",
			Id = Guid.NewGuid().ToString(),
			ParentId = this._fid,
			FileName = file.FileName,
			FileNewName = this.GetNewName(file.FileName),
			FileSize = file.ContentLength.ToString()
		};
	}
	public string GetUserCode()
	{
		if (this._userCode != "00000000")
		{
			return JsonHelper.Serialize(new string[]
			{
				this._userCode,
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
	public PersonalFileModel GetPersonalModel(HttpPostedFile file)
	{
		return new PersonalFileModel
		{
			CreateTime = new DateTime?(DateTime.Now),
			FileOwner = this._userCode,
			FileType = "0",
			Id = Guid.NewGuid().ToString(),
			ParentId = this._fid,
			FileName = file.FileName,
			FileNewName = this.GetNewName(file.FileName),
			FileSize = file.ContentLength.ToString(),
			ShareUsers = ""
		};
	}
}
