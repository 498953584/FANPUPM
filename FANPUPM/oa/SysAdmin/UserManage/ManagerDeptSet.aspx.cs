using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_SysAdmin_UserManage_ManagerDeptSet : BasePage, IRequiresSessionState
{
	private new string UserCode = "";
	private string ControlDept = "";
	protected static DeptManageDb DeptAct
	{
		get
		{
			return new DeptManageDb();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		this.UserCode = base.Request.QueryString["yhdm"].ToString();
		if (!this.Page.IsPostBack)
		{
			userManageDb userManageDb = new userManageDb();
			this.ControlDept = "," + userManageDb.manageDept(this.UserCode) + ",";
			this.tvDepartment.Attributes["onclick"] = "javascript:postBackObject();";
			this.tvDepartment_AppendNode(this.tvDepartment.Nodes, 0, this.UserCode, true);
		}
	}
	private void tvDepartment_AppendNode(TreeNodeCollection nodes, int parentID, string UserCode, bool Enabled)
	{
		DataTable subDepartment = oa_SysAdmin_UserManage_ManagerDeptSet.DeptAct.GetSubDepartment(parentID);
		int count = subDepartment.Rows.Count;
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				DataRow dataRow = subDepartment.Rows[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["v_bmmc"].ToString();
				treeNode.NavigateUrl = "#";
				string value = "," + dataRow["i_bmdm"].ToString() + ",";
				if (this.ControlDept.IndexOf(value) >= 0)
				{
					treeNode.Checked = true;
					this.tvDepartment_AppendNode(treeNode.ChildNodes, Convert.ToInt32(treeNode.Value), UserCode, false);
				}
				else
				{
					this.tvDepartment_AppendNode(treeNode.ChildNodes, Convert.ToInt32(treeNode.Value), UserCode, true);
				}
				nodes.Add(treeNode);
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string text = ",";
		foreach (TreeNode treeNode in this.tvDepartment.CheckedNodes)
		{
			text = text + treeNode.Value + ",";
		}
		userManageDb userManageDb = new userManageDb();
		if (userManageDb.managerDeptUpdate(this.UserCode, text.Trim(new char[]
		{
			','
		})))
		{
			string script = "\r\n\t\t\t\talert('保存成功！');\r\n\t\t\t\twindow.returnValue = true; \r\n\t\t\t\twindow.parent.close();\r\n\t\t\t";
			base.RegisterScript(script);
			return;
		}
		string script2 = "\r\n\t\t\t\talert('保存失败！');\r\n\t\t\t";
		base.RegisterScript(script2);
	}
}


