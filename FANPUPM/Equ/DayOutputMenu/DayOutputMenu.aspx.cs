using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Wuqi.Webdiyer;
public partial class Equ_DayOutputMenu_DayOutputMenu : NBasePage, IRequiresSessionState
{
	private EquEquipmentTypeService equType = new EquEquipmentTypeService();
	private EquEquipmentService equService = new EquEquipmentService();
	private EquShipDayReportService dayReport = new EquShipDayReportService();
	private string typeId = string.Empty;
	private bool isShip = true;
	private EquStyleEnum equEnum;
	private static string[] treeParents = new string[]
	{
		"船机设备",
		"陆上设备"
	};

	protected override void OnInit(System.EventArgs e)
	{
		this.AspNetPager1.PageSize = NBasePage.pagesize;
		if (!string.IsNullOrEmpty(base.Request.QueryString["isShip"]) && "0".Equals(base.Request.QueryString["isShip"]))
		{
			this.isShip = false;
		}
		if (!string.IsNullOrEmpty(base.Request.QueryString["EquEnum"]))
		{
			this.equEnum = (EquStyleEnum)int.Parse(base.Request.QueryString["EquEnum"]);
			this.getTypeId();
		}
		base.OnInit(e);
	}
	private void getTypeId()
	{
		string typeName = string.Empty;
		switch (this.equEnum)
		{
		case EquStyleEnum.EquGrap:
			typeName = "抓扬式挖泥船";
			break;
		case EquStyleEnum.EquCutter:
			typeName = "绞吸式挖泥船";
			break;
		case EquStyleEnum.EquTrailing:
			typeName = "耙吸式挖泥船";
			break;
		case EquStyleEnum.EquBarge:
			typeName = "自航泥驳";
			break;
		case EquStyleEnum.EquLoader:
			typeName = "装载机";
			break;
		case EquStyleEnum.EquExcvavtor:
			typeName = "挖掘机";
			break;
		case EquStyleEnum.EquAirAndDrill:
			typeName = "空压机-钻孔机";
			break;
		case EquStyleEnum.EquMix:
			typeName = "搅拌运输车";
			break;
		case EquStyleEnum.EquDump:
			typeName = "自卸车";
			break;
		}
		EquEquipmentType equEquipmentType = (
			from equTypeInfo in this.equType
			where equTypeInfo.Name.Equals(typeName)
			select equTypeInfo).FirstOrDefault<EquEquipmentType>();
		if (equEquipmentType != null)
		{
			this.typeId = equEquipmentType.Id;
		}
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
		HiddenField arg_36_0 = this.hfldEquEnum;
		int num = (int)this.equEnum;
		arg_36_0.Value = num.ToString();
	}
	private void BindTreeView()
	{
		TreeNode treeNode = new TreeNode();
		treeNode.Text = (this.isShip ? Equ_DayOutputMenu_DayOutputMenu.treeParents[0] : Equ_DayOutputMenu_DayOutputMenu.treeParents[1]);
		treeNode.Value = "根节点";
		treeNode.Expanded = new bool?(true);
		this.getTypeNode(treeNode);
		this.trvwEquipmentType.Nodes.Add(treeNode);
		if (string.IsNullOrEmpty(this.hfldNodeValuePath.Value))
		{
			this.hfldNodeValuePath.Value = this.trvwEquipmentType.SelectedNode.ValuePath;
			return;
		}
		this.trvwEquipmentType.FindNode(this.hfldNodeValuePath.Value.Trim()).Select();
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
			foreach (EquEquipmentType current in targetType)
			{
				TreeNode treeNode = this.getTreeNode(current);
				this.getEquTypeNodes(current.Id, treeNode);
				parentNode.ChildNodes.Add(treeNode);
			}
		}
		this.addEquipmentNode(typeId, parentNode);
	}
	private TreeNode getTreeNode(EquEquipmentType typeInstance)
	{
		return new TreeNode(typeInstance.Name, string.Format("{0}#{1}", typeInstance.Id, "设备类型节点"))
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
				TreeNode treeNode = new TreeNode(current.EquName, string.Format("{0}#{1}", current.Id, "设备节点"));
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
			TreeNode treeNode = new TreeNode(string.Format("{0}年", year + i), string.Format("{0}#{1}", year + i, "年度节点"));
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
			if (System.DateTime.Now.Year == year && System.DateTime.Now.Month == i && (string.IsNullOrEmpty(this.hfldEquId.Value.Trim()) || string.IsNullOrEmpty(this.hfldMonth.Value.Trim())))
			{
				this.hfldEquId.Value = equId;
				this.hfldMonth.Value = string.Format("{0}-{1:d2}", year, i);
				yearNode.Expanded = new bool?(true);
				treeNode.Expanded = new bool?(true);
				treeNode.Selected = true;
			}
			else
			{
				treeNode.Expanded = new bool?(false);
			}
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
		int year = System.Convert.ToInt32(this.hfldMonth.Value.Trim().Split(new char[]
		{
			'-'
		})[0]);
		int month = System.Convert.ToInt32(this.hfldMonth.Value.Trim().Split(new char[]
		{
			'-'
		})[1]);
		string equId = this.hfldEquId.Value.Trim();
		int startIndex = (this.AspNetPager1.CurrentPageIndex - 1) * this.AspNetPager1.PageSize;
		switch (this.equEnum)
		{
		case EquStyleEnum.EquGrap:
		{
			EquShipDayReportService equShipDayReportService = new EquShipDayReportService();
			DataTable equDayReportList = equShipDayReportService.GetEquDayReportList(equId, year, month);
			this.gvwProductionReport.DataSource = this.GetCurrentPageData(equDayReportList, startIndex);
			this.gvwProductionReport.DataBind();
			break;
		}
		case EquStyleEnum.EquCutter:
		case EquStyleEnum.EquTrailing:
		case EquStyleEnum.EquBarge:
		case EquStyleEnum.EquLoader:
		case EquStyleEnum.EquExcvavtor:
		case EquStyleEnum.EquAirAndDrill:
		case EquStyleEnum.EquMix:
		case EquStyleEnum.EquDump:
			break;
		default:
			return;
		}
	}
	private DataTable GetCurrentPageData(DataTable allData, int startIndex)
	{
		if (allData == null || allData.Rows.Count <= this.AspNetPager1.PageSize)
		{
			return allData;
		}
		DataTable dataTable = allData.Copy();
		dataTable.Clear();
		DataRow[] array = allData.Select(string.Format("Num >= {0} AND Num <= {1}", startIndex + 1, startIndex + this.AspNetPager1.PageSize));
		DataRow[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			DataRow dataRow = array2[i];
			dataTable.Rows.Add(dataRow.ItemArray);
		}
		return dataTable;
	}
	protected string GetEquDateInfo(string constructionDate)
	{
		if (!string.IsNullOrEmpty(constructionDate))
		{
			return string.Format("{0}号", System.Convert.ToDateTime(constructionDate).Day);
		}
		return string.Empty;
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
		}
	}
	protected void btnDel_Click(object sender, System.EventArgs e)
	{
		System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
		if (this.hfldDayId.Value.Contains('['))
		{
			list = JsonHelper.GetListFromJson(this.hfldDayId.Value);
		}
		else
		{
			list.Add(this.hfldDayId.Value);
		}
		try
		{
			foreach (string current in list)
			{
				EquShipDayReport byId = this.dayReport.GetById(current);
				this.dayReport.Delete(byId);
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
		if (!this.trvwEquipmentType.SelectedValue.Contains(","))
		{
			base.RegisterAlert("当前选中的节点不正", string.Format("您当前选中的为{0}，请选择设备下的月份节点！", this.trvwEquipmentType.SelectedValue.Split(new char[]
			{
				'#'
			})[1]));
			if (!string.IsNullOrEmpty(this.hfldNodeValuePath.Value.Trim()))
			{
				this.trvwEquipmentType.FindNode(this.hfldNodeValuePath.Value.Trim()).Selected = true;
			}
			return;
		}
		string[] array = this.trvwEquipmentType.SelectedValue.Split(new char[]
		{
			','
		});
		this.hfldMonth.Value = array[0];
		this.hfldEquId.Value = array[1];
		this.hfldNodeValuePath.Value = this.trvwEquipmentType.SelectedNode.ValuePath;
		this.BindGvw();
	}
}


