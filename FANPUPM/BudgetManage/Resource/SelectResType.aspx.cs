using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class BudgetManage_Resource_SelectResType : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		this.BindTv();
	}
	public void BindTv()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_ResourceType> queryable =
				from m in pm2Entities.Res_ResourceType
				where m.Res_ResourceType2.ResourceTypeId == null
				select m;
			foreach (Res_ResourceType current in queryable)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.PopulateOnDemand = true;
				treeNode.Value = current.ResourceTypeId;
				treeNode.Text = current.ResourceTypeName;
				treeNode.NavigateUrl = "javascript:saveSelectedValue('" + treeNode.Value + "');";
				this.tvResource.Nodes.Add(treeNode);
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
				treeNode.NavigateUrl = "javascript:saveSelectedValue('" + treeNode.Value + "');";
				if (base.Request["id"] == treeNode.Value)
				{
					treeNode.Select();
				}
				e.Node.ChildNodes.Add(treeNode);
			}
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
	}
}


