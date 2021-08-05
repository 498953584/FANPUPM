using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using cn.justwin.BLL;
using cn.justwin.Domain.Services;
using com.jwsoft.pm.data;

public partial class TowerStation_TowerStationEdit : NBasePage, IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack)
        {
            return;
        }
        var mode = Request.QueryString["mode"];
        var oid = Request.QueryString["oid"];
        BindProvice();
        switch (mode)
        {
            case "1":
                break;
            case "2":
            case "3":
                var strSql = @"SELECT * FROM dbo.TowerStationInfo WHERE TowerStationGUID=@oid;";
                var dt = publicDbOpClass.ExecuteDataTable(CommandType.Text, strSql, new[] {new SqlParameter("oid", oid)});
                if (dt != null && dt.Rows.Count > 0)
                {
                    var dr = dt.Rows[0];
                    TxtName.Text = dr["TowerName"].ToString();

                    TxtProvince.Text = dr["Province"].ToString();
                    TxtCity.Text = dr["City"].ToString();
                    TxtArea.Text = dr["Area"].ToString();
                    TxtAddress.Text = dr["Address"].ToString();
                    TxtLiaison.Text = dr["Liaison"].ToString();
                    TxtPhone.Text = dr["Phone"].ToString();
                    TxtEmail.Text = dr["Email"].ToString();
                    TxtBuildTime.Text = string.IsNullOrEmpty(dr["BuildTime"].ToString())
                        ? string.Empty
                        : DateTime.Parse(dr["BuildTime"].ToString()).ToString("yyyy-MM-dd");
                    TxtNominalLongitude.Text = dr["NominalLongitude"].ToString();
                    TxtNominalDimension.Text = dr["NominalDimension"].ToString();
                    TxtGPSLongitude.Text = dr["GPSLongitude"].ToString();
                    TxtGPSDimension.Text = dr["GPSDimension"].ToString();
                    TxtMapLongitude.Text = dr["MapLongitude"].ToString();
                    TxtMapDimension.Text = dr["MapDimension"].ToString();
                    TxtMapDatum.Text = dr["MapDatum"].ToString();
                    TxtHorizontalPosition.Text = dr["HorizontalPosition"].ToString();
                    TxtShootingPosition.Text = dr["ShootingPosition"].ToString();
                    TxtAltitude.Text = dr["Altitude"].ToString();
                    TxtMapPosition.Text = dr["MapPosition"].ToString();
                    TxtRoadDistance.Text = dr["RoadDistance"].ToString();
                    TxtPrivateRoadLength.Text = dr["PrivateRoadLength"].ToString();
                    TxtBSHeight.Text = dr["BSHeight"].ToString();
                    TxtBSLayersNum.Text = dr["BSLayersNum"].ToString();
                    TxtBSType.Text = dr["BSType"].ToString();
                    TxtBSYears.Text = dr["BSYears"].ToString();
                    TxtSpecificationWidth.Text = dr["SpecificationWidth"].ToString();
                    TxtSpecificationHeight.Text = dr["SpecificationHeight"].ToString();
                    TxtPeLoadingCapacity.Text = dr["PeLoadingCapacity"].ToString();
                    TxtCeLoadingCapacity.Text = dr["CeLoadingCapacity"].ToString();
                    TxtCeSpecificationWidth.Text = dr["CeSpecificationWidth"].ToString();
                    TxtCeSpecificationHeight.Text = dr["CeSpecificationHeight"].ToString();
                    TxtMeasureHeight.Text = dr["MeasureHeight"].ToString();
                    TxtUniversalLadderWidth.Text = dr["UniversalLadderWidth"].ToString();
                    TxtUniversalLadderHeight.Text = dr["UniversalLadderHeight"].ToString();
                    TxtCurrentAvailablePalletNum.Text = dr["CurrentAvailablePalletNum"].ToString();
                    TxtLastAvailablePalletNum.Text = dr["LastAvailablePalletNum"].ToString();
                    TxtCurrentCableFeederNum.Text = dr["CurrentCableFeederNum"].ToString();
                    TxtLastCableFeederNum.Text = dr["LastCableFeederNum"].ToString();
                    TxtFeederType.Text = dr["FeederType"].ToString();
                    TxtPowerModel.Text = dr["PowerModel"].ToString();
                    TxtPowerSn.Text = dr["PowerSn"].ToString();
                    TxtAirconditionOperatorName.Text = dr["AirconditionOperatorName"].ToString();
                    TxtAirconditionModel.Text = dr["AirconditionModel"].ToString();
                    TxtAirconditionBrand.Text = dr["AirconditionBrand"].ToString();
                    TxtAirconditionSn.Text = dr["AirconditionSn"].ToString();
                    TxtAirconditionCapacity.Text = dr["AirconditionCapacity"].ToString();
                    TxtIronTowerHeight.Text = dr["IronTowerHeight"].ToString();
                    TxtIronTowerLocation.Text = dr["IronTowerLocation"].ToString();
                    TxtIronTowerManufacturer.Text = dr["IronTowerManufacturer"].ToString();
                    TxtLightingManufacturer.Text = dr["LightingManufacturer"].ToString();
                    TxtMonitorManufacturer.Text = dr["MonitorManufacturer"].ToString();
                    TxtSensingManufacturer.Text = dr["SensingManufacturer"].ToString();
                    TxtNetworkManufacturer.Text = dr["NetworkManufacturer"].ToString();
                    TxtVisualDistance.Text = dr["VisualDistance"].ToString();
                    TxtVisitorsFlowrate.Text = dr["VisitorsFlowrate"].ToString();
                    TxtMediaSpecification.Text = dr["MediaSpecification"].ToString();
                    TxtPublishingIndustry.Text = dr["PublishingIndustry"].ToString();
                    TxtGroundClearance.Text = dr["GroundClearance"].ToString();
                    TxtVehicleFlowrate.Text = dr["VehicleFlowrate"].ToString();
                    TxtLaunchCycle.Text = dr["LaunchCycle"].ToString();
                    TxtReleaseBrand.Text = dr["ReleaseBrand"].ToString();
                    TxtOutdoorAdManufacturer.Text = dr["OutdoorAdManufacturer"].ToString();
                    TxtPowerSupplyManufacturer.Text = dr["PowerSupplyManufacturer"].ToString();
                    TxtRSUManufacturer.Text = dr["RSUManufacturer"].ToString();
                    SetRadio("PlaceMode", dr["PlaceMode"].ToString());
                    SetRadio("BuildState", dr["BuildState"].ToString());
                    SetIsRadio("IsStateOwned", dr["IsStateOwned"].ToString());
                    SetIsRadio("IsIntelligence", dr["IsIntelligence"].ToString());
                    SetRadio("PhotoType", dr["PhotoType"].ToString());
                    SetRadio("RoadType", dr["RoadType"].ToString());
                    SetRadio("PrivateRoadType", dr["PrivateRoadType"].ToString());
                    SetRadio("NeedImprove", dr["NeedImprove"].ToString());
                    SetRadio("BasicTerrain", dr["BasicTerrain"].ToString());
                    SetRadio("BasicLandCategory", dr["BasicLandCategory"].ToString());
                    SetRadio("Obstacle", dr["Obstacle"].ToString());
                    SetRadio("PlaceNeedImprove", dr["PlaceNeedImprove"].ToString());
                    SetRadio("BSTypeOfBS", dr["BSTypeOfBS"].ToString());
                    SetRadio("BSBuildingUse", dr["BSBuildingUse"].ToString());
                    SetRadio("ScPlanCableLayout", dr["ScPlanCableLayout"].ToString());
                    SetRadio("PowerSource", dr["PowerSource"].ToString());
                    SetRadio("NeedExtraDispose", dr["NeedExtraDispose"].ToString());
                    SetRadio("IronTowerClass", dr["IronTowerClass"].ToString());
                    SetRadio("IronTowerType", dr["IronTowerType"].ToString());
                    SetRadio("LightingSubsystem", dr["LightingSubsystem"].ToString());
                    SetRadio("MonitorSubsystem", dr["MonitorSubsystem"].ToString());
                    SetRadio("SensingSubsystem", dr["SensingSubsystem"].ToString());
                    SetRadio("NetworkSubsystem", dr["NetworkSubsystem"].ToString());
                    SetRadio("OutdoorAdSubsystem", dr["OutdoorAdSubsystem"].ToString());
                    SetRadio("PowerSupplySubsystem", dr["PowerSupplySubsystem"].ToString());
                    SetRadio("RSUSubsystem", dr["RSUSubsystem"].ToString());
                    SetIsRadio("IsUseCrane", dr["IsUseCrane"].ToString());
                    SetIsRadio("IsStairsAvailable", dr["IsStairsAvailable"].ToString());
                    SetIsRadio("IsPassengerElevator", dr["IsPassengerElevator"].ToString());
                    SetIsRadio("IsCargoElevator", dr["IsCargoElevator"].ToString());
                    SetIsRadio("IsGrounded", dr["IsGrounded"].ToString());
                    SetIsRadio("IsPossibleToUse", dr["IsPossibleToUse"].ToString());
                    SetIsRadio("IsMainGroundingSize", dr["IsMainGroundingSize"].ToString());
                    SetIsRadio("IsLightningProtection", dr["IsLightningProtection"].ToString());
                    SetIsRadio("LpIsPossibleToUse", dr["LpIsPossibleToUse"].ToString());
                    SetIsRadio("ScIsContact", dr["ScIsContact"].ToString());
                    SetIsRadio("PowerState", dr["PowerState"].ToString());
                    SetIsRadio("AirconditionState", dr["AirconditionState"].ToString());
                }
                break;
        }
        
    }
    private void BindProvice()
    {
        DataTable aLLProvince = new BasicProvinceService().GetALLProvince();
        this.dropprovince.DataSource = aLLProvince;
        this.dropprovince.DataValueField = "Id";
        this.dropprovince.DataTextField = "Name";
        this.dropprovince.DataBind();
        this.dropprovince.Items.Insert(0, new ListItem("请选择", ""));
        this.dropcity.Items.Add(new ListItem("请选择", ""));
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string strSql;
        var mode = Request.QueryString["mode"];
        string filepath = string.Empty;
        if (!string.IsNullOrEmpty(this.fplLoginLogo.PostedFile.FileName))
        {
            string contentType = this.fplLoginLogo.PostedFile.ContentType;
            if (contentType == "image/bmp" || contentType == "image/gif" || contentType == "image/x-png" || contentType == "image/jpeg")
            {
                string arg_72_0 = this.fplLoginLogo.PostedFile.FileName;
                filepath = base.Server.MapPath("/UploadFiles/UserLogo/"+System.Guid.NewGuid().ToString()+arg_72_0);
                //FileInfo fileInfo = new FileInfo(filepath);
                
                this.fplLoginLogo.SaveAs(filepath);
                //base.RegisterScript("top.ui.show('上传成功')");
            }
            else
            {
                //base.RegisterScript("top.ui.alert('上传的登录页面的logo格式不正确')");
            }
        }
        var hashtable = new Hashtable
        {
            {"Towername",TxtName.Text },{ "PhotoPath",filepath},
            {"Province", TxtProvince.Text}, {"City", TxtCity.Text}, {"Area", TxtArea.Text}, {"Address", TxtAddress.Text},
            {"Liaison", TxtLiaison.Text}, {"Phone", TxtPhone.Text}, {"Email", TxtEmail.Text}, {"PlaceMode", GetRadioText("PlaceMode")},
            {"BuildState", GetRadioText("BuildState")}, {"BuildTime", TxtBuildTime.Text},
            {"IsStateOwned", GetIsRadioValue("IsStateOwned")}, {"IsIntelligence", GetIsRadioValue("IsIntelligence")},
            {"NominalLongitude", TxtNominalLongitude.Text}, {"NominalDimension", TxtNominalDimension.Text},
            {"GPSLongitude", TxtGPSLongitude.Text}, {"GPSDimension", TxtGPSDimension.Text},
            {"MapLongitude", TxtMapLongitude.Text}, {"MapDimension", TxtMapDimension.Text}, {"MapDatum", TxtMapDatum.Text},
            {"PhotoType", GetRadioText("PhotoType")}, {"HorizontalPosition", TxtHorizontalPosition.Text},
            {"ShootingPosition", TxtShootingPosition.Text}, {"Altitude", TxtAltitude.Text}, {"MapPosition", TxtMapPosition.Text},
            {"RoadDistance", TxtRoadDistance.Text}, {"RoadType", GetRadioText("RoadType")}, {"PrivateRoadLength", TxtPrivateRoadLength.Text},
            {"PrivateRoadType", GetRadioText("PrivateRoadType")}, {"NeedImprove", GetRadioText("NeedImprove")},
            {"BasicTerrain", GetRadioText("BasicTerrain")},
            {"BasicLandCategory", GetRadioText("BasicLandCategory")}, {"Obstacle", GetRadioText("Obstacle")},
            {"PlaceNeedImprove", GetRadioText("PlaceNeedImprove")},
            {"BSHeight", TxtBSHeight.Text}, {"BSLayersNum", TxtBSLayersNum.Text}, {"BSType", TxtBSType.Text}, {"BSYears", TxtBSYears.Text},
            {"BSTypeOfBS", GetRadioText("BSTypeOfBS")}, {"BSBuildingUse", GetRadioText("BSBuildingUse")},
            {"IsUseCrane", GetIsRadioValue("IsUseCrane")},
            {"IsStairsAvailable", GetIsRadioValue("IsStairsAvailable")}, {"SpecificationWidth", TxtSpecificationWidth.Text},
            {"SpecificationHeight", TxtSpecificationHeight.Text}, {"IsPassengerElevator", GetIsRadioValue("IsPassengerElevator")},
            {"PeLoadingCapacity", TxtPeLoadingCapacity.Text}, {"IsCargoElevator", GetIsRadioValue("IsCargoElevator")},
            {"CeLoadingCapacity", TxtCeLoadingCapacity.Text}, {"CeSpecificationWidth", TxtCeSpecificationWidth.Text},
            {"CeSpecificationHeight", TxtCeSpecificationHeight.Text}, {"MeasureHeight", TxtMeasureHeight.Text},
            {"UniversalLadderWidth", TxtUniversalLadderWidth.Text}, {"UniversalLadderHeight", TxtUniversalLadderHeight.Text},
            {"IsGrounded", GetIsRadioValue("IsGrounded")}, {"IsPossibleToUse", GetIsRadioValue("IsPossibleToUse")},
            {"IsMainGroundingSize", GetIsRadioValue("IsMainGroundingSize")},
            {"IsLightningProtection", GetIsRadioValue("IsLightningProtection")}, {"LpIsPossibleToUse", GetIsRadioValue("LpIsPossibleToUse")},
            {"ScIsContact", GetIsRadioValue("ScIsContact")}, {"ScPlanCableLayout", GetRadioText("ScPlanCableLayout")},
            {"CurrentAvailablePalletNum", TxtCurrentAvailablePalletNum.Text}, {"LastAvailablePalletNum", TxtLastAvailablePalletNum.Text},
            {"CurrentCableFeederNum", TxtCurrentCableFeederNum.Text}, {"LastCableFeederNum", TxtLastCableFeederNum.Text},
            {"FeederType", TxtFeederType.Text}, {"PowerSource", GetRadioText("PowerSource")},
            {"NeedExtraDispose", GetRadioText("NeedExtraDispose")},
            {"PowerModel", TxtPowerModel.Text}, {"PowerSn", TxtPowerSn.Text}, {"PowerState", GetIsRadioValue("PowerState")},
            {"AirconditionOperatorName", TxtAirconditionOperatorName.Text}, {"AirconditionModel", TxtAirconditionModel.Text},
            {"AirconditionBrand", TxtAirconditionBrand.Text}, {"AirconditionSn", TxtAirconditionSn.Text},
            {"AirconditionCapacity", TxtAirconditionCapacity.Text}, {"AirconditionState", GetIsRadioValue("AirconditionState")},
            {"IronTowerHeight", TxtIronTowerHeight.Text}, {"IronTowerClass", GetRadioText("IronTowerClass")},
            {"IronTowerType", GetRadioText("IronTowerType")},
            {"IronTowerLocation", TxtIronTowerLocation.Text}, {"IronTowerManufacturer", TxtIronTowerManufacturer.Text},
            {"LightingSubsystem", GetRadioText("LightingSubsystem")}, {"LightingManufacturer", TxtLightingManufacturer.Text},
            {"MonitorSubsystem", GetRadioText("MonitorSubsystem")}, {"MonitorManufacturer", TxtMonitorManufacturer.Text},
            {"SensingSubsystem", GetRadioText("SensingSubsystem")}, {"SensingManufacturer", TxtSensingManufacturer.Text},
            {"NetworkSubsystem", GetRadioText("NetworkSubsystem")}, {"NetworkManufacturer", TxtNetworkManufacturer.Text},
            {"OutdoorAdSubsystem", GetRadioText("OutdoorAdSubsystem")}, {"VisualDistance", TxtVisualDistance.Text},
            {"VisitorsFlowrate", TxtVisitorsFlowrate.Text}, {"MediaSpecification", TxtMediaSpecification.Text},
            {"PublishingIndustry", TxtPublishingIndustry.Text}, {"GroundClearance", TxtGroundClearance.Text},
            {"VehicleFlowrate", TxtVehicleFlowrate.Text}, {"LaunchCycle", TxtLaunchCycle.Text}, {"ReleaseBrand", TxtReleaseBrand.Text},
            {"OutdoorAdManufacturer", TxtOutdoorAdManufacturer.Text}, {"PowerSupplySubsystem", GetRadioText("PowerSupplySubsystem")},
            {"PowerSupplyManufacturer", TxtPowerSupplyManufacturer.Text}, {"RSUSubsystem", GetRadioText("RSUSubsystem")},
            {"RSUManufacturer", TxtRSUManufacturer.Text},
        };
        switch (mode)
        {
            case "1":
                var str = string.Join(",", hashtable.OfType<DictionaryEntry>().Select(_ => _.Key.ToString()));
                strSql = string.Join(",", hashtable.OfType<DictionaryEntry>().Select(_ =>
                {
                    if (_.Value == null || string.IsNullOrEmpty(_.Value.ToString()))
                    {
                        return "NULL";
                    }

                    return _.Value is string ? SqlStringConstructor.GetQuotedString(_.Value.ToString()) : _.Value.ToString();
                }));
                
                publicDbOpClass.ExecuteSQL("Insert Into TowerStationInfo(" + str + ")Values(" + strSql + ")");
                break;
            case "2":
                var oid = Request.QueryString["oid"];
                strSql = string.Join(",", hashtable.OfType<DictionaryEntry>().Select(_ =>
                {
                    if (_.Value == null || string.IsNullOrEmpty(_.Value.ToString()))
                    {
                        return _.Key + "=NULL";
                    }

                    return _.Key + "=" + (_.Value is string ? SqlStringConstructor.GetQuotedString(_.Value.ToString()) : _.Value.ToString());
                }));
                publicDbOpClass.ExecuteNonQuery(CommandType.Text, "Update TowerStationInfo set " + strSql + " WHERE TowerStationGUID=@oid",
                    new[] {new SqlParameter("@oid", oid)});
                break;
        }
        RegisterScript("top.ui.tabSuccess({ parentName: '_TowerStationManage'})");
    }

    /// <summary>
    /// 获取多个选项的单选控件值
    /// </summary>
    /// <param name="groupName">按钮组名</param>
    /// <returns></returns>
    private string GetRadioText(string groupName)
    {
        var c = Form.Controls.OfType<RadioButton>().FirstOrDefault(_ => _.GroupName == groupName && _.Checked);
        return c == null ? string.Empty : c.Text;
    }

    /// <summary>
    /// 获取是否单选控件值
    /// </summary>
    /// <param name="groupName">按钮组名</param>
    /// <returns></returns>
    private string GetIsRadioValue(string groupName)
    {
        foreach (var value in new[] {"0", "1"})
        {
            var c = FindControl(groupName + value) as RadioButton;
            if (c != null)
            {
                return value;
            }
        }
        return string.Empty;
    }
    /// <summary>
    /// 设置多个选项的单选控件值
    /// </summary>
    /// <param name="groupName">按钮组名</param>
    /// <param name="text">文本值</param>
    private void SetRadio(string groupName, string text)
    {
        var c = FindControl(groupName + text.Replace("/", "")) as RadioButton;
        if (c != null)
        {
            c.Checked = true;
        }
    }

    /// <summary>
    /// 设置是否单选控件值
    /// </summary>
    /// <param name="groupName">按钮组名</param>
    /// <param name="value">值</param>
    /// <returns></returns>
    private void SetIsRadio(string groupName, string value)
    {
        if (value != "0" && value != "1")
        {
            return;
        }
        var c = FindControl(groupName + value) as RadioButton;
        if (c != null)
        {
            c.Checked = true;
        }
    }
    
}