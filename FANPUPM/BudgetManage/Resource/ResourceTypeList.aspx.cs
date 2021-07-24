using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_ResourceTypeList : NBasePage, IRequiresSessionState
{

	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindTv();
		this.BindGv();
		if (!string.IsNullOrEmpty(this.tvResource.SelectedValue))
		{
			this.hdParentId.Value = this.tvResource.SelectedValue;
			return;
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
		{
			this.hdParentId.Value = base.Request["id"].ToString();
			return;
		}
		this.hdParentId.Value = this.tvResource.SelectedValue;
	}
	public void BindGv()
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			if (!string.IsNullOrEmpty(this.tvResource.SelectedValue))
			{
				IOrderedQueryable<Res_ResourceType> source =
					from m in pm2Entities.Res_ResourceType
					where m.Res_ResourceType2.ResourceTypeId == this.tvResource.SelectedValue
					orderby m.ResourceTypeCode
					select m;
				this.AspNetPager1.RecordCount = source.Count<Res_ResourceType>();
				this.gvResource.DataSource = source.Skip(NBasePage.pagesize * (currentPageIndex - 1)).Take(NBasePage.pagesize);
			}
			else
			{
				if (!string.IsNullOrEmpty(base.Request.QueryString["id"]))
				{
					string str_resource = base.Request["id"].ToString();
					IOrderedQueryable<Res_ResourceType> source2 =
						from m in pm2Entities.Res_ResourceType
						where m.Res_ResourceType2.ResourceTypeId == str_resource
						orderby m.ResourceTypeCode
						select m;
					this.AspNetPager1.RecordCount = source2.Count<Res_ResourceType>();
					this.gvResource.DataSource = source2.Skip(NBasePage.pagesize * (currentPageIndex - 1)).Take(NBasePage.pagesize);
				}
				else
				{
					IOrderedQueryable<Res_ResourceType> source3 =
						from m in pm2Entities.Res_ResourceType
						where m.Res_ResourceType2.ResourceTypeId == null && m.ResourceTypeName != "人工" && m.ResourceTypeName != "材料" && m.ResourceTypeName != "机械"
						orderby m.ResourceTypeCode
						select m;
					this.AspNetPager1.RecordCount = source3.Count<Res_ResourceType>();
					this.gvResource.DataSource = source3.Skip(NBasePage.pagesize * (currentPageIndex - 1)).Take(NBasePage.pagesize);
				}
			}
			this.gvResource.DataBind();
		}
	}
	public void BindTv()
	{
		TreeNode treeNode = new TreeNode("目录", "");
		if (base.Request["id"] == "")
		{
			treeNode.Select();
		}
		string arg_42_0 = base.Request["id"];
		DataTable resType = cn.justwin.BLL.Resource.GetResType();
		base.Cache[CacheParameter.ResourceType] = resType;
		using (new pm2Entities())
		{
			DataRow[] array = resType.Select(" ParentId is null ");
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode2 = new TreeNode();
				treeNode2.PopulateOnDemand = true;
				treeNode2.Value = dataRow["ResourceTypeId"].ToString();
				treeNode2.Text = dataRow["ResourceTypeName"].ToString();
				if (base.Request["id"] == treeNode2.Value)
				{
					treeNode2.Select();
				}
				treeNode.ChildNodes.Add(treeNode2);
			}
		}
		this.tvResource.Nodes.Add(treeNode);
		if (base.Request["id"] == null && this.tvResource.Nodes.Count > 0)
		{
			this.tvResource.Nodes[0].Select();
		}
	}
	protected void TreeView_Populate(object sender, TreeNodeEventArgs e)
	{
		using (new pm2Entities())
		{
			DataTable dataTable = base.Cache[CacheParameter.ResourceType] as DataTable;
			DataRow[] array = dataTable.Select("ParentId='" + e.Node.Value + "'");
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				TreeNode treeNode = new TreeNode();
				if (ResType.IsContainsChild(dataRow["ResourceTypeId"].ToString()))
				{
					treeNode.PopulateOnDemand = true;
				}
				treeNode.Value = dataRow["ResourceTypeId"].ToString();
				treeNode.Text = dataRow["ResourceTypeName"].ToString();
				if (base.Request["id"] == treeNode.Value)
				{
					treeNode.Select();
				}
				e.Node.ChildNodes.Add(treeNode);
			}
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		bool flag = false;
		System.Collections.IEnumerator enumerator = this.gvResource.Rows.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				GridViewRow gridViewRow = (GridViewRow)enumerator.Current;
				CheckBox cb = gridViewRow.FindControl("cbBox") as CheckBox;
				if (cb != null && cb.Checked)
				{
					using (pm2Entities pm2Entities = new pm2Entities())
					{
						IQueryable<Res_Resource> source =
							from m in pm2Entities.Res_Resource
							where m.Res_ResourceType.ResourceTypeId == cb.ToolTip
							select m;
						IQueryable<Res_ResourceType> source2 =
							from m in pm2Entities.Res_ResourceType
							where m.Res_ResourceType2.ResourceTypeId == cb.ToolTip
							select m;
						string value = (
							from m in pm2Entities.Bud_CostAccounting
							where m.ResourceType == cb.ToolTip
							select m.Id).FirstOrDefault<string>();
						if (source.Count<Res_Resource>() > 0 || source2.Count<Res_ResourceType>() > 0 || !string.IsNullOrEmpty(value))
						{
							flag = true;
							break;
						}
						list.Add(cb.ToolTip);
					}
				}
			}
		}
		finally
		{
			System.IDisposable disposable = enumerator as System.IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
		if (flag)
		{
			base.RegisterScript("alert('系统提示：\\n请先删除子项！')");
			return;
		}
		foreach (string item in list)
		{
			using (pm2Entities pm2Entities2 = new pm2Entities())
			{
				Res_ResourceType entity = (
					from m in pm2Entities2.Res_ResourceType
					where m.ResourceTypeId == item
					select m).First<Res_ResourceType>();
				pm2Entities2.DeleteObject(entity);
				pm2Entities2.SaveChanges();
			}
		}
		base.RegisterScript(string.Concat(new string[]
		{
			"window.location.href='ResourceTypeList.aspx?id=",
			this.tvResource.SelectedValue,
			"';document.cookie='scrollTop='+'",
			base.Request.Cookies["scrollTop"].Value.ToString(),
			"'"
		}));
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvPurchaseplan_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			try
			{
				e.Row.Attributes["id"] = this.gvResource.DataKeys[e.Row.RowIndex].Value.ToString();
			}
			catch
			{
			}
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
		this.hdParentId.Value = this.tvResource.SelectedValue;
	}
}


