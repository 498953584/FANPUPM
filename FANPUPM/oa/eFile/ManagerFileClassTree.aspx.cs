using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_eFile_ManagerFileClassTree : BasePage, IRequiresSessionState
{

	private PTMultiClassesAction pca
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}
	public string sl
	{
		get
		{
			object obj = this.ViewState["SL"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["SL"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (base.Request["sl"] != null)
		{
			this.sl = base.Request["sl"].ToString();
		}
		this.TVClass_AppendNode(this.TreeView1.Nodes, "");
	}
	private void TVClass_AppendNode(TreeNodeCollection nodes, string ParentCode)
	{
		DataTable list = this.pca.GetList(string.Concat(new string[]
		{
			" ClassTypeCode = '012' and CorpCode = '",
			this.Session["CorpCode"].ToString(),
			"' and ParentClassCode ='",
			ParentCode,
			"'"
		}));
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
				if (this.sl == "")
				{
					treeNode.NavigateUrl = "ProjectFileManage.aspx?rt=2&prj=00000000-0000-0000-0000-000000000000&isFirst=1&uc=" + this.Session["yhdm"].ToString() + "&cid=" + dataRow["ClassID"].ToString();
				}
				else
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"ProjectFileManage.aspx?rt=2&prj=00000000-0000-0000-0000-000000000000&isFirst=1&uc=",
						this.Session["yhdm"].ToString(),
						"&cid=",
						dataRow["ClassID"].ToString(),
						"&sl=",
						this.sl
					});
				}
				this.TVClass_AppendNode(treeNode.ChildNodes, dataRow["ClassCode"].ToString());
				nodes.Add(treeNode);
			}
		}
	}
}


