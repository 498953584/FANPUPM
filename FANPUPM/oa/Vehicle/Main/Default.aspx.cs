using ASP;
using cn.justwin.VehiclesBLL;
using cn.justwin.VehiclesModel;
using com.jwsoft.common.baseclass;
using System;
using System.Collections.Generic;
using System.Web.Profile;
using System.Web.SessionState;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class oa_Vehicle_Main_VehicleMainEdit : NBasePage, IRequiresSessionState
{
	private string _VehicletId = string.Empty;
	protected string selectVehicle = " onclick=selectVehicleType()";
	protected string selectPurchaser = "onclick=selectPurchaser()";
	protected string selectVehicleState = "onclick=selectVehicleState()";
	private string action = string.Empty;
	private MainBLL mainBll = new MainBLL();

	protected override void OnInit(EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["Action"]))
		{
			this.action = base.Request["Action"];
		}
		if (!string.IsNullOrEmpty(base.Request["VehicletId"]))
		{
			this._VehicletId = base.Request["VehicletId"];
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, EventArgs e)
	{
		if (!this.Page.IsPostBack)
		{
			if (string.Compare(this.action, "Update", true) == 0)
			{
				this.initUpdate();
				return;
			}
			if (string.Compare(this.action, "Query", true) == 0)
			{
				this.btnSave.Visible = false;
				this.selectVehicle = "";
				this.selectPurchaser = "";
				this.selectVehicleState = "";
				this.initQuery();
			}
		}
	}
	protected void btnSave_Click(object sender, EventArgs e)
	{
		if (string.Compare(this.action, "Add", true) == 0)
		{
			MainModel mainModel = new MainModel();
			this.initAdd(mainModel);
			this.add(mainModel);
			return;
		}
		if (string.Compare(this.action, "Update", true) == 0)
		{
			MainModel model = new MainModel();
			this.initAdd(model);
			this.update(model);
		}
	}
	private void add(MainModel MODEL)
	{
		try
		{
			List<MainModel> list = new List<MainModel>();
			list = this.mainBll.GetModelList("VehicleCode='" + MODEL.VehicleCode + "'");
			if (list.Count > 0)
			{
				base.RegisterScript("alert('系统提示：\\n\\n此车牌号码已经存在')");
			}
			else
			{
				this.mainBll.Add(MODEL);
				base.RegisterScript("top.ui.tabSuccess({ parentName: '_VehicleMain' });");
			}
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n保存失败')");
		}
	}
	private void initAdd(MainModel model)
	{
		string text = this.txtVehicleCode.Text;
		string vehicleName = this.txtVehicleName.Text.ToString().Trim();
		string text2 = this.txtVehicleIdentify.Text;
		string text3 = this.txtEngineCode.Text;
		string text4 = this.txtSpecification.Text;
		DateTime? purchaseDate = new DateTime?(default(DateTime));
		if (this.txtPurchaseDate.Text.ToString() == "")
		{
			purchaseDate = null;
		}
		else
		{
			purchaseDate = new DateTime?(DateTime.Parse(this.txtPurchaseDate.Text.ToString()));
		}
		DateTime? onHouserDate = new DateTime?(default(DateTime));
		if (this.txtOnHouserDate.Text.ToString() == "")
		{
			onHouserDate = null;
		}
		else
		{
			onHouserDate = new DateTime?(DateTime.Parse(this.txtOnHouserDate.Text.ToString()));
		}
		DateTime? inspectionDate = new DateTime?(default(DateTime));
		if (this.txtInspectionDate.Text.ToString() == "")
		{
			inspectionDate = null;
		}
		else
		{
			inspectionDate = new DateTime?(DateTime.Parse(this.txtInspectionDate.Text.ToString()));
		}
		DateTime? insuranceDate = new DateTime?(default(DateTime));
		if (this.txtInsuranceDate.Text.ToString() == "")
		{
			insuranceDate = null;
		}
		else
		{
			insuranceDate = new DateTime?(DateTime.Parse(this.txtInsuranceDate.Text.ToString()));
		}
		string text5 = this.txtAddress.Text;
		string sparekey = this.txtSparekey.Text.Trim().ToString();
		string text6 = this.txtAbility.Text;
		decimal value = decimal.Parse("0.0");
		if (this.txtFatfare.Text.ToString() != "")
		{
			value = decimal.Parse(this.txtFatfare.Text);
		}
		if (this.txtRecordedprice.Text.ToString() != "")
		{
			decimal value2 = decimal.Parse(this.txtRecordedprice.Text);
			model.Recordedprice = new decimal?(value2);
		}
		else
		{
			model.Recordedprice = new decimal?(decimal.Parse("0.0"));
		}
		model.ManufactureDate = this.txtManufactureDate.Text;
		decimal value3 = decimal.Parse("0.0");
		if (this.txtDepreciationRate.Text.ToString() != "")
		{
			value3 = decimal.Parse(this.txtDepreciationRate.Text);
		}
		string text7 = this.txtRemark.Text;
		string text8 = this.txtPurchaser.Text;
		string value4 = this.hfldVehicleType.Value;
		string value5 = this.hfldVehicleState.Value;
		int value6 = 0;
		if (this.RadButIsShare.Checked)
		{
			value6 = 1;
		}
		model.State = new Guid(value5);
		model.VehicleType = new Guid(value4);
		model.VehicleCode = text;
		model.VehicleName = vehicleName;
		model.VehicleIdentify = text2;
		model.EngineCode = text3;
		model.Specification = text4;
		model.PurchaseDate = purchaseDate;
		model.OnHouserDate = onHouserDate;
		model.InspectionDate = inspectionDate;
		model.InsuranceDate = insuranceDate;
		model.Address = text5;
		model.Sparekey = sparekey;
		model.Ability = text6;
		model.Fatfare = new decimal?(value);
		model.DepreciationRate = new decimal?(value3);
		model.Remark = text7;
		model.Purchaser = text8;
		model.IsShare = new int?(value6);
		if (this._VehicletId != "")
		{
			model.guid = new Guid(this._VehicletId);
		}
	}
	private void initUpdate()
	{
		if (!string.IsNullOrEmpty(this._VehicletId))
		{
			MainModel mainModel = new MainModel();
			mainModel = this.mainBll.GetModel(new Guid(this._VehicletId));
			if (mainModel != null)
			{
				this.vehicle_Code_Number_session.Value = mainModel.VehicleCode;
				this.txtVehicleCode.Text = mainModel.VehicleCode;
				this.txtVehicleIdentify.Text = mainModel.VehicleIdentify;
				this.txtEngineCode.Text = mainModel.EngineCode;
				this.txtSpecification.Text = mainModel.Specification;
				if (mainModel.PurchaseDate.HasValue && mainModel.PurchaseDate.ToString() != "")
				{
					this.txtPurchaseDate.Text = Convert.ToDateTime(mainModel.PurchaseDate).ToShortDateString();
				}
				if (mainModel.OnHouserDate.HasValue && mainModel.OnHouserDate.ToString() != "")
				{
					this.txtOnHouserDate.Text = Convert.ToDateTime(mainModel.OnHouserDate).ToShortDateString();
				}
				if (mainModel.InspectionDate.HasValue && mainModel.InspectionDate.ToString() != "")
				{
					this.txtInspectionDate.Text = Convert.ToDateTime(mainModel.InspectionDate).ToShortDateString();
				}
				if (mainModel.InsuranceDate.HasValue && mainModel.InsuranceDate.ToString() != "")
				{
					this.txtInsuranceDate.Text = Convert.ToDateTime(mainModel.InsuranceDate).ToShortDateString();
				}
				this.txtAddress.Text = mainModel.Address;
				this.txtSparekey.Text = mainModel.Sparekey;
				this.txtAbility.Text = mainModel.Ability;
				this.txtFatfare.Text = mainModel.Fatfare.ToString();
				this.txtRecordedprice.Text = mainModel.Recordedprice.ToString();
				if (mainModel.ManufactureDate != null && mainModel.ManufactureDate.ToString().Length > 0)
				{
					this.txtManufactureDate.Text = Convert.ToDateTime(mainModel.ManufactureDate).ToShortDateString();
				}
				else
				{
					this.txtManufactureDate.Text = "";
				}
				this.txtDepreciationRate.Text = mainModel.DepreciationRate.ToString();
				this.txtRemark.Text = mainModel.Remark;
				this.hfldVehicleType.Value = mainModel.VehicleType.ToString();
				this.txtVehicleType.Text = this.GetTypeOrStateName(mainModel.VehicleType);
				this.hfldVehicleState.Value = mainModel.State.ToString();
				this.txtVehicleState.Text = this.GetTypeOrStateName(mainModel.State);
				this.txtPurchaser.Text = mainModel.Purchaser;
				this.txtVehicleName.Text = mainModel.VehicleName;
				if (mainModel.IsShare == 1)
				{
					this.RadButIsShare.Checked = true;
					return;
				}
				this.RadButIsShare.Checked = false;
			}
		}
	}
	private void initQuery()
	{
		if (!string.IsNullOrEmpty(this._VehicletId))
		{
			MainModel mainModel = new MainModel();
			mainModel = this.mainBll.GetModel(new Guid(this._VehicletId));
			if (mainModel != null)
			{
				this.txtVehicleCode.Text = mainModel.VehicleCode;
				this.txtVehicleName.Text = mainModel.VehicleName;
				this.txtVehicleIdentify.Text = mainModel.VehicleIdentify;
				this.txtEngineCode.Text = mainModel.EngineCode;
				this.txtSpecification.Text = mainModel.Specification;
				if (mainModel.PurchaseDate.HasValue && mainModel.PurchaseDate.ToString() != "")
				{
					this.txtPurchaseDate.Text = Convert.ToDateTime(mainModel.PurchaseDate).ToShortDateString();
				}
				if (mainModel.OnHouserDate.HasValue && mainModel.OnHouserDate.ToString() != "")
				{
					this.txtOnHouserDate.Text = Convert.ToDateTime(mainModel.OnHouserDate).ToShortDateString();
				}
				if (mainModel.InspectionDate.HasValue && mainModel.InspectionDate.ToString() != "")
				{
					this.txtInspectionDate.Text = Convert.ToDateTime(mainModel.InspectionDate).ToShortDateString();
				}
				if (mainModel.InsuranceDate.HasValue && mainModel.InsuranceDate.ToString() != "")
				{
					this.txtInsuranceDate.Text = Convert.ToDateTime(mainModel.InsuranceDate).ToShortDateString();
				}
				this.txtAddress.Text = mainModel.Address;
				this.txtSparekey.Text = mainModel.Sparekey;
				this.txtAbility.Text = mainModel.Ability;
				this.txtFatfare.Text = mainModel.Fatfare.ToString();
				this.txtRecordedprice.Text = mainModel.Recordedprice.ToString();
				if (mainModel.ManufactureDate != null && mainModel.ManufactureDate.ToString().Length > 0)
				{
					this.txtManufactureDate.Text = Convert.ToDateTime(mainModel.ManufactureDate).ToShortDateString();
				}
				else
				{
					this.txtManufactureDate.Text = "";
				}
				this.txtDepreciationRate.Text = mainModel.DepreciationRate.ToString();
				this.txtRemark.Text = mainModel.Remark;
				this.hfldVehicleType.Value = mainModel.VehicleType.ToString();
				this.txtVehicleType.Text = this.GetTypeOrStateName(mainModel.VehicleType);
				this.hfldVehicleState.Value = mainModel.State.ToString();
				this.txtVehicleState.Text = this.GetTypeOrStateName(mainModel.State);
				this.txtPurchaser.Text = mainModel.Purchaser;
				this.txtVehicleName.Text = mainModel.VehicleName;
				if (mainModel.IsShare == 1)
				{
					this.RadButIsShare.Checked = true;
					return;
				}
				this.RadButIsShare.Checked = false;
			}
		}
	}
	private void update(MainModel model)
	{
		try
		{
			if (this.vehicle_Code_Number_session.Value != null && this.vehicle_Code_Number_session.Value.Trim() != this.txtVehicleCode.Text.Trim())
			{
				List<MainModel> list = new List<MainModel>();
				list = this.mainBll.GetModelList("VehicleCode='" + model.VehicleCode + "'");
				if (list.Count > 0)
				{
					base.RegisterScript("alert('系统提示：\\n\\n此车牌号码已经存在')");
					return;
				}
			}
			this.mainBll.Update(model);
			base.RegisterScript("top.ui.tabSuccess({ parentName: '_VehicleMain' });");
		}
		catch (Exception)
		{
			base.RegisterScript("alert('系统提示：\\n\\n保存失败')");
		}
	}
	private string GetTypeOrStateName(Guid guid)
	{
		TypeAndStateBll typeAndStateBll = new TypeAndStateBll();
		TypeAndState model = typeAndStateBll.GetModel(guid);
		return model.Name;
	}
}


