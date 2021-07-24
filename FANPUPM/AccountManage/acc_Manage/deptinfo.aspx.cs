using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class SalaryManage_SetSalary_deptinfo : BasePage, IRequiresSessionState
{
	private int _iSysID;

	protected void Page_Load(object sender, EventArgs e)
	{
		try
		{
			this._iSysID = int.Parse(base.Request.QueryString["sysid"].ToString());
		}
		catch
		{
		}
		if (this._iSysID == 0)
		{
			SysManageDb sysManageDb = new SysManageDb();
			this._iSysID = sysManageDb.GetDefault();
		}
		this.TViewDept_Create(this._iSysID);
	}
	private void TViewDept_Create(int iSysID)
	{
		this.TView.Nodes.Clear();
		DeptManageDb deptManageDb = new DeptManageDb();
		DataSet allDepartmentSet = deptManageDb.GetAllDepartmentSet();
		DataTable deptDTable = allDepartmentSet.Tables[0];
		TreeNode preNodes = new TreeNode();
		this.TViewDept_Generate(preNodes, deptDTable, "0", iSysID);
		this.Page.RegisterClientScriptBlock("gourl", "<SCRIPT language=\"JavaScript\">parent.FraHuman.location.href='human.aspx?sysid=" + iSysID.ToString() + "&deptid=0';</SCRIPT>");
	}
	private void TViewDept_Generate(TreeNode preNodes, DataTable deptDTable, string isjCode, int iSysID)
	{
		DataRow[] array = deptDTable.Select("i_sjdm=" + isjCode);
		if (isjCode == "0")
		{
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode = new TreeNode();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Text = dataRow["v_bmmc"].ToString();
				if (dataRow["i_bmdm"].ToString().Trim() != "1")
				{
					treeNode.NavigateUrl = "human.aspx?sysid=" + this._iSysID.ToString() + "&deptid=" + dataRow["i_bmdm"].ToString();
					treeNode.Target = "FraHuman";
				}
				this.TView.Nodes.Add(treeNode);
				this.TViewDept_Generate(treeNode, deptDTable, dataRow["i_bmdm"].ToString(), iSysID);
			}
			return;
		}
		DataRow[] array3 = array;
		for (int j = 0; j < array3.Length; j++)
		{
			DataRow dataRow2 = array3[j];
			TreeNode treeNode = new TreeNode();
			treeNode.Value = dataRow2["i_bmdm"].ToString();
			treeNode.Text = dataRow2["v_bmmc"].ToString();
			if (dataRow2["i_bmdm"].ToString().Trim() != "1")
			{
				treeNode.NavigateUrl = "human.aspx?sysid=" + this._iSysID.ToString() + "&deptid=" + dataRow2["i_bmdm"].ToString();
				treeNode.Target = "FraHuman";
			}
			preNodes.ChildNodes.Add(treeNode);
			this.TViewDept_Generate(treeNode, deptDTable, dataRow2["i_bmdm"].ToString(), iSysID);
		}
	}
}


