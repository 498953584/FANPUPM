using ASP;
using com.jwsoft.common.baseclass;
using com.jwsoft.pm.entpm.action;
using com.jwsoft.pm.entpm.model;
using com.jwsoft.web.WebControls;
using Microsoft.Web.UI.WebControls;
using System;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
	public partial class MaintainView : PageBase, IRequiresSessionState
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["mc"] == null)
				{
					this.js.Text = "alert('参数错误！');window.close();";
				}
				else
				{
					this.ViewState["MAINTAINUNIQUECODE"] = new Guid(base.Request["mc"]);
				}
				this.RestoreMaintainInfo((Guid)this.ViewState["MAINTAINUNIQUECODE"]);
			}
		}
		private void RestoreMaintainInfo(Guid maintainUniqueCode)
		{
			EquipmentMaintainInfo equipmentMaintainInfo = new EquipmentMaintainInfo();
			equipmentMaintainInfo = EquipmentMaintainAction.GetSingleEquipemntMaintainInfo(maintainUniqueCode);
			this.ShowRepairInfo(equipmentMaintainInfo);
			this.ShowEquipmentInfo(equipmentMaintainInfo.EquipmentUniqueCode);
			this.dgdFittingsDetail_Bind(EquipmentMaintainAction.GetMaintainFittingsList(maintainUniqueCode));
		}
		private void ShowRepairInfo(EquipmentMaintainInfo EptMtinfo)
		{
			this.txtMaintainType.Text = EptMtinfo.MaintainType;
			this.txtMaintainCost.Text = EptMtinfo.MaintainCost.ToString();
			this.txtMaintainDate.Text = ((EptMtinfo.MaintainDate == DateTime.MinValue) ? DateTime.Today.ToShortDateString() : EptMtinfo.MaintainDate.ToShortDateString());
			this.txtFault.Text = EptMtinfo.Fault;
			this.txtMaintainContent.Text = EptMtinfo.MaintainContent;
			this.txtMaintainceMan.Text = EptMtinfo.MaintainceMan;
			this.txtAppraisal.Text = EptMtinfo.Appraisal;
			this.txtAppraiser.Text = EptMtinfo.Appraiser;
			this.txtAppraisalDate.Text = ((EptMtinfo.AppraisalDate == DateTime.MinValue) ? DateTime.Today.ToShortDateString() : EptMtinfo.AppraisalDate.ToShortDateString());
		}
		private void ShowEquipmentInfo(Guid equipmentUniqueCode)
		{
			EquipmentInfo singleEquipmentInfo = EquipmentAction.GetSingleEquipmentInfo(equipmentUniqueCode);
			this.lblEquipmentName.Text = singleEquipmentInfo.EquipmentName.ToString();
			this.lblEquipmentSpec.Text = singleEquipmentInfo.Spec.ToString();
			this.lblEquipmentCode.Text = singleEquipmentInfo.EquipmentManCode.ToString();
		}
		private void dgdFittingsDetail_Bind(EquipmentMaintainFittingCollection al)
		{
			this.dgdFittings.DataSource = al;
			this.dgdFittings.DataBind();
			this.Session["RepairRecordList"] = al;
			this.lblState.Text = "共" + al.Count.ToString() + "条记录！";
		}
		private void dgdFittings_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			switch (e.Item.ItemType)
			{
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
				e.Item.Attributes["onmouseover"] = "doMouseOver(this);";
				e.Item.Attributes["onmouseout"] = "doMouseOut(this);";
				return;
			default:
				return;
			}
		}
		protected override void OnInit(EventArgs e)
		{
			this.InitializeComponent();
			this.Hander_Bind();
			base.OnInit(e);
		}
		private void InitializeComponent()
		{
		}
		private void Hander_Bind()
		{
			this.dgdFittings.ItemDataBound += new DataGridItemEventHandler(this.dgdFittings_ItemDataBound);
		}
	}


