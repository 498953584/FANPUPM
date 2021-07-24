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
public partial class FileInfoManager_FileDirectory : NBasePage, IRequiresSessionState
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
		this.BindTree();
		this.hdId.Value = this.tvFile.SelectedValue;
		this.hdGuid.Value = System.Guid.NewGuid().ToString();
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
		this.BindGv();
	}
	public void BindGv()
	{
		Common2.BindGvTable(Common2.GetTable("F_FileInfo", " where ParentId='" + this.tvFile.SelectedValue + "' and ParentId!=Id  and FileType=2 and IsValid='True'  order by CreateTime desc"), this.gvFile, true);
	}
	public void BindTree()
	{
		this.tvFile.Nodes.Clear();
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId = Id and FileType=2  and IsValid='False'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			if (listArray[0] == current)
			{
				treeNode.Select();
			}
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
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType=2  and IsValid='False'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = FileInfoManager_FileDirectory.imageUrl(current.Id);
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
		this.hdId.Value = this.tvFile.SelectedValue;
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
			this.BindGv();
			base.RegisterScript("location.href='FileDirectory.aspx?id=" + this.tvFile.SelectedValue + "';alert('系统提示:\\n\\n 删除成功！');");
		}
		catch (System.Exception)
		{
			base.RegisterScript("alert('系统提示:\\n\\n 删除失败！');");
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
	protected void btnReturn_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldPurchaseChecked.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
		this.DelDirectory(listFromJson);
		if (listFromJson != null)
		{
			string arg_34_0 = this.fileInfoBll.GetModel(listFromJson[0]).ParentId;
			int num = Common2.ExecuteNoQuery("update F_FileInfo set IsValid='False'  where id in(" + StringUtility.GetArrayToInStr(listFromJson.ToArray()) + ")");
			if (num > 0)
			{
				base.RegisterScript("location.href='FileDirectory.aspx?id=" + this.tvFile.SelectedValue + "';alert('系统提示:\\n\\n 恢复成功！');");
			}
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
}


