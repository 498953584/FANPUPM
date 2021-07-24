using ASP;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_VehicleUserControl_state : NBasePage, IRequiresSessionState
{
	private TypeAndStateBll _vehiclesType = new TypeAndStateBll();

	protected override void OnInit(EventArgs e)
	{
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindProject();
		}
	}
	private void DataBindProject()
	{
		Guid guid = default(Guid);
		this.trvwControlType.Nodes.Clear();
		DataTable list = this._vehiclesType.GetList(" ParentGuid='" + guid + "' and type=1 ");
		TreeNode treeNode = new TreeNode("状态", "0");
		treeNode.NavigateUrl = "javascript:nonFun();";
		this.trvwControlType.Nodes.Add(treeNode);
		foreach (DataRow dataRow in list.Rows)
		{
			TreeNode treeNode2 = new TreeNode(dataRow["Name"].ToString(), dataRow["guid"].ToString());
			treeNode2.NavigateUrl = "javascript:nonFun();";
			treeNode2.ToolTip = "type";
			this.AddContractType(treeNode2, dataRow["guid"].ToString());
			treeNode.ChildNodes.Add(treeNode2);
		}
		this.trvwControlType.ExpandAll();
	}
	private void AddContractType(TreeNode parentNode, string prjGuid)
	{
		string strWhere = string.Format(" ParentGuid = '{0}' and type=1 ", prjGuid);
		List<TypeAndState> modelList = this._vehiclesType.GetModelList(strWhere);
		if (modelList.Count > 0)
		{
			foreach (TypeAndState current in modelList)
			{
				TreeNode treeNode = new TreeNode(current.Name, current.guid.ToString());
				treeNode.ToolTip = "ContractType";
				parentNode.ChildNodes.Add(treeNode);
			}
		}
	}
	protected void trvwControlType_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.hfldVehicleStateID.Value = this.trvwControlType.SelectedNode.Value;
		this.hfldVehicleStateName.Value = this.trvwControlType.SelectedNode.Text;
	}
}


