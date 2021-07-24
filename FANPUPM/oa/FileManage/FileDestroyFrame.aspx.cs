using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_FileManage_FileDestroyFrame : BasePage, IRequiresSessionState
{
	public OAFileLibraryAction amAction
	{
		get
		{
			return new OAFileLibraryAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Library_Bind();
		}
	}
	private void Library_Bind()
	{
		this.TVLibrary.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "档案名称";
		this.TVLibrary.Nodes.Add(treeNode);
		DataTable list = this.amAction.GetList("IsValid=1");
		foreach (DataRow dataRow in list.Rows)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Text = dataRow["LibraryName"].ToString();
			treeNode2.Target = "frmLibrary";
			treeNode2.NavigateUrl = string.Concat(new string[]
			{
				"FileDestroy.aspx?isFirst=false&uc=",
				this.Session["yhdm"].ToString(),
				"&lc=",
				dataRow["LibraryCode"].ToString(),
				"&ln=",
				dataRow["LibraryName"].ToString()
			});
			treeNode.Nodes.Add(treeNode2);
		}
	}
}


