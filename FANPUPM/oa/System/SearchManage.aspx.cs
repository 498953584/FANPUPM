using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_System_SearchManage : BasePage, IRequiresSessionState
{
	public SystemInfoAction sia
	{
		get
		{
			return new SystemInfoAction();
		}
	}

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.dropdownlistBind();
			this.Bind();
		}
	}
	protected void dropdownlistBind()
	{
		DataTable corpName = this.sia.GetCorpName();
		this.drpbm.DataValueField = "CorpCode";
		this.drpbm.DataTextField = "CorpName";
		this.drpbm.DataSource = corpName;
		this.drpbm.DataBind();
	}
	private void Bind()
	{
		this.tv.Nodes.Clear();
		this.tv.Target = "Search_right";
		DataTable dataTable = new DataTable();
		if (this.drpbm.SelectedValue == "00")
		{
			dataTable = this.sia.GetClassID("a.CorpCode='" + this.drpbm.SelectedValue + "' and b.ClassTypeCode in(002,003)");
		}
		else
		{
			dataTable = this.sia.GetClassID("a.CorpCode='" + this.drpbm.SelectedValue + "' and b.ClassTypeCode='003'");
		}
		DataRow[] array = dataTable.Select("ParentClassCode=''", " ClassCode asc ");
		for (int i = 0; i < array.Length; i++)
		{
			Microsoft.Web.UI.WebControls.TreeNode treeNode = new Microsoft.Web.UI.WebControls.TreeNode();
			treeNode.Text = array[i]["ClassName"].ToString();
			treeNode.NavigateUrl = "Search_right.aspx?cid=" + array[i]["ClassID"].ToString();
			treeNode.Target = "rFrame";
			this.tv.Nodes.Add(treeNode);
			this.CreateChildNodes(treeNode, dataTable, "ParentClassCode='" + array[i]["ClassCode"].ToString() + "'");
		}
	}
	private void CreateChildNodes(Microsoft.Web.UI.WebControls.TreeNode tvBooks, DataTable dt, string strFilter)
	{
		DataRow[] array = dt.Select(strFilter ?? "", " ClassCode asc ");
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				Microsoft.Web.UI.WebControls.TreeNode treeNode = new Microsoft.Web.UI.WebControls.TreeNode();
				treeNode.Target = "rFrame";
				treeNode.Text = array[i]["ClassName"].ToString();
				treeNode.NavigateUrl = "Search_right.aspx?cid=" + array[i]["ClassID"].ToString();
				tvBooks.Nodes.Add(treeNode);
				strFilter = string.Concat(new string[]
				{
					"ClassTypeCode=",
					array[i]["ClassTypeCode"].ToString(),
					" and ParentClassCode='",
					array[i]["ClassCode"].ToString(),
					"'"
				});
				this.CreateChildNodes(treeNode, dt, strFilter);
			}
		}
	}
	protected void drpbm_SelectedIndexChanged(object sender, EventArgs e)
	{
		this.Bind();
	}
}


