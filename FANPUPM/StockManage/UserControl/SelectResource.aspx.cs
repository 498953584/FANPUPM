using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Serialize;
using System;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class StockManage_UserControl_SelectResource : NBasePage, IRequiresSessionState
{
	private ISerializable ser = new JsonSerializer();
	private string type = string.Empty;
	private new int pagesize3 = 15;

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request.QueryString["type"] != null)
		{
			this.type = base.Request.QueryString["type"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		System.DateTime arg_05_0 = System.DateTime.Now;
		if (!base.IsPostBack)
		{
			this.InitDropResourceType();
			this.InitTrvwResourceType();
			this.InitAspNetPager();
			this.DataBindResource();
			this.hfldshow.Value = "1";
		}
		this.hfldConsType.Value = this.type;
		this.gvwSelectedResource.DataSource = null;
		this.gvwSelectedResource.DataBind();
		System.DateTime arg_63_0 = System.DateTime.Now;
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
		this.hfldshow.Value = "0";
	}
	protected void gvwResource_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwResource.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["code"] = this.gvwResource.DataKeys[e.Row.RowIndex].Values[1].ToString();
		}
	}
	protected void trvwResourceType_TreeNodePopulate(object sender, TreeNodeEventArgs e)
	{
		DataTable resType = this.GetResType();
		DataRow[] array = resType.Select(" ParentId='" + e.Node.Value + "'");
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			TreeNode treeNode = new TreeNode();
			treeNode.Value = dataRow["ResourceTypeId"].ToString();
			treeNode.Text = dataRow["ResourceTypeName"].ToString();
			if (resType.Select(" ParentId='" + treeNode.Value + "'").Count<DataRow>() > 0)
			{
				treeNode.PopulateOnDemand = true;
			}
			e.Node.ChildNodes.Add(treeNode);
		}
	}
	private void InitDropResourceType()
	{
		System.DateTime arg_05_0 = System.DateTime.Now;
		string text = base.Request["TypeCode"];
		if (!string.IsNullOrEmpty(text))
		{
			using (new pm2Entities())
			{
				DataTable resType = this.GetResType();
				DataTable dataTable = new DataTable();
				dataTable.Columns.Add(new DataColumn("ResourceTypeCode"));
				dataTable.Columns.Add(new DataColumn("ResourceTypeName"));
				if (text == "0")
				{
					DataRow[] array = resType.Select(" ParentId is null ");
					DataRow[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						DataRow dataRow = array2[i];
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["ResourceTypeCode"] = dataRow["ResourceTypeCode"];
						dataRow2["ResourceTypeName"] = dataRow["ResourceTypeName"];
						dataTable.Rows.Add(dataRow2);
					}
				}
				else
				{
					string[] array3 = text.Split(new char[]
					{
						','
					});
					string[] array4 = array3;
					for (int j = 0; j < array4.Length; j++)
					{
						string str = array4[j];
						DataRow[] array5 = resType.Select(" ResourceTypeCode='" + str + "'");
						DataRow dataRow3 = dataTable.NewRow();
						dataRow3["ResourceTypeCode"] = array5[0]["ResourceTypeCode"];
						dataRow3["ResourceTypeName"] = array5[0]["ResourceTypeName"];
						dataTable.Rows.Add(dataRow3);
					}
				}
				this.dropResourceType.DataSource = dataTable;
				this.dropResourceType.DataBind();
			}
		}
		System.DateTime arg_1A1_0 = System.DateTime.Now;
	}
	private void InitTrvwResourceType()
	{
		System.DateTime arg_05_0 = System.DateTime.Now;
		if (!string.IsNullOrEmpty(base.Request["TypeCode"]))
		{
			this.trvwResourceType.Nodes.Clear();
			string selectedValue = this.dropResourceType.SelectedValue;
			DataTable resType = this.GetResType();
			DataRow[] array = resType.Select(string.Format(" ResourceTypeCode = '{0}'", selectedValue));
			string text = array[0]["ResourceTypeName"].ToString();
			string text2 = array[0]["ResourceTypeId"].ToString();
			TreeNode treeNode = new TreeNode(text, text2);
			this.trvwResourceType.Nodes.Add(treeNode);
			DataRow[] array2 = resType.Select(" ParentId='" + text2 + "'");
			DataRow[] array3 = array2;
			for (int i = 0; i < array3.Length; i++)
			{
				DataRow dataRow = array3[i];
				TreeNode treeNode2 = new TreeNode();
				treeNode2.Value = dataRow["ResourceTypeId"].ToString();
				treeNode2.Text = dataRow["ResourceTypeName"].ToString();
				if (resType.Select(" ParentId='" + treeNode2.Value + "'").Count<DataRow>() > 0)
				{
					treeNode2.PopulateOnDemand = true;
				}
				treeNode.ChildNodes.Add(treeNode2);
			}
			this.trvwResourceType.DataBind();
		}
		System.DateTime arg_153_0 = System.DateTime.Now;
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
				cn.justwin.BLL.Resource resource = new cn.justwin.BLL.Resource();
				DataTable resource2 = resource.GetResource(typeId, this.pagesize3, this.AspNetPager1.CurrentPageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
				this.AspNetPager1.RecordCount = resource.GetResourceCount(typeId, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
				this.gvwResource.DataSource = resource2;
				this.gvwResource.DataBind();
				return;
			}
		}
		cn.justwin.BLL.Resource resource3 = new cn.justwin.BLL.Resource();
		DataTable resourceByRerourceType = resource3.GetResourceByRerourceType(this.trvwResourceType.SelectedValue, this.pagesize3, this.AspNetPager1.CurrentPageIndex, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		this.AspNetPager1.RecordCount = resource3.GetResourceCoutByResourceType(this.trvwResourceType.SelectedValue, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		this.gvwResource.DataSource = resourceByRerourceType;
		this.gvwResource.DataBind();
	}
	private void DataBindResource()
	{
		System.DateTime arg_05_0 = System.DateTime.Now;
		this.txtCode.Text.Trim();
		this.txtName.Text.Trim();
		this.InitResource();
		System.DateTime arg_33_0 = System.DateTime.Now;
	}
	private void InitAspNetPager()
	{
		System.DateTime arg_05_0 = System.DateTime.Now;
		this.txtCode.Text = string.Empty;
		this.txtName.Text = string.Empty;
		this.AspNetPager1.PageSize = this.pagesize3;
		this.AspNetPager1.CurrentPageIndex = 1;
		if (string.IsNullOrEmpty(this.trvwResourceType.SelectedValue))
		{
			using (pm2Entities pm2Entities = new pm2Entities())
			{
				string typeId = (
					from t in pm2Entities.Res_ResourceType
					where t.ResourceTypeCode == this.dropResourceType.SelectedValue
					select t.ResourceTypeId).FirstOrDefault<string>();
				cn.justwin.BLL.Resource resource = new cn.justwin.BLL.Resource();
				this.AspNetPager1.RecordCount = resource.GetResourceCount(typeId, string.Empty, string.Empty);
				goto IL_1B3;
			}
		}
		cn.justwin.BLL.Resource resource2 = new cn.justwin.BLL.Resource();
		this.AspNetPager1.RecordCount = resource2.GetResourceCoutByResourceType(this.trvwResourceType.SelectedValue, this.txtCode.Text.Trim(), this.txtName.Text.Trim());
		IL_1B3:
		System.DateTime arg_1B8_0 = System.DateTime.Now;
	}
	private DataTable GetResType()
	{
		if (base.Cache[CacheParameter.ResourceType] == null)
		{
			base.Cache[CacheParameter.ResourceType] = cn.justwin.BLL.Resource.GetResType();
		}
		DataTable dataTable = base.Cache[CacheParameter.ResourceType] as DataTable;
		if (dataTable == null)
		{
			dataTable = cn.justwin.BLL.Resource.GetResType();
		}
		return dataTable;
	}
}


