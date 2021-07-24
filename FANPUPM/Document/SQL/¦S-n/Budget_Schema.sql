--表删除
DROP TABLE Res_Price
DROP TABLE Res_PriceType
--DROP TABLE Res_ResourceImage
DROP TABLE Res_Resource
DROP TABLE Res_Attribute
DROP TABLE Res_ResourceType
DROP TABLE Res_Unit   
-------------------
DROP TABLE Res_Template 
DROP TABLE Res_TemplateItem
-------------------
DROP TABLE Bud_Template
DROP TABLE Bud_TemplateType
-------------------
DROP TABLE Bud_TaskType
DROP TABLE Bud_TaskResource
DROP TABLE Bud_Task

GO

--单位表
IF OBJECT_ID(N'Res_Unit',N'U') IS NOT NULL
DROP TABLE Res_Unit
GO
CREATE TABLE Res_Unit
(
	UnitId nvarchar(500) primary key,--Guid
	Code nvarchar(500) not null,--类型编码
	Name nvarchar(1000) not null,--类型名称
	InputUser nvarchar(20) null,--录入人
	InputDate DateTime default(getdate())--录入时间
)
GO
EXEC sp_addextendedproperty @name='MS_Description', @value='单位表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Res_Unit'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Unit', @level2type=N'COLUMN', @level2name=N'UnitId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Unit', @level2type=N'COLUMN', @level2name=N'Code'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型名称' ,@level0type=N'SCHEMA', @level0name=N'dbo',
@level1type=N'TABLE', @level1name=N'Res_Unit', @level2type=N'COLUMN', @level2name=N'Name'


--资源类型表
IF OBJECT_ID(N'Res_ResourceType',N'U') IS NOT NULL
DROP TABLE Res_ResourceType
GO
CREATE TABLE Res_ResourceType
(
	ResourceTypeId nvarchar(500) primary key,--GUID
	ParentId nvarchar(500) REFERENCES Res_ResourceType(ResourceTypeId) NULL,--父节点ID
	ResourceTypeCode nvarchar(500) not null,--类型编码
	ResourceTypeName nvarchar(1000) not null,--类型名称
	InputUser nvarchar(20) null,--录入人
	InputDate DateTime default(getdate()),--录入时间
	IsValid bit,--是否有效
)
GO
EXEC sp_addextendedproperty @name='MS_Description', @value='资源类型表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Res_ResourceType'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'GUID' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_ResourceType', @level2type=N'COLUMN', @level2name=N'ResourceTypeId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父节点ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_ResourceType', @level2type=N'COLUMN', @level2name=N'ParentId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_ResourceType', @level2type=N'COLUMN', @level2name=N'ResourceTypeCode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_ResourceType', @level2type=N'COLUMN', @level2name=N'ResourceTypeName'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否有效' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_ResourceType', @level2type=N'COLUMN', @level2name=N'IsValid'
GO


--类别属性(主材，辅材)
--类别表里的顶层节点才有类别属性（人、材、机...）
IF OBJECT_ID(N'Res_Attribute','U') IS NOT NULL
DROP TABLE Res_Attribute
GO
CREATE TABLE Res_Attribute
(
	AttributeId nvarchar(500) PRIMARY KEY,--ID
	ResourceTypeId nvarchar(500) REFERENCES Res_ResourceType(ResourceTypeId),--类别ID
	AttributeName nvarchar(1000),--名称
	InputUser nvarchar(20) null,--录入人
	InputDate DateTime default(getdate())--录入时间
)
EXEC sp_addextendedproperty @name='MS_Description', @value='类别属性表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Res_Attribute'


EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Attribute', @level2type=N'COLUMN', @level2name=N'AttributeId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类别ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Attribute', @level2type=N'COLUMN', @level2name=N'ResourceTypeId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Attribute', @level2type=N'COLUMN', @level2name=N'AttributeName'
GO

