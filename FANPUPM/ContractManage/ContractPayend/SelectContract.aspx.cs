using ASP;
using cn.justwin.BLL;
using cn.justwin.contractBLL;
using cn.justwin.contractModel;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class ContractManage_ContractPayend_SelectContract : NBasePage, IRequiresSessionState
{
	private IncometContractBll incometContractBll = new IncometContractBll();

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindContract();
		}
	}
	public void BindContract()
	{
		List<IncometContractModel> listArray = this.incometContractBll.GetListArray(" where isFContract='true' and IsArchived='false'");
		foreach (IncometContractModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Text = current.ContractName;
			treeNode.ToolTip = current.ContractCode;
			treeNode.Value = current.ContractID;
			this.ChileNode(treeNode);
			this.tvContract.Nodes.Add(treeNode);
		}
	}
	public void ChileNode(TreeNode node)
	{
		List<IncometContractModel> listArray = this.incometContractBll.GetListArray(" where FCode='" + node.Value + "' and IsArchived='false'");
		foreach (IncometContractModel current in listArray)
		{
			TreeNode treeNode = new TreeNode();
			treeNode.Text = current.ContractName;
			treeNode.ToolTip = current.ContractCode;
			treeNode.Value = current.ContractID;
			node.ChildNodes.Add(treeNode);
			this.ChileNode(treeNode);
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		base.RegisterScript(string.Concat(new string[]
		{
			"setVal('",
			this.tvContract.SelectedValue,
			"','",
			this.tvContract.SelectedNode.Text,
			"')"
		}));
	}
	protected void tvContract_SelectedNodeChanged(object sender, EventArgs e)
	{
		IncometContractModel model = this.incometContractBll.GetModel(this.tvContract.SelectedValue);
		this.lblContractCode.Text = model.ContractCode;
		this.lblContractName.Text = model.ContractName;
		this.lblContractPrice.Text = model.ContractPrice.ToString();
		this.lblSinedTime.Text = Common2.GetTime(model.SignedTime.ToString());
		this.lblContractTitle.Text = model.ContractName + "合同信息";
	}
}


