
--创建设备管理数据表
--删除外键 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ParentId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_EquipmentType DROP CONSTRAINT FK_ParentId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_TypeId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Equipment DROP CONSTRAINT FK_TypeId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_BudOilWearId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Ship_RefuelApply DROP CONSTRAINT FK_BudOilWearId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_SRefule_EquipmentId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Ship_RefuelApply DROP CONSTRAINT FK_SRefule_EquipmentId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RepairPlan_EquipmentId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RepairPlan DROP CONSTRAINT FK_RepairPlan_EquipmentId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RepairPlanId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RepairApply DROP CONSTRAINT FK_RepairPlanId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Repair_EquipmentId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RepairApply DROP CONSTRAINT FK_Repair_EquipmentId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Repair_ApplyId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RepairReport DROP CONSTRAINT FK_Repair_ApplyId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Repair_ReportId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RepairStock DROP CONSTRAINT FK_Repair_ReportId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_EnterId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_OilOut DROP CONSTRAINT FK_EnterId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RequirePlanId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Progress DROP CONSTRAINT FK_RequirePlanId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ProgressId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_ProgressDetail DROP CONSTRAINT FK_ProgressId
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Discard_EquipmentId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Discard DROP CONSTRAINT FK_Discard_EquipmentId
GO


--删除表 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_EquipmentType') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_EquipmentType
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Equipment') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Equipment
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Ship_BudOilWear') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Ship_BudOilWear
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Ship_RefuelApply') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Ship_RefuelApply
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RepairPlan') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RepairPlan
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RepairApply') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RepairApply
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RepairReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RepairReport
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RepairStock') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RepairStock
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_OilEnter') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_OilEnter
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_OilOut') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_OilOut
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RequirePlan') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RequirePlan
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Progress') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Progress
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_ProgressDetail') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_ProgressDetail
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Discard') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Discard
GO


--创建表 
--创建设备类型表
CREATE TABLE Equ_EquipmentType ( 
	Id nvarchar(500) NOT NULL,    --  设备类型ID 
	ParentId nvarchar(500),    --  父节点 
	Name nvarchar(1000) NOT NULL,    --  设备名称 
	No int NOT NULL,    --  序号 
	Code nvarchar(200) NOT NULL,    --  根据父节点的序号和本身的序号自动生成，3位一级 
	Flag nvarchar(50)		--特殊标识
)
GO
--创建设备台账表
CREATE TABLE Equ_Equipment ( 
	Id nvarchar(500) NOT NULL,    --  设备ID 
	EquipmentCode nvarchar(500) NOT NULL,    --  设备编号 
	ResourceId nvarchar(500) NOT NULL,    --  资源Id 
	TypeId nvarchar(500) NOT NULL,    --  设备类型ID 
	Accuracy nvarchar(1000),    --  精度 
	FactoryNumber nvarchar(1000),    --  出厂编号 
	DepreciationRate decimal(18,3) NOT NULL,    --  折旧率 
	FactoryDate datetime,    --  出厂日期 
	PurchaseDate datetime,    --  购置日期 
	PeriodicVertification nvarchar(50),    --  检定周期 
	DurableYear nvarchar(50),    --  耐用年限 
	PurchasePrice decimal(18,3) NOT NULL,    --  原值 
	SupplierId int,    --  制造厂家 
	State int NOT NULL,    --  设备状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建油耗预算表
CREATE TABLE Equ_Ship_BudOilWear ( 
	Id nvarchar(500) NOT NULL,    --  油耗预算Id 
	Code nvarchar(500) NOT NULL,    --  预算编号 
	PrjId nvarchar(500) NOT NULL,    --  项目Id 
	TaskId nvarchar(500) NOT NULL,    --  任务节点 
	ConBudOilWear decimal(18,3) NOT NULL,    --  合同预算油耗 
	Sump decimal(18,3),    --  挖深 
	SoilTexture nvarchar(500),    --  土质 
	DemandShipType nvarchar(500),    --  需求船型 
	IsOutLease bit NOT NULL,    --  是否对外出租 
	StartDate datetime,    --  预计开工时间 
	EndDate datetime,    --  预计完工时间 
	QuotaOilWear decimal(18,3) NOT NULL,    --  定额油耗 
	ConstructionPlace nvarchar(500),    --  施工地点 
	BudQutity decimal(18,3) NOT NULL,    --  预算挖泥方量 
	BudOilWear decimal(18,3) NOT NULL,    --  预算油耗 
	BudOilPrice decimal(18,3) NOT NULL,    --  预算油单价 
	QuotaOilWearQuantiy decimal(18,3) NOT NULL,    --  定额油耗数量 
	BudOilWearQuantity decimal(18,3) NOT NULL,    --  预算油耗数量 
	Note nvarchar(max),    --  备注 
	FlowState int NOT NULL,    --  流程状态 
	InputDate datetime NOT NULL    --  录入时间 
)
GO
--创建加油申请表
CREATE TABLE Equ_Ship_RefuelApply ( 
	Id nvarchar(500) NOT NULL,    --  申请ID 
	PrjId nvarchar(500) NOT NULL,    --  项目ID 
	BudOilWearId nvarchar(500) NOT NULL,    --  油耗预算Id 
	EquipmentId nvarchar(500) NOT NULL,    --  油耗预算 
	ApplyQuantity decimal(18,3) NOT NULL,    --  本次申请加油量 
	StockQuantity decimal(18,3) NOT NULL,    --  现有库存量 
	BudCompleteQuantity decimal(18,3) NOT NULL,    --  预计完成工程量 
	ConstructionDate datetime,    --  施工日期 
	TotalConstructionDates decimal(18,3) NOT NULL,    --  累计加油时间 
	Sump decimal(18,3) NOT NULL,    --  挖深 
	CompletedQuantity decimal(18,3) NOT NULL,    --  开工至今该船累计完成该项目的工程量 
	TotalRefuel decimal(18,3) NOT NULL,    --  开工至今该项目该船累计加油数量 
	ApplyRefuelPlace nvarchar(500) NOT NULL,    --  申请加油地点 
	ApplyRefuelDate datetime,    --  申请加油时间 
	Reason nvarchar(max),    --  原因分析 
	IsEntrustPurchase bit NOT NULL,    --  是否委托采购 
	ApplyMaster nvarchar(50),    --  申请船主 
	OilsType nvarchar(500),    --  油品种类 
	Fueler nvarchar(500),    --  供油船 
	FuelerOwner nvarchar(500),    --  船东 
	LocaleLeader char(8),    --  现场负责人 
	LeaderPhone nvarchar(50),    --  现场负责人电话 
	Note nvarchar(max),    --  备注 
	FlowState int NOT NULL,    --  流程状态 
	InputUser char(8) NOT NULL,    --  录入人 
	InputDate datetime NOT NULL,    --  录入时间 
	LastModifyUser char(8) NOT NULL,    --  最终变更人 
	LastModifyDate datetime NOT NULL    --  最终变更时间 
)
GO
--创建维修计划表
CREATE TABLE Equ_RepairPlan ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500) NOT NULL,    --  设备Id 
	Code nvarchar(500) NOT NULL,    --  维修计划单号 
	ApplyDepId int,    --  申请部门 
	RepairContent nvarchar(500),    --  保养内容 
	RepairStartDate datetime,    --  维修开始时间 
	RepairEndDate datetime,    --  维修结束时间 
	Cost decimal(18,3),    --  预估花费 
	RepairType char(1),    --  维修方式 "0" 自行维修 "1" 委外维修 
	EquipmentType char(1),    --  设备类型: "0" 代表船机设备   "1" 代表陆上设备 
	RmFlag char(1),    --  维保标识：R表示维修，M表示保养 
	FlowState int NOT NULL,    --  流程状态 
	InputUser varchar(8) NOT NULL,    --  录入人 
	InputDate datetime DEFAULT GETDATE() NOT NULL,    --  录入时间 
	Note nvarchar(max)    --  备注 
)
GO
--创建维修申请表
CREATE TABLE Equ_RepairApply ( 
	Id nvarchar(500) NOT NULL,    --  维修申请单Id 
	RepairPlanId nvarchar(500),    --  维修计划Id 
	EquipmentId nvarchar(500) NOT NULL,    --  设备Id 
	Code nvarchar(500) NOT NULL,    --  编号 
	ConstructionPlace nvarchar(500),    --  施工区域 
	DepreciationAmount decimal(18,3) NOT NULL,    --  已折旧金额 
	LastRepairDate datetime,    --  上次维修日期 
	LastRepairContent nvarchar(500),    --  上次维修内容 
	LastRepairCost decimal(18,3) NOT NULL,    --  上次维修费用 
	BudRepairCost decimal(18,3) NOT NULL,    --  本次预计费维修用 
	BudRepairStartDate datetime,    --  本次预计开始时间 
	BudRepairEndDate datetime,    --  本次预计结束时间 
	BudRepareContent nvarchar(500),    --  本次维修内容 
	ApplyDepId int,    --  申请部门 
	ApplyDate datetime,    --  申请日期 
	TaskName nvarchar(500),    --  分项工程名称 
	RepairType char(1) NOT NULL,    --  维修方式 "0" 自行维修 "1" 委外维修 
	EquipmentType char(1) NOT NULL,    --  设备类型: "0" 代表船机设备   "1" 代表陆上设备 
	RmFlag char(1),    --  维保标识：R表示维修，M表示保养 
	RepairReason nvarchar(500),    --  维修原因说明 
	FlowState int NOT NULL,    --  流程状态 
	InputUser varchar(8) NOT NULL,    --  录入人 
	InputDate datetime DEFAULT GETDATE() NOT NULL,    --  录入时间 
	Note nvarchar(max)    --  备注 
)
GO
--创建维修上报表
CREATE TABLE Equ_RepairReport ( 
	Id nvarchar(500) NOT NULL,    --  上报ID 
	ApplyId nvarchar(500) NOT NULL,    --  维修申请ID 
	ReportDate datetime NOT NULL,    --  上报日期 
	FaultDescription nvarchar(500),    --  故障简介 
	RepairContent nvarchar(500),    --  维修内容 
	RepairStartDate datetime,    --  维修开始时间 
	RepairEndDate datetime,    --  维修结束时间 
	Reason nvarchar(max),    --  原因分析 
	OutCompany nvarchar(500),    --  委外维修公司 
	OutDepartment nvarchar(500),    --  委外维修部门 
	ContractId nvarchar(64),    --  委外维修合同 
	OutSubContractor nvarchar(500),    --  委外分包商 
	RepairPerson nvarchar(50),    --  维修人员 
	LaborCost decimal(18,3) NOT NULL,    --  人工费 
	StuffCost decimal(18,3) NOT NULL,    --  材料费 
	RepairCost decimal(18,3) NOT NULL,    --  维修费用 
	Acceptor char(8),    --  验收人 
	EquipmentType char(1) NOT NULL,    --  设备类型: "0" 代表船机设备   "1" 代表陆上设备 
	FlowState int NOT NULL,    --  流程状态 
	InputUser char(8) NOT NULL,    --  录入人 
	InputDate datetime NOT NULL,    --  录入时间 
	Note nvarchar(max)    --  备注 
)
GO
--创建维修所需的配件表
CREATE TABLE Equ_RepairStock ( 
	Id nvarchar(500) NOT NULL,    --  维修零部件ID 
	ReportId nvarchar(500) NOT NULL,    --  维修上报Id 
	ResourceId nvarchar(500) NOT NULL,    --  零部件ID 
	Quantity decimal(18,3) NOT NULL,    --  数量 
	UnitPrice decimal(18,3) NOT NULL,    --  单价 
	CorpId nvarchar(64),    --  供应商 
	ReceivePerson char(8) NOT NULL,    --  领用人 
	ReceiveDate datetime NOT NULL    --  领用时间 
)
GO
--创建陆上设备油耗入库表
CREATE TABLE Equ_OilEnter ( 
	Id nvarchar(200) NOT NULL,    --  Id 
	Code nvarchar(200) NOT NULL,    --  入库编号 
	PrjId nvarchar(200),    --  项目Id 
	ContractId nvarchar(200),    --  采购合同ID 
	PurchaseCode nvarchar(64),    --  采购单编号 
	UnitPrice decimal(18,3),    --  单价 
	Quantity decimal(18,3),    --  数量 
	StoreKeeper varchar(8),    --  库管员 
	SignInUser varchar(8),    --  签收人 
	EnterDate datetime
)
GO
--创建陆上设备油耗出库表
CREATE TABLE Equ_OilOut ( 
	Id nvarchar(200) NOT NULL,    --  Id 
	EnterId nvarchar(200),    --  入库Id 
	PrjId nvarchar(200),    --  项目Id 
	Code nvarchar(200),    --  出库编号 
	EquipmentId nvarchar(200),
	StoreKeeper varchar(8),    --  库管员 
	TaskId nvarchar(200),    --  分部分项 
	HireType nvarchar(50),    --  租用类型：S表示自用，L表示外租 
	HireContractId nvarchar(200),    --  外租合同 
	IsAsupply bit,    --  是否甲供 
	BalanceMode nvarchar(50),    --  结算方式 
	AsupplyContractId nvarchar(200),    --  甲供合同编号 
	SignInUser varchar(8),    --  签收人 
	Quantity decimal(18,3),    --  数量 
	UnitPrice decimal(18,2),    --  单价 
	OutDate datetime,    --  出库时间 
	FlowState int NOT NULL    --  流程状态 
)
GO
--创建设备需求计划表
CREATE TABLE Equ_RequirePlan ( 
	Id nvarchar(500) NOT NULL,
	Code nvarchar(200) NOT NULL,    --  编号 
	PrjId nvarchar(500),    --  项目Id 
	TaskId nvarchar(500),    --  分部分项 
	FlowState int NOT NULL,    --  流程状态 
	InputDate datetime DEFAULT GETDATE() NOT NULL    --  录入时间 
)
GO
--创建工程进度表
CREATE TABLE Equ_Progress ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	RequirePlanId nvarchar(500),    --  需求计划Id 
	Year int NOT NULL,    --  年份 
	Month int NOT NULL,    --  月份 
	InputDate datetime DEFAULT GETDATE() NOT NULL    --  录入时间 
)
GO
--创建工程进度明细表
CREATE TABLE Equ_ProgressDetail ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	ProgressId nvarchar(500),    --  工程进度Id 
	EquipmentTypeId nvarchar(500),    --  设备类型 
	EnterDate datetime,    --  预计进场时间 
	OutDate datetime,    --  预计退场时间 
	EnterArea nvarchar(500),    --  预计进场地点 
	EquipmentSource nvarchar(500),    --  设备来源 
	BudCost decimal(18,3),    --  预算费用 
	Quantity decimal(18,3)    --  数量 
)
GO
--创建设备报废表
CREATE TABLE Equ_Discard ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500) NOT NULL,    --  设备Id 
	AlreadyDepreciations  decimal(18,3) NOT NULL,    --  已提折旧 
	NetWorth decimal(18,3) NOT NULL,    --  资产净值 
	Reason nvarchar(max),    --  报废原因 
	Applicant varchar(8),    --  申请人 
	ApplyDate datetime,    --  申请日期 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO

--创建主键 
ALTER TABLE Equ_EquipmentType ADD CONSTRAINT PK_EquipmentType 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Equipment ADD CONSTRAINT PK_Equ_Equipment 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Ship_BudOilWear ADD CONSTRAINT PK_Equ_BudOilWear 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Ship_RefuelApply ADD CONSTRAINT PK_Equ_RefuelApply 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RepairPlan ADD CONSTRAINT PK_Equ_RepairPlan 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RepairApply ADD CONSTRAINT PK_Equ_RepairApply 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RepairReport ADD CONSTRAINT PK_Equ_RepairReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RepairStock ADD CONSTRAINT PK_Equ_RepairStock 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_OilEnter ADD CONSTRAINT PK_OilIn 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_OilOut ADD CONSTRAINT PK_OilOut 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RequirePlan ADD CONSTRAINT PK_Equ_RequirePlan 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Progress ADD CONSTRAINT PK_Equ_Progress 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_ProgressDetail ADD CONSTRAINT PK_Equ_ProgressDetail 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Discard ADD CONSTRAINT PK_Equ_Discard 
	PRIMARY KEY CLUSTERED (Id)
GO

--  创建外键
ALTER TABLE Equ_EquipmentType ADD CONSTRAINT FK_ParentId 
	FOREIGN KEY (ParentId) REFERENCES Equ_EquipmentType (Id)