--资源
IF OBJECT_ID (N'Res_Resource', N'U') IS NOT NULL
DROP TABLE Res_Resource;
GO
CREATE TABLE Res_Resource
(
	ResourceId nvarchar(500) primary key,--Guid
	ResourceCode nvarchar(500) not null,--资源编码
	ResourceName nvarchar(1000) not null,--资源名称
	Brand nvarchar(1000),--品牌
	TaxRate decimal(18,3),--税率
	Specification nvarchar(1000),--规格
	TechnicalParameter nvarchar(1000),--技术参数
	ResourceType nvarchar(500) REFERENCES Res_ResourceType(ResourceTypeId),--资源类型
	Unit nvarchar(500) REFERENCES Res_Unit(UnitId) null,--单位
	Attribute nvarchar(500) REFERENCES Res_Attribute null,--属性
	Series nvarchar(500) null,--系列
    SupplierId int, --null供应商Id
    ModelNumber nvarchar(1000),--型号
    Note nvarchar(max),--备注
	InputUser nvarchar(20) null,--录入人
	InputDate DateTime default(getdate())--录入时间
)
GO
EXEC sp_addextendedproperty @name='MS_Description', @value='资源表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Res_Resource'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'ResourceId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资源编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'ResourceCode'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资源名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'ResourceName'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'品牌' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'Brand'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'税率' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'TaxRate'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'规格' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'Specification'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技术参数' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'TechnicalParameter'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资源类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'ResourceType'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'Unit'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'属性' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'Attribute'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系列' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'Series'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'SupplierId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'型号' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'ModelNumber'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Resource', @level2type=N'COLUMN', @level2name=N'Note'
GO


--资源图片表
--没有使用
--IF OBJECT_ID('Res_ResourceImage','U') IS NOT NULL
--DROP TABLE Res_ResourceImage
--GO
--
--CREATE TABLE Res_ResourceImage
--(
--	ImageId nvarchar(500) primary key,--ID
--	ResourceId nvarchar(500) REFERENCES Res_Resource(ResourceId) NOT NULL,--资源ID
--	ImageName nvarchar(1000) not null,--图片名称
--	ImageUrl nvarchar(1000) not null,--图片路径
--	InputUser nvarchar(20) null,--录入人
--	InputDate DateTime default(getdate())--录入时间
--)


--价格类型
IF OBJECT_ID(N'Res_PriceType','U') IS NOT NULL
DROP TABLE Res_PriceType
GO
CREATE TABLE Res_PriceType
(
	PriceTypeId nvarchar(500) primary key,--ID
	PriceTypeCode nvarchar(500) not null,--编码
	PriceTypeName nvarchar(1000) not null,--名称
	Note nvarchar(max),--备注
	InputUser nvarchar(20) null,--录入人
	InputDate DateTime default(getdate()),--录入时间
	UserCodes nvarchar(max) DEFAULT '["00000000"]'权限人员
) 
EXEC sp_addextendedproperty @name='MS_Description', @value='价格类型表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Res_PriceType'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_PriceType', @level2type=N'COLUMN', @level2name=N'PriceTypeId'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_PriceType', @level2type=N'COLUMN', @level2name=N'PriceTypeCode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_PriceType', @level2type=N'COLUMN', @level2name=N'PriceTypeName'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_PriceType', @level2type=N'COLUMN', @level2name=N'Note'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限相关人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_PriceType', @level2type=N'COLUMN', @level2name=N'UserCodes'
GO
INSERT INTO Res_PriceType(PriceTypeId, PriceTypeCode, PriceTypeName, Note)
VALUES('192340F1-08E3-4B32-B65D-75E785062D05', '001','预算价', '预算价，不能删除')


--价格
IF OBJECT_ID(N'Res_Price','U') IS NOT NULL
DROP TABLE Res_Price
GO
CREATE TABLE Res_Price
(
	ResourceId nvarchar(500) REFERENCES Res_Resource(ResourceId),--资源ID
	PriceTypeId nvarchar(500) REFERENCES Res_PriceType(PriceTypeId),--价格类型编码
	PriceValue decimal(18,3) null,--价格
	InputUser nvarchar(20) null,--录入人
	InputDate DateTime default(getdate()),--录入时间
    PRIMARY KEY(ResourceId,PriceTypeId)
)
GO
EXEC sp_addextendedproperty @name='MS_Description', @value='价格表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Res_Price'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资源ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Price', @level2type=N'COLUMN', @level2name=N'ResourceId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格类型编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Price', @level2type=N'COLUMN', @level2name=N'PriceTypeId'

EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'价格' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_Price', @level2type=N'COLUMN', @level2name=N'PriceValue'
GO


--资源库模板表
IF OBJECT_ID('Res_Template','U') IS NOT NULL
DROP TABLE Res_Template
GO
CREATE TABLE Res_Template
(
	TemplateId NVARCHAR(500) PRIMARY KEY,
	TemplateName NVARCHAR(1000) NOT NULL,
	StartRowIndex INT NULL,--首行
	IsValid BIT DEFAULT 1 NOT NULL, --是否有效
	InputUser NVARCHAR(500),
	InputDate DATETIME DEFAULT GETDATE()
)

--资源库模板库对应表
IF OBJECT_ID('Res_TemplateItem','U') IS NOT NULL
DROP TABLE Res_TemplateItem
GO
CREATE TABLE Res_TemplateItem
(
	ItemId NVARCHAR(500) PRIMARY KEY,
	TemplateId NVARCHAR(500) REFERENCES Res_Template(TemplateId),
	ExcelColumn NVARCHAR(500) NOT NULL,--Excel列
	ExcelRealCoumn NVARCHAR(500) NOT NULL,--Excel真实列
	DbColumn NVARCHAR(500) NOT NULL--Db列
)

--WBS模板类型
IF OBJECT_ID('Bud_TemplateType','U') IS NOT NULL
DROP TABLE Bud_TemplateType
GO
CREATE TABLE Bud_TemplateType
(
	TypeId NVARCHAR(500) PRIMARY KEY,
	TypeCode NVARCHAR(500) NOT NULL,
	TypeName NVARCHAR(1000) NOT NULL,
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE())
)
GO

--WBS模板表
IF OBJECT_ID('Bud_Template','U') IS NOT NULL
DROP TABLE Bud_Template
GO
CREATE TABLE Bud_Template
(
	TemplateId NVARCHAR(500) PRIMARY KEY,
	TemplateTypeId NVARCHAR(500) REFERENCES Bud_TemplateType(TypeId),--模板类型ID
	TemplateCode NVARCHAR(500) NOT NULL,--模板编码
	TemplateName NVARCHAR(1000) NOT NULL,--模板名称
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE())
)
GO

--WBS模板项表
IF OBJECT_ID('Bud_TemplateItem','U') IS NOT NULL
DROP TABLE Bud_TemplateItem
GO
CREATE TABLE Bud_TemplateItem
(
	TemplateItemId NVARCHAR(500) PRIMARY KEY,
	ParentId NVARCHAR(500) ,--父节点ID
	ItemCode NVARCHAR(500),--编码
	ItemName NVARCHAR(500),--名称
	OrderNumber NVARCHAR(500),--排序
	TaskType NVARCHAR(500) REFERENCES Bud_TaskType(TaskTypeId) NOT NULL,--节点类型
	Unit NVARCHAR(500),--单位
	Quantity DECIMAL(18,3) NOT NULL DEFAULT(0.0),--工程量
	UnitPrice DECIMAL(18,3) DEFAULT(0.0),--单价
	Note NVARCHAR(MAX),--备注
)

--模板资源表
IF OBJECT_ID('Bud_TemplateResource', 'U') IS NOT NULL
    DROP TABLE Bud_TemplateResource
GO
CREATE TABLE Bud_TemplateResource
(
    TemplateResourceId nvarchar(500) PRIMARY KEY NOT NULL,
    TemplateItemId nvarchar(500) REFERENCES Bud_TemplateItem(TemplateItemId),--节点Id
    ResourceId nvarchar(500) REFERENCES Res_Resource(ResourceId),--资源Id
    ResourceQuantity decimal(18,3),--资源数量
	ResourcePrice decimal(18,3),
    InputUser nvarchar(50) NOT NULL,
    InputDate datetime NOT NULL DEFAULT(GETDATE())
)

