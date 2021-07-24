using ASP;
using cn.justwin.BLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class EPC_Workflow_TemplatePrivilege : NBasePage, IRequiresSessionState
{
	private PtYhmcBll yhmc = new PtYhmcBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			string templateid = base.Request["tid"].ToString();
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			DataTable tempLimitUser = FlowTemplateAction.GetTempLimitUser(templateid);
			if (tempLimitUser.Rows.Count > 0)
			{
				for (int i = 0; i < tempLimitUser.Rows.Count; i++)
				{
					HiddenField expr_64 = this.hfldUserCodes;
					expr_64.Value = expr_64.Value + tempLimitUser.Rows[i][0].ToString() + ",";
					list.Add(tempLimitUser.Rows[i][0].ToString());
				}
				this.DataBindUserName(list);
				return;
			}
		}
		else
		{
			if (this.hfldUserCodes.Value != "")
			{
				System.Collections.Generic.List<string> @string = this.GetString(this.hfldUserCodes.Value);
				this.DataBindUserName(@string);
			}
		}
	}
	private void DataBindUserName(System.Collections.Generic.List<string> userCodes)
	{
		System.Collections.Generic.List<PtYhmc> list = new System.Collections.Generic.List<PtYhmc>();
		foreach (string current in userCodes)
		{
			PtYhmc modelById = this.yhmc.GetModelById(current);
			if (modelById != null)
			{
				list.Add(modelById);
			}
		}
		this.lbUser.DataSource = list;
		this.lbUser.DataBind();
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
		this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where state=1 and i_bmdm = {0} ", selectedValue));
		this.lbSelect.DataBind();
		if (this.hfldUserCodes.Value != "")
		{
			System.Collections.Generic.List<string> @string = this.GetString(this.hfldUserCodes.Value);
			this.DataBindUserName(@string);
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> @string = this.GetString(this.hfldUserCodes.Value);
		string templateid = base.Request["tid"].ToString();
		try
		{
			FlowTemplateAction.AddTempLimitUser(templateid, @string);
			string str = string.Concat(new string[]
			{
				"TemplateList.aspx?ti=",
				base.Request["code"].ToString(),
				"&class=",
				base.Request["class"].ToString(),
				"&flag=",
				base.Request["flag"].ToString(),
				" "
			});
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			stringBuilder.Append("alert('系统提示：\\n\\n更新成功')").Append(System.Environment.NewLine);
			stringBuilder.Append("winclose('flowclass', '" + str + "', true);");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (System.Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n更新失败')");
		}
	}
	public System.Collections.Generic.List<string> GetString(string str)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string text;
		if (str.Substring(0, 1) == ",")
		{
			text = str.Substring(1, str.Length - 2);
		}
		else
		{
			text = str.Substring(0, str.Length - 1);
		}
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
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where state=1 and  v_xm like '%{0}%'", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
		if (!string.IsNullOrEmpty(this.hfldUserCodes.Value.Trim()))
		{
			System.Collections.Generic.List<string> @string = this.GetString(this.hfldUserCodes.Value);
			this.DataBindUserName(@string);
		}
	}
}