GO
ALTER TABLE Equ_Equipment ADD CONSTRAINT FK_TypeId 
	FOREIGN KEY (TypeId) REFERENCES Equ_EquipmentType (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_Ship_RefuelApply ADD CONSTRAINT FK_BudOilWearId 
	FOREIGN KEY (BudOilWearId) REFERENCES Equ_Ship_BudOilWear (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_Ship_RefuelApply ADD CONSTRAINT FK_SRefule_EquipmentId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RepairPlan ADD CONSTRAINT FK_RepairPlan_EquipmentId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
ALTER TABLE Equ_RepairApply ADD CONSTRAINT FK_RepairPlanId 
	FOREIGN KEY (RepairPlanId) REFERENCES Equ_RepairPlan (Id)
GO
ALTER TABLE Equ_RepairApply ADD CONSTRAINT FK_Repair_EquipmentId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RepairReport ADD CONSTRAINT FK_Repair_ApplyId 
	FOREIGN KEY (ApplyId) REFERENCES Equ_RepairApply (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RepairStock ADD CONSTRAINT FK_Repair_ReportId 
	FOREIGN KEY (ReportId) REFERENCES Equ_RepairReport (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_OilOut ADD CONSTRAINT FK_EnterId 
	FOREIGN KEY (EnterId) REFERENCES Equ_OilEnter (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_Progress ADD CONSTRAINT FK_RequirePlanId 
	FOREIGN KEY (RequirePlanId) REFERENCES Equ_RequirePlan (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_ProgressDetail ADD CONSTRAINT FK_ProgressId 
	FOREIGN KEY (ProgressId) REFERENCES Equ_Progress (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_Discard ADD CONSTRAINT FK_Discard_EquipmentId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO

EXEC sp_addextendedproperty 'MS_Description', '设备类型', 'Schema', dbo, 'table', Equ_EquipmentType
GO
EXEC sp_addextendedproperty 'MS_Description', '设备类型ID', 'Schema', dbo, 'table', Equ_EquipmentType, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '父节点', 'Schema', dbo, 'table', Equ_EquipmentType, 'column', ParentId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备名称', 'Schema', dbo, 'table', Equ_EquipmentType, 'column', Name
GO
EXEC sp_addextendedproperty 'MS_Description', '序号', 'Schema', dbo, 'table', Equ_EquipmentType, 'column', No
GO
EXEC sp_addextendedproperty 'MS_Description', '根据父节点的序号和本身的序号自动生成，3位一级', 'Schema', dbo, 'table', Equ_EquipmentType, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '特殊标识', 'Schema', dbo, 'table', Equ_EquipmentType, 'column', Flag
GO
EXEC sp_addextendedproperty 'MS_Description', '设备', 'Schema', dbo, 'table', Equ_Equipment
GO
EXEC sp_addextendedproperty 'MS_Description', '设备ID', 'Schema', dbo, 'table', Equ_Equipment, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备编号', 'Schema', dbo, 'table', Equ_Equipment, 'column', EquipmentCode
GO
EXEC sp_addextendedproperty 'MS_Description', '资源Id', 'Schema', dbo, 'table', Equ_Equipment, 'column', ResourceId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备类型ID', 'Schema', dbo, 'table', Equ_Equipment, 'column', TypeId
GO
EXEC sp_addextendedproperty 'MS_Description', '精度', 'Schema', dbo, 'table', Equ_Equipment, 'column', Accuracy
GO
EXEC sp_addextendedproperty 'MS_Description', '出厂编号', 'Schema', dbo, 'table', Equ_Equipment, 'column', FactoryNumber
GO
EXEC sp_addextendedproperty 'MS_Description', '折旧率', 'Schema', dbo, 'table', Equ_Equipment, 'column', DepreciationRate
GO
EXEC sp_addextendedproperty 'MS_Description', '出厂日期', 'Schema', dbo, 'table', Equ_Equipment, 'column', FactoryDate
GO
EXEC sp_addextendedproperty 'MS_Description', '购置日期', 'Schema', dbo, 'table', Equ_Equipment, 'column', PurchaseDate
GO
EXEC sp_addextendedproperty 'MS_Description', '检定周期', 'Schema', dbo, 'table', Equ_Equipment, 'column', PeriodicVertification
GO
EXEC sp_addextendedproperty 'MS_Description', '耐用年限', 'Schema', dbo, 'table', Equ_Equipment, 'column', DurableYear
GO
EXEC sp_addextendedproperty 'MS_Description', '原值', 'Schema', dbo, 'table', Equ_Equipment, 'column', PurchasePrice
GO
EXEC sp_addextendedproperty 'MS_Description', '制造厂家', 'Schema', dbo, 'table', Equ_Equipment, 'column', SupplierId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备状态', 'Schema', dbo, 'table', Equ_Equipment, 'column', State
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_Equipment, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '油耗预算', 'Schema', dbo, 'table', Equ_Ship_BudOilWear
GO
EXEC sp_addextendedproperty 'MS_Description', '油耗预算Id', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '预算编号', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '任务节点', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '合同预算油耗', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', ConBudOilWear
GO
EXEC sp_addextendedproperty 'MS_Description', '挖深', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', Sump
GO
EXEC sp_addextendedproperty 'MS_Description', '土质', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', SoilTexture
GO
EXEC sp_addextendedproperty 'MS_Description', '需求船型', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', DemandShipType
GO
EXEC sp_addextendedproperty 'MS_Description', '是否对外出租', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', IsOutLease
GO
EXEC sp_addextendedproperty 'MS_Description', '预计开工时间', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '预计完工时间', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '定额油耗', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', QuotaOilWear
GO
EXEC sp_addextendedproperty 'MS_Description', '施工地点', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '预算挖泥方量', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', BudQutity
GO
EXEC sp_addextendedproperty 'MS_Description', '预算油耗', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', BudOilWear
GO
EXEC sp_addextendedproperty 'MS_Description', '预算油单价', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', BudOilPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '定额油耗数量', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', QuotaOilWearQuantiy
GO
EXEC sp_addextendedproperty 'MS_Description', '预算油耗数量', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', BudOilWearQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_Ship_BudOilWear, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', '加油申请', 'Schema', dbo, 'table', Equ_Ship_RefuelApply
GO
EXEC sp_addextendedproperty 'MS_Description', '申请ID', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '项目ID', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '油耗预算Id', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', BudOilWearId
GO
EXEC sp_addextendedproperty 'MS_Description', '油耗预算', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '本次申请加油量', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', ApplyQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '现有库存量', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', StockQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '预计完成工程量', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', BudCompleteQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '累计加油时间', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', TotalConstructionDates
GO
EXEC sp_addextendedproperty 'MS_Description', '挖深', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', Sump
GO
EXEC sp_addextendedproperty 'MS_Description', '开工至今该船累计完成该项目的工程量', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', CompletedQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '开工至今该项目该船累计加油数量', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', TotalRefuel
GO
EXEC sp_addextendedproperty 'MS_Description', '申请加油地点', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', ApplyRefuelPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '申请加油时间', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', ApplyRefuelDate
GO
EXEC sp_addextendedproperty 'MS_Description', '原因分析', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', Reason
GO
EXEC sp_addextendedproperty 'MS_Description', '是否委托采购', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', IsEntrustPurchase
GO
EXEC sp_addextendedproperty 'MS_Description', '申请船主', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', ApplyMaster
GO
EXEC sp_addextendedproperty 'MS_Description', '油品种类', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', OilsType
GO
EXEC sp_addextendedproperty 'MS_Description', '供油船', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', Fueler
GO
EXEC sp_addextendedproperty 'MS_Description', '船东', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', FuelerOwner
GO
EXEC sp_addextendedproperty 'MS_Description', '现场负责人', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', LocaleLeader
GO
EXEC sp_addextendedproperty 'MS_Description', '现场负责人电话', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', LeaderPhone
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入人', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', InputUser
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', '最终变更人', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', LastModifyUser
GO
EXEC sp_addextendedproperty 'MS_Description', '最终变更时间', 'Schema', dbo, 'table', Equ_Ship_RefuelApply, 'column', LastModifyDate
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '维修计划单号', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '申请部门', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', ApplyDepId
GO
EXEC sp_addextendedproperty 'MS_Description', '保养内容', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', RepairContent
GO
EXEC sp_addextendedproperty 'MS_Description', '维修开始时间', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', RepairStartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '维修结束时间', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', RepairEndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '预估花费', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', Cost
GO
EXEC sp_addextendedproperty 'MS_Description', '维修方式 "0" 自行维修 "1" 委外维修', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', RepairType
GO
EXEC sp_addextendedproperty 'MS_Description', '设备类型: "0" 代表船机设备   "1" 代表陆上设备', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', EquipmentType
GO
EXEC sp_addextendedproperty 'MS_Description', '维保标识：R表示维修，M表示保养', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', RmFlag
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入人', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', InputUser
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RepairPlan, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '维修申请单Id', 'Schema', dbo, 'table', Equ_RepairApply, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '维修计划Id', 'Schema', dbo, 'table', Equ_RepairApply, 'column', RepairPlanId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RepairApply, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '编号', 'Schema', dbo, 'table', Equ_RepairApply, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '施工区域', 'Schema', dbo, 'table', Equ_RepairApply, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '已折旧金额', 'Schema', dbo, 'table', Equ_RepairApply, 'column', DepreciationAmount
GO
EXEC sp_addextendedproperty 'MS_Description', '上次维修日期', 'Schema', dbo, 'table', Equ_RepairApply, 'column', LastRepairDate
GO
EXEC sp_addextendedproperty 'MS_Description', '上次维修内容', 'Schema', dbo, 'table', Equ_RepairApply, 'column', LastRepairContent
GO
EXEC sp_addextendedproperty 'MS_Description', '上次维修费用', 'Schema', dbo, 'table', Equ_RepairApply, 'column', LastRepairCost
GO
EXEC sp_addextendedproperty 'MS_Description', '本次预计费维修用', 'Schema', dbo, 'table', Equ_RepairApply, 'column', BudRepairCost
GO
EXEC sp_addextendedproperty 'MS_Description', '本次预计开始时间', 'Schema', dbo, 'table', Equ_RepairApply, 'column', BudRepairStartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '本次预计结束时间', 'Schema', dbo, 'table', Equ_RepairApply, 'column', BudRepairEndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '本次维修内容', 'Schema', dbo, 'table', Equ_RepairApply, 'column', BudRepareContent
GO
EXEC sp_addextendedproperty 'MS_Description', '申请部门', 'Schema', dbo, 'table', Equ_RepairApply, 'column', ApplyDepId
GO
EXEC sp_addextendedproperty 'MS_Description', '申请日期', 'Schema', dbo, 'table', Equ_RepairApply, 'column', ApplyDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分项工程名称', 'Schema', dbo, 'table', Equ_RepairApply, 'column', TaskName
GO
EXEC sp_addextendedproperty 'MS_Description', '维修方式 "0" 自行维修 "1" 委外维修', 'Schema', dbo, 'table', Equ_RepairApply, 'column', RepairType
GO
EXEC sp_addextendedproperty 'MS_Description', '设备类型: "0" 代表船机设备   "1" 代表陆上设备', 'Schema', dbo, 'table', Equ_RepairApply, 'column', EquipmentType
GO
EXEC sp_addextendedproperty 'MS_Description', '维保标识：R表示维修，M表示保养', 'Schema', dbo, 'table', Equ_RepairApply, 'column', RmFlag
GO
EXEC sp_addextendedproperty 'MS_Description', '维修原因说明', 'Schema', dbo, 'table', Equ_RepairApply, 'column', RepairReason
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RepairApply, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入人', 'Schema', dbo, 'table', Equ_RepairApply, 'column', InputUser
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_RepairApply, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RepairApply, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '维修情况上报', 'Schema', dbo, 'table', Equ_RepairReport
GO
EXEC sp_addextendedproperty 'MS_Description', '上报ID', 'Schema', dbo, 'table', Equ_RepairReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '维修申请ID', 'Schema', dbo, 'table', Equ_RepairReport, 'column', ApplyId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RepairReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '故障简介', 'Schema', dbo, 'table', Equ_RepairReport, 'column', FaultDescription
GO
EXEC sp_addextendedproperty 'MS_Description', '维修内容', 'Schema', dbo, 'table', Equ_RepairReport, 'column', RepairContent
GO
EXEC sp_addextendedproperty 'MS_Description', '维修开始时间', 'Schema', dbo, 'table', Equ_RepairReport, 'column', RepairStartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '维修结束时间', 'Schema', dbo, 'table', Equ_RepairReport, 'column', RepairEndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '原因分析', 'Schema', dbo, 'table', Equ_RepairReport, 'column', Reason
GO
EXEC sp_addextendedproperty 'MS_Description', '委外维修公司', 'Schema', dbo, 'table', Equ_RepairReport, 'column', OutCompany
GO
EXEC sp_addextendedproperty 'MS_Description', '委外维修部门', 'Schema', dbo, 'table', Equ_RepairReport, 'column', OutDepartment
GO
EXEC sp_addextendedproperty 'MS_Description', '委外维修合同', 'Schema', dbo, 'table', Equ_RepairReport, 'column', ContractId
GO
EXEC sp_addextendedproperty 'MS_Description', '委外分包商', 'Schema', dbo, 'table', Equ_RepairReport, 'column', OutSubContractor
GO
EXEC sp_addextendedproperty 'MS_Description', '维修人员', 'Schema', dbo, 'table', Equ_RepairReport, 'column', RepairPerson
GO
EXEC sp_addextendedproperty 'MS_Description', '人工费', 'Schema', dbo, 'table', Equ_RepairReport, 'column', LaborCost
GO
EXEC sp_addextendedproperty 'MS_Description', '材料费', 'Schema', dbo, 'table', Equ_RepairReport, 'column', StuffCost
GO
EXEC sp_addextendedproperty 'MS_Description', '维修费用', 'Schema', dbo, 'table', Equ_RepairReport, 'column', RepairCost
GO
EXEC sp_addextendedproperty 'MS_Description', '验收人', 'Schema', dbo, 'table', Equ_RepairReport, 'column', Acceptor
GO
EXEC sp_addextendedproperty 'MS_Description', '设备类型: "0" 代表船机设备   "1" 代表陆上设备', 'Schema', dbo, 'table', Equ_RepairReport, 'column', EquipmentType
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RepairReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入人', 'Schema', dbo, 'table', Equ_RepairReport, 'column', InputUser
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_RepairReport, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RepairReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '维修零部件', 'Schema', dbo, 'table', Equ_RepairStock
GO
EXEC sp_addextendedproperty 'MS_Description', '维修零部件ID', 'Schema', dbo, 'table', Equ_RepairStock, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '维修上报Id', 'Schema', dbo, 'table', Equ_RepairStock, 'column', ReportId
GO
EXEC sp_addextendedproperty 'MS_Description', '零部件ID', 'Schema', dbo, 'table', Equ_RepairStock, 'column', ResourceId
GO
EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Equ_RepairStock, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '单价', 'Schema', dbo, 'table', Equ_RepairStock, 'column', UnitPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '供应商', 'Schema', dbo, 'table', Equ_RepairStock, 'column', CorpId
GO
EXEC sp_addextendedproperty 'MS_Description', '领用人', 'Schema', dbo, 'table', Equ_RepairStock, 'column', ReceivePerson
GO
EXEC sp_addextendedproperty 'MS_Description', '领用时间', 'Schema', dbo, 'table', Equ_RepairStock, 'column', ReceiveDate
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_OilEnter, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '入库编号', 'Schema', dbo, 'table', Equ_OilEnter, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_OilEnter, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '采购合同ID', 'Schema', dbo, 'table', Equ_OilEnter, 'column', ContractId
GO
EXEC sp_addextendedproperty 'MS_Description', '采购单编号', 'Schema', dbo, 'table', Equ_OilEnter, 'column', PurchaseCode
GO
EXEC sp_addextendedproperty 'MS_Description', '单价', 'Schema', dbo, 'table', Equ_OilEnter, 'column', UnitPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Equ_OilEnter, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '库管员', 'Schema', dbo, 'table', Equ_OilEnter, 'column', StoreKeeper
GO
EXEC sp_addextendedproperty 'MS_Description', '签收人', 'Schema', dbo, 'table', Equ_OilEnter, 'column', SignInUser
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_OilOut, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '入库Id', 'Schema', dbo, 'table', Equ_OilOut, 'column', EnterId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_OilOut, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '出库编号', 'Schema', dbo, 'table', Equ_OilOut, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '库管员', 'Schema', dbo, 'table', Equ_OilOut, 'column', StoreKeeper
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_OilOut, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '租用类型：S表示自用，L表示外租', 'Schema', dbo, 'table', Equ_OilOut, 'column', HireType
GO
EXEC sp_addextendedproperty 'MS_Description', '外租合同', 'Schema', dbo, 'table', Equ_OilOut, 'column', HireContractId
GO
EXEC sp_addextendedproperty 'MS_Description', '是否甲供', 'Schema', dbo, 'table', Equ_OilOut, 'column', IsAsupply
GO
EXEC sp_addextendedproperty 'MS_Description', '结算方式', 'Schema', dbo, 'table', Equ_OilOut, 'column', BalanceMode
GO
EXEC sp_addextendedproperty 'MS_Description', '甲供合同编号', 'Schema', dbo, 'table', Equ_OilOut, 'column', AsupplyContractId
GO
EXEC sp_addextendedproperty 'MS_Description', '签收人', 'Schema', dbo, 'table', Equ_OilOut, 'column', SignInUser
GO
EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Equ_OilOut, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '单价', 'Schema', dbo, 'table', Equ_OilOut, 'column', UnitPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '出库时间', 'Schema', dbo, 'table', Equ_OilOut, 'column', OutDate
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_OilOut, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '编号', 'Schema', dbo, 'table', Equ_RequirePlan, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RequirePlan, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RequirePlan, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RequirePlan, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_RequirePlan, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_Progress, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '需求计划Id', 'Schema', dbo, 'table', Equ_Progress, 'column', RequirePlanId
GO
EXEC sp_addextendedproperty 'MS_Description', '年份', 'Schema', dbo, 'table', Equ_Progress, 'column', Year
GO
EXEC sp_addextendedproperty 'MS_Description', '月份', 'Schema', dbo, 'table', Equ_Progress, 'column', Month
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', Equ_Progress, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '工程进度Id', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', ProgressId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备类型', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', EquipmentTypeId
GO
EXEC sp_addextendedproperty 'MS_Description', '预计进场时间', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', EnterDate
GO
EXEC sp_addextendedproperty 'MS_Description', '预计退场时间', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', OutDate
GO
EXEC sp_addextendedproperty 'MS_Description', '预计进场地点', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', EnterArea
GO
EXEC sp_addextendedproperty 'MS_Description', '设备来源', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', EquipmentSource
GO
EXEC sp_addextendedproperty 'MS_Description', '预算费用', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', BudCost
GO
EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Equ_ProgressDetail, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '设备报废', 'Schema', dbo, 'table', Equ_Discard
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_Discard, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_Discard, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '已提折旧', 'Schema', dbo, 'table', Equ_Discard, 'column', AlreadyDepreciations 
GO
EXEC sp_addextendedproperty 'MS_Description', '资产净值', 'Schema', dbo, 'table', Equ_Discard, 'column', NetWorth
GO
EXEC sp_addextendedproperty 'MS_Description', '报废原因', 'Schema', dbo, 'table', Equ_Discard, 'column', Reason
GO
EXEC sp_addextendedproperty 'MS_Description', '申请人', 'Schema', dbo, 'table', Equ_Discard, 'column', Applicant
GO
EXEC sp_addextendedproperty 'MS_Description', '申请日期', 'Schema', dbo, 'table', Equ_Discard, 'column', ApplyDate
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_Discard, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_Discard, 'column', Note
GO

--创建设备管理其他表的SQL					lhy				2013-09-12 16:50
--删除外键
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Deploy_RequirePlanId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_DeployPlan DROP CONSTRAINT FK_Deploy_RequirePlanId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DeployPlan_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_DeployPlan DROP CONSTRAINT FK_DeployPlan_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_AcceptanceDetail_AcceptanceId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_AcceptanceDetail DROP CONSTRAINT FK_AcceptanceDetail_AcceptanceId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Alloc_DeployPlanId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Alloc DROP CONSTRAINT FK_Alloc_DeployPlanId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Lease_DeployPlanId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_Lease DROP CONSTRAINT FK_Lease_DeployPlanId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_DeployPlanId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_PurchaseApply DROP CONSTRAINT FK_DeployPlanId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_PurchaseApplyId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_PurchaseApplyDetail DROP CONSTRAINT FK_PurchaseApplyId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RoadDrain_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadDrainReport DROP CONSTRAINT FK_RoadDrain_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RoadDrillReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadDrillReport DROP CONSTRAINT FK_RoadDrillReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FKRoadDumpReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadDumpReport DROP CONSTRAINT FKRoadDumpReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RoadElseReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadElseReport DROP CONSTRAINT FK_RoadElseReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RoadInterlock_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadInterlockReport DROP CONSTRAINT FK_RoadInterlock_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RoadMixerReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadMixerReport DROP CONSTRAINT FK_RoadMixerReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RoadTeamReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_RoadTeamReport DROP CONSTRAINT FK_RoadTeamReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ShipElseReport_EqutId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_ShipElseReport DROP CONSTRAINT FK_ShipElseReport_EqutId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ShipFlatReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_ShipFlatReport DROP CONSTRAINT FK_ShipFlatReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ShipGrapReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_ShipGrapReport DROP CONSTRAINT FK_ShipGrapReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ShipMudReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_ShipMudReport DROP CONSTRAINT FK_ShipMudReport_EquId
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_ShipTeamReport_EquId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_ShipTeamReport DROP CONSTRAINT FK_ShipTeamReport_EquId
GO

--删除表 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_DeployPlan') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_DeployPlan
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Acceptance') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Acceptance
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_AcceptanceDetail') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_AcceptanceDetail
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Alloc') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Alloc
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_Lease') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_Lease
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_PurchaseApply') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_PurchaseApply
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_PurchaseApplyDetail') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_PurchaseApplyDetail
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadDrainReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadDrainReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadDrillReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadDrillReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadDumpReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadDumpReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadElseReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadElseReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadInterlockReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadInterlockReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadMixerReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadMixerReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_RoadTeamReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_RoadTeamReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_ShipElseReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_ShipElseReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_ShipFlatReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_ShipFlatReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_ShipGrapReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_ShipGrapReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_ShipMudReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_ShipMudReport
GO
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_ShipTeamReport') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_ShipTeamReport
GO

--创建设备调配计划表 
CREATE TABLE Equ_DeployPlan ( 
	Id nvarchar(500) NOT NULL,
	RequirePlanId nvarchar(500) NOT NULL,    --  需求计划Id 
	EquipmentId nvarchar(500) NOT NULL,    --  设备ID 
	Code nvarchar(500) NOT NULL,    --  计划编号 
	PrjId nvarchar(500) NOT NULL,    --  项目Id 
	TaskId nvarchar(500),    --  分项工程 
	EquipmentSource nvarchar(500),    --  设备来源 
	Sump decimal(18,3) NOT NULL,    --  挖深 
	BudQuantity decimal(18,3) NOT NULL,    --  本月预算工程量 
	BudOilWear decimal(18,3) NOT NULL,    --  本月预算油耗 
	EnterDate datetime,    --  进场时间 
	EnterArea nvarchar(500),    --  进场地点 
	OutDate datetime,    --  出场时间 
	OutArea nvarchar(500),    --  出场地点 
	MachineTeam decimal(18,3),    --  台班 
	OutDepId int,    --  调出部门 
	InDepId int,    --  调入部门 
	ApplyDate datetime,    --  提出申请日期 
	ArriveDate datetime,    --  最迟到位日期 
	MaintenanceState nvarchar(50),    --  设备保养状态 
	FlowState int NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建设备验收表
CREATE TABLE Equ_Acceptance ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	PurchaseCode nvarchar(64),    --  采购单编号 
	Acceptor varchar(8),    --  验收人 
	AcceptDate datetime DEFAULT GETDATE() NOT NULL,    --  验收时间 
	FlowState int DEFAULT -1 NOT NULL    --  流程状态 
)
GO
--创建设备验收明细表
CREATE TABLE Equ_AcceptanceDetail ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	AcceptanceId nvarchar(500),    --  验收单Id 
	ResId nvarchar(500),
	TypeId nvarchar(500) NOT NULL,
	Specification nvarchar(500),    --  规格型号 
	ManufacturerId int,    --  生产厂家 
	SupplierId int,    --  供应商 
	Qty int DEFAULT 1 NOT NULL,    --  数量 
	UnitPrice decimal(18,3) DEFAULT 0.0 NOT NULL,    --  单价 
	TechnicalParameter nvarchar(500),    --  技术参数 
	Info nvarchar(max),    --  随机资料 
	Note nvarchar(max)    --  备注 
)
GO
--创建设备调拨表
CREATE TABLE Equ_Alloc ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	DeployPlanId nvarchar(500),    --  调配计划Id 
	EquipmentId nvarchar(500) NOT NULL,    --  设备Id 
	ShipEquChargeMan varchar(8),    --  船机设备部负责人 
	EquState int NOT NULL,
	CalloutDepId int,    --  调出单位 
	CallouEquAdmin varchar(8),    --  调出部门设备管理员 
	CalloutEquChargeMan varchar(8),    --  调出部门负责人 
	CallinDepId int,    --  调入单位 
	CallinEquAdmin varchar(8),    --  调入单位设备管理员 
	CallinEquChargeMan varchar(8),    --  调入单位负责人 
	Receiver varchar(8),    --  接受人 
	AllocDate datetime,    --  调拨日期 
	FlowState int NOT NULL    --  流程状态 
)
GO
--创建设备租赁表
CREATE TABLE Equ_Lease ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	DeployPlanId nvarchar(500),    --  调配计划Id 
	Code nvarchar(500) NOT NULL,    --  租赁单号 
	EquipmentId nvarchar(500) NOT NULL,    --  设备Id 
	LeaseType char(1),    --  租赁方式：A表示承租，B表示出租 
	ACorpId int,    --  租用单位 
	BCorpId int,    --  出租单位 
	StartDate datetime,    --  起租时间 
	EndDate datetime,    --  停租时间 
	Duration nvarchar(50),    --  租用时长 
	ContractId nvarchar(500),    --  租用合同Id 
	Reason nvarchar(max),    --  租用原因 
	Cost decimal(18,3),    --  租用费用 
	ChargeMan varchar(8),    --  负责人 
	Note nvarchar(max),    --  备注 
	FlowState int NOT NULL    --  流程状态 
)
GO
--创建固定资产采购申请表
CREATE TABLE Equ_PurchaseApply ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	DeployPlanId nvarchar(500),    --  调配计划Id 
	ApplyCode nvarchar(500),    --  申请编号 
	Applicant varchar(8) NOT NULL,    --  申请人 
	ApplyDate datetime NOT NULL,    --  申请日期 
	FlowState int NOT NULL    --  流程状态 
)
GO
--创建固定资产采购申请明细表
CREATE TABLE Equ_PurchaseApplyDetail ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	PurchaseApplyId nvarchar(500) NOT NULL,    --  采购申请Id 
	ResId nvarchar(500) NOT NULL,    --  资源Id 
	Quantity int NOT NULL,    --  申请数量 
	UnitPrice decimal(18,3) NOT NULL,    --  预计单价 
	SuggestFactory nvarchar(500),    --  建议厂家 
	RequireReason nvarchar(max),    --  需求原因 
	ArriveDate datetime,    --  计划到货日期 
	ArrivePlace nvarchar(500)    --  到货地点 
)
GO
--创建排水板设备上报表
CREATE TABLE Equ_RoadDrainReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	TaskId nvarchar(500),    --  分部分项 
	SubcontractGroup nvarchar(500),    --  分包队伍 
	ConstructionQty decimal(18,3),    --  施工产量 
	SubcontractChargeMan varchar(8),    --  分包现场负责人 
	OwnerOperator varchar(8),    --  甲方施工员 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建钻孔机上报表
CREATE TABLE Equ_RoadDrillReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	TaskId nvarchar(500),    --  分部分项 
	Location nvarchar(500),    --  位置 
	Uom nvarchar(50),    --  计量单位 
	ConstructPlace nvarchar(500),    --  施工地点 
	DrillCount int DEFAULT 0 NOT NULL,    --  孔数 
	TotalLength decimal(18,3) DEFAULT 0.0 NOT NULL,    --  总长 
	EquipmentStatus nvarchar(500),    --  设备状况 
	MeasureUser varchar(8),    --  计量员 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建自卸车上报表
CREATE TABLE Equ_RoadDumpReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	TaskId nvarchar(500),    --  分部分项 
	WeighbridgeRoom nvarchar(500),    --  地磅房 
	Sn nvarchar(500),    --  流水号 
	TruckNo nvarchar(500),    --  车号 
	CargoNo nvarchar(500),    --  货号 
	GrossWeigh decimal(18,3) DEFAULT 0.0 NOT NULL,    --  毛重 
	BareWeigh decimal(18,3) DEFAULT 0.0 NOT NULL,    --  空重 
	NetWeigh decimal(18,3) DEFAULT 0.0 NOT NULL,    --  净重 
	CubeQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  方数 
	TruckQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  车数 
	WeighbridgeUser varchar(8),    --  过磅员 
	GrossWeighDate datetime,    --  毛重日期 
	GrossWeighTime nvarchar(50),    --  毛重时间 
	DiscardPlace nvarchar(500),    --  抛弃地点 
	StoneDumperId nvarchar(500),    --  抛石船Id 
	Note nvarchar(max),    --  备注 
	FlowState int DEFAULT -1 NOT NULL    --  流程状态 
)
GO
--创建其他船设备上报表
CREATE TABLE Equ_RoadElseReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	TaskId nvarchar(500),    --  分部分项 
	SubcontractGroup nvarchar(500),    --  分包队伍 
	Qty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  产量 
	SubcontractChargeMan varchar(8),    --  分包现场负责人 
	OwnerOperator varchar(8),    --  甲方施工员 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建预制联锁块上报表
CREATE TABLE Equ_RoadInterlockReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	TaskId nvarchar(500),    --  分部分项Id 
	SubcontractGroup nvarchar(500),    --  分包队伍 
	Qty decimal(18,3),    --  产量 
	SubcontractChargeMan varchar(8),    --  分包现场负责人 
	OwnerOperator varchar(8),    --  甲方负责人 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建搅拌车上报表
CREATE TABLE Equ_RoadMixerReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	TaskId nvarchar(500),    --  分部分项 
	Driver varchar(8),    --  司机 
	TruckQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  车数 
	CubeQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  出库方数 
	ExworksUser varchar(8),    --  出厂确认人 
	AffirmCubeQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  现场确认方数 
	Associater nvarchar(20),    --  现场交接人(系统外) 
	ChargeMan varchar(8),    --  现场负责人 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建陆上设备按台班上报表
CREATE TABLE Equ_RoadTeamReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	ReportDate datetime NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	PrjId nvarchar(500),    --  项目Id 
	BudTask nvarchar(500),    --  分部分项 
	Motorcade nvarchar(500),    --  车队 
	Uom nvarchar(500),    --  计量单位 
	TeamTime decimal(18,3) NOT NULL,    --  台班时间 
	TeamQty decimal(18,3) NOT NULL,    --  台班数量 
	ConstructionPlace nvarchar(500),    --  施工地点 
	ConstructionContent nvarchar(500),    --  施工内容 
	EquipmentStatus nvarchar(500),    --  设备状况 
	ConstructionUser varchar(8),    --  现场施工员 
	Note nvarchar(max),    --  备注 
	StartDate datetime,    --  开始时间 
	EndDate datetime,    --  结束时间 
	SubcontractTeam nvarchar(500),    --  分包队伍 
	SubconstractChargeMan nvarchar(50),    --  分包现场负责人 
	OwnerWorker nvarchar(50),    --  甲方施工员 
	Type char(1) NOT NULL,    --  类型：G-挖掘机，L-装载机,E-其他设备 
	FlowState int NOT NULL    --  流程状态 
)
GO
--创建其他船上报表
CREATE TABLE Equ_ShipElseReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	ConstructionPlace nvarchar(500),    --  施工区域 
	StartDate datetime,    --  开始时间 
	EndDate datetime,    --  结束时间 
	ConstructionDuration decimal(10,2) DEFAULT 0.0 NOT NULL,    --  施工时长 
	Qty decimal(10,2) DEFAULT 0.0 NOT NULL,    --  产量 
	BillingUser varchar(8),    --  开单员 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建平板船上报表
