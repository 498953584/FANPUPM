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
public partial class Equ_ShipProductionReport_ShipProductionReportEdit : NBasePage, IRequiresSessionState
{
	private EquEquipmentService equipmentSer = new EquEquipmentService();
	private BasicCodeListService basicCodeSer = new BasicCodeListService();
	private EquShipDayReportService dayService = new EquShipDayReportService();
	private string equId = string.Empty;
	private string dayId = string.Empty;
	private bool isAdd = true;

	protected override void OnInit(System.EventArgs e)
	{
		if (!string.IsNullOrEmpty(base.Request["equId"]))
		{
			this.equId = base.Request["equId"].ToString();
		}
		if (!string.IsNullOrEmpty(base.Request["dayId"]))
		{
			this.dayId = base.Request["dayId"].ToString();
			this.isAdd = false;
		}
		base.OnInit(e);
	}
	protected void Page_Load(object sender, System.EventArgs e)
	{
		if (!base.IsPostBack)
		{
			this.BindProCalculateType();
			this.BindInfo();
		}
		this.txtPrjName.Attributes["ReadyOnly"] = "true";
		this.txtBargeName0.Attributes["ReadyOnly"] = "true";
		this.hfldContractName.Attributes["ReadyOnly"] = "true";
	}
	private void BindProCalculateType()
	{
		System.Collections.Generic.IList<BasicCodeList> dataSource = (
			from ba in this.basicCodeSer.GetByType("ProCalculateType")
			orderby ba.ItemCode
			select ba).ToList<BasicCodeList>();
		this.ddlProCalculateType.DataSource = dataSource;
		this.ddlProCalculateType.DataTextField = "ItemName";
		this.ddlProCalculateType.DataValueField = "ItemCode";
		this.ddlProCalculateType.DataBind();
	}
	private void BindInfo()
	{
		EquEquipment equInfo = this.equipmentSer.GetById(this.equId);
		this.txtEquName.Text = equInfo.EquName;
		this.txtEquCode.Text = equInfo.EquCode;
		BasicCodeList basicCodeList = this.basicCodeSer.GetByType("EquProperty").FirstOrDefault((BasicCodeList pro) => pro.ItemCode == equInfo.EquProperty);
		if (basicCodeList != null)
		{
			this.txtEquProperty.Text = basicCodeList.ItemName;
		}
		if (!this.isAdd)
		{
			EquShipDayReport byId = this.dayService.GetById(this.dayId);
			if (byId != null)
			{
				PTPrjInfoService pTPrjInfoService = new PTPrjInfoService();
				PTPrjInfo byId2 = pTPrjInfoService.GetById(byId.PrjId);
				if (byId2 != null)
				{
					this.txtPrjName.Text = byId2.PrjName;
					this.hfldPrjId.Value = byId.PrjId;
				}
				this.txtReportDate.Text = Common2.GetTime(byId.ReportDate);
				this.txtConstructionDate.Text = Common2.GetTime(byId.ConstructionDate);
				this.txtWorkDurationT1.Text = (byId.WorkDurationT1.HasValue ? byId.WorkDurationT1.Value.ToString("0.000") : "0.000");
				this.txtDayOilWear.Text = (byId.DayOilWear.HasValue ? byId.DayOilWear.Value.ToString("0.000") : "0.000");
				this.txtNotWorkRestDurationT3.Text = (byId.NotWorkRestDurationT3.HasValue ? byId.NotWorkRestDurationT3.Value.ToString("0.000") : "0.000");
				this.txtWorkRestDurationT2.Text = (byId.WorkRestDurationT2.HasValue ? byId.WorkRestDurationT2.Value.ToString("0.000") : "0.000");
				this.txtNote.Text = byId.Note;
				this.setOutputValueType(byId);
			}
		}
		else
		{
			this.dayId = System.Guid.NewGuid().ToString();
			this.txtConstructionDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
			this.txtReportDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");
		}
		this.hfldDayId.Value = this.dayId;
		this.flAnnx.MID = 1901;
		this.flAnnx.FID = this.hfldDayId.Value;
		this.flAnnx.Type = 1;
	}
	private void setOutputValueType(EquShipDayReport report)
	{
		switch (report.OutputValueType)
		{
		case 0:
			this.cbxProduction.Checked = true;
			this.cbxTeam.Checked = false;
			this.rbIsTeamInfo.Checked = false;
			this.ddlProCalculateType.SelectedValue = (report.CalculateType.HasValue ? report.CalculateType.Value.ToString() : "0");
			return;
		case 1:
			this.cbxProduction.Checked = false;
			this.cbxTeam.Checked = true;
			this.rbIsTeamInfo.Checked = false;
			return;
		case 2:
			this.cbxProduction.Checked = true;
			this.cbxTeam.Checked = true;
			if (report.IsTeamInfo == 1)
			{
				this.rbIsTeamInfo.Attributes["value"] = "0";
				this.rbIsTeamInfo.Checked = true;
			}
			else
			{
				this.rbIsTeamInfo.Attributes["value"] = "1";
				this.rbIsTeamInfo.Checked = false;
			}
			this.ddlProCalculateType.SelectedValue = (report.CalculateType.HasValue ? report.CalculateType.Value.ToString() : "0");
			return;
		default:
			this.cbxProduction.Checked = false;
			this.cbxTeam.Checked = false;
			this.rbIsTeamInfo.Checked = false;
			return;
		}
	}
	protected void btnSave_Click(object sender, System.EventArgs e)
	{
		try
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			if (this.isAdd)
			{
				EquShipDayReport dayReportModel = this.GetDayReportModel(null);
				this.dayService.Add(dayReportModel);
				stringBuilder.Append("top.ui.show('新增成功');").Append(System.Environment.NewLine);
			}
			else
			{
				EquShipDayReport byId = this.dayService.GetById(this.dayId);
				EquShipDayReport dayReportModel = this.GetDayReportModel(byId);
				this.dayService.Update(dayReportModel);
				stringBuilder.Append("top.ui.show('修改成功');").Append(System.Environment.NewLine);
			}
			stringBuilder.Append("winclose('ShipGrapReportEdit', '../DayOutputMenu/DayOutputMenu.aspx?isShip=1&EquEnum=1', true);");
			base.RegisterScript(stringBuilder.ToString());
		}
		catch (System.Exception)
		{
			base.RegisterScript("top.ui.show('保存失败！')");
		}
	}
	private EquShipDayReport GetDayReportModel(EquShipDayReport dayModel)
	{
		if (dayModel == null)
		{
			dayModel = new EquShipDayReport();
			dayModel.DayId = System.Guid.NewGuid().ToString();
			dayModel.FlowState = -1;
		}
		dayModel.EquId = this.equId;
		dayModel.ReportDate = System.Convert.ToDateTime(this.txtReportDate.Text.Trim());
		dayModel.ConstructionDate = new System.DateTime?(System.Convert.ToDateTime(this.txtConstructionDate.Text.Trim()));
		dayModel.PrjId = this.hfldPrjId.Value;
		dayModel.WorkDurationT1 = new decimal?(System.Convert.ToDecimal(this.txtWorkDurationT1.Text.Trim()));
		dayModel.WorkRestDurationT2 = new decimal?(System.Convert.ToDecimal(this.txtWorkRestDurationT2.Text.Trim()));
		dayModel.NotWorkRestDurationT3 = new decimal?(System.Convert.ToDecimal(this.txtNotWorkRestDurationT3.Text.Trim()));
		dayModel.DayOilWear = new decimal?(System.Convert.ToDecimal(this.txtDayOilWear.Text.Trim()));
		dayModel.OutputValueType = this.getOutputValueType();
		if (!"-1".Equals(this.ddlProCalculateType.SelectedValue))
		{
			dayModel.CalculateType = new int?(System.Convert.ToInt32(this.ddlProCalculateType.SelectedValue));
		}
		if (dayModel.OutputValueType == 2)
		{
			dayModel.IsTeamInfo = (string.IsNullOrEmpty(base.Request.Form["rbIsTeamInfo"]) ? 0 : 1);
		}
		else
		{
			dayModel.IsTeamInfo = 0;
		}
		if ("3".Equals(this.ddlProCalculateType.SelectedValue))
		{
			string value = this.txtDiscountRate.Text.Trim().Replace("%", string.Empty);
			if (!string.IsNullOrEmpty(value))
			{
				dayModel.DiscountRate = new decimal?(System.Convert.ToDecimal(value));
			}
		}
		dayModel.InputUser = base.UserCode;
		dayModel.InputDate = System.DateTime.Now;
		dayModel.Note = this.txtNote.Text.Trim();
		return dayModel;
	}
	private int getOutputValueType()
	{
		if (this.cbxProduction.Checked && this.cbxTeam.Checked)
		{
			return 2;
		}
		if (this.cbxProduction.Checked)
		{
			return 0;
		}
		if (this.cbxTeam.Checked)
		{
			return 1;
		}
		return -1;
	}
}


