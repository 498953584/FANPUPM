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
public partial class BudgetManage_Resource_ResourceQuery : NBasePage, IRequiresSessionState
{
	public static cn.justwin.BLL.Resource resource = new cn.justwin.BLL.Resource();
	private string resTypeId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTv();
		}
	}
	public void BindTv()
	{
		System.DateTime arg_05_0 = System.DateTime.Now;
		TreeNode treeNode = new TreeNode("目录", "");
		treeNode.NavigateUrl = "ResourceQueryDetail.aspx?id=" + treeNode.Value;
		treeNode.Target = "InfoList";
		treeNode.Selected = true;
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
				treeNode2.NavigateUrl = "ResourceQueryDetail.aspx?id=" + treeNode2.Value;
				treeNode2.Target = "InfoList";
				treeNode.ChildNodes.Add(treeNode2);
				this.BindSubTree(treeNode2, treeNode2.Value);
			}
		}
		this.tvResource.Nodes.Add(treeNode);
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
				treeNode.NavigateUrl = "ResourceQueryDetail.aspx?id=" + treeNode.Value;
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
				treeNode.NavigateUrl = "ResourceQueryDetail.aspx?id=" + treeNode.Value;
				treeNode.Target = "InfoList";
				e.Node.ChildNodes.Add(treeNode);
			}
		}
	}
	public string pnode(TreeNode node)
	{
		if (node.Parent != null)
		{
			return this.pnode(node.Parent);
		}
		return node.Value;
	}
}