CREATE TABLE Equ_ShipFlatReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	PrjId nvarchar(500),    --  项目Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	ConstructionPlace nvarchar(500),    --  施工区域 
	CabinCapacity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  舱容 
	StartDate datetime,    --  开始时间 
	EndDate datetime,    --  结束时间 
	ConstructionDuration decimal(18,3) DEFAULT 0.0,    --  施工时长 
	ShipCount int DEFAULT 1 NOT NULL,    --  船数 
	Quantity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  产量 
	DeductQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  扣方 
	CompleteQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  当次完成方数 
	BillingUser varchar(8),    --  开单员 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建抓斗船上报表
CREATE TABLE Equ_ShipGrapReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	PrjId nvarchar(500),    --  项目Id 
	ConstructionPlace nvarchar(500),    --  项目区域 
	Qty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  产量 
	ConstructionDuration decimal(18,3) DEFAULT 0.0 NOT NULL,    --  施工时长 
	MudShipId nvarchar(500),    --  挖泥船Id 
	CabinCapacity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  舱容 
	StartDate datetime,    --  开始时间 
	EndDate datetime,    --  结束时间 
	MudDuration decimal(18,3) DEFAULT 0.0,    --  装驳时长（小时） 
	DeductQuantity decimal(18,3),    --  扣方 
	MudTotalQuantity decimal(18,3) DEFAULT 0.0,    --  泥驳总方量 
	BillingUser varchar(8),    --  开单员 
	FlowState int DEFAULT -1,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建泥驳船上报表
