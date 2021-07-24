using ASP;
using cn.justwin.BLL;
using cn.justwin.fileInfoBll;
using cn.justwin.fileInfoModel;
using cn.justwin.PrjManager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_Completed_SetDirectory : NBasePage, IRequiresSessionState
{
	private FileInfoBll fileInfoBll = new FileInfoBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
			this.SetIfSrc();
		}
	}
	protected void SetIfSrc()
	{
		string text = this.hfldSelDriectoryId.Value;
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.Append("$('#ifFilesInfo').attr('src','/PrjManager/Completed/IFDirectoryFilesInfo.aspx");
		if (!string.IsNullOrEmpty(text))
		{
			text = text.Substring(0, text.Length - 1);
			stringBuilder.Append("?id=" + text + "');");
		}
		else
		{
			stringBuilder.Append("');");
		}
		base.RegisterScript(stringBuilder.ToString());
	}
	protected void BindTree()
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId = Id and FileType!=0  and IsValid='false'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Value = current.Id;
			treeNode.Text = current.FileName;
			treeNode.SelectAction = TreeNodeSelectAction.None;
			treeNode.ImageUrl = this.imageUrl(current, base.UserCode);
			this.AddNode(treeNode);
			this.tvFile.Nodes.Add(treeNode);
		}
		if (this.tvFile.Nodes.Count > 0)
		{
			this.tvFile.Nodes[0].ShowCheckBox = new bool?(false);
		}
	}
	protected string imageUrl(FileInfoModel var, string usercode)
	{
		return "";
	}
	protected TreeNode AddNode(TreeNode node)
	{
		System.Collections.Generic.List<FileInfoModel> listArray = this.fileInfoBll.GetListArray(" where ParentId='" + node.Value + "' and id != parentId and FileType!=0  and IsValid='false'  order by CreateTime desc");
		foreach (FileInfoModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.ImageUrl = this.imageUrl(current, base.UserCode);
			treeNode.Value = current.Id;
			if (current.UserCodes.Contains(base.UserCode))
			{
				treeNode.Text = current.FileName;
				treeNode.NavigateUrl = "#" + treeNode.Value + "#";
				treeNode.SelectAction = TreeNodeSelectAction.Select;
			}
			else
			{
				treeNode.Text = "<font color='#808080'>" + current.FileName + "</font>";
				treeNode.SelectAction = TreeNodeSelectAction.None;
				treeNode.ToolTip = "无权限";
			}
			node.ChildNodes.Add(treeNode);
			System.Collections.Generic.List<string> annexAddress = CompletedAnnex.GetAnnexAddress(base.Request["prjId"], base.Request["prjComId"]);
			if (annexAddress.Contains(treeNode.Value))
			{
				treeNode.Checked = true;
				this.ExpandSelectNode(treeNode);
				HiddenField expr_11F = this.hfldSelDriectoryId;
				expr_11F.Value = expr_11F.Value + treeNode.Value + ",";
				treeNode.Parent.Expand();
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
		try
		{
			TreeNodeCollection checkedNodes = this.tvFile.CheckedNodes;
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			foreach (TreeNode treeNode in checkedNodes)
			{
				list.Add(treeNode.Value);
			}
			string text = base.Request["prjId"];
			string text2 = base.Request["prjComId"];
			string type = base.Request["type"];
			CompletedAnnex.SetAnnex(text, text2, list, type);
			int num = 0;
			bool flag = CompletedAnnex.ExistAnnexAddress(text, text2);
			if (flag)
			{
				num = CompletedAnnex.GetFilesCount(text, text2);
			}
			base.RegisterScript(string.Concat(new object[]
			{
				"setParentAdunct('",
				text2,
				"','",
				text,
				"','",
				num,
				"','",
				flag,
				"');"
			}));
		}
		catch (System.Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n设置失败！');");
		}
	}
}


