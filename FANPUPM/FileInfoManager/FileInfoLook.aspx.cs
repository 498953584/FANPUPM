using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using cn.justwin.PrjManager;
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
			return;
		}
		this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
		{
			"00000000"
		});
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
		this.lblMsgAleave.Text = "<div style='color:Red; margin-left:20px; margin-top:20px; '>您没有该目录的权限，请与系统管理员联系。</div>";
	}
	public void BindDdl()
	{
		DataTable treeNode = this.GetTreeNode();
		this.ddlScope.DataSource = treeNode;
		this.ddlScope.DataBind();
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
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId = Id and FileType!=0  and IsValid='false'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			treeNode.ImageUrl = "/images/tree/FileInfo/folder.gif";
			this.AddNode(treeNode);
			this.tvFile.Nodes.Add(treeNode);
		}
	}
	public TreeNode AddNode(TreeNode node)
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType!=0  and IsValid='false'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = FileInfoManager_FileInfoList.imageUrl(current, base.UserCode);
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			node.ChildNodes.Add(treeNode);
			if (this.ViewState["findDirectoryId"] == null)
			{
				string text = base.Request["prjId"];
				if (!string.IsNullOrEmpty(text))
				{
					System.Collections.Generic.List<string> annexAddress = CompletedAnnex.GetAnnexAddress(text, base.Request["prjComId"]);
					if (annexAddress.Contains(treeNode.Value))
					{
						treeNode.Select();
						this.ExpandSelectNode(node);
						this.ViewState["findDirectoryId"] = node.Value;
					}
				}
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
			if (fileInfoBll.GetListArray(" where parentId='" + var.Id + "' AND IsValid=0 ").Count > 0)
			{
				result = "/images/tree/FileInfo/folderAdd.png";
			}
		}
		return result;
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
			e.Row.Attributes["name"] = this.gvFile.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["user"] = this.gvFile.DataKeys[e.Row.RowIndex].Values[2].ToString();
			e.Row.Attributes["fileType"] = this.gvFile.DataKeys[e.Row.RowIndex].Values[3].ToString();
			string b = this.gvFile.DataKeys[e.Row.RowIndex]["FileOwner"].ToString();
			string text = "只读";
			if (base.UserCode == "00000000" || base.UserCode == b)
			{
				text = "读/写";
			}
			e.Row.Cells[e.Row.Cells.Count - 1].Text = text;
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
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
}