CREATE TABLE Equ_ShipMudReport ( 
	Id nvarchar(500) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	ReportDate datetime DEFAULT GETDATE() NOT NULL,    --  上报日期 
	ConstructionDate datetime,    --  施工日期 
	PrjId nvarchar(500),    --  项目Id 
	ConstructionPlace nvarchar(500),    --  施工区域 
	CabinCapacity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  舱容 
	StartDate datetime,    --  开始时间 
	EndDate datetime,    --  结束时间 
	ConstructionDuration decimal(18,3) DEFAULT 0.0 NOT NULL,    --  施工时长 
	DeductQuantity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  扣方 
	MudTotalQty decimal(18,3) DEFAULT 0.0 NOT NULL,    --  泥驳总方量 
	BillingUser varchar(8),    --  开单员 
	FlowState int DEFAULT -1 NOT NULL,    --  流程状态 
	Note nvarchar(max)    --  备注 
)
GO
--创建船机按台班上报表
CREATE TABLE Equ_ShipTeamReport ( 
	Id nvarchar(50) NOT NULL,    --  Id 
	EquipmentId nvarchar(500),    --  设备Id 
	ConstructionDate datetime,    --  施工日期 
	ReportDate datetime NOT NULL,    --  上报日期 
	PrjId nvarchar(500),    --  项目Id 
	ConstructionPlace nvarchar(500),    --  施工区域 
	HoldCapacity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  舱容 
	StartDate datetime,    --  开始时间 
	EndDate datetime,    --  完成时间 
	MachineTeamCount decimal(18,3) DEFAULT 0.0 NOT NULL,    --  台班数 
	MachineTeamDuration decimal(18,3) DEFAULT 0.0 NOT NULL,    --  台班时间 
	Quantity decimal(18,3) DEFAULT 0.0 NOT NULL,    --  产量 
	BillingUser varchar(8),    --  开单员 
	Type char(1) NOT NULL,    --  类型：D-耙吸船，E-其他船 
	Note nvarchar(max),    --  备注 
	FlowState int DEFAULT -1 NOT NULL    --  流程状态 
)
GO

--创建主键 
ALTER TABLE Equ_DeployPlan ADD CONSTRAINT PK_Equ_DeploymentPlan 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Acceptance ADD CONSTRAINT PK_Equ_Acceptance 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_AcceptanceDetail ADD CONSTRAINT PK_Equ_AcceptanceDetail 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Alloc ADD CONSTRAINT PK_Equ_Alloc 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_Lease ADD CONSTRAINT PK_Equ_Lease 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_PurchaseApply ADD CONSTRAINT PK_Equ_FixedPurchaseApply 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_PurchaseApplyDetail ADD CONSTRAINT PK_Equ_PurchaseApplyDetail 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadDrainReport ADD CONSTRAINT PK_Equ_RoadDrainRpt 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadDrillReport ADD CONSTRAINT PK_Equ_RoadDrillReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadDumpReport ADD CONSTRAINT PK_Equ_RoadDumpReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadElseReport ADD CONSTRAINT PK_Equ_RoadElseReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadInterlockReport ADD CONSTRAINT PK_Equ_RoadInterlockReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadMixerReport ADD CONSTRAINT PK_Equ_RoadMixerReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_RoadTeamReport ADD CONSTRAINT PK_Equ_RoadTeamReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_ShipElseReport ADD CONSTRAINT PK_Equ_ShipElseReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_ShipFlatReport ADD CONSTRAINT PK_Equ_ShipFlatReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_ShipGrapReport ADD CONSTRAINT PK_Equ_ShipGrapReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_ShipMudReport ADD CONSTRAINT PK_Equ_ShipMudReport 
	PRIMARY KEY CLUSTERED (Id)
GO
ALTER TABLE Equ_ShipTeamReport ADD CONSTRAINT PK_Equ_ShipQuantitiesReport 
	PRIMARY KEY CLUSTERED (Id)
GO

