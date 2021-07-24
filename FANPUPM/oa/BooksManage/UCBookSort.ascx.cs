using ASP;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
public partial class oa_BooksManage_UCBookSort : System.Web.UI.UserControl
{

	public string TargetFrame
	{
		get
		{
			object obj = this.ViewState["TargetFrame"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["TargetFrame"] = value;
		}
	}
	public string PrjUrl
	{
		get
		{
			object obj = this.ViewState["PrjUrl"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["PrjUrl"] = value;
		}
	}
	public string TypeName
	{
		get
		{
			object obj = this.ViewState["TypeName"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["TypeName"] = value;
		}
	}
	public string ClassTypeCode
	{
		get
		{
			object obj = this.ViewState["ClassTypeCode"];
			if (obj != null)
			{
				return (string)obj;
			}
			return string.Empty;
		}
		set
		{
			this.ViewState["ClassTypeCode"] = value;
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
		this.TVBookClass.Target = this.TargetFrame;
		TreeNode treeNode = new TreeNode();
		treeNode.Text = this.TypeName;
		this.TVBookClass.Nodes.Add(treeNode);
		DataTable booksClass = BooksCommonClass.GetBooksClass(this.ClassTypeCode);
		this.CreateChildNodes(treeNode, booksClass, "ParentClassCode=''");
	}
	private void CreateChildNodes(TreeNode tvBooks, DataTable dt, string strFilter)
	{
		DataRow[] array = dt.Select(strFilter ?? "", " ClassCode asc ");
		if (array.Length > 0)
		{
			for (int i = 0; i < array.Length; i++)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = array[i]["ClassName"].ToString();
				if (this.PrjUrl.IndexOf("?") == -1)
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						this.PrjUrl,
						"?uc=",
						base.Session["yhdm"].ToString(),
						"&cid=",
						array[i]["ClassID"].ToString()
					});
				}
				else
				{
					treeNode.NavigateUrl = string.Concat(new string[]
					{
						this.PrjUrl,
						"uc=",
						base.Session["yhdm"].ToString(),
						"&cid=",
						array[i]["ClassID"].ToString()
					});
				}
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
}


