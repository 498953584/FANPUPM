using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class HR_Leave_StatMain : BasePage, IRequiresSessionState
{
	public StatAction sa
	{
		get
		{
			return new StatAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			base.Response.Cache.SetNoStore();
			this.Tree_Bind(this.TVCorpCode.Nodes, "1");
		}
	}
	private void Tree_Bind(TreeNodeCollection nodes, string sj)
	{
		DataTable bMList = this.sa.GetBMList(this.Session["CorpCode"].ToString(), sj);
		if (bMList.Rows.Count > 0)
		{
			foreach (DataRow dataRow in bMList.Rows)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = dataRow["V_BMMC"].ToString();
				treeNode.Value = dataRow["i_bmdm"].ToString();
				treeNode.NavigateUrl = "StatCountList.aspx?bm=" + dataRow["i_bmdm"].ToString();
				treeNode.Target = "frmStat";
				nodes.Add(treeNode);
				this.Tree_Bind(treeNode.ChildNodes, dataRow["i_bmdm"].ToString());
			}
		}
	}
}