--创建外键
ALTER TABLE Equ_DeployPlan ADD CONSTRAINT FK_Deploy_RequirePlanId 
	FOREIGN KEY (RequirePlanId) REFERENCES Equ_RequirePlan (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_DeployPlan ADD CONSTRAINT FK_DeployPlan_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_AcceptanceDetail ADD CONSTRAINT FK_AcceptanceDetail_AcceptanceId 
	FOREIGN KEY (AcceptanceId) REFERENCES Equ_Acceptance (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_Alloc ADD CONSTRAINT FK_Alloc_DeployPlanId 
	FOREIGN KEY (DeployPlanId) REFERENCES Equ_DeployPlan (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_Lease ADD CONSTRAINT FK_Lease_DeployPlanId 
	FOREIGN KEY (DeployPlanId) REFERENCES Equ_DeployPlan (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_PurchaseApply ADD CONSTRAINT FK_DeployPlanId 
	FOREIGN KEY (DeployPlanId) REFERENCES Equ_DeployPlan (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_PurchaseApplyDetail ADD CONSTRAINT FK_PurchaseApplyId 
	FOREIGN KEY (PurchaseApplyId) REFERENCES Equ_PurchaseApply (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadDrainReport ADD CONSTRAINT FK_RoadDrain_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadDrillReport ADD CONSTRAINT FK_RoadDrillReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadDumpReport ADD CONSTRAINT FKRoadDumpReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadElseReport ADD CONSTRAINT FK_RoadElseReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadInterlockReport ADD CONSTRAINT FK_RoadInterlock_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadMixerReport ADD CONSTRAINT FK_RoadMixerReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_RoadTeamReport ADD CONSTRAINT FK_RoadTeamReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_ShipElseReport ADD CONSTRAINT FK_ShipElseReport_EqutId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_ShipFlatReport ADD CONSTRAINT FK_ShipFlatReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_ShipGrapReport ADD CONSTRAINT FK_ShipGrapReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_ShipMudReport ADD CONSTRAINT FK_ShipMudReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
ALTER TABLE Equ_ShipTeamReport ADD CONSTRAINT FK_ShipTeamReport_EquId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO

--创建注释
EXEC sp_addextendedproperty 'MS_Description', '设备调配计划', 'Schema', dbo, 'table', Equ_DeployPlan
GO
EXEC sp_addextendedproperty 'MS_Description', '需求计划Id', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', RequirePlanId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备ID', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '计划编号', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '分项工程', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备来源', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', EquipmentSource
GO
EXEC sp_addextendedproperty 'MS_Description', '挖深', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', Sump
GO
EXEC sp_addextendedproperty 'MS_Description', '本月预算工程量', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', BudQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '本月预算油耗', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', BudOilWear
GO
EXEC sp_addextendedproperty 'MS_Description', '进场时间', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', EnterDate
GO
EXEC sp_addextendedproperty 'MS_Description', '进场地点', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', EnterArea
GO
EXEC sp_addextendedproperty 'MS_Description', '出场时间', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', OutDate
GO
EXEC sp_addextendedproperty 'MS_Description', '出场地点', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', OutArea
GO
EXEC sp_addextendedproperty 'MS_Description', '台班', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', MachineTeam
GO
EXEC sp_addextendedproperty 'MS_Description', '调出部门', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', OutDepId
GO
EXEC sp_addextendedproperty 'MS_Description', '调入部门', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', InDepId
GO
EXEC sp_addextendedproperty 'MS_Description', '提出申请日期', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', ApplyDate
GO
EXEC sp_addextendedproperty 'MS_Description', '最迟到位日期', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', ArriveDate
GO
EXEC sp_addextendedproperty 'MS_Description', '设备保养状态', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', MaintenanceState
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_DeployPlan, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_Acceptance, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '采购单编号', 'Schema', dbo, 'table', Equ_Acceptance, 'column', PurchaseCode
GO
EXEC sp_addextendedproperty 'MS_Description', '验收人', 'Schema', dbo, 'table', Equ_Acceptance, 'column', Acceptor
GO
EXEC sp_addextendedproperty 'MS_Description', '验收时间', 'Schema', dbo, 'table', Equ_Acceptance, 'column', AcceptDate
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_Acceptance, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '验收单Id', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', AcceptanceId
GO

EXEC sp_addextendedproperty 'MS_Description', '规格型号', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', Specification
GO
EXEC sp_addextendedproperty 'MS_Description', '生产厂家', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', ManufacturerId
GO
EXEC sp_addextendedproperty 'MS_Description', '供应商', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', SupplierId
GO
EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', Qty
GO
EXEC sp_addextendedproperty 'MS_Description', '单价', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', UnitPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '技术参数', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', TechnicalParameter
GO
EXEC sp_addextendedproperty 'MS_Description', '随机资料', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', Info
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_AcceptanceDetail, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_Alloc, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '调配计划Id', 'Schema', dbo, 'table', Equ_Alloc, 'column', DeployPlanId
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_Alloc, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '船机设备部负责人', 'Schema', dbo, 'table', Equ_Alloc, 'column', ShipEquChargeMan
GO

EXEC sp_addextendedproperty 'MS_Description', '调出单位', 'Schema', dbo, 'table', Equ_Alloc, 'column', CalloutDepId
GO
EXEC sp_addextendedproperty 'MS_Description', '调出部门设备管理员', 'Schema', dbo, 'table', Equ_Alloc, 'column', CallouEquAdmin
GO
EXEC sp_addextendedproperty 'MS_Description', '调出部门负责人', 'Schema', dbo, 'table', Equ_Alloc, 'column', CalloutEquChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '调入单位', 'Schema', dbo, 'table', Equ_Alloc, 'column', CallinDepId
GO
EXEC sp_addextendedproperty 'MS_Description', '调入单位设备管理员', 'Schema', dbo, 'table', Equ_Alloc, 'column', CallinEquAdmin
GO
EXEC sp_addextendedproperty 'MS_Description', '调入单位负责人', 'Schema', dbo, 'table', Equ_Alloc, 'column', CallinEquChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '接受人', 'Schema', dbo, 'table', Equ_Alloc, 'column', Receiver
GO
EXEC sp_addextendedproperty 'MS_Description', '调拨日期', 'Schema', dbo, 'table', Equ_Alloc, 'column', AllocDate
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_Alloc, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_Lease, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '调配计划Id', 'Schema', dbo, 'table', Equ_Lease, 'column', DeployPlanId
GO
EXEC sp_addextendedproperty 'MS_Description', '租赁单号', 'Schema', dbo, 'table', Equ_Lease, 'column', Code
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_Lease, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '租赁方式：A表示承租，B表示出租', 'Schema', dbo, 'table', Equ_Lease, 'column', LeaseType
GO
EXEC sp_addextendedproperty 'MS_Description', '租用单位', 'Schema', dbo, 'table', Equ_Lease, 'column', ACorpId
GO
EXEC sp_addextendedproperty 'MS_Description', '出租单位', 'Schema', dbo, 'table', Equ_Lease, 'column', BCorpId
GO
EXEC sp_addextendedproperty 'MS_Description', '起租时间', 'Schema', dbo, 'table', Equ_Lease, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '停租时间', 'Schema', dbo, 'table', Equ_Lease, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '租用时长', 'Schema', dbo, 'table', Equ_Lease, 'column', Duration
GO
EXEC sp_addextendedproperty 'MS_Description', '租用合同Id', 'Schema', dbo, 'table', Equ_Lease, 'column', ContractId
GO
EXEC sp_addextendedproperty 'MS_Description', '租用原因', 'Schema', dbo, 'table', Equ_Lease, 'column', Reason
GO
EXEC sp_addextendedproperty 'MS_Description', '租用费用', 'Schema', dbo, 'table', Equ_Lease, 'column', Cost
GO
EXEC sp_addextendedproperty 'MS_Description', '负责人', 'Schema', dbo, 'table', Equ_Lease, 'column', ChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_Lease, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_Lease, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_PurchaseApply, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '调配计划Id', 'Schema', dbo, 'table', Equ_PurchaseApply, 'column', DeployPlanId
GO
EXEC sp_addextendedproperty 'MS_Description', '申请编号', 'Schema', dbo, 'table', Equ_PurchaseApply, 'column', ApplyCode
GO
EXEC sp_addextendedproperty 'MS_Description', '申请人', 'Schema', dbo, 'table', Equ_PurchaseApply, 'column', Applicant
GO
EXEC sp_addextendedproperty 'MS_Description', '申请日期', 'Schema', dbo, 'table', Equ_PurchaseApply, 'column', ApplyDate
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_PurchaseApply, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '采购申请Id', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', PurchaseApplyId
GO
EXEC sp_addextendedproperty 'MS_Description', '资源Id', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', ResId
GO
EXEC sp_addextendedproperty 'MS_Description', '申请数量', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '预计单价', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', UnitPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '建议厂家', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', SuggestFactory
GO
EXEC sp_addextendedproperty 'MS_Description', '需求原因', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', RequireReason
GO
EXEC sp_addextendedproperty 'MS_Description', '计划到货日期', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', ArriveDate
GO
EXEC sp_addextendedproperty 'MS_Description', '到货地点', 'Schema', dbo, 'table', Equ_PurchaseApplyDetail, 'column', ArrivePlace
GO
EXEC sp_addextendedproperty 'MS_Description', '排水板设备上报', 'Schema', dbo, 'table', Equ_RoadDrainReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '分包队伍', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', SubcontractGroup
GO
EXEC sp_addextendedproperty 'MS_Description', '施工产量', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', ConstructionQty
GO
EXEC sp_addextendedproperty 'MS_Description', '分包现场负责人', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', SubcontractChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '甲方施工员', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', OwnerOperator
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadDrainReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '钻孔机上报', 'Schema', dbo, 'table', Equ_RoadDrillReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '位置', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', Location
GO
EXEC sp_addextendedproperty 'MS_Description', '计量单位', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', Uom
GO
EXEC sp_addextendedproperty 'MS_Description', '施工地点', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', ConstructPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '孔数', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', DrillCount
GO
EXEC sp_addextendedproperty 'MS_Description', '总长', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', TotalLength
GO
EXEC sp_addextendedproperty 'MS_Description', '设备状况', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', EquipmentStatus
GO
EXEC sp_addextendedproperty 'MS_Description', '计量员', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', MeasureUser
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadDrillReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '自卸车上报', 'Schema', dbo, 'table', Equ_RoadDumpReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '地磅房', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', WeighbridgeRoom
GO
EXEC sp_addextendedproperty 'MS_Description', '流水号', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', Sn
GO
EXEC sp_addextendedproperty 'MS_Description', '车号', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', TruckNo
GO
EXEC sp_addextendedproperty 'MS_Description', '货号', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', CargoNo
GO
EXEC sp_addextendedproperty 'MS_Description', '毛重', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', GrossWeigh
GO
EXEC sp_addextendedproperty 'MS_Description', '空重', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', BareWeigh
GO
EXEC sp_addextendedproperty 'MS_Description', '净重', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', NetWeigh
GO
EXEC sp_addextendedproperty 'MS_Description', '方数', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', CubeQty
GO
EXEC sp_addextendedproperty 'MS_Description', '车数', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', TruckQty
GO
EXEC sp_addextendedproperty 'MS_Description', '过磅员', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', WeighbridgeUser
GO
EXEC sp_addextendedproperty 'MS_Description', '毛重日期', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', GrossWeighDate
GO
EXEC sp_addextendedproperty 'MS_Description', '毛重时间', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', GrossWeighTime
GO
EXEC sp_addextendedproperty 'MS_Description', '抛弃地点', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', DiscardPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '抛石船Id', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', StoneDumperId
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadDumpReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '其它设备上报', 'Schema', dbo, 'table', Equ_RoadElseReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '分包队伍', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', SubcontractGroup
GO
EXEC sp_addextendedproperty 'MS_Description', '产量', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', Qty
GO
EXEC sp_addextendedproperty 'MS_Description', '分包现场负责人', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', SubcontractChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '甲方施工员', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', OwnerOperator
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadElseReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '预制联锁块上报', 'Schema', dbo, 'table', Equ_RoadInterlockReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项Id', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '分包队伍', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', SubcontractGroup
GO
EXEC sp_addextendedproperty 'MS_Description', '产量', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', Qty
GO
EXEC sp_addextendedproperty 'MS_Description', '分包现场负责人', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', SubcontractChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '甲方负责人', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', OwnerOperator
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadInterlockReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '搅拌车上报', 'Schema', dbo, 'table', Equ_RoadMixerReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', TaskId
GO
EXEC sp_addextendedproperty 'MS_Description', '司机', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', Driver
GO
EXEC sp_addextendedproperty 'MS_Description', '车数', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', TruckQty
GO
EXEC sp_addextendedproperty 'MS_Description', '出库方数', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', CubeQty
GO
EXEC sp_addextendedproperty 'MS_Description', '出厂确认人', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', ExworksUser
GO
EXEC sp_addextendedproperty 'MS_Description', '现场确认方数', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', AffirmCubeQty
GO
EXEC sp_addextendedproperty 'MS_Description', '现场交接人(系统外)', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', Associater
GO
EXEC sp_addextendedproperty 'MS_Description', '现场负责人', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', ChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadMixerReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '陆上设备按台班上报', 'Schema', dbo, 'table', Equ_RoadTeamReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '分部分项', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', BudTask
GO
EXEC sp_addextendedproperty 'MS_Description', '车队', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', Motorcade
GO
EXEC sp_addextendedproperty 'MS_Description', '计量单位', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', Uom
GO
EXEC sp_addextendedproperty 'MS_Description', '台班时间', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', TeamTime
GO
EXEC sp_addextendedproperty 'MS_Description', '台班数量', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', TeamQty
GO
EXEC sp_addextendedproperty 'MS_Description', '施工地点', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '施工内容', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', ConstructionContent
GO
EXEC sp_addextendedproperty 'MS_Description', '设备状况', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', EquipmentStatus
GO
EXEC sp_addextendedproperty 'MS_Description', '现场施工员', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', ConstructionUser
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '结束时间', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '分包队伍', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', SubcontractTeam
GO
EXEC sp_addextendedproperty 'MS_Description', '分包现场负责人', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', SubconstractChargeMan
GO
EXEC sp_addextendedproperty 'MS_Description', '甲方施工员', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', OwnerWorker
GO
EXEC sp_addextendedproperty 'MS_Description', '类型：G-挖掘机，L-装载机,E-其他设备', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', Type
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_RoadTeamReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '其他船型上报', 'Schema', dbo, 'table', Equ_ShipElseReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工区域', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '结束时间', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工时长', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', ConstructionDuration
GO
EXEC sp_addextendedproperty 'MS_Description', '产量', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', Qty
GO
EXEC sp_addextendedproperty 'MS_Description', '开单员', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', BillingUser
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_ShipElseReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工区域', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '舱容', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', CabinCapacity
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '结束时间', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工时长', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', ConstructionDuration
GO
EXEC sp_addextendedproperty 'MS_Description', '船数', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', ShipCount
GO
EXEC sp_addextendedproperty 'MS_Description', '产量', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '扣方', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', DeductQty
GO
EXEC sp_addextendedproperty 'MS_Description', '当次完成方数', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', CompleteQty
GO
EXEC sp_addextendedproperty 'MS_Description', '开单员', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', BillingUser
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_ShipFlatReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '抓斗船上报', 'Schema', dbo, 'table', Equ_ShipGrapReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '项目区域', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '产量', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', Qty
GO
EXEC sp_addextendedproperty 'MS_Description', '施工时长', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', ConstructionDuration
GO
EXEC sp_addextendedproperty 'MS_Description', '挖泥船Id', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', MudShipId
GO
EXEC sp_addextendedproperty 'MS_Description', '舱容', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', CabinCapacity
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '结束时间', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '装驳时长（小时）', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', MudDuration
GO
EXEC sp_addextendedproperty 'MS_Description', '扣方', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', DeductQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '泥驳总方量', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', MudTotalQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '开单员', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', BillingUser
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_ShipGrapReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '泥驳船上报', 'Schema', dbo, 'table', Equ_ShipMudReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '施工区域', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '舱容', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', CabinCapacity
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '结束时间', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '施工时长', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', ConstructionDuration
GO
EXEC sp_addextendedproperty 'MS_Description', '扣方', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', DeductQuantity
GO
EXEC sp_addextendedproperty 'MS_Description', '泥驳总方量', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', MudTotalQty
GO
EXEC sp_addextendedproperty 'MS_Description', '开单员', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', BillingUser
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_ShipMudReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '船机按台班上报', 'Schema', dbo, 'table', Equ_ShipTeamReport
GO
EXEC sp_addextendedproperty 'MS_Description', 'Id', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备Id', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '施工日期', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', ConstructionDate
GO
EXEC sp_addextendedproperty 'MS_Description', '上报日期', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', ReportDate
GO
EXEC sp_addextendedproperty 'MS_Description', '项目Id', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', PrjId
GO
EXEC sp_addextendedproperty 'MS_Description', '施工区域', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', ConstructionPlace
GO
EXEC sp_addextendedproperty 'MS_Description', '舱容', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', HoldCapacity
GO
EXEC sp_addextendedproperty 'MS_Description', '开始时间', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', StartDate
GO
EXEC sp_addextendedproperty 'MS_Description', '完成时间', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', EndDate
GO
EXEC sp_addextendedproperty 'MS_Description', '台班数', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', MachineTeamCount
GO
EXEC sp_addextendedproperty 'MS_Description', '台班时间', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', MachineTeamDuration
GO
EXEC sp_addextendedproperty 'MS_Description', '产量', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', Quantity
GO
EXEC sp_addextendedproperty 'MS_Description', '开单员', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', BillingUser
GO
EXEC sp_addextendedproperty 'MS_Description', '类型：D-耙吸船，E-其他船', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', Type
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', Note
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Equ_ShipTeamReport, 'column', FlowState
GO

--增加设备人员表			lhy			2013-09-17		17:00
--删除外键
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_EquipmentUserId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Equ_EquipmentUser DROP CONSTRAINT FK_EquipmentUserId
GO
--删除表
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Equ_EquipmentUser') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Equ_EquipmentUser
GO
--创建设备人员表
CREATE TABLE Equ_EquipmentUser ( 
	Id nvarchar(500) NOT NULL,    --  设备人员Id 
	EquipmentId nvarchar(500) NOT NULL,    --  设备ID 
	UserCode varchar(8) NOT NULL    --  人员编号 
)
GO
--创建主键 
ALTER TABLE Equ_EquipmentUser ADD CONSTRAINT PK_Equ_EquipmentUser 
	PRIMARY KEY CLUSTERED (Id)
GO
--创建外键
ALTER TABLE Equ_EquipmentUser ADD CONSTRAINT FK_EquipmentUserId 
	FOREIGN KEY (EquipmentId) REFERENCES Equ_Equipment (Id)
	ON DELETE CASCADE
GO
--添加字段注释
EXEC sp_addextendedproperty 'MS_Description', '设备人员', 'Schema', dbo, 'table', Equ_EquipmentUser
GO
EXEC sp_addextendedproperty 'MS_Description', '设备人员Id', 'Schema', dbo, 'table', Equ_EquipmentUser, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '设备ID', 'Schema', dbo, 'table', Equ_EquipmentUser, 'column', EquipmentId
GO
EXEC sp_addextendedproperty 'MS_Description', '人员编号', 'Schema', dbo, 'table', Equ_EquipmentUser, 'column', UserCode
GO

--在模块表中增加设备管理菜单
INSERT INTO PT_MK VALUES
('26','设备管理','','y',19,'MenuIco/13.gif',24913,6,'0','0','','1')
INSERT INTO PT_MK VALUES
('2601','设备类别','/Equ/Type/EquipmentTypeList.aspx','y',1,'',24914,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('2602','设备台账','','y',2,'',24915,2,'0','0','','1')
INSERT INTO PT_MK VALUES
('260201','台账维护','/Equ/Equipment/EquipmentList.aspx?action=edit','y',1,'',24916,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('260202','台账查询','/Equ/Equipment/EquipmentList.aspx?action=Query','y',2,'',24920,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('2603','船机设备','','y',3,'',24921,4,'0','0','','1')
INSERT INTO PT_MK VALUES
('260301','船机油耗管理','','y',1,'',24922,2,'0','0','','1')
INSERT INTO PT_MK VALUES
('26030101','油耗预算管理','/Equ/ShipOilWear/BudOilWearList.aspx','y',1,'',24923,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('26030102','加油申请','/Equ/ShipOilWear/RefuelApplyList.aspx','y',1,'',24926,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('260305','船机维修管理','','y',5,'',24933,3,'0','0','','1')
INSERT INTO PT_MK VALUES
('26030501','维修申请','/Equ/Repair/RepairApplyList.aspx?equipmentType=0','y',2,'',24934,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('26030502','维修上报','/Equ/Repair/RepairReportList.aspx?equipmentType=0','y',3,'',24939,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('26030503','维修计划','/Equ/Repair/RepairPlanList.aspx?equipmentType=0','y',1,'',24940,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('2604','陆上设备','','y',4,'',24935,2,'0','0','','1')
INSERT INTO PT_MK VALUES
('260401','油耗管理','','y',0,'',24936,2,'0','0','','1')
INSERT INTO PT_MK VALUES
('26040101','入库登记','/Equ/OilWearManage/EquOilEnter.aspx','y',1,'',24937,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('26040102','出库登记','/Equ/OilWearManage/EquOilOut.aspx','y',2,'',24938,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('260402','设备维修管理','','y',2,'',24949,3,'0','0','','1')
INSERT INTO PT_MK VALUES
('26040201','维修计划','/Equ/Repair/RepairPlanList.aspx?equipmentType=1','y',1,'',24950,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('26040202','维修申请','/Equ/Repair/RepairApplyList.aspx?equipmentType=1','y',2,'',24951,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('26040203','维修上报','/Equ/Repair/RepairReportList.aspx?equipmentType=1','y',3,'',24952,0,'0','0','','1')
GO
INSERT INTO PT_MK VALUES
('2605','设备需求调配管理','','y',5,'',24941,2,'0','0','','1')
INSERT INTO PT_MK VALUES
('260501','设备需求计划','/Equ/RequirePlan/RequirePlanList.aspx','y',1,'',24942,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('2606','设备报废','/Equ/EquipmentDiscard/EquDiscardList.aspx','y',6,'',24946,0,'0','0','','1')
--模块表中增加设备管理菜单
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260302','产量管理','','y','2','','24924','5','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26030201','抓斗船上报','/Equ/ShipGrapReport/ShipGrapReportList.aspx','y','1','','24925','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26030202','按台班上报','/Equ/ShipTeamReport/ShipTeamReportList.aspx','y','5','','24947','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26030203','泥驳船上报','/Equ/ShipMudReport/ShipMudReportList.aspx','y','2','','24957','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26030204','平板船上报','/Equ/ShipFlatReport/ShipFlatReportList.aspx','y','3','','24959','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26030205','其他船上报','/Equ/ShipElseReport/ShipElseReportList.aspx','y','4','','24960','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260403','产量管理','','y','3','','24955','7','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040301','按台班上报','/Equ/RoadTeamReport/RoadTeamReportList.aspx','y','7','','24956','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040302','自卸车上报','/Equ/RoadDumpReport/RoadDumpReportList.aspx','y','1','','24961','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040303','钻孔机上报','/Equ/RoadDrillReport/RoadDrillReportList.aspx','y','2','','24962','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040304','搅拌车上报','/Equ/RoadMixerReport/RoadMixerReportList.aspx','y','3','','24964','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040305','排水板设备上报','/Equ/RoadDrainReport/RoadDrainReportList.aspx','y','4','','24965','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040306','预制联锁块上报','/Equ/RoadInterlockReport/RoadInterlockReportList.aspx','y','5','','24966','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('26040307','其他设备上报','/Equ/RoadElseReport/RoadElseReportList.aspx','y','6','','24967','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260502','设备调配计划','/Equ/EquDeployPlan/EquDeployPlanList.aspx','y','2','','24953','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260503','设备租赁','/Equ/EquLease/EquLeaseList.aspx','y','3','','24954','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260504','固定资产采购申请','/Equ/PurchaseApply/PurchaseApplyList.aspx','y','4','','24958','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260505','设备调拨','/Equ/EquAlloc/EquAllocList.aspx','y','5','','24963','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260506','设备验收','/Equ/EquAcceptance/EquAcceptanceList.aspx','y','6','','24995','0','0','0','','1'); 
GO
--模块表中增加设备人员表菜单
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('260203','设备人员','/Equ/Equipment/EquipmentUser.aspx','y','3','','24998','0','0','0','','1'); 
GO
--模块表中报表中心增加设备管理报表菜单
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('8809','设备统计报表','','y','10','','24943','8','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880901','船机设备报表','','y','1','','24944','11','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090101','单船油耗分析','/Equ/Report/Ship/ShipOilWearAnalysis.aspx','y','1','','24968','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090102','加油申请单','/Equ/Report/Ship/RefuelApply.aspx','y','2','','24969','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090103','单船加油量记录表','/Equ/Report/Ship/RefuelRecord.aspx','y','3','','24970','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090104','维修保养计划完成表','/Equ/Report/Ship/RepairPlanComplete.aspx','y','4','','24971','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090105','维修保养费一览表','/Equ/Report/Ship/RepairCost.aspx?equipmentType=0','y','5','','24972','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090106','耙吸船产量报表(按台班上报)','/Equ/Report/Ship/ShipTeamReport.aspx?type=D','y','6','','24973','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090107','其他船产量报表(按台班上报)','/Equ/Report/Ship/ShipTeamReport.aspx?type=E','y','7','','24974','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090108','抓斗船泥驳船产量表','/Equ/Report/Ship/GrabMudReport.aspx','y','8','','24975','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090109','泥驳船产量表','/Equ/Report/Ship/MudShipReport.aspx','y','9','','24976','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090110','平板船产量表','/Equ/Report/Ship/ShipFlatReport.aspx','y','10','','24977','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090111','其他船产量表','/Equ/Report/Ship/ShipElseReport.aspx','y','11','','24978','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880902','陆上设备报表','','y','2','','24945','13','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090201','单机油耗报表','/Equ/Report/Land/OilWearSummary.aspx','y','1','','24948','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090202','维修保养费一览表','/Equ/Report/Ship/RepairCost.aspx?equipmentType=1','y','2','','24981','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090203','设备委外维修报表','/Equ/Report/Land/OutRepairCost.aspx','y','3','','24982','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090204','维修保养计划完成表','/Equ/Report/Land/RepairPlanComplete.aspx','y','4','','24983','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090205','挖掘机日机械产量表（按台班上报）','/Equ/Report/Land/RoadTeamReport.aspx?type=G','y','5','','24984','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090206','装载机日机械产量表（按台班上报）','/Equ/Report/Land/RoadTeamReport.aspx?type=L','y','6','','24985','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090207','其他设备日机械产量报表（按台班上报）','/Equ/Report/Land/RoadTeamReport.aspx?type=E','y','7','','24986','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090208','搅拌车日机械产量表','/Equ/Report/Land/RoadMixerReport.aspx','y','8','','24987','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090209','预制联锁块日机械产量报表','/Equ/Report/Land/RoadInterlockReport.aspx','y','9','','24988','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090210','排水板日机械产量报表','/Equ/Report/Land/RoadDrainReport.aspx','y','10','','24989','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090211','其他设备日机械产量报表（按产量上报）','/Equ/Report/Land/RoadElseReport.aspx','y','11','','24990','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090212','自卸车日机械产量表','/Equ/Report/Land/DumpReport.aspx','y','12','','24993','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090213','钻孔机月汇总表','/Equ/Report/Land/DrillMonthReport.aspx','y','13','','24996','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880903','工资报表','/Salary2/DepartmentFrame.aspx?path=SaMonthReport','y','3','','24979','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880904','人员一览表','/HR/StaffList.aspx','y','4','','24980','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880905','工程设备需求计划报表','/Equ/Report/RequirePlanReport.aspx','y','5','','24991','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880906','设备调配计划报表','/Equ/Report/DeployPlanReport.aspx','y','6','','24992','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880907','设备租赁台帐明细报表','/Equ/Report/EquLeaseReport.aspx','y','7','','24994','0','0','0','','1');
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090112','月度经营情况报表','/Equ/Report/Ship/MonthSituationReport.aspx','y','12','','24999','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('88090214','月度经营情况报表','/Equ/Report/Land/MonthSituationReport.aspx','y','14','','25000','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,
IsMaintainable,helppath,Isdisplay) VALUES 
('880908','人员请假统计表','/Equ/Report/Ship/SailorsAttendance.aspx','y','8','','24997','0','0','0','','1'); 

GO
--设备管理审核
--船机油耗预算
INSERT INTO WF_BusinessCode 
VALUES('142','船机油耗预算','id','Equ_Ship_BudOilWear','id','FlowState',
'/Equ/ShipOilWear/BudOilWearView.aspx',NULL,'26','PrjId','Code')
INSERT INTO WF_Business_Class
VALUES('142','001','船机油耗预算',NEWID())
--船机加油申请
INSERT INTO WF_BusinessCode 
VALUES('143','船机加油申请','id','Equ_Ship_RefuelApply','id','FlowState',
'/Equ/ShipOilWear/RefuelApplyView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('143','001','船机加油申请',NEWID())
--维修保养计划审核
INSERT INTO WF_BusinessCode 
VALUES('145','维修保养计划审核','Id','Equ_RepairPlan','Id','FlowState',
'/Equ/Repair/RepairPlanView.aspx',NULL,'26',NULL,'Code')
INSERT INTO WF_Business_Class
VALUES('145','001','维修保养计划审核',NEWID())
--维修申请
INSERT INTO WF_BusinessCode 
VALUES('147','维修申请审核','Id','Equ_RepairApply','Id','FlowState',
'/Equ/Repair/RepairApplyView.aspx',NULL,'26',NULL,'Code')
INSERT INTO WF_Business_Class
VALUES('147','001','维修申请审核',NEWID())
--维修上报
INSERT INTO WF_BusinessCode 
VALUES('148','维修上报审核','Id','Equ_RepairReport','Id','FlowState',
'/Equ/Repair/RepairReportView.aspx',NULL,'26',NULL,'(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('148','001','维修上报审核',NEWID())
--油耗出库审核
INSERT INTO WF_BusinessCode 
VALUES('149','油耗出库审核','Id','Equ_OilOut','Id','FlowState',
'/Equ/OilWearManage/EquOilOutView.aspx',NULL,'26','PrjId','Code')
INSERT INTO WF_Business_Class
VALUES('149','001','油耗出库审核',NEWID())
--设备需求计划审核
INSERT INTO WF_BusinessCode 
VALUES('150','设备需求计划审核','Id','Equ_RequirePlan','Id','FlowState',
'/Equ/RequirePlan/RequirePlanView.aspx',NULL,'26','PrjId','Code')
INSERT INTO WF_Business_Class
VALUES('150','001','设备需求计划审核',NEWID())
--设备报废审核
INSERT INTO WF_BusinessCode 
VALUES('151','设备报废审核','Id','Equ_Discard','Id','FlowState',
'/Equ/EquipmentDiscard/EquDiscardView.aspx',NULL,'26','NULL','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('151','001','设备报废审核',NEWID())
GO
--增加设备流程审核
--抓斗船上报审核
INSERT INTO WF_BusinessCode 
VALUES('144','抓斗船上报审核','Id','Equ_ShipGrapReport','Id','FlowState',
'/Equ/ShipGrapReport/ShipGrapReportView.aspx',NULL,'26','NULL','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('144','001','抓斗船上报审核',NEWID())
--船机调配计划审核
INSERT INTO WF_BusinessCode 
VALUES('146','设备调配计划','Id','Equ_DeployPlan','Id','FlowState',
'/Equ/EquDeployPlan/EquDeployPlanView.aspx',NULL,'26','PrjId','Code')
INSERT INTO WF_Business_Class
VALUES('146','001','设备调配计划',NEWID())
--船机台班上报审核
INSERT INTO WF_BusinessCode 
VALUES('152','船机台班上报审核','Id','Equ_ShipTeamReport','Id','FlowState',
'/Equ/ShipTeamReport/ShipTeamReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('152','001','船机台班上报审核',NEWID())
--陆上设备台班上报审核
INSERT INTO WF_BusinessCode 
VALUES('153','陆上设备台班上报审核','Id','Equ_RoadTeamReport','Id','FlowState',
'/Equ/RoadTeamReport/RoadTeamReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('153','001','陆上设备台班上报审核',NEWID())
--设备租赁审核
INSERT INTO WF_BusinessCode 
VALUES('154','设备租赁审核','Id','Equ_Lease','Id','FlowState',
'/Equ/EquLease/EquLeaseView.aspx',NULL,'26',NULL,'Code')
INSERT INTO WF_Business_Class
VALUES('154','001','设备租赁审核',NEWID())
--固定资产采购审核
INSERT INTO WF_BusinessCode 
VALUES('155','固定资产采购审核','Id','Equ_PurchaseApply','Id','FlowState',
'/Equ/PurchaseApply/PurchaseApplyView.aspx',NULL,'26',NULL,'ApplyCode')
INSERT INTO WF_Business_Class
VALUES('155','001','固定资产采购审核',NEWID())
--设备调拨审核
INSERT INTO WF_BusinessCode 
VALUES('156','设备调拨审核','Id','Equ_Alloc','Id','FlowState',
'/Equ/EquAlloc/EquAllocView.aspx',NULL,'26',NULL,'(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('156','001','设备调拨审核',NEWID())
--泥驳船上报审核
INSERT INTO WF_BusinessCode 
VALUES('157','泥驳船上报审核','Id','Equ_ShipMudReport','Id','FlowState',
'/Equ/ShipMudReport/ShipMudReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('157','001','泥驳船上报审核',NEWID())
--平板船上报审核
INSERT INTO WF_BusinessCode 
VALUES('158','平板船上报审核','Id','Equ_ShipFlatReport','Id','FlowState',
'/Equ/ShipFlatReport/ShipFlatReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('158','001','平板船上报审核',NEWID())
--其他船上报审核
INSERT INTO WF_BusinessCode 
VALUES('159','其他船上报审核','Id','Equ_ShipElseReport','Id','FlowState',
'/Equ/ShipElseReport/ShipElseReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('159','001','其他船上报审核',NEWID())
--自卸车上报审核
INSERT INTO WF_BusinessCode 
VALUES('160','自卸车上报审核','Id','Equ_RoadDumpReport','Id','FlowState',
'/Equ/RoadDumpReport/RoadDumpReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('160','001','自卸车上报审核',NEWID())
--钻孔机上报审核
INSERT INTO WF_BusinessCode 
VALUES('161','钻孔机上报审核','Id','Equ_RoadDrillReport','Id','FlowState',
'/Equ/RoadDrillReport/RoadDrillReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('161','001','钻孔机上报审核',NEWID())
--搅拌车上报审核
INSERT INTO WF_BusinessCode 
VALUES('162','搅拌车上报审核','Id','Equ_RoadMixerReport','Id','FlowState',
'/Equ/RoadMixerReport/RoadMixerReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('162','001','搅拌车上报审核',NEWID())
--排水板设备上报审核
INSERT INTO WF_BusinessCode 
VALUES('163','排水板设备上报审核','Id','Equ_RoadDrainReport','Id','FlowState',
'/Equ/RoadDrainReport/RoadDrainReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('163','001','排水板设备上报审核',NEWID())
--预制联锁块上报审核
INSERT INTO WF_BusinessCode 
VALUES('164','预制联锁块上报审核','Id','Equ_RoadInterlockReport','Id','FlowState',
'/Equ/RoadInterlockReport/RoadInterlockReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('164','001','预制联锁块上报审核',NEWID())
--其他设备上报审核
INSERT INTO WF_BusinessCode 
VALUES('165','其他设备上报审核','Id','Equ_RoadElseReport','Id','FlowState',
'/Equ/RoadElseReport/RoadElseReportView.aspx',NULL,'26','PrjId','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('165','001','其他设备上报审核',NEWID())
--设备验收审核
INSERT INTO WF_BusinessCode 
VALUES('166','设备验收审核','Id','Equ_Acceptance','Id','FlowState',
'/Equ/EquAcceptance/EquAcceptanceView.aspx',NULL,'26','NULL','(SELECT''查看'')')
INSERT INTO WF_Business_Class
VALUES('166','001','设备验收审核',NEWID())
GO

--初始化设备类型
--DELETE FROM Equ_EquipmentType
INSERT INTO Equ_EquipmentType
VALUES('4d837219-ced7-4983-b00e-6f6aa8395b95',NULL,'船机',1,'001','Ship')
INSERT INTO Equ_EquipmentType
VALUES('63e3da8c-a2cc-4adf-b295-806bc8316966','4d837219-ced7-4983-b00e-6f6aa8395b95','抓斗船',11,'001011','Grap')
INSERT INTO Equ_EquipmentType
VALUES('a8e4fcab-d125-43ad-9143-8baabbe02b80','4d837219-ced7-4983-b00e-6f6aa8395b95','泥驳船',12,'001012','MudBarge')
INSERT INTO Equ_EquipmentType
VALUES('9e0c17f3-8f5e-499a-ba2d-dd5c22bee54f','4d837219-ced7-4983-b00e-6f6aa8395b95','耙吸船',13,'001013','Dredger')
INSERT INTO Equ_EquipmentType
VALUES('c0cf9157-ac7b-4bc9-b00f-cf5373f3e028','4d837219-ced7-4983-b00e-6f6aa8395b95','平板船',14,'001014','Flat')
INSERT INTO Equ_EquipmentType
VALUES('091e9af3-4330-4539-be55-d7f5f0c5387c',NULL,'陆上设备',2,'002','Land')
INSERT INTO Equ_EquipmentType
VALUES('b38d59b0-41d8-4b74-9596-15d342d0b7bf','091e9af3-4330-4539-be55-d7f5f0c5387c','装载机',21,'002021','Loader')
INSERT INTO Equ_EquipmentType
VALUES('1099eafd-33c7-4fa6-88e0-034040083577','091e9af3-4330-4539-be55-d7f5f0c5387c','挖掘机',22,'002022','Grab')
INSERT INTO Equ_EquipmentType
VALUES('2b809b5a-bbec-453f-a16e-65995281cbbf','091e9af3-4330-4539-be55-d7f5f0c5387c','自卸车',23,'002023','Dump')
INSERT INTO Equ_EquipmentType
VALUES('7d535951-6256-4ed7-be7c-b4f2a70fbc22','091e9af3-4330-4539-be55-d7f5f0c5387c','钻孔机',24,'002024','Drill')
INSERT INTO Equ_EquipmentType
VALUES('00aed949-8b9e-4a0a-9e59-98ea62116c95','091e9af3-4330-4539-be55-d7f5f0c5387c','排水板',25,'002025','Drain')
INSERT INTO Equ_EquipmentType
VALUES('240a65e1-f33e-4201-8f60-ca4edffde0ff','091e9af3-4330-4539-be55-d7f5f0c5387c','预制联锁块',26,'002026','Interlock')
INSERT INTO Equ_EquipmentType
VALUES('5d0849c7-1701-48bb-a03c-582458d89f28','091e9af3-4330-4539-be55-d7f5f0c5387c','搅拌车',27,'002027','Mixer')
GO