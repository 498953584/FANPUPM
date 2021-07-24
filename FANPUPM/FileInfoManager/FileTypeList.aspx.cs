using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using com.jwsoft.pm.entpm;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class FileInfoManager_FileTypeList : NBasePage, IRequiresSessionState
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
			this.hfldManagerCode.Value = string.Empty;
		}
		else
		{
			this.hdUserCodes.Value = JsonHelper.Serialize(new string[]
			{
				"00000000"
			});
			this.hfldManagerCode.Value = base.UserCode;
		}
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		this.BindGv();
	}
	public void BindGv()
	{
		string selectedValue = this.tvFile.SelectedValue;
		int flodersCount = FileInfoBll.GetFlodersCount(selectedValue);
		this.AspNetPager1.RecordCount = flodersCount;
		DataTable folders = FileInfoBll.GetFolders(selectedValue, this.AspNetPager1.PageSize, this.AspNetPager1.CurrentPageIndex);
		Common2.BindGvTable(folders, this.gvFile, true);
	}
	public void BindTree()
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId = Id and FileType=2  and IsValid='false'  order by CreateTime desc");
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
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType=2  and IsValid='false'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = this.imageUrl(current.Id);
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			node.ChildNodes.Add(treeNode);
			if (base.Request.QueryString["id"] != null && base.Request.QueryString["id"] == current.Id)
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
	public string imageUrl(string id)
	{
		string result = "/images/tree/FileInfo/folderSubtract.png";
		if (this.fileInfoBll.GetListArray(" where parentId='" + id + "' AND FileType=2 AND IsValid=0").Count > 0)
		{
			result = "/images/tree/FileInfo/folderAdd.png";
		}
		return result;
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		string str = "";
		string value = this.hfldFolderName.Value;
		int count = this.fileInfoBll.GetListArray(string.Concat(new string[]
		{
			" where FileName='",
			value,
			"' and FileType!=0 and ParentId='",
			this.tvFile.SelectedValue,
			"'  and IsValid='false'"
		})).Count;
		if (this.hdType.Value == "add")
		{
			if (count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n 添加失败，此目录名称已存在！');");
				return;
			}
			FileInfoModel fileInfoModel = new FileInfoModel();
			fileInfoModel.CreateTime = new System.DateTime?(System.DateTime.Now);
			fileInfoModel.FileName = value;
			fileInfoModel.FileOwner = base.UserCode;
			fileInfoModel.FileSize = "";
			fileInfoModel.FileNewName = "";
			fileInfoModel.FileType = "2";
			fileInfoModel.Id = this.hdGuid.Value;
			fileInfoModel.UserCodes = this.hdUserCodes.Value;
			fileInfoModel.ParentId = ((this.tvFile.SelectedValue == "") ? this.hdGuid.Value : this.tvFile.SelectedValue);
			str = fileInfoModel.ParentId;
			this.fileInfoBll.Add(fileInfoModel);
		}
		else
		{
			FileInfoModel model = this.fileInfoBll.GetModel(this.hfldPurchaseChecked.Value);
			if (model != null)
			{
				if (count > 0 && model.FileName != value)
				{
					base.RegisterScript("alert('系统提示：\\n\\n 编辑失败，此目录名称已存在！');");
					return;
				}
				model.FileName = value;
				str = model.ParentId;
				this.fileInfoBll.Update(model);
			}
		}
		base.RegisterScript("location='FileTypeList.aspx?id=" + str + "'");
	}
	protected void tvFile_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.hdId.Value = this.tvFile.SelectedValue;
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		string value = this.hfldPurchaseChecked.Value;
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(value);
		try
		{
			foreach (string current in listFromJson)
			{
				FileInfoBll.MoveRecycle(current);
			}
			base.RegisterScript("alert('系统提示:\\n\\n 已成功删除，并放入回收站！');location='FileTypeList.aspx?id=" + this.tvFile.SelectedValue + "'");
		}
		catch
		{
			base.RegisterScript("alert('系统提示:\\n\\n 删除失败！');");
		}
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
	protected void SetLimit()
	{
		string value = this.hfldUserCodes.Value;
		string text = "";
		if (value != "")
		{
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>(value.Split(new char[]
			{
				','
			}));
			text = JsonHelper.Serialize(list.ToArray());
		}
		Common2.ExecuteNoQuery(string.Concat(new string[]
		{
			"UPDATE F_FileInfo SET UserCodes='",
			text,
			"' WHERE Id='",
			this.hfldPurchaseChecked.Value,
			"' "
		}));
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


