using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_FileManage_FileCatalogFrame : BasePage, IRequiresSessionState
{
	public static string tt = "";

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
			if (base.Request["look"] != null)
			{
				oa_FileManage_FileCatalogFrame.tt = "view";
			}
			this.FileCatalog_Bind();
		}
	}
	private void FileCatalog_Bind()
	{
		this.TVLibrary.Nodes.Clear();
		DataTable list = this.amAction.GetList("IsValid=1");
		foreach (DataRow dataRow in list.Rows)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Text = dataRow["LibraryName"].ToString();
			treeNode.Target = "frmLibrary";
			treeNode.NavigateUrl = "FileCatalogManage.aspx?isFirst=false&lc=" + dataRow["LibraryCode"].ToString() + "&isview=" + oa_FileManage_FileCatalogFrame.tt;
			this.TVLibrary.Nodes.Add(treeNode);
			DataTable maxAndMinYear = OAFileCatalogAction.GetMaxAndMinYear("LibraryCode='" + dataRow["LibraryCode"].ToString() + "' order by FileAge");
			foreach (DataRow dataRow2 in maxAndMinYear.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow2["FileAge"].ToString();
				treeNode2.Target = "frmLibrary";
				treeNode2.NavigateUrl = string.Concat(new string[]
				{
					"FileCatalogManage.aspx?isFirst=false&lc=",
					dataRow["LibraryCode"].ToString(),
					"&y=",
					dataRow2["FileAge"].ToString(),
					"&isview=",
					oa_FileManage_FileCatalogFrame.tt
				});
				treeNode.Nodes.Add(treeNode2);
				DataTable list2 = this.fcAction.GetList("LibraryCode='" + dataRow["LibraryCode"].ToString() + "'");
				if (list2.Rows.Count > 0)
				{
					TreeNode treeNode3 = new TreeNode();
					treeNode3.Text = "永久";
					treeNode3.Target = "frmLibrary";
					treeNode3.NavigateUrl = string.Concat(new string[]
					{
						"FileCatalogManage.aspx?isFirst=false&lc=",
						dataRow["LibraryCode"].ToString(),
						"&y=",
						dataRow2["FileAge"].ToString(),
						"&l=1&isview=",
						oa_FileManage_FileCatalogFrame.tt
					});
					treeNode2.Nodes.Add(treeNode3);
					foreach (DataRow dataRow3 in list2.Rows)
					{
						TreeNode treeNode4 = new TreeNode();
						treeNode4.Text = dataRow3["ClassName"].ToString();
						treeNode4.NavigateUrl = string.Concat(new string[]
						{
							"FileCatalogManage.aspx?isFirst=false&lc=",
							dataRow["LibraryCode"].ToString(),
							"&y=",
							dataRow2["FileAge"].ToString(),
							"&l=1&cid=",
							dataRow3["ClassID"].ToString(),
							"&isview=",
							oa_FileManage_FileCatalogFrame.tt
						});
						treeNode4.Target = "frmLibrary";
						treeNode3.Nodes.Add(treeNode4);
					}
					TreeNode treeNode5 = new TreeNode();
					treeNode5.Text = "长期";
					treeNode5.Target = "frmLibrary";
					treeNode5.NavigateUrl = string.Concat(new string[]
					{
						"FileCatalogManage.aspx?isFirst=false&lc=",
						dataRow["LibraryCode"].ToString(),
						"&y=",
						dataRow2["FileAge"].ToString(),
						"&l=2&isview=",
						oa_FileManage_FileCatalogFrame.tt
					});
					treeNode2.Nodes.Add(treeNode5);
					foreach (DataRow dataRow4 in list2.Rows)
					{
						TreeNode treeNode6 = new TreeNode();
						treeNode6.Text = dataRow4["ClassName"].ToString();
						treeNode6.NavigateUrl = string.Concat(new string[]
						{
							"FileCatalogManage.aspx?isFirst=false&lc=",
							dataRow["LibraryCode"].ToString(),
							"&y=",
							dataRow2["FileAge"].ToString(),
							"&l=2&cid=",
							dataRow4["ClassID"].ToString(),
							"&isview=",
							oa_FileManage_FileCatalogFrame.tt
						});
						treeNode6.Target = "frmLibrary";
						treeNode5.Nodes.Add(treeNode6);
					}
					TreeNode treeNode7 = new TreeNode();
					treeNode7.Text = "短期";
					treeNode7.Target = "frmLibrary";
					treeNode7.NavigateUrl = string.Concat(new string[]
					{
						"FileCatalogManage.aspx?isFirst=false&lc=",
						dataRow["LibraryCode"].ToString(),
						"&y=",
						dataRow2["FileAge"].ToString(),
						"&l=3&isview=",
						oa_FileManage_FileCatalogFrame.tt
					});
					treeNode2.Nodes.Add(treeNode7);
					foreach (DataRow dataRow5 in list2.Rows)
					{
						TreeNode treeNode8 = new TreeNode();
						treeNode8.Text = dataRow5["ClassName"].ToString();
						treeNode8.NavigateUrl = string.Concat(new string[]
						{
							"FileCatalogManage.aspx?isFirst=false&lc=",
							dataRow["LibraryCode"].ToString(),
							"&y=",
							dataRow2["FileAge"].ToString(),
							"&l=3&cid=",
							dataRow5["ClassID"].ToString(),
							"&isview=",
							oa_FileManage_FileCatalogFrame.tt
						});
						treeNode8.Target = "frmLibrary";
						treeNode7.Nodes.Add(treeNode8);
					}
				}
			}
		}
	}
}


