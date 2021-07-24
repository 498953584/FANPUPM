using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_eFile_BorroweFileSelectMainTree : BasePage, IRequiresSessionState
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
	protected int p
	{
		get
		{
			return Convert.ToInt32(this.ViewState["P"]);
		}
		set
		{
			this.ViewState["P"] = value;
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack && base.Request["p"] != null)
		{
			this.p = Convert.ToInt32(base.Request["p"].ToString());
			if (this.p == 1)
			{
				this.ClassTypeCode = "005";
			}
			else
			{
				this.ClassTypeCode = "004";
			}
			this.FillTrees1.Nodes.Clear();
			this.FillTrees1.Target = "frmMain";
			this.TVClass_AppendNode(this.FillTrees1.Nodes, "");
		}
	}
	private void TVClass_AppendNode(TreeNodeCollection nodes, string ParentCode)
	{
		DataTable dataTable = new DataTable();
		if (this.ClassTypeCode == "004")
		{
			dataTable = this.pca.GetList(string.Concat(new string[]
			{
				" ClassTypeCode = '012' and CorpCode = '",
				this.Session["CorpCode"].ToString(),
				"' and ParentClassCode ='",
				ParentCode,
				"'"
			}));
		}
		else
		{
			if (this.ClassTypeCode == "005")
			{
				dataTable = this.pca.GetList(string.Concat(new string[]
				{
					" ClassTypeCode = '",
					this.ClassTypeCode,
					"' and  ParentClassCode ='",
					ParentCode,
					"'"
				}));
			}
		}
		int count = dataTable.Rows.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = dataTable.Rows[i];
				TreeNode treeNode = new TreeNode();
				treeNode.NodeData = dataRow["ClassID"].ToString();
				treeNode.Text = dataRow["ClassName"].ToString().Replace("&nbsp;", "").Trim();
				treeNode.NavigateUrl = string.Concat(new object[]
				{
					"BorroweFileSelectMain.aspx?p=",
					this.p,
					"&cd=",
					dataRow["ClassID"].ToString()
				});
				nodes.Add(treeNode);
				this.TVClass_AppendNode(treeNode.Nodes, dataRow["ClassCode"].ToString());
			}
		}
	}
	protected void DDLproject_SelectedIndexChanged(object sender, EventArgs e)
	{
	}
}


