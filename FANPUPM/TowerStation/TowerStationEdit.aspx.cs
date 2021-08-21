using cn.justwin.BLL;
using com.jwsoft.pm.data;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.SessionState;
using System.Web.UI.WebControls;

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
        BindProvince();
        BindRadioButtonList();
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
                    TxtName.Text = dr["Name"].ToString();
                    DdlProvince.Text = dr["Province"].ToString();
                    DdlProvince_SelectedIndexChanged(null, null);
                    DdlCity.Text = dr["City"].ToString();
                    DdlCity_SelectedIndexChanged(null, null);
                    DdlArea.Text = dr["Area"].ToString();
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
                    SetRadio(this.PlaceMode, dr["PlaceMode"].ToString());
                    
                    SetRadio(this.BuildState, dr["BuildState"].ToString());
                    SetIsRadio("IsStateOwned", dr["IsStateOwned"].ToString());
                    SetIsRadio("IsIntelligence", dr["IsIntelligence"].ToString());
                    SetIsRadio("IsGatherArea", dr["IsGatherArea"].ToString());
                    SetRadio("PhotoType", dr["PhotoType"].ToString());
                    SetRadio(this.RoadType, dr["RoadType"].ToString());
                    SetRadio(this.PrivateRoadType, dr["PrivateRoadType"].ToString());
                    SetRadio(this.NeedImprove, dr["NeedImprove"].ToString());
                    SetRadio(this.BasicTerrain, dr["BasicTerrain"].ToString());
                    SetRadio(this.BasicLandCategory, dr["BasicLandCategory"].ToString());
                    SetRadio(this.Obstacle, dr["Obstacle"].ToString());
                    SetRadio(this.PlaceNeedImprove, dr["PlaceNeedImprove"].ToString());
                    SetRadio(this.BSTypeOfBS, dr["BSTypeOfBS"].ToString());
                    SetRadio(this.BSBuildingUse, dr["BSBuildingUse"].ToString());
                    SetRadio(this.ScPlanCableLayout, dr["ScPlanCableLayout"].ToString());
                    SetRadio(this.PowerSource, dr["PowerSource"].ToString());
                    SetRadio(this.NeedExtraDispose, dr["NeedExtraDispose"].ToString());
                    SetRadio(this.IronTowerClass, dr["IronTowerClass"].ToString());
                    SetRadio(this.IronTowerType, dr["IronTowerType"].ToString());
                    SetRadio(this.LightingSubsystem, dr["LightingSubsystem"].ToString());
                    SetRadio(this.MonitorSubsystem, dr["MonitorSubsystem"].ToString());
                    SetRadio(this.SensingSubsystem, dr["SensingSubsystem"].ToString());
                    SetRadio(this.NetworkSubsystem, dr["NetworkSubsystem"].ToString());
                    SetRadio(this.OutdoorAdSubsystem, dr["OutdoorAdSubsystem"].ToString());
                    SetRadio(this.PowerSupplySubsystem, dr["PowerSupplySubsystem"].ToString());
                    SetRadio(this.RSUSubsystem, dr["RSUSubsystem"].ToString());
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
                    img360度全景拍摄.Src = dr["Photo"].ToString();
                    img其他.Src = dr["Photo1"].ToString();
                    img地点入口.Src = dr["Photo2"].ToString(); 
                    img查看铁塔.Src = dr["Photo3"].ToString();
                    img电源.Src = dr["Photo4"].ToString();
                    img展示天线负荷.Src = dr["Photo5"].ToString();
                }

                
                break;
        }
        var strComSql = "select distinct NetworkManufacturer value from TowerStationInfo";
        var Comdt = publicDbOpClass.ExecuteDataTable(CommandType.Text, strComSql, null);
        if (Comdt != null && Comdt.Rows.Count > 0)
        {
            var list = Comdt.AsEnumerable().Select(t => t.Field<string>("value")).ToList();
            hidAutocompleteValue.Value = JsonConvert.SerializeObject(list);

        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        string strSql;
        var mode = Request.QueryString["mode"];
        string photo = null, photo1 = null, photo2 = null, photo3 = null, photo4 = null, photo5 = null;
        try
        {
            string path = "/TowerStation/imageUp";
            string sPath = Server.MapPath("/");
            if (!Directory.Exists(sPath + path)) {
                Directory.CreateDirectory(sPath + path);
            }
            if (Fup360度全景拍摄.HasFile)
            {
                photo = path + "/" + Guid.NewGuid() + Path.GetExtension(Fup360度全景拍摄.FileName);
                Fup360度全景拍摄.SaveAs(sPath+photo);
            }
            if (Fup其他.HasFile)
            {
                photo1 = path + "/" + Guid.NewGuid() + Path.GetExtension(Fup其他.FileName);
                Fup其他.SaveAs(sPath + photo1);
            }
            if (Fup地点入口.HasFile)
            {
                photo2 = path + "/" + Guid.NewGuid() + Path.GetExtension(Fup地点入口.FileName);
                Fup地点入口.SaveAs(sPath + photo2);
            }
            if (Fup查看铁塔.HasFile)
            {
                photo3 = path + "/" + Guid.NewGuid() + Path.GetExtension(Fup查看铁塔.FileName);
                Fup查看铁塔.SaveAs(sPath + photo3);
            }
            if (Fup电源.HasFile)
            {
                photo4 = path + "/" + Guid.NewGuid() + Path.GetExtension(Fup电源.FileName);
                Fup电源.SaveAs(sPath + photo4);
            }
            if (Fup展示天线负荷.HasFile)
            {
                photo5 = path + "/" + Guid.NewGuid() + Path.GetExtension(Fup展示天线负荷.FileName);
                Fup展示天线负荷.SaveAs(sPath + photo5);
            }
        }
        catch (Exception)
        {

            throw;
        }
        var hashtable = new Hashtable
        {
            {"Province", DdlProvince.Text}, {"City", DdlCity.Text}, {"Area", DdlArea.Text}, {"Address", TxtAddress.Text},
            {"Liaison", TxtLiaison.Text}, {"Phone", TxtPhone.Text}, {"Email", TxtEmail.Text}, {"PlaceMode", GetRadioText(this.PlaceMode)},
            {"BuildState", GetRadioText(this.BuildState)}, {"BuildTime", TxtBuildTime.Text},
            {"IsStateOwned", GetIsRadioValue("IsStateOwned")}, {"IsIntelligence", GetIsRadioValue("IsIntelligence")},
            {"NominalLongitude", TxtNominalLongitude.Text}, {"NominalDimension", TxtNominalDimension.Text},
            {"GPSLongitude", TxtGPSLongitude.Text}, {"GPSDimension", TxtGPSDimension.Text},
            {"MapLongitude", TxtMapLongitude.Text}, {"MapDimension", TxtMapDimension.Text}, {"MapDatum", TxtMapDatum.Text},
            {"PhotoType", GetRadioText("PhotoType")}, {"HorizontalPosition", TxtHorizontalPosition.Text},
            {"ShootingPosition", TxtShootingPosition.Text}, {"Altitude", TxtAltitude.Text}, {"MapPosition", TxtMapPosition.Text},
            {"RoadDistance", TxtRoadDistance.Text}, {"RoadType", GetRadioText(this.RoadType)}, {"PrivateRoadLength", TxtPrivateRoadLength.Text},
            {"PrivateRoadType", GetRadioText(this.PrivateRoadType)}, {"NeedImprove", GetRadioText(this.NeedImprove)},
            {"BasicTerrain", GetRadioText(this.BasicTerrain)},
            {"BasicLandCategory", GetRadioText(this.BasicLandCategory)}, {"Obstacle", GetRadioText(this.Obstacle)},
            {"PlaceNeedImprove", GetRadioText(this.PlaceNeedImprove)},
            {"BSHeight", TxtBSHeight.Text}, {"BSLayersNum", TxtBSLayersNum.Text}, {"BSType", TxtBSType.Text}, {"BSYears", TxtBSYears.Text},
            {"BSTypeOfBS", GetRadioText(this.BSTypeOfBS)}, {"BSBuildingUse", GetRadioText(this.BSBuildingUse)},
            {"IsUseCrane", GetIsRadioValue("IsUseCrane")},
            {"IsGatherArea", GetIsRadioValue("IsGatherArea")},
            {"IsStairsAvailable", GetIsRadioValue("IsStairsAvailable")}, {"SpecificationWidth", TxtSpecificationWidth.Text},
            {"SpecificationHeight", TxtSpecificationHeight.Text}, {"IsPassengerElevator", GetIsRadioValue("IsPassengerElevator")},
            {"PeLoadingCapacity", TxtPeLoadingCapacity.Text}, {"IsCargoElevator", GetIsRadioValue("IsCargoElevator")},
            {"CeLoadingCapacity", TxtCeLoadingCapacity.Text}, {"CeSpecificationWidth", TxtCeSpecificationWidth.Text},
            {"CeSpecificationHeight", TxtCeSpecificationHeight.Text}, {"MeasureHeight", TxtMeasureHeight.Text},
            {"UniversalLadderWidth", TxtUniversalLadderWidth.Text}, {"UniversalLadderHeight", TxtUniversalLadderHeight.Text},
            {"IsGrounded", GetIsRadioValue("IsGrounded")}, {"IsPossibleToUse", GetIsRadioValue("IsPossibleToUse")},
            {"IsMainGroundingSize", GetIsRadioValue("IsMainGroundingSize")},
            {"IsLightningProtection", GetIsRadioValue("IsLightningProtection")}, {"LpIsPossibleToUse", GetIsRadioValue("LpIsPossibleToUse")},
            {"ScIsContact", GetIsRadioValue("ScIsContact")}, {"ScPlanCableLayout", GetRadioText(this.ScPlanCableLayout)},
            {"CurrentAvailablePalletNum", TxtCurrentAvailablePalletNum.Text}, {"LastAvailablePalletNum", TxtLastAvailablePalletNum.Text},
            {"CurrentCableFeederNum", TxtCurrentCableFeederNum.Text}, {"LastCableFeederNum", TxtLastCableFeederNum.Text},
            {"FeederType", TxtFeederType.Text}, {"PowerSource", GetRadioText(this.PowerSource)},
            {"NeedExtraDispose", GetRadioText(this.NeedExtraDispose)},
            {"PowerModel", TxtPowerModel.Text}, {"PowerSn", TxtPowerSn.Text}, {"PowerState", GetIsRadioValue("PowerState")},
            {"AirconditionOperatorName", TxtAirconditionOperatorName.Text}, {"AirconditionModel", TxtAirconditionModel.Text},
            {"AirconditionBrand", TxtAirconditionBrand.Text}, {"AirconditionSn", TxtAirconditionSn.Text},
            {"AirconditionCapacity", TxtAirconditionCapacity.Text}, {"AirconditionState", GetIsRadioValue("AirconditionState")},
            {"IronTowerHeight", TxtIronTowerHeight.Text}, {"IronTowerClass", GetRadioText(this.IronTowerClass)},
            {"IronTowerType", GetRadioText(this.IronTowerType)},
            {"IronTowerLocation", TxtIronTowerLocation.Text}, {"IronTowerManufacturer", TxtIronTowerManufacturer.Text},
            {"LightingSubsystem", GetRadioText(this.LightingSubsystem)}, {"LightingManufacturer", TxtLightingManufacturer.Text},
            {"MonitorSubsystem", GetRadioText(this.MonitorSubsystem)}, {"MonitorManufacturer", TxtMonitorManufacturer.Text},
            {"SensingSubsystem", GetRadioText(this.SensingSubsystem)}, {"SensingManufacturer", TxtSensingManufacturer.Text},
            {"NetworkSubsystem", GetRadioText(this.NetworkSubsystem)}, {"NetworkManufacturer", TxtNetworkManufacturer.Text},
            {"OutdoorAdSubsystem", GetRadioText(this.OutdoorAdSubsystem)}, {"VisualDistance", TxtVisualDistance.Text},
            {"VisitorsFlowrate", TxtVisitorsFlowrate.Text}, {"MediaSpecification", TxtMediaSpecification.Text},
            {"PublishingIndustry", TxtPublishingIndustry.Text}, {"GroundClearance", TxtGroundClearance.Text},
            {"VehicleFlowrate", TxtVehicleFlowrate.Text}, {"LaunchCycle", TxtLaunchCycle.Text}, {"ReleaseBrand", TxtReleaseBrand.Text},
            {"OutdoorAdManufacturer", TxtOutdoorAdManufacturer.Text}, {"PowerSupplySubsystem", GetRadioText(this.PowerSupplySubsystem)},
            {"PowerSupplyManufacturer", TxtPowerSupplyManufacturer.Text}, {"RSUSubsystem", GetRadioText(this.RSUSubsystem)},
            {"RSUManufacturer", TxtRSUManufacturer.Text},{"Name", TxtName.Text},{"Photo", photo},{"Photo1", photo1},{"Photo2", photo2},{"Photo3", photo3},{"Photo4", photo4},{"Photo5", photo5},
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
                        return _.Key + "=" + (new[] {"Photo", "Photo1", "Photo2", "Photo3", "Photo4", "Photo5",}.Contains(_.Key)
                            ? _.Key.ToString()
                            : "NULL");
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
        DdlCity.Items.Insert(0, new ListItem("请选择", ""));
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
        DdlArea.Items.Insert(0, new ListItem("请选择", ""));
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
        DdlProvince.Items.Insert(0, new ListItem("请选择", ""));
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
    /// 获取多个选项的单选控件值
    /// </summary>
    /// <param name="groupName">按钮组名</param>
    /// <returns></returns>
    private string GetRadioText(RadioButtonList radio)
    {
        if (radio.SelectedItem != null) {
            return radio.SelectedItem.Value;
        }
        return "";
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
            if (c != null && c.Checked)
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
    /// 设置默认选项项
    /// </summary>
    /// <param name="radio"></param>
    /// <param name="text"></param>
    private void SetRadio(RadioButtonList radio, string text)
    {
        if (!string.IsNullOrEmpty(text)) {
            foreach (ListItem item in radio.Items)
            {
                if (item.Value.Equals(text))
                {
                    item.Selected = true;
                    break;
                }
            }
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

    /// <summary>
    /// 初始化2个以上的单选按钮，从字典取数
    /// </summary>
    private void BindRadioButtonList() {
        DataTable codeList = publicDbOpClass.DataTableQuary("SELECT CodeID value,CodeName text,SignCode2 SignCode FROM [XPM_Basic_CodeList]");
        BindRadioButton(this.PlaceMode, codeList, "PlaceMode");
        BindRadioButton(this.BuildState, codeList, "BuildState");
        BindRadioButton(this.BSTypeOfBS, codeList, "BSTypeOfBS");
        BindRadioButton(this.BSBuildingUse, codeList, "BSBuildingUse");
        BindRadioButton(this.RoadType, codeList, "RoadType");
        BindRadioButton(this.PrivateRoadType, codeList, "RoadType");
        BindRadioButton(this.NeedImprove, codeList, "NeedImprove");
        BindRadioButton(this.BasicTerrain, codeList, "BasicTerrain");
        BindRadioButton(this.BasicLandCategory, codeList, "BasicLandCategory");
        BindRadioButton(this.Obstacle, codeList, "Obstacle");
        BindRadioButton(this.PlaceNeedImprove, codeList, "PlaceNeedImprove");
        BindRadioButton(this.ScPlanCableLayout, codeList, "ScPlanCableLayout");
        BindRadioButton(this.IronTowerClass, codeList, "IronTowerClass");
        BindRadioButton(this.LightingSubsystem, codeList, "LightingSubsystem");
        BindRadioButton(this.PowerSource, codeList, "PowerSource");
        BindRadioButton(this.NeedExtraDispose, codeList, "NeedExtraDispose");
        BindRadioButton(this.MonitorSubsystem, codeList, "MonitorSubsystem");
        BindRadioButton(this.SensingSubsystem, codeList, "SensingSubsystem");
        BindRadioButton(this.NetworkSubsystem, codeList, "NetworkSubsystem");
        BindRadioButton(this.OutdoorAdSubsystem, codeList, "OutdoorAdSubsystem");
        BindRadioButton(this.PowerSupplySubsystem, codeList, "PowerSupplySubsystem");
        BindRadioButton(this.RSUSubsystem, codeList, "RSUSubsystem");
    }

    private void BindRadioButton(RadioButtonList radio, DataTable codeList, string rodioName) {
        DataTable dt = codeList.AsEnumerable().Where(p => p.Field<string>("SignCode") == rodioName).CopyToDataTable<DataRow>();
        radio.DataSource = dt;
        radio.DataBind();
    }
}