--节点类型
IF OBJECT_ID('Bud_TaskType','U') IS NOT NULL
DROP TABLE Bud_TaskType
GO
CREATE TABLE Bud_TaskType
(
	TaskTypeId NVARCHAR(500) PRIMARY KEY,
	TaskTypeCode NVARCHAR(500) NOT NULL UNIQUE,
	TaskTypeName NVARCHAR(500) NOT NULL UNIQUE,
	TaskTypeLayer INT NOT NULL,--级别：1,2,3
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE())
)

--节点表
IF OBJECT_ID('Bud_Task','U') IS NOT NULL
DROP TABLE Bud_Task
GO
CREATE TABLE Bud_Task
(
	TaskId NVARCHAR(500) PRIMARY KEY,
	ParentId NVARCHAR(500) ,--父节点ID
	OrderNumber NVARCHAR(500),--排序
	Version INT DEFAULT(1),--版本号
	PrjId NVARCHAR(500),--项目GUID
	TaskCode NVARCHAR(500) NOT NULL,
	TaskName NVARCHAR(500) NOT NULL,
	Unit NVARCHAR(500),--单位
	Quantity DECIMAL(18,3) NOT NULL DEFAULT(0.0),--工程量
	UnitPrice DECIMAL(18,3) DEFAULT(0.0),--综合单价
	Total DECIMAL(18,3) DEFAULT(0.0),--小计
	StartDate DATETIME DEFAULT(GETDATE()),--开始时间
	EndDate DATETIME DEFAULT(GETDATE()),--结束时间
	Note NVARCHAR(MAX),--备注
	IsValid BIT DEFAULT 1,--是否有效，默认有效
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE()),
    Modified nvarchar(200) --'1'表示修改过
)
EXEC sp_addextendedproperty @name='MS_Description', @value='节点表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Bud_Task'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'TaskType'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'TaskCode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'TaskName'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'Unit'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程量' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'Quantity'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'StartDate'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'EndDate'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'综合单价' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'UnitPrice'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小计' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'Total'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_Task', @level2type=N'COLUMN', @level2name=N'Note'
GO

--项目节点表
IF OBJECT_ID('Bud_PrjTaskInfo', 'U') IS NOT NULL
DROP TABLE Bud_PrjTaskInfo
CREATE TABLE Bud_PrjTaskInfo
(
	PrjTaskInfoId NVARCHAR(500) PRIMARY KEY,
	PrjId NVARCHAR(500),--项目ID
	IsLocked BIT,--是否锁定
)

--节点资源表
IF OBJECT_ID('Bud_TaskResource','U') IS NOT NULL
DROP TABLE Bud_TaskResource
GO
CREATE TABLE Bud_TaskResource
(
	TaskResourceId NVARCHAR(500) PRIMARY KEY,
	TaskId NVARCHAR(500) REFERENCES Bud_Task(TaskId),--节点ID
	ResourceId NVARCHAR(500) REFERENCES Res_Resource(ResourceId),--资源ID
	ResourceQuantity DECIMAL(18,3), --节点数量
    ResourcePrice DECIMAL(18,3),--资源价格
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE())
)



-----PROC
--分页获取资源 Bery 2011-1-17 
IF OBJECT_ID('usp_GetResource','P') IS NOT NULL
DROP PROC usp_GetResource
GO
CREATE PROC usp_GetResource
	@typeId NVARCHAR(400),
	@pageSize int,
	@pageIndex int
AS
WITH cte_Resource
AS
(
	SELECT * FROM Res_ResourceType WHERE ResourceTypeId = @typeId
	UNION ALL
	SELECT Res_ResourceType.* FROM cte_Resource 
	INNER JOIN Res_ResourceType ON cte_Resource.ResourceTypeId = Res_ResourceType.ParentId
)
SELECT TOP(@pageSize) * FROM
(
	SELECT  ROW_NUMBER() OVER (ORDER BY len(ResourceTypeCode) ASC, ParentId DESC) AS row_number,Res_Resource.*,Name
	FROM Res_Resource 
	INNER JOIN Res_Unit ON Res_Unit.UnitId = Res_Resource.Unit
	INNER JOIN cte_Resource ON cte_Resource.ResourceTypeId = Res_Resource.ResourceType
) AS rt
WHERE row_number > (@pageIndex - 1) * @pageSize

