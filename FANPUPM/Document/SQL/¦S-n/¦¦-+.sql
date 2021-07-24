----------------slm 20110817   车辆管理新增表
/*==============================================================*/
/* Table: OA_Vehicle_InsuranceAndReview    车辆交强险和年审车                     */
/*==============================================================*/
create table OA_Vehicle_InsuranceAndReview (
   Guid                 uniqueidentifier     not null,
   code                 nvarchar(100)        null,
   Date                 datetime             null,
   Type                 int                  null,
   VehicleCode          uniqueidentifier     null,
   constraint PK_OA_VEHICLE_INSURANCEANDREVI primary key (Guid)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆交强险与车辆年审',
   'user', @CurrentUser, 'table', 'OA_Vehicle_InsuranceAndReview'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Guid',
   'user', @CurrentUser, 'table', 'OA_Vehicle_InsuranceAndReview', 'column', 'Guid'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '单号',
   'user', @CurrentUser, 'table', 'OA_Vehicle_InsuranceAndReview', 'column', 'code'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '时间',
   'user', @CurrentUser, 'table', 'OA_Vehicle_InsuranceAndReview', 'column', 'Date'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '交强险（0）或者是年检（1）',
   'user', @CurrentUser, 'table', 'OA_Vehicle_InsuranceAndReview', 'column', 'Type'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆guid',
   'user', @CurrentUser, 'table', 'OA_Vehicle_InsuranceAndReview', 'column', 'VehicleCode'
go

/*==============================================================*/
/* Table: OA_Vehicle_Main          车辆详细登记表																		                  */
/*==============================================================*/
create table OA_Vehicle_Main (
   guid                 uniqueidentifier     not null,
   VehicleCode          nvarchar(100)        null,
   VehicleName          nvarchar(100)        null,
   VehicleIdentify      nvarchar(200)        null,
   EngineCode           nvarchar(100)        null,
   Specification        nvarchar(max)        null,
   VehicleType          uniqueidentifier     null,
   PurchaseDate         datetime             null,
   OnHouserDate         datetime             null,
   InspectionDate       datetime             null,
   InsuranceDate        datetime             null,
   Address              nvarchar(max)        null,
   Sparekey             nvarchar(100)        null,
   Ability              nvarchar(100)        null,
   Fatfare              decimal(18,2)        null,
   Recordedprice        decimal(18,2)        null,
   ManufactureDate      datetime             null,
   DepreciationRate     decimal(18,2)        null,
   State                uniqueidentifier     null,
   Remark               nvarchar(max)        null,
   Purchaser            nvarchar(100)        null,
   IsShare              int                  null,
   constraint PK_OA_VEHICLE_MAIN primary key (guid)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆基本信息表',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Guid',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'guid'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车牌号码',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'VehicleCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆户主',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'VehicleName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆识别号码',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'VehicleIdentify'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发动机号',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'EngineCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '规格',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Specification'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆类型',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'VehicleType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '购买日期',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'PurchaseDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上户日期',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'OnHouserDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '年检日期',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'InspectionDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '保险日期',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'InsuranceDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '存放地',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Address'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备用钥匙',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Sparekey'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '能力',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Ability'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发票价',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Fatfare'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入账价',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Recordedprice'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '出厂日期',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'ManufactureDate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '月折旧率',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'DepreciationRate'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '使用状态',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'State'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Remark'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '购买人',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'Purchaser'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '入股标示',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Main', 'column', 'IsShare'
go


/*==============================================================*/
/* Table: OA_Vehicle_Reimbursement           司机报销表                   */
/*==============================================================*/
create table OA_Vehicle_Reimbursement (
   Guid                 uniqueidentifier     not null,
   ReimbursementCode    nvarchar(100)        null,
   UserName             nvarchar(50)         null,
   VehicleGuid          uniqueidentifier     null,
   Date                 datetime             null,
   Destination          nvarchar(Max)        null,
   Tolls                decimal(18,2)        null,
   Repairs              decimal(18,2)        null,
   FuelCosts            decimal(18,2)        null,
   MaintenanceCosts     decimal(18,2)        null,
   Remark               nvarchar(Max)        null,
   constraint PK_OA_VEHICLE_REIMBURSEMENT primary key (Guid)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '司机报销表',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Guid',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'Guid'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '司机报销单号',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'ReimbursementCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '司机名称',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'UserName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车牌号',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'VehicleGuid'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '报销时间',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'Date'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '目的地',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'Destination'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '过路费',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'Tolls'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '修理费',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'Repairs'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '油费',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'FuelCosts'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '保养费',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'MaintenanceCosts'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'OA_Vehicle_Reimbursement', 'column', 'Remark'
go

/*==============================================================*/
/* Table: OA_Vehicle_TypeAndState       车辆类型和状态表												                        */
/*==============================================================*/
create table OA_Vehicle_TypeAndState (
   guid                 uniqueidentifier     not null,
   code                 nvarchar(100)        null,
   Name                 nvarchar(100)        null,
   ParentGuid           uniqueidentifier     null,
   Type                 int                  null,
   constraint PK_OA_VEHICLE_TYPEANDSTATE primary key (guid)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '车辆类型及状态',
   'user', @CurrentUser, 'table', 'OA_Vehicle_TypeAndState'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'Guid',
   'user', @CurrentUser, 'table', 'OA_Vehicle_TypeAndState', 'column', 'guid'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '编号',
   'user', @CurrentUser, 'table', 'OA_Vehicle_TypeAndState', 'column', 'code'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '名称',
   'user', @CurrentUser, 'table', 'OA_Vehicle_TypeAndState', 'column', 'Name'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父级Guid',
   'user', @CurrentUser, 'table', 'OA_Vehicle_TypeAndState', 'column', 'ParentGuid'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型(0)or状态(1)',
   'user', @CurrentUser, 'table', 'OA_Vehicle_TypeAndState', 'column', 'Type'
go


------车辆信息添加自增长标识列id slm 20110913
ALTER TABLE OA_Vehicle_Main ADD id int Identity
