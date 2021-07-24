
--核算成本
IF OBJECT_ID('Bud_CostAccounting', 'U') IS NOT NULL
    DROP TABLE Bud_CostAccounting
GO
CREATE TABLE Bud_CostAccounting
(
    Id nvarchar(200) PRIMARY KEY,--Guid
    CBSCode nvarchar(200) NOT NULL, --CBS编码
    Name nvarchar(200) NOT NULL, --名称
    Type nvarchar(1) NOT NULL, --"D"表示直接成本，"I"表示间接成本
    ResourceType nvarchar(200), -- 资源类型Id
    Note nvarchar(200) --摘要
)
GO

--间接成本预算
IF OBJECT_ID('Bud_IndirectBudget', 'U') IS NOT NULL
    DROP TABLE Bud_IndirectBudget
GO
CREATE TABLE Bud_IndirectBudget
(
    Id nvarchar(200) PRIMARY KEY, --Guid
    ProjectId nvarchar(200) NOT NULL, --项目ID
    CBSCode nvarchar(200) NOT NULL, --CBSCode
    BudgetAmount decimal(18,3) NOT NULL DEFAULT(0.0), --预算金额
    AccountAmount decimal(18,3) NOT NULL DEFAULT(0.0), --核算金额
    State nvarchar(1) NOT NULL DEFAULT('0'), --状态 0(未上报)，1(上报中)，2(已审核)， 3(取消上报)， 4(上报中)
    InputUser nvarchar(200) NOT NULL, --录入人
    InputDate datetime NOT NULL DEFAULT(GETDATE()), --录入时间
    Note nvarchar(max) --摘要
)
GO

--间接成本月度预算
IF OBJECT_ID('Bud_IndirectMonthBudget', 'U') IS NOT NULL
    DROP TABLE Bud_IndirectMonthBudget
GO
CREATE TABLE Bud_IndirectMonthBudget
(
    Id nvarchar(200) PRIMARY KEY, --Guid
    IndirectBudget nvarchar(200) REFERENCES Bud_IndirectBudget(Id) NOT NULL, --间接成本预算Id
    Year int NOT NULL, --年份
    Month int NOT NULL, --月份
    Amount decimal(18,3) --金额
)
GO

--间接成本日记账
IF OBJECT_ID('Bud_IndirectDiaryCost', 'U') IS NOT NULL
    DROP TABLE Bud_IndirectDiaryCost
GO
CREATE TABLE Bud_IndirectDiaryCost
(
    InDiaryId nvarchar(200) PRIMARY KEY, --Guid
    ProjectId nvarchar(200) NOT NULL, --项目Id
    Name nvarchar(200) NOT NULL, --费用项目
    Department nvarchar(200), --发生单位
    IssuedBy nvarchar(200) NOT NULL, --经手人
	FlowState int NOT NULL DEFAULT(-1), --流程状态
    InputUser nvarchar(200) NOT NULL, --录入人
    InputDate datetime NOT NULL --录入时间
)
GO

--间接成本日记账明细
IF OBJECT_ID('Bud_IndirectDiaryDetails', 'U') IS NOT NULL
    DROP TABLE Bud_IndirectDiaryDetails
GO
CREATE TABLE Bud_IndirectDiaryDetails
(
    InDiaryDetailsId nvarchar(200) PRIMARY KEY, --Guid
	InDiaryId nvarchar(200) REFERENCES Bud_IndirectDiaryCost(InDiaryId) ON DELETE CASCADE, --日记账Id
    Name nvarchar(200) NOT NULL, --名称
    Amount decimal(18,3) NOT NULL DEFAULT(0.0), --金额
    CBSCode nvarchar(200) NOT NULL, --CBS编码
    Note nvarchar(max) --摘要
)
GO

--审核
insert dbo.WF_BusinessCode values('087','间接成本日记账','InDiaryId','Bud_IndirectDiaryCost','InDiaryId','FlowState','/BudgetManage/Cost/CostVerifyRecord.aspx',null,'29')
insert dbo.WF_Business_Class values('087','001','间接成本日记账审核')

--组织机构费用预算
IF OBJECT_ID('Bud_OrganizationBudget', 'U') IS NOT NULL
    DROP TABLE Bud_OrganizationBudget
