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
public partial class Equ_Equipment_MultiSelectEquType : NBasePage, IRequiresSessionState
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
		this.BindGvw();
	}
	public void BindGvw()
	{
		int arg_11_0 = this.AspNetPager1.CurrentPageIndex;
		string parentId = string.IsNullOrEmpty(this.trvwEquipmentType.SelectedValue) ? null : this.trvwEquipmentType.SelectedValue;
		System.Collections.Generic.List<EquEquipmentType> dataSource = (
			from t in this.equTypeSer
			where t.ParentId == parentId
			orderby t.No
			select t).ToList<EquEquipmentType>();
		this.gvwEquipmentType.DataSource = dataSource;
		this.gvwEquipmentType.DataBind();
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
	protected void TreeView_SelectedNodeChanged(object sender, System.EventArgs e)
	{
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
		}
	}
}


