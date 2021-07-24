using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.stockBLL;
using cn.justwin.stockModel;
using com.jwsoft.pm.entpm.action;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Common_DivSetRole : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private string idName = string.Empty;
	private string tbName = string.Empty;
	private string field = string.Empty;
	private string btnId = string.Empty;
	private System.Collections.Generic.List<string> listContractId;
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
		this.listContractId = JsonHelper.GetListFromJson(this.id);
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			if (this.listContractId.Count <= 1)
			{
				DataTable table = Common2.GetTable(this.tbName, string.Concat(new string[]
				{
					" where ",
					this.idName,
					"='",
					this.id,
					"'"
				}));
				this.hfldUserCodes.Value = table.Rows[0][this.field].ToString();
				System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
				if (listFromJson.Count > 0)
				{
					this.DataBindUserName(listFromJson);
					return;
				}
			}
		}
		else
		{
			if (this.hfldUserCodes.Value != "")
			{
				System.Collections.Generic.List<string> userCodes = JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToList<string>();
				this.DataBindUserName(userCodes);
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
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> userCodes = this.GetUserCodes(this.hfldUserCodes.Value);
		if (this.listContractId.Count > 1)
		{
			using (System.Collections.Generic.List<string>.Enumerator enumerator = this.listContractId.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					string current = enumerator.Current;
					DataTable table = Common2.GetTable(this.tbName, string.Concat(new string[]
					{
						" where ",
						this.idName,
						"='",
						current,
						"'"
					}));
					System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(table.Rows[0][this.field].ToString());
					listFromJson.AddRange(userCodes.ToArray());
					System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
					string[] array = listFromJson.ToArray();
					for (int i = 0; i < array.Length; i++)
					{
						string item = array[i];
						if (!list.Contains(item))
						{
							list.Add(item);
						}
					}
					string text = JsonHelper.Serialize(list.ToArray());
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
						current,
						"'"
					}));
				}
				goto IL_22D;
			}
		}
		System.Collections.Generic.List<string> listFromJson2 = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
		string text2 = JsonHelper.Serialize(listFromJson2.ToArray());
		Common2.ExecuteNoQuery(string.Concat(new string[]
		{
			"update ",
			this.tbName,
			" set ",
			this.field,
			" = '",
			text2,
			"' where ",
			this.idName,
			" = '",
			this.id,
			"'"
		}));
		IL_22D:
		base.RegisterScript("saveEvent();");
	}
	protected void btnQuery_Click(object sender, System.EventArgs e)
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format(" where v_xm like '%{0}%'", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
		System.Collections.Generic.List<string> listFromJson = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
		this.DataBindUserName(listFromJson);
	}
	protected System.Collections.Generic.List<string> GetUserCodes(string str)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (!string.IsNullOrEmpty(str) && !str.Contains("["))
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
		return list;
	}
}


