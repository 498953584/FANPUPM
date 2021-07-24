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
public partial class Equ_Type_EquipmentTypeList : NBasePage, IRequiresSessionState
{
	private EquEquipmentTypeService equTypeSer = new EquEquipmentTypeService();
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request["id"]))
		{
			this.id = base.Request["id"].ToString();
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.InitPage();
		}
	}
	public void InitPage()
	{
		this.BindTrvw();
		if (!string.IsNullOrEmpty(this.trvwEquipmentType.SelectedValue))
		{
			this.hfldParentId.Value = this.trvwEquipmentType.SelectedValue;
		}
		else
		{
			if (!string.IsNullOrEmpty(this.id))
			{
				this.hfldParentId.Value = this.id;
			}
			else
			{
				this.hfldParentId.Value = this.trvwEquipmentType.SelectedValue;
			}
		}
		this.BindGvw();
	}
	public void BindGvw()
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		int currentPageIndex = this.AspNetPager1.CurrentPageIndex;
		string parentId = string.IsNullOrEmpty(this.hfldParentId.Value) ? null : this.hfldParentId.Value;
		System.Collections.Generic.List<EquEquipmentType> source = (
			from t in this.equTypeSer
			where t.ParentId == parentId
			orderby t.No
			select t).ToList<EquEquipmentType>();
		this.AspNetPager1.RecordCount = source.Count<EquEquipmentType>();
		this.gvwEquipmentType.DataSource = source.Skip(NBasePage.pagesize * (currentPageIndex - 1)).Take(NBasePage.pagesize);
		this.gvwEquipmentType.DataBind();
	}
	public void BindTrvw()
	{
		this.trvwEquipmentType.Nodes.Clear();
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
		if (list.Count > 0)
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
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		EquEquipmentService source = new EquEquipmentService();
		foreach (GridViewRow gridViewRow in this.gvwEquipmentType.Rows)
		{
			CheckBox checkBox = gridViewRow.FindControl("chkBox") as CheckBox;
			if (checkBox.Checked)
			{
				list.Add(checkBox.ToolTip);
			}
		}
		try
		{
			foreach (string equTypeId in list)
			{
				System.Collections.Generic.List<EquEquipmentType> list2 = (
					from t in this.equTypeSer
					where t.ParentId == equTypeId
					select t).ToList<EquEquipmentType>();
				if (list2.Count > 0)
				{
					base.RegisterScript("top.ui.show('请先删除子项！')");
					return;
				}
				System.Collections.Generic.List<EquEquipment> list3 = (
					from es in source
					where es.TypeId == equTypeId
					select es).ToList<EquEquipment>();
				if (list3.Count > 0)
				{
					base.RegisterScript("top.ui.show('请先删除对应的设备！')");
					return;
				}
				EquEquipmentType byId = this.equTypeSer.GetById(equTypeId);
				this.equTypeSer.Delete(byId);
			}
			base.RegisterScript("window.location.href='../../Equ/Type/EquipmentTypeList.aspx?id=" + this.trvwEquipmentType.SelectedValue + "'");
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！')");
		}
	}
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
		this.hfldParentId.Value = this.trvwEquipmentType.SelectedValue;
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGvw();
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void gvwEquipmentType_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwEquipmentType.DataKeys[e.Row.RowIndex].Value.ToString();
			string value = string.Empty;
			if (this.gvwEquipmentType.DataKeys[e.Row.RowIndex].Values["Flag"] != null)
			{
				value = this.gvwEquipmentType.DataKeys[e.Row.RowIndex].Values["Flag"].ToString();
			}
			e.Row.Attributes["flag"] = value;
		}
	}
}


