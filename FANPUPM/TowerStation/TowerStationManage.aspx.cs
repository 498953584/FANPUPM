using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using cn.justwin.BLL;
using com.jwsoft.pm.data;

public partial class TowerStation_TowerStationManage : NBasePage, IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            AllData_bind();
        }
    }
    protected void BtnDel_Click(object sender, EventArgs e)
    {
        var btn = sender as LinkButton;
        if (btn == null || string.IsNullOrEmpty(btn.CommandArgument))
        {
            return;
        }

        var strSql = "DELETE FROM dbo.TowerStationInfo WHERE TowerStationGUID='" + btn.CommandArgument.Replace("'", "''") + "'";
        if (publicDbOpClass.ExecSqlString(strSql) > 0)
        {
            Page.ClientScript.RegisterStartupScript(GetType(), "", "alert('删除成功!')", true);
            AllData_bind();
        }
    }
    protected void AllData_bind()
    {
        var hashtable = new Hashtable
        {
            {"Province", TxtProvince.Text}, {"City", TxtCity.Text}, {"Area", TxtArea.Text}, {"Address", TxtAddress.Text},
            {"Liaison", TxtLiaison.Text}, {"Phone", TxtPhone.Text}, {"Email", TxtEmail.Text}, {"PlaceMode", DdlPlaceMode.SelectedValue},
            {"BuildState", DdlBuildState.SelectedValue},// {"BuildTime", TxtBuildTime.Text},
            //{"IsStateOwned", GetIsRadioValue("IsStateOwned")}, {"IsIntelligence", GetIsRadioValue("IsIntelligence")},
            //{"NominalLongitude", TxtNominalLongitude.Text}, {"NominalDimension", TxtNominalDimension.Text},
            //{"GPSLongitude", TxtGPSLongitude.Text}, {"GPSDimension", TxtGPSDimension.Text},
            //{"MapLongitude", TxtMapLongitude.Text}, {"MapDimension", TxtMapDimension.Text}, {"MapDatum", TxtMapDatum.Text},
            //{"PhotoType", GetRadioText("PhotoType")}, {"HorizontalPosition", TxtHorizontalPosition.Text},
            //{"ShootingPosition", TxtShootingPosition.Text}, {"Altitude", TxtAltitude.Text}, {"MapPosition", TxtMapPosition.Text},
            //{"RoadDistance", TxtRoadDistance.Text}, {"RoadType", GetRadioText("RoadType")}, {"PrivateRoadLength", TxtPrivateRoadLength.Text},
            //{"PrivateRoadType", GetRadioText("PrivateRoadType")}, {"NeedImprove", GetRadioText("NeedImprove")},
            //{"BasicTerrain", GetRadioText("BasicTerrain")},
            //{"BasicLandCategory", GetRadioText("BasicLandCategory")}, {"Obstacle", GetRadioText("Obstacle")},
            //{"PlaceNeedImprove", GetRadioText("PlaceNeedImprove")},
            //{"BSHeight", TxtBSHeight.Text}, {"BSLayersNum", TxtBSLayersNum.Text}, {"BSType", TxtBSType.Text}, {"BSYears", TxtBSYears.Text},
            //{"BSTypeOfBS", GetRadioText("BSTypeOfBS")}, {"BSBuildingUse", GetRadioText("BSBuildingUse")},
            //{"IsUseCrane", GetIsRadioValue("IsUseCrane")},
            //{"IsStairsAvailable", GetIsRadioValue("IsStairsAvailable")}, {"SpecificationWidth", TxtSpecificationWidth.Text},
            //{"SpecificationHeight", TxtSpecificationHeight.Text}, {"IsPassengerElevator", GetIsRadioValue("IsPassengerElevator")},
            //{"PeLoadingCapacity", TxtPeLoadingCapacity.Text}, {"IsCargoElevator", GetIsRadioValue("IsCargoElevator")},
            //{"CeLoadingCapacity", TxtCeLoadingCapacity.Text}, {"CeSpecificationWidth", TxtCeSpecificationWidth.Text},
            //{"CeSpecificationHeight", TxtCeSpecificationHeight.Text}, {"MeasureHeight", TxtMeasureHeight.Text},
            //{"UniversalLadderWidth", TxtUniversalLadderWidth.Text}, {"UniversalLadderHeight", TxtUniversalLadderHeight.Text},
            //{"IsGrounded", GetIsRadioValue("IsGrounded")}, {"IsPossibleToUse", GetIsRadioValue("IsPossibleToUse")},
            //{"IsMainGroundingSize", GetIsRadioValue("IsMainGroundingSize")},
            //{"IsLightningProtection", GetIsRadioValue("IsLightningProtection")}, {"LpIsPossibleToUse", GetIsRadioValue("LpIsPossibleToUse")},
            //{"ScIsContact", GetIsRadioValue("ScIsContact")}, {"ScPlanCableLayout", GetRadioText("ScPlanCableLayout")},
            //{"CurrentAvailablePalletNum", TxtCurrentAvailablePalletNum.Text}, {"LastAvailablePalletNum", TxtLastAvailablePalletNum.Text},
            //{"CurrentCableFeederNum", TxtCurrentCableFeederNum.Text}, {"LastCableFeederNum", TxtLastCableFeederNum.Text},
            //{"FeederType", TxtFeederType.Text}, {"PowerSource", GetRadioText("PowerSource")},
            //{"NeedExtraDispose", GetRadioText("NeedExtraDispose")},
            //{"PowerModel", TxtPowerModel.Text}, {"PowerSn", TxtPowerSn.Text}, {"PowerState", GetIsRadioValue("PowerState")},
            //{"AirconditionOperatorName", TxtAirconditionOperatorName.Text}, {"AirconditionModel", TxtAirconditionModel.Text},
            //{"AirconditionBrand", TxtAirconditionBrand.Text}, {"AirconditionSn", TxtAirconditionSn.Text},
            //{"AirconditionCapacity", TxtAirconditionCapacity.Text}, {"AirconditionState", GetIsRadioValue("AirconditionState")},
            //{"IronTowerHeight", TxtIronTowerHeight.Text}, {"IronTowerClass", GetRadioText("IronTowerClass")},
            //{"IronTowerType", GetRadioText("IronTowerType")},
            //{"IronTowerLocation", TxtIronTowerLocation.Text}, {"IronTowerManufacturer", TxtIronTowerManufacturer.Text},
            //{"LightingSubsystem", GetRadioText("LightingSubsystem")}, {"LightingManufacturer", TxtLightingManufacturer.Text},
            //{"MonitorSubsystem", GetRadioText("MonitorSubsystem")}, {"MonitorManufacturer", TxtMonitorManufacturer.Text},
            //{"SensingSubsystem", GetRadioText("SensingSubsystem")}, {"SensingManufacturer", TxtSensingManufacturer.Text},
            //{"NetworkSubsystem", GetRadioText("NetworkSubsystem")}, {"NetworkManufacturer", TxtNetworkManufacturer.Text},
            //{"OutdoorAdSubsystem", GetRadioText("OutdoorAdSubsystem")}, {"VisualDistance", TxtVisualDistance.Text},
            //{"VisitorsFlowrate", TxtVisitorsFlowrate.Text}, {"MediaSpecification", TxtMediaSpecification.Text},
            //{"PublishingIndustry", TxtPublishingIndustry.Text}, {"GroundClearance", TxtGroundClearance.Text},
            //{"VehicleFlowrate", TxtVehicleFlowrate.Text}, {"LaunchCycle", TxtLaunchCycle.Text}, {"ReleaseBrand", TxtReleaseBrand.Text},
            //{"OutdoorAdManufacturer", TxtOutdoorAdManufacturer.Text}, {"PowerSupplySubsystem", GetRadioText("PowerSupplySubsystem")},
            //{"PowerSupplyManufacturer", TxtPowerSupplyManufacturer.Text}, {"RSUSubsystem", GetRadioText("RSUSubsystem")},
            //{"RSUManufacturer", TxtRSUManufacturer.Text},
        };

        var strWhere = string.Join(" AND ", hashtable.OfType<DictionaryEntry>()
            .Where(_ => _.Value != null && !string.IsNullOrEmpty(_.Value.ToString())).Select(_ =>
            {
                if (_.Value is string)
                {
                    return _.Key + " LIKE '%" + _.Value.ToString().Replace("'", "''") + "%'";
                }
                return _.Key + "=" + _.Value;
            }));

        var strSql = @"
SELECT TowerStationGUID,Province,ISNULL(MapLongitude,'') + CASE WHEN ISNULL(MapLongitude,'')<>'' AND ISNULL(MapDimension,'')<>'' THEN '、' ELSE '' END + ISNULL(MapDimension,'') AS MapCoordinates
FROM dbo.TowerStationInfo";
        if (!string.IsNullOrEmpty(strWhere))
        {
            strSql += " WHERE " + strWhere;
        }
        var dataSource = publicDbOpClass.DataTableQuary(strSql);
        //AspNetPager1.RecordCount = 10;
        //AspNetPager1.PageSize = NBasePage.pagesize;
        GvTowerStation.DataSource = dataSource;
        GvTowerStation.DataBind();
    }

    protected void GvTowerStation_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GvTowerStation.PageIndex = e.NewPageIndex;
        AllData_bind();
    }
    protected void AspNetPager1_PageChanged(object sender, System.EventArgs e)
    {
        AllData_bind();
    }

    protected void btnSearch_Click(object sender, System.EventArgs e)
    {
        AllData_bind();
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
        foreach (var value in new[] { "0", "1" })
        {
            var c = FindControl(groupName + value) as RadioButton;
            if (c != null)
            {
                return value;
            }
        }
        return string.Empty;
    }
}