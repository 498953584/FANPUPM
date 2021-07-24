using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class StockManage_basicset_AlarmNumFrame : NBasePage, IRequiresSessionState
{
	private Treasury treasury = new Treasury();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
		}
	}
	protected void BindTree()
	{
		this.tvTreasury.Nodes.Clear();
		System.Collections.Generic.IList<TreasuryModel> list = this.treasury.GetList(" tcode='0'");
		foreach (TreasuryModel current in list)
		{
			TreeNode treeNode = new TreeNode(current.tname, current.tcode);
			treeNode.NavigateUrl = "~/StockManage/basicset/SetAlarmNum.aspx?tcode=" + current.tcode;
			treeNode.Selected = true;
			this.AddChildNodes(treeNode);
			this.tvTreasury.Nodes.Add(treeNode);
		}
		this.tvTreasury.ExpandAll();
	}
	protected void AddChildNodes(TreeNode node)
	{
		System.Collections.Generic.IList<TreasuryModel> list = this.treasury.GetList(" ptcode='" + node.Value + "'");
		foreach (TreasuryModel current in list)
		{
			TreeNode treeNode = new TreeNode(current.tname, current.tcode);
			treeNode.NavigateUrl = "~/StockManage/basicset/SetAlarmNum.aspx?tcode=" + current.tcode;
			node.ChildNodes.Add(treeNode);
			this.AddChildNodes(treeNode);
		}
	}
}


