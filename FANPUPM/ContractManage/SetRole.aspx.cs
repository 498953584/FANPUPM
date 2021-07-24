using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
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
public partial class ContractManage_SetRole : NBasePage, IRequiresSessionState
{
	private string id = string.Empty;
	private string idName = string.Empty;
	private string tbName = string.Empty;
	private string field = string.Empty;
	private string btnId = string.Empty;
	private List<string> ContractTypeId;
	private PtYhmcBll yhmc = new PtYhmcBll();
	private ContractType contractType = new ContractType();

	protected override void OnInit(EventArgs e)
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
	protected void Page_Load(object sender, EventArgs e)
	{
		base.Response.Cache.SetNoStore();
		this.ContractTypeId = JsonHelper.GetListFromJson(this.id);
		if (!this.Page.IsPostBack)
		{
			this.DataBindDepartment();
			if (this.ContractTypeId.Count <= 1)
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
				List<string> listFromJson = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
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
				List<string> listFromJson2 = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
				if (listFromJson2.Count > 0)
				{
					this.DataBindUserName(listFromJson2);
				}
			}
		}
	}
	private void DataBindUserName(List<string> userCodes)
	{
		List<PtYhmc> list = new List<PtYhmc>();
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
	protected void trvwDepartment_SelectedNodeChanged(object sender, EventArgs e)
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
		List<string> listFromJson = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
		this.DataBindUserName(listFromJson);
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		List<string> userCodes = this.GetUserCodes(this.hfldUserCodes.Value);
		if (this.ContractTypeId.Count > 1)
		{
			using (List<string>.Enumerator enumerator = this.ContractTypeId.GetEnumerator())
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
					List<string> listFromJson = JsonHelper.GetListFromJson(table.Rows[0][this.field].ToString());
					listFromJson.AddRange(userCodes.ToArray());
					List<string> list = new List<string>();
					foreach (string current2 in listFromJson)
					{
						if (!list.Contains(current2))
						{
							list.Add(current2);
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
				goto IL_237;
			}
		}
		List<string> listFromJson2 = JsonHelper.GetListFromJson(this.hfldUserCodes.Value);
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
		IL_237:
		base.RegisterScript("saveEvent();");
	}
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		PtYhmcBll ptYhmcBll = new PtYhmcBll();
		this.lbSelect.DataSource = ptYhmcBll.GetAllModelByWhere(string.Format("where state=1 and v_xm LIKE '%{0}%' ", this.txtQuery.Text.Trim()));
		this.lbSelect.DataBind();
		List<string> listFromJson = JsonHelper.GetListFromJson(JsonHelper.Serialize(JsonHelper.GetListFromJson(this.hfldUserCodes.Value).ToArray()));
		if (listFromJson.Count > 0)
		{
			this.DataBindUserName(listFromJson);
		}
	}
	protected List<string> GetUserCodes(string str)
	{
		List<string> list = new List<string>();
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


