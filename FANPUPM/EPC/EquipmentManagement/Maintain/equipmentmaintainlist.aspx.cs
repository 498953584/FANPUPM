using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class EquipmentRecord : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["ec"] == null)
				{
					this.JS.Text = "alert('参数错误！');window.close();";
				}
				else
				{
					this.ViewState["EQUIPMENTUNIQUECODE"] = new Guid(base.Request["ec"]);
				}
				this.dgdRepairList_Bind((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]);
				this.hdnEquipmentCode.Value = ((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]).ToString();
				this.BtnAdd.Attributes["onclick"] = "if (!openMaintainEdit('0')){return false;}";
				this.BtnDelete.Attributes["onclick"] = "javascript:if(!confirm('确定要删除当前选中数据吗？')){return false;}";
				this.BtnUpdate.Attributes["onclick"] = "if (!openMaintainEdit('1')){return false;}";
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
			this.dgdRepairList.ItemDataBound += new DataGridItemEventHandler(this.DGRepairList_ItemDataBound);
		}
		private void dgdRepairList_Bind(Guid equipmentUniqueCode)
		{
			EquipmentMaintainCollection equipmentMaintainList = EquipmentMaintainAction.GetEquipmentMaintainList(equipmentUniqueCode);
			this.dgdRepairList.DataSource = equipmentMaintainList;
			this.dgdRepairList.DataBind();
			this.lblState.Text = "共" + equipmentMaintainList.Count.ToString() + "条记录！";
		}
		private void DGRepairList_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			{
				EquipmentMaintainInfo equipmentMaintainInfo = (EquipmentMaintainInfo)e.Item.DataItem;
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgdRepairList.ClientID,
					"');clickRow('",
					equipmentMaintainInfo.MaintainUniqueCode.ToString(),
					"');"
				});
				return;
			}
			default:
				return;
			}
		}
		protected void BtnDelete_Click(object sender, EventArgs e)
		{
			if (EquipmentMaintainAction.DelMaintainInfo(new Guid(this.hdnMaintainCode.Value)) == 1)
			{
				this.JS.Text = "alert('数据删除成功！');";
			}
			else
			{
				this.JS.Text = "alert('数据删除失败！');";
			}
			this.dgdRepairList_Bind((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]);
		}
		protected void BtnUpdate_Click(object sender, EventArgs e)
		{
			this.dgdRepairList_Bind((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]);
		}
		protected void BtnAdd_Click(object sender, EventArgs e)
		{
			this.dgdRepairList_Bind((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]);
		}
	}


