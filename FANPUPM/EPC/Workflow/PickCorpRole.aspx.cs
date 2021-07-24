using ASP;
using cn.justwin.BLL;
using cn.justwin.stockDAL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_PickCorpRole : NBasePage, IRequiresSessionState
{
	private PTbdmService ptdbmBll = new PTbdmService();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
		}
	}
	protected void BindTree()
	{
		this.TvBranch.Nodes.Clear();
		System.Collections.Generic.IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere(" where i_sjdm=0 ");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			this.AddChildNodes(treeNode);
			this.TvBranch.Nodes.Add(treeNode);
		}
	}
	protected void AddChildNodes(TreeNode node)
	{
		System.Collections.Generic.IList<PTbdm> pTbdmByWhere = this.ptdbmBll.GetPTbdmByWhere("where i_sjdm='" + node.Value + "'");
		foreach (PTbdm current in pTbdmByWhere)
		{
			TreeNode treeNode = new TreeNode(current.V_BMMC, current.i_bmdm.ToString());
			treeNode.ToolTip = string.Concat(current.i_bmdm);
			node.ChildNodes.Add(treeNode);
			this.AddChildNodes(treeNode);
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		foreach (TreeNode treeNode in this.TvBranch.CheckedNodes)
		{
			HiddenField expr_25 = this.hfldCorpCode;
			expr_25.Value = expr_25.Value + treeNode.Value + ",";
		}
		if (!string.IsNullOrEmpty(this.hfldCorpCode.Value))
		{
			this.hfldCorpCode.Value = this.hfldCorpCode.Value.Substring(0, this.hfldCorpCode.Value.Length - 1);
		}
		base.RegisterScript("setVal()");
	}
}


