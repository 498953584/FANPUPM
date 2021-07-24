insert into Res_ResourceType(ResourceTypeId,ParentId,ResourceTypeCode,ResourceTypeName,InputUser,InputDate,IsValid)
(
	select CategoryCode,NULL,CategoryCode,CategoryName,owner,versiontime ,isvalid 
	from [pmtemp].[dbo].[EPM_Res_Category] AS c
	WHERE categoryparentcode = ''
)

insert into Res_ResourceType(ResourceTypeId,ParentId,ResourceTypeCode,ResourceTypeName,InputUser,InputDate,IsValid)
(
	select CategoryCode as id, (SELECT CategoryCode from [EPM_Res_Category] WHERE c.CategoryParentCode = CategoryCode)
		,CategoryCode,CategoryName,owner,versiontime ,isvalid 
	from [pmtemp].[dbo].[EPM_Res_Category] AS c
	WHERE categoryparentcode <> ''
)


--select * from Res_ResourceType
--select * from EPM_Res_Resource where resourcecode='0101001'
--select * from Res_Resource
--select* from epm_res_unit
--select * from EPM_Res_Gauge
--select * from res_unit
--delete from res_unit

insert into res_unit(unitid,code,[name],inputuser,inputdate)
(
select unitid,newid(),unitname,owner,versiontime from [pmtemp].[dbo].[epm_res_unit]
)


insert into  Res_Resource(resourceid,resourcecode,resourcename,resourcetype,specification,unit,inputuser,inputdate,note)
(
select newid()id,resourcecode as iddddd,resourcename,categorycode,specification,
(select top 1 unitid from [pmtemp].[dbo].[EPM_Res_Gauge]  where c.resourcecode=resourcecode)as unitttt
,owner,versiontime,resourceremark from [pmtemp].[dbo].[EPM_Res_Resource] as c
)

--select unitttt  from (
--select newid()id,resourcecode as iddddd,resourcename,categorycode,specification,
--(select unitid from [pmtemp].[dbo].[EPM_Res_Gauge]  where c.resourcecode=resourcecode)as unitttt
--,owner,versiontime,resourceremark from [pmtemp].[dbo].[EPM_Res_Resource] as c) as Total where unitttt not in (select code from Res_unit)
--select * from [pmtemp].[dbo].[epm_res_unit]
--select * from [epm_res_unit]

--select code  from  res_unit where code  not in(select unitid from [pmtemp].[dbo].[epm_res_unit])