GO
CREATE TABLE Bud_OrganizationBudget
(
    Id nvarchar(200) PRIMARY KEY, --Guid
    OrganizationBudgetId nvarchar(200) NOT NULL, --组织机构编码
    CBSCode nvarchar(200) NOT NULL, --CBS编码
    BudgetAmount decimal(18,3), --预算金额
    AccountingAmount decimal(18,3), --核算金额
    State nvarchar(1) NOT NULL DEFAULT('0'), --状态 0(未上报)，1(上报中)，2(已审核)， 3(取消上报)， 4(上报中)
    InputUser nvarchar(200) NOT NULL, --录入人
    InputDate datetime NOT NULL DEFAULT(GETDATE()), --录入时间
    Note nvarchar(max) --摘要
)

--施工报量 
IF OBJECT_ID('Bud_ConsReport', 'U') IS NOT NULL
    DROP TABLE Bud_ConsReport
CREATE TABLE Bud_ConsReport
(
    ConsReportId nvarchar(500) primary key, --Guid
    PrjId nvarchar(500) NOT NULL, --所属项目Id
    State nvarchar(1) NOT NULL DEFAULT('0'), --状态 0(未上报)，1(上报中)，2(已审核)， 3(取消上报)， 4(上报中)（add 2011-06-17）
    InputUser nvarchar(500) NOT NULL, --录入人
    InputDate datetime NOT NULL, --录入时间
    WorkCard nvarchar(max), --工作记录
    IsValid bit DEFAULT(0) --是否有效
)

--施工报量分项工程
IF OBJECT_ID('Bud_ConsTask', 'U') IS NOT NULL
    DROP TABLE Bud_ConsTask
CREATE TABLE Bud_ConsTask 
(
    ConsTaskId nvarchar(500) primary key, --Guid
    ConsReportId nvarchar(500) REFERENCES Bud_ConsReport(ConsReportId) ON DELETE CASCADE, --所属ConsReport
    TaskId nvarchar(500) REFERENCES Bud_Task(TaskId) NOT NULL, --关联节点
    CompleteQuantity decimal(18,3) NOT NULL,--完成量
    AccountingQuantity decimal(18,3), --核算量（add 2011-06-17）
    WorkContent nvarchar(max), --工作内容
    Note nvarchar(max), --摘要
)

IF OBJECT_ID('Bud_ConsTaskRes') IS NOT NULL
    DROP TABLE Bud_ConsTaskRes
CREATE TABLE Bud_ConsTaskRes
(
    ConsTaskResId nvarchar(500) PRIMARY KEY, --Guid
    ConsTaskId nvarchar(500) NOT NULL REFERENCES Bud_ConsTask(ConsTaskId) ON DELETE CASCADE, --所属ConsTask
    ResourceId nvarchar(500) REFERENCES Res_Resource(ResourceId) NOT NULL,--ResourceId
    Quantity decimal(18,3) NOT NULL, --完成量
    AccountingQuantity decimal(18,3), --核算量 (add 2011-06-17)
    UnitPrice decimal(18,3) NOT NULL,--单价
    CBSCode nvarchar(200), --CBS编码 (add 2011-06-17)
)

--李海莹
create function [dbo].[GetResourceType](@ResourceId nvarchar(500)) 
returns nvarchar(500) 
as 
begin 
declare @ResourceType nvarchar(500)
declare @TopResType nvarchar(500)
select @ResourceType= ResourceType from Res_Resource where ResourceId=@ResourceId
if(@ResourceType is not null)
 begin
  set @TopResType=dbo.GetTopResourceType(@ResourceType)
 end 
 return @TopResType
end 
GO

CREATE function [dbo].[GetTopResourceType](@ResourceType nvarchar(500)) 
returns nvarchar(500) 
as 
begin 
declare @TempResType nvarchar(500)
declare @ReturnResType nvarchar(500)
declare @ResCount int
set @ResCount=0
select @TempResType= ParentId from Res_ResourceType where ResourceTypeId=@ResourceType
select @ResCount=count(*) from Res_ResourceType where ParentId=@TempResType
if (@ResCount > 0)
begin
 set @ReturnResType=dbo.GetTopResourceType(@TempResType)
end 
else
begin
select @ReturnResType= ResourceTypeId from Res_ResourceType where ResourceTypeId=@ResourceType
end
return @ReturnResType
end



