using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;

public class EquipmentInfo
{
    public string Id
    {
        get;
        set;
    }
    public string Code
    {
        get;
        set;
    }
    public string Name
    {
        get;
        set;
    }
    public string Specification
    {
        get;
        set;
    }
    public string TypeName
    {
        get;
        set;
    }
    public string Accuracy
    {
        get;
        set;
    }
    public int? SupplierId
    {
        get;
        set;
    }
    public string SupplierName
    {
        get;
        set;
    }
    public int State
    {
        get;
        set;
    }
    public string StateName
    {
        get;
        set;
    }
    public decimal PurchasePrice
    {
        get;
        set;
    }
    public string Note
    {
        get;
        set;
    }
    public string PurchaseDate
    {
        get;
        set;
    }
}

public partial class Equ_Equipment_SelectEquipment : NBasePage, IRequiresSessionState
{
	private EquEquipmentTypeService equTypeSer = new EquEquipmentTypeService();
	private EquEquipmentService equipmentSer = new EquEquipmentService();
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindState();
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindTrvw();
		this.BindGv();
	}
	public void BindGv()
	{
		int arg_0B_0 = this.AspNetPager1.CurrentPageIndex;
		System.Collections.Generic.List<EquipmentInfo> equipments = this.GetEquipments();
		this.AspNetPager1.RecordCount = equipments.Count;
		this.gvEquipment.DataSource = equipments.Skip((this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize).Take(this.AspNetPager1.PageSize);
		this.gvEquipment.DataBind();
	}
	public void BindTrvw()
	{
		TreeNode treeNode = new TreeNode("目录", "");
		if (base.Request["id"] == "")
		{
			treeNode.Select();
		}
		System.Collections.Generic.List<EquEquipmentType> list = (
			from t in this.equTypeSer
			where t.ParentId == null
			orderby t.Code
			select t).ToList<EquEquipmentType>();
		foreach (EquEquipmentType current in list)
		{
			TreeNode treeNode2 = new TreeNode();
			treeNode2.Value = current.Id.ToString();
			treeNode2.Text = current.Name.ToString();
			if (base.Request["id"] == treeNode2.Value)
			{
				treeNode2.Select();
			}
			treeNode.ChildNodes.Add(treeNode2);
			this.BindChildNode(treeNode2);
		}
		this.trvwEquipmentType.Nodes.Add(treeNode);
		if (base.Request["id"] == null && this.trvwEquipmentType.Nodes.Count > 0)
		{
			this.trvwEquipmentType.Nodes[0].Select();
		}
	}
	private void BindChildNode(TreeNode parentNode)
	{
		System.Collections.Generic.List<EquEquipmentType> list = (
			from t in this.equTypeSer
			where t.ParentId == parentNode.Value
			orderby t.Code
			select t).ToList<EquEquipmentType>();
		if (list.Count<EquEquipmentType>() > 0)
		{
			foreach (EquEquipmentType current in list)
			{
				TreeNode treeNode = new TreeNode();
				treeNode.Text = current.Name;
				treeNode.Value = current.Id;
				if (base.Request["id"] == treeNode.Value)
				{
					treeNode.Select();
				}
				parentNode.ChildNodes.Add(treeNode);
				this.BindChildNode(treeNode);
			}
		}
	}
	protected void trvwEquipmentType_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGv();
	}
	protected void gvEquipment_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["code"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Values["Code"].ToString();
			e.Row.Attributes["name"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Values["Name"].ToString();
			e.Row.Attributes["specification"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Values["Specification"].ToString();
			e.Row.Attributes["purchasePrice"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Values["PurchasePrice"].ToString();
			e.Row.Attributes["purchaseDate"] = this.gvEquipment.DataKeys[e.Row.RowIndex].Values["PurchaseDate"].ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGv();
	}
	protected string GetCorpName(object corpId)
	{
		string result = string.Empty;
		if (corpId != null)
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId = xPMBasicContactCorpService.GetById(System.Convert.ToInt32(corpId));
			if (byId != null)
			{
				result = byId.CorpName;
			}
		}
		return result;
	}
	private System.Collections.Generic.List<EquipmentInfo> GetEquipments()
	{
		new ResResourceService();
		BasicCodeListService inner = new BasicCodeListService();
		string state = this.ddlState.SelectedValue;
		string code = this.txtCode.Text.Trim();
		string name = this.txtName.Text.Trim();
		System.Collections.Generic.List<EquipmentInfo> list = (
			from es in this.equipmentSer
			join ets in this.equTypeSer on es.TypeId equals ets.Id
			join bs in inner on es.State equals bs.ItemCode
			where bs.TypeCode == "EquState" && es.TypeId == this.trvwEquipmentType.SelectedValue
			orderby es.EquCode
			select new EquipmentInfo
			{
				Id = es.Id,
				Code = es.EquCode,
				Name = es.EquName,
				TypeName = ets.Name,
				State = es.State,
				StateName = bs.ItemName,
				SupplierId = es.SupplierId,
				PurchasePrice = es.PurchasePrice,
				Note = es.Note,
				PurchaseDate = (es.PurchaseDate == (System.DateTime?)null) ? "" : es.PurchaseDate.Value.ToString("yyyy-MM-dd")
			}).ToList<EquipmentInfo>();
		if (!string.IsNullOrEmpty(state))
		{
			list = (
				from res in list
				where res.State == System.Convert.ToInt32(state)
				select res).ToList<EquipmentInfo>();
		}
		if (!string.IsNullOrEmpty(code))
		{
			list = (
				from res in list
				where res.Code.Contains(code)
				select res).ToList<EquipmentInfo>();
		}
		if (!string.IsNullOrEmpty(name))
		{
			list = (
				from res in list
				where res.Name.Contains(name)
				select res).ToList<EquipmentInfo>();
		}
		return list;
	}
	private void BindState()
	{
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		System.Collections.Generic.List<BasicCodeList> dataSource = (
			from cs in basicCodeListService.GetByType("EquState")
			orderby cs.ItemCode
			select cs).ToList<BasicCodeList>();
		this.ddlState.DataSource = dataSource;
		this.ddlState.DataTextField = "ItemName";
		this.ddlState.DataValueField = "ItemCode";
		this.ddlState.DataBind();
		this.ddlState.Items.Insert(0, new ListItem("", ""));
	}
}


