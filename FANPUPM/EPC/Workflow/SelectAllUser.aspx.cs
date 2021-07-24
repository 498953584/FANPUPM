using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using cn.justwin.stockBLL;
using cn.justwin.stockDAL;
using cn.justwin.stockModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_SelectAllUser : NBasePage, IRequiresSessionState
{
	private PtYhmcService ptYhmcBll = new PtYhmcService();
	private PTbdmService ptdbmBll = new PTbdmService();
	private string idcsv = string.Empty;
	private static System.Collections.Generic.IList<string> userCodeList;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["idcsv"]))
		{
			this.idcsv = base.Request["idcsv"];
			Common_SelectAllUser.userCodeList = (
				from c in this.idcsv.Split(new char[]
				{
					','
				})
				where c.Length == 8
				select c).ToList<string>();
		}
		else
		{
			Common_SelectAllUser.userCodeList = new System.Collections.Generic.List<string>();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindTree();
			if (Common_SelectAllUser.userCodeList != null)
			{
				this.AddUsers(Common_SelectAllUser.userCodeList);
			}
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
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} and  State!= 2", selectedValue));
		this.lbSelect.DataBind();
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		this.lbSelect.DataSource = this.ptYhmcBll.GetAllModelByWhere(string.Format("where v_xm like '%{0}%'", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
	}
	protected void AddUsers(System.Collections.Generic.IList<string> userCodes)
	{
		if (userCodes != null)
		{
			PtYhmcBll ptYhmcBll = new PtYhmcBll();
			ptYhmcBll.GetNames(userCodes);
			this.hfldCodeJson.Value = JsonHelper.JsonSerializer<string[]>(userCodes.ToArray<string>());
			PTYhmcService pTYhmcService = new PTYhmcService();
			System.Collections.Generic.IList<string> nameList = pTYhmcService.GetNameList(userCodes);
			this.hfldNameJson.Value = JsonHelper.JsonSerializer<string[]>(nameList.ToArray<string>());
		}
	}
}


