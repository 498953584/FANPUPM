using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using com.jwsoft.web.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class FileInfoManager_FileInfoList : NBasePage, IRequiresSessionState
{
	private FileInfoBll fileInfoBll = new FileInfoBll();

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
			"', 'type': '1'}]"
		});
	}
	public void SearchSet()
	{
		if (this.hdSearchShow.Value == "0")
		{
			this.searchTr.Attributes.Add("style", "display:none;");
			this.btnS.Attributes.Add("style", "display:block;cursor: pointer;");
			this.heightS.Attributes.Add("style", "display:block;cursor: pointer;");
		}
		else
		{
			this.searchTr.Attributes.Add("style", "display:block;");
			this.btnS.Attributes.Add("style", "display:none;");
			this.heightS.Attributes.Add("style", "display:none;");
		}
		if (this.tvFile.Nodes.Count > 0 && this.tvFile.SelectedValue == this.tvFile.Nodes[0].Value)
		{
			this.FileUpload2.Visible = false;
			return;
		}
		this.FileUpload2.Visible = true;
	}
	public void InitPage()
	{
		this.BindTree();
		this.BindDdl();
		if (this.tvFile.Nodes.Count > 0 && this.tvFile.SelectedValue == "")
		{
			this.tvFile.Nodes[0].Select();
			this.tvFile.Nodes[0].ImageUrl = "/images/tree/FileInfo/folder.gif";
		}
		this.ViewState["search"] = "0";
		if (!string.IsNullOrEmpty(base.Request["search"]))
		{
			this.ViewState["search"] = base.Request["search"];
		}
		this.hdUser.Value = base.UserCode;
		this.hdGuid.Value = System.Guid.NewGuid().ToString();
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.BindGv();
		this.hdSeleValue.Value = this.tvFile.SelectedValue;
		if (base.UserCode != "00000000")
		{
			this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
			{
				base.UserCode,
				"00000000"
			});
			this.hfldManagerCode.Value = string.Empty;
			return;
		}
		this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
		{
			"00000000"
		});
		this.hfldManagerCode.Value = base.UserCode;
	}
	public void BindGv()
	{
		string selectedValue = this.ddlScope.SelectedValue;
		bool isGetAll = true;
		if (this.ViewState["search"].ToString() == "0")
		{
			selectedValue = this.tvFile.SelectedValue;
			isGetAll = false;
		}
		int count = this.fileInfoBll.GetListArray(string.Concat(new string[]
		{
			" where id='",
			selectedValue,
			"'and ( userCodes like '%",
			base.UserCode,
			"%' or userCodes='00000000' )and IsValid='false'  order by CreateTime desc"
		})).Count;
		if (count > 0)
		{
			this.btn_Search.Enabled = true;
			this.lblMsgAleave.Text = "";
			int allFilesCount = FileInfoBll.GetAllFilesCount(selectedValue, base.UserCode, this.txtStartTime.Text, this.txtEndTime.Text, this.txtFileName.Text.Trim(), WebUtil.GetBSize(this.txtStartSize.Text), WebUtil.GetBSize(this.txtEndSize.Text), this.txtFileOwner.Text.Trim(), isGetAll);
			this.AspNetPager1.RecordCount = allFilesCount;
			DataTable allFiles = FileInfoBll.GetAllFiles(selectedValue, base.UserCode, this.txtStartTime.Text, this.txtEndTime.Text, this.txtFileName.Text.Trim(), WebUtil.GetBSize(this.txtStartSize.Text), WebUtil.GetBSize(this.txtEndSize.Text), this.txtFileOwner.Text.Trim(), isGetAll, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
			Common2.BindGvTable(allFiles, this.gvFile, true);
			return;
		}
		this.gvFile.DataBind();
		this.btn_Search.Enabled = false;
		this.FileUpload2.Visible = false;
		this.lblMsgAleave.Text = "<div style='color:Red; margin-left:20px; margin-top:20px; '>您没有该目录的权限，请与系统管理员联系。</div>";
	}
	public void BindDdl()
	{
		DataTable treeNode = this.GetTreeNode();
		this.ddlScope.DataSource = treeNode;
		this.ddlScope.DataBind();
		if (!string.IsNullOrEmpty(base.Request["did"]))
		{
			this.ddlScope.SelectedValue = base.Request["did"];
		}
	}
	protected DataTable GetTreeNode()
	{
		DataTable dataTable = new DataTable();
		dataTable.Columns.Add("FileName");
		dataTable.Columns.Add("Id");
		string b = "/images/tree/FileInfo/folder.gif";
		foreach (TreeNode treeNode in this.tvFile.Nodes)
		{
			if (treeNode.ImageUrl == b)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["FileName"] = treeNode.Text;
				dataRow["Id"] = treeNode.Value;
				dataTable.Rows.Add(dataRow);
			}
			this.AddChildNodes(treeNode, dataTable);
		}
		return dataTable;
	}
	protected void AddChildNodes(TreeNode node, DataTable dtSource)
	{
		string b = "/images/tree/FileInfo/folderAdd2.png";
		string b2 = "/images/tree/FileInfo/folderSubtract2.png";
		foreach (TreeNode treeNode in node.ChildNodes)
		{
			DataRow dataRow = dtSource.NewRow();
			if (treeNode.ImageUrl != b && treeNode.ImageUrl != b2)
			{
				dataRow["FileName"] = treeNode.Text;
				dataRow["Id"] = treeNode.Value;
				dtSource.Rows.Add(dataRow);
			}
			if (treeNode.ChildNodes.Count > 0)
			{
				this.AddChildNodes(treeNode, dtSource);
			}
		}
	}
	public void BindTree()
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId = Id and FileType!=0  and IsValid=0  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			if (base.Request.QueryString["id"] != null && base.Request.QueryString["id"] == current.Id)
			{
				treeNode.Select();
			}
			treeNode.ImageUrl = "/images/tree/FileInfo/folder.gif";
			this.AddNode(treeNode);
			this.tvFile.Nodes.Add(treeNode);
		}
	}
	public TreeNode AddNode(TreeNode node)
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType!=0  and IsValid=0  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = FileInfoManager_FileInfoList.imageUrl(current, base.UserCode);
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			node.ChildNodes.Add(treeNode);
			if (base.Request["id"] == current.Id)
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
	public static string imageUrl(FileInfoModel var, string usercode)
	{
		FileInfoBll fileInfoBll = new FileInfoBll();
		string result;
		if (!var.UserCodes.Contains(usercode))
		{
			result = "/images/tree/FileInfo/folderSubtract2.png";
			if (fileInfoBll.GetListArray(" where parentId='" + var.Id + "' AND IsValid=0 ").Count > 0)
			{
				result = "/images/tree/FileInfo/folderAdd2.png";
			}
		}
		else
		{
			result = "/images/tree/FileInfo/folderSubtract.png";
			if (fileInfoBll.GetListArray(" where parentId='" + var.Id + "'  AND IsValid=0 ").Count > 0)
			{
				result = "/images/tree/FileInfo/folderAdd.png";
			}
		}
		return result;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string text = this.hfldFIleName.Value;
		FileInfoModel model = this.fileInfoBll.GetModel(this.hfldPurchaseChecked.Value);
		if (model.FileType == "0")
		{
			string text2 = model.FileName.Substring(model.FileName.LastIndexOf('.'));
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
			model.FileName = text;
		}
		this.fileInfoBll.Update(model);
		string arg_9B_0 = model.ParentId;
		this.BindGv();
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
			e.Row.Attributes["id"] = this.gvFile.DataKeys[e.Row.RowIndex].Values[0].ToString();
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
		if (string.IsNullOrEmpty(FileName))
		{
			return "";
		}
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
				"' dfTitle='",
				FileName,
				"' href='#' style='text-decoration:underline;' onclick='ondbSelectValue(this.parentNode.parentNode.id)'>",
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
					"' dfTitle='",
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
					"' dfTitle='",
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
		string value = this.hfldPurchaseChecked.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
		string text = "已成功删除，并放入回收站！";
		try
		{
			foreach (string current in listFromJson)
			{
				FileInfoBll.MoveRecycle(current);
			}
		}
		catch
		{
			text = "删除失败！";
		}
		base.RegisterLoadEvent(string.Concat(new object[]
		{
			"function() {alert('系统提示：\\n\\n",
			text,
			"');location='FileInfoList.aspx?id=",
			this.tvFile.SelectedValue,
			"&did=",
			this.ddlScope.SelectedValue,
			"&search=",
			this.ViewState["search"],
			"'}"
		}));
	}
	public void DelFile(System.Collections.Generic.List<string> listId)
	{
		foreach (string current in listId)
		{
			if (!string.IsNullOrEmpty(current))
			{
				FileInfoModel model = this.fileInfoBll.GetModel(current);
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
		this.BindGv();
	}
	private void DownLoad(string path)
	{
		path = base.Server.MapPath(path);
		if (path != null && System.IO.File.Exists(path))
		{
			System.IO.FileInfo fileInfo = new System.IO.FileInfo(path);
			base.Response.Clear();
			base.Response.AddHeader("Content-Disposition", "attachment;   filename=" + HttpUtility.UrlEncode(fileInfo.Name, System.Text.Encoding.UTF8));
			base.Response.AddHeader("Content-Length", fileInfo.Length.ToString());
			base.Response.ContentType = "application/octet-stream";
			base.Response.WriteFile(fileInfo.FullName);
			base.Response.End();
		}
	}
	protected void btnDown_Click(object sender, System.EventArgs e)
	{
		this.DownLoad(this.hdPath.Value);
	}
	protected void btnUpLoad_Click(object sender, System.EventArgs e)
	{
		base.RegisterScript("location='FileInfoList.aspx?id=" + this.tvFile.SelectedValue + "'");
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


