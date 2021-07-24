using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Organization_PostWeave : BasePage, IRequiresSessionState
{
	public PTDUTYAction hrAction
	{
		get
		{
			return new PTDUTYAction();
		}
	}
	public AnnexAction _AnnexAction
	{
		get
		{
			return new AnnexAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.Tree_Bind();
		}
	}
	protected void GVBook_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			DataRowView dataRowView = (DataRowView)e.Row.DataItem;
			int num = 0;
			DataTable dutyList = this.hrAction.GetDutyList(dataRowView["I_DUTYID"].ToString());
			if (dutyList.Rows.Count > 0)
			{
				num = 1;
			}
			e.Row.Attributes["onmouseover"] = "OnMouseOverRecord(this);";
			e.Row.Attributes["onclick"] = string.Concat(new object[]
			{
				"OnRecord(this);ClickRow('",
				dataRowView["I_DUTYID"].ToString(),
				"',",
				num,
				");"
			});
			e.Row.Cells[0].Text = Convert.ToString(e.Row.RowIndex + 1);
		}
	}
	protected void btnAdd_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnEdit_Click(object sender, EventArgs e)
	{
		this.GVBook.DataBind();
	}
	protected void btnDel_Click(object sender, EventArgs e)
	{
		if (this.hrAction.Delete(int.Parse(this.HdnDUTYID.Value)) > 0)
		{
			this.GVBook.DataBind();
		}
	}
	private void Tree_Bind()
	{
		DataTable departmentTree = this.hrAction.GetDepartmentTree("0");
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.Select();
				this.TVDept.Nodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
		if (!string.IsNullOrEmpty(this.TVDept.SelectedValue))
		{
			this.btnAdd.Enabled = true;
			this.HdnDeptID.Value = this.TVDept.SelectedValue;
			this.HdnDeptName.Value = this.TVDept.SelectedNode.Text;
		}
	}
	private void Bind_SubTree(TreeNode ftn, string strBMDM)
	{
		string b = base.Request["sel_val"];
		DataTable departmentTree = this.hrAction.GetDepartmentTree(strBMDM);
		if (departmentTree.Rows.Count > 0)
		{
			foreach (DataRow dataRow in departmentTree.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				if (treeNode.Value == b)
				{
					treeNode.Select();
				}
				ftn.ChildNodes.Add(treeNode);
				this.Bind_SubTree(treeNode, dataRow["i_bmdm"].ToString());
			}
		}
	}
	protected void TVDept_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.btnAdd.Enabled = true;
		this.HdnDeptID.Value = this.TVDept.SelectedValue;
		this.HdnDeptName.Value = this.TVDept.SelectedNode.Text;
		this.GVBook.DataBind();
	}
}


