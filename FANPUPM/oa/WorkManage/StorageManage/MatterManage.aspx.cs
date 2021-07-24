using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
public partial class oa_WorkManage_StorageManage_MatterManage : BasePage, IRequiresSessionState
{
	public ptOfficeResClassAction amAction
	{
		get
		{
			return new ptOfficeResClassAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		this.TVBookClass.Nodes.Clear();
		this.TVBookClass.Target = "frmMatter";
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "办公用品类";
		this.TVBookClass.Nodes.Add(treeNode);
		DataTable list = this.amAction.GetList("");
		this.CreateChildNodes(treeNode, list, "ParentCode=''");
	}
	private void CreateChildNodes(TreeNode tvBooks, DataTable dt, string strFilter)
	{
		DataRow[] array = dt.Select(strFilter ?? "", " TypeCode asc ");
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = array[i]["TypeName"].ToString();
				treeNode.NavigateUrl = "MatterBill.aspx?uc=" + this.Session["yhdm"].ToString() + "&cc=" + array[i]["TypeCode"].ToString();
				tvBooks.Nodes.Add(treeNode);
				strFilter = " ParentCode='" + array[i]["TypeCode"].ToString() + "'";
				this.CreateChildNodes(treeNode, dt, strFilter);
			}
		}
	}
}


