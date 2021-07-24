using ASP;
using cn.justwin.BLL;
using cn.justwin.PrjManager;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class PrjManager_SetPrjMember : NBasePage, IRequiresSessionState
{
	private PtYhmcBll yhmc = new PtYhmcBll();

	protected void Page_Load(object sender, System.EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			string text = base.Request["id"].ToString();
			if (!text.Contains(","))
			{
				System.Collections.Generic.List<string> codes = PrjMember.GetCodes(text);
				if (codes.Count > 0)
				{
					this.hfldUserCodes.Value = "[\"";
					foreach (string current in codes)
					{
						HiddenField expr_87 = this.hfldUserCodes;
						expr_87.Value = expr_87.Value + current + "\",\"";
					}
					this.hfldUserCodes.Value = this.hfldUserCodes.Value.Substring(0, this.hfldUserCodes.Value.Length - 2) + "]";
					this.DataBindUserName(codes);
					return;
				}
			}
		}
		else
		{
			System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
			this.DataBindUserName(listFromJson);
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
		this.lblUserNames.DataSource = list;
		this.lblUserNames.DataBind();
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
		if (selectedValue != "1")
		{
			this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where state=1 and i_bmdm = {0} ", selectedValue));
		}
		else
		{
			this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where state=1 ", new object[0]));
		}
		this.lbSelect.DataBind();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
		this.DataBindUserName(listFromJson);
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format(" where v_xm like '%{0}%'", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
		this.DataBindUserName(listFromJson);
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> userCodes = this.GetUserCodes(this.hfldUserCodes.Value);
		string text = base.Request["id"].ToString();
		try
		{
			if (text.Contains(","))
			{
				System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(text);
				PrjMember.Add(listFromJson, userCodes);
			}
			else
			{
				PrjMember.Add(text, userCodes);
			}
			base.RegisterScript("top.ui.winSuccess({parentName: '_SetPrjMember'});top.ui.show('更新成功！');");
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.alert('更新失败！');");
		}
	}
	public System.Collections.Generic.List<string> GetString(string str)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (str != "")
		{
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
		}
		return list;
	}
	protected System.Collections.Generic.List<string> GetUserCodes(string str)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (!string.IsNullOrEmpty(str))
		{
			if (!str.Contains("["))
			{
				string text = "[\"" + str + "\"]";
				string[] array = text.Substring(0, text.Length - 1).Substring(1).Split(new char[]
				{
					','
				});
				string[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					string text2 = array2[i];
					string item = text2.Substring(0, text2.Length - 1).Substring(1);
					list.Add(item);
				}
			}
			else
			{
				list = JsonHelper.GetListFromJson(str);
			}
		}
		return list;
	}
}


