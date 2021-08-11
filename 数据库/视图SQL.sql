create view V_TowerStation 
as
select a.TowerStationGUID ,a.Name,a.GPSLongitude,a.GPSDimension,a.NetworkManufacturer,a.NetworkSubsystem,b.TowerStationGUID b_TowerStationGUID,b.name b_name,b.GPSLongitude b_GPSLongitude,b.GPSDimension b_GPSDimension,b.NetworkManufacturer b_NetworkManufacturer,b.NetworkSubsystem b_NetworkSubsystem,dbo.fnGetDistance(a.GPSLongitude,a.GPSDimension,b.GPSLongitude,b.GPSDimension)jl from dbo.TowerStationInfo a
left join (select TowerStationGUID,Name,GPSLongitude,GPSDimension,NetworkManufacturer,NetworkSubsystem from dbo.TowerStationInfo )  b on 1=1