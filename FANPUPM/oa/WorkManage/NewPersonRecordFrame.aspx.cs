using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_WorkManage_NewPersonRecordFrame : BasePage, IRequiresSessionState
{
	public HROrgManpowerPlanAction hrmpAction
	{
		get
		{
			return new HROrgManpowerPlanAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Tree_Bind();
		}
	}
	private void Tree_Bind()
	{
		bool isAll = false;
		string text = this.Session["CorpCode"].ToString().Trim();
		if (text == "00")
		{
			isAll = true;
		}
		this.TVDept.Nodes.Clear();
		this.TVDept.Target = "frmInStoreroom";
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "华夏幸福基业";
		treeNode.Value = "";
		this.TVDept.Nodes.Add(treeNode);
		DataTable corpDepartment = this.hrmpAction.GetCorpDepartment(text, isAll);
		if (corpDepartment.Rows.Count > 0)
		{
			foreach (DataRow dataRow in corpDepartment.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["CorpName"].ToString();
				treeNode2.Value = dataRow["CorpCode"].ToString();
				treeNode2.NavigateUrl = "NewPersonRecord.aspx?cc=" + dataRow["CorpCode"].ToString();
				treeNode.ChildNodes.Add(treeNode2);
			}
		}
	}
}


