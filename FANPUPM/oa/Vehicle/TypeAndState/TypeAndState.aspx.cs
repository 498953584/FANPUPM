using ASP;
using cn.justwin.BLL;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_TypeAndState_TypeAndState : NBasePage, IRequiresSessionState
{
	private TypeAndStateBll TASBLL = new TypeAndStateBll();

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		this.vac.Value = "0";
		if (!base.IsPostBack)
		{
			string value = base.Request.QueryString["prjID"];
			if (!string.IsNullOrEmpty(value))
			{
				this.hfldSelectedPrj.Value = value;
			}
			else
			{
				if (this.hfldSelectedPrj.Value == "")
				{
					this.hfldSelectedPrj.Value = "00000000-0000-0000-0000-000000000000";
				}
			}
			this.DataBindProject();
			string strWhere = string.Format(" ParentGuid = '{0}'", this.hfldSelectedPrj.Value);
			DataTable list = this.TASBLL.GetList(strWhere);
			if (list.Rows.Count >= 2)
			{
				this.vac.Value = "1";
				this.btnAdd.Disabled = false;
			}
			this.VehicleType.DataSource = list;
			this.VehicleType.DataBind();
			this.KeepTreeViewState();
		}
	}
	private void KeepTreeViewState()
	{
		foreach (TreeNode treeNode in this.trvwProject.Nodes)
		{
			if (this.hfldSelectedPrj.Value == "00000000-0000-0000-0000-000000000000")
			{
				treeNode.Selected = true;
				break;
			}
			this.GetSelectedNode(treeNode);
		}
	}
	private void GetSelectedNode(TreeNode CNode)
	{
		foreach (TreeNode treeNode in CNode.ChildNodes)
		{
			if (treeNode.Value == this.hfldSelectedPrj.Value)
			{
				treeNode.Selected = true;
				break;
			}
			this.GetSelectedNode(treeNode);
		}
	}
	protected void gvwContractType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.VehicleType.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void trvwProject_SelectedNodeChanged(object sender, EventArgs e)
	{
		try
		{
			if (this.trvwProject.SelectedNode.Parent == this.trvwProject.Nodes[0])
			{
				this.hfldSelectedPrj.Value = this.trvwProject.SelectedValue;
				this.DataBindContractType();
				this.btnAdd.Disabled = false;
			}
			else
			{
				if (this.trvwProject.SelectedValue == "0")
				{
					this.hfldSelectedPrj.Value = "00000000-0000-0000-0000-000000000000";
					this.DataBindContractType();
					this.btnAdd.Disabled = false;
				}
				else
				{
					this.hfldSelectedPrj.Value = this.trvwProject.SelectedValue;
					this.DataBindContractType();
					this.btnAdd.Disabled = false;
				}
			}
		}
		catch
		{
		}
	}
	private void DataBindContractType()
	{
		this.vac.Value = "0";
		this.btnAdd.Visible = true;
		if (!string.IsNullOrEmpty(this.trvwProject.SelectedValue))
		{
			if (this.trvwProject.SelectedValue == "0")
			{
				string strWhere = string.Format(" ParentGuid = '{0}'", "00000000-0000-0000-0000-000000000000");
				DataTable list = this.TASBLL.GetList(strWhere);
				this.VehicleType.DataSource = list;
				this.VehicleType.DataBind();
				if (list.Rows.Count >= 2)
				{
					this.vac.Value = "1";
					return;
				}
				this.vac.Value = "0";
				return;
			}
			else
			{
				this.vac.Value = "0";
				string strWhere2 = string.Format(" ParentGuid = '{0}'", this.trvwProject.SelectedValue);
				this.VehicleType.DataSource = this.TASBLL.GetList(strWhere2);
				this.VehicleType.DataBind();
			}
		}
	}
	private void DataBindProject()
	{
		Guid guid = default(Guid);
		this.trvwProject.Nodes.Clear();
		DataTable list = this.TASBLL.GetList(" ParentGuid='" + guid + "'");
		if (list.Rows.Count < 2)
		{
			this.NN.Value = "N";
		}
		TreeNode treeNode = new TreeNode("车辆类型和状态", "0");
		this.trvwProject.Nodes.Add(treeNode);
		foreach (DataRow dataRow in list.Rows)
		{
			TreeNode treeNode2 = new TreeNode(dataRow["Name"].ToString(), dataRow["guid"].ToString());
			this.AddContractType(treeNode2, dataRow["guid"].ToString());
			treeNode.ChildNodes.Add(treeNode2);
		}
		this.trvwProject.ExpandAll();
	}
	private void AddContractType(TreeNode node, string guid)
	{
		string strWhere = string.Format(" ParentGuid = '{0}'", guid);
		List<TypeAndState> modelList = this.TASBLL.GetModelList(strWhere);
		foreach (TypeAndState current in modelList)
		{
			TreeNode child = new TreeNode(current.Name, current.guid.ToString());
			node.ChildNodes.Add(child);
		}
	}
	protected void btnDelete_Click(object sender, EventArgs e)
	{
		if (!string.IsNullOrEmpty(this.hfldContractTypeGuid.Value))
		{
			List<string> list = new List<string>();
			if (this.hfldContractTypeGuid.Value.Contains("["))
			{
				list = JsonHelper.GetListFromJson(this.hfldContractTypeGuid.Value);
			}
			else
			{
				list.Add(this.hfldContractTypeGuid.Value);
			}
			if (list.Count > 0)
			{
				for (int i = 0; i < list.Count; i++)
				{
					List<TypeAndState> modelList = this.TASBLL.GetModelList("ParentGuid='" + new Guid(list[i].ToString()) + "'");
					if (this.TASBLL.GetModel(new Guid(list[i].ToString())).ParentGuid.ToString() == default(Guid).ToString() && modelList.Count > 0)
					{
						StringBuilder stringBuilder = new StringBuilder();
						stringBuilder.Append("alert('系统提示：\\n\\n 类型正在使用！');").Append(Environment.NewLine);
						base.RegisterScript(stringBuilder.ToString());
						break;
					}
					MainBLL mainBLL = new MainBLL();
					if (mainBLL.Exists(string.Concat(new string[]
					{
						"VehicleType='",
						list[i].ToString(),
						"' or State='",
						list[i].ToString(),
						"'"
					})))
					{
						StringBuilder stringBuilder2 = new StringBuilder();
						stringBuilder2.Append("alert('系统提示：\\n\\n 类型正在使用！');").Append(Environment.NewLine);
						base.RegisterScript(stringBuilder2.ToString());
						break;
					}
					this.TASBLL.Delete(new Guid(list[i].ToString()));
				}
				this.DataBindContractType();
				this.DataBindProject();
				this.KeepTreeViewState();
			}
		}
	}
}


