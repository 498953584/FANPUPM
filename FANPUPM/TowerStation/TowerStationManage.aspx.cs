using cn.justwin.BLL;
using com.jwsoft.pm.data;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TowerStation_TowerStationManage : NBasePage, IRequiresSessionState
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindProvince();
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
            {"Province", DdlProvince.Text}, {"City", DdlCity.Text}, {"Area", DdlArea.Text}, {"Address", TxtAddress.Text},
            {"Liaison", TxtLiaison.Text}, {"Phone", TxtPhone.Text}, {"Email", TxtEmail.Text}, {"PlaceMode", DdlPlaceMode.SelectedValue},
            {"BuildState", DdlBuildState.SelectedValue}, {"Name",TxtName.Text},
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
        if (!string.IsNullOrEmpty(TxtBuildTimeStart.Text))
        {
            strWhere += " AND BuildTime>='" + TxtBuildTimeStart.Text + "'";
        }
        if (!string.IsNullOrEmpty(TxtBuildTimeEnd.Text))
        {
            strWhere += " AND BuildTime<='" + TxtBuildTimeEnd.Text + "'";
        }

        var strSql = @"
SELECT tsi.TowerStationGUID, p.name + c.name + cou.name AS Province,tsi.Name,
    ISNULL(tsi.MapLongitude, '')
    + CASE WHEN ISNULL(tsi.MapLongitude, '') <> '' AND ISNULL(tsi.MapDimension, '') <> '' THEN '、' ELSE '' END
    + ISNULL(tsi.MapDimension, '') AS MapCoordinates
FROM dbo.TowerStationInfo tsi
     LEFT JOIN dbo.province p ON tsi.Province = p.province_id
     LEFT JOIN dbo.city c ON tsi.City = c.city_id
     LEFT JOIN dbo.country cou ON tsi.Area = cou.country_id";
        if (!string.IsNullOrEmpty(strWhere))
        {
            strSql += " WHERE " + (strWhere.StartsWith(" AND ") ? "1=1" : string.Empty) + strWhere;
        }
        var dataSource = publicDbOpClass.DataTableQuary(strSql);
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

    protected void BtnSearch_Click(object sender, System.EventArgs e)
    {
        AllData_bind();
    }

    /// <summary>
    /// 省选择事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DdlProvince_SelectedIndexChanged(object sender, EventArgs e)
    {
        var sql = @"SELECT  name, city_id, province_id FROM dbo.city WHERE province_id=@province_id";
        SqlParameter[] paras =
        {
            new SqlParameter("@province_id", DdlProvince.SelectedValue)
        };
        var dt = publicDbOpClass.ExecuteDataTable(CommandType.Text, sql, paras);
        DdlCity.DataSource = dt;
        DdlCity.DataTextField = "name";
        DdlCity.DataValueField = "city_id";
        DdlCity.DataBind();
        DdlCity.Items.Insert(0, new ListItem("", ""));
        DdlCity_SelectedIndexChanged(null, null);
    }
    /// <summary>
    /// 市选择事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DdlCity_SelectedIndexChanged(object sender, EventArgs e)
    {
        var sql = @"SELECT  name, country_id, city_id FROM dbo.country WHERE city_id=@city_id";
        SqlParameter[] paras =
        {
            new SqlParameter("@city_id", DdlCity.SelectedValue)
        };
        var dt = publicDbOpClass.ExecuteDataTable(CommandType.Text, sql, paras);
        DdlArea.DataSource = dt;
        DdlArea.DataTextField = "name";
        DdlArea.DataValueField = "country_id";
        DdlArea.DataBind();
        DdlArea.Items.Insert(0, new ListItem("", ""));
    }
    /// <summary>
    /// 省数据绑定
    /// </summary>
    private void BindProvince()
    {
        var strSql = "SELECT	 name, province_id FROM dbo.province";
        var dt = publicDbOpClass.DataTableQuary(strSql);
        DdlProvince.DataSource = dt;
        DdlProvince.DataTextField = "name";
        DdlProvince.DataValueField = "province_id";
        DdlProvince.DataBind();
        DdlProvince.Items.Insert(0, new ListItem("", ""));
    }

}