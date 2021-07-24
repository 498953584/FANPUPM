using ASP;
using cn.justwin.BLL;
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
public partial class WEB_UserRole : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private string idName = string.Empty;
	private string tbName = string.Empty;
	private string field = string.Empty;
	private PtYhmcBll yhmc = new PtYhmcBll();
	private static List<string> userCodes;

	protected override void OnInit(EventArgs e)
	{
		this.id = base.Request.QueryString["id"];
		this.idName = base.Request.QueryString["idName"];
		this.tbName = base.Request.QueryString["tbName"];
		this.field = base.Request.QueryString["field"];
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			System.Data.DataTable table = Common2.GetTable(this.tbName, string.Concat(new string[]
			{
				" where ",
				this.idName,
				"='",
				this.id,
				"'"
			}));
			WEB_UserRole.userCodes = JsonHelper.GetListFromJson(table.Rows[0][this.field].ToString());
			if (WEB_UserRole.userCodes.Count > 0)
			{
				List<string> names = this.yhmc.GetNames(WEB_UserRole.userCodes);
				this.lblUserNames.Text = JsonHelper.Serialize(names.ToArray());
			}
		}
	}
	private void DataBindDepartment()
	{
		System.Data.DataTable deparmentsByParent = this.GetDeparmentsByParent("0");
		foreach (System.Data.DataRow dataRow in deparmentsByParent.Rows)
		{
			TreeNode treeNode = new TreeNode(dataRow["v_bmmc"].ToString(), dataRow["i_bmdm"].ToString());
			this.trvwDepartment.Nodes.Add(treeNode);
			this.IterateDepartment(treeNode);
		}
	}
	private void IterateDepartment(TreeNode parentNode)
	{
		System.Data.DataTable deparmentsByParent = this.GetDeparmentsByParent(parentNode.Value);
		foreach (System.Data.DataRow dataRow in deparmentsByParent.Rows)
		{
			TreeNode treeNode = new TreeNode(dataRow["v_bmmc"].ToString(), dataRow["i_bmdm"].ToString());
			parentNode.ChildNodes.Add(treeNode);
			if (this.GetDeparmentsByParent(treeNode.Value).Rows.Count > 0)
			{
				this.IterateDepartment(treeNode);
			}
		}
	}
	private System.Data.DataTable GetDeparmentsByParent(string parent)
	{
		DeptManageDb deptManageDb = new DeptManageDb();
		System.Data.DataView defaultView = deptManageDb.GetAllDepartment().DefaultView;
		defaultView.RowFilter = string.Format("i_sjdm = {0}", parent);
		return defaultView.ToTable();
	}
	protected void trvwDepartment_SelectedNodeChanged(object sender, EventArgs e)
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		string selectedValue = this.trvwDepartment.SelectedValue;
		this.gvwUser.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0} and v_yhdm!='00000000'", selectedValue));
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
			if (WEB_UserRole.userCodes.Contains(label.ToolTip))
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
	protected void chk_CheckedChanged(object sender, EventArgs e)
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
					if (!WEB_UserRole.userCodes.Contains(label.ToolTip))
					{
						WEB_UserRole.userCodes.Add(label.ToolTip);
					}
				}
				else
				{
					if (WEB_UserRole.userCodes.Contains(label.ToolTip))
					{
						WEB_UserRole.userCodes.Remove(label.ToolTip);
					}
				}
			}
		}
		List<string> names = this.yhmc.GetNames(WEB_UserRole.userCodes);
		this.lblUserNames.Text = JsonHelper.Serialize(names.ToArray());
	}
	protected void chkAll_CheckedChanged(object sender, EventArgs e)
	{
		if (this.gvwUser.HeaderRow.Controls[0].FindControl("chkAll") is CheckBox)
		{
			CheckBox checkBox = this.gvwUser.HeaderRow.Controls[0].FindControl("chkAll") as CheckBox;
			if (checkBox.Checked)
			{
				IEnumerator enumerator = this.gvwUser.Rows.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
						if (gridViewRow.Controls[1].FindControl("lblName") is Label)
						{
							Label label = gridViewRow.Controls[1].FindControl("lblName") as Label;
							if (!WEB_UserRole.userCodes.Contains(label.ToolTip))
							{
								WEB_UserRole.userCodes.Add(label.ToolTip);
							}
						}
					}
					return;
				}
				finally
				{
					IDisposable disposable = enumerator as IDisposable;
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
					if (WEB_UserRole.userCodes.Contains(label2.ToolTip))
					{
						WEB_UserRole.userCodes.Remove(label2.ToolTip);
					}
				}
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		string text = JsonHelper.Serialize(WEB_UserRole.userCodes.ToArray());
		Common2.ExecuteNoQuery(string.Concat(new string[]
		{
			"update ",
			this.tbName,
			" set ",
			this.field,
			" = '",
			text,
			"' where ",
			this.idName,
			" = '",
			this.id,
			"'"
		}));
		base.RegisterScript("window.close();");
	}
}


