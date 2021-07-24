using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using com.jwsoft.pm.entpm;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class FileInfoManager_PersonalFileList : NBasePage, IRequiresSessionState
{
	private PersonalFileBll personalFileBll = new PersonalFileBll();

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
		this.SearchSet();
		this.FileUpload2.ScriptData = string.Concat(new string[]
		{
			"[{'ucode':'",
			base.UserCode,
			"', 'fid': '",
			this.tvFile.SelectedValue,
			"', 'type': '2'}]"
		});
	}
	public void InitPage()
	{
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.hdSeleValue.Value = base.Request["id"];
		}
		this.addModelByFistr();
		this.BindTree();
		this.BindDdl();
		if (base.Request.QueryString["id"] == null)
		{
			this.tvFile.Nodes[0].Select();
		}
		this.ViewState["search"] = "0";
		if (!string.IsNullOrEmpty(base.Request["search"]))
		{
			this.ViewState["search"] = base.Request["search"];
		}
		this.hdUser.Value = base.UserCode;
		this.hdGuid.Value = System.Guid.NewGuid().ToString();
		this.hdSeleValue.Value = this.tvFile.SelectedValue;
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.BindGv();
	}
	public void SearchSet()
	{
		if (this.hdSearchShow.Value == "0")
		{
			this.searchTr.Attributes.Add("style", "display:none;");
			this.btnS.Attributes.Add("style", "display:block;cursor: pointer;");
			this.heightS.Attributes.Add("style", "display:block;cursor: pointer;");
			return;
		}
		this.searchTr.Attributes.Add("style", "display:block;");
		this.btnS.Attributes.Add("style", "display:none;");
		this.heightS.Attributes.Add("style", "display:none;");
	}
	public void BindDdl()
	{
		this.ddlScope.DataSource = this.personalFileBll.GetListArray(" where FileType!=0 and  fileOwner='" + base.UserCode + "'  order by CreateTime asc");
		this.ddlScope.DataBind();
		if (!string.IsNullOrEmpty(base.Request["did"]))
		{
			this.ddlScope.SelectedValue = base.Request["did"];
		}
	}
	public void addModelByFistr()
	{
		if (this.personalFileBll.GetListArray(" where fileOwner='" + base.UserCode + "'").Count == 0)
		{
			string text = System.Guid.NewGuid().ToString();
			PersonalFileModel model = this.GetModel();
			model.FileName = "目录";
			model.Id = text;
			model.ParentId = text;
			model.ShareUsers = "";
			this.personalFileBll.Add(model);
		}
	}
	public void BindGv()
	{
		string selectedValue = this.ddlScope.SelectedValue;
		bool isGetAll = true;
		bool isShare = false;
		if (this.ViewState["search"].ToString() == "0")
		{
			selectedValue = this.tvFile.SelectedValue;
			isGetAll = false;
		}
		this.btnAdd.Disabled = false;
		this.FileUpload2.Visible = true;
		int recordCount;
		DataTable storageDataSource;
		if (selectedValue == "0")
		{
			this.btnAdd.Disabled = true;
			this.FileUpload2.Visible = false;
			string shareFolderIds = (this.ViewState["shareId"] == null) ? string.Empty : this.ViewState["shareId"].ToString();
			recordCount = PersonalFileBll.GetShareFolersCount(shareFolderIds);
			storageDataSource = PersonalFileBll.GetShareFolders(shareFolderIds, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		}
		else
		{
			PersonalFileModel model = this.personalFileBll.GetModel(selectedValue);
			if (model != null)
			{
				if (model.FileOwner != base.UserCode)
				{
					this.btnAdd.Disabled = true;
					this.FileUpload2.Visible = false;
					isShare = true;
				}
				if (this.tvFile.Nodes[0].Value == model.Id)
				{
					this.FileUpload2.Visible = false;
				}
			}
			recordCount = PersonalFileBll.GetAllFilesCount(selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, this.txtFileName.Text, WebUtil.GetBSize(this.txtStartSize.Text), WebUtil.GetBSize(this.txtEndSize.Text), base.UserCode, isGetAll, isShare);
			storageDataSource = PersonalFileBll.GetAllFiles(selectedValue, this.txtStartTime.Text, this.txtEndTime.Text, this.txtFileName.Text, WebUtil.GetBSize(this.txtStartSize.Text), WebUtil.GetBSize(this.txtEndSize.Text), base.UserCode, isGetAll, isShare, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		}
		this.AspNetPager1.RecordCount = recordCount;
		Common2.BindGvTable(storageDataSource, this.gvFile, true);
	}
	public void BindTree()
	{
		string value = this.hdSeleValue.Value;
		System.Collections.Generic.List<PersonalFileModel> listArray = this.personalFileBll.GetListArray(" where ParentId = Id and FileType =1 and FileOwner='" + base.UserCode + "'  order by CreateTime desc");
		foreach (PersonalFileModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			if (value == current.Id)
			{
				treeNode.Select();
			}
			treeNode.ImageUrl = "/images/tree/FileInfo/folder.gif";
			this.AddNode(treeNode);
			this.tvFile.Nodes.Add(treeNode);
		}
		TreeNode treeNode2 = new TreeNode("共享文件", "0", "/images/tree/FileInfo/folder.gif");
		if (value == "0")
		{
			treeNode2.Select();
		}
		if (value == "0" || base.Request["limit"] == "0")
		{
			this.FileUpload2.Visible = false;
		}
		System.Collections.Generic.List<PersonalFileModel> listArray2 = this.personalFileBll.GetListArray(" where fileType!=0 and ShareUsers like'%" + base.UserCode + "%'  order by CreateTime desc");
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		foreach (PersonalFileModel current2 in listArray2)
		{
			TreeNode treeNode3 = new TreeNode();
			treeNode3.Value = current2.Id;
			treeNode3.Text = current2.FileName;
			treeNode3.ImageUrl = this.ShareImageUrl(current2.Id);
			if (value == current2.Id)
			{
				treeNode3.Select();
				this.FileUpload2.Visible = false;
			}
			if (!this.selectShare(treeNode3.Value))
			{
				list.Add(treeNode3.Value);
				treeNode2.ChildNodes.Add(treeNode3);
			}
		}
		this.ViewState["shareId"] = StringUtility.GetArrayToInStr(list.ToArray());
		this.tvFile.Nodes.AddAt(1, treeNode2);
		foreach (TreeNode node in treeNode2.ChildNodes)
		{
			this.AddShareChildFolder(node);
		}
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
	protected string ShareImageUrl(string id)
	{
		PersonalFileBll personalFileBll = new PersonalFileBll();
		string result = "/images/tree/FileInfo/folderSubtract.png";
		if (personalFileBll.GetListArray(string.Concat(new string[]
		{
			" where parentId='",
			id,
			"' AND ShareUsers like'%",
			base.UserCode,
			"%'"
		})).Count > 0)
		{
			result = "/images/tree/FileInfo/folderAdd.png";
		}
		return result;
	}
	public bool selectShare(string id)
	{
		PersonalFileModel model = this.personalFileBll.GetModel(id);
		string parentId = model.ParentId;
		if (parentId == id)
		{
			return false;
		}
		PersonalFileModel model2 = this.personalFileBll.GetModel(parentId);
		string shareUsers = model2.ShareUsers;
		return !model.ShareUsers.Contains(base.UserCode) || shareUsers.Contains(base.UserCode) || this.selectShare(parentId);
	}
	protected void AddShareChildFolder(TreeNode node)
	{
		string value = node.Value;
		System.Collections.Generic.List<PersonalFileModel> listArray = this.personalFileBll.GetListArray(string.Concat(new string[]
		{
			" WHERE  parentId='",
			value,
			"' AND fileType='1' AND ShareUsers LIKE '%",
			base.UserCode,
			"%'order by CreateTime DESC"
		}));
		if (listArray != null || listArray.Count > 0)
		{
			foreach (PersonalFileModel current in listArray)
			{
				TreeNode treeNode = new TreeNode(current.FileName, current.Id);
				treeNode.ImageUrl = this.ShareImageUrl(current.Id);
				node.ChildNodes.Add(treeNode);
				if (this.hdSeleValue.Value == current.Id)
				{
					treeNode.Select();
					this.ExpandSelectNode(treeNode);
				}
				this.AddShareChildFolder(treeNode);
			}
		}
	}
	public TreeNode AddNode(TreeNode node)
	{
		System.Collections.Generic.List<PersonalFileModel> listArray = this.personalFileBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType!=0 order by CreateTime desc ");
		foreach (PersonalFileModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = FileInfoManager_PersonalFileList.imageUrl(current.Id);
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			node.ChildNodes.Add(treeNode);
			if (this.hdSeleValue.Value == current.Id)
			{
				treeNode.Select();
				this.ExpandSelectNode(treeNode);
			}
			this.AddNode(treeNode);
		}
		return node;
	}
	protected void ExpandSelectNode(TreeNode node)
	{
		if (node != null)
		{
			TreeNode parent = node.Parent;
			if (node.Depth == 1 || parent == null)
			{
				node.ExpandAll();
				return;
			}
			if (parent.Depth == 1)
			{
				parent.ExpandAll();
				return;
			}
			this.ExpandSelectNode(parent);
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		PersonalFileModel model = this.GetModel();
		string text = this.hfldFolderName.Value;
		int count = this.personalFileBll.GetListArray(string.Concat(new string[]
		{
			" where FileName='",
			text,
			"' and FileType!=0 and ParentId='",
			model.ParentId,
			"'"
		})).Count;
		if (this.hdType.Value == "add")
		{
			if (count > 0)
			{
				base.RegisterLoadEvent("function(){alert('系统提示：\\n\\n 添加失败，此目录名称已存在！');}");
				return;
			}
			this.personalFileBll.Add(model);
		}
		else
		{
			PersonalFileModel model2 = this.personalFileBll.GetModel(this.hfldPurchaseChecked.Value);
			if (count > 0 && this.txtFileInfo.Text.Trim() != model2.FileName)
			{
				base.RegisterLoadEvent("function(){alert('系统提示：\\n\\n 编辑失败，此目录名称已存在！');}");
				return;
			}
			if (model2.FileType == "0")
			{
				string text2 = model2.FileName.Substring(model2.FileName.LastIndexOf('.'));
				if (text.LastIndexOf('.') != -1)
				{
					if (text.Substring(text.LastIndexOf('.')) != text2)
					{
						text += text2;
					}
				}
				else
				{
					text += text2;
				}
				model2.FileName = text;
			}
			else
			{
				model2.FileName = text;
			}
			this.personalFileBll.Update(model2);
		}
		base.RegisterScript("location.href='PersonalFileList.aspx?id=" + this.tvFile.SelectedValue + "'");
	}
	public PersonalFileModel GetModel()
	{
		return new PersonalFileModel
		{
			CreateTime = new System.DateTime?(System.DateTime.Now),
			FileName = this.hfldFolderName.Value,
			FileNewName = "",
			FileOwner = base.UserCode,
			FileSize = "",
			FileType = "1",
			Id = this.hdGuid.Value,
			ParentId = (this.tvFile.SelectedValue == "") ? this.hdGuid.Value : this.tvFile.SelectedValue,
			ShareUsers = ""
		};
	}
	protected void tvFile_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		try
		{
			this.ddlScope.SelectedValue = this.tvFile.SelectedValue;
		}
		catch
		{
		}
		this.ViewState["search"] = "0";
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
		this.hdSeleValue.Value = this.tvFile.SelectedValue;
	}
	protected void gvFile_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex != -1 && this.gvFile.DataKeys[e.Row.RowIndex].Values[1].ToString() != "0")
		{
			e.Row.Attributes["id"] = this.gvFile.DataKeys[e.Row.RowIndex]["id"].ToString();
			e.Row.Attributes["fileType"] = this.gvFile.DataKeys[e.Row.RowIndex]["FileType"].ToString();
			string b = this.gvFile.DataKeys[e.Row.RowIndex]["FileOwner"].ToString();
			string text = "只读";
			string value = "0";
			if (base.UserCode == "00000000" || base.UserCode == b)
			{
				text = "读/写";
				value = "1";
			}
			e.Row.Cells[e.Row.Cells.Count - 1].Text = text;
			e.Row.Attributes["limit"] = value;
		}
	}
	public string GetFileType(string FileName, string FileType)
	{
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
				string str = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
				strWhere = " where TypeSuffix='" + str + "'";
			}
		}
		System.Collections.Generic.List<FileTypeModel> listArray = fileTypeBll.GetListArray(strWhere);
		if (listArray.Count > 0)
		{
			return listArray[0].TypeName;
		}
		listArray = fileTypeBll.GetListArray(" where TypeSuffix='.other'");
		return listArray[0].TypeName;
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
				"' style='border:0px;float: left;padding: 5px 5px 0px 0px;'  /><span style='float: left;padding-top: 5px; text-decoration:underline;'>",
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
				"' style='border:0px;float: left;padding: 5px 5px 0px 0px;' /><span style='float: left;padding-top: 5px; text-decoration:underline;'>",
				StringUtility.GetStr(FileName, 25),
				"</span>"
			});
		}
		if (FileType != "0")
		{
			text2 = string.Concat(new string[]
			{
				"<a class='tooltip' title='",
				FileName,
				"'dfTitle='",
				FileName,
				"' href='#' onclick='ondbSelectValue(this.parentNode.parentNode.id)'>",
				text2,
				" </a>"
			});
		}
		else
		{
			if (text == ".jpg" || text == ".gif" || text == ".html" || text == ".htm" || text == ".txt")
			{
				text2 = string.Concat(new string[]
				{
					"<a class='tooltip' title='",
					FileName,
					"'dfTitle='",
					FileName,
					"' href='#' onclick=\"DownLoad('/UploadFiles/FileInfo/",
					fileNewName,
					"')\">",
					text2,
					" </a>"
				});
			}
			else
			{
				text2 = string.Concat(new string[]
				{
					"<a class='tooltip' title='",
					FileName,
					"'dfTitle='",
					FileName,
					"' href='/UploadFiles/FileInfo/",
					fileNewName,
					"' target='_blank' >",
					text2,
					" </a>"
				});
			}
		}
		return text2;
	}
	public string GetLookFile(string FileName, string fileType, string fileNewName)
	{
		if (fileType == "0")
		{
			try
			{
				string a = FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'));
				if (a == ".jpg" || a == ".gif" || a == ".html" || a == ".htm" || a == ".txt")
				{
					return "<a href='/UploadFiles/FileInfo/" + fileNewName + "' target='_blank'>在线预览</a>";
				}
			}
			catch
			{
			}
		}
		return "";
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		bool flag = true;
		string value = this.hfldPurchaseChecked.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
		foreach (string current in listFromJson)
		{
			int count = this.personalFileBll.GetListArray(string.Concat(new string[]
			{
				" where (parentId!=id and parentId='",
				current,
				"') or (fileType='2' and id='",
				current,
				"')"
			})).Count;
			if (count > 0)
			{
				flag = false;
				break;
			}
		}
		if (!flag)
		{
			base.RegisterLoadEvent("function() {alert('系统提示: \\n\\n 此文件夹不可删除,请检查是否包含子项！')}");
			return;
		}
		try
		{
			this.DelFile(listFromJson);
			int num = Common2.DelByStrWhere("F_PersonalFile", " where id in(" + StringUtility.GetArrayToInStr(listFromJson.ToArray()) + ")");
			if (num > 0)
			{
				base.RegisterLoadEvent(string.Concat(new object[]
				{
					"function() {alert('系统提示: \\n\\n 删除成功！');location='PersonalFileList.aspx?id=",
					this.tvFile.SelectedValue,
					"&did=",
					this.ddlScope.SelectedValue,
					"&search=",
					this.ViewState["search"],
					"'}"
				}));
			}
			else
			{
				base.RegisterScript("alert('系统提示: \\n\\n 删除失败！');");
			}
		}
		catch
		{
			base.RegisterScript("alert('系统提示: \\n\\n 出现异常删除失败！');");
		}
	}
	public void DelFile(System.Collections.Generic.List<string> listId)
	{
		foreach (string current in listId)
		{
			if (!string.IsNullOrEmpty(current))
			{
				PersonalFileModel model = this.personalFileBll.GetModel(current);
				if (model.FileType == "0")
				{
					string path = base.Server.MapPath("~/UploadFiles/FileInfo/" + model.FileNewName);
					if (System.IO.File.Exists(path))
					{
						System.IO.File.Delete(path);
					}
				}
			}
		}
	}
	protected void btn_Search_Click(object sender, System.EventArgs e)
	{
		this.ViewState["search"] = "1";
		this.AspNetPager1.CurrentPageIndex = 1;
		this.hdSeleValue.Value = this.ddlScope.SelectedValue;
		this.BindGv();
	}
	public string GetShare(string shareUsers)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(shareUsers);
		string text = "";
		foreach (string current in listFromJson)
		{
			text = text + PageHelper.QueryUser(this, current) + ",";
		}
		return text;
	}
	protected void btnReturn_Click(object sender, System.EventArgs e)
	{
		if (base.Request["id"] == null || base.Request["id"] == "0")
		{
			base.RegisterScript("alert('系统提示：\\n\\n 此目录为顶级目录')");
			return;
		}
		PersonalFileModel model = this.personalFileBll.GetModel(base.Request.QueryString["id"]);
		base.RegisterScript("location='PersonalFileList.aspx?id=" + model.ParentId + "'");
	}
	protected void btnUpLoad_Click(object sender, System.EventArgs e)
	{
	}
	protected void btnClickFile_Click(object sender, System.EventArgs e)
	{
	}
	protected void SetLimit()
	{
		string value = this.hfldUserCodes.Value;
		string shareUser = "";
		if (value != "")
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(value.Split(new char[]
			{
				','
			}));
			shareUser = JsonHelper.Serialize(list.ToArray());
		}
		PersonalFileBll.UpdateFolderShareUser(this.hfldPurchaseChecked.Value, shareUser);
		this.BindGv();
	}
	protected void btnUpdateUserCodes_Click(object sender, System.EventArgs e)
	{
		this.SetLimit();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


