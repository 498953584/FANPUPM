using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_SysAdmin_RoleManage_RoleType : BasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.TVRoleType_Create();
		}
	}
	private void TVRoleType_Create()
	{
		DataTable roleTypeFirstLevel = roleManageDb.getRoleTypeFirstLevel();
		for (int i = 0; i < roleTypeFirstLevel.Rows.Count; i++)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Text = roleTypeFirstLevel.Rows[i]["RoleTypeName"].ToString();
			treeNode.NavigateUrl = base.Server.UrlPathEncode(string.Concat(new object[]
			{
				"RoleList.aspx?rt=",
				roleTypeFirstLevel.Rows[i]["RoleTypeCode"],
				"&rn=",
				roleTypeFirstLevel.Rows[i]["RoleTypeName"]
			}));
			treeNode.Target = "right";
			this.tvRoleType.Nodes.Add(treeNode);
		}
	}
}


