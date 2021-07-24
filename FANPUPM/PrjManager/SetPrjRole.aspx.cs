using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.Tender;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_SetPrjRole : NBasePage, IRequiresSessionState
{
	private PtYhmcBll yhmc = new PtYhmcBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			string text = base.Request["ids"].ToString();
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			if (!text.Contains(","))
			{
				System.Collections.Generic.List<TenderUser> byId = TenderUser.GetById(text);
				foreach (TenderUser current in byId)
				{
					HiddenField expr_6E = this.hfldUserCodes;
					expr_6E.Value = expr_6E.Value + current.UserCode + ",";
					list.Add(current.UserCode);
				}
				this.DataBindUserName(list);
			}
		}
	}
	private void DataBindUserName(System.Collections.Generic.List<string> userCodes)
	{
		System.Collections.Generic.List<string> names = this.yhmc.GetNames(userCodes);
		System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
		for (int i = 0; i < names.Count; i++)
		{
			stringBuilder.Append("<div  style='margin: 2px 2px 1px 2px; padding:2px 7px 2px 2px; border:solid 1px grey; width:180px;'>");
			stringBuilder.Append("<span>").Append(names[i]).Append("</span>");
			stringBuilder.AppendFormat("<span id='{0}' style='float:right;cursor:pointer;' onclick=usersClick(this);  class='userName'>×</span>", userCodes[i]).Append("<br/>");
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
		if (this.hfldUserCodes.Value != "")
		{
			System.Collections.Generic.List<string> @string = this.GetString(this.hfldUserCodes.Value);
			this.DataBindUser(@string);
			this.DataBindUserName(@string);
		}
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		string selectedValue = this.trvwDepartment.SelectedValue;
		this.gvwUser.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where state=1 and i_bmdm = {0} ", selectedValue));
		this.gvwUser.DataBind();
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
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> @string = this.GetString(this.hfldUserCodes.Value);
		string text = base.Request["ids"].ToString();
		try
		{
			if (text.Contains(","))
			{
				System.Collections.Generic.List<string> string2 = this.GetString(text);
				TenderUser.AddUser(string2, @string);
			}
			else
			{
				TenderUser.AddUser(text, @string);
			}
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("alert('系统提示：\\n\\n更新成功')").Append(System.Environment.NewLine);
			stringBuilder.Append("winclose('SetPrjRole', 'PrjInfoList.aspx', true);");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("alert('系统提示：\\n\\n更新失败')");
		}
	}
	public System.Collections.Generic.List<string> GetString(string str)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string text = str.Substring(0, str.Length - 1);
		string[] array = text.Split(new char[]
		{
			','
		});
		string[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			string item = array2[i];
			list.Add(item);
		}
		return list;
	}
}


