using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_FileManage_FileLendSearchFrame : BasePage, IRequiresSessionState
{

	public OAFileLibraryAction amAction
	{
		get
		{
			return new OAFileLibraryAction();
		}
	}
	public OAFileClassesAction fcAction
	{
		get
		{
			return new OAFileClassesAction();
		}
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.FileCatalog_Bind();
		}
	}
	private void FileCatalog_Bind()
	{
		this.TVLibrary.Nodes.Clear();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "档案分类";
		this.TVLibrary.Nodes.Add(treeNode);
		DataTable list = this.amAction.GetList("IsValid=1");
		foreach (DataRow dataRow in list.Rows)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Text = dataRow["LibraryName"].ToString();
			treeNode2.Target = "frmLibrary";
			treeNode2.NavigateUrl = "FileLendSearch.aspx?isFirst=false&lc=" + dataRow["LibraryCode"].ToString();
			treeNode.Nodes.Add(treeNode2);
			DataTable maxAndMinYear = OAFileCatalogAction.GetMaxAndMinYear("LibraryCode='" + dataRow["LibraryCode"].ToString() + "' order by FileAge");
			foreach (DataRow dataRow2 in maxAndMinYear.Rows)
			{
				TreeNode treeNode3 = new TreeNode();
				treeNode3.Text = dataRow2["FileAge"].ToString();
				treeNode3.Target = "frmLibrary";
				treeNode3.NavigateUrl = "FileLendSearch.aspx?isFirst=false&lc=" + dataRow["LibraryCode"].ToString() + "&y=" + dataRow2["FileAge"].ToString();
				treeNode2.Nodes.Add(treeNode3);
				DataTable list2 = this.fcAction.GetList("LibraryCode='" + dataRow["LibraryCode"].ToString() + "'");
				if (list2.Rows.Count > 0)
				{
					TreeNode treeNode4 = new TreeNode();
					treeNode4.Text = "永久";
					treeNode4.Target = "frmLibrary";
					treeNode4.NavigateUrl = string.Concat(new string[]
					{
						"FileLendSearch.aspx?isFirst=false&lc=",
						dataRow["LibraryCode"].ToString(),
						"&y=",
						dataRow2["FileAge"].ToString(),
						"&l=1"
					});
					treeNode3.Nodes.Add(treeNode4);
					foreach (DataRow dataRow3 in list2.Rows)
					{
						TreeNode treeNode5 = new TreeNode();
						treeNode5.Text = dataRow3["ClassName"].ToString();
						treeNode5.NavigateUrl = string.Concat(new string[]
						{
							"FileLendSearch.aspx?isFirst=false&lc=",
							dataRow["LibraryCode"].ToString(),
							"&y=",
							dataRow2["FileAge"].ToString(),
							"&l=1&cid=",
							dataRow3["ClassID"].ToString()
						});
						treeNode5.Target = "frmLibrary";
						treeNode4.Nodes.Add(treeNode5);
					}
					TreeNode treeNode6 = new TreeNode();
					treeNode6.Text = "长期";
					treeNode6.Target = "frmLibrary";
					treeNode6.NavigateUrl = string.Concat(new string[]
					{
						"FileLendSearch.aspx?isFirst=false&lc=",
						dataRow["LibraryCode"].ToString(),
						"&y=",
						dataRow2["FileAge"].ToString(),
						"&l=2"
					});
					treeNode3.Nodes.Add(treeNode6);
					foreach (DataRow dataRow4 in list2.Rows)
					{
						TreeNode treeNode7 = new TreeNode();
						treeNode7.Text = dataRow4["ClassName"].ToString();
						treeNode7.NavigateUrl = string.Concat(new string[]
						{
							"FileLendSearch.aspx?isFirst=false&lc=",
							dataRow["LibraryCode"].ToString(),
							"&y=",
							dataRow2["FileAge"].ToString(),
							"&l=2&cid=",
							dataRow4["ClassID"].ToString()
						});
						treeNode7.Target = "frmLibrary";
						treeNode6.Nodes.Add(treeNode7);
					}
					TreeNode treeNode8 = new TreeNode();
					treeNode8.Text = "短期";
					treeNode8.Target = "frmLibrary";
					treeNode8.NavigateUrl = string.Concat(new string[]
					{
						"FileLendSearch.aspx?isFirst=false&lc=",
						dataRow["LibraryCode"].ToString(),
						"&y=",
						dataRow2["FileAge"].ToString(),
						"&l=3"
					});
					treeNode3.Nodes.Add(treeNode8);
					foreach (DataRow dataRow5 in list2.Rows)
					{
						TreeNode treeNode9 = new TreeNode();
						treeNode9.Text = dataRow5["ClassName"].ToString();
						treeNode9.NavigateUrl = string.Concat(new string[]
						{
							"FileLendSearch.aspx?isFirst=false&lc=",
							dataRow["LibraryCode"].ToString(),
							"&y=",
							dataRow2["FileAge"].ToString(),
							"&l=3&cid=",
							dataRow5["ClassID"].ToString()
						});
						treeNode9.Target = "frmLibrary";
						treeNode8.Nodes.Add(treeNode9);
					}
				}
			}
		}
		TreeNode treeNode10 = new TreeNode();
		treeNode10.Text = "我的借阅记录";
		treeNode10.Target = "frmLibrary";
		treeNode10.NavigateUrl = "MyLoanList.aspx";
		this.TVLibrary.Nodes.Add(treeNode10);
	}
}


