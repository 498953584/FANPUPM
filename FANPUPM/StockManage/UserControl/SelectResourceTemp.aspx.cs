using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Serialize;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_UserControl_SelectResourceTemp : NBasePage, IRequiresSessionState
{
	private ISerializable ser = new JsonSerializer();
	private new static readonly int pagesize3 = 15;
	private string resourceName = string.Empty;
	private string typeCode = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["resourceName"]))
		{
			this.resourceName = base.Server.UrlDecode(base.Request["resourceName"]);
		}
		if (!string.IsNullOrEmpty(base.Request["TypeCode"]))
		{
			this.typeCode = base.Server.UrlDecode(base.Request["TypeCode"]);
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitDropResourceType();
			this.InitTrvwResourceType();
			this.InitAspNetPager();
			if (this.resourceName != "")
			{
				this.txtName.Text = this.resourceName;
				this.BindByName();
				return;
			}
			this.DataBindResource();
		}
	}
	protected void dropResourceType_SelectedIndexChanged(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.InitTrvwResourceType();
		this.InitResource();
	}
	protected void trvwResourceType_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.InitAspNetPager();
		this.DataBindResource();
	}
	protected void btnSelect_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.DataBindResource();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindResource();
	}
	protected void gvwResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwResource.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["code"] = this.gvwResource.DataKeys[e.Row.RowIndex].Values[1].ToString();
			e.Row.Attributes["name"] = this.gvwResource.DataKeys[e.Row.RowIndex].Values[2].ToString();
		}
	}
	protected void trvwResourceType_TreeNodePopulate(object sender, TreeNodeEventArgs e)
	{
		this.AddChildResourceType(e.Node);
	}
	private void InitDropResourceType()
	{
		string typeCode = "0";
		if (!string.IsNullOrEmpty(base.Request["TypeCode"]))
		{
			typeCode = base.Request["TypeCode"];
		}
		if (!string.IsNullOrEmpty(typeCode))
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				IQueryable<Res_ResourceType> dataSource;
				if (typeCode == "0")
				{
					dataSource =
						from t in pm2Entities.Res_ResourceType
						where t.Res_ResourceType2 == null
						select t;
				}
				else
				{
					dataSource =
						from t in pm2Entities.Res_ResourceType
						where t.ResourceTypeCode == typeCode
						select t;
				}
				this.dropResourceType.DataSource = dataSource;
				this.dropResourceType.DataBind();
			}
		}
	}
	private void InitTrvwResourceType()
	{
		if (!string.IsNullOrEmpty(this.dropResourceType.SelectedValue))
		{
			this.trvwResourceType.Nodes.Clear();
			string typeCode = this.dropResourceType.SelectedValue;
			Res_ResourceType res_ResourceType = new Res_ResourceType();
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				res_ResourceType = (
					from t in pm2Entities.Res_ResourceType
					where t.ResourceTypeCode == typeCode
					select t).FirstOrDefault<Res_ResourceType>();
			}
			TreeNode treeNode = new TreeNode(res_ResourceType.ResourceTypeName, res_ResourceType.ResourceTypeId);
			this.AddChildResourceType(treeNode);
			this.trvwResourceType.Nodes.Add(treeNode);
			this.trvwResourceType.DataBind();
		}
	}
	private void AddChildResourceType(TreeNode parentNode)
	{
		System.Collections.Generic.List<Res_ResourceType> list = new System.Collections.Generic.List<Res_ResourceType>();
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			list = (
				from t in pm2Entities.Res_ResourceType
				where t.Res_ResourceType2.ResourceTypeId == parentNode.Value
				select t).ToList<Res_ResourceType>();
			if (list.Count != 0)
			{
				foreach (Res_ResourceType current in list)
				{
					TreeNode treeNode = new TreeNode(current.ResourceTypeName, current.ResourceTypeId);
					if (this.GetResourceTypeChildCount(current.ResourceTypeId, pm2Entities) > 0)
					{
						treeNode.PopulateOnDemand = true;
					}
					parentNode.ChildNodes.Add(treeNode);
				}
			}
		}
	}
	private int GetResourceTypeChildCount(string typeId, pm2Entities context)
	{
		return (
			from t in context.Res_ResourceType
			where t.Res_ResourceType2.ResourceTypeId == typeId
			select t).Count<Res_ResourceType>();
	}
	private void BindByName()
	{
		if (!string.IsNullOrEmpty(this.trvwResourceType.SelectedValue))
		{
			Resource resource = new Resource();
			DataTable resourceByRerourceName = resource.GetResourceByRerourceName(StockManage_UserControl_SelectResourceTemp.pagesize3, this.AspNetPager1.CurrentPageIndex, this.txtName.Text.Trim());
			this.AspNetPager1.RecordCount = resource.GetResourceCount(this.txtName.Text.Trim());
			this.gvwResource.DataSource = resourceByRerourceName;
			this.gvwResource.DataBind();
		}
	}
	private void InitResource()
	{
		if (string.IsNullOrEmpty(this.trvwResourceType.SelectedValue))
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				string typeId = (
					from t in pm2Entities.Res_ResourceType
					where t.ResourceTypeCode == this.dropResourceType.SelectedValue
					select t.ResourceTypeId).FirstOrDefault<string>();
				Resource resource = new Resource();
				DataTable resource2 = resource.GetResource(typeId, StockManage_UserControl_SelectResourceTemp.pagesize3, this.AspNetPager1.CurrentPageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
				this.AspNetPager1.RecordCount = resource.GetResourceCount(typeId, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
				this.gvwResource.DataSource = resource2;
				this.gvwResource.DataBind();
				return;
			}
		}
		Resource resource3 = new Resource();
		DataTable resourceByRerourceType = resource3.GetResourceByRerourceType(this.trvwResourceType.SelectedValue, StockManage_UserControl_SelectResourceTemp.pagesize3, this.AspNetPager1.CurrentPageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		this.AspNetPager1.RecordCount = resource3.GetResourceCoutByResourceType(this.trvwResourceType.SelectedValue, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		this.gvwResource.DataSource = resourceByRerourceType;
		this.gvwResource.DataBind();
	}
	private void DataBindResource()
	{
		this.txtCode.Text.Trim();
		this.txtName.Text.Trim();
		this.InitResource();
	}
	private void InitAspNetPager()
	{
		this.txtCode.Text = string.Empty;
		this.txtName.Text = string.Empty;
		this.AspNetPager1.PageSize = StockManage_UserControl_SelectResourceTemp.pagesize3;
		this.AspNetPager1.CurrentPageIndex = 1;
		if (string.IsNullOrEmpty(this.trvwResourceType.SelectedValue))
		{
			if (string.IsNullOrEmpty(this.dropResourceType.SelectedValue))
			{
				return;
			}
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				string typeId = (
					from t in pm2Entities.Res_ResourceType
					where t.ResourceTypeCode == this.dropResourceType.SelectedValue
					select t.ResourceTypeId).FirstOrDefault<string>();
				Resource resource = new Resource();
				this.AspNetPager1.RecordCount = resource.GetResourceCount(typeId, string.Empty, string.Empty);
				return;
			}
		}
		Resource resource2 = new Resource();
		resource2.GetResourceByRerourceType(this.trvwResourceType.SelectedValue, StockManage_UserControl_SelectResourceTemp.pagesize3, this.AspNetPager1.CurrentPageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		this.AspNetPager1.RecordCount = resource2.GetResourceCoutByResourceType(this.trvwResourceType.SelectedValue, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
	}
}


