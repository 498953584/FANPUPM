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
public partial class Equ_Equipment_EquipmentView : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equSer = new EquEquipmentService();
	private BasicCodeListService clSer = new BasicCodeListService();
	private string action = string.Empty;
	private string id = string.Empty;

	protected override void OnInit(System.EventArgs e)
	{
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
			this.BindEquInfo();
		}
	}
	private void BindEquInfo()
	{
		new ResResourceService();
		EquEquipmentTypeService equEquipmentTypeService = new EquEquipmentTypeService();
		EquEquipment byId = this.equSer.GetById(this.id);
		this.lblEquCode.Text = byId.EquCode;
		this.lblEquName.Text = byId.EquName;
		EquEquipmentType byId2 = equEquipmentTypeService.GetById(byId.TypeId);
		if (byId2 != null)
		{
			this.lblEquTypeName.Text = byId2.Name;
		}
		if (byId.SupplierId.HasValue)
		{
			XPMBasicContactCorpService xPMBasicContactCorpService = new XPMBasicContactCorpService();
			XPMBasicContactCorp byId3 = xPMBasicContactCorpService.GetById(byId.SupplierId.Value);
			if (byId3 != null)
			{
				this.lblCorpName.Text = byId3.CorpName;
			}
		}
		this.lblFactoryNumber.Text = byId.FactoryNumber;
		this.lblFactoryDate.Text = this.ConvertToString(byId.FactoryDate);
		this.lblPurchaseDate.Text = this.ConvertToString(byId.PurchaseDate);
		this.lblDepreciationRate.Text = byId.DepreciationRate.ToString("0.000");
		this.lblPeriodicVertification.Text = byId.PeriodicVertification;
		this.lblDurableYear.Text = byId.DurableYear;
		this.lblPurchasePrice.Text = byId.PurchasePrice.ToString("0.000");
		this.lblState.Text = this.GetStateOrProperty("EquState", byId.State);
		this.lblEquProperty.Text = this.GetStateOrProperty("EquProperty", byId.EquProperty);
		this.lblReceiptNo.Text = byId.ReceiptNo;
		if (equEquipmentTypeService.IsShip(byId.TypeId))
		{
			this.trShip.Style["Display"] = "Block";
			this.lblShipLength.Text = byId.ShipLength;
			this.lblShipWidth.Text = byId.ShipWidth;
			this.lblShipCapacity.Text = byId.ShipCapaticy;
			EquShipTechnicalParasService equShipTechnicalParasService = new EquShipTechnicalParasService();
			System.Collections.Generic.IList<EquShipTechnicalParas> equShipTechParasByEquId = equShipTechnicalParasService.GetEquShipTechParasByEquId(this.id);
			if (equShipTechParasByEquId == null || equShipTechParasByEquId.Count <= 0)
			{
				goto IL_352;
			}
			if (equShipTechParasByEquId.Count == 1)
			{
				this.lblOtherShipInfo.Text = equShipTechParasByEquId.First<EquShipTechnicalParas>().OtherShipInfo;
			}
			int num = 0;
			Label label = null;
			using (System.Collections.Generic.IEnumerator<EquShipTechnicalParas> enumerator = equShipTechParasByEquId.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EquShipTechnicalParas current = enumerator.Current;
					if (num == 0)
					{
						this.lblOtherShipInfo.Text = string.Format("参数{0}:   {1}", (num + 1).ToString(), current.OtherShipInfo);
						label = (this.plOtherShips.FindControl("lblOtherShipInfo") as Label);
					}
					else
					{
						Literal literal = new Literal();
						literal.Text = "<br />";
						this.plOtherShips.Controls.Add(literal);
						Label label2 = new Label();
						label2.ID = label.ID + num.ToString();
						label2.Text = string.Format("参数{0}:   {1}", num + 1, current.OtherShipInfo);
						this.plOtherShips.Controls.Add(label2);
					}
					num++;
				}
				goto IL_352;
			}
		}
		this.trShip.Style["Display"] = "None";
		IL_352:
		this.lblMiddleInspectDate.Text = this.ConvertToString(byId.MiddleInspectDate);
		this.lblYearInspectDate.Text = this.ConvertToString(byId.YearInspectDate);
		this.lblOtherCredentials.Text = byId.OtherCredentials;
		this.lblAnnex.Text = FileView.FilesBind(1901, byId.Id);
		this.lblNotes.Text = byId.Note;
	}
	private string ConvertToString(System.DateTime? value)
	{
		if (value.HasValue)
		{
			return value.Value.ToString("yyyy-MM-dd");
		}
		return string.Empty;
	}
	private string GetStateOrProperty(string key, int value)
	{
		string arg_12_0 = string.Empty;
		BasicCodeList basicCodeList = this.clSer.GetByType(key).FirstOrDefault((BasicCodeList code) => code.ItemCode == value);
		if (basicCodeList != null)
		{
			return basicCodeList.ItemName;
		}
		return string.Empty;
	}
}


