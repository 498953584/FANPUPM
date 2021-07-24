using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_FileClassTree : BasePage, IRequiresSessionState
{
	private PTMultiClassesAction pca
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.DDLPrjCode.DataBind();
			this.TVClass_AppendNode(this.TreeView1.Nodes, "");
		}
	}
	protected void DDLPrjCode_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.TreeView1.Nodes.Clear();
		this.TVClass_AppendNode(this.TreeView1.Nodes, "");
	}
	private void TVClass_AppendNode(TreeNodeCollection nodes, string ParentCode)
	{
		DataTable list = this.pca.GetList(" ClassTypeCode = '005'  and ParentClassCode ='" + ParentCode + "'");
		int count = list.Rows.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = list.Rows[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Value = dataRow["ClassID"].ToString();
				treeNode.Text = dataRow["ClassName"].ToString().Replace("&nbsp;", "").Trim();
				treeNode.Target = "frmFileList";
				treeNode.NavigateUrl = string.Concat(new string[]
				{
					"ProjectFileManage.aspx?rt=1&prj=",
					this.DDLPrjCode.SelectedValue,
					"&isFirst=1&uc=",
					this.Session["yhdm"].ToString(),
					"&cid=",
					dataRow["ClassID"].ToString()
				});
				this.TVClass_AppendNode(treeNode.ChildNodes, dataRow["ClassCode"].ToString());
				nodes.Add(treeNode);
			}
		}
	}
	protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
	{
	}
}


