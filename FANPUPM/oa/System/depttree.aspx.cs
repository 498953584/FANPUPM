using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_System_depttree : BasePage, IRequiresSessionState
{
	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.SystemInfoClass_Created();
		}
	}
	protected void SystemInfoClass_Created()
	{
		DataTable classID = this.sia.GetClassID("b.ClassTypeCode='002'");
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "集团制度分类";
		treeNode.NavigateUrl = "";
		treeNode.Target = "rFram";
		this.tv.Nodes.Add(treeNode);
		foreach (DataRow dataRow in classID.Rows)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Text = dataRow["ClassName"].ToString();
			treeNode2.NavigateUrl = "jtgl_right.aspx?cid=" + dataRow["ClassID"].ToString();
			treeNode2.Target = "rFrame";
			treeNode.Nodes.Add(treeNode2);
		}
	}
}


