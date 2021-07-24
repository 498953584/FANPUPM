using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_System_SystemInfoMain : PageBase, IRequiresSessionState
{

	private PTMultiClassesAction pca
	{
		get
		{
			return new PTMultiClassesAction();
		}
	}
	public string ctc
	{
		get
		{
			object obj = this.ViewState["CTC"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["CTC"] = value;
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack && base.Request["ctc"] != null)
		{
			this.ctc = base.Request["ctc"].ToString();
			if (this.ctc == "002")
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Value = "";
				treeNode.Text = "制度分类";
				treeNode.Target = "frmSysList";
				this.TVClass_AppendNode(treeNode.ChildNodes, "");
				this.TreeView1.Nodes.Add(treeNode);
			}
		}
	}
	private void TVClass_AppendNode(TreeNodeCollection nodes, string ParentCode)
	{
		DataTable dataTable = new DataTable();
		if (this.ctc == "002")
		{
			dataTable = this.pca.GetList(string.Concat(new string[]
			{
				" ClassTypeCode = '",
				this.ctc,
				"'and ParentClassCode ='",
				ParentCode,
				"'"
			}));
		}
		int count = dataTable.Rows.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Value = dataRow["ClassID"].ToString();
				treeNode.Text = dataRow["ClassName"].ToString().Replace("&nbsp;", "").Trim();
				treeNode.Target = "frmSysList";
				if (base.Request["audit"] != null)
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"SystemInfoList.aspx?rt=2&isFirst=1&uc=",
						this.Session["yhdm"].ToString(),
						"&cid=",
						dataRow["ClassID"].ToString(),
						"&audit=&ctc=",
						this.ctc
					});
				}
				else
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						"SystemInfoList.aspx?rt=2&isFirst=1&uc=",
						this.Session["yhdm"].ToString(),
						"&cid=",
						dataRow["ClassID"].ToString(),
						"&ctc=",
						this.ctc
					});
				}
				this.TVClass_AppendNode(treeNode.ChildNodes, dataRow["ClassCode"].ToString());
				nodes.Add(treeNode);
			}
		}
	}
}


