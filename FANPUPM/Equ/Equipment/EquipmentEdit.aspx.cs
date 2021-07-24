using ASP;
using cn.justwin.BLL;
using cn.justwin.Domain.Entities;
using cn.justwin.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class Equ_Equipment_EquipmentEdit : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equSer = new EquEquipmentService();
	private EquEquipmentTypeService typeService = new EquEquipmentTypeService();
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["action"]))
		{
			this.action = base.Request["action"].ToString();
		}
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
			this.BindStateAndProperty();
			this.BindEquInfo();
		}
		this.flAnnx.MID = 1901;
		this.flAnnx.FID = this.hdfEquId.Value;
		this.flAnnx.Type = 1;
	}
	private void BindEquInfo()
	{
		if (this.action == "edit")
		{
			new ResResourceService();
			EquEquipment byId = this.equSer.GetById(this.id);
			this.txtEquipmentCode.Text = byId.EquCode;
			this.txtEquName.Text = byId.EquName;
			this.hfldEquTypeId.Value = byId.TypeId;
			EquEquipmentType byId2 = this.typeService.GetById(byId.TypeId);
			if (byId2 != null)
			{
				this.txtEquTypeName.Value = byId2.Name;
			}
			this.ddlProperty.SelectedValue = byId.EquProperty.ToString();
			this.txtPurchasePrice.Text = byId.PurchasePrice.ToString("0.000");
			this.txtDepreciationRate.Text = string.Format("{0}%", byId.DepreciationRate.ToString("0.000"));
			this.txtPurchaseDate.Text = (byId.PurchaseDate.HasValue ? byId.PurchaseDate.Value.ToString("yyyy-MM-dd") : string.Empty);
			this.txtDurableYear.Text = byId.DurableYear;
			this.txtFactoryNumber.Text = byId.FactoryNumber;
			this.ddlState.SelectedValue = byId.State.ToString();
			if (byId.SupplierId.HasValue)
			{
				this.hfldCorpId.Value = byId.SupplierId.Value.ToString();
				XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
				XPMBasicContactCorp byId3 = xPMBasicContactCorpService.GetById(byId.SupplierId.Value);
				if (byId3 != null)
				{
					this.txtCorpName.Value = byId3.CorpName;
				}
			}
			this.txtFactoryDate.Text = (byId.FactoryDate.HasValue ? byId.FactoryDate.Value.ToString("yyyy-MM-dd") : string.Empty);
			this.txtPeriodicVertification.Text = byId.PeriodicVertification;
			this.txtReceiptNo.Text = byId.ReceiptNo;
			if (this.typeService.IsShip(byId.TypeId))
			{
				this.txtShipLength.Text = byId.ShipLength;
				this.txtShipWidth.Text = byId.ShipWidth;
				this.txtShipCapacity.Text = byId.ShipCapaticy;
				EquShipTechnicalParasService equShipTechnicalParasService = new EquShipTechnicalParasService();
				System.Collections.Generic.IList<EquShipTechnicalParas> equShipTechParasByEquId = equShipTechnicalParasService.GetEquShipTechParasByEquId(this.id);
				if (equShipTechParasByEquId != null && equShipTechParasByEquId.Count > 0)
				{
					foreach (EquShipTechnicalParas current in equShipTechParasByEquId)
					{
						if (!string.IsNullOrEmpty(this.hfldOtherShipsInfo.Value.Trim()))
						{
							HiddenField expr_2CB = this.hfldOtherShipsInfo;
							expr_2CB.Value += "|";
						}
						HiddenField expr_2E6 = this.hfldOtherShipsInfo;
						expr_2E6.Value += current.OtherShipInfo;
					}
				}
			}
			this.txtMiddleInspectDate.Text = (byId.MiddleInspectDate.HasValue ? byId.MiddleInspectDate.Value.ToString("yyyy-MM-dd") : string.Empty);
			this.txtYearInspectDate.Text = (byId.YearInspectDate.HasValue ? byId.YearInspectDate.Value.ToString("yyyy-MM-dd") : string.Empty);
			this.txtOtherCredentials.Text = byId.OtherCredentials;
			this.txtNotes.Text = byId.Note;
			this.SetShipInfoVisible();
		}
		else
		{
			this.id = System.Guid.NewGuid().ToString();
		}
		this.hdfEquId.Value = this.id;
	}
	private void BindStateAndProperty()
	{
		BasicCodeListService basicCodeListService = new BasicCodeListService();
		this.ddlState.DataSource = (
			from cs in basicCodeListService.GetByType("EquState")
			orderby cs.ItemCode
			select cs).ToList<BasicCodeList>();
		this.ddlState.DataTextField = "ItemName";
		this.ddlState.DataValueField = "ItemCode";
		this.ddlState.DataBind();
		this.ddlProperty.DataSource = (
			from cs in basicCodeListService.GetByType("EquProperty")
			orderby cs.ItemCode
			select cs).ToList<BasicCodeList>();
		this.ddlProperty.DataTextField = "ItemName";
		this.ddlProperty.DataValueField = "ItemCode";
		this.ddlProperty.DataBind();
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			string equCode = this.txtEquipmentCode.Text.Trim();
			System.Collections.Generic.List<EquEquipment> list = (
				from es in this.equSer
				where es.EquCode == equCode
				select es).ToList<EquEquipment>();
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			if ("add".Equals(this.action))
			{
				if (list.Count > 0)
				{
					base.RegisterScript("top.ui.show('编号已经存在，请重新输入！')");
					return;
				}
				EquEquipment model = this.GetModel(null);
				this.equSer.Add(model);
				this.AddOtherShipsInfo();
				stringBuilder.Append("top.ui.show('添加成功');").Append(System.Environment.NewLine);
			}
			else
			{
				EquEquipment byId = this.equSer.GetById(this.id);
				if (list.Count > 0 && byId.EquCode != equCode)
				{
					base.RegisterScript("top.ui.show('编号已经存在，请重新输入！')");
					return;
				}
				this.equSer.Update(this.GetModel(byId));
				this.AddOtherShipsInfo();
				stringBuilder.Append("top.ui.show('编辑成功');").Append(System.Environment.NewLine);
			}
			stringBuilder.Append("winclose('EquipmentEdit', 'EquipmentList.aspx', true)");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	private EquEquipment GetModel(EquEquipment model)
	{
		if (model == null)
		{
			model = new EquEquipment();
			model.Id = this.hdfEquId.Value;
		}
		model.EquCode = this.txtEquipmentCode.Text.Trim();
		model.EquName = this.txtEquName.Text.Trim();
		model.TypeId = this.hfldEquTypeId.Value;
		model.FactoryNumber = this.txtFactoryNumber.Text.Trim();
		model.FactoryDate = this.SetDateTime(this.txtFactoryDate.Text.Trim());
		model.PurchaseDate = this.SetDateTime(this.txtPurchaseDate.Text.Trim());
		decimal depreciationRate = 0m;
		if (!string.IsNullOrEmpty(this.txtDepreciationRate.Text.Trim()))
		{
			depreciationRate = System.Convert.ToDecimal(this.txtDepreciationRate.Text.Trim().Replace("%", string.Empty));
		}
		model.DepreciationRate = depreciationRate;
		model.PeriodicVertification = this.txtPeriodicVertification.Text.Trim();
		model.DurableYear = this.txtDurableYear.Text.Trim();
		decimal purchasePrice = 0m;
		if (!string.IsNullOrEmpty(this.txtPurchasePrice.Text.Trim()))
		{
			purchasePrice = System.Convert.ToDecimal(this.txtPurchasePrice.Text.Trim());
		}
		model.PurchasePrice = purchasePrice;
		model.SupplierId = this.SetInteger(this.hfldCorpId.Value);
		model.State = System.Convert.ToInt32(this.ddlState.SelectedValue);
		model.EquProperty = System.Convert.ToInt32(this.ddlProperty.SelectedValue);
		model.ReceiptNo = this.txtReceiptNo.Text.Trim();
		if (this.typeService.IsShip(this.hfldEquTypeId.Value.Trim()))
		{
			model.ShipLength = this.txtShipLength.Text.Trim();
			model.ShipWidth = this.txtShipWidth.Text.Trim();
			model.ShipCapaticy = this.txtShipCapacity.Text.Trim();
		}
		model.MiddleInspectDate = this.SetDateTime(this.txtMiddleInspectDate.Text.Trim());
		model.YearInspectDate = this.SetDateTime(this.txtYearInspectDate.Text.Trim());
		model.OtherCredentials = this.txtOtherCredentials.Text.Trim();
		model.InputUser = base.UserCode;
		model.InputDate = System.DateTime.Now;
		model.Note = this.txtNotes.Text.Trim();
		return model;
	}
	private System.DateTime? SetDateTime(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			return new System.DateTime?(System.Convert.ToDateTime(value));
		}
		return null;
	}
	private decimal? SetDecimal(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			return new decimal?(System.Convert.ToDecimal(value));
		}
		return null;
	}
	private int? SetInteger(string value)
	{
		if (!string.IsNullOrEmpty(value))
		{
			return new int?(System.Convert.ToInt32(value));
		}
		return null;
	}
	protected void hdButton_Click(object sender, System.EventArgs e)
	{
		this.SetShipInfoVisible();
	}
	private void SetShipInfoVisible()
	{
		if (!string.IsNullOrEmpty(this.hfldEquTypeId.Value.Trim()))
		{
			if (!this.typeService.IsShip(this.hfldEquTypeId.Value.Trim()))
			{
				this.trShip.Style["Display"] = "none";
				return;
			}
			this.trShip.Style["Display"] = "Block";
		}
	}
	private void AddOtherShipsInfo()
	{
		EquShipTechnicalParasService equShipTechnicalParasService = new EquShipTechnicalParasService();
		equShipTechnicalParasService.DeleteAllEquShipTechInfos(this.hdfEquId.Value.Trim());
		if (!string.IsNullOrEmpty(this.hfldOtherShipsInfo.Value.Trim()))
		{
			string[] otherShipInfo = this.hfldOtherShipsInfo.Value.Split(new char[]
			{
				'|'
			});
			equShipTechnicalParasService.InsertEquShipTechParasTable(this.hdfEquId.Value.Trim(), otherShipInfo);
		}
	}
}


