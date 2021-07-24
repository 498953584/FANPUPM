using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Basic_Resource_ResourceSelect_ResourceTree : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		base.Response.Clear();
		if (!base.IsPostBack)
		{
			if (base.Request.QueryString["pt"] == null || base.Request.QueryString["rs"] == null)
			{
				this.js.Text = "alert('系统提示：\\n页面错误！\\n请找技术人员！');window.close();return false;";
				return;
			}
			this.BindTVResTree();
		}
	}
	protected void BindTVResTree()
	{
		ResourceSelectBllAction resourceSelectBllAction = new ResourceSelectBllAction();
		DataTable categoryList = resourceSelectBllAction.GetCategoryList((base.Request.QueryString["rs"] == "") ? -1 : int.Parse(base.Request.QueryString["rs"]));
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "资源分类";
		this.CreateResTree(categoryList, treeNode, "");
		this.TVResTree.Nodes.Add(treeNode);
	}
	protected void CreateResTree(DataTable dt, TreeNode tn, string root)
	{
		DataRow[] array = dt.Select("CategoryParentCode='" + root + "' and isvalid=1");
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			TreeNode treeNode = new TreeNode();
			treeNode.Text = dataRow["CategoryName"].ToString();
			treeNode.Target = "resFraList";
			treeNode.NavigateUrl = string.Concat(new string[]
			{
				"ResourceDetails.aspx?cc=",
				dataRow["CategoryCode"].ToString(),
				"&rs=",
				dataRow["ResourceStyle"].ToString(),
				"&pt=",
				base.Request.QueryString["pt"]
			});
			tn.ChildNodes.Add(treeNode);
			this.CreateResTree(dt, treeNode, dataRow["CategoryCode"].ToString());
		}
	}
}


