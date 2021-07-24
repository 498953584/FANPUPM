using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.data;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Enterprise_InsClassTree : BasePage, IRequiresSessionState
{
	private static string dRole = "";

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.Bind_DataTV();
			this.txtSearch.Attributes["readonly"] = "readonly";
		}
	}
	protected void Bind_DataTV()
	{
		string sqlString = " select i_dutyid from pt_yhmc where v_yhdm='" + this.Session["yhdm"].ToString() + "' ";
		Enterprise_InsClassTree.dRole = publicDbOpClass.ExecuteScalar(sqlString).ToString();
		DataTable dt = new DataTable();
		string sqlString2 = " select * from InstitutionClass order by LeveCode ";
		dt = publicDbOpClass.DataTableQuary(sqlString2);
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "制度分类";
		this.TVInsClassTree.Nodes.Add(treeNode);
		this.CreateTree(dt, treeNode, "");
	}
	protected void CreateTree(DataTable dt, TreeNode tn, string leveCode)
	{
		DataRow[] array = dt.Select(string.Concat(new object[]
		{
			"LeveCode like '",
			leveCode,
			"%' and len(LeveCode)='",
			leveCode.Length + 3,
			"'"
		}), " levecode asc ");
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			TreeNode treeNode = new TreeNode();
			if (dataRow["PermissionSet"].ToString().IndexOf(Enterprise_InsClassTree.dRole) >= 0 || dataRow["PermissionSet"].ToString().IndexOf(this.Session["yhdm"].ToString()) >= 0 || dataRow["PermissionClass"].ToString() == "-1")
			{
				treeNode.Text = dataRow["ClassName"].ToString();
				treeNode.Value = dataRow["LeveCode"].ToString();
				tn.ChildNodes.Add(treeNode);
			}
			this.CreateTree(dt, treeNode, dataRow["LeveCode"].ToString());
		}
	}
	protected void TVInsClassTree_SelectedNodeChanged(object sender, EventArgs e)
	{
		this.txtSearch.Text = this.TVInsClassTree.SelectedNode.Text;
		this.HdnLeveCode.Value = this.TVInsClassTree.SelectedValue;
	}
}


