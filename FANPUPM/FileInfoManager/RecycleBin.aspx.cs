using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class FileInfoManager_RecycleBin : NBasePage, IRequiresSessionState
{
	private FileInfoBll fileInfoBll = new FileInfoBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.gvFile.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		if (base.UserCode != "00000000")
		{
			this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
			{
				base.UserCode,
				"00000000"
			});
		}
		else
		{
			this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
			{
				"00000000"
			});
		}
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.BindGv();
	}
	public void BindGv()
	{
		int recycleRecord = FileInfoBll.GetRecycleRecord(base.UserCode);
		this.AspNetPager1.RecordCount = recycleRecord;
		DataTable recycleInfo = FileInfoBll.GetRecycleInfo(base.UserCode, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		this.gvFile.DataSource = recycleInfo;
		this.gvFile.DataBind();
	}
	public TreeNode AddNode(TreeNode node)
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType=2  and IsValid='False'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = FileInfoManager_RecycleBin.imageUrl(current.Id);
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			if (base.Request.QueryString["id"] != null && base.Request.QueryString["id"] == current.Id)
			{
				treeNode.Select();
			}
			node.ChildNodes.Add(treeNode);
			this.AddNode(treeNode);
		}
		return node;
	}
	public static string imageUrl(string id)
	{
		PersonalFileBll personalFileBll = new PersonalFileBll();
		string result = "/images/tree/FileInfo/folderSubtract.png";
		if (personalFileBll.GetListArray(" where parentId='" + id + "'").Count > 0)
		{
			result = "/images/tree/FileInfo/folderAdd.png";
		}
		return result;
	}
	protected void tvFile_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		try
		{
			string value = this.hfldPurchaseChecked.Value;
			System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
			foreach (string arg_23_0 in listFromJson)
			{
				this.DelFileAndAdjunct(listFromJson);
			}
			base.RegisterScript("alert('系统提示:\\n\\n 删除成功！');location=location;");
		}
		catch (System.Exception)
		{
			base.RegisterScript("alert('系统提示:\\n\\n 删除失败，竣工验收单正在使用！');");
		}
	}
	protected void DelFileAndAdjunct(System.Collections.Generic.List<string> lstId)
	{
		foreach (string current in lstId)
		{
			DataTable children = FileInfoBll.GetChildren(current);
			foreach (DataRow dataRow in children.Rows)
			{
				string text = dataRow["Id"].ToString();
				string a = dataRow["FileType"].ToString();
				if (a == "0")
				{
					string fileName = dataRow["FileNewName"].ToString();
					this.fileInfoBll.Delete(text);
					this.DelFile(fileName);
				}
				else
				{
					System.Collections.Generic.List<string> lstId2 = new System.Collections.Generic.List<string>
					{
						text
					};
					this.DelFileAndAdjunct(lstId2);
				}
			}
			this.fileInfoBll.Delete(current);
		}
	}
	protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex != -1 && this.gvFile.DataKeys[e.Row.RowIndex].Values[1].ToString() != "0")
		{
			e.Row.Attributes["id"] = this.gvFile.DataKeys[e.Row.RowIndex].Values[0].ToString();
		}
	}
	protected void gvFile_PageIndexChanging(object sender, GridViewPageEventArgs e)
	{
		this.gvFile.PageIndex = e.NewPageIndex;
		this.BindGv();
	}
	public new string GetUserName(string userCodes)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(userCodes);
		string text = "";
		foreach (string current in listFromJson)
		{
			text = text + PageHelper.QueryUser(this, current) + ",";
		}
		return text;
	}
	protected void btnRecover_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldPurchaseChecked.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
		try
		{
			foreach (string current in listFromJson)
			{
				FileInfoBll.Recover(current, base.UserCode);
			}
			base.RegisterScript("alert('系统提示:\\n\\n 恢复成功！');location=location;");
		}
		catch
		{
			base.RegisterScript("alert('系统提示:\\n\\n 恢复失败！');");
		}
	}
	protected void DelDirectory(System.Collections.Generic.List<string> ids)
	{
		string arrayToInStr = StringUtility.GetArrayToInStr(ids.ToArray());
		DataTable repeatDirectoryId = FileInfoBll.GetRepeatDirectoryId(arrayToInStr);
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		foreach (DataRow dataRow in repeatDirectoryId.Rows)
		{
			string item = dataRow["id"].ToString();
			list.Add(item);
		}
		if (repeatDirectoryId != null && repeatDirectoryId.Rows.Count > 0)
		{
			this.DelFileAndAdjunct(list);
		}
	}
	public void DelFile(string fileName)
	{
		string path = base.Server.MapPath("~/UploadFiles/FileInfo/" + fileName);
		if (System.IO.File.Exists(path))
		{
			System.IO.File.Delete(path);
		}
	}
	public string GetFileName(string FileName, string FileType, string fileNewName)
	{
		string text = "";
		if (FileType == "0" && FileName.IndexOf(".") != -1)
		{
			text = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
		}
		FileTypeBll fileTypeBll = new FileTypeBll();
		string strWhere;
		if (FileType == "1" || FileType == "2")
		{
			strWhere = " where TypeSuffix='.folder'";
		}
		else
		{
			if (string.IsNullOrEmpty(FileName) || FileName.LastIndexOf(".") == -1)
			{
				strWhere = " where TypeSuffix='.other'";
			}
			else
			{
				FileName.LastIndexOf('.');
				strWhere = " where TypeSuffix='" + text + "'";
			}
		}
		System.Collections.Generic.List<FileTypeModel> listArray = fileTypeBll.GetListArray(strWhere);
		string text2;
		if (listArray.Count > 0)
		{
			text2 = string.Concat(new string[]
			{
				"<img src='",
				listArray[0].TypeImg,
				"' style='border:0px;float: left;padding: 5px 5px 0px 0px;'  /><span style='float: left;padding-top: 5px;'>",
				StringUtility.GetStr(FileName, 25),
				"</span>"
			});
		}
		else
		{
			listArray = fileTypeBll.GetListArray(" where TypeSuffix='.other'");
			text2 = string.Concat(new string[]
			{
				"<img src='",
				listArray[0].TypeImg,
				"' style='border:0px;float: left;padding: 5px 5px 0px 0px;' /><span style='float: left;padding-top: 5px;'>",
				StringUtility.GetStr(FileName, 25),
				"</span>"
			});
		}
		if (FileType != "0")
		{
			text2 = string.Concat(new string[]
			{
				"<span class='tooltip' title='",
				FileName,
				"' dfTitle='",
				FileName,
				"'  >",
				text2,
				" </span>"
			});
		}
		else
		{
			if (text == ".jpg" || text == ".gif" || text == ".html" || text == ".htm" || text == ".txt")
			{
				text2 = string.Concat(new string[]
				{
					"<span class='tooltip' title='",
					FileName,
					"' dfTitle='",
					FileName,
					"' >",
					text2,
					" </span>"
				});
			}
			else
			{
				text2 = string.Concat(new string[]
				{
					"<span class='tooltip' title='",
					FileName,
					"' fTitle='",
					FileName,
					"' >",
					text2,
					" </span>"
				});
			}
		}
		return text2;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


