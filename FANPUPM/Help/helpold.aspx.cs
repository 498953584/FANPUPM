using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class helpold : PageBase, System.Web.SessionState.IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			string text = this.Session["mkdm"].ToString();
			if (text.Length > 2)
			{
				text = text.Substring(0, 2);
			}
			this.TrvHelp_AppendNode(text);
		}
	}
	private void TrvHelp_AppendNode(string parentCode)
	{
		this.TrvHelp.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "系统帮助";
		treeNode.Value = "";
		treeNode.NavigateUrl = "/EPC/UserControl1/webNullTS.aspx";
		treeNode.Target = "ifrMain";
		this.TrvHelp.Nodes.Add(treeNode);
		DataTable dataTable = publicDbOpClass.DataTableQuary("SELECT * FROM  PT_MK WHERE  C_MKDM = '" + parentCode + "' order by PT_MK.i_xh,PT_MK.c_mkdm");
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["v_MKMC"].ToString();
				treeNode2.Value = dataRow["C_MKDM"].ToString();
				if (dataRow["HelpPath"].ToString().Trim() != "")
				{
					treeNode2.NavigateUrl = "/Help/" + dataRow["HelpPath"].ToString();
				}
				else
				{
					treeNode2.NavigateUrl = "/Help/helpNothing.aspx?mc=" + base.Server.UrlEncode(dataRow["v_MKMC"].ToString());
				}
				treeNode2.Target = "ifrMain";
				treeNode.ChildNodes.Add(treeNode2);
				this.TrvHelp_SubTree(treeNode2, parentCode);
			}
		}
	}
	private void TrvHelp_SubTree(TreeNode ftn, string parentCode)
	{
		DataTable dataTable = publicDbOpClass.DataTableQuary(string.Concat(new object[]
		{
			"SELECT * FROM  PT_MK WHERE  C_MKDM like '",
			parentCode,
			"%' and len(c_mkdm)=",
			parentCode.Length,
			"+2"
		}));
		if (dataTable.Rows.Count > 0)
		{
			foreach (DataRow dataRow in dataTable.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["v_MKMC"].ToString();
				treeNode.Value = dataRow["C_MKDM"].ToString();
				if (dataRow["HelpPath"].ToString().Trim() != "")
				{
					treeNode.NavigateUrl = "/Help/" + dataRow["HelpPath"].ToString();
				}
				else
				{
					treeNode.NavigateUrl = "/Help/helpNothing.aspx?mc=" + base.Server.UrlEncode(dataRow["v_MKMC"].ToString());
				}
				treeNode.Target = "ifrMain";
				ftn.ChildNodes.Add(treeNode);
				this.TrvHelp_SubTree(treeNode, dataRow["C_MKDM"].ToString());
			}
		}
	}
}


