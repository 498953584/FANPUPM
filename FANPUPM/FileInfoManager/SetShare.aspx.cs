using ASP;
using cn.justwin.BLL;
using cn.justwin.contractDAL;
using cn.justwin.stockBLL;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class FileInfoManager_SetShare : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private string idName = string.Empty;
	private string tbName = string.Empty;
	private string field = string.Empty;
	private string btnId = string.Empty;
	private PtYhmcBll yhmc = new PtYhmcBll();
	private ContractType contractType = new ContractType();

	protected override void OnInit(System.EventArgs e)
	{
		this.id = base.Request.QueryString["id"];
		this.idName = base.Request.QueryString["idName"];
		this.tbName = base.Request.QueryString["tbName"];
		this.field = base.Request.QueryString["field"];
		if (!string.IsNullOrEmpty(base.Request["btnId"]))
		{
			this.btnId = base.Request["btnId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			DataTable table = Common2.GetTable(this.tbName, string.Concat(new string[]
			{
				" where ",
				this.idName,
				"='",
				this.id,
				"'"
			}));
			if (table.Rows.Count > 0)
			{
				this.hfldUserCodes.Value = table.Rows[0][this.field].ToString();
				System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
				if (listFromJson.Count > 0)
				{
					this.DataBindUserName(listFromJson);
				}
			}
			else
			{
				this.hfldUserCodes.Value = "[\"00000000\"]";
				System.Collections.Generic.List<string> listFromJson2 = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
				this.DataBindUserName(listFromJson2);
			}
			this.thisUserCode.Value = base.UserCode;
		}
	}
	private void DataBindUserName(System.Collections.Generic.List<string> userCodes)
	{
		System.Collections.Generic.List<string> names = this.yhmc.GetNames(userCodes);
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		for (int i = 0; i < userCodes.Count; i++)
		{
			stringBuilder.Append("<div  style='margin: 2px 2px 1px 2px; padding:2px 7px 2px 2px; border:solid 1px grey; width:180px;'>");
			stringBuilder.Append("<span>").Append(names[i]).Append("</span>");
			stringBuilder.AppendFormat("<span id='{0}' style='float:right;cursor:pointer;' onclick='setDelUser(this)'  class='userName'>×</span>", userCodes[i]).Append("<br/>");
			stringBuilder.Append("</div>");
		}
		if (stringBuilder.Length > 0)
		{
			this.lblUserNames.Text = stringBuilder.ToString();
			return;
		}
		this.lblUserNames.Text = "";
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
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		string selectedValue = this.trvwDepartment.SelectedValue;
		this.gvwUser.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where i_bmdm = {0}", selectedValue));
		this.gvwUser.DataBind();
		this.DataBindUser(listFromJson);
		this.DataBindUserName(listFromJson);
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
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
		string text = JsonHelper.Serialize(listFromJson.ToArray());
		string[] array = this.hdIds.Value.Split(new char[]
		{
			','
		});
		for (int i = 0; i < array.Length; i++)
		{
			string text2 = array[i];
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
				text2,
				"'"
			}));
		}
		if (string.IsNullOrEmpty(this.btnId))
		{
			base.RegisterScript("alert('系统提示:\\n\\n更新成功！');window.opener.location = window.opener.location;window.close();");
			return;
		}
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		stringBuilder.AppendFormat("window.opener.document.getElementById('{0}').click();", this.btnId).AppendLine();
		stringBuilder.Append("window.close();").AppendLine();
		base.RegisterScript(stringBuilder.ToString());
	}
	private void DataBindUser(System.Collections.Generic.List<string> userCodes)
	{
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
			if (userCodes.Contains(label.ToolTip))
			{
				checkBox.Checked = true;
			}
			else
			{
				checkBox.Checked = false;
			}
		}
	}
}


