using ASP;
using cn.justwin.BLL;
using cn.justwin.DAL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.entpm;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class BudgetManage_Resource_ResourceEdit : NBasePage, IRequiresSessionState
{
	private string resourceId = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (base.Request["id"] != null)
		{
			this.resourceId = base.Request["id"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
		this.FileUpload1.RecordCode = this.hdGuid.Value;
	}
    public void InitPage()
    {
        this.BindDdl();
        if (!string.IsNullOrEmpty(this.resourceId))
        {
            this.lblTitle.Text = "编辑资源";
            using (pm2Entities entities = new pm2Entities())
            {
                this.hdGuid.Value = this.resourceId;
                var resource = (from m in entities.Res_Resource
                                where m.ResourceId == this.resourceId
                                select new { Res_Unit = m.Res_Unit, Brand = m.Brand, ResourceCode = m.ResourceCode, ResourceName = m.ResourceName, Specification = m.Specification, TaxRate = m.TaxRate, TechnicalParameter = m.TechnicalParameter, Res_Attribute = m.Res_Attribute, Series = m.Series, ModelNumber = m.ModelNumber, Note = m.Note, SupplierId = m.SupplierId }).First();
                this.txtBrand.Text = resource.Brand;
                this.txtResourceCode.Text = resource.ResourceCode;
                this.txtResourceName.Text = resource.ResourceName;
                this.txtSpecification.Text = resource.Specification;
                this.txtTaxRate.Text = resource.TaxRate.ToString();
                this.txtTechnicalParameter.Text = resource.TechnicalParameter;
                this.txtSeries.Text = resource.Series;
                this.txtModelNumber.Text = resource.ModelNumber;
                this.txtNote.Text = resource.Note;
                if (resource.SupplierId.HasValue)
                {
                    XPM_Basic_ContactCorp corp = (from c in entities.XPM_Basic_ContactCorp
                                                  where c.CorpID == resource.SupplierId
                                                  select c).FirstOrDefault<XPM_Basic_ContactCorp>();
                    if (corp != null)
                    {
                        this.txtSupplier.Text = corp.CorpName;
                        this.hfldSupplier.Value = corp.CorpID.ToString();
                    }
                }
                if (resource.Res_Attribute != null)
                {
                    this.ddlAttribute.SelectedValue = resource.Res_Attribute.AttributeId;
                }
                if (resource.Res_Unit != null)
                {
                    this.ddlUnit.SelectedValue = resource.Res_Unit.UnitId;
                }
                goto Label_05D9;
            }
        }
        this.lblTitle.Text = "新增资源";
        this.hdGuid.Value = Guid.NewGuid().ToString();
        this.txtResourceCode.Text = this.GenerateResCode();
    Label_05D9:
        this.BindGv();
    }
	public void BindDdl()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			this.ddlUnit.DataSource =
				from r in pm2Entities.Res_Unit
				orderby r.Name
				select r;
			this.ddlUnit.DataBind();
			string nodeId = string.Empty;
			if (!string.IsNullOrEmpty(base.Request["nodeId"]))
			{
				nodeId = base.Request["nodeId"];
			}
			else
			{
				string text = (
					from r in pm2Entities.Res_Resource
					where r.ResourceId == this.resourceId
					select r.Res_ResourceType.ResourceTypeId).FirstOrDefault<string>();
				string text2 = text;
				while (!string.IsNullOrEmpty(text2))
				{
					text2 = this.GetParentId(text2);
					if (!string.IsNullOrEmpty(text2))
					{
						text = text2;
					}
				}
				nodeId = text;
			}
			this.ddlAttribute.DataSource =
				from a in pm2Entities.Res_Attribute
				where a.Res_ResourceType.ResourceTypeId == nodeId
				select a;
			this.ddlAttribute.DataBind();
			this.ddlAttribute.Items.Insert(0, new ListItem("", ""));
		}
	}
	public string GetParentId(string typeId)
	{
		string cmdText = "SELECT ParentId FROM Res_ResourceType WHERE ResourceTypeId='" + typeId + "'";
		object obj = SqlHelper.ExecuteScalar(CommandType.Text, cmdText, new SqlParameter[0]);
		return DBHelper.GetString(obj);
	}
	public void BindGv()
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IOrderedQueryable<Res_PriceType> dataSource =
				from m in pm2Entities.Res_PriceType
				orderby m.PriceTypeCode
				select m;
			this.gvwPriceType.DataSource = dataSource;
			this.gvwPriceType.DataBind();
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		int num = 0;
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_Resource> source =
				from m in pm2Entities.Res_Resource
				where m.ResourceCode == this.txtResourceCode.Text.Trim()
				select m;
			if (!string.IsNullOrEmpty(this.resourceId))
			{
				Res_Resource res_Resource = (
					from m in pm2Entities.Res_Resource
					where m.ResourceId == this.resourceId
					select m).First<Res_Resource>();
				if (res_Resource.ResourceCode != this.txtResourceCode.Text.Trim() && source.Count<Res_Resource>() > 0)
				{
					base.RegisterScript("top.ui.alert('资源编号已存在!!请重新定义!!')");
					return;
				}
				this.UpdateResource(res_Resource, pm2Entities);
				num = pm2Entities.SaveChanges();
			}
			else
			{
				if (source.Count<Res_Resource>() > 0)
				{
					base.RegisterScript("top.ui.alert('资源编号已存在!!请重新定义!!')");
					return;
				}
				Res_Resource res_Resource2 = this.CreateResource(pm2Entities);
				pm2Entities.AddToRes_Resource(res_Resource2);
				num = pm2Entities.SaveChanges();
			}
		}
		this.AddPrice();
		if (num > 0)
		{
			string.Concat(new string[]
			{
				"/BudgetManage/Resource/ResourceDetail.aspx?id=",
				base.Request["parentId"],
				"&nodeId=",
				base.Request["parentId"],
				"&parentId=",
				base.Request["nodeId"]
			});
			base.RegisterScript("top.ui.show('" + this.SetMsg() + "成功！'); top.ui.closeTab({ refresh: '1', parent: 'resource' });");
			return;
		}
		base.RegisterScript("top.ui.alert('" + this.SetMsg() + "失败！');");
	}
	private string SetMsg()
	{
		if (!string.IsNullOrEmpty(this.resourceId))
		{
			return "更新";
		}
		return "添加";
	}
	private Res_Resource CreateResource(pm2Entities pm2)
	{
		Res_Resource res_Resource = new Res_Resource();
		res_Resource.ResourceId = this.hdGuid.Value.Trim();
		this.UpdateResource(res_Resource, pm2);
		res_Resource.InputUser = PageHelper.QueryUser(this, base.UserCode);
		res_Resource.InputDate = new System.DateTime?(System.DateTime.Now);
		string parentId = base.Request["parentId"];
		Res_ResourceType res_ResourceType = (
			from m in pm2.Res_ResourceType
			where m.ResourceTypeId == parentId
			select m).First<Res_ResourceType>();
		res_ResourceType.Res_Resource.Add(res_Resource);
		return res_Resource;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvwPriceType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowIndex > -1)
		{
			e.Row.Attributes["id"] = this.gvwPriceType.DataKeys[e.Row.RowIndex].Value.ToString();
		}
	}
	public void UpdateResource(Res_Resource resource, pm2Entities pm2)
	{
		resource.ResourceCode = this.txtResourceCode.Text.Trim();
		resource.ResourceName = this.txtResourceName.Text.Trim();
		resource.Series = this.txtSeries.Text;
		resource.ModelNumber = this.txtModelNumber.Text;
		if (!string.IsNullOrEmpty(this.hfldSupplier.Value))
		{
			resource.SupplierId = new int?(System.Convert.ToInt32(this.hfldSupplier.Value));
		}
		else
		{
			resource.SupplierId = null;
		}
		resource.Brand = this.txtBrand.Text;
		if (!string.IsNullOrEmpty(this.txtTaxRate.Text))
		{
			resource.TaxRate = new decimal?(System.Convert.ToDecimal(this.txtTaxRate.Text));
		}
		else
		{
			resource.TaxRate = null;
		}
		resource.Specification = this.txtSpecification.Text;
		resource.TechnicalParameter = this.txtTechnicalParameter.Text;
		resource.Note = this.txtNote.Text;
		Res_Unit res_Unit = (
			from m in pm2.Res_Unit
			where m.UnitId == this.ddlUnit.SelectedValue
			select m).First<Res_Unit>();
		res_Unit.Res_Resource.Add(resource);
		if (!string.IsNullOrEmpty(this.ddlAttribute.SelectedValue))
		{
			Res_Attribute res_Attribute = (
				from m in pm2.Res_Attribute
				where m.AttributeId == this.ddlAttribute.SelectedValue
				select m).First<Res_Attribute>();
			res_Attribute.Res_Resource.Add(resource);
			return;
		}
		resource.Res_Attribute = null;
	}
	public string GetPrice(string typeId)
	{
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_Price> source =
				from m in pm2Entities.Res_Price
				where m.PriceTypeId == typeId && m.ResourceId == this.resourceId
				select m;
			if (source.Count<Res_Price>() > 0)
			{
				Res_Price res_Price = source.First<Res_Price>();
				string result;
				if (res_Price != null)
				{
					result = res_Price.PriceValue.ToString();
					return result;
				}
				result = "";
				return result;
			}
		}
		return "";
	}
	public void AddPrice()
	{
		string id = this.hdGuid.Value;
		if (!string.IsNullOrEmpty(this.resourceId))
		{
			id = this.resourceId;
		}
		using (pm2Entities pm2Entities = new pm2Entities())
		{
			IQueryable<Res_Price> queryable =
				from m in pm2Entities.Res_Price
				where m.ResourceId == id
				select m;
			foreach (Res_Price current in queryable)
			{
				pm2Entities.DeleteObject(current);
			}
			pm2Entities.SaveChanges();
		}
		System.Collections.IEnumerator enumerator2 = this.gvwPriceType.Rows.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				GridViewRow gridViewRow = (GridViewRow)enumerator2.Current;
				TextBox tb = gridViewRow.FindControl("txtPrice") as TextBox;
				using (pm2Entities pm2Entities2 = new pm2Entities())
				{
					Res_Price res_Price = new Res_Price();
					if (!string.IsNullOrEmpty(tb.Text))
					{
						res_Price.PriceValue = System.Convert.ToDecimal(tb.Text);
					}
					Res_PriceType res_PriceType = (
						from m in pm2Entities2.Res_PriceType
						where m.PriceTypeId == tb.ToolTip
						select m).First<Res_PriceType>();
					res_PriceType.Res_Price.Add(res_Price);
					Res_Resource res_Resource = (
						from m in pm2Entities2.Res_Resource
						where m.ResourceId == id
						select m).First<Res_Resource>();
					res_Resource.Res_Price.Add(res_Price);
					pm2Entities2.AddToRes_Price(res_Price);
					pm2Entities2.SaveChanges();
				}
			}
		}
		finally
		{
			System.IDisposable disposable = enumerator2 as System.IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
		}
	}
	private string GenerateResCode()
	{
		string text = string.Empty;
		string typeId = base.Request["parentId"];
		ResResourceTypeService resResourceTypeService = new ResResourceTypeService();
		ResResourceService resResourceService = new ResResourceService();
		ResResourceType byId = resResourceTypeService.GetById(typeId);
		text = byId.ResourceTypeCode;
		int num = (
			from r in resResourceService
			where r.ResourceType == typeId
			select r).Count<ResResource>();
		text += (num + 1).ToString().PadLeft(5, '0');
		if (resResourceService.IsExists(text))
		{
			text += "01";
		}
		return text;
	}
}


