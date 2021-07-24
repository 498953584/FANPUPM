using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class WEB_NewsFrame : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitTypeTree();
		}
	}
	public void InitTypeTree()
	{
		TreeNode treeNode = new TreeNode();
		treeNode.ID = "";
		treeNode.Text = "新闻分类";
		treeNode.Target = "frmPage";
		treeNode.NavigateUrl = "WebManagerList.aspx?c_xwlxdm=%&c_xwlxmc=";
		this.trNewsType.Nodes.Add(treeNode);
		this.trNewsType.NodeID = "c_xwlxdm";
		this.trNewsType.NodeText = "c_xwlxmc";
		this.trNewsType.ParentId = "c_parentid";
		this.trNewsType.Url = "WebManagerList.aspx";
		this.trNewsType.Locations = "c_xwlxdm,c_xwlxmc";
		NewsAction newsAction = new NewsAction();
		this.trNewsType.TreeDataSource = newsAction.getAllNewsType();
		this.trNewsType.FillTree();
		this.trNewsType.Target = "frmPage";
	}
}


