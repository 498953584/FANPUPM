
---------------------------数据字典生成工具(V2.5)--------------------------------
GO
IF NOT EXISTS(SELECT 1 FROM sysobjects WHERE id=OBJECT_ID('[UserJurisdiction]'))
BEGIN
/*==============================================================*/
/* Table: UserJurisdiction                                              */
/*==============================================================*/
CREATE TABLE [dbo].[UserJurisdiction](
	[UserJurisdictionGUID] uniqueidentifier  DEFAULT(NEWSEQUENTIALID()) ,
	[v_yhdm] nvarchar(8)   ,
	[ProvinceId] nvarchar(50)   ,
	[CityId] nvarchar(50)   ,
	[AreaId] nvarchar(50)   )
	

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', '用户管辖区域','user', @CurrentUser, 'table', 'UserJurisdiction'
execute sp_addextendedproperty 'MS_Description',  '主键' ,'user', @CurrentUser, 'table', 'UserJurisdiction', 'column', 'UserJurisdictionGUID'
execute sp_addextendedproperty 'MS_Description',  '用户主键' ,'user', @CurrentUser, 'table', 'UserJurisdiction', 'column', 'v_yhdm'
execute sp_addextendedproperty 'MS_Description',  '省' ,'user', @CurrentUser, 'table', 'UserJurisdiction', 'column', 'ProvinceId'
execute sp_addextendedproperty 'MS_Description',  '市' ,'user', @CurrentUser, 'table', 'UserJurisdiction', 'column', 'CityId'
execute sp_addextendedproperty 'MS_Description',  '区' ,'user', @CurrentUser, 'table', 'UserJurisdiction', 'column', 'AreaId'

END
GO