--获取资源数目 Bery 2011-1-17
IF OBJECT_ID('usp_GetResourceCount','P') IS NOT NULL
DROP PROC usp_GetResourceCount
GO
CREATE PROC usp_GetResourceCount
	@typeId NVARCHAR(400)
AS
WITH cte_Resource
AS
(
	SELECT * FROM Res_ResourceType WHERE ResourceTypeId = @typeId
	UNION ALL
	SELECT Res_ResourceType.* FROM cte_Resource 
	INNER JOIN Res_ResourceType ON cte_Resource.ResourceTypeId = Res_ResourceType.ParentId
)
SELECT COUNT(*)
FROM Res_Resource 
INNER JOIN cte_Resource ON cte_Resource.ResourceTypeId = Res_Resource.ResourceType


--资源临时表，预算导入时，资源导入失败时存放
IF OBJECT_ID (N'Res_ResourceTemp', N'U') IS NOT NULL
DROP TABLE Res_ResourceTemp;
GO
CREATE TABLE Res_ResourceTemp
(
    Id nvarchar(500) primary key,
	ResourceId nvarchar(500) REFERENCES Res_Resource(ResourceId) ON DELETE CASCADE,-- 资源Id
    TaskId nvarchar(500) REFERENCES Bud_Task(TaskId) ON DELETE CASCADE,--节点Id
	ResourceCode nvarchar(500),--资源编码
	ResourceName nvarchar(1000),--资源名称
    UnitPrice decimal(18,3) ,--单价,
    Quantity decimal(18,3), --数量
    Amount decimal(18,3), --金额
    PrjId nvarchar(500) --项目Id
)
GO

--合同预算节点
IF OBJECT_ID('Bud_ContractTask','U') IS NOT NULL
DROP TABLE Bud_ContractTask
GO
CREATE TABLE Bud_ContractTask
(
	TaskId NVARCHAR(500) PRIMARY KEY,
	ParentId NVARCHAR(500) ,--父节点ID
	OrderNumber NVARCHAR(500),--排序
	--Version INT DEFAULT(1),--版本号
	PrjId NVARCHAR(500),--项目GUID
	TaskCode NVARCHAR(500) NOT NULL,
	TaskName NVARCHAR(500) NOT NULL,
	Unit NVARCHAR(500),--单位
	Quantity DECIMAL(18,3) NOT NULL DEFAULT(0.0),--工程量
	UnitPrice DECIMAL(18,3) DEFAULT(0.0),--综合单价
	Total DECIMAL(18,3) DEFAULT(0.0),--小计
	StartDate DATETIME DEFAULT(GETDATE()),--开始时间
	EndDate DATETIME DEFAULT(GETDATE()),--结束时间
	Note NVARCHAR(MAX),--备注
	IsValid BIT DEFAULT 1,--是否有效，默认有效
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE()),
    --Modified nvarchar(200) --'1'表示修改过
)
EXEC sp_addextendedproperty @name='MS_Description', @value='节点表', @level0type=N'Schema', @level0name=N'dbo', 
@level1type=N'table', @level1name=N'Bud_ContractTask'
--EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'类型' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
--@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'TaskType'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'TaskCode'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'TaskName'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单位' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'Unit'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工程量' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'Quantity'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'开始时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'StartDate'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'结束时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'EndDate'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'综合单价' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'UnitPrice'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'小计' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'Total'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Bud_ContractTask', @level2type=N'COLUMN', @level2name=N'Note'
GO

--合同节点资源表
IF OBJECT_ID('Bud_ContractResource','U') IS NOT NULL
DROP TABLE Bud_ContractResource
GO
CREATE TABLE Bud_ContractResource
(
	TaskResourceId NVARCHAR(500) PRIMARY KEY,
	TaskId NVARCHAR(500) REFERENCES Bud_ContractTask(TaskId) ON DELETE CASCADE,--节点ID
	ResourceId NVARCHAR(500) REFERENCES Res_Resource(ResourceId) ON DELETE CASCADE,--资源ID
	ResourceQuantity DECIMAL(18,3), --节点数量
    ResourcePrice DECIMAL(18,3),--资源价格
	InputUser NVARCHAR(50) NOT NULL,
	InputDate DATETIME NOT NULL DEFAULT(GETDATE())
)


