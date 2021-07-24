using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_TypeAttribute : NBasePage, IRequiresSessionState
{
	private string resourceTypeId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["ResourceTypeId"]))
		{
			this.resourceTypeId = base.Request["ResourceTypeId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.DataBindResourceType();
			this.InitAspNetPager();
		}
	}
	private void DataBindResourceType()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IOrderedQueryable<Res_ResourceType> orderedQueryable =
				from rt in pm2Entities.Res_ResourceType
				where rt.Res_ResourceType2 == null
				orderby rt.ResourceTypeCode
				select rt;
			if (orderedQueryable.Count<Res_ResourceType>() > 0)
			{
				foreach (Res_ResourceType current in orderedQueryable)
				{
					TreeNode treeNode = new TreeNode(current.ResourceTypeName, current.ResourceTypeId);
					this.trvwResourceType.Nodes.Add(treeNode);
					if (!string.IsNullOrEmpty(this.resourceTypeId) && treeNode.Value == this.resourceTypeId)
					{
						treeNode.Selected = true;
					}
				}
				if (string.IsNullOrEmpty(this.resourceTypeId))
				{
					this.trvwResourceType.Nodes[0].Selected = true;
					this.AddResourceTypeIdToUrl();
				}
			}
		}
	}
	private void DataBindTypeAttribute()
	{
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			string resourceTypeId = this.trvwResourceType.SelectedValue;
			IQueryable<Res_Attribute> dataSource = (
				from ta in pm2Entities.Res_Attribute
				where ta.Res_ResourceType.ResourceTypeId == resourceTypeId
				orderby ta.AttributeId
				select ta).Skip(NBasePage.pagesize * (currentPageIndex - 1)).Take(NBasePage.pagesize);
			this.gvwTypeAttribute.DataSource = dataSource;
			this.gvwTypeAttribute.DataBind();
		}
	}
	private void AddResourceTypeIdToUrl()
	{
		string selectedValue = this.trvwResourceType.SelectedValue;
		string message = "window.location.href = jw.modifyParam({ name: 'ResourceTypeId', value: '" + selectedValue + "' }); ";
		base.RegisterScript(message);
	}
	private void InitAspNetPager()
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			this.AspNetPager1.RecordCount = (
				from a in pm2Entities.Res_Attribute
				where a.Res_ResourceType.ResourceTypeId == this.trvwResourceType.SelectedValue
				select a).Count<Res_Attribute>();
		}
	}
	protected void trvwResourceType_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.AddResourceTypeIdToUrl();
		this.DataBindTypeAttribute();
		this.InitAspNetPager();
	}
	protected void btnDelete_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldAttributeId.Value.Contains("["))
		{
			list = JsonHelper.GetListFromJson(this.hfldAttributeId.Value);
		}
		else
		{
			list.Add(this.hfldAttributeId.Value);
		}
		if (!this.DeleteValidate(list))
		{
			base.RegisterScript("alert('系统提示：\\n\\n系统属性不能删除！');");
			return;
		}
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			try
			{
				foreach (string id in list)
				{
					Res_Attribute res_Attribute = (
						from a in pm2Entities.Res_Attribute
						where a.AttributeId == id
						select a).FirstOrDefault<Res_Attribute>();
					if (res_Attribute != null)
					{
						pm2Entities.DeleteObject(res_Attribute);
					}
				}
				pm2Entities.SaveChanges();
			}
			catch
			{
				base.RegisterScript("alert('系统提示：\\n\\n该属性正在使用不能删除！');");
			}
		}
		this.DataBindTypeAttribute();
		this.InitAspNetPager();
	}
	protected void gvwTypeAttribute_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwTypeAttribute.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.DataBindTypeAttribute();
	}
	private bool DeleteValidate(System.Collections.Generic.IList<string> attributeIds)
	{
		System.Collections.Generic.IList<string> notDelResourceType = this.GetNotDelResourceType();
		foreach (string current in notDelResourceType)
		{
			if (attributeIds.Contains(current))
			{
				return false;
			}
		}
		return true;
	}
	private System.Collections.Generic.IList<string> GetNotDelResourceType()
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		string text = ConfigHelper.Get("MajorStuff");
		string text2 = ConfigHelper.Get("MinorStuff");
		if (!string.IsNullOrEmpty(text))
		{
			list.Add(text);
		}
		if (!string.IsNullOrEmpty(text2))
		{
			list.Add(text2);
		}
		return list;
	}
}


