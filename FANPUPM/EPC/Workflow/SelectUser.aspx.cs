using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_SelectUser : NBasePage, IRequiresSessionState
{
	private string method = string.Empty;
	private static System.Collections.Generic.List<string> userCodes;
	private PtYhmcBll ptYhmcBll = new PtYhmcBll();
	private ContractType contractType = new ContractType();
	private PTbdmBll ptdbmBll = new PTbdmBll();

	protected override void OnInit(System.EventArgs e)
	{
		this.method = base.Request.QueryString["Method"];
		base.OnInit(e);
	}
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
			treeNode.Selected = true;
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
	protected void trvwDepartment_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		string selectedValue = this.TvBranch.SelectedValue;
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} and state=1", selectedValue));
		this.lbSelect.DataBind();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		base.RegisterScript("setVal('" + this.method + "')");
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where v_xm like '%{0}%' and state=1", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
	}
}


