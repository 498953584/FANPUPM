using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using cn.justwin.stockBLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractType_RoleControl : NBasePage, IRequiresSessionState
{
	private string typeID = string.Empty;
	private PtYhmcBll yhmc = new PtYhmcBll();
	private static System.Collections.Generic.List<string> userCodes;
	private ContractType contractType = new ContractType();

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["TypeID"]))
		{
			this.typeID = base.Request["TypeID"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			ContractTypeModel model = this.contractType.GetModel(this.typeID);
			ContractManage_ContractType_RoleControl.userCodes = JsonHelper.GetListFromJson(model.UserCodes);
			if (ContractManage_ContractType_RoleControl.userCodes.Count > 0)
			{
				System.Collections.Generic.List<string> names = this.yhmc.GetNames(ContractManage_ContractType_RoleControl.userCodes);
				this.lblUserNames.Text = JsonHelper.Serialize(names.ToArray());
			}
		}
	}
	private void DataBindDepartment()
	{
		DataTable deparmentsByParent = this.GetDeparmentsByParent("0");
		foreach (DataRow dataRow in deparmentsByParent.Rows)
		{
			TreeNode treeNode = new TreeNode(dataRow["v_bmmc"].ToString(), dataRow["i_bmdm"].ToString());
			this.trvwDepartment.Nodes.Add(treeNode);
			this.IterateDepartment(treeNode);
		}
	}
	private void IterateDepartment(TreeNode parentNode)
	{
		DataTable deparmentsByParent = this.GetDeparmentsByParent(parentNode.Value);
		foreach (DataRow dataRow in deparmentsByParent.Rows)
		{
			TreeNode treeNode = new TreeNode(dataRow["v_bmmc"].ToString(), dataRow["i_bmdm"].ToString());
			parentNode.ChildNodes.Add(treeNode);
			if (this.GetDeparmentsByParent(treeNode.Value).Rows.Count > 0)
			{
				this.IterateDepartment(treeNode);
			}
		}
	}
	private DataTable GetDeparmentsByParent(string parent)
	{
		DeptManageDb deptManageDb = new DeptManageDb();
		DataView defaultView = deptManageDb.GetAllDepartment().DefaultView;
		defaultView.RowFilter = string.Format("i_sjdm = {0}", parent);
		return defaultView.ToTable();
	}
	protected void trvwDepartment_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		string selectedValue = this.trvwDepartment.SelectedValue;
		this.gvwUser.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} ", selectedValue));
		this.gvwUser.DataBind();
		foreach (GridViewRow gridViewRow in this.gvwUser.Rows)
		{
			CheckBox checkBox = new CheckBox();
			if (gridViewRow.Controls[0].FindControl("chk") is CheckBox)
			{
				checkBox = (gridViewRow.Controls[0].FindControl("chk") as CheckBox);
			}
			Label label = new Label();
			if (gridViewRow.Controls[1].FindControl("lblName") is Label)
			{
				label = (gridViewRow.Controls[1].FindControl("lblName") as Label);
			}
			if (ContractManage_ContractType_RoleControl.userCodes.Contains(label.ToolTip))
			{
				checkBox.Checked = true;
			}
		}
	}
	protected void gvwUser_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwUser.DataKeys[e.Row.RowIndex].Value.ToString();
			if (e.Row.Controls[1].FindControl("lblName") is Label)
			{
				Label label = e.Row.Controls[1].FindControl("lblName") as Label;
				label.ToolTip = this.gvwUser.DataKeys[e.Row.RowIndex].Value.ToString();
			}
		}
	}
	protected void chk_CheckedChanged(object sender, System.EventArgs e)
	{
		foreach (GridViewRow gridViewRow in this.gvwUser.Rows)
		{
			if (gridViewRow.Controls[0].FindControl("chk") is CheckBox)
			{
				CheckBox checkBox = gridViewRow.Controls[0].FindControl("chk") as CheckBox;
				Label label = new Label();
				if (gridViewRow.Controls[1].FindControl("lblName") is Label)
				{
					label = (gridViewRow.Controls[1].FindControl("lblName") as Label);
				}
				if (checkBox.Checked)
				{
					if (!ContractManage_ContractType_RoleControl.userCodes.Contains(label.ToolTip))
					{
						ContractManage_ContractType_RoleControl.userCodes.Add(label.ToolTip);
					}
				}
				else
				{
					if (ContractManage_ContractType_RoleControl.userCodes.Contains(label.ToolTip))
					{
						ContractManage_ContractType_RoleControl.userCodes.Remove(label.ToolTip);
					}
				}
			}
		}
		System.Collections.Generic.List<string> names = this.yhmc.GetNames(ContractManage_ContractType_RoleControl.userCodes);
		this.lblUserNames.Text = JsonHelper.Serialize(names.ToArray());
	}
	protected void chkAll_CheckedChanged(object sender, System.EventArgs e)
	{
		if (this.gvwUser.HeaderRow.Controls[0].FindControl("chkAll") is CheckBox)
		{
			CheckBox checkBox = this.gvwUser.HeaderRow.Controls[0].FindControl("chkAll") as CheckBox;
			if (checkBox.Checked)
			{
				System.Collections.IEnumerator enumerator = this.gvwUser.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
						if (gridViewRow.Controls[1].FindControl("lblName") is Label)
						{
							Label label = gridViewRow.Controls[1].FindControl("lblName") as Label;
							if (!ContractManage_ContractType_RoleControl.userCodes.Contains(label.ToolTip))
							{
								ContractManage_ContractType_RoleControl.userCodes.Add(label.ToolTip);
							}
						}
					}
					return;
				}
				finally
				{
					System.IDisposable disposable = enumerator as System.IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			foreach (GridViewRow gridViewRow2 in this.gvwUser.Rows)
			{
				if (gridViewRow2.Controls[1].FindControl("lblName") is Label)
				{
					Label label2 = gridViewRow2.Controls[1].FindControl("lblName") as Label;
					if (ContractManage_ContractType_RoleControl.userCodes.Contains(label2.ToolTip))
					{
						ContractManage_ContractType_RoleControl.userCodes.Remove(label2.ToolTip);
					}
				}
			}
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		ContractTypeModel model = this.contractType.GetModel(this.typeID);
		model.UserCodes = JsonHelper.Serialize(ContractManage_ContractType_RoleControl.userCodes.ToArray());
		this.contractType.Update(model);
		base.RegisterScript("window.close();");
	}
}


