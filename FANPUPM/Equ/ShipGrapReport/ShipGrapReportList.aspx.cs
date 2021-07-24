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
public partial class Equ_ShipProductionReport_ShipProductionReportList : NBasePage, IRequiresSessionState
{
	private EquEquipmentTypeService equType = new EquEquipmentTypeService();
	private EquEquipmentService equService = new EquEquipmentService();
	private string typeId = string.Empty;
	private bool isShip = true;
	private static string[] treeParents = new string[]
	{
		"船机设备",
		"陆上设备"
	};

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request.QueryString["typeId"]))
		{
			this.typeId = base.Request.QueryString["typeId"];
		}
		if (string.IsNullOrEmpty(base.Request.QueryString["isShip"]) && "0".Equals(base.Request.QueryString["isShip"]))
		{
			this.isShip = false;
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			if (string.IsNullOrEmpty(this.typeId))
			{
				return;
			}
			this.BindTreeView();
			this.BindGvw();
		}
	}
	private void BindTreeView()
	{
		this.getTypeNode(new TreeNode
		{
			Text = this.isShip ? Equ_ShipProductionReport_ShipProductionReportList.treeParents[0] : Equ_ShipProductionReport_ShipProductionReportList.treeParents[1],
			Value = string.Empty,
			Expanded = new bool?(true)
		});
	}
	private void getTypeNode(TreeNode root)
	{
		System.Collections.Generic.IList<EquEquipmentType> targetType = this.getTargetType(true, string.Empty);
		if (targetType != null && targetType.Count > 0)
		{
			foreach (EquEquipmentType current in targetType)
			{
				TreeNode treeNode = this.getTreeNode(current);
				this.getEquTypeNodes(current.Id, treeNode);
				root.ChildNodes.Add(treeNode);
			}
		}
	}
	private void getEquTypeNodes(string typeId, TreeNode parentNode)
	{
		System.Collections.Generic.IList<EquEquipmentType> targetType = this.getTargetType(false, typeId);
		if (targetType != null && targetType.Count > 0)
		{
			using (System.Collections.Generic.IEnumerator<EquEquipmentType> enumerator = targetType.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EquEquipmentType current = enumerator.Current;
					TreeNode treeNode = this.getTreeNode(current);
					this.getEquTypeNodes(current.Id, treeNode);
					parentNode.ChildNodes.Add(treeNode);
				}
				return;
			}
		}
		this.addEquipmentNode(typeId, parentNode);
	}
	private TreeNode getTreeNode(EquEquipmentType typeInstance)
	{
		return new TreeNode(typeInstance.Name, typeInstance.Id)
		{
			Expanded = new bool?(true)
		};
	}
	private void addEquipmentNode(string typeId, TreeNode typeNode)
	{
		System.Collections.Generic.IList<EquEquipment> list = (
			from equ in this.equService
			where typeId.Equals(equ.TypeId)
			select equ).ToList<EquEquipment>();
		if (list != null && list.Count > 0)
		{
			foreach (EquEquipment current in list)
			{
				TreeNode treeNode = new TreeNode(current.EquName, current.Id);
				treeNode.Expanded = new bool?(true);
				this.addYearNode(treeNode, current.Id);
				typeNode.ChildNodes.Add(treeNode);
			}
		}
	}
	private void addYearNode(TreeNode equNode, string equId)
	{
		int year = System.DateTime.Now.Year;
		for (int i = -1; i < 2; i++)
		{
			TreeNode treeNode = new TreeNode(string.Format("{0}年", year + i), (year + i).ToString());
			treeNode.Expanded = new bool?(false);
			this.addMonthNode(treeNode, year + i, equId);
			equNode.ChildNodes.Add(treeNode);
		}
	}
	private void addMonthNode(TreeNode yearNode, int year, string equId)
	{
		for (int i = 1; i <= 12; i++)
		{
			TreeNode treeNode = new TreeNode(string.Format("{0}月", i), string.Format("{0}-{1:d2},{2}", year, i, equId));
			treeNode.Expanded = new bool?(false);
			yearNode.ChildNodes.Add(treeNode);
		}
	}
	private System.Collections.Generic.IList<EquEquipmentType> getTargetType(bool isParent, string parentId)
	{
		System.Collections.Generic.IList<EquEquipmentType> result;
		if (isParent)
		{
			result = (
				from type in this.equType
				where this.typeId.Equals(type.Id)
				select type).ToList<EquEquipmentType>();
		}
		else
		{
			result = (
				from type in this.equType
				where parentId.Equals(type.ParentId)
				select type).ToList<EquEquipmentType>();
		}
		return result;
	}
	private void BindGvw()
	{
		int arg_0B_0 = this.AspNetPager1.CurrentPageIndex;
	}
	protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
	{
		this.BindGvw();
	}
	protected void gvwProductionReport_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		if (e.Row.RowType == DataControlRowType.DataRow)
		{
			e.Row.Attributes["id"] = this.gvwProductionReport.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["guid"] = this.gvwProductionReport.DataKeys[e.Row.RowIndex].Value.ToString();
			e.Row.Attributes["prjId"] = this.gvwProductionReport.DataKeys[e.Row.RowIndex].Values["PrjId"].ToString();
		}
	}
	protected void btnSearch_Click(object sender, System.EventArgs e)
	{
		this.AspNetPager1.CurrentPageIndex = 1;
		this.BindGvw();
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldId.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldId.Value);
		}
		else
		{
			list.Add(this.hfldId.Value);
		}
		try
		{
			foreach (string arg_4E_0 in list)
			{
			}
			base.RegisterScript("top.ui.show('删除成功！');");
			this.BindGvw();
		}
		catch
		{
			base.RegisterScript("top.ui.show('删除失败！');");
		}
	}
	protected void trvwEquipmentType_SelectedNodeChanged(object sender, System.EventArgs e)
	{
	}
}


