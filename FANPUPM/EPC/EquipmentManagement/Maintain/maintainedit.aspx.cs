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
	public partial class MaintainEdit : PageBase, IRequiresSessionState
	{

		protected void Page_Load(object sender, EventArgs e)
		{
			if (!this.Page.IsPostBack)
			{
				if (base.Request["ec"] == null || base.Request["mc"] == null || base.Request["t"] == null)
				{
                    this.js.Text = "alert('参数错误！');window.close();";
				}
				else
				{
					this.ViewState["OPTYPE"] = base.Request["t"].Trim();
					this.ViewState["EQUIPMENTUNIQUECODE"] = new Guid(base.Request["ec"]);
					if (this.ViewState["OPTYPE"].ToString() == "0")
					{
						this.ViewState["MAINTAINUNIQUECODE"] = Guid.NewGuid();
					}
					else
					{
						this.ViewState["MAINTAINUNIQUECODE"] = new Guid(base.Request["mc"]);
					}
				}
				this.ShowEquipmentInfo((Guid)this.ViewState["EQUIPMENTUNIQUECODE"]);
				this.RestoreMaintainInfo((Guid)this.ViewState["MAINTAINUNIQUECODE"]);
			}
		}
		private void RestoreMaintainInfo(Guid maintainUniqueCode)
		{
			EquipmentMaintainInfo eptMtinfo = new EquipmentMaintainInfo();
			eptMtinfo = EquipmentMaintainAction.GetSingleEquipemntMaintainInfo(maintainUniqueCode);
			this.ShowRepairInfo(eptMtinfo);
			if (this.ViewState["OPTYPE"].ToString() == "0")
			{
				this.dgdFittingsDetail_Bind(new EquipmentMaintainFittingCollection());
				return;
			}
			this.dgdFittingsDetail_Bind(EquipmentMaintainAction.GetMaintainFittingsList(maintainUniqueCode));
		}
		private void ShowRepairInfo(EquipmentMaintainInfo EptMtinfo)
		{
			this.txtMaintainType.Text = EptMtinfo.MaintainType;
			this.txtMaintainCost.Text = EptMtinfo.MaintainCost.ToString();
			this.dtxtMaintainDate.Text = ((EptMtinfo.MaintainDate == DateTime.MinValue) ? DateTime.Today.ToShortDateString() : EptMtinfo.MaintainDate.ToShortDateString());
			this.txtFault.Text = EptMtinfo.Fault;
			this.txtMaintainContent.Text = EptMtinfo.MaintainContent;
			this.txtMaintainceMan.Text = EptMtinfo.MaintainceMan;
			this.txtAppraisal.Text = EptMtinfo.Appraisal;
			this.txtAppraiser.Text = EptMtinfo.Appraiser;
			this.dtxtAppraisalDate.Text = ((EptMtinfo.AppraisalDate == DateTime.MinValue) ? DateTime.Today.ToShortDateString() : EptMtinfo.AppraisalDate.ToShortDateString());
		}
		private void ShowEquipmentInfo(Guid equipmentUniqueCode)
		{
			EquipmentInfo singleEquipmentInfo = EquipmentAction.GetSingleEquipmentInfo(equipmentUniqueCode);
			this.lblEquipmentName.Text = singleEquipmentInfo.EquipmentName.ToString();
			this.lblEquipmentSpec.Text = singleEquipmentInfo.Spec.ToString();
			this.lblEquipmentCode.Text = singleEquipmentInfo.EquipmentManCode.ToString();
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
			this.btnDel.Click += new EventHandler(this.btnDel_Click);
			this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
		}
		private EquipmentMaintainFittingCollection dgdFittingsDetail_GetData()
		{
			EquipmentMaintainFittingCollection equipmentMaintainFittingCollection = (EquipmentMaintainFittingCollection)this.Session["RepairRecordList"];
			for (int i = this.dgdFittings.Items.Count - 1; i >= 0; i--)
			{
				DataGridItem dataGridItem = this.dgdFittings.Items[i];
				equipmentMaintainFittingCollection[i].FittingsName = ((TextBox)dataGridItem.Cells[0].Controls[1]).Text;
				equipmentMaintainFittingCollection[i].Spec = ((TextBox)dataGridItem.Cells[1].Controls[1]).Text;
				equipmentMaintainFittingCollection[i].Quality = Convert.ToInt32(((TextBox)dataGridItem.Cells[2].Controls[1]).Text);
				equipmentMaintainFittingCollection[i].Operation = ((TextBox)dataGridItem.Cells[3].Controls[1]).Text;
				equipmentMaintainFittingCollection[i].Remark = ((TextBox)dataGridItem.Cells[4].Controls[1]).Text;
			}
			return equipmentMaintainFittingCollection;
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
				e.Item.Attributes["onclick"] = string.Concat(new string[]
				{
					"doClick(this,'",
					this.dgdFittings.ClientID,
					"');clickRow('",
					e.Item.ItemIndex.ToString(),
					"');"
				});
				((TextBox)e.Item.Cells[2].Controls[1]).Attributes["onblur"] = "checkQuantity(this);";
				return;
			default:
				return;
			}
		}
		private EquipmentMaintainInfo GetEquipmentMaintainInfo()
		{
			return new EquipmentMaintainInfo
			{
				MaintainUniqueCode = (Guid)this.ViewState["MAINTAINUNIQUECODE"],
				EquipmentUniqueCode = (Guid)this.ViewState["EQUIPMENTUNIQUECODE"],
				MaintainType = this.txtMaintainType.Text.Trim(),
				MaintainCost = Convert.ToDecimal(this.txtMaintainCost.Text.Trim()),
				Fault = this.txtFault.Text.Trim().ToString(),
				MaintainContent = this.txtMaintainContent.Text.Trim().ToString(),
				MaintainceMan = this.txtMaintainceMan.Text.Trim().ToString(),
				MaintainDate = Convert.ToDateTime(this.dtxtMaintainDate.Text.Trim()),
				RecordDate = DateTime.Today,
				Appraisal = this.txtAppraisal.Text.Trim(),
				AppraisalDate = Convert.ToDateTime(this.dtxtAppraisalDate.Text.Trim()),
				Appraiser = this.txtAppraiser.Text.Trim()
			};
		}
		protected void BtnRepairEdit_Click(object sender, EventArgs e)
		{
			EquipmentMaintainFittingCollection templist = this.dgdFittingsDetail_GetData();
			string a;
			if ((a = this.ViewState["OPTYPE"].ToString()) != null)
			{
				if (!(a == "0"))
				{
					if (!(a == "1"))
					{
						return;
					}
					if (EquipmentMaintainAction.UpdMaintainInfo(this.GetEquipmentMaintainInfo(), templist, (Guid)this.ViewState["MAINTAINUNIQUECODE"]) == 1)
					{
						this.js.Text = "alert('数据修改成功');window.returnValue=true;window.close();";
						return;
					}
					this.js.Text = "alert('数据修改失败');";
				}
				else
				{
					if (EquipmentMaintainAction.AddMaintainInfo(this.GetEquipmentMaintainInfo(), templist) == 1)
					{
						this.js.Text = "alert('数据增加成功');window.returnValue = true;window.close();";
						return;
					}
					this.js.Text = "alert('数据添加失败');";
					return;
				}
			}
		}
		private void btnAdd_Click(object sender, EventArgs e)
		{
			EquipmentMaintainFittingCollection equipmentMaintainFittingCollection = this.dgdFittingsDetail_GetData();
			equipmentMaintainFittingCollection.Add(new EquipmentMaintainFittingInfo
			{
				MaintainUniqueCode = (Guid)this.ViewState["MAINTAINUNIQUECODE"]
			});
			this.dgdFittingsDetail_Bind(equipmentMaintainFittingCollection);
		}
		private void btnDel_Click(object sender, EventArgs e)
		{
			EquipmentMaintainFittingCollection equipmentMaintainFittingCollection = this.dgdFittingsDetail_GetData();
			equipmentMaintainFittingCollection.RemoveAt(int.Parse(this.hdnSelectedIndex.Value));
			this.dgdFittingsDetail_Bind(equipmentMaintainFittingCollection);
		}
	}


