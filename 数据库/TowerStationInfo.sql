---------------------------数据字典生成工具(V2.5)--------------------------------
GO
IF NOT EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID('[TowerStationInfo]'))
BEGIN
/*==============================================================*/
/* Table: TowerStationInfo                                              */
/*==============================================================*/
CREATE TABLE [dbo].[TowerStationInfo](
	[TowerStationGUID] uniqueidentifier  DEFAULT(newsequentialid()) NOT NULL  ,
	[Province] nvarchar(50)   ,
	[City] nvarchar(50)   ,
	[Area] nvarchar(50)   ,
	[Address] nvarchar(200)   ,
	[Liaison] nvarchar(50)   ,
	[Phone] nvarchar(50)   ,
	[Email] nvarchar(60)   ,
	[PlaceMode] nvarchar(10)   ,
	[BuildState] nvarchar(20)   ,
	[BuildTime] datetime   ,
	[IsStateOwned] tinyint   ,
	[IsIntelligence] tinyint   ,
	[NominalLongitude] nvarchar(50)   ,
	[NominalDimension] nvarchar(50)   ,
	[GPSLongitude] nvarchar(50)   ,
	[GPSDimension] nvarchar(50)   ,
	[MapLongitude] nvarchar(50)   ,
	[MapDimension] nvarchar(50)   ,
	[MapDatum] nvarchar(50)   ,
	[Photo] nvarchar(max),
	[PhotoType] nvarchar(50)   ,
	[HorizontalPosition] nvarchar(200)   ,
	[ShootingPosition] nvarchar(200)   ,
	[Altitude] nvarchar(50)   ,
	[MapPosition] nvarchar(200)   ,
	[RoadDistance] decimal(18,4)   ,
	[RoadType] nvarchar(50)   ,
	[PrivateRoadLength] decimal(18,4)   ,
	[PrivateRoadType] nvarchar(50)   ,
	[NeedImprove] nvarchar(50)   ,
	[BasicTerrain] nvarchar(50)   ,
	[BasicLandCategory] nvarchar(50)   ,
	[Obstacle] nvarchar(50)   ,
	[PlaceNeedImprove] nvarchar(50)   ,
	[BSHeight] decimal(18,4)   ,
	[BSLayersNum] decimal(18,4)   ,
	[BSType] nvarchar(50)   ,
	[BSYears] decimal(18,4)   ,
	[BSTypeOfBS] nvarchar(50)   ,
	[BSBuildingUse] nvarchar(50)   ,
	[IsUseCrane] tinyint   ,
	[IsStairsAvailable] tinyint   ,
	[SpecificationWidth] decimal(18,4)   ,
	[SpecificationHeight] decimal(18,4)   ,
	[IsPassengerElevator] tinyint   ,
	[PeLoadingCapacity] decimal(18,4)   ,
	[IsCargoElevator] tinyint   ,
	[CeLoadingCapacity] decimal(18,4)   ,
	[CeSpecificationWidth] decimal(18,4)   ,
	[CeSpecificationHeight] decimal(18,4)   ,
	[MeasureHeight] decimal(18,4)   ,
	[UniversalLadderWidth] decimal(18,4)   ,
	[UniversalLadderHeight] decimal(18,4)   ,
	[IsGrounded] tinyint   ,
	[IsPossibleToUse] tinyint   ,
	[IsMainGroundingSize] tinyint   ,
	[IsLightningProtection] tinyint   ,
	[LpIsPossibleToUse] tinyint   ,
	[ScIsContact] tinyint   ,
	[ScPlanCableLayout] nvarchar(50)   ,
	[CurrentAvailablePalletNum] decimal(18,4)   ,
	[LastAvailablePalletNum] decimal(18,4)   ,
	[CurrentCableFeederNum] decimal(18,4)   ,
	[LastCableFeederNum] decimal(18,4)   ,
	[FeederType] nvarchar(100)   ,
	[PowerSource] nvarchar(50)   ,
	[NeedExtraDispose] nvarchar(50)   ,
	[PowerModel] nvarchar(50)   ,
	[PowerSn] nvarchar(50)   ,
	[PowerState] tinyint   ,
	[AirconditionOperatorName] nvarchar(100)   ,
	[AirconditionModel] nvarchar(50)   ,
	[AirconditionBrand] nvarchar(50)   ,
	[AirconditionSn] nvarchar(50)   ,
	[AirconditionCapacity] nvarchar(50)   ,
	[AirconditionState] tinyint   ,
	[IronTowerHeight] decimal(18,4)   ,
	[IronTowerClass] nvarchar(50)   ,
	[IronTowerType] nvarchar(50)   ,
	[IronTowerLocation] decimal(18,4)   ,
	[IronTowerManufacturer] nvarchar(50)   ,
	[LightingSubsystem] nvarchar(50)   ,
	[LightingManufacturer] nvarchar(50)   ,
	[MonitorSubsystem] nvarchar(50)   ,
	[MonitorManufacturer] nvarchar(50)   ,
	[SensingSubsystem] nvarchar(50)   ,
	[SensingManufacturer] nvarchar(50)   ,
	[NetworkSubsystem] nvarchar(50)   ,
	[NetworkManufacturer] nvarchar(50)   ,
	[OutdoorAdSubsystem] nvarchar(50)   ,
	[VisualDistance] nvarchar(50)   ,
	[VisitorsFlowrate] nvarchar(50)   ,
	[MediaSpecification] nvarchar(50)   ,
	[PublishingIndustry] nvarchar(50)   ,
	[GroundClearance] nvarchar(50)   ,
	[VehicleFlowrate] nvarchar(50)   ,
	[LaunchCycle] nvarchar(50)   ,
	[ReleaseBrand] nvarchar(50)   ,
	[OutdoorAdManufacturer] nvarchar(50)   ,
	[PowerSupplySubsystem] nvarchar(50)   ,
	[PowerSupplyManufacturer] nvarchar(50)   ,
	[RSUSubsystem] nvarchar(50)   ,
	[RSUManufacturer] nvarchar(50)   ,
	PRIMARY KEY(TowerStationGUID)
)
	

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', '塔站信息表','user', @CurrentUser, 'table', 'TowerStationInfo'
execute sp_addextendedproperty 'MS_Description',  '主键' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'TowerStationGUID'
execute sp_addextendedproperty 'MS_Description',  '省' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Province'
execute sp_addextendedproperty 'MS_Description',  '市' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'City'
execute sp_addextendedproperty 'MS_Description',  '区' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Area'
execute sp_addextendedproperty 'MS_Description',  '地址' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Address'
execute sp_addextendedproperty 'MS_Description',  '联系人' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Liaison'
execute sp_addextendedproperty 'MS_Description',  '电话号码' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Phone'
execute sp_addextendedproperty 'MS_Description',  '电邮地址' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Email'
execute sp_addextendedproperty 'MS_Description',  '地点获取方式' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PlaceMode'
execute sp_addextendedproperty 'MS_Description',  '建设状态' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BuildState'
execute sp_addextendedproperty 'MS_Description',  '建设时间' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BuildTime'
execute sp_addextendedproperty 'MS_Description',  '是否国有' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsStateOwned'
execute sp_addextendedproperty 'MS_Description',  '是否智能' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsIntelligence'
execute sp_addextendedproperty 'MS_Description',  '标称经度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'NominalLongitude'
execute sp_addextendedproperty 'MS_Description',  '标称维度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'NominalDimension'
execute sp_addextendedproperty 'MS_Description',  'GPS经度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'GPSLongitude'
execute sp_addextendedproperty 'MS_Description',  'GPS维度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'GPSDimension'
execute sp_addextendedproperty 'MS_Description',  '地图经度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MapLongitude'
execute sp_addextendedproperty 'MS_Description',  '地图维度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MapDimension'
execute sp_addextendedproperty 'MS_Description',  '地图基准' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MapDatum'
execute sp_addextendedproperty 'MS_Description',  '照片记录类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PhotoType'
execute sp_addextendedproperty 'MS_Description',  '大致水平位置' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'HorizontalPosition'
execute sp_addextendedproperty 'MS_Description',  '拍摄位置' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'ShootingPosition'
execute sp_addextendedproperty 'MS_Description',  '离拔高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Altitude'
execute sp_addextendedproperty 'MS_Description',  '在地图上标记位置' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MapPosition'
execute sp_addextendedproperty 'MS_Description',  '离公路距离' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'RoadDistance'
execute sp_addextendedproperty 'MS_Description',  '路面类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'RoadType'
execute sp_addextendedproperty 'MS_Description',  '私人道路长度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PrivateRoadLength'
execute sp_addextendedproperty 'MS_Description',  '私人路面类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PrivateRoadType'
execute sp_addextendedproperty 'MS_Description',  '地面需改进的地' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'NeedImprove'
execute sp_addextendedproperty 'MS_Description',  '基本地形' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BasicTerrain'
execute sp_addextendedproperty 'MS_Description',  '土地基本类别' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BasicLandCategory'
execute sp_addextendedproperty 'MS_Description',  '障碍' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'Obstacle'
execute sp_addextendedproperty 'MS_Description',  '地点需改进' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PlaceNeedImprove'
execute sp_addextendedproperty 'MS_Description',  '高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BSHeight'
execute sp_addextendedproperty 'MS_Description',  '层数' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BSLayersNum'
execute sp_addextendedproperty 'MS_Description',  '类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BSType'
execute sp_addextendedproperty 'MS_Description',  '年限' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BSYears'
execute sp_addextendedproperty 'MS_Description',  '建筑结构类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BSTypeOfBS'
execute sp_addextendedproperty 'MS_Description',  '建筑用途' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'BSBuildingUse'
execute sp_addextendedproperty 'MS_Description',  '需使用吊车' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsUseCrane'
execute sp_addextendedproperty 'MS_Description',  '楼梯可用' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsStairsAvailable'
execute sp_addextendedproperty 'MS_Description',  '规格尺寸宽度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'SpecificationWidth'
execute sp_addextendedproperty 'MS_Description',  '规格尺寸高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'SpecificationHeight'
execute sp_addextendedproperty 'MS_Description',  '客用电梯' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsPassengerElevator'
execute sp_addextendedproperty 'MS_Description',  '装载量' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PeLoadingCapacity'
execute sp_addextendedproperty 'MS_Description',  '货物电梯' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsCargoElevator'
execute sp_addextendedproperty 'MS_Description',  '装载量' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'CeLoadingCapacity'
execute sp_addextendedproperty 'MS_Description',  '规格尺寸宽度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'CeSpecificationWidth'
execute sp_addextendedproperty 'MS_Description',  '规格尺寸高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'CeSpecificationHeight'
execute sp_addextendedproperty 'MS_Description',  '衡量高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MeasureHeight'
execute sp_addextendedproperty 'MS_Description',  '通用整梯宽度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'UniversalLadderWidth'
execute sp_addextendedproperty 'MS_Description',  '通用整梯高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'UniversalLadderHeight'
execute sp_addextendedproperty 'MS_Description',  '是否接地' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsGrounded'
execute sp_addextendedproperty 'MS_Description',  '是否有使用的可能' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsPossibleToUse'
execute sp_addextendedproperty 'MS_Description',  '主接地尺寸' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsMainGroundingSize'
execute sp_addextendedproperty 'MS_Description',  '是否防雷' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IsLightningProtection'
execute sp_addextendedproperty 'MS_Description',  '是否有使用的可能' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'LpIsPossibleToUse'
execute sp_addextendedproperty 'MS_Description',  '轴、线缆是否接触' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'ScIsContact'
execute sp_addextendedproperty 'MS_Description',  '计划线缆布局' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'ScPlanCableLayout'
execute sp_addextendedproperty 'MS_Description',  '可用托盘数量这次' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'CurrentAvailablePalletNum'
execute sp_addextendedproperty 'MS_Description',  '可用托盘数量上次' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'LastAvailablePalletNum'
execute sp_addextendedproperty 'MS_Description',  '线缆支线数量这次' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'CurrentCableFeederNum'
execute sp_addextendedproperty 'MS_Description',  '线缆支线数量上次' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'LastCableFeederNum'
execute sp_addextendedproperty 'MS_Description',  '支线类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'FeederType'
execute sp_addextendedproperty 'MS_Description',  '电力来源' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PowerSource'
execute sp_addextendedproperty 'MS_Description',  '需要额外配置' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'NeedExtraDispose'
execute sp_addextendedproperty 'MS_Description',  '其他信息型号' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PowerModel'
execute sp_addextendedproperty 'MS_Description',  '其他信息序列号' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PowerSn'
execute sp_addextendedproperty 'MS_Description',  '其他信息状态' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PowerState'
execute sp_addextendedproperty 'MS_Description',  '运营商名称' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'AirconditionOperatorName'
execute sp_addextendedproperty 'MS_Description',  '空调型号' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'AirconditionModel'
execute sp_addextendedproperty 'MS_Description',  '空调品牌' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'AirconditionBrand'
execute sp_addextendedproperty 'MS_Description',  '空调序列号' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'AirconditionSn'
execute sp_addextendedproperty 'MS_Description',  '空调能力' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'AirconditionCapacity'
execute sp_addextendedproperty 'MS_Description',  '空调状态' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'AirconditionState'
execute sp_addextendedproperty 'MS_Description',  '塔高' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IronTowerHeight'
execute sp_addextendedproperty 'MS_Description',  '铁塔种类' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IronTowerClass'
execute sp_addextendedproperty 'MS_Description',  '铁塔类型' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IronTowerType'
execute sp_addextendedproperty 'MS_Description',  '铁塔位置结构' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IronTowerLocation'
execute sp_addextendedproperty 'MS_Description',  '铁塔制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'IronTowerManufacturer'
execute sp_addextendedproperty 'MS_Description',  '照明子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'LightingSubsystem'
execute sp_addextendedproperty 'MS_Description',  '照明系统制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'LightingManufacturer'
execute sp_addextendedproperty 'MS_Description',  '监控子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MonitorSubsystem'
execute sp_addextendedproperty 'MS_Description',  '监控系统制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MonitorManufacturer'
execute sp_addextendedproperty 'MS_Description',  '传感子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'SensingSubsystem'
execute sp_addextendedproperty 'MS_Description',  '传感系统制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'SensingManufacturer'
execute sp_addextendedproperty 'MS_Description',  '网络服务子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'NetworkSubsystem'
execute sp_addextendedproperty 'MS_Description',  '网络服务系统的制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'NetworkManufacturer'
execute sp_addextendedproperty 'MS_Description',  '户外广告服务子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'OutdoorAdSubsystem'
execute sp_addextendedproperty 'MS_Description',  '可视距离' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'VisualDistance'
execute sp_addextendedproperty 'MS_Description',  '人流量' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'VisitorsFlowrate'
execute sp_addextendedproperty 'MS_Description',  '媒体规格' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'MediaSpecification'
execute sp_addextendedproperty 'MS_Description',  '发布行业' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PublishingIndustry'
execute sp_addextendedproperty 'MS_Description',  '离地高度' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'GroundClearance'
execute sp_addextendedproperty 'MS_Description',  '车流量' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'VehicleFlowrate'
execute sp_addextendedproperty 'MS_Description',  '投放周期' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'LaunchCycle'
execute sp_addextendedproperty 'MS_Description',  '发布品牌' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'ReleaseBrand'
execute sp_addextendedproperty 'MS_Description',  '户外广告服务系统的制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'OutdoorAdManufacturer'
execute sp_addextendedproperty 'MS_Description',  '充电供电子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PowerSupplySubsystem'
execute sp_addextendedproperty 'MS_Description',  '充电供电系统的制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'PowerSupplyManufacturer'
execute sp_addextendedproperty 'MS_Description',  'RSU子系统' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'RSUSubsystem'
execute sp_addextendedproperty 'MS_Description',  'RSU系统的制造商' ,'user', @CurrentUser, 'table', 'TowerStationInfo', 'column', 'RSUManufacturer'

END
GO
