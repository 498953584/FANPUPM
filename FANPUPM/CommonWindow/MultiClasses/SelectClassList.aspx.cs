using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class CommonWindow_MultiClasses_SelectClassList : BasePage, IRequiresSessionState
{

	public string ClassTypeCode
	{
		get
		{
			return this.ViewState["CLASSTYPECODE"].ToString();
		}
		set
		{
			this.ViewState["CLASSTYPECODE"] = value;
		}
	}
	private PTMultiClassesAction pca
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack && base.Request["ct"] != null)
		{
			this.ClassTypeCode = base.Request["ct"].ToString();
			this.TVClass_AppendNode(this.TVClassList.Nodes, "");
		}
	}
	private void TVClass_AppendNode(TreeNodeCollection nodes, string ParentCode)
	{
		DataTable list = this.pca.GetList(string.Concat(new string[]
		{
			" ClassTypeCode = '",
			this.ClassTypeCode,
			"' and CorpCode = '",
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
				treeNode.Value = dataRow["ClassCode"].ToString();
				treeNode.Text = dataRow["ClassName"].ToString().Replace("&nbsp;", "").Trim();
				treeNode.NavigateUrl = string.Concat(new string[]
				{
					"javascript:clicknode('",
					treeNode.Value,
					"','",
					treeNode.Text,
					"')"
				});
				this.TVClass_AppendNode(treeNode.ChildNodes, treeNode.Value);
				nodes.Add(treeNode);
			}
		}
	}
}


