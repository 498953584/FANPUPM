using ASP;
using cn.justwin.BLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ModuleSet_Workflow_WorkFlowCountFrame : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!base.IsPostBack)
		{
			this.TVRole_Create();
		}
	}
	private void TVRole_Create()
	{
		string text = "";
		DataTable dataTable = FlowTemplateAction.QueryBusinessCode();
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "业务编码";
		treeNode.NavigateUrl = "";
		treeNode.Target = "frmPage";
		this.TVRole.Nodes.Add(treeNode);
		foreach (DataRow dataRow in dataTable.Rows)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Text = dataRow["BusinessName"].ToString();
			string businessCode = dataRow["BusinessCode"].ToString();
			treeNode2.NavigateUrl = "WorkFlowCount.aspx?ti=" + dataRow["BusinessCode"].ToString() + "&class=" + text.ToString();
			treeNode2.Target = "frmPage";
			treeNode.ChildNodes.Add(treeNode2);
			DataTable dataTable2 = FlowTemplateAction.QueryBusinessClass(businessCode);
			foreach (DataRow dataRow2 in dataTable2.Rows)
			{
				TreeNode treeNode3 = new TreeNode();
				treeNode3.Text = dataRow2["BusinessClassName"].ToString();
				treeNode3.NavigateUrl = "WorkFlowCount.aspx?ti=" + dataRow2["BusinessCode"].ToString() + "&class=" + dataRow2["BusinessClass"].ToString();
				treeNode3.Target = "frmPage";
				treeNode2.ChildNodes.Add(treeNode3);
			}
		}
	}
}


