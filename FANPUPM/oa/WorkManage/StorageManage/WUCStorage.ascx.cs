using ASP;
using com.jwsoft.pm.entpm.action;
using Microsoft.Web.UI.WebControls;
using System;
using System.Data;
using System.Web.Profile;
using System.Web.UI;
public partial class oa_WorkManage_StorageManage_WUCStorage : System.Web.UI.UserControl
{
	private ptOfficeResDepotAction ofaction
	{
		get
		{
			return new ptOfficeResDepotAction();
		}
	}
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

	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			this.Bind();
		}
	}
	private void Bind()
	{
		this.TVStorage.Nodes.Clear();
		this.TVStorage.Target = this.TargetFrame;
		TreeNode treeNode = new TreeNode();
		treeNode.Text = "仓库名称";
		this.TVStorage.Nodes.Add(treeNode);
		DataTable list = this.ofaction.GetList("IsValid=1");
		if (list.Rows.Count > 0)
		{
			foreach (DataRow dataRow in list.Rows)
			{
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Text = dataRow["DepotName"].ToString();
				if (this.PrjUrl.IndexOf("?") == -1)
				{
					treeNode2.NavigateUrl = string.Concat(new string[]
					{
						this.PrjUrl,
						"?uc=",
						base.Session["yhdm"].ToString(),
						"&cc=",
						dataRow["CorpCode"].ToString(),
						"&dd=",
						dataRow["DepotID"].ToString()
					});
				}
				else
				{
					treeNode2.NavigateUrl = string.Concat(new string[]
					{
						this.PrjUrl,
						"&uc=",
						base.Session["yhdm"].ToString(),
						"&cc=",
						dataRow["CorpCode"].ToString(),
						"&dd=",
						dataRow["DepotID"].ToString()
					});
				}
				treeNode.Nodes.Add(treeNode2);
			}
		}
	}
}


