using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_ResourceList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTv();
		}
	}
	public void BindTv()
	{
		TreeNode treeNode = new TreeNode("目录", "");
		treeNode.NavigateUrl = string.Concat(new string[]
		{
			"ResourceDetail.aspx?id=",
			treeNode.Value,
			"&nodeId=",
			treeNode.Value,
			"&parentId=",
			this.pnode(treeNode)
		});
		treeNode.Target = "InfoList";
		treeNode.Selected = true;
		this.tvResource.Nodes.Add(treeNode);
		if (base.Cache[CacheParameter.ResourceType] == null)
		{
			DataTable resType = cn.justwin.BLL.Resource.GetResType();
			base.Cache[CacheParameter.ResourceType] = resType;
		}
		using (new pm2Entities())
		{
			DataTable dataTable = base.Cache[CacheParameter.ResourceType] as DataTable;
			DataRow[] array = dataTable.Select(" ParentId is null ");
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Value = dataRow["ResourceTypeId"].ToString();
				treeNode2.Text = dataRow["ResourceTypeName"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
				treeNode2.NavigateUrl = string.Concat(new string[]
				{
					"ResourceDetail.aspx?id=",
					treeNode2.Value,
					"&nodeId=",
					treeNode2.Value,
					"&parentId=",
					this.pnode(treeNode2)
				});
				treeNode2.Target = "InfoList";
				this.BindSubTree(treeNode2, treeNode2.Value);
			}
		}
	}
	public void BindSubTree(TreeNode tree, string value)
	{
		using (new pm2Entities())
		{
			DataTable dataTable = base.Cache[CacheParameter.ResourceType] as DataTable;
			DataRow[] array = dataTable.Select("ParentId='" + value + "'");
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Value = dataRow["ResourceTypeId"].ToString();
				treeNode.Text = dataRow["ResourceTypeName"].ToString();
				tree.ChildNodes.Add(treeNode);
				treeNode.NavigateUrl = string.Concat(new string[]
				{
					"ResourceDetail.aspx?id=",
					treeNode.Value,
					"&nodeId=",
					treeNode.Value,
					"&parentId=",
					this.pnode(treeNode)
				});
				treeNode.Target = "InfoList";
				this.BindSubTree(treeNode, treeNode.Value);
			}
		}
	}
	protected void TreeView_Populate(object sender, TreeNodeEventArgs e)
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_ResourceType> queryable =
				from m in pm2Entities.Res_ResourceType
				where m.Res_ResourceType2.ResourceTypeId == e.Node.Value
				select m;
			foreach (Res_ResourceType current in queryable)
			{
				TreeNode treeNode = new TreeNode();
				if (ResType.IsContainsChild(current.ResourceTypeId))
				{
					treeNode.PopulateOnDemand = true;
				}
				treeNode.Value = current.ResourceTypeId;
				treeNode.Text = current.ResourceTypeName;
				e.Node.ChildNodes.Add(treeNode);
				treeNode.NavigateUrl = string.Concat(new string[]
				{
					"ResourceDetail.aspx?id=",
					treeNode.Value,
					"&nodeId=",
					treeNode.Value,
					"&parentId=",
					this.pnode(treeNode)
				});
				treeNode.Target = "InfoList";
			}
		}
	}
	public string pnode(TreeNode node)
	{
		if (node == null)
		{
			return string.Empty;
		}
		if (node.Parent == null)
		{
			return node.Value;
		}
		if (node.Parent.Value != "")
		{
			return this.pnode(node.Parent);
		}
		return node.Value;
	}
}


