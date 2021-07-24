using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.web.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_SelProject : NBasePage, IRequiresSessionState
{
	private RoleProjectAction rpa
	{
		get
		{
			return new RoleProjectAction();
		}
	}

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.DDL_ProClass_Bind();
			this.CreateProjectTree(this.rpa.GetProjectList(this.DDL_ProClass.SelectedValue.ToString().Trim()), this.TVProject.Nodes, "");
		}
	}
	private void DDL_ProClass_Bind()
	{
		this.DDL_ProClass.DataTextField = "CodeName";
		this.DDL_ProClass.DataValueField = "CodeID";
		this.DDL_ProClass.DataSource = PMAction.GetPrjTypeCodeid();
		this.DDL_ProClass.DataBind();
	}
	protected void DDL_ProClass_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.TVProject.Nodes.Clear();
		this.CreateProjectTree(this.rpa.GetProjectList(this.DDL_ProClass.SelectedValue.ToString().Trim()), this.TVProject.Nodes, "");
	}
	protected void CreateProjectTree(DataTable dtProject, TreeNodeCollection Nds, string SystemCode)
	{
		if (SystemCode == "")
		{
			DataRow[] array = dtProject.Select("len(TypeCode) = 5");
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = array[i]["PrjName"].ToString();
				treeNode.Value = array[i]["i_xh"].ToString();
				treeNode.NavigateUrl = "#";
				treeNode.ToolTip = array[i]["i_xh"].ToString();
				Nds.Add(treeNode);
				treeNode.Expanded = new bool?(true);
				this.CreateProjectTree(dtProject, treeNode.ChildNodes, array[i]["TypeCode"].ToString());
			}
			return;
		}
		DataRow[] array2 = dtProject.Select("TypeCode like '" + SystemCode + "%' and (len(TypeCode) - 5) = " + SystemCode.Length.ToString());
		for (int j = 0; j < array2.Length; j++)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Text = array2[j]["PrjName"].ToString();
			treeNode.Value = array2[j]["i_xh"].ToString();
			treeNode.NavigateUrl = "#";
			treeNode.ToolTip = array2[j]["i_xh"].ToString();
			Nds.Add(treeNode);
			treeNode.Expanded = new bool?(false);
			this.CreateProjectTree(dtProject, treeNode.ChildNodes, array2[j]["TypeCode"].ToString());
		}
	}
	private void CycleTree(TreeNodeCollection nodes)
	{
		foreach (TreeNode treeNode in nodes)
		{
			if (treeNode.Checked)
			{
				if (treeNode.ChildNodes.Count != 0)
				{
					this.CycleTree(treeNode.ChildNodes);
				}
				else
				{
					HiddenField expr_3E = this.HdnProjectCodes;
					expr_3E.Value = expr_3E.Value + treeNode.Value + ",";
				}
			}
		}
	}
}


