update Rep_Main set selectsql='select * from (select TypeCode as "序号", prjname as "项目名称",owner as "建设单位",prjcost as "造价",prjplace as "项目地点",startdate as "开始日期",enddate as "结束日期",prjstate as "状态"  from PT_PrjInfo where IsValid=1) a' where ReportID=888888

USE pm2
GO
/****** 工作日志表:  Table [dbo].[pm_Construction_Log]    脚本日期: 09/14/2009 10:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pm_Construction_Log](          
	[logID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProjectId] [varchar](50) NULL,
	[code] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[part] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[attendance] [int] NULL,
	[temperature] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[amweather] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[pmweather] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[operations] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[thisDate] [datetime] NULL,
	[daycontent] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[design] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[acceptance] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[beton] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[datum] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[product] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[situation] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_pm_Construction_log] PRIMARY KEY CLUSTERED 
(
	[logID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]



USE pm2
GO
/****** 施工日志表:  Table [dbo].[pm_Construction_Log]    脚本日期: 09/14/2009 10:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pm_Work_Log](
	[logID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProjectId] [varchar](50) NULL,
	[code] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[part] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[attendance] [int] NULL,
	[temperature] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[amweather] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[pmweather] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[operations] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[thisDate] [datetime] NULL,
	[daycontent] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[design] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[acceptance] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[beton] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[datum] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[product] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[situation] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_pm_Work_log] PRIMARY KEY CLUSTERED 
(
	[logID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


update WF_BusinessCode set dowithurl='/EPC/CostManagement/costinputtop.aspx?Type=ShenHe' where BusinessCode='018'--修改成本管理--其它成本日记账--费用录入  审核有问题

--1.	物质管理—物质报表添加一个库存报表信息报表  路径：EPC/Report/stock.aspx?reportid=831205
--2.	施工日志完成 路径：EPC/ConstructSchedule/ConstructionLogmain.aspx
--3.	工作日志 路径：EPC/ConstructSchedule/WorkLogmain.aspx
--4. 邮件查看 /oa/UserDefineFlow/inbox.aspx

update  pt_d_bm set v_bmbm = 0115 where i_bmdm = 50----09-10-30_ldh用于修改物质设备部（问题：在物质设备中添加用户企业经营部也会出现）
HR/Personnel/Employee.aspx?sfyx=1--人员信息管理09-11-3_Ldh（问题认识管理中冻结和启用）
HR/Personnel/Employee.aspx?sfyx=2--离职员工管理09-11-3_Ldh（问题认识管理中冻结和启用）

alter table EPM_Con_ContractMain alter column PayMode varchar(400)--点击修改-表单支付方式处 字符有长度限制 我们需要增加





----------------------------------------------------------------------------物资模块

EXEC sp_rename 'Sm_Treasury.ID号', 'tid', 'COLUMN';
GO --将中文列名修改为英文


-------------------------------------------------------------
-----向表Sm_Resource_PriceType增加字段rptIsShow
-----rptIsShow表示此价格类型是否显示：0表示不显示，1表示显示
-----2010-06-03  
-----增加人：何亚坤


ALTER TABLE Sm_Resource_PriceType ADD rptIsShow int


----向表Sm_Resource_PriceType增加字段IsDefault
----值为1表示默认 0表示非默认
----2010-6-12

ALTER TABLE Sm_Resource_PriceType ADD IsDefault int default 0


------向调拨表Sm_Allocation增加字段 OutAllocationPerson  InAllocationPerson  
------OutAllocationPerson 调拨拨出人  InAllocationPerson 调拨接收人  
------在数据库中是以编码的形式存在
-----2010-06-23
alter table Sm_Allocation add OutAllocationPerson nvarchar(64)
alter table Sm_Allocation add InAllocationPerson nvarchar(64)

----向表epm_res_resource 增加字段 imgurl
----保存资源图片
----用于在资源资源维护中显示图片

alter table epm_res_resource add imgurl nvarchar(200)

-----此视图只在原有的基础之上增加了imgurl字段


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[EPM_V_Res_ResourceBasic]
AS
SELECT     dbo.EPM_Res_Resource.VersionCode, dbo.EPM_Res_Resource.ResourceCode, dbo.EPM_Res_Resource.CategoryCode, imgurl,
                      dbo.EPM_Res_Resource.ResourceName, dbo.EPM_Res_Resource.Specification, dbo.EPM_Res_Resource.ResourceStyle, 
                      dbo.EPM_Res_PriceRelations.PriceItemID, dbo.EPM_Res_PriceItem.PriceItemName, dbo.EPM_Res_PriceRelations.Price, 
                      dbo.EPM_Res_Gauge.UnitID, dbo.EPM_Res_Unit.UnitName, dbo.EPM_Res_Gauge.IsValid, dbo.EPM_Res_Gauge.IsDefault, 
                      dbo.EPM_Res_PriceItem.IsValid AS Expr1, dbo.EPM_Res_PriceItem.IsDefault AS Expr2, dbo.EPM_Res_Resource.IsValid AS Expr3, 
                      dbo.EPM_Res_Category.CategoryName, dbo.EPM_Res_Gauge.Quotiety, dbo.EPM_Res_Resource.ResourceType
FROM         dbo.EPM_Res_Unit RIGHT OUTER JOIN
                      dbo.EPM_Res_Category RIGHT OUTER JOIN
                      dbo.EPM_Res_Resource ON dbo.EPM_Res_Category.CategoryCode = dbo.EPM_Res_Resource.CategoryCode AND 
                      dbo.EPM_Res_Category.VersionCode = dbo.EPM_Res_Resource.VersionCode LEFT OUTER JOIN
                      dbo.EPM_Res_Gauge ON dbo.EPM_Res_Resource.VersionCode = dbo.EPM_Res_Gauge.VersionCode AND 
                      dbo.EPM_Res_Resource.ResourceCode = dbo.EPM_Res_Gauge.ResourceCode LEFT OUTER JOIN
                      dbo.EPM_Res_PriceItem INNER JOIN
                      dbo.EPM_Res_PriceRelations ON dbo.EPM_Res_PriceItem.PriceItemID = dbo.EPM_Res_PriceRelations.PriceItemID ON 
                      dbo.EPM_Res_Resource.VersionCode = dbo.EPM_Res_PriceRelations.VersionCode AND 
                      dbo.EPM_Res_Resource.ResourceCode = dbo.EPM_Res_PriceRelations.ResourceCode ON 
                      dbo.EPM_Res_Unit.UnitID = dbo.EPM_Res_Gauge.UnitID
WHERE     (dbo.EPM_Res_Gauge.IsDefault = 1) AND (dbo.EPM_Res_PriceItem.IsDefault = 1) AND (dbo.EPM_Res_Gauge.IsValid = 1) AND 
                      (dbo.EPM_Res_PriceItem.IsValid = 1) AND (dbo.EPM_Res_Resource.IsValid = 1) AND 
                      (dbo.EPM_Res_Resource.VersionCode = '896431D1-F875-47EC-8164-CED63F6E65F2')
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


------------------------------------------------------------
--增加库存权限菜单--
--insert WF_BusinessCode(


--修改基本设置表
ALTER TABLE Sm_Set ALTER COLUMN paraid  nvarchar(50)
ALTER TABLE Sm_Set ALTER COLUMN paraname nvarchar(100) not null
ALTER TABLE Sm_Set add CONSTRAINT pk1 primary key(paraname)  



--给采购表添加一个项目字段 2010.6.10
alter table Sm_Purchaseplan add Project nvarchar(64) 

--给采购单表添加项目字段
alter table Sm_Purchase add Project nvarchar(64) null;

--入库单添加项目字段
alter table Sm_Storage add project nvarchar(64)

--给入库单添加甲供标志 2010-7-8 Bery
Alter table Sm_Storage add isfirst bit null;
--预警物资数量表 2010-7-15
create table Sm_AlarmNum
(
said nvarchar(50) not null,
scode nvarchar(50) not null ,
AlarmNum decimal(18,3) not null,
settime smalldatetime not null,
tcode nvarchar(512) not null 
primary key(said,tcode)
) 

--给用户表添加离职日期字段 2010-8-20 李真
alter table pt_yhmc add leavetime datetime 
--设置预警表添加字段
alter table Sm_AlarmNum add InfoCode nvarchar(50)


--修改日程安排字段
ALTER TABLE OA_Calendar_Info ALTER COLUMN Content nvarchar(max)

--收入合同结算编号字段修改 2010-12-24
alter table dbo.Con_Incomet_Modify alter column ChangeCode nvarchar(64)

--皮肤 2011-1-4 LZ
insert into PT_SkinType(SkinID,SkinName) values('4','灰色经典');

--向相关单位表增加字段 2011-03-02 hyk
--记录品牌信息,邮箱信息,国家信息
alter table XPM_Basic_ContactCorp add Brand nvarchar(250)
alter table XPM_Basic_ContactCorp add Email nvarchar(100)
alter table XPM_Basic_ContactCorp add Contry nvarchar(300)
---修改相关单位的手机号码的长度 2011-03-03 hyk
alter table XPM_Basic_ContactCorp alter column HandPhone varchar(250)

---修改相关单位中的联系电话的长度 2011-03-09 hykun
alter table XPM_Basic_ContactCorp alter column Telephone varchar(250)

--删除Bud_Task和Bud_TemplateItem表中的自身外键关联 2011-3-22 Bery
ALTER TABLE Bud_Task DROP CONSTRAINT FK__Bud_Task__Parent__031D3AFB
ALTER TABLE Bud_TemplateItem DROP CONSTRAINT FK__Bud_Templ__Paren__2FEFE172

--添加ResourcePrice列 2011-3-23 Bery
ALTER TABLE Bud_TaskResource ADD ResourcePrice DECIMAL(18,3)

--给价格类型表添加初始化数据 2011-3-24 10:43:53
INSERT INTO Res_PriceType(PriceTypeId, PriceTypeCode, PriceTypeName, Note) VALUES('192340F1-08E3-4B32-B65D-75E785062D05', '001','预算价', '预算价，不能删除')

--添加表Bud_TemplateResource 2011-3-24 10:43:57
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

--修改Bud_Task 2011-3-24 10:46:50
ALTER TABLE Bud_Task DROP CONSTRAINT FK__Bud_Task__TaskTy__04115F34
ALTER TABLE Bud_Task DROP COLUMN TaskType

--修改 Bud_TemplateItem 2011-3-24 10:51:13
ALTER TABLE Bud_TemplateItem DROP CONSTRAINT FK__Bud_Templ__TaskT__3E08F69F
ALTER TABLE Bud_TemplateItem DROP COLUMN TaskType
ALTER TABLE Bud_TemplateItem ADD TemplateId nvarchar(500) REFERENCES Bud_Template(TemplateId)

--修改部门表中“V_bmqc”字段使其满足部门较多的时候的需求 hykun 2011-03-08
alter table pt_d_bm alter column v_bmqc nvarchar(1500)

--修改表 xpm_basic_contactcorp 中字段 corpbrief、speciality、postcode类型长度 hzy 2011-4-01
alter table xpm_basic_contactcorp alter column corpbrief nvarchar(3000)
alter table xpm_basic_contactcorp alter column speciality nvarchar(max)
alter table xpm_basic_contactcorp alter column postcode varchar(100)
----创建表 InstitutionClass 制度分类表
create table InstitutionClass
(
	InsId  int primary key identity(1,1),
	ClassName	nvarchar(200) not null,
	ClassCode   nvarchar(100),
	LeveCode	varchar(150) not null,
	Remark	nvarchar(500),
	PermissionClass int not null,---分为三种：值为-1时，默认所有人都有权限；为0时：以部门设定权限；为1时：以个人设定权限。
	PermissionSet   nvarchar(max),----权限的集合
	WritePerson     varchar(15),---添加分类的人
	WriteTime	datetime
)
---创建表 制度详细信息表
create table Institutions
(
	InsCode uniqueidentifier primary key,
	ClassCode nvarchar(150) not null,
	UniqueCode nvarchar(180) not null,
	InsName   nvarchar(180) not null,
	InsContent text,
	IssuePerson  nvarchar(30),
	IssueDate    datetime,
	status		int,
	IsValid		int,
	UserCode	varchar(15),
	writedate	datetime
)
----向表XPM_Basic_AnnexSettings 插入数据，记录制度管理中附件的保存设置
----制度管理 新增 2011-04-03

insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('138','Institutions','制度管理',8388608,'*','/UploadFiles/institutions',8)

insert into WF_BusinessCode(businesscode,businessname,keyword,linktable,primaryfield,statefield,dowithurl,c_mkdm)
values('069','企业制度审核','InsCode','Institutions','InsCode','status','/oa/System/Institution/InstitutionView.aspx',28)

insert into wf_business_class(businesscode,businessclass,businessclassname)
values('069','001','企业制度审核')
--------------------- 制度移植结束


--价格类型添加权限人员字段 Bery 2011-4-12 
ALTER TABLE Res_PriceType ADD UserCodes nvarchar(max) DEFAULT '["00000000"]'
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'权限相关人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', 
@level1type=N'TABLE', @level1name=N'Res_PriceType', @level2type=N'COLUMN', @level2name=N'UserCodes'
--UPDATE Res_PriceType SET UserCodes = '["00000000"]' --给已经存在的数据添加默认管理员权限

--修改表 xpm_basic_contactcorp 中字段website fax类型长度 hzy 2011-04-13
alter table xpm_basic_contactcorp alter column website varchar(Max)
alter table xpm_basic_contactcorp alter column fax varchar(max)

--修改表 xpm_basic_contactcorp 中字段website fax类型长度 wmb 2011-04-26,加推荐商字段，其它字符串字段改为最大长度
alter table dbo.XPM_Basic_ContactCorp  add IsHot varchar(10) 

--alter table dbo.XPM_Basic_ContactCorp  alter column CorpName   nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column CorpName   nvarchar(4000) not null
alter  table   XPM_Basic_ContactCorp   add   constraint   pk_Basic_Con   primary   key(CorpName);
alter table dbo.XPM_Basic_ContactCorp  alter column Speciality nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Aptitude  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Capital   nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column UnderlayAbility   nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Address   nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column CorpBrief   nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Corporation  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column LinkMan  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Telephone  nvarchar(max)

alter table dbo.XPM_Basic_ContactCorp  alter column HandPhone  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Fax  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column ShopCard  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column TaxCard  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column AccountBank  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Zone  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column BankAccounts  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column PostCode  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column  WebSite nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column PeopleNumber  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Client  nvarchar(max)

alter table dbo.XPM_Basic_ContactCorp  alter column Owner  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column UserCode  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Brand  nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column  Email nvarchar(max)
alter table dbo.XPM_Basic_ContactCorp  alter column Contry  nvarchar(max)


--修改支出合同审核页面地址 Bery  2011-05-06
UPDATE WF_BusinessCode SET DoWithUrl = '/ContractManage/PayoutPayment/PaymentQuery.aspx'
WHERE BusinessCode = '084' 

--添加支出合同控制指标表 Bery  2011-05-11
IF OBJECT_ID(N'Con_Payout_Target', 'U') IS NOT NULL
    DROP TABLE Con_Payout_Target
GO
CREATE TABLE Con_Payout_Target
(
    TargetId nvarchar(500) primary key, --GUid
    ContractId nvarchar(64) REFERENCES Con_Payout_Contract(ContractId), --支出合同Id
    TaskId nvarchar(500) REFERENCES Bud_Task(TaskId), --任务节点Id
    SignAmount decimal(18, 3) --签订金额
)
GO
EXEC sp_addextendedproperty @name = 'MS_Description', @value = N'支出合同控制指标表', @level0type = N'SCHEMA', @level0name = N'dbo', 
    @level1type = 'TABLE', @level1name = N'Con_Payout_Target'
EXEC sp_addextendedproperty @name = 'MS_Description', @value = 'Guid', @level0type = 'SCHEMA', @level0name = 'dbo',
    @level1type = 'TABLE', @level1name = 'Con_Payout_Target', @level2type = 'COLUMN', @level2name = 'TargetId'
EXEC sp_addextendedproperty @name = 'MS_Description', @value = '支出合同Id', @level0type = 'SCHEMA', @level0name = 'dbo',
    @level1type = 'TABLE', @level1name = 'Con_Payout_Target', @level2type = 'COLUMN', @level2name = 'ContractId'
EXEC sp_addextendedproperty @name = 'MS_Description', @value = '任务节点Id', @level0type = 'SCHEMA', @level0name = 'dbo',
    @level1type = 'TABLE', @level1name = 'Con_Payout_Target', @level2type = 'COLUMN', @level2name = 'TaskId'
EXEC sp_addextendedproperty @name = 'MS_Description', @value = '签订金额', @level0type = 'SCHEMA', @level0name = 'dbo',
    @level1type = 'TABLE', @level1name = 'Con_Payout_Target', @level2type = 'COLUMN', @level2name = 'SignAmount'


--添加预算变更的标识列 Bery 2011-05-19
ALTER TABLE Bud_Task ADD Modified nvarchar(200) --'1'表示修改过

--编码库里添加任务类型 Bery 2011-05-19  13:47
IF ((SELECT COUNT(1) FROM XPM_Basic_CodeType WHERE SignCode = 'TaskType') = 0)
BEGIN
	INSERT INTO XPM_Basic_CodeType(TypeName, IsVisible, IsValid, Remark, 
		SignCode, Owner, VersionTime, ContractCropType) VALUES('任务类型',1,1,'任务类型','TaskType','0',GETDATE(),NEWID())
	DECLARE @typeId int
	SELECT @typeId = TypeId FROM XPM_Basic_CodeType WHERE SignCode = 'TaskType'
	INSERT INTO XPM_Basic_CodeList(CodeID,TypeID,ParentCodeID,ParentCodeList,CodeName,ChildNumber,IsFixed,IsDefault,IsValid,IsVisible,Owner,VersionTime) VALUES(1,@typeId,0,',1,','单位工程',0,0,0,1,1,'000000','2011-05-19 13:58:05.647')
	INSERT INTO XPM_Basic_CodeList(CodeID,TypeID,ParentCodeID,ParentCodeList,CodeName,ChildNumber,IsFixed,IsDefault,IsValid,IsVisible,Owner,VersionTime) VALUES(2,@typeId,0,',2,','分部工程',0,0,0,1,1,'000000','2011-05-19 13:58:14.680')
	INSERT INTO XPM_Basic_CodeList(CodeID,TypeID,ParentCodeID,ParentCodeList,CodeName,ChildNumber,IsFixed,IsDefault,IsValid,IsVisible,Owner,VersionTime) VALUES(3,@typeId,0,',3,','分项工程',0,0,0,1,1,'000000','2011-05-19 13:58:22.913')
	INSERT INTO XPM_Basic_CodeList(CodeID,TypeID,ParentCodeID,ParentCodeList,CodeName,ChildNumber,IsFixed,IsDefault,IsValid,IsVisible,Owner,VersionTime) VALUES(4,@typeId,0,',4,','明细',0,0,0,1,1,'000000','2011-05-19 13:58:30.367')
END


--支出合同里添加 是否为虚拟合同字段 syf  2011-05-20 9:54
alter table Con_Payout_Contract add fictitious int
update Con_Payout_Contract set fictitious=1 --1为不是虚拟合同 0 为是虚拟合同


--收入合同里添加 合同状态字段 syf 2011-05-20 10:34
alter table Con_Incomet_Contract add sign int--合同是否已签订  1 已签订 0 未签订


--供应商评分表    zyg   2011-05-23


CREATE TABLE [dbo].[Res_SuperGradeTab](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[superid] [varchar](30) NULL,
	[goodsid] [varchar](16) NULL,
	[billsid] [varchar](32) NULL,
	[gradePeopid] [varchar](8) NULL,
	[gradeTime] [datetime] NULL CONSTRAINT [DF_Res_SuperGradeTab_gradeTime]  DEFAULT (getdate()),
	[fileisall] [int] NULL,
	[numisover] [int] NULL,
	[lookisgood] [int] NULL,
	[tpyeisright] [int] NULL,
	[timeisquk] [int] NULL,
	[smallisok] [int] NULL,
 CONSTRAINT [PK_RES_SUPERGRADETAB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF

--仓库管理表增加关联项目字段 syf 2011-05-24 10:29
alter table Sm_Treasury add prjCode varchar(50)

--增加发货通知单表 syf 2011-05-26 8:48
/****** 对象:  Table [dbo].[sm_sendGoods]    脚本日期: 05/26/2011 08:46:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sm_sendGoods](
	[sgId] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[scode] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[snCode] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[number] [numeric](18, 3) NULL,
	[suppyCode] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_sm_sendGoods] PRIMARY KEY CLUSTERED 
(
	[sgId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'物资编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_sendGoods', @level2type=N'COLUMN', @level2name=N'scode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发货单编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_sendGoods', @level2type=N'COLUMN', @level2name=N'snCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'数量' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_sendGoods', @level2type=N'COLUMN', @level2name=N'number'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'供应商编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_sendGoods', @level2type=N'COLUMN', @level2name=N'suppyCode'



--增加发货通知单对应物资表 syf 2011-05-26 8:48
/****** 对象:  Table [dbo].[sm_SendNote]    脚本日期: 05/26/2011 08:46:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[sm_SendNote](
	[snId] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[snCode] [varchar](100) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[snAddTime] [datetime] NULL,
	[snAddUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[sendState] [int] NULL,
	[remark] [varchar](1000) COLLATE Chinese_PRC_CI_AS NULL,
	[prjCode] [uniqueidentifier] NULL,
 CONSTRAINT [PK_sm_SendNote] PRIMARY KEY CLUSTERED 
(
	[snId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'snId'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N' 编码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'snCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'snAddTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'snAddUser'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'接收状态 0 未接收  1 已接收' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'sendState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联项目' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'sm_SendNote', @level2type=N'COLUMN', @level2name=N'prjCode'


---修改发货通知单物资部 增加采购价格列 syf 2011-5-30 17:23
alter table sm_sendGoods add price decimal(18,3)



---增加部门表中的公共信息列：地址，邮编，传真   zyg   2011-05-30
ALTER TABLE PT_d_bm  add  adss CHAR(100),yb CHAR(10),fx CHAR(10)


--资源临时表，预算导入时，资源导入失败时存放 bery 2011-06-01 
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


---合同状态 syf 2011-06-08 9:55
--给支出合同增加一个合同状态列
--0 执行 1 暂停 2 保内 3 保外 4 解除 5 终止
alter table Con_Payout_Contract add conState int default 0

--给收入合同增加一个合同状态列
--0 执行 1 暂停 2 保内 3 保外 4 解除 5 终止
alter table Con_Incomet_Contract add conState int default 0

--给人员信息表增加列 syf 2011-6-9 14:30
--rdeNature --户籍性质
--conEndDate--最新合同终止日期
--urgentCellMan--紧急联系人
--ucmConcern--紧急联系人与本人关系
--ucmTel--紧急联系人电话
alter table PT_yhmc add rdeNature varchar(10),
conEndDate datetime,urgentCellMan varchar(20),ucmConcern varchar(50),ucmTel varchar(50)


--增加资金申请流程 syf 2011-6-13 11:30
insert into dbo.WF_BusinessCode values( 
'085','账户资金申请','id','fund_Reqinfo','reqNum','auditState',	
'/AccountManage/fund_Reqinfo/fund_ReqinfoView.aspx','',	'30')

insert into dbo.WF_Business_Class values('085','001','资金申请')


--给支出合同付款增加一个审核自定义事件（付款审核通过了给入账管理增加一条记录） syf 2011-6-14 13:08
insert into SelfEventInfo values('Con_Payout_Payment','accOperMSClass','AccountManage.BLL')

--给合同收款加一列,来判断此收款是否生成入账单 0 否 1 是 syf 2011-6-14 15:43
alter table Con_Incomet_Payment add state int default (0) 


---添加收支计划表 syf 2011-6-15
/****** 对象:  Table [dbo].[fund_InExPlan]    脚本日期: 06/15/2011 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fund_InExPlan](
	[ID] [nvarchar](64) NOT NULL,
	[IEPNum] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[IEPname] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[IEPtype] [int] NULL,
	[IEPdate] [datetime] NULL,
	[prjNum] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[state] [int] NULL,
 CONSTRAINT [PK_fund_InExPlan] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_InExPlan', @level2type=N'COLUMN',@level2name=N'IEPNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_InExPlan', @level2type=N'COLUMN',@level2name=N'IEPname'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划类型 0年度计划 1 季度计划 2 月度计划' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_InExPlan', @level2type=N'COLUMN',@level2name=N'IEPtype'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编制日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_InExPlan', @level2type=N'COLUMN',@level2name=N'IEPdate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_InExPlan', @level2type=N'COLUMN',@level2name=N'prjNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程审核状态' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_InExPlan', @level2type=N'COLUMN',@level2name=N'state'


--给资金支付计划增加审核流程  syf 2011-6-16 9:46
insert into dbo.WF_BusinessCode values(
'086','收支计划审核','ID','fund_InExPlan','id','state','/AccountManage/IncomeExpensePlan/InExPlanView.aspx',NULL,'30')
insert into dbo.WF_Business_Class values(
'086','001','收支计划审核')

--资金申请增加审核后的添加入账表中功能 wmb 2011-6-20 15:22
insert into SelfEventInfo values('fund_Reqinfo ','AccountCommit','AccountManageAoper')

--入帐表
GO
/****** 对象:  Table [dbo].[fund_AccountOperate]    脚本日期: 06/20/2011 15:24:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[fund_AccountOperate](
	[AoID] [int] IDENTITY(1,1) NOT NULL,
	[AccountNum] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Acredence] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AccounType] [int] NULL,
	[AccountMony] [decimal](18, 3) NULL,
	[RealMony] [decimal](18, 3) NULL,
	[DepID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[SumitMan] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[SumiTime] [datetime] NULL,
	[IsAccount] [int] NULL,
	[AccounTime] [datetime] NULL,
	[AccountMan] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[projnum] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[contracnum] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AccountMark] [text] COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_ACCOUNTOPERATE] PRIMARY KEY CLUSTERED 
(
	[AoID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'AccountNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'凭证' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'Acredence'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账类型 0 启动资金 1 合同款 2 拆借，3，其它' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'AccounType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'AccountMony'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账金额' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'RealMony'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'部门id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'DepID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'SumitMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提交时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'SumiTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否入账(0,否;1,是)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'IsAccount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'AccounTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'AccountMan'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'projnum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'contracnum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'fund_AccountOperate', @level2type=N'COLUMN',@level2name=N'AccountMark'



--流程超级删除功能完善 syf 2011-7-06 9:07
/****** 对象:  Table [dbo].[WF_supperDelete]    脚本日期: 07/06/2011 09:07:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[WF_supperDelete](
	[BusinessCode] [varchar](3) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[BussinessClass] [varchar](3) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[FatherId] [int] NULL CONSTRAINT [DF_WF_supperDelete_FatherId]  DEFAULT ((1)),
	[TableName] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[line] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[linkLine] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[linkTable] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'父键  如果此表还有外键，则继续添加，且此表的父键列为此列父键列的值' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WF_supperDelete', @level2type=N'COLUMN',@level2name=N'FatherId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外键表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WF_supperDelete', @level2type=N'COLUMN',@level2name=N'TableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'此外键表的列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WF_supperDelete', @level2type=N'COLUMN',@level2name=N'line'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'与此外键表对应的表中的对应列' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WF_supperDelete', @level2type=N'COLUMN',@level2name=N'linkLine'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'走流程的表' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'WF_supperDelete', @level2type=N'COLUMN',@level2name=N'linkTable'


INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '072','001',1,'Sm_Purchaseplan_Stock','ppcode','ppcode','Sm_Purchaseplan')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '081','001',1,'Con_Payout_Payment','ContractID','ContractID','Con_Payout_Contract')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '071','001',1,'Sm_Wantplan_Stock','wpcode','swcode','Sm_Wantplan')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '073','001',1,'dbo.Sm_Purchase_Stock','pscode','pcode','Sm_Purchase')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '074','001',1,'dbo.Sm_Storage_Stock','stcode','scode','Sm_Storage')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '075','001',1,'dbo.Sm_Allocation_Stock','acode','acode','Sm_Allocation')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '076','001',1,'dbo.Sm_out_Stock','orcode','orcode','Sm_OutReserve')
INSERT [WF_supperDelete] ([BusinessCode],[BussinessClass],[FatherId],[TableName],[line],[linkLine],[linkTable]) VALUES ( '077','001',1,'dbo.Sm_back_Stock','rcode','rcode','dbo.Sm_Refunding')
insert into WF_supperDelete values ('081','001','1','Con_Payout_Modify','contractid','contractid','con_payout_contract')
insert into WF_supperDelete values('081','001','1','Con_Payout_Balance','contractid','contractid','con_payout_contract')


--编码库中增加 实施项目部 syf 2011-7-06 14:12
insert into XPM_Basic_CodeType(TypeName, IsVisible, IsValid,remark,SignCode) 
values('实施项目部','true','true','实施项目部','xmb')

--2011-07-20 16:08 公告管理
update [PT_MK] set [V_CDLJ]='oa/Bulletin/BulletinManage.aspx?type=manage' where [C_MKDM]='280303'


---在考勤管理里面增加 旷工（Holiday8)  应到天数（FactDay） hykun 2011-07-20
alter table HR_Leave_Stat add Holiday8 decimal(4,1) default 0.0
alter table HR_Leave_Stat add FactDay decimal(4,1) default 0.0


--间接成本日记账  Bery 2011-07-25 14:55
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

--修改模块和权限 Bery 2011-07-26 09:30
ALTER TABLE [dbo].[PT_Manager_Privilege] DROP   CONSTRAINT [管理员权限-模块]
GO
ALTER TABLE [dbo].[PT_Manager_Privilege]  ADD  CONSTRAINT [管理员权限-模块] FOREIGN KEY([C_MKDM])
REFERENCES [dbo].[PT_MK] ([C_MKDM]) ON DELETE CASCADE ON UPDATE CASCADE
GO


---修改PT_d_CorpCode 字段的长度  syf 2011-07-26 14:09
alter table PT_d_CorpCode alter column  CorpName varchar(100)


-----PT_yhmc 增加 userCode 字段 syf 2011-07-29 13:04 
alter table dbo.PT_yhmc add userCode varchar(50) null


--递归查询资源类型  Bery 2011-08-01 11:35
IF OBJECT_ID('ufn_GetResourceType', 'IF') IS NOT NULL
	DROP FUNCTION ufn_GetResourceType
GO
CREATE FUNCTION ufn_GetResourceType(@ResourceTypeId nvarchar(200))
RETURNS TABLE
AS
RETURN
(
	WITH cteResourceType AS
	(
		SELECT ResourceTypeId, ParentId FROM Res_ResourceType
		WHERE ResourceTypeId = @ResourceTypeId
		UNION ALL 
		SELECT Res_ResourceType.ResourceTypeId, Res_ResourceType.ParentId FROM Res_ResourceType
		INNER JOIN cteResourceType ON Res_ResourceType.ParentId = cteResourceType.ResourceTypeId
	)
	SELECT cteResourceType.ResourceTypeId FROM cteResourceType
)
GO
--SELECT * FROM ufn_GetResourceType('6A1A7050-1F92-4291-B932-43E1DFCE6E91')--材料




------------gei 现场收货增加权限（发货时限定收货人） syf 2011-08-11 14:30
alter table sm_SendNote add limits varchar(1000) 


--Con_Payout_Contract表添加字段  lpw 2011-08-15 11:32
--ALTER TABLE Con_Payout_Contract ADD CapitalNumber varchar(200)--大写金额支持
--ALTER TABLE Con_Payout_Contract ADD financeNumber varchar(50) --财务项目编号
--ALTER TABLE Con_Payout_Contract ADD financeProject varchar(50) --财务合同编号

----Con_Payout_Payment表添加字段,  lpw 2011-08-15 11:32
--ALTER TABLE Con_Payout_Payment ADD PayType SMALLINT --支付类型(支票.现金....)
--ALTER TABLE Con_Payout_Payment ADD CapitalNumber nvarchar(1000) --大写金额




--审核间接成本日记账 bery 2011-08-17 16:08
insert dbo.WF_BusinessCode values('087','间接成本日记账','InDiaryId','Bud_IndirectDiaryCost','InDiaryId','FlowState','/BudgetManage/Cost/CostVerifyRecord.aspx',null,'29')
insert dbo.WF_Business_Class values('087','001','间接成本日记账审核')


--组织机构月度预算 Bery 2011-08-23 09:42
IF OBJECT_ID('Bud_OrganizationMonthBudget', 'U') IS NOT NULL
	DROP TABLE Bud_OrganizationMonthBudget
GO
CREATE TABLE Bud_OrganizationMonthBudget
(
	Id nvarchar(200) PRIMARY KEY, --GUID
	OrganizationBudget nvarchar(200) REFERENCES Bud_OrganizationBudget(Id) ON DELETE CASCADE, --月度预算Id	
	Year int NOT NULL, --年份
	Month int NOT NULL, --月份
	Amount decimal(18,3) --金额
)

--合同类型添加是否有效字段 Bery 2011-08-24 09:00
ALTER TABLE Con_ContractType ADD IsValid bit DEFAULT(1) --是否有效 默认有效

--甲供入库单添加附件 Bery 2011-08-24 14:47
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1806','FirstStorage','甲供入库单',8388608,'*','/UploadFiles/StockManage/FirstStorage/',8)

-- 物资需求.. 新增 倒库时间 lpw 2011年8月29日14:38:49
ALTER TABLE Sm_Wantplan_Stock ADD arrivalDate DATETIME;
ALTER TABLE Sm_Purchaseplan_Stock ADD arrivalDate DATETIME;
ALTER TABLE Sm_Purchase_Stock ADD arrivalDate DATETIME;
-----将项目状态分为 投标项目状态 和 规划项目状态 syf 2011-08-29 14:43
--update XPM_Basic_CodeType set TypeName='投标项目状态',Remark='规划项目状态' where typeid=7

--insert into XPM_Basic_CodeType(TypeName, IsVisible, IsValid, Remark, SignCode, Owner, VersionTime)
--values('规划项目状态','True','True','规划项目状态','prjState',	'000000','2011-8-29 0:00:00')


---公告管理修改   slm 2011-08-29

---修改公告视图


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[v_bulletin_list]
AS
SELECT     a.I_BULLETINID, a.CorpCode, b.v_bmmc as CorpName , a.V_RELUSERCODE, a.V_RELEASEUSER, a.V_TITLE, a.V_CONTENT, a.URL, a.DTM_RELEASETIME, 
                      a.DTM_EXPRIESDATE, a.I_RELEASEBOUND, a.DeptRange, a.AuditState, CONVERT(varchar(10), a.DTM_RELEASETIME, 20) AS rq
FROM         dbo.PT_BULLETIN_MAIN AS a inner JOIN
                      dbo.pt_d_bm AS b ON a.CorpCode = b.v_bmbm where b.c_sfyx='y'
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


---优化数据库内容  slm  2011-08-29
update pt_d_bm set V_bmbm='00' where i_bmdm=1


--标识该采购单是否是在合同页面添加的  Bery 2011-08-29 15:01
ALTER TABLE Sm_Purchase ADD IsConPurchase bit DEFAULT(0)

--修改支出合同审核页面 lhy 2011-09-05 09:15
UPDATE WF_BusinessCode SET DoWithUrl='/ContractManage/PayoutContract/ParyoutContractQuery.aspx' 
WHERE BusinessCode='081'

--支出合同关联的采购单与支出合同同时审核 lhy 2011-09-05 09:18
IF OBJECT_ID('trig_update_purchaseState','TR') IS NOT NULL
DROP TRIGGER trig_update_purchaseState
GO
CREATE TRIGGER [dbo].[trig_update_purchaseState]
   ON  [dbo].[Con_Payout_Contract]
   AFTER UPDATE
AS 
BEGIN
	DECLARE @contractId NVARCHAR(64),
    @flowState INT,
    @count INT
    SET @count=0
    SELECT @contractId=ContractId,@flowState=FlowState FROM INSERTED
    SELECT @count=COUNT(*) FROM Sm_Purchase WHERE [Contract]=@contractId
    IF(@count>0)
      BEGIN
       UPDATE dbo.Sm_Purchase SET Flowstate=@flowState WHERE [Contract]=@contractId
      END
END


--  设备管理 状态 原数据状态值和名称
-- lpw 2011年9月5日
set identity_insert [XPM_Basic_CodeList] ON--打开
INSERT [XPM_Basic_CodeList] ([NoteID],[CodeID],[TypeID],[ParentCodeID],[ParentCodeList],[CodeName],[ChildNumber],[IsFixed],[IsDefault],[IsValid],[IsVisible],[Owner],[VersionTime]) VALUES ( 401,1,152,0,',1,','正常状态',0,0,0,1,1,'000000','2011/9/2 16:35:30');
INSERT [XPM_Basic_CodeList] ([NoteID],[CodeID],[TypeID],[ParentCodeID],[ParentCodeList],[CodeName],[ChildNumber],[IsFixed],[IsDefault],[IsValid],[IsVisible],[Owner],[VersionTime]) VALUES ( 404,2,152,0,',2,','封存状态',0,0,0,1,1,'000000','2011/9/2 16:35:44');
INSERT [XPM_Basic_CodeList] ([NoteID],[CodeID],[TypeID],[ParentCodeID],[ParentCodeList],[CodeName],[ChildNumber],[IsFixed],[IsDefault],[IsValid],[IsVisible],[Owner],[VersionTime]) VALUES ( 403,3,152,0,',3,','报废状态',0,0,0,1,1,'000000','2011/9/2 16:35:12');
set identity_insert [XPM_Basic_CodeList] OFF--关闭
                                            --
----编码库中新增设备状态, lpw 2011年9月5日 编号ID定死
SET IDENTITY_INSERT [XPM_Basic_CodeType] ON
INSERT [XPM_Basic_CodeType] ([TypeID],[TypeName],[IsVisible],[IsValid],
[Remark],[SignCode],[Owner],[VersionTime],[ContractCropType])
 VALUES ( 152,N'设备状态',1,1,N'设备状态',N'sbzt',N'0',N'2011/9/2 15:57:56',N'91ed256c-126a-4db9-8d95-38b8793cd722')
SET IDENTITY_INSERT [XPM_Basic_CodeType] OFF                                            
                                            
                                            

--资源编码添加唯一健约束  Bery 2011-09-05 14:22
alter table dbo.Res_Resource add constraint UQ_ResourceCode unique(ResourceCode)

--CBSCode 添加唯一健约束 Bery 2011-09-05 15:37
IF OBJECT_ID('uq_CBSCode', 'UQ') IS NOT NULL
	ALTER TABLE Bud_CostAccounting DROP CONSTRAINT uq_CBSCode
GO
ALTER TABLE Bud_CostAccounting ADD CONSTRAINT uq_CBSCode UNIQUE(CBSCode)

--组织机构费用日记账 Bery 2011-09-05 15:39
IF OBJECT_ID('Bud_OrgDiaryCost', 'U') IS NOT NULL
	DROP TABLE Bud_OrgDiaryCost
GO
CREATE TABLE Bud_OrgDiaryCost
(
	OrgDiaryId nvarchar(200) PRIMARY KEY,
	OrgId int REFERENCES PT_d_bm(i_bmdm) 
		ON DELETE CASCADE ON UPDATE CASCADE, --组织机构Id
	Name nvarchar(200) NOT NULL, --费用项目
	Department nvarchar(200), --发生单位
	IssuedBy nvarchar(200) NOT NULL, --经手人
	FlowState int NOT NULL DEFAULT(-1), --流程状态
	InputUser nvarchar(200) NOT NULL, --录入人
	InputDate datetime NOT NULL DEFAULT(GETDATE()) --录入时间
)
--组织机构费用日记账明细
IF OBJECT_ID('Bud_OrgDiaryDetails', 'U') IS NOT NULL
	DROP TABLE Bud_OrgDiaryDetails
GO
CREATE TABLE Bud_OrgDiaryDetails
(
	OrgDiaryDetailsId nvarchar(200) PRIMARY KEY,
	OrgDiaryId nvarchar(200) REFERENCES Bud_OrgDiaryCost(OrgDiaryId)
		ON DELETE CASCADE ON UPDATE CASCADE, --费用日记账Id
	Name nvarchar(200) NOT NULL, --名称
	Amount decimal(18,3) NOT NULL DEFAULT(0.0), --金额
	CBSCode nvarchar(200) NOT NULL REFERENCES Bud_CostAccounting(CBSCode)
		ON DELETE CASCADE ON UPDATE CASCADE,
	Note nvarchar(max) --摘要
)

-- 在甲供入库材料表中增加 验收情况 lpw 2011年9月7日13:27:42
alter   table   Sm_Storage_Stock   add   checkCondition   VARCHAR(50); 
-- 在入库信息中增加 移交人 监理 lpw 2011年9月7日13:27:42
ALTER TABLE Sm_Storage ADD trustee VARCHAR(50);
ALTER TABLE Sm_Storage ADD supervisor VARCHAR(50);

--在项目信息管理表中增加 联系电话 业务经理 等级 jzm 2011年9月8号
ALTER TABLE PT_PrjInfo ADD telephone VARCHAR(20);
ALTER TABLE PT_PrjInfo ADD grade VARCHAR(50);
ALTER TABLE PT_PrjInfo ADD businessman VARCHAR(50);

--组织机构审核 Bery 2011-09-09 14:13
insert dbo.WF_BusinessCode values('088','组织机构日记账','OrgDiaryId','Bud_OrgDiaryCost','OrgDiaryId','FlowState','/BudgetManage/Cost/OrgVerifyRecord.aspx',null,'29',null,'Name')
insert dbo.WF_Business_Class values('088','001','组织机构日记账审核')

-- 设备台账新增所属项目字段 lpw 2011-9-9 17:10:56
if exists (select * from sysobjects where id = OBJECT_ID('[Ent_Ept_Equipments_Stock]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [Ent_Ept_Equipments_Stock]
CREATE TABLE [Ent_Ept_Equipments_Stock] (
[EquipmentUniqueCode] [varchar]  (64) NOT NULL,
[project] [varchar]  (64) NULL)


-----------项目查询时显示项目编号  syf 2011-09-14
update Rep_Main set selectsql='select * from (select TypeCode as "序号", prjname as "项目名称",owner as "建设单位",prjcost as "造价",prjplace as "项目地点",startdate as "开始日期",enddate as "结束日期",prjstate as "状态" ,prjCode as "项目编号" from PT_PrjInfo where IsValid=1) a' where ReportID='888888'

-----设备检定提醒  slm 20110914
insert into  PT_D_TXLX (V_LXBM,V_LXMC,V_TPLJ,V_DBLJ,C_OPENFLAG,FilterField) values('023','设备检定提醒','new_Mail.gif','',1,'EquipmentManagement')

--收入合同变更-工程确认单 Bery 2011-09-15 09:56
IF OBJECT_ID('Con_IncomeModify_Technology', 'U') IS NOT NULL
	DROP TABLE Con_IncomeModify_Technology
GO
CREATE TABLE Con_IncomeModify_Technology
(
	Id int PRIMARY KEY IDENTITY, --主键
	ConModifyId nvarchar(64) REFERENCES Con_Incomet_Modify(ID)
		ON DELETE CASCADE ON UPDATE CASCADE, --收入合同变更的ID
	TechnologyId int REFERENCES Prj_TechnologyManage(ID)
		ON DELETE CASCADE ON UPDATE CASCADE --单据ID
)
GO
--导航>> 科技进步管理 下增加  (工程确认单,工程洽商单 ) lpw 2011年9月15日
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'9140',N'工程确认单',N'y',22,2218,2,N'0',N'0',N'1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'914001',N'工程确认单',N'EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx&Type=Edit&PrjState=0&Levels=7',N'y',1,2219,0,N'0',N'0',N'1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'914002',N'工程确认单查询',N'EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx&Type=View&PrjState=0&Levels=7',N'y',2,2220,0,N'0',N'0',N'1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'9150',N'工程洽商单',N'y',22,2222,2,N'0',N'0',N'1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'915001',N'工程洽商单',N'EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx&Type=Edit&PrjState=0&Levels=8',N'y',1,2223,0,N'0',N'0',N'1')
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'915002',N'工程洽商单查询',N'EPC/17/Frame.aspx?url=../../epc/17/Ppm/ScienceInnovate/EngineerConfirmList.aspx&Type=View&PrjState=0&Levels=8',N'y',2,2224,0,N'0',N'0',N'1')
--资源属性引用外键资源分类修改 2011-9-19
GO
ALTER TABLE [dbo].[Res_Attribute]  WITH CHECK ADD FOREIGN KEY([ResourceTypeId])
REFERENCES [dbo].[Res_ResourceType] ([ResourceTypeId]) ON DELETE CASCADE 


--修改物资模块  Bery  2011-09-22 13:20
--入库单添加确认入库时间
ALTER TABLE Sm_Storage ADD isintime datetime 
--调拨单添加确认调拨时间
ALTER TABLE Sm_Allocation ADD isouttime datetime 
--调拨单添加确认接受时间
ALTER TABLE Sm_Allocation ADD isintime datetime 
--出库单添加确认出库时间
ALTER TABLE Sm_OutReserve ADD isouttime datetime 
--调拨单添加确认退库时间
ALTER TABLE Sm_Refunding ADD isintime datetime 

--仓库库存表添加入库类型字段  Bery  2011-09-23 
ALTER TABLE Sm_Treasury_Stock ADD Type char(1);
GO
IF OBJECT_ID('C_Type', 'C') IS NOT  NULL
	ALTER TABLE Sm_Treasury_Stock DROP CONSTRAINT C_Type
GO
ALTER TABLE Sm_Treasury_Stock ADD CONSTRAINT C_Type
	CHECK (Type IN ('I', 'S', 'F', 'A', 'B','T')) 
	--I：初始化，S：入库，F：甲供入库，A：调拨入库，B：退库，T：盘存
GO

--EPM_Datum_Affair中新增一列CA 标示 lpw 2011年9月23日9:56:18  
ALTER TABLE EPM_Datum_Affair ADD CA INT

--统一资源类型  Bery  2011-09-23 13:36
UPDATE PT_MK SET V_MKMC='资源属性' 
WHERE C_MKDM = '991005'
AND V_MKMC = '类别属性'

---项目信息 外键表 包含(联系电话 等级 联系人) 2011年9月27日8:57:52
if exists (select * from sysobjects where id = OBJECT_ID('[PT_PrjInfo_ZTB_Stock]') and OBJECTPROPERTY(id, 'IsUserTable') = 1) 
DROP TABLE [PT_PrjInfo_ZTB_Stock]
CREATE TABLE [PT_PrjInfo_ZTB_Stock] (
[PrjGuid] [uniqueidentifier] NOT NULL,
[grade] [varchar]  (64) NULL,
[businessman] [varchar]  (64) NULL,
[telephone] [varchar]  (64) NULL)

-----项目立项提醒   slm   20110927
insert into  PT_D_TXLX (V_LXBM,V_LXMC,V_TPLJ,V_DBLJ,C_OPENFLAG,FilterField) values('024','项目立项提醒','new_Mail.gif','',1,'ProjectList')



--增加盘点结存表单表 2011-10-08
IF OBJECT_ID (N'Sm_Stocktake', N'U') IS NOT NULL
DROP TABLE Sm_Stocktake;
GO
CREATE TABLE Sm_Stocktake
(
    StocktakeId nvarchar(500) primary key, --盘点单Id
	StocktakeCode nvarchar(500) not null,    --盘点单编码
    StocktakeName nvarchar(500) not null,    --盘点单名称
    TreasuryCode nvarchar(512) REFERENCES Sm_Treasury(Tcode) ON DELETE CASCADE,--仓库编码
    StocktakeDate nvarchar(6) not null,--盘点年月，如：201109
	InputUser nvarchar(500) not null, --盘点人
	InputDate dateTime DEFAULT(GETDATE()), --插入时间
    EndDate dateTime, --盘点截至日期
    State int not null,--盘点状态:0 为挂起；1 保存；2 锁定
    LockDate dateTime, --锁定盘点的时间
    Note nvarchar(MAX)--备注
)
GO



--增加盘点结存物资表
IF OBJECT_ID (N'Sm_Stocktake_Detail', N'U') IS NOT NULL
DROP TABLE Sm_Stocktake_Detail;
GO
CREATE TABLE Sm_Stocktake_Detail
(
    DetailId nvarchar(500) primary key, --Id
    StocktakeId nvarchar(500) REFERENCES Sm_Stocktake(StocktakeId) ON DELETE CASCADE,--盘点单Id
    ResourceCode nvarchar(500) not null, --物资编码
    ResourceName nvarchar(500) not null, --物资名称
    Specification nvarchar(1000) not null, --规格
    Unit  nvarchar(500) not null, --单位
    Price decimal(18,3) not null, --价格
    SupplierId nvarchar(1000),--供应商Id
    Supplier nvarchar(1000) not null,--供应商
    LastMonthNum decimal(18,3) not null,--上期结余
    StorageNum decimal(18,3) not null,--入库数量
    FirstStorageNum decimal(18,3) not null,--甲供入库数量
    OutReserveNum decimal(18,3) not null, --出库数量
    TransferringInNum decimal(18,3) not null, --调拨入库
    TransferringOutNum decimal(18,3) not null, --调拨出库
    RefundingNum decimal(18,3) not null,--退库
    BookNum decimal(18,3) not null,--账面金额
    StocktakeNum decimal(18,3) not null, --盘点数量
	InputDate dateTime DEFAULT(GETDATE()), --插入时间
    Note nvarchar(MAX)--备注
)
GO

--冒泡提醒的个人设置 Bery 2011-10-10 17:21:00
IF OBJECT_ID('PopupSetting', 'U') IS NOT NULL
	DROP TABLE PopupSetting
GO
CREATE TABLE PopupSetting (
	UserCode varchar(8) REFERENCES PT_yhmc(v_yhdm) ON DELETE CASCADE, --用户代码
	Module nvarchar(30), --模块
	IsValid bit NOT NULL DEFAULT(0), --是否提醒
	PRIMARY KEY(UserCode, Module) --主键（用户代码+模块）
)
GO
--记录已经提醒过的记录
IF OBJECT_ID('PopupRecord', 'U') IS NOT NULL
	DROP TABLE PopupRecord
GO
CREATE TABLE PopupRecord (
	Id int PRIMARY KEY IDENTITY,
	UserCode varchar(8) REFERENCES PT_yhmc(v_yhdm) ON DELETE CASCADE, --用户代码
	Module nvarchar(30), --模块
	PopupId nvarchar(200), --提醒的Id
	PopupTime datetime DEFAULT(GETDATE()) --提醒时间
)
GO
--流程审核添加项目关联字段 Bery 2011-10-10 17:20:45
ALTER TABLE WF_BusinessCode ADD ProjectField varchar(50) NULL
GO
UPDATE WF_BusinessCode SET ProjectField='PrjCode' WHERE BusinessCode='023'
UPDATE WF_BusinessCode SET ProjectField='ProjectCode' WHERE BusinessCode='026'
UPDATE WF_BusinessCode SET ProjectField='ProjectCode' WHERE BusinessCode='028'
UPDATE WF_BusinessCode SET ProjectField='ProjectCode' WHERE BusinessCode='030'
UPDATE WF_BusinessCode SET ProjectField='PrjCode' WHERE BusinessCode='032'
UPDATE WF_BusinessCode SET ProjectField='PrjCode' WHERE BusinessCode='034'
UPDATE WF_BusinessCode SET ProjectField='PrjCode' WHERE BusinessCode='036'
UPDATE WF_BusinessCode SET ProjectField='PrjId' WHERE BusinessCode='040'
UPDATE WF_BusinessCode SET ProjectField='PrejectName' WHERE BusinessCode='050'
UPDATE WF_BusinessCode SET ProjectField='procode' WHERE BusinessCode='071'
UPDATE WF_BusinessCode SET ProjectField='project' WHERE BusinessCode='072'
UPDATE WF_BusinessCode SET ProjectField='Project' WHERE BusinessCode='073'
UPDATE WF_BusinessCode SET ProjectField='project' WHERE BusinessCode='074'
UPDATE WF_BusinessCode SET ProjectField='procode' WHERE BusinessCode='076'
UPDATE WF_BusinessCode SET ProjectField='procode' WHERE BusinessCode='077'
UPDATE WF_BusinessCode SET ProjectField='PrjGuid' WHERE BusinessCode='081'
UPDATE WF_BusinessCode SET ProjectField='PrjNum' WHERE BusinessCode='085'
UPDATE WF_BusinessCode SET ProjectField='prjNum' WHERE BusinessCode='086'
UPDATE WF_BusinessCode SET ProjectField='ProjectId' WHERE BusinessCode='087'


-------桌面模块 几个表 syf 2011-10-11 14:32
------------------------------------------------begin--------------------------------------------------------------------------------------------------------------------
/****** 对象:  Table [dbo].[frame_Desktop_UserSet]    脚本日期: 10/11/2011 14:30:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[frame_Desktop_UserSet](
	[userCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[GirdColNum] [int] NOT NULL,
	[RowInGrid] [int] NOT NULL,
	[GirdWidth] [int] NOT NULL,
	[ColGapWidth] [int] NOT NULL CONSTRAINT [DF_frame_Desktop_UserSet_ColGapWidth]  DEFAULT ((20)),
	[HideNullGrid] [nchar](1) COLLATE Chinese_PRC_CI_AS NULL,
	[RowGapWidth] [int] NULL CONSTRAINT [DF_frame_Desktop_UserSet_RowGapWidth]  DEFAULT ((10)),
 CONSTRAINT [PK_frame_Desktop_UserSet] PRIMARY KEY CLUSTERED 
(
	[userCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号：default' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'userCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'列数,可选范围2~8' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'GirdColNum'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表格里行数,可选范围5~10' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'RowInGrid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'GirdWidth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目列间隙宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'ColGapWidth'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'隐藏空栏目；默认不隐藏' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'HideNullGrid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'栏目行间隙宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserSet', @level2type=N'COLUMN',@level2name=N'RowGapWidth'


/****** 对象:  Table [dbo].[frame_Desktop_UserModel]    脚本日期: 10/11/2011 14:30:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[frame_Desktop_UserModel](
	[userCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ModelId] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[orderId] [int] NULL,
	[MNewName] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_frame_Desktop_UserModel] PRIMARY KEY CLUSTERED 
(
	[userCode] ASC,
	[ModelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserModel', @level2type=N'COLUMN',@level2name=N'userCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserModel', @level2type=N'COLUMN',@level2name=N'ModelId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'排序编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserModel', @level2type=N'COLUMN',@level2name=N'orderId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'模板对应新名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_UserModel', @level2type=N'COLUMN',@level2name=N'MNewName'


/****** 对象:  Table [dbo].[frame_Desktop_ModelInfo]    脚本日期: 10/11/2011 14:30:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[frame_Desktop_ModelInfo](
	[ModelID] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[tableName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[colName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[colTime] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[selWhere] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[moreSrc] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[nameSrc] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[colId] [varchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[Remark] [varchar](64) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_frame_Desktop_ModelInfo] PRIMARY KEY CLUSTERED 
(
	[ModelID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应模块ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'ModelID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询的表名' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'tableName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段1--显示名称的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'colName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'字段2--显示时间的' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'colTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'条件语句' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'selWhere'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点击 更多 的时候连接的页面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'moreSrc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'点击 字段1 时连接的页面' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'nameSrc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'查询表的主键' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_ModelInfo', @level2type=N'COLUMN',@level2name=N'colId'



INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colName],[colTime],[selWhere],[moreSrc],[nameSrc],[colId],[Remark]) VALUES ( '280303','[v_bulletin_list]','v_title','dtm_releasetime','where dtm_expriesdate>getdate()-1 and auditstate=1 order by DTM_RELEASETIME desc','/oa/Bulletin/BulletinManage.aspx?type=see','/oa/bulletin/BulletinLock.aspx?ic=','i_bulletinid','公告2011.9添加')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colName],[colTime],[selWhere],[moreSrc],[nameSrc],[colId],[Remark]) VALUES ( '281103','Institutions','InsName','writedate','where status=1 and isvalid=1 order by writedate desc','/oa/System/Institution/InstitutionListSearch.aspx','/oa/System/Institution/InstitutionView.aspx?ic=','insCode','2011.9添加')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colName],[colTime],[selWhere],[moreSrc],[nameSrc],[colId],[Remark]) VALUES ( '2827','Web_News','v_xwbt','dtm_fbsj','WHERE (c_xwlxdm = 99) AND c_sfyx =''y''  order by  dtm_fbsj desc','/WEB/WebManagerList.aspx?c_xwlxdm=99&c_xwlxmc=公司新闻&browse=true','/WEB/WebSel.aspx?cd=99&nid=','i_xw_id','2011.9添加')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[colName],[colTime],[moreSrc],[colId],[Remark]) VALUES ( '283818','BusinessClassName','rq','/EPC/WorkFlow/PTAuditList.aspx','NoteID','2011.9添加')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colName],[colTime],[selWhere],[moreSrc],[colId],[Remark]) VALUES ( '2801','[PT_DBSJ]','[V_Content]','convert(varchar(10),DTM_DBSJ,20) as DTM_DBSJ','where [V_YHDM] =@yhdm and datediff(ss,DTM_DBSJ,getdate())>0 and v_dblj != '' ORDER BY [DTM_DBSJ] desc','/SysFrame/PTDBSJList.aspx','[I_DBSJ_ID]','2011.9添加')





INSERT [frame_Desktop_UserSet] ([userCode],[GirdColNum],[RowInGrid],[GirdWidth],[ColGapWidth],[HideNullGrid],[RowGapWidth]) VALUES ( 'default',3,6,320,10,'n',10)
INSERT [frame_Desktop_UserSet] ([userCode],[GirdColNum],[RowInGrid],[GirdWidth],[ColGapWidth],[HideNullGrid],[RowGapWidth]) VALUES ( 'defaultold',2,5,280,10,'n',10)



INSERT [frame_Desktop_UserModel] ([userCode],[ModelId],[orderId],[MNewName]) VALUES ( 'default','280303',1,'公告管理')
INSERT [frame_Desktop_UserModel] ([userCode],[ModelId],[orderId],[MNewName]) VALUES ( 'default','281103',2,'制度查询')
INSERT [frame_Desktop_UserModel] ([userCode],[ModelId],[orderId],[MNewName]) VALUES ( 'default','2827',0,'内部新闻')
INSERT [frame_Desktop_UserModel] ([userCode],[ModelId],[orderId],[MNewName]) VALUES ( 'default','283818',0,'待审流程列表')
INSERT [frame_Desktop_UserModel] ([userCode],[ModelId],[orderId],[MNewName]) VALUES ( 'default','2801',0,'待办工作')

----------------------------------------------------------End-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

-------------------桌面显示外部链接  syf 2011.10.17 13:00
insert into frame_Desktop_ModelInfo values('381705','dbo.frame_Desktop_Weblink','WebName','AddTime','where userCode=@yhdm','/TableTop/WebLink.aspx','','LinkID','2011.10.14 添加外部链接')

-------添加外部链接模块 syf 2011.10.17 13:10
INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( '381705','外部快捷连接','/TableTop/WebLink.aspx','y',5,2257,0,'1','0','1')

-------添加外部模块表  syf 2011.10.17 13:12
/****** 对象:  Table [dbo].[frame_Desktop_Weblink]    脚本日期: 10/17/2011 13:12:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[frame_Desktop_Weblink](
	[LinkID] [int] IDENTITY(1,1) NOT NULL,
	[userCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[orderId] [int] NULL,
	[WebName] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[WebAddr] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[Remark] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AddTime] [datetime] NULL CONSTRAINT [DF_frame_Desktop_Weblink_AddTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_frame_Desktop_Weblink] PRIMARY KEY CLUSTERED 
(
	[LinkID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_Weblink', @level2type=N'COLUMN',@level2name=N'userCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷连接名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_Weblink', @level2type=N'COLUMN',@level2name=N'WebName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'快捷连接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'frame_Desktop_Weblink', @level2type=N'COLUMN',@level2name=N'WebAddr'

--Bery 2011-10-20 12:44:13
ALTER TABLE dbo.Sm_Stocktake ADD BeginDate DATETIME

--资金支付申请 Bery  2011-10-21 16:25:00
IF OBJECT_ID('Con_Income_PaymentApply', 'U') IS NOT NULL
	DROP TABLE Con_Income_PaymentApply
GO
CREATE TABLE Con_Income_PaymentApply
(
	PaymentId nvarchar(64) PRIMARY KEY, 
	PaymentCode nvarchar(64) NOT NULL, --支付申请编码
	ContractId nvarchar(64) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE, --合同ID
	PaymentPenson nvarchar(64) NOT NULL, --申请人
	PaymentAmount decimal(18,3) NOT NULL, --申请金额
	PaymentDate datetime NOT NULL, --要求支付日期
	PaymentMode nvarchar(20) NOT NULL, --支付方式
	FlowState int NOT NULL, --流程状态
	Notes nvarchar(max) NOT NULL, --备注
	InputPerson nvarchar(64) NOT NULL, --录入人
	InputDate datetime NOT NULL DEFAULT(GETDATE()), --录入时间
	ContainPending bit DEFAULT(0) --是否包含待审数据
)

-------------将附件里关联项目编号的改为关联对应的项目guid码   syf  2011.10.26 13:10
update XPM_Basic_AnnexList set recordCode = B.PrjGuid from XPM_Basic_AnnexList A, PT_PrjInfo B where A.recordCode = B.typeCode and A.moduleid = 18;


--资金支付申请  Bery  2011-10-28 16:25:41
--收入合同收款添加附件 
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1913','AddIncometPaymentApply','收入合同资金支付',8388608,'*','/UploadFiles/ContractManage/AddIncometPaymentApply/',8)
--收入合同资金支付
insert dbo.WF_BusinessCode values('101','收入合同资金支付','PaymentId','Con_Income_PaymentApply','PaymentId','FlowState','/ContractManage/PaymentApply/PaymentApplyQuery.aspx',null,'05',null)
insert dbo.WF_Business_Class values('101','001','收入合同资金支付')

--审核记录审核内容查看  Bery  2011-10-28 16:26:25
ALTER TABLE WF_BusinessCode ADD NameField varchar(50) NULL
GO
UPDATE WF_BusinessCode SET NameField='V_TITLE' WHERE BusinessCode='002'
UPDATE WF_BusinessCode SET NameField='SupplyPlanCode' WHERE BusinessCode='022'
UPDATE WF_BusinessCode SET NameField='StockInBillCode' WHERE BusinessCode='024'
UPDATE WF_BusinessCode SET NameField='OutBillCode' WHERE BusinessCode='026'
UPDATE WF_BusinessCode SET NameField='BackBillCode' WHERE BusinessCode='028'
UPDATE WF_BusinessCode SET NameField='OutBillCode' WHERE BusinessCode='030'
UPDATE WF_BusinessCode SET NameField='PlanCode' WHERE BusinessCode='032'
UPDATE WF_BusinessCode SET NameField='PlanCode' WHERE BusinessCode='036'
UPDATE WF_BusinessCode SET NameField='CorpName' WHERE BusinessCode='042'
UPDATE WF_BusinessCode SET NameField='CorpName' WHERE BusinessCode='044'
UPDATE WF_BusinessCode SET NameField='CorpName' WHERE BusinessCode='045'
UPDATE WF_BusinessCode SET NameField='InsName' WHERE BusinessCode='069'
UPDATE WF_BusinessCode SET NameField='swcode' WHERE BusinessCode='071'
UPDATE WF_BusinessCode SET NameField='ppcode' WHERE BusinessCode='072'
UPDATE WF_BusinessCode SET NameField='pcode' WHERE BusinessCode='073'
UPDATE WF_BusinessCode SET NameField='scode' WHERE BusinessCode='074'
UPDATE WF_BusinessCode SET NameField='acode' WHERE BusinessCode='075'
UPDATE WF_BusinessCode SET NameField='orcode' WHERE BusinessCode='076'
UPDATE WF_BusinessCode SET NameField='rcode' WHERE BusinessCode='077'
UPDATE WF_BusinessCode SET NameField='ContractCode' WHERE BusinessCode='081'
UPDATE WF_BusinessCode SET NameField='ModifyCode' WHERE BusinessCode='082'
UPDATE WF_BusinessCode SET NameField='BalanceCode' WHERE BusinessCode='083'
UPDATE WF_BusinessCode SET NameField='PaymentCode' WHERE BusinessCode='084'
UPDATE WF_BusinessCode SET NameField='IEPNum' WHERE BusinessCode='086'
UPDATE WF_BusinessCode SET NameField='Name' WHERE BusinessCode='087'
UPDATE WF_BusinessCode SET NameField='PaymentCode' WHERE BusinessCode='101'
UPDATE WF_BusinessCode SET NameField='Title' WHERE BusinessCode='999'




-------------桌面内部链接  syf  2011.10.31 13：37

/****** 对象:  Table [dbo].[frame_Desktop_Menulink]    脚本日期: 10/31/2011 13:33:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[frame_Desktop_Menulink](
	[userCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ModelId] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[orderId] [int] NULL,
	[MNewName] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[addTime] [datetime] NULL,
 CONSTRAINT [PK_frame_Desktop_Menulink] PRIMARY KEY CLUSTERED 
(
	[userCode] ASC,
	[ModelId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

------------显示桌面内部链接  syf  2011.10.31  13:37
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colTime],[moreSrc],[Remark]) VALUES ( '381703','frame_Desktop_Menulink','addTime','/TableTop/menuList.aspx','2011.10.19 添加菜单链接')



--------------项目状态 未中标 可用 syf 2011.10.31 14:58
update XPM_Basic_CodeList set isValid=1 where noteId=349

--成本分析 CBS分解 必有数据 2011.11.04  ZFJ 
IF OBJECT_ID('Bud_CostAccounting', 'U') IS NOT NULL
BEGIN
IF((SELECT COUNT(*) FROM Bud_CostAccounting)=0)
	BEGIN 
	DELETE FROM Bud_CostAccounting
	INSERT INTO Bud_CostAccounting VALUES('5eb43a75-5e5c-4335-9634-b3b6a8f8c63f','001','核算成本','','','')
	INSERT INTO Bud_CostAccounting VALUES('82321993-c6dc-43c7-a8f8-3775c02b9d0c','001001','直接成本','D','','')
	INSERT INTO Bud_CostAccounting VALUES('6f5712ca-0816-4da3-8e66-367d40806cae','001002','间接成本','I','','')
	END
END
--模板节点资源删除删除且节点下的资源 2011.11.08 ZFJ
IF OBJECT_ID('Bud_TemplateResource','U') IS NOT NULL
BEGIN
	BEGIN TRY
		ALTER TABLE Bud_TemplateResource  WITH CHECK ADD FOREIGN KEY(ResourceId)
		REFERENCES Res_Resource (ResourceId)	
		ON UPDATE CASCADE
		ON DELETE CASCADE 
	END TRY
	BEGIN CATCH
		PRINT ERROR_MESSAGE() 
	END CATCH
END

--资源库的资源按资源编码降序排列  SXH  2011.11.08 14:15
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go


ALTER PROC [dbo].[cpResource]
@pageSize int,
@pageIndex int,
@ResourceTypeId nvarchar(500)
AS
DECLARE @pCount int
DECLARE @t1 table
(
	number int,
	typeName nvarchar(500)
)
INSERT INTO @t1(number,typeName)
SELECT ROW_NUMBER() OVER(ORDER BY PriceTypeId) as number, '[' + PriceTypeName + ']' as typeName FROM Res_PriceType
DECLARE @index int
DECLARE @count int
DECLARE @str nvarchar(max)
SET @pCount = (@pageIndex - 1) * @pageSize
SET @index = 1
SELECT @count= COUNT(PriceTypeId) FROM Res_PriceType
SET @str = ''
WHILE(@index <= @count)
BEGIN
	SET @str = @str + ',' + (SELECT typeName FROM @t1 WHERE number = @index) 
	SET @index = @index+1
END
SET @str = SUBSTRING(@str,2,LEN(@str))
EXEC('
SELECT TOP('+ @pageSize +') * FROM 
(
	SELECT ROW_NUMBER() OVER(ORDER BY InputDate) AS Number,* FROM 
	(
		SELECT r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand,
		  r.TaxRate,r.Specification,r.ResourceType,r.InputDate,
		  r.TechnicalParameter, r.Series, r.ModelNumber,r.Note,
		  u.Name,p.PriceValue,pt.PriceTypeName,c.CorpName 
		FROM Res_Price AS p
		RIGHT JOIN Res_Resource AS r ON p.ResourceId = r.ResourceId
		LEFT JOIN  Res_PriceType AS pt ON pt.PriceTypeId = p.PriceTypeId
		LEFT JOIN Res_Unit AS u ON u.UnitId = r.Unit
		LEFT JOIN XPM_Basic_ContactCorp AS c ON c.CorpID = r.SupplierId
		GROUP BY r.ResourceId,r.ResourceCode,r.ResourceName,r.Brand,
		  r.TaxRate,r.Specification,r.ResourceType,r.InputDate,
		  r.Series, r.ModelNumber,r.Note,c.CorpName,
		  r.TechnicalParameter,u.Name,p.PriceValue,pt.PriceTypeName
	) rtt
	PIVOT 
	(
		MAX(PriceValue) FOR PriceTypeName in (' + @str + ')
	)
	AS pvt
) AS st
WHERE Number > '+@pCount+' AND ResourceType = '''+ @ResourceTypeId +'''
ORDER BY ResourceCode DESC')
                    
--施工保留添加缺失字段 Bery 2011-11-09 15:34
--核算量
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Bud_ConsTask'
	AND COLUMN_NAME = 'AccountingQuantity') = 0
ALTER TABLE Bud_ConsTask ADD AccountingQuantity decimal(18,3)
--核算量
--IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
--	WHERE TABLE_NAME = 'Bud_ConsTaskRes'
--	AND COLUMN_NAME = 'AccountingQuantity') = 0
--ALTER TABLE Bud_ConsTask ADD AccountingQuantity decimal(18,3)
--CBS编码
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Bud_ConsTaskRes'
	AND COLUMN_NAME = 'CBSCode') = 0
ALTER TABLE Bud_ConsTask ADD AccountingQuantity nvarchar(200)


--------------现场收货增加 现场收货情况说明 字段  syf 2011.11.10 10:36
alter table dbo.sm_receiveNote add explain varchar(2000) NOT NULL

-------------------给收货验收增加流程审核 syf 2011.11.10 15:43--------------Begin------------------------------
alter table dbo.sm_receiveNote add AuditState int default -1
--增加流程
insert into dbo.WF_Business_Class values('096','001','现场收货审核')
insert into dbo.WF_BusinessCode values('096','现场收货审核','rnId','sm_receiveNote','rnId','AuditState','/StockManage/receiveGoods/ViewReceiveNote.aspx','','03','','scode')
--增加自定义事件
insert into SelfEventInfo values('sm_receiveNote','receiveSelf','cn.justwin.stockBLL')
-----------------------------------------------------------------------------End---------------------------------------------

--审核记录审核内容查看补充   sxh  2011-11-11 9:45 
UPDATE  WF_BusinessCode set NameField='SchemeName' where BusinessCode='050'
UPDATE  WF_BusinessCode set NameField='TCO_Name' where BusinessCode='023'
UPDATE  WF_BusinessCode set NameField='Name' where BusinessCode='088'


--支出合同关联的采购单与支出合同同时审核 lhy  2011-11-11 12:17
IF OBJECT_ID('trig_update_purchaseState','TR') IS NOT NULL
DROP TRIGGER trig_update_purchaseState
GO
CREATE TRIGGER [dbo].[trig_update_purchaseState]
   ON  [dbo].[Con_Payout_Contract]
   AFTER UPDATE
AS 
BEGIN
	DECLARE @contractId NVARCHAR(64),
    @flowState INT,
    @count INT
    SET @count=0
    SELECT @contractId=ContractId,@flowState=FlowState FROM INSERTED
    SELECT @count=COUNT(*) FROM Sm_Purchase WHERE [Contract]=@contractId
    IF(@count>0)
      BEGIN
       UPDATE dbo.Sm_Purchase SET Flowstate=@flowState WHERE [Contract]=@contractId
      END
END
GO

--修改域名访问错误  Bery  2011-11-17 10:08
ALTER TABLE pt_Log ALTER COLUMN V_USERIP NVARCHAR(200)


-------------------现场收货增加提醒  syf  2011-11-17 16:46
insert into PT_D_TXLX values ('026','现场收货提醒','','',1,'')
alter table PT_DBSJ_Today alter column V_DBLJ varchar(1000)


------------修改设备计划审核时关联的查看页面 syf 2011-11-24 14:10
update WF_BusinessCode set DoWithUrl='/EPC/EquipmentManagement/Plan/equipmentPlanView.aspx' where businessCode='032'


--合同类型添加科目关联 Bery 2011-11-29 10:00
ALTER TABLE Con_ContractType ADD CBSCode nvarchar(200) NULL 
	REFERENCES Bud_CostAccounting(CBSCode) ON DELETE SET NULL

--支出合同关联资金计划 Bery 2011-11-29 10:00
ALTER TABLE Con_Payout_Contract ADD MonthPlanUID uniqueidentifier
--收入合同关联资金计划 Bery 2011-11-29 10:00
ALTER TABLE Con_Incomet_Contract ADD MonthPlanUID uniqueidentifier

------------修改桌面（给桌面增加六个内部应用）  syf 2011-11-30 10:16
--------begin-------------
alter table frame_Desktop_Menulink add sequence varchar(2)
update frame_Desktop_Menulink set sequence=1
update frame_Desktop_ModelInfo set moreSrc='/TableTop/menuList.aspx?op=1' where modelid='381703'


INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colTime],[moreSrc],[Remark]) VALUES ( '381707','frame_Desktop_Menulink','addTime','/TableTop/menuList.aspx?op=2','内部应用二')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colTime],[moreSrc],[Remark]) VALUES ( '381708','frame_Desktop_Menulink','addTime','/TableTop/menuList.aspx?op=3','内部应用三')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colTime],[moreSrc],[Remark]) VALUES ( '381709','frame_Desktop_Menulink','addTime','/TableTop/menuList.aspx?op=4','内部应用四')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colTime],[moreSrc],[Remark]) VALUES ( '381710','frame_Desktop_Menulink','addTime','/TableTop/menuList.aspx?op=5','内部应用五')
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colTime],[moreSrc],[Remark]) VALUES ( '381711','frame_Desktop_Menulink','addTime','/TableTop/menuList.aspx?op=6','内部应用六')

----默认设置增加一种类型
INSERT [frame_Desktop_UserSet] ([userCode],[GirdColNum],[RowInGrid],[GirdWidth],[ColGapWidth],[HideNullGrid],[RowGapWidth]) VALUES ( 'defaultNew',3,9,400,30,'y',30)
---------end-------------

--Bery  2011-12-06 10:59
ALTER TABLE Con_Payout_Contract DROP COLUMN MonthPlanUID 
ALTER TABLE Con_Incomet_Contract DROP COLUMN MonthPlanUID 
--支出合同支付关联资金计划
ALTER TABLE Con_Payout_Payment ADD MonthPlanUID uniqueidentifier
--收入合同支付关联资金计划
ALTER TABLE Con_Incomet_Payment ADD MonthPlanUID uniqueidentifier

--人力资源添加以往工作表现 Bery  2011-12-13 09:57
ALTER TABLE PT_yhmc ADD PastPerformance nvarchar(200) --以往工作表现

--收入合同添加审核  Bery  2011-12-19 10:35
ALTER TABLE Con_Incomet_Contract ADD FlowState int DEFAULT(-1)
--首次启用收入合同审核时需要执行
UPDATE Con_Incomet_Contract SET FlowState = -1 WHERE FlowState IS NULL

--收入合同添加签订人字段  Bery  2011-12-20 10:46
ALTER TABLE Con_Incomet_Contract ADD SignPeople varchar(8) References PT_yhmc(v_yhdm)

--收入合同添加审核   2011年12月26日 09:01:11
insert dbo.WF_BusinessCode values('103','收入合同','ContractID',
        'Con_Incomet_Contract','ContractID','FlowState',
        '/ContractManage/IncometContract/IncometContractQuery.aspx',null,
        '05','Project','ContractCode')
insert dbo.WF_Business_Class values('103','001','收入合同审核')

--施工保量添加核算量字段
GO
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Bud_ConsTaskRes'
	AND COLUMN_NAME = 'AccountingQuantity') = 0
ALTER TABLE Bud_ConsTaskRes ADD AccountingQuantity decimal(18,3);
GO

 -- 获取资源的根节点资源类型 函数 ZFJ 2011-12-29
IF OBJECT_ID (N'ufn_GetRootResTypeId', N'FN') IS NOT NULL
    DROP FUNCTION ufn_GetRootResTypeId;
GO
CREATE FUNCTION ufn_GetRootResTypeId(@ResourceId VARCHAR(100)) 
RETURNS VARCHAR(100)
BEGIN
	DECLARE @RootResTypeId VARCHAR(100);
	WITH cteResType AS
		(
			SELECT ResourceTypeId, ParentId, ResourceTypeCode, ResourceTypeName
			FROM Res_ResourceType
			WHERE ResourceTypeId = (SELECT ResourceType FROM Res_Resource WHERE ResourceId=@ResourceId)
			UNION ALL 
			SELECT RT.ResourceTypeId, RT.ParentId, RT.ResourceTypeCode, RT.ResourceTypeName
			FROM Res_ResourceType AS RT
			INNER JOIN cteResType ON RT.ResourceTypeID = cteResType.ParentId
		)
	SELECT @RootResTypeId=ResourceTypeId FROM cteResType AS ResType
	RETURN @RootResTypeId
END
GO

--孙新华  合同支付删除不用的方法    2011-12-31  14:07
DELETE FROM SelfEventInfo WHERE classname='accOperMSClass'
GO

----------------桌面内部应用一点击更多调出页面修改  syf 2012-01-05 9:35
update dbo.frame_Desktop_ModelInfo set moreSrc='/TableTop/menuList.aspx?op=1' where modelid='381703'

--------------- 技术总结审核连接修改  slm 2011-1-6
update WF_BusinessCode set DoWithUrl='/EPC/17/Ppm/ScienceInnovate/ScienceInnovateSumEdit.aspx?Type=view' where BusinessCode='040'

-----------公告视图修改 slm 2012-1-9

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[v_bulletin_list]
AS
SELECT     a.I_BULLETINID, a.CorpCode, a.V_RELUSERCODE, a.V_RELEASEUSER, a.V_TITLE, a.V_CONTENT, a.URL, a.DTM_RELEASETIME, 
                      a.DTM_EXPRIESDATE, a.I_RELEASEBOUND, a.DeptRange, a.AuditState, CONVERT(varchar(10), a.DTM_RELEASETIME, 20) AS rq
FROM         dbo.PT_BULLETIN_MAIN AS a
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO

--流程老数据处理添加sql (流程更新之前一定要备份数据库）  孙新华 2012.1.12 16:45
--INSERT [PT_MK] ([C_MKDM],[V_MKMC],[V_CDLJ],[C_BS],[I_XH],[I_ID],[i_ChildNum],[IsBasic],[IsMaintainable],[Isdisplay]) VALUES ( N'4811',N'流程老数据处理',N'EPC/Workflow/FlowQuestion.aspx',N'y',11,2358,0,N'0',N'0',N'1')

--INSERT PT_YHMC_Privilege VALUES('00000000','4811','0','0')
--GO


------------项目账户还款的时候，保存的还款编号和新增时显示的不一样的问题  syf 2012-01-13 11:37
alter table dbo.Fund_Prj_Loan_Return alter column FR_Code nchar(14)



--更新 安全措施和安全目标 连接地址 lpw 2012-01-13 
--安全措施
UPDATE PT_MK SET	V_CDLJ = 'EPC/Frame.aspx?Url=../../EPC/QuaitySafety/ProjectSupervise.aspx&Flag=S&Type=Edit&TypeId=6&CA=6&PrjState=0' WHERE C_MKDM='940903'
UPDATE PT_MK SET V_CDLJ = 'EPC/Frame.aspx?Url=../../EPC/QuaitySafety/ProjectSupervise.aspx&Flag=S&Type=List&TypeId=6&CA=6&PrjState=0' WHERE C_MKDM='940906'
	
--安全目标	
UPDATE PT_MK SET V_CDLJ = 'EPC/Frame.aspx?Url=../../EPC/QuaitySafety/SafetyMeasure/MeasureList.aspx&Type=Edit&PrjState=0' WHERE C_MKDM='942601'
UPDATE PT_MK SET V_CDLJ = 'EPC/Frame.aspx?Url=../../EPC/QuaitySafety/SafetyMeasure/MeasureList.aspx&Type=List&PrjState=0' WHERE C_MKDM='942602'


--更新字段类型的大小 解决安全质量备注提示字符限制问题.
--EPM_Datum_Affair  2012年1月15日 lpw
ALTER TABLE EPM_Datum_Affair
ALTER COLUMN Remark varchar(MAX)
ALTER TABLE EPM_Datum_Affair
ALTER COLUMN Context varchar(MAX)

--删除资料管理无用字段  Bery  2012-01-31 17:05
IF (SELECT COUNT(*) FROM information_schema.columns 
	WHERE TABLE_NAME = 'F_FileInfo' AND COLUMN_NAME = 'FileState') = 1
	ALTER TABLE F_FileInfo DROP COLUMN FileState
GO
--资料管理添加字段  存储回收站中记录祖先节点信息（JSON对象）Bery  2012-01-31 17:05
ALTER TABLE F_FileInfo ADD AncestorInfo nvarchar(max)
GO

--收入合同添加质保期  Bery  2012-02-03 14:51
ALTER TABLE Con_Incomet_Contract ADD QualityPeriod nvarchar(200)
GO
--资料管理查询删除节点的祖先信息 2012-02-03 15:30
IF OBJECT_ID('uspGetChildrenFoler','P') IS NOT NULL
DROP PROC uspGetChildrenFoler
GO
--个人资料管理 获取目录下的所有子项（包含目录和文件）
CREATE PROCEDURE [dbo].[uspGetChildrenFoler]
	@parentId nvarchar(100)
AS
BEGIN
	DECLARE @Ids VARCHAR(MAX)
	SET @Ids='''';
	WITH Children AS
	(
		SELECT Id,ParentId,FileNewName,FileName,FileType FROM F_PersonalFile WHERE Id=@parentId 
		UNION ALL
		SELECT T1.Id,T1.ParentId,T1.FileNewName,T1.FileName,T1.FileType FROM F_PersonalFile AS T1
		INNER JOIN Children ON Children.Id=T1.ParentId
	)
	SELECT @Ids=@Ids+Id+''',''' FROM Children
	SELECT LEFT(@Ids,LEN(@Ids)-2)
END
GO

--更改WF_RoleUsers中的CorpCode类型，满足部门相关的需要   孙新华  2012-02-07 13:23
ALTER TABLE WF_RoleUsers ALTER COLUMN CorpCode varchar(4000)
GO

--添加预警表    Bery   2012-02-29
IF OBJECT_ID('PT_Warning') IS NOT NULL
	DROP TABLE PT_Warning
GO
CREATE TABLE PT_Warning
(
	WarningId int IDENTITY PRIMARY KEY,
	WarningTitle nvarchar(200),			--标题
	WarningContent nvarchar(max),		--内容
	UserCode nvarchar(20),				--用户代码
	RelationsTable nvarchar(100),		--关联表的名称
	RelationsColumn nvarchar(200),		--关联表的字段名称
	RelationsKey nvarchar(200),			--关联的键值
	URI nvarchar(400),					--超链接地址
	IsValid bit, 						--是否有效
	InputDate datetime DEFAULT GETDATE()--录入时间
)
GO

--资源类别添加只读选项  Bery    2012-03-02 15:12
ALTER TABLE InstitutionClass ADD ReadOnly bit NOT NULL
	CONSTRAINT DF_ReadOnly DEFAULT(0)
	
--资金管理项目相关报错  孙新华  2012-03-05 15:12
UPDATE WF_BusinessCode SET ProjectField='prjGuid' WHERE BusinessCode='093'
UPDATE WF_BusinessCode SET ProjectField='prjGuid' WHERE BusinessCode='094'
UPDATE WF_BusinessCode SET ProjectField='prjGuid' WHERE BusinessCode='098'
GO	

--视图：查询每个任务节点的预算小计  Bery    2012-03-08 
IF OBJECT_ID('vTaskSum') IS NOT NULL
	DROP VIEW vTaskSum
GO
CREATE VIEW vTaskSum
AS
SELECT TaskId, SUM(ResourceQuantity * ResourcePrice) AS Sum
FROM Bud_TaskResource
GROUP BY TaskId
GO

--取消审核原因	Bery	2012-03-12
ALTER TABLE Bud_ConsReport ADD CancelAuditReason nvarchar(max)
GO

--收入合同添加返还日期	Bery	2012-03-12
ALTER TABLE Con_Incomet_Contract ADD RefundDate datetime

--需求计划添加设计编码 Bery 2012-03-15
ALTER TABLE Sm_Wantplan_Stock ADD DesignCode nvarchar(100)


--添加流程模板权限表 Bery 2012-03-19
IF OBJECT_ID('WF_Template_Privilege') IS NOT NULL
	DROP TABLE WF_Template_Privilege
GO
CREATE TABLE WF_Template_Privilege
(
		PrivilegeId int IDENTITY PRIMARY KEY,
	TemplateId int CONSTRAINT WF_Tem_Pri_TemplateId 
		REFERENCES WF_Template(TemplateId) ON DELETE CASCADE,	--模板ID
	UserCode varchar(8) CONSTRAINT WF_Tem_Pri_UserCode
		REFERENCES PT_yhmc(v_yhdm) ON DELETE CASCADE 			--用户编号
)
GO

--支出合同添加字段  Bery 2012-03-19
ALTER TABLE Con_Payout_Payment ADD Beneficiary nvarchar(200) --收款单位
ALTER TABLE Con_Payout_Payment ADD Bank nvarchar(200) --收款单位
ALTER TABLE Con_Payout_Payment ADD Account nvarchar(200) --收款单位'
GO

--删除无用的流程模板  Bery  2012-03-19
IF ((SELECT COUNT(*) FROM PT_MK WHERE C_MKDM = '30') = 0)
BEGIN
	BEGIN TRY
		BEGIN TRAN
			DELETE FROM WF_Business_Class WHERE BusinessCode = 
				(SELECT BusinessCode FROM WF_BusinessCode WHERE LinkTable = 'fund_Reqinfo')
			DELETE FROM WF_Template  WHERE BusinessCode = 
				(SELECT BusinessCode FROM WF_BusinessCode WHERE LinkTable = 'fund_Reqinfo')
			DELETE FROM WF_BusinessCode WHERE LinkTable = 'fund_Reqinfo'
			
			DELETE FROM WF_Business_Class WHERE BusinessCode = 
				(SELECT BusinessCode FROM WF_BusinessCode WHERE LinkTable = 'fund_InExPlan')
			DELETE FROM WF_Template  WHERE BusinessCode = 
				(SELECT BusinessCode FROM WF_BusinessCode WHERE LinkTable = 'fund_InExPlan')
			DELETE FROM WF_BusinessCode WHERE LinkTable = 'fund_InExPlan'
		COMMIT TRAN
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		PRINT ERROR_MESSAGE()
	END CATCH
END
GO

--维护流程  Bery  2012-03-19
IF OBJECT_ID('[流程流转从-主]') IS NOT NULL
BEGIN
	ALTER TABLE WF_Instance DROP [流程流转从-主]
	ALTER TABLE WF_Instance ADD CONSTRAINT [流程流转从-主]
	FOREIGN KEY([ID]) REFERENCES [dbo].[WF_Instance_Main] ([ID]) ON DELETE CASCADE
END

--公告日志  Bery 2012-03-21
IF OBJECT_ID('PT_BULLETIN_LOG') IS NOT NULL
	DROP TABLE PT_BULLETIN_LOG
GO
CREATE TABLE PT_BULLETIN_LOG
(
	LogId int IDENTITY PRIMARY KEY,		--日志ID
	BulletinId uniqueidentifier CONSTRAINT PT_BULLETIN_LOG_BulletinId
		REFERENCES PT_BULLETIN_MAIN(I_BULLETINID) ON DELETE CASCADE,--公告ID
	UserCode varchar(8) CONSTRAINT PT_BULLETIN_LOG_UserCode	
		REFERENCES PT_yhmc(v_yhdm) ON DELETE CASCADE,		--用户编号
	InputDate datetime					--录入时间
)

--流程权限列表添加管理员默认权限  孙新华  2012-03-23 11:22
INSERT INTO WF_Template_Privilege (TemplateId,UserCode)
SELECT TemplateID,'00000000' FROM WF_Template 
GO

--桌面添加预警模块代码  孙新华 2012-03-23 11:32
INSERT [frame_Desktop_ModelInfo] ([ModelID],[tableName],[colName],[colTime],[selWhere],[moreSrc],[nameSrc],[colId],[Remark]) VALUES ( '2842','PT_Warning','WarningTitle','InputDate',NULL,'/oa/Warning/WarningList.aspx',NULL,'WarningId','2012.2添加')

--菜单   Bery   2012-03-23 11:32
--预警提醒菜单
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('2842','预警提醒','/oa/Warning/WarningList.aspx','y',14,'',2369,0,'0','0','','1')
--模块人员菜单
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('3806','模块人员','oa/SysAdmin/Modules/ModulesLimit.aspx','y',6,'',2370,0,'0','0','','1')
--公告查看记录菜单
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('280306','公告查看记录','oa/Bulletin/BulletinUserQuery.aspx','y',6,'',2393,0,'0','0','','1')


--取消上报原因	Bery  2012-03-23 14:22
ALTER TABLE Bud_ConsReport ADD CancelReportReason nvarchar(max)
GO

--资金管理流程字段更新  孙新华  2012-03-27 15:46
UPDATE WF_BusinessCode SET NameField='PayOutCode' WHERE BusinessCode IN (094,098)
UPDATE WF_BusinessCode SET NameField='FR_Code' WHERE BusinessCode IN (102)
GO


--解决历史遗留问题	Bery	流程超时处理  2012-03-28
UPDATE WF_TemplateNode SET DueMode = 2
WHERE DueMode = 'N' OR DueMode IS NULL

--桌面代办工作改为待办工作显示错误问题   孙新华  2012-03-28 17:31
UPDATE frame_Desktop_UserModel SET MNewName='待办工作' 
WHERE userCode='default' AND ModelId=2801
GO

--解决资金计划错误	Bery(胡经理给的SQL)	2012-04-05	
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[cont_sum_paid]
AS
SELECT     ContractID, ContractName, BName,  ModifiedMoney AS contMoney,
      (SELECT     SUM(PaymentMoney) AS Expr1
        FROM          dbo.Con_Payout_Payment
        WHERE      (FlowState = 1) AND (ContractID = c.ContractID)) AS PaymentMoney,
      (SELECT     SUM(BalanceMoney) AS Expr1
        FROM          dbo.Con_Payout_Balance
        WHERE      (FlowState = 1) AND (ContractID = c.ContractID)) AS BalanceMoney
FROM         dbo.Con_Payout_Contract AS c
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


--删除SelfEventInfo, 使用XML文件    Bery  2012-04-16 
DROP TABLE SelfEventInfo
GO

--预算变更表    Bery  2012-04-19 14:06
IF OBJECT_ID('Bud_TaskChange') IS NOT NULL
    DROP TABLE Bud_TaskChange
GO
CREATE TABLE Bud_TaskChange
(
    TaskChangeId nvarchar(200) PRIMARY KEY,	
    PrjId nvarchar(200),		--项目GUID
    Version int,				--关联Bud_Task.Version
    VersionCode nvarchar(200),	--版本号
    FlowState int,				--流程状态
    Note nvarchar(max),			--备注
    InputUser varchar(8) REFERENCES PT_Yhmc(v_yhdm) ON DELETE SET NULL,
    InputDate datetime DEFAULT(GETDATE())
)
GO

--添加初始化数据       2012-04-19 14:06
IF (SELECT COUNT(*) FROM Bud_TaskChange) = 0
BEGIN
	WITH cteBudTask AS
	(
		SELECT DISTINCT PrjId, Version
		FROM Bud_Task
	)
	INSERT INTO Bud_TaskChange
	SELECT NEWID(), PrjId, Version, 'V' + CAST(Version AS NVARCHAR(4)) + '.0', 1, '', NULL, GETDATE()
	FROM cteBudTask
END
UPDATE Bud_TaskChange SET FlowState = '-1'
WHERE NOT EXISTS (
	SELECT * FROM Bud_PrjTaskInfo 
	WHERE Bud_PrjTaskInfo.PrjId = Bud_TaskChange.PrjId
	AND IsLocked = 1
)
AND FlowState= 1
GO

-- 触发器   该项目下第一次配置WBS时, 向Bud_TaskChange添加数据    2012-04-19 14:06
IF OBJECT_ID('trigInsertBudTask') IS NOT NULL
	DROP TRIGGER trigInsertBudTask
GO
CREATE TRIGGER trigInsertBudTask
	ON Bud_Task AFTER INSERT
AS
BEGIN
	IF ((SELECT COUNT(*) FROM Bud_TaskChange,inserted 
			WHERE Bud_TaskChange.PrjId = inserted.PrjId) = 0)
		INSERT INTO Bud_TaskChange
		SELECT NEWID(), PrjId, 1, 'V1.0', -1, '', NULL, GETDATE() FROM inserted 
END
GO

--视图, 获取项目的可用预算版本号            2012-04-19 14:07
--必须再次关联Bud_TaskChange, 查出TaskChangeId, EF必须能自动推算出主键列
IF OBJECT_ID('vGetCurBudVersion') IS NOT NULL
	DROP VIEW vGetCurBudVersion
GO
CREATE VIEW vGetCurBudVersion
AS
WITH cteTaskChange
AS
(
	SELECT PrjId, MAX(Version) AS CurVersion
	FROM Bud_TaskChange
	WHERE FlowState = 1
	GROUP BY PrjId
)
SELECT T.TaskChangeId, C.PrjId, C.CurVersion
FROM Bud_TaskChange AS T
JOIN cteTaskChange AS C ON C.PrjId = T.PrjId
	AND T.Version = C.CurVersion


--预算变更添加审核流程  Bery    2012-04-20
INSERT INTO WF_BusinessCode VALUES ('109','预算审核','TaskChangeId','Bud_TaskChange','TaskChangeId','FlowState',
                                    '/BudgetManage/Budget/BudTaskView.aspx',null,'13','PrjId','VersionCode')
INSERT INTO WF_Business_Class VALUES ('109','001','预算审更核')

--删除原进度管理触发器      2012-04-20
IF OBJECT_ID('trig_update_budtask','TR') IS NOT NULL
DROP TRIGGER trig_update_budtask
GO
IF OBJECT_ID ('trig_insert_budtask','TR') IS NOT NULL
DROP TRIGGER trig_insert_budtask
GO
IF OBJECT_ID ('trig_delete_budtask','TR') IS NOT NULL 
DROP TRIGGER trig_delete_budtask
GO 
IF OBJECT_ID ('trig_insert_Bud_ConsTask','TR') IS NOT NULL
DROP TRIGGER trig_insert_Bud_ConsTask
GO
IF OBJECT_ID ('trig_update_Bud_ConsTask','TR') IS NOT NULL
DROP TRIGGER trig_update_Bud_ConsTask
GO 
IF OBJECT_ID ('trig_delete_Bud_ConsTask','TR') IS NOT NULL
DROP TRIGGER trig_delete_Bud_ConsTask
GO
IF OBJECT_ID ('trig_insert_update_delete_Bud_TaskResource','TR') IS NOT NULL
DROP TRIGGER trig_insert_update_delete_Bud_TaskResource
GO 
IF OBJECT_ID ('trig_update_EPM_Task_TaskList','TR') IS NOT NULL
DROP TRIGGER trig_update_EPM_Task_TaskList
GO
IF OBJECT_ID ('trig_delete_EPM_Task_TaskList','TR') IS NOT NULL
DROP TRIGGER trig_delete_EPM_Task_TaskList

--删除进度管理相关表
GO 
--IF OBJECT_ID ('EPM_Task_TaskList','U') IS NOT NULL
--DROP TABLE EPM_Task_TaskList 
--GO
IF OBJECT_ID ('EPM_Task_Resource','U') IS NOT NULL
DROP TABLE EPM_Task_Resource
GO
IF OBJECT_ID('EPM_Book_ConstructTask','U') IS NOT NULL
DROP TABLE EPM_Book_ConstructTask
GO 
IF OBJECT_ID('EPM_Book_Resource','U') IS NOT NULL
DROP TABLE EPM_Book_Resource
--GO
--IF OBJECT_ID('EPM_Task_TaskRelation','U') IS NOT NULL
--DROP TABLE EPM_Task_TaskRelation
--GO
--IF OBJECT_ID('EPM_UF_Successor','TF') IS NOT NULL
--DROP FUNCTION EPM_UF_Successor

--合同模块中添加独立的权限管理菜单	Bery	2012-05-03 09:30:00
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('0513','权限管理','','y',13,'',2404,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('051301','合同类型','/ContractManage/ContractType/Permission.aspx','y',1,'',2405,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('051302','支出合同','/ContractManage/PayoutContract/Permission.aspx','y',2,'',2406,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('051303','收入合同','/ContractManage/IncometContract/Permission.aspx','y',3,'',2407,0,'0','0','','1')


--视图：查询每个节点的每次上报小计  Bery  2012-05-04 17:00:33
IF OBJECT_ID('vConsTaskSum') IS NOT NULL
	DROP VIEW vConsTaskSum
GO
CREATE VIEW vConsTaskSum 
AS
SELECT CT.TaskId, CR.InputDate, CT.AccountingQuantity,
	(UnitPrice * CTR.AccountingQuantity) AS Sum
FROM Bud_ConsTaskRes AS CTR
LEFT JOIN Bud_ConsTask AS CT ON CT.ConsTaskId = CTR.ConsTaskId
LEFT JOIN Bud_ConsReport AS CR ON CR.ConsReportId = CT.ConsReportId
GO


--移植施工日志	Bery	2012-05-04 17:52:44
CREATE TABLE [dbo].[OPM_EPCM_BuildDiary](
	[UID] [uniqueidentifier] NOT NULL,
	[SN] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PrjID] [uniqueidentifier] NOT NULL,
	[IsValid] [char](1) COLLATE Chinese_PRC_CI_AS NULL DEFAULT 1,
        [Sfgl] [char](1) COLLATE Chinese_PRC_CI_AS NULL DEFAULT 1,
	[AddUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AddTime] [datetime] NULL,
	[Remark] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Fsrq] [datetime] NULL,
	[Yjqk] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Ysqk] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Sjbg] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Cljc] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Jsjd] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Zljj] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Clsj] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Wbhy] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Sjjc] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Bzrq] [datetime] NULL,
	[Aqcl] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Qtqk] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Jbr] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Cemperature2] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Cemperature8] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Cemperature14] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Cemperature20] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AmWeather] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PmWeather] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[FlowState] [int] NULL,
	[Shyj] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
	[Type] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Record] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_OPM_EPCM_BuildDiary] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键施工日志ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工日志编号:用户登录ID+YYYYMMDD' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'SN'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'PrjID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'表示项目是否有效，0表示无效，1表示有效
   ' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'IsValid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否关联工程量清单' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Sfgl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'AddUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'添加时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'AddTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Remark'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'发生日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Fsrq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'预检情况' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Yjqk'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'验收情况' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Ysqk'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'设计变更' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Sjbg'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'材料进场' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Cljc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'技术交底' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Jsjd'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'资料交接' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Zljj'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'材料送检' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Clsj'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'外部会议' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Wbhy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上级检查' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Sjjc'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'编制日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Bzrq'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'安全处理' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Aqcl'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'其它情况' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Qtqk'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Jbr'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'2时温度' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Cemperature2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'8时温度' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Cemperature8'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'14时温度' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Cemperature14'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'20时温度' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Cemperature20'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上午天气' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'AmWeather'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'下午天气' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'PmWeather'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程状态(-3：重报，-2：驳回，-1：未提交，0：审核中，1：已审核，其它为未审核)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'FlowState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核意见' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Shyj'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'日志类型(角色类型，业主:yz;施工方:sgf;监理:jl)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Type'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary', @level2type=N'COLUMN', @level2name=N'Record'
GO
/****** 对象:  Table [dbo].[OPM_EPCM_BuildDiary_mx]    脚本日期: 03/26/2012 16:06:41 ******/


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OPM_EPCM_BuildDiary_mx](
	[UID] [uniqueidentifier] NOT NULL,
	[BDID] [uniqueidentifier] NULL,
	[TaskCode] [varchar](60) COLLATE Chinese_PRC_CI_AS NULL,
	[TaskName] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[WorkGroup] [varchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WorkersCount] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[jdqk] [varchar](2000) COLLATE Chinese_PRC_CI_AS NULL,
	[BuildPosition] [varchar](2000) COLLATE Chinese_PRC_CI_AS NULL,
	[Remark] [varchar](MAX) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_OPM_EPCM_BuildDiary_mx] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工日志明细ID，主键' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'UID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工日志外键' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'BDID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务代码预留' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'TaskCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'任务名称，直接录入' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'TaskName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作班组' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'WorkGroup'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作人数' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'WorkersCount'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'进度情况' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'jdqk'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'施工部位' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'BuildPosition'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_BuildDiary_mx', @level2type=N'COLUMN', @level2name=N'Remark'
GO

--施工日志添加审核		Bery	2012-05-04 17:53:59
INSERT dbo.WF_BusinessCode VALUES('110','施工日志审核','UID',
			'OPM_EPCM_BuildDiary','UID','FlowState',
			'/EPC/BuildDiary/DiaryInfoView.aspx',null,
			'91','PrjID','SN')
INSERT dbo.WF_Business_Class VALUES('110','001','施工日志审核')
GO
--施工日志菜单      2012-05-04 14:00
UPDATE PT_MK SET V_CDLJ='/EPC/BuildDiary/PrjFrame.aspx?businessType=BuildDiaryList&opType=edit'
WHERE C_MKDM='910201'
UPDATE PT_MK SET V_CDLJ='/EPC/BuildDiary/PrjFrame.aspx?businessType=BuildDiaryList&opType=view'
WHERE C_MKDM='910202'
GO

--添加合同预算审核字段		Bery    2012-05-07 09:44
IF OBJECT_ID('DF_BudPrjTaskInfo_ConFlowState') IS NOT NULL
	ALTER TABLE Bud_PrjTaskInfo DROP CONSTRAINT DF_BudPrjTaskInfo_ConFlowState
GO
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Bud_PrjTaskInfo' AND COLUMN_NAME = 'ConFlowState') = 1
	ALTER TABLE Bud_PrjTaskInfo DROP COLUMN ConFlowState
GO	
ALTER TABLE Bud_PrjTaskInfo ADD ConFlowState int 
	CONSTRAINT DF_BudPrjTaskInfo_ConFlowState DEFAULT(1) NOT NULL

--盘点结存添加审核功能		Bery    2012-05-07 09:44
IF OBJECT_ID('DF_SmStocktake_FlowState') IS NOT NULL
	ALTER TABLE Sm_Stocktake DROP CONSTRAINT DF_SmStocktake_FlowState
GO
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'Sm_Stocktake' AND COLUMN_NAME = 'FlowState') = 1
	ALTER TABLE Sm_Stocktake DROP COLUMN FlowState
GO	
ALTER TABLE Sm_Stocktake ADD FlowState int 
	CONSTRAINT DF_SmStocktake_FlowState DEFAULT(1) NOT NULL

--删除无用的存储过程    Bery    2012-05-07 09:52
IF OBJECT_ID('uspGetProject2') IS NOT NULL
	DROP PROCEDURE uspGetProject2
GO


--项目检查添加审核状态	Bery	2012-05-07 14:32
ALTER TABLE Prj_ItemInspect ADD UID nvarchar(200) NOT NULL CONSTRAINT DF_Prj_ItemInspect_UID DEFAULT(NEWID())
GO
ALTER TABLE Prj_ItemInspect ADD FlowState int NOT NULL CONSTRAINT DF_Prj_ItemInspect_FlowState DEFAULT(-1)
GO
UPDATE Prj_ItemInspect SET FlowState = 1 
GO
INSERT dbo.WF_BusinessCode VALUES('112','项目检查审核','UID', 'Prj_ItemInspect','UID','FlowState',
			'/EPC/17/Entpm/PrjCheck/CheckManage.aspx?Type=View',null, '90','PrjCode','AcceptCheckContent')
INSERT dbo.WF_Business_Class VALUES('112','001','项目检查审核')	
GO

IF OBJECT_ID('Prj_v_ItemInspect') IS NOT NULL
	DROP VIEW Prj_v_ItemInspect
GO
CREATE VIEW Prj_v_ItemInspect
AS
SELECT Prj_ItemInspectSort.ItemInspectSortName, Prj_ItemInspect.*
FROM   Prj_ItemInspect 
INNER JOIN Prj_ItemInspectSort ON Prj_ItemInspect.AcceptCheckSort = Prj_ItemInspectSort.SortID
GO

--盘点结存审核	Bery    2012-05-09 09:52
INSERT dbo.WF_BusinessCode VALUES('115','盘点结存审核','StocktakeId', 'Sm_Stocktake','StocktakeId','FlowState',
			'/StockManage/Stocktake/StocktakeView.aspx',null, '03',NULL,'StocktakeName')
INSERT dbo.WF_Business_Class VALUES('115','001','盘点结存审核')	
GO


--项目奖罚添加审核
ALTER TABLE Prj_ItemProg ADD UID nvarchar(200) NOT NULL 
	CONSTRAINT DF_Prj_ItemProg_UID DEFAULT(NEWID())
GO
ALTER TABLE Prj_ItemProg ADD FlowState int 
	CONSTRAINT DF_Prj_ItemProg_FlowState DEFAULT(-1) NOT NULL
GO
UPDATE Prj_ItemProg SET FlowState = 1
GO
INSERT dbo.WF_BusinessCode VALUES('120','项目奖罚审核','UID', 'Prj_ItemProg','UID','FlowState',
			'/EPC/17/Ppm/Prog/ItemProgManage.aspx?Type=View',null, '90','PrjCode','ProgUnit')
INSERT dbo.WF_Business_Class VALUES('120','001','项目奖罚审核')	
GO
--更新视图
IF OBJECT_ID('Prj_V_ItemProg') IS NOT NULL
	DROP VIEW Prj_V_ItemProg
GO
CREATE VIEW Prj_V_ItemProg
AS
SELECT dbo.Prj_ProgSort.ProgSortName, dbo.Prj_ItemProg.ID AS Expr1, dbo.Prj_ItemProg.PrjCode AS Expr2, dbo.Prj_ItemProg.ProgUnit AS Expr3, 
	dbo.Prj_ItemProg.ByProgObject AS Expr4, dbo.Prj_ItemProg.ProgGist AS Expr5, dbo.Prj_ItemProg.ProgCause AS Expr6, dbo.Prj_ItemProg.ProgMoney AS Expr7, 
	dbo.Prj_ItemProg.ProgSortCode AS Expr8, dbo.Prj_ItemProg.ProgDate AS Expr9, dbo.Prj_ItemProg.Remark AS Expr10, dbo.Prj_ItemProg.ApprovePerson AS Expr11, 
	dbo.Prj_ItemProg.ApproveResult AS Expr12, dbo.Prj_ItemProg.ApproveDate AS Expr13, dbo.Prj_ItemProg.ApproveIdea AS Expr14, 
	dbo.Prj_ItemProg.ProgSign AS Expr15, dbo.Prj_ItemProg.Principal AS Expr16, dbo.Prj_ItemProg.ProgBurstunit AS Expr17, dbo.Prj_ItemProg.IsAction AS Expr18, 
	dbo.Prj_ItemProg.filesType AS Expr19, dbo.Prj_ItemProg.mark AS Expr20, dbo.Prj_ItemProg.*
FROM dbo.Prj_ProgSort INNER JOIN
	dbo.Prj_ItemProg ON dbo.Prj_ProgSort.ProgSortCode = dbo.Prj_ItemProg.ProgSortCode
GO
--删除原来的项目奖罚审核的菜单
--INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('909107','项目奖罚审核','EPC/17/Frame.aspx?url=../../epc/17/PPM/Prog/ItemProgList.aspx&Type=ShenHe&PrjState=0&Levels=0','y',7,'SysFrame/MenuIco/7.gif',706,0,'0','0',NULL,'1')
DELETE FROM PT_Role_Privilege WHERE ModuleCode = '909107'
DELETE FROM PT_mk WHERE C_MKDM = '909107'
GO


--设计变更      lhy    2012-05-11 09:02
ALTER TABLE Prj_TechnologyManage ADD FlowState INT
   CONSTRAINT Default_TechnologyManage DEFAULT (1) NOT NULL 


ALTER TABLE Prj_TechnologyManage ADD TechGuid uniqueidentifier
   CONSTRAINT Guid_TechnologyManage DEFAULT (NewId()) NOT NULL 

insert dbo.WF_BusinessCode values('113','设计变更审核','TechGuid',
			'Prj_TechnologyManage','TechGuid','FlowState',
			'/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&bs=4',null,
			'91','PrjCode','ItemName')

insert dbo.WF_Business_Class values('113','001','设计变更审核')


--技术交底
ALTER TABLE Prj_TechnologyTell ADD FlowState INT
   CONSTRAINT Default_TechnologyTell DEFAULT (1) NOT NULL 

ALTER TABLE Prj_TechnologyTell ADD TechGuid uniqueidentifier
   CONSTRAINT Guid_TechnologyTell DEFAULT (NewId()) NOT NULL 


insert dbo.WF_BusinessCode values('114','技术交底审核','TechGuid',
			'Prj_TechnologyTell','TechGuid','FlowState',
			'/EPC/17/Ppm/ScienceInnovate/TechnologyJDEdit.aspx?Type=View',null,
			'91','PrjCode','SerialNumber')

insert dbo.WF_Business_Class values('114','001','技术交底审核')

--修改视图
IF OBJECT_ID('Prj_V_TechnologyJD') IS NOT NULL
	DROP VIEW Prj_V_TechnologyJD
GO
CREATE VIEW [dbo].[Prj_V_TechnologyJD]
AS
SELECT     MainID AS Expr1, PrjCode AS Expr2, SerialNumber AS Expr3, FillDate AS Expr4, FillPeople AS Expr5, PrejectName AS Expr6, ConstructionUnit AS Expr7, 
                      ByTellUnit AS Expr8, TellPeople AS Expr9, ByTellPeople AS Expr10, TellLocus AS Expr11, TellDate AS Expr12, TellContentAbstract AS Expr13, Remark AS Expr14,
                          (SELECT     v_xm
                            FROM          dbo.PT_yhmc
                            WHERE      (v_yhdm = dbo.Prj_TechnologyTell.FillPeople)) AS FillName,
                          (SELECT     v_xm
                            FROM          dbo.PT_yhmc AS PT_yhmc_2
                            WHERE      (v_yhdm = dbo.Prj_TechnologyTell.TellPeople)) AS TellName,
                          (SELECT     v_xm
                            FROM          dbo.PT_yhmc AS PT_yhmc_1
                            WHERE      (v_yhdm = dbo.Prj_TechnologyTell.ByTellPeople)) AS ByTellName, dbo.Prj_TechnologyTell.*, mark AS Expr15, filesType AS Expr16
FROM         dbo.Prj_TechnologyTell

GO


--图纸自会审
insert dbo.WF_BusinessCode values('116','图纸审核','TechGuid',
			'Prj_TechnologyManage','TechGuid','FlowState',
			'/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&bs=2&sm=10',null,
			'91','PrjCode','ItemName')

insert dbo.WF_Business_Class values('116','001','图纸审核')


--工程联系单
insert dbo.WF_BusinessCode values('117','工程联系单审核','TechGuid',
			'Prj_TechnologyManage','TechGuid','FlowState',
			'/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&bs=3',null,
			'91','PrjCode','ItemName')

insert dbo.WF_Business_Class values('117','001','工程联系单审核')


--工程洽商单
insert dbo.WF_BusinessCode values('118','工程洽商单审核','TechGuid',
			'Prj_TechnologyManage','TechGuid','FlowState',
			'/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&bs=8',null,
			'91','PrjCode','ItemName')

insert dbo.WF_Business_Class values('118','001','工程洽商单审核')


--技术总结
UPDATE WF_BusinessCode SET NameField='SummaryName'
WHERE BusinessCODE='040'


--添加合同预算审核    lhy 2012-5-14 16:05
IF OBJECT_ID('Bud_ContractTaskAudit') IS NOT NULL
    DROP TABLE Bud_ContractTaskAudit
GO
CREATE TABLE Bud_ContractTaskAudit
(
    ContractTaskAuditId nvarchar(200) PRIMARY KEY,	
    PrjId nvarchar(200),		--项目GUID
    PrjName nvarchar(100),      --项目名称
    FlowState int,				--流程状态
    InputDate datetime DEFAULT(GETDATE())
)
GO


IF OBJECT_ID('trigInsertContractTask') IS NOT NULL
	DROP TRIGGER trigInsertContractTask
GO
CREATE TRIGGER trigInsertContractTask
ON Bud_ContractTask AFTER INSERT
AS
	BEGIN
    	IF (SELECT COUNT(*) FROM Bud_ContractTaskAudit,inserted 
			WHERE Bud_ContractTaskAudit.PrjId = inserted.PrjId) = 0
          BEGIN
			 INSERT INTO Bud_ContractTaskAudit
			 SELECT NEWID(),PrjId,
            (SELECT PrjName FROM Pt_PrjInfo WHERE PrjGuid=PrjId),
             -1,GETDATE() FROM inserted 
          END
	END
GO


insert dbo.WF_BusinessCode values('121','合同预算审核','ContractTaskAuditId',
			'Bud_ContractTaskAudit','ContractTaskAuditId','FlowState',
			'/BudgetManage/Budget/ContractTaskView.aspx',null,
			'13','PrjId','PrjName')
insert dbo.WF_Business_Class values('121','001','合同预算审核')
GO




--添加初始化数据    
IF (SELECT COUNT(*) FROM Bud_ContractTaskAudit) = 0
BEGIN
	WITH ContractTask AS
	(
		SELECT DISTINCT PrjId,PrjName
		FROM Bud_ContractTask JOIN Pt_PrjInfo
        ON Bud_ContractTask.PrjId=Pt_PrjInfo.PrjGuid
	)
	INSERT INTO Bud_ContractTaskAudit
	SELECT NEWID(), PrjId,PrjName, 1,GETDATE()
	FROM ContractTask
END
GO

--把预算审核改成目标成本审核
UPDATE WF_BusinessCode SET BusinessName='目标成本审核' 
WHERE BusinessCode='109'

UPDATE WF_Business_Class SET BusinessClassName='目标成本审核' 
WHERE BusinessCode='109'
GO

--间接成本预算添加流程审核  By Zhang Fujun  Date：2012-05-15 10:39:40.127
--1.数据处理，老的成本预算更新到核算
UPDATE Bud_IndirectBudget 
SET AccountAmount=BudgetAmount
WHERE State!=2 AND BudgetAmount!=0
GO
UPDATE Bud_OrganizationBudget 
SET AccountingAmount=BudgetAmount
WHERE State!=2 AND BudgetAmount!=0
GO
--2.间接成本预算流程审核
IF OBJECT_ID('Bud_IndirectBudgetWF','U') IS NOT NULL
DROP TABLE Bud_IndirectBudgetWF
GO
CREATE TABLE Bud_IndirectBudgetWF
(
	Id UNIQUEIDENTIFIER PRIMARY KEY ,--主键 项目Id/或组织机构Id  
    RelatedId NVARCHAR(100) NOT NULL,--项目Id/或组织机构Id  
	FlowState INT, -- 流程状态
	InputDate DATETIME,
	InputUser NVARCHAR(8)
)
GO
--3.添加间接成本预算审核
IF NOT EXISTS(SELECT * FROM WF_BusinessCode WHERE BusinessCode='119')
BEGIN
	INSERT INTO dbo.WF_BusinessCode(BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl
	,LookUrl,C_MKDM) VALUES('119','间接成本预算审核','Id','Bud_IndirectBudgetWF','Id','FlowState',
	'/BudgetManage/Cost/IndirectView.aspx',null,'29')
	INSERT INTO dbo.WF_Business_Class VALUES('119','001','间接成本预算审核')
END

GO
--4.更改进度计划审核 审核记录内容链接
UPDATE WF_BusinessCode SET NameField='ProgressName'
WHERE BusinessCode=107
GO
--5.更改进度计划调整审核 审核记录内容链接
UPDATE WF_BusinessCode SET NameField='VersionName'
WHERE BusinessCode=108
GO
--更新间接成本预算项目相关审核  By Zhang Fujun  Date：2012-05-16 14:38:53.090
UPDATE WF_BusinessCode SET ProjectField='' WHERE BusinessCode='119'
GO
--添加间接成本预算（组织机构审核）
IF NOT EXISTS(SELECT * FROM WF_BusinessCode WHERE BusinessCode='124')
BEGIN
	INSERT INTO dbo.WF_BusinessCode(BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl
	,LookUrl,C_MKDM) VALUES('124','组织机构预算审核','Id','Bud_IndirectBudgetWF','Id','FlowState',
	'/BudgetManage/Cost/IndirectView.aspx',null,'29')
	INSERT INTO dbo.WF_Business_Class VALUES('124','001','组织机构预算审核')
END
GO
--END 
--施工报量 添加FlowState     lhy 2012-05-17
ALTER TABLE Bud_ConsReport ADD FlowState INT
   CONSTRAINT Default_ConsReport DEFAULT (1) NOT NULL  
GO

--修改施工报量任务节点的审核量
UPDATE Bud_ConsTask SET AccountingQuantity=CompleteQuantity
WHERE ConsTaskId IN(
SELECT ConsTaskId FROM dbo.Bud_ConsTask
left join Bud_ConsReport on Bud_ConsTask.ConsReportId=Bud_ConsReport.ConsReportId
WHERE state in (0,1,3,4)
)
GO

--修改施工报量资源的审核量和CBSCode
WITH cteConsTask AS
(
	SELECT Bud_ConsTask.ConsTaskId Id,Bud_ConsTask.ConsReportId,Bud_ConsTask.TaskId,Bud_ConsTask.CompleteQuantity,
	Bud_ConsTask.WorkContent,Bud_ConsTask.Note,Bud_ConsTask.AccountingQuantity,Bud_ConsReport.state FROM dbo.Bud_ConsTask
	left join Bud_ConsReport on Bud_ConsTask.ConsReportId=Bud_ConsReport.ConsReportId
	WHERE state in (0,1,3,4) 
),cteConsRes AS
(
	SELECT ConsTaskRes.*,ResourceType,dbo.GetTopResourceType(ResourceType) TopResourceTypeId 
	FROM Bud_ConsTaskRes ConsTaskRes INNER JOIN cteConsTask
	ON ConsTaskRes.ConsTaskId=cteConsTask.Id
	INNER JOIN Res_Resource Resource on ConsTaskRes.ResourceId=Resource.ResourceId
),cteConsResCost AS
(
	SELECT cteConsRes.*,Bud_CostAccounting.CBSCode CostCBSCode FROM cteConsRes LEFT JOIN Bud_CostAccounting
	ON cteConsRes.TopResourceTypeId=Bud_CostAccounting.ResourceType
)
UPDATE Bud_ConsTaskRes SET CBSCode = cteConsResCost.CostCBSCode,AccountingQuantity=Bud_ConsTaskRes.Quantity
FROM Bud_ConsTaskRes JOIN cteConsResCost ON cteConsResCost.ConsTaskResId = Bud_ConsTaskRes.ConsTaskResId
GO

--施工报量审核
insert dbo.WF_BusinessCode values('123','施工报量审核','ConsReportId',
			'Bud_ConsReport','ConsReportId','FlowState',
			'/BudgetManage/Construct/QueryConstructTask.aspx',null,
			'29','PrjId','InputDate')

insert dbo.WF_Business_Class values('123','001','施工报量审核')


--基础权限表    Bery    2012-05-17 13:10
IF OBJECT_ID('Basic_Privilege') IS NOT NULL
	DROP TABLE Basic_Privilege
GO
CREATE TABLE Basic_Privilege
(
	PrivilegeId nvarchar(200) PRIMARY KEY,
	RelationsTable nvarchar(200) NOT NULL,
	RelationsKey nvarchar(200) NOT NULL, --关联的主键值
	UserCode varchar(8) NOT NULL --用户编号
)


--完成量月报表    lhy   2012-5-18
IF OBJECT_ID('vConsTaskSum') IS NOT NULL
	DROP VIEW vConsTaskSum
GO
CREATE VIEW vConsTaskSum
AS
SELECT CT.TaskId, CR.InputDate, CT.AccountingQuantity,
	(UnitPrice * CTR.AccountingQuantity) AS Sum
FROM Bud_ConsTaskRes AS CTR
LEFT JOIN Bud_ConsTask AS CT ON CT.ConsTaskId = CTR.ConsTaskId
LEFT JOIN Bud_ConsReport AS CR ON CR.ConsReportId = CT.ConsReportId
WHERE FlowState=1
GO


--更新TypeCode 采用5个字符进行分级, 解决1000个项目问题  Bery    2012-05-23 08:23
UPDATE PT_PrjInfo SET TypeCode = '00' + TypeCode 
WHERE LEN(TypeCode) = 3
GO
UPDATE PT_PrjInfo SET TypeCode = '00' + SUBSTRING(TypeCode, 1, 3) + '00' + SUBSTRING(TypeCode, 4, 6)
WHERE LEN(TypeCode) = 6
GO


--解决科技管理附件的问题     lhy  2012-05-30   08:50
--施工组织
WITH cteTechAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_TechnologyConstructOrganize.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_TechnologyConstructOrganize ON ReCordCode= CONVERT(NVARCHAR, Id)
WHERE ModuleId='1720'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=FlowGuid
FROM XPM_Basic_AnnexList INNER JOIN cteTechAnnex ON XPM_Basic_AnnexList.AnnexCode=cteTechAnnex.AnnexCode
GO


--专项方案

--修改视图
IF OBJECT_ID('Prj_V_ExpertProject') IS NOT NULL
	DROP VIEW Prj_V_ExpertProject
GO
CREATE VIEW [dbo].[Prj_V_ExpertProject]
AS
SELECT dbo.Prj_ExpertSchemeManage.*, dbo.PT_D_BM.V_BMMC, 
      dbo.Pt_PrjInfo.PrjName,
          (SELECT dbo.pt_yhmc.v_xm
         FROM dbo.pt_yhmc
         WHERE dbo.pt_yhmc.v_yhdm = dbo.Prj_ExpertSchemeManage.WeavePeople) 
      AS Weavemc,
          (SELECT dbo.pt_yhmc.v_xm
         FROM dbo.pt_yhmc
         WHERE dbo.pt_yhmc.v_yhdm = dbo.Prj_ExpertSchemeManage.FillPeople) 
      AS fillmc
FROM dbo.Prj_ExpertSchemeManage INNER JOIN
      dbo.Pt_PrjInfo ON 
      dbo.Prj_ExpertSchemeManage.PrejectName = dbo.Pt_PrjInfo.PrjGuid INNER JOIN
      dbo.PT_D_BM ON dbo.Prj_ExpertSchemeManage.PrjCode = dbo.PT_D_BM.i_bmdm
GO


--修改老数据
WITH cteExpertAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_ExpertSchemeManage.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_ExpertSchemeManage ON ReCordCode= CONVERT(NVARCHAR, MainId)
WHERE ModuleId='1722'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=FlowGuid
FROM XPM_Basic_AnnexList INNER JOIN cteExpertAnnex ON XPM_Basic_AnnexList.AnnexCode=cteExpertAnnex.AnnexCode
GO


--技术标准台账
ALTER TABLE Prj_TechnologyCriterion ADD TechGuid uniqueidentifier
   CONSTRAINT Guid_Prj_TechnologyCriterion DEFAULT (NewId()) NOT NULL 
GO


WITH cteTechAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_TechnologyCriterion.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_TechnologyCriterion ON ReCordCode= CONVERT(NVARCHAR(200), MainId)
WHERE ModuleId='1725'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=TechGuid
FROM XPM_Basic_AnnexList INNER JOIN cteTechAnnex ON XPM_Basic_AnnexList.AnnexCode=cteTechAnnex.AnnexCode
GO


--企业技术标准
ALTER TABLE Prj_EnterpriseTechnologyCriterion ADD EnterGuid uniqueidentifier
   CONSTRAINT Guid_Prj_EnterpriseTechnologyCriterion DEFAULT (NewId()) NOT NULL 
GO

WITH cteEnterAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_EnterpriseTechnologyCriterion.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_EnterpriseTechnologyCriterion ON ReCordCode= CONVERT(NVARCHAR, MainKey)
WHERE ModuleId='1721'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=EnterGuid
FROM XPM_Basic_AnnexList INNER JOIN cteEnterAnnex ON XPM_Basic_AnnexList.AnnexCode=cteEnterAnnex.AnnexCode
GO

--技术变更
WITH cteTechAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_TechnologyManage.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_TechnologyManage ON ReCordCode= CONVERT(NVARCHAR(200), Id)
WHERE ModuleId='1726'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=TechGuid
FROM XPM_Basic_AnnexList INNER JOIN cteTechAnnex ON XPM_Basic_AnnexList.AnnexCode=cteTechAnnex.AnnexCode
GO

--技术交底
WITH cteTechAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_TechnologyTell.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_TechnologyTell ON ReCordCode= CONVERT(NVARCHAR(200), MainId)
WHERE ModuleId='1728'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=TechGuid
FROM XPM_Basic_AnnexList INNER JOIN cteTechAnnex ON XPM_Basic_AnnexList.AnnexCode=cteTechAnnex.AnnexCode
GO

--技术总结
WITH cteTechAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_Summary.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_Summary ON ReCordCode= CONVERT(NVARCHAR(200), Id)
WHERE ModuleId='1754'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=WfGuid
FROM XPM_Basic_AnnexList INNER JOIN cteTechAnnex ON XPM_Basic_AnnexList.AnnexCode=cteTechAnnex.AnnexCode
GO


--技术进步
ALTER TABLE Prj_ProgressPlan ADD ProgressGuid uniqueidentifier
   CONSTRAINT Guid_Prj_ProgressPlan DEFAULT (NewId()) NOT NULL 
GO



WITH cteProgressAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_ProgressPlan.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_ProgressPlan ON ReCordCode= CONVERT(NVARCHAR(200), PlanId)
WHERE ModuleId='1747'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=ProgressGuid
FROM XPM_Basic_AnnexList INNER JOIN cteProgressAnnex ON XPM_Basic_AnnexList.AnnexCode=cteProgressAnnex.AnnexCode
GO



--技术进步项目实施上报情况
ALTER TABLE Prj_ProgressPlan_Child ADD ProgressGuid uniqueidentifier
   CONSTRAINT Guid_Prj_ProgressPlan_Child DEFAULT (NewId()) NOT NULL 
GO

WITH cteProgressAnnex AS
(
SELECT AnnexCode,ReCordCode,Prj_ProgressPlan_Child.* FROM dbo.XPM_Basic_AnnexList 
INNER JOIN Prj_ProgressPlan_Child ON ReCordCode= CONVERT(NVARCHAR(200), MainId)
WHERE ModuleId='1724'
)
UPDATE XPM_Basic_AnnexList SET ReCordCode=ProgressGuid
FROM XPM_Basic_AnnexList INNER JOIN cteProgressAnnex ON XPM_Basic_AnnexList.AnnexCode=cteProgressAnnex.AnnexCode
GO


-- 添加部门负责人字段   Bery    2012-06-14 10:49
ALTER TABLE PT_yhmc ADD IsChargeMan bit DEFAULT(0) NOT NULL

-- 修改编码库字段长度    Bery    2012-06-19 08:51
ALTER TABLE XPM_Basic_CodeType ALTER COLUMN TypeName varchar(30) NOT NULL
ALTER TABLE XPM_Basic_CodeType ALTER COLUMN SignCode varchar(30) NOT NULL
ALTER TABLE XPM_Basic_CodeType ALTER COLUMN Owner varchar(8) NOT NULL


-- 隐藏编码库中的项目状态   Bery    2012-06-20 15:37
UPDATE XPM_Basic_CodeType SET IsVisible = 0 WHERE TypeId = '7'

-- 使用老的投标的客户在添加完PT_PrjInfo_ZTB_Detail后需要执行    Bery    2012-06-20 15:52
--INSERT INTO PT_PrjInfo_ZTB_Detail(PrjGuid, IsTender,SetUpFlowState)
--SELECT PrjGuid, 1, 1 FROM PT_PrjInfo WHERE PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail)

-- 解决项目超过1000个后, 处理遗留数据   Bery    2012-06-21 10:00
UPDATE PT_PrjInfo SET TypeCode = '00' + SUBSTRING(TypeCode, 1, 3) + '00' + SUBSTRING(TypeCode, 4, 6)
WHERE LEN(TypeCode) = 6
UPDATE PT_PrjInfo SET TypeCode = '00' + TypeCode
WHERE LEN(TypeCode) = 3

-- 项目表添加默认值     Bery    2012-06-21 10:35
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD CONSTRAINT DF_IsTender DEFAULT 0 FOR IsTender 

--将物资模块里里供应商字段为0的改为空字符串 lhy 2012-2-6-25 15：54
--将仓库物资里供应商字段为0的改为空字符串
UPDATE Sm_Treasury_Stock SET corp='' WHERE corp='0'
--将出库物资里供应商字段为0的改为空字符串
UPDATE Sm_out_Stock SET corp='' WHERE corp='0'
--将退库物资里供应商字段为0的改为空字符串
UPDATE Sm_back_Stock SET corp='' WHERE corp='0'
GO
--修改岗位名称长度,不能为空   Zhang Fujun  2012-07-04 15:05:04.220
ALTER TABLE PT_D_Role 
	ALTER COLUMN RoleTypeName VARCHAR(200) NOT NULL
GO

-- 修改人力资源中传真号码的长度		Bery	2012-07-05
ALTER TABLE PT_d_bm ALTER COLUMN fx char(30)
GO
--删除【项目信息管理】中的流程 Zhang Fujun  2012-07-09 09:31:43.113 
DELETE  FROM WF_Template WHERE  BusinessCode =
	(SELECT BusinessCode FROM WF_BusinessCode  
		WHERE BusinessCode='099' AND BusinessName='项目信息管理')
DELETE FROM WF_Business_Class WHERE BusinessCode='099' 
	AND BusinessClassName='项目信息审核'
DELETE FROM WF_BusinessCode  WHERE BusinessCode='099'	
	AND BusinessName='项目信息管理'
GO

--添加供应商台账  lhy 2012-7-13
INSERT INTO dbo.PT_MK VALUES
('050611','供应商台账','/ContractManage/ContractReport/BCorpNamePeport.aspx','y',11,'',2445,0,'0','0','','1')

--添加表Con_Modify_Stock 2011-07-19 
IF OBJECT_ID('Con_Modify_Stock', 'U') IS NOT NULL
    DROP TABLE Con_Modify_Stock
GO
CREATE TABLE Con_Modify_Stock
(
    ModifyStockId NVARCHAR(200) PRIMARY KEY NOT NULL,
    ModifyId NVARCHAR(64) REFERENCES Con_Payout_Modify(ModifyId) ON DELETE CASCADE,--变更Id
    PurchaseId NVARCHAR(50),--采购物资Id
    Scode Nvarchar(50),-- 物资编码 
	Pscode NVARCHAR(64),--采购单编码
    Sprice DECIMAL(18,3),  --价格
    Quantity DECIMAL(18,3),  --数量
    Corp NVARCHAR(64), --供应商
	ArrivalDate DateTime--到货日期
) 
GO
IF OBJECT_ID('Con_Balance_Stock', 'U') IS NOT NULL
    DROP TABLE Con_Balance_Stock
GO
CREATE TABLE Con_Balance_Stock
(
    BalanceStockId nvarchar(200) PRIMARY KEY NOT NULL,
    BalanceId nvarchar(64) REFERENCES Con_Payout_Balance(BalanceId) ON DELETE CASCADE,--结算Id
    PurchaseId nvarchar(50) REFERENCES Sm_Purchase_Stock(psid),--采购物资Id
    ArrivaledQuantity decimal(18,3)--到货数量
) 
GO
--修改支出合同变更审核时的查看页面
UPDATE WF_BusinessCode SET DoWithUrl='/ContractManage/PayoutModify/ModifyQuery.aspx'
WHERE BusinessCode='082'
--修改支出合同结算审核时的查看页面
UPDATE WF_BusinessCode SET DoWithUrl='/ContractManage/PayoutBalance/BalanceQuery.aspx'
WHERE BusinessCode='083'
GO
--新增合同汇总
INSERT INTO dbo.PT_MK VALUES
('881009','支出合同汇总表','/ContractManage/ContractReport/PayoutContractSum.aspx','y',9,'',2446,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('881010','收入合同汇总表','/ContractManage/ContractReport/IncometContractSum.aspx','y',10,'',2447,0,'0','0','','1')
GO

--修改编码库表CodeName字段的长度 Feng Yuanyuan  2012-07-26 14:10 
ALTER TABLE dbo.XPM_Basic_CodeList ALTER COLUMN  CodeName varchar(100)


--新增损耗出库SM_Wastage  lhy 2012-7-27
IF OBJECT_ID('SM_Wastage', 'U') IS NOT NULL
    DROP TABLE SM_Wastage
GO
CREATE TABLE SM_Wastage
(
    WastageId NVARCHAR(50) NOT NULL,--损耗Id
    WastageCode NVARCHAR(64) PRIMARY KEY NOT NULL,--损耗编码
    Treasurycode NVARCHAR(512) REFERENCES Sm_Treasury(tcode) ON DELETE CASCADE,--仓库编码
    InputPerson NVARCHAR(64) NOT NULL,--录入人
    InputDate DATETIME NOT NULL,--录入时间
    FlowState INT NOT NULL,  --流程状态
    Isout BIT NOT NULL,   --是否出库
    IsOutTime DATETIME NULL, --确认出库时间
    Explain NVARCHAR(MAX) NULL  --说明
) 
GO
--新增损耗出库物资Sm_Wastage_Stock
IF OBJECT_ID('Sm_Wastage_Stock', 'U') IS NOT NULL
    DROP TABLE Sm_Wastage_Stock
GO
CREATE TABLE Sm_Wastage_Stock
(
    WastageStockId NVARCHAR(50) PRIMARY KEY NOT NULL,--损耗物资Id
    WastageCode NVARCHAR(64) REFERENCES SM_Wastage(WastageCode) ON DELETE CASCADE,--损耗出库单
    ResourceCode NVARCHAR(50) NOT NULL, --资源编码
    Sprice DECIMAL(18,3) NOT NULL,   --价格
    Number DECIMAL(18,3) NOT NULL, --确认出库时间
    Corp NVARCHAR(64) NOT NULL  --供应商
) 
GO
--审核
insert dbo.WF_BusinessCode values('125','报损出库审核','WastageId',
			'Sm_Wastage','WastageId','FlowState',
			'/StockManage/SmWastage/ViewWastage.aspx',null,
			'03',null,'WastageCode')
insert dbo.WF_Business_Class values('125','001','报损出库审核')
--盘点结存  报损数量
ALTER TABLE Sm_Stocktake_Detail ADD WastageNum  DECIMAL(18,3)
   CONSTRAINT WastageNum_Sm_Stocktake_Detail DEFAULT (0.000) NOT NULL 
GO
--添加报损出库模块
INSERT INTO dbo.PT_MK VALUES
('0316','报损出库','','y',13,'',2448,2,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('031601','报损出库管理','StockManage/SmWastage/SmWastageList.aspx','y',1,'',2449,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('031602','确认报损出库','StockManage/SmWastage/ConfirmWastage.aspx','y',2,'',2451,0,'0','0','','1')
GO


--添加RTX帐号   Bery    2012-08-01 15:11
ALTER TABLE PT_yhmc ADD RTXID nvarchar(200)
GO
--设置默认RTX帐号为PM2系统帐号  Bery    2012-08-01 15:11
UPDATE PT_yhmc SET RTXID = PT_LOGIN.V_DLID
FROM PT_LOGIN INNER JOIN PT_yhmc ON PT_yhmc.v_yhdm = PT_LOGIN.V_YHDM
GO
--修改成本归集 lhy 2012-8-3 16:45
ALTER TABLE Res_ResourceType ADD CBSCode NVARCHAR(200) NULL 
GO
UPDATE Res_ResourceType SET CBSCode='001001001' WHERE ResourceTypeId='6A1A7050-1F92-4291-B932-43E1DFCE6E90'
UPDATE Res_ResourceType SET CBSCode='001001002' WHERE ResourceTypeId='6A1A7050-1F92-4291-B932-43E1DFCE6E91'
UPDATE Res_ResourceType SET CBSCode='001001003' WHERE ResourceTypeId='6A1A7050-1F92-4291-B932-43E1DFCE6E92'
GO
--添加成本归集模块
INSERT INTO dbo.PT_MK VALUES
('2909','成本归集','','y',9,'',2452,2,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('290901','合同类型归集','BudgetManage/Construct/ContractTypeCostAllocation.aspx','y',1,'',2453,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('290902','资源分类归集','BudgetManage/Construct/ResTypeCostAllocation.aspx','y',2,'',2454,0,'0','0','','1')
GO

--添加log4net支持数据库     Bery    2012-08-18 14:17
IF OBJECT_ID('Log') IS NOT NULL
	DROP TABLE Log
GO
CREATE TABLE [dbo].[Log] (
    [Id] [int] IDENTITY (1, 1) NOT NULL,
    [Date] [datetime] NOT NULL,
    [Thread] [varchar] (255) NOT NULL,
    [Level] [varchar] (50) NOT NULL,
    [Logger] [varchar] (255) NOT NULL,
    [Message] [varchar] (4000) NOT NULL,
    [Exception] [varchar] (2000) NULL
)
GO

--返回从当前部门到顶层部门的信息    Bery    2012-08-23 09:40
IF OBJECT_ID('ufnRootDepTab') IS NOT NULL
	DROP FUNCTION ufnRootDepTab
GO
CREATE FUNCTION ufnRootDepTab(@bmdm int)
RETURNS TABLE
AS
RETURN (
	WITH cteRootDep
	AS
	(
		SELECT i_sjdm, i_bmdm, V_BMMC
		FROM PT_d_bm
		WHERE i_bmdm = @bmdm
		UNION ALL 
		SELECT T.i_sjdm, T.i_bmdm, T.V_BMMC
		FROM PT_d_bm AS T
		INNER JOIN cteRootDep ON T.i_bmdm = cteRootDep.i_sjdm
	)
	SELECT * FROM cteRootDep
)
GO

--根据部门id获取部门全名        Bery        2012-08-23 09:40
IF OBJECT_ID('ufnRootDepName') IS NOT NULL
	DROP FUNCTION ufnRootDepName
GO
CREATE FUNCTION ufnRootDepName(@bmdm int)
RETURNS nvarchar(4000)
AS
BEGIN
	DECLARE @bmmc AS nvarchar(4000);
	SELECT @bmmc = ISNULL(@bmmc + '\', '') + V_BMMC
	FROM (
		SELECT TOP(20) V_BMMC FROM ufnRootDepTab(@bmdm)
		ORDER BY i_bmdm
	) AS V;
	RETURN(@bmmc)
END
GO


--添加岗位名称字段                      Bery    2012-08-27 10:11
ALTER TABLE PT_DUTY ADD DutyName NVARCHAR(200);
GO
--处理遗留数据，更新岗位名称            Bery    2012-08-27 10:11
UPDATE PT_DUTY 
SET DutyName = RoleTypeName
FROM PT_DUTY
JOIN PT_D_Role ON PT_DUTY.DutyCode = PT_D_Role.RoleTypeCode
GO
--删除外键                              Bery    2012-08-27 10:47
ALTER TABLE PT_DUTY DROP CONSTRAINT [部门岗位-职位族]
GO

--修改角色管理地址                      Bery    2012-08-27 14:11
UPDATE PT_MK SET V_CDLJ = 'oa/SysAdmin/RoleManage/RoleList.aspx' WHERE V_CDLJ = 'oa/SysAdmin/RoleManage/roleManage.aspx' 
GO

--删除职位族管理菜单                    Bery    2012-08-27 14:38
DELETE FROM PT_Role_Privilege WHERE ModuleCode = '080103'
DELETE FROM PT_MK WHERE C_MKDM = '080103'
GO

--增加上传Logo模块     lhy 2012-08-27 16:54
INSERT INTO dbo.PT_MK VALUES
('3827','上传Logo','/TableTop/UploadLogo.aspx','y',27,'',2455,0,'0','0','','1')
GO

--处理采购单空项目数据 根据合同获得项目 lhy 2012-08-28 9:00
WITH PurchaseContract AS
(
SELECT payoutContract.ContractName,payoutContract.PrjGuid,purchase.* FROM Sm_Purchase purchase 
INNER JOIN Con_Payout_Contract payoutContract ON [Contract]=ContractId WHERE Project is null or Project=''
)
UPDATE Sm_Purchase SET Project=PrjGuid FROM Sm_Purchase purchase 
INNER JOIN PurchaseContract ON purchase.pid=PurchaseContract.pid


--修改“更新用户角色权限”      Bery        2012-09-03 11:10
IF OBJECT_ID('P_Plat_UpdUserPriv') IS NOT NULL
	DROP PROC P_Plat_UpdUserPriv
GO
CREATE PROCEDURE P_Plat_UpdUserPriv     
	--功能：根据针对用户所指定的角色的权限，和当前用户所具有的权限进行合并  
	--exec P_Plat_UpdUserPriv '00000000','2,3'  
	@UserCode varchar(30),  
	@RoleCodes varchar(100)  
AS  
begin  
	--定义表变量  
	declare @UserPriv table  
	(  
	ModuleCode varchar(30),  
	IsBasic char(1),  
	IsHaveOp char(1)  
	)  

	--插入用户当前的权限  
	insert into @UserPriv (ModuleCode,IsBasic,IsHaveOp)  
	select C_MKDM as ModuleCode,IsBasic,isnull(IsHaveOp,0) as IsHaveOp from PT_YHMC_Privilege  
	where V_YHDM = @UserCode  
  
	--插入多个角色的权限并集  
	insert into @UserPriv (ModuleCode,IsBasic,IsHaveOp)  
	select ModuleCode,IsBasic,max(IsHaveOp) as IsHaveOp from PT_Role_Privilege  
	where charindex(','+Cast(RoleCode as varchar(10))+',',','+@RoleCodes+',',1) <> 0  
	and ModuleCode not in (select C_MKDM as ModuleCode from PT_YHMC_Privilege where V_YHDM = @UserCode)  
	group by ModuleCode,IsBasic  
  
	/*--更新用户的系统权限*/  
	delete from PT_YHMC_Privilege where V_YHDM = @UserCode  
	insert into PT_YHMC_Privilege (V_YHDM,C_MKDM,IsBasic,IsHaveOp)  
	select DISTINCT @UserCode,ModuleCode,'0' AS IsBasic, '0' as IsHaveOp 
	from @UserPriv  
	order by ModuleCode  
end  
GO

--修改物资管理中增加项目树的路径 lhy 2012-09-04 17:00
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=wantPlan'
WHERE C_MKDM='0303'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=purchasePlan'
WHERE C_MKDM='0304'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=purchase'
WHERE C_MKDM='0305'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=sendNoteList'
WHERE C_MKDM='031001'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=receiveNoteList'
WHERE C_MKDM='031005'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=reciveEditList'
WHERE C_MKDM='031006'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=delReceiveNote'
WHERE C_MKDM='031007'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=outReserve'
WHERE C_MKDM='031101'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=qOutReserve'
WHERE C_MKDM='031102'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=refundingList'
WHERE C_MKDM='031301'
UPDATE PT_MK SET V_CDLJ='StockManage/basicset/SmWantPlanFrame.aspx?path=qoRefundingList'
WHERE C_MKDM='031302'

--现场收货(收货单)增加调拨Id字段  lhy 2012-09-07 08:30
ALTER TABLE sm_receiveNote ADD sAllocationId  nvarchar(64)

--修改模块中人员类别分类的路径
UPDATE PT_MK SET V_CDLJ='CommonWindow/SingleClasses/DocClass.aspx?flt=HumanType&f=0&title=staffSort'
WHERE C_MKDM LIKE '080506'

--中间交接资料添加字段（接受人，交接人）    Fyy     2012-09-20 16:48
Alter table Prj_TechnologyManage add ReceivePerson varchar(20) 
Alter table Prj_TechnologyManage add JoinPerson varchar(20)

--解决支出合同出错问题      Bery        2012-09-21 11:20
IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Con_Payout_Contract' AND COLUMN_NAME = 'CapitalNumber') = 0
	ALTER TABLE Con_Payout_Contract ADD CapitalNumber varchar(200)--大写金额支持
GO
IF (SELECT COUNT(1) FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'Con_Payout_Payment' AND COLUMN_NAME = 'CapitalNumber') = 0
	ALTER TABLE Con_Payout_Payment ADD CapitalNumber nvarchar(1000) --大写金额
GO


--不同模块间去掉依赖        Bery        2012-09-22 08:17
ALTER TABLE PT_DBSJ DROP CONSTRAINT [督办事件-用户]


--模块权限表更换主键        Bery        2012-09-28 13:51
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE TABLE_NAME = 'PT_YHMC_Privilege' AND CONSTRAINT_NAME = 'PK_PT_YHMC_PRIVILEGE') = 1
	ALTER TABLE PT_YHMC_Privilege DROP CONSTRAINT PK_PT_YHMC_PRIVILEGE
GO
ALTER TABLE PT_YHMC_Privilege ADD Id nvarchar(200) NOT NULL DEFAULT(NEWID())
ALTER TABLE PT_YHMC_Privilege ADD CONSTRAINT PK_PTYHMCPrivilege_Id PRIMARY KEY (Id)
GO


--系统配置表                Bery        2012-10-09 14:08
IF OBJECT_ID('Basic_Config') IS NOT NULL
	DROP TABLE Basic_Config
GO
CREATE TABLE Basic_Config
(
	Id nvarchar(64) PRIMARY KEY,				--ID
	ParaName nvarchar(200) UNIQUE NOT NULL,		--参数名称		
	ParaValue nvarchar(200),					--参数值
	Note nvarchar(max)							--备注
)


--预警表更换主键            Bery        2012-10-10 10:03
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE TABLE_NAME = 'PopupSetting' AND CONSTRAINT_NAME = 'PK__PopupSetting__04C657A2') = 1
	ALTER TABLE PopupSetting DROP CONSTRAINT PK__PopupSetting__04C657A2
GO
ALTER TABLE PopupSetting ADD Id nvarchar(200) NOT NULL DEFAULT(NEWID())
ALTER TABLE PopupSetting ADD CONSTRAINT PK_PopupSetting_Id PRIMARY KEY (Id)
GO

--增加提醒类型     lhy		2012-10-12  9:30
INSERT INTO PT_D_TXLX VALUES('027','报名提醒','','',1,'')
GO

--归档表添加字段            Bery        2012-10-17 14:30
ALTER TABLE Files ADD Content nvarchar(max)
GO

--WF_TemplateNode添加主键   bery        2012-10-18 14:10
ALTER TABLE WF_TemplateNode ADD Id nvarchar(200) PRIMARY KEY DEFAULT(NEWID())
GO

--WF_Business_Class更换主键 Bery        2012-10-18 14:58         
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.CONSTRAINT_TABLE_USAGE
WHERE TABLE_NAME = 'WF_Business_Class' AND CONSTRAINT_NAME = 'PK_WF_Business_Class') = 1
	ALTER TABLE WF_Business_Class DROP CONSTRAINT PK_WF_Business_Class
GO
ALTER TABLE WF_Business_Class ADD Id nvarchar(200) NOT NULL DEFAULT(NEWID())
ALTER TABLE WF_Business_Class ADD CONSTRAINT PK_WFBusinessClass_Id PRIMARY KEY (Id)
GO


--更换相关单位主键          Bery        2012-10-31 10:20
ALTER TABLE XPM_Basic_ContactCorp DROP CONSTRAINT pk_Basic_Con
GO
ALTER TABLE XPM_Basic_ContactCorp ADD CONSTRAINT PK_BasicContactCorp_CorpID PRIMARY KEY(CorpID)
GO


-- 备用金添加审核           Bery        2012-10-31 14:44
INSERT WF_BusinessCode values('129','备用金申请','Id','PC_PettyCash','Id','FlowState',
	'/PettyCash/PettyCashDetail.aspx',null, '21','PrjTypeCode','(SELECT''查看'')')
GO
INSERT WF_Business_Class(BusinessCode, BusinessClass, BusinessClassName) values('129','001','备用金申请')
GO


--删除与主题风格的关联      Bery        2012-11-01 08:52
ALTER TABLE PT_LOGIN DROP CONSTRAINT FK_PT_LOGIN_PT_SkinType
GO


--CBS分解增加外包费用   lhy 2012-11-02 14:00
INSERT INTO Bud_CostAccounting VALUES (NEWID(),'001001000','外包费用','D','','')
--在模块中添加统计利润
INSERT INTO PT_MK VALUES
('290719','利润分析','BudgetManage/Report/ProfitStatistics.aspx','y',20,'',2467,0,'0','0','','1')

--修改项目立项与投标中项目名称的字段长度  lhy 2012-11-04  9:40
alter table PT_PrjInfo alter column  PrjName nvarchar(100)
alter table dbo.PT_PrjInfo_ZTB alter column  PrjName nvarchar(100)
GO

--添加备用金            Bery        2012-11-09 10:14 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('PC_PettyCash') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
    DROP TABLE PC_PettyCash
GO
CREATE TABLE PC_PettyCash ( 
	Id nvarchar(200) NOT NULL,    --  主键 
	Applicant varchar(8) NOT NULL,    --  申请人 
	ApplicationDate datetime NOT NULL,    --  申请日期 
	Cash decimal(18,3) NOT NULL,    --  本次申请金额 
	Account nvarchar(200) NOT NULL,    --  申请人账号 
	Bank nvarchar(200) NOT NULL,    --  开户行 
	Payer nvarchar(200) NOT NULL,    --  付款单位 
	Matter nvarchar(200) NOT NULL,    --  事项 
	CashDate datetime NOT NULL,    --  用款日期 
	PrjTypeCode nvarchar(200),    --  项目TypeCode 
	ApplicationReason nvarchar(max),    --  申请事由 
	FlowState int NOT NULL,    --  流程状态 
	InputUser varchar(8) NOT NULL,    --  录入时间 
	InputDate datetime DEFAULT GETDATE() NOT NULL,    --  录入时间 
	ModifyUser varchar(8) NOT NULL,    --  修改人 
	ModifyDate datetime DEFAULT GETDATE() NOT NULL    --  修改时间 
)
GO
--  Create Primary Key Constraints 
ALTER TABLE PC_PettyCash ADD CONSTRAINT PK_PC_PettyCash 
	PRIMARY KEY CLUSTERED (Id)
GO
EXEC sp_addextendedproperty 'MS_Description', '备用金', 'Schema', dbo, 'table', PC_PettyCash
GO
EXEC sp_addextendedproperty 'MS_Description', '主键', 'Schema', dbo, 'table', PC_PettyCash, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '申请人', 'Schema', dbo, 'table', PC_PettyCash, 'column', Applicant
GO
EXEC sp_addextendedproperty 'MS_Description', '申请日期', 'Schema', dbo, 'table', PC_PettyCash, 'column', ApplicationDate
GO
EXEC sp_addextendedproperty 'MS_Description', '本次申请金额', 'Schema', dbo, 'table', PC_PettyCash, 'column', Cash
GO
EXEC sp_addextendedproperty 'MS_Description', '申请人账号', 'Schema', dbo, 'table', PC_PettyCash, 'column', Account
GO
EXEC sp_addextendedproperty 'MS_Description', '开户行', 'Schema', dbo, 'table', PC_PettyCash, 'column', Bank
GO
EXEC sp_addextendedproperty 'MS_Description', '付款单位', 'Schema', dbo, 'table', PC_PettyCash, 'column', Payer
GO
EXEC sp_addextendedproperty 'MS_Description', '事项', 'Schema', dbo, 'table', PC_PettyCash, 'column', Matter
GO
EXEC sp_addextendedproperty 'MS_Description', '用款日期', 'Schema', dbo, 'table', PC_PettyCash, 'column', CashDate
GO
EXEC sp_addextendedproperty 'MS_Description', '项目TypeCode', 'Schema', dbo, 'table', PC_PettyCash, 'column', PrjTypeCode
GO
EXEC sp_addextendedproperty 'MS_Description', '申请事由', 'Schema', dbo, 'table', PC_PettyCash, 'column', ApplicationReason
GO
EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', PC_PettyCash, 'column', FlowState
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', PC_PettyCash, 'column', InputUser
GO
EXEC sp_addextendedproperty 'MS_Description', '录入时间', 'Schema', dbo, 'table', PC_PettyCash, 'column', InputDate
GO
EXEC sp_addextendedproperty 'MS_Description', '修改人', 'Schema', dbo, 'table', PC_PettyCash, 'column', ModifyUser
GO
EXEC sp_addextendedproperty 'MS_Description', '修改时间', 'Schema', dbo, 'table', PC_PettyCash, 'column', ModifyDate
GO

--添加备用金菜单        Bery        2012-11-09 10:14
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('21','备用金管理','','y',21,'MenuIco/13.gif',2463,4,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('2101','备用金申请','/PettyCash/PettyCashList.aspx','y',1,'',2464,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('2102','借款月度汇总(个人)','/PettyCash/PettyCashMonth.aspx','y',2,'',2465,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('2103','借款往来明细(财务)','/PettyCash/PettyCashManager.aspx','y',3,'',2466,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('2104','借款月度汇总(财务)','/PettyCash/PettyCsshMonthManager.aspx','y',4,'',2468,0,'0','0','','1')
GO 

--CBS分解增加其他   lhy 2012-11-09 13:30
INSERT INTO Bud_CostAccounting VALUES (NEWID(),'001001999','其他','D','','')
GO

--编码库移除项目状态        Bery        2012-11-12 10:18
UPDATE XPM_Basic_CodeType SET IsVisible = 0 WHERE SignCode = 'Pr0jectState'
GO

--邮件表                    Bery        2012-11-15 15:04
IF OBJECT_ID('OA_Mail') IS NULL
BEGIN
CREATE TABLE OA_Mail
(
	MailId nvarchar(200) PRIMARY KEY,	--主键
	ToMailId nvarchar(200),				--如果是已发送邮件，存储发送给对方的邮件ID
	MailName nvarchar(200),				--名称
	MailContent nvarchar(MAX),			--内容
	MailFrom nvarchar(20),				--发件人
	MailTo nvarchar(20),				--收件人
	AllMailToCode nvarchar(max),		--所有收件人编码
	AllMailTo nvarchar(MAX),			--所有收件人
	AllCopytoCode nvarchar(MAX),		--所有抄送人
	AllCopyto nvarchar(MAX),			--所有抄送人
	MailType varchar(2),				--邮件类型，I:收件箱，O:已发送邮件，D:草稿箱，R:撤回邮件
	IsReaded bit NOT NULL DEFAULT(0),	--是否已读
	ReadTime datetime,					--阅读时间
	IsValid bit NOT NULL DEFAULT(1),	--是否有效
    AnnexId nvarchar(200),              --附件ID
	InputDate datetime NOT NULL DEFAULT(GETDATE())	--录入时间
)
END
GO


--角色表去掉角色类型关联    Bery        2012-11-16 11:56
ALTER TABLE PT_Role DROP CONSTRAINT [角色-角色类型]
GO

--把质量资料和安全资料更改为质量类别和安全类别  FYY 2012-11-20 11:28:00.157
update EPM_Datum_Class set TypeName='质量类别' where TypeId=2
update EPM_Datum_Class set TypeName='安全类别' where TypeId=3
GO


--函数：根据TaskId获取小计      Bery        2012-11-20 13:38
IF OBJECT_ID('dbo.fn_GetTotal') IS NOT NULL
	DROP FUNCTION dbo.fn_GetTotal
GO
CREATE FUNCTION dbo.fn_GetTotal(@TaskId nvarchar(200))
RETURNS decimal(18,3)
BEGIN
	DECLARE @Total decimal(18,3)
	SELECT @Total = SUM(ResourceQuantity * ResourcePrice) 
	FROM Bud_TaskResource AS TR
	JOIN (
		SELECT Bud_Task.* FROM Bud_Task
		JOIN 
		(
			SELECT * FROM Bud_Task
			WHERE TaskId = @TaskId
		) AS T ON Bud_Task.PrjId = T.PrjId 
			AND Bud_Task.Version = T.Version
			AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
			--AND Bud_Task.TaskId <> T.TaskId
	) AS T2 ON TR.TaskId = T2.TaskId
	RETURN @Total
END 
GO

--函数：根据TaskId获取子项数        Bery    2012-11-20 13:38
IF OBJECT_ID('dbo.fn_GetCount') IS NOT NULL
	DROP FUNCTION dbo.fn_GetCount
GO
CREATE FUNCTION dbo.fn_GetCount(@TaskId nvarchar(200))
RETURNS int
BEGIN
	DECLARE @Count decimal(18,3)
	SELECT @Count = COUNT(1) 
	FROM Bud_Task
	JOIN 
		(
			SELECT * FROM Bud_Task
			WHERE TaskId = @TaskId
		) AS T ON Bud_Task.PrjId = T.PrjId 
			AND Bud_Task.Version = T.Version
			AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
			AND Bud_Task.TaskId <> T.TaskId
	RETURN @Count
END 
GO

--改变附件上传大小为1G FYY 2012-11-22 13:29:13.280
update XPM_Basic_AnnexSettings set FileSize=1073741824 
GO

--开工中添加实际负责人	Bery    2012-11-30 10:15
IF ((SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PT_StartReport') = 1
	AND
	(SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PT_StartReport' AND COLUMN_NAME = 'ActualPrincipal') = 0)
	ALTER TABLE PT_StartReport ADD ActualPrincipal nvarchar(50)
GO

--修改计划表的数据类型 fyy 2012-12-04 10:13:19.790 
ALTER TABLE Pm_WorkPlan_PlanSummary ALTER COLUMN WkpSelfScore decimal(18, 3)
GO

--把支出合同中乙方名称修改为ID          Bery            2012-12-10 16:35
UPDATE Con_Payout_Contract SET BName = (SELECT CorpID FROM XPM_Basic_ContactCorp WHERE XPM_Basic_ContactCorp.CorpName = BName)
WHERE BName IN ( SELECT CorpName FROM XPM_Basic_ContactCorp )
GO


--代表工作添加是否打开字段      bery    2012-12-13 14:01
ALTER TABLE PT_DBSJ ADD IsOpened bit DEFAULT(0) NOT NULL
GO
ALTER TABLE PT_DBSJ_Today ADD IsOpened bit DEFAULT(0) NOT NULL
GO

--预警添加是否打开字段
ALTER TABLE PT_Warning ADD IsOpened bit DEFAULT(0) NOT NULL
GO

--将未开工的中标项目移植到项目立项中		lhy     2012-12-13  19:30
--定义未开工的中标项目Guid游标
DECLARE currentZTBPrjGuid CURSOR FOR 
SELECT ZTB.PrjGuid FROM PT_PrjInfo_ZTB ZTB
LEFT JOIN PT_PrjInfo Info ON ZTB.PrjGuid=Info.PrjGuid
WHERE ZTB.PrjState=5 AND Info.PrjGuid IS NULL
ORDER BY ZTB.StartDate DESC

OPEN currentZTBPrjGuid --打开游标
DECLARE @PrjGuid nvarchar(500) --定义变量
set @PrjGuid=''
DECLARE @COUNT INT
SET @COUNT=1
FETCH NEXT FROM currentZTBPrjGuid INTO @PrjGuid   --循环游标
WHILE @@FETCH_STATUS = 0
BEGIN
	--TypeCode
	DECLARE @TypeCode NVARCHAR(80)
	SELECT @TypeCode=MAX(TypeCode)+@COUNT FROM PT_PrjInfo
	where len(typeCode)=5
	IF(len(@TypeCode)=1)
	BEGIN
		SET @TypeCode='0000'+@TypeCode
	END
	ELSE IF(len(@TypeCode)=2)
	BEGIN
		SET @TypeCode='000'+@TypeCode
	END
	ELSE IF(len(@TypeCode)=3)
	BEGIN
		SET @TypeCode='00'+@TypeCode
	END
	ELSE IF(len(@TypeCode)=4)
	BEGIN
		SET @TypeCode='0'+@TypeCode
	END
	ELSE IF(len(@TypeCode)=5)
	BEGIN
		SET @TypeCode=+@TypeCode
	END	
	--i_xh
	DECLARE @i_xh INT
	SELECT @i_xh=MAX(i_xh)+@COUNT FROM PT_PrjInfo
	
	--user
	DECLARE @user AS nvarchar(4000)
	set @user=''
	SELECT @user= ISNULL(@user + ',' ,',') + UserCode 
	FROM (
		SELECT DISTINCT UserCode FROM PT_PrjInfo_ZTB_User 
		WHERE PrjGuid = @PrjGuid
	)AS T
	if(@user='')
	BEGIN 
		SET @user='00000000'
	END
	--将中标的项目移植到项目立项中
	INSERT INTO PT_PrjInfo (TypeCode,i_xh,PrjState,xmgroup,UserCode,PrjCode,PrjGuid,PrjName,StartDate,
	EndDate,ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,
	Area,PrjKindClass,PrjPlace,Remark1,Owner,Counsellor,Designer,Inspector,PrjInfo,
	OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay,
	KeyPart,WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,
	UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,Podepom
	)
	SELECT @TypeCode,@i_xh,5,'',InputUser, PrjCode,PT_PrjInfo_ZTB.PrjGuid,PrjName,StartDate,EndDate,
	ComputeClass,RationClass,PrjCost,ContractSum,Duration,QualityClass,
	Area,PrjKindClass,PrjPlace,Remark,Owner,Counsellor,Designer,Inspector,PrjInfo,
	OwnerCode,MarketInfoGuid,Rank,BudgetWay,ContractWay,PayCondition,TenderWay,PayWay,KeyPart,
	WorkUnit,LinkMan,PrjManager,BuildingType,TotalHouseNum,BuildingArea,
	UsegrounArea,UndergroundArea,PrjFundInfo,OtherStatement,@user FROM PT_PrjInfo_ZTB 
	LEFT JOIN  PT_PrjInfo_ZTB_Detail ON PT_PrjInfo_ZTB.PrjGuid=PT_PrjInfo_ZTB_Detail.PrjGuid  
	WHERE PT_PrjInfo_ZTB.PrjGuid=@PrjGuid
	UPDATE PT_PrjInfo_ZTB_Detail SET SetUpFlowState=1 WHERE PrjGuid=@PrjGuid --修改审核状态
	SET @COUNT=@COUNT+1
FETCH NEXT FROM currentZTBPrjGuid INTO @PrjGuid
END 
CLOSE currentZTBPrjGuid				--关闭游标
DEALLOCATE currentZTBPrjGuid
GO

--修改项目立项项目经理字段长度		lhy		2012-12-17 15:30
ALTER TABLE PT_PrjInfo ALTER COLUMN PrjManager varchar(100)

--修改桌面上发布公告为历史公告 fyy 2012-12-18 17:08:30.660
UPDATE frame_Desktop_ModelInfo SET ModelID='280305',
       moreSrc='oa/bulletin/HistoryBulletin.aspx'
WHERE ModelID='280303' 
GO

--修改桌面上历史公告 fyy 2012-12-20 13:23:54.087
UPDATE frame_Desktop_ModelInfo SET 
       moreSrc='oa/Bulletin/BulletinManage.aspx?type=manage'
WHERE ModelID='280305' 
GO

--修改桌面上历史公告 fyy 2012-12-20 13:57:06.860
UPDATE frame_Desktop_UserModel SET ModelId='280305' where ModelId='280303'

UPDATE frame_Desktop_UserModel SET MNewName='公告管理' where ModelId='280305'

GO

--添加薪酬管理模块菜单			lhy  2012-12-21  16:15
INSERT INTO dbo.PT_MK VALUES
('09','薪酬管理','','y',21,'MenuIco/13.gif',2469,5,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('0901','工资项管理','/Salary2/SalaryItemList.aspx','y',1,'',2470,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('0902','帐套管理','/Salary2/SalaryBooksList.aspx','y',2,'',2471,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('0903','员工帐套管理','/Salary2/DepartmentFrame.aspx?path=UserSaBooks','y',3,'',2472,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('0904','工资管理','/Salary2/DepartmentFrame.aspx?path=SaMonthSalary','y',4,'',2473,0,'0','0','','1')
INSERT INTO dbo.PT_MK VALUES
('0905','发放工资','/Salary2/DepartmentFrame.aspx?path=PayoffSalary','y',5,'',2475,0,'0','0','','1')
GO
--创建表
--  Drop Foreign Key Constraints 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Month_ItemId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SA_MonthSalary DROP CONSTRAINT FK_Month_ItemId;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('PK_Books_ItemID') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SA_SalaryBooksItem DROP CONSTRAINT PK_Books_ItemID;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_Item_BooksId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SA_SalaryBooksItem DROP CONSTRAINT FK_Item_BooksId;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_UserBooksId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE SA_UserSalaryBooks DROP CONSTRAINT FK_UserBooksId;
--  Drop Tables, Stored Procedures and Views 
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_MonthSalary') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_MonthSalary;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_Payoff') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_Payoff;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_PersonalTax') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_PersonalTax;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_SalaryBooks') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_SalaryBooks;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_SalaryBooksItem') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_SalaryBooksItem;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_SalaryItem') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_SalaryItem;
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('SA_UserSalaryBooks') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE SA_UserSalaryBooks;
GO
--  Create Tables 
CREATE TABLE SA_MonthSalary ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	UserCode varchar(8) NOT NULL,    --  用户代码 
	Year int NOT NULL,    --  年份 
	Month int NOT NULL,    --  月份 
	ItemId nvarchar(200) NOT NULL,    --  工资项ID 
	Cost decimal(10,2) NOT NULL    --  费用 
);
CREATE TABLE SA_Payoff ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	UserCode varchar(8) NOT NULL,    --  员工编号 
	Year int NOT NULL,    --  年份 
	Month int NOT NULL,    --  月份 
	IsPayoff bit DEFAULT 1 NOT NULL    --  是否发放 
);
CREATE TABLE SA_PersonalTax ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	FloorLevel decimal(18,3) NOT NULL,    --  下限 
	TopLevel decimal(18,3) NOT NULL,    --  上限 
	TaxRate decimal(18,3) NOT NULL,    --  税率 
	Deduct decimal(18,3) NOT NULL    --  扣除数 
);
CREATE TABLE SA_SalaryBooks ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	Name nvarchar(200) NOT NULL,    --  名称 
	IsValid bit DEFAULT 1 NOT NULL,    --  是否可用 
	Note nvarchar(max),    --  备注 
	InputUser varchar(8) NOT NULL,    --  录入人 
	InputDate datetime NOT NULL
);
CREATE TABLE SA_SalaryBooksItem ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	No int NOT NULL,    --  序号 
	BooksId nvarchar(200) NOT NULL,    --  帐套ID 
	ItemId nvarchar(200) NOT NULL,    --  工资项ID 
	DefaultValue decimal(18,3),    --  默认值 
	IsFormula bit DEFAULT 0 NOT NULL,    --  是否公式 
	Formula nvarchar(4000),    --  公式 
	IsShow bit DEFAULT 1 NOT NULL    --  是否显示 
);
CREATE TABLE SA_SalaryItem ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	No int NOT NULL,    --  序号 
	Name nvarchar(200) NOT NULL,    --  名称 
	IsAllowDel bit DEFAULT 1 NOT NULL,    --  是否允许删除 
	Code nvarchar(30),    --  PersonalTax 表示 个人所得税 TaxExemption 表示 免征额 TaxRate 表示 税率 Deduct	表示 速扣数 
	Note nvarchar(max)    --  备注 
);
CREATE TABLE SA_UserSalaryBooks ( 
	Id nvarchar(200) NOT NULL,    --  ID 
	UserCode varchar(8) NOT NULL,    --  用户代码 
	BooksId nvarchar(200) NOT NULL    --  帐套ID 
);
GO
--  Create Primary Key Constraints 
ALTER TABLE SA_MonthSalary ADD CONSTRAINT PK_SA_MonthSalary 
	PRIMARY KEY CLUSTERED (Id);
ALTER TABLE SA_Payoff ADD CONSTRAINT PK_SA_Payoff 
	PRIMARY KEY CLUSTERED (Id);
ALTER TABLE SA_PersonalTax ADD CONSTRAINT PK_SA_PersonalTax 
	PRIMARY KEY CLUSTERED (Id);
ALTER TABLE SA_SalaryBooks ADD CONSTRAINT PK_SA_SalaryBooks 
	PRIMARY KEY CLUSTERED (Id);
ALTER TABLE SA_SalaryBooksItem ADD CONSTRAINT PK_SA_SalaryBooksItem 
	PRIMARY KEY CLUSTERED (Id);
ALTER TABLE SA_SalaryItem ADD CONSTRAINT PK_SA_SalaryItem 
	PRIMARY KEY CLUSTERED (Id);
ALTER TABLE SA_UserSalaryBooks ADD CONSTRAINT PK_SA_UserSalaryBooks 
	PRIMARY KEY CLUSTERED (Id);
--  Create Foreign Key Constraints 
ALTER TABLE SA_MonthSalary ADD CONSTRAINT FK_Month_ItemId 
	FOREIGN KEY (ItemId) REFERENCES SA_SalaryItem (Id);
ALTER TABLE SA_SalaryBooksItem ADD CONSTRAINT PK_Books_ItemID 
	FOREIGN KEY (ItemId) REFERENCES SA_SalaryItem (Id);
ALTER TABLE SA_SalaryBooksItem ADD CONSTRAINT FK_Item_BooksId 
	FOREIGN KEY (BooksId) REFERENCES SA_SalaryBooks (Id)
	ON DELETE CASCADE;
ALTER TABLE SA_UserSalaryBooks ADD CONSTRAINT FK_UserBooksId 
	FOREIGN KEY (BooksId) REFERENCES SA_SalaryBooks (Id)
	ON DELETE CASCADE;
GO
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_MonthSalary, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '用户代码', 'Schema', dbo, 'table', SA_MonthSalary, 'column', UserCode;
EXEC sp_addextendedproperty 'MS_Description', '年份', 'Schema', dbo, 'table', SA_MonthSalary, 'column', Year;
EXEC sp_addextendedproperty 'MS_Description', '月份', 'Schema', dbo, 'table', SA_MonthSalary, 'column', Month;
EXEC sp_addextendedproperty 'MS_Description', '工资项ID', 'Schema', dbo, 'table', SA_MonthSalary, 'column', ItemId;
EXEC sp_addextendedproperty 'MS_Description', '费用', 'Schema', dbo, 'table', SA_MonthSalary, 'column', Cost;
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_Payoff, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '员工编号', 'Schema', dbo, 'table', SA_Payoff, 'column', UserCode;
EXEC sp_addextendedproperty 'MS_Description', '年份', 'Schema', dbo, 'table', SA_Payoff, 'column', Year;
EXEC sp_addextendedproperty 'MS_Description', '月份', 'Schema', dbo, 'table', SA_Payoff, 'column', Month;
EXEC sp_addextendedproperty 'MS_Description', '是否发放', 'Schema', dbo, 'table', SA_Payoff, 'column', IsPayoff;
EXEC sp_addextendedproperty 'MS_Description', '个人所得税', 'Schema', dbo, 'table', SA_PersonalTax;
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_PersonalTax, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '下限', 'Schema', dbo, 'table', SA_PersonalTax, 'column', FloorLevel;
EXEC sp_addextendedproperty 'MS_Description', '上限', 'Schema', dbo, 'table', SA_PersonalTax, 'column', TopLevel;
EXEC sp_addextendedproperty 'MS_Description', '税率', 'Schema', dbo, 'table', SA_PersonalTax, 'column', TaxRate;
EXEC sp_addextendedproperty 'MS_Description', '扣除数', 'Schema', dbo, 'table', SA_PersonalTax, 'column', Deduct;
EXEC sp_addextendedproperty 'MS_Description', '帐套', 'Schema', dbo, 'table', SA_SalaryBooks;
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_SalaryBooks, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '名称', 'Schema', dbo, 'table', SA_SalaryBooks, 'column', Name;
EXEC sp_addextendedproperty 'MS_Description', '是否可用', 'Schema', dbo, 'table', SA_SalaryBooks, 'column', IsValid;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', SA_SalaryBooks, 'column', Note;
EXEC sp_addextendedproperty 'MS_Description', '录入人', 'Schema', dbo, 'table', SA_SalaryBooks, 'column', InputUser;
EXEC sp_addextendedproperty 'MS_Description', '帐套明细', 'Schema', dbo, 'table', SA_SalaryBooksItem;
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '序号', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', No;
EXEC sp_addextendedproperty 'MS_Description', '帐套ID', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', BooksId;
EXEC sp_addextendedproperty 'MS_Description', '工资项ID', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', ItemId;
EXEC sp_addextendedproperty 'MS_Description', '默认值', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', DefaultValue;
EXEC sp_addextendedproperty 'MS_Description', '是否公式', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', IsFormula;
EXEC sp_addextendedproperty 'MS_Description', '公式', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', Formula;
EXEC sp_addextendedproperty 'MS_Description', '是否显示', 'Schema', dbo, 'table', SA_SalaryBooksItem, 'column', IsShow;
EXEC sp_addextendedproperty 'MS_Description', '工资项', 'Schema', dbo, 'table', SA_SalaryItem;
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_SalaryItem, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '序号', 'Schema', dbo, 'table', SA_SalaryItem, 'column', No;
EXEC sp_addextendedproperty 'MS_Description', '名称', 'Schema', dbo, 'table', SA_SalaryItem, 'column', Name;
EXEC sp_addextendedproperty 'MS_Description', '是否允许删除', 'Schema', dbo, 'table', SA_SalaryItem, 'column', IsAllowDel;
EXEC sp_addextendedproperty 'MS_Description', 'PersonalTax 表示 个人所得税
TaxExemption 表示 免征额
TaxRate 表示 税率
Deduct	表示 速扣数', 'Schema', dbo, 'table', SA_SalaryItem, 'column', Code;
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', SA_SalaryItem, 'column', Note;
EXEC sp_addextendedproperty 'MS_Description', '员工帐套', 'Schema', dbo, 'table', SA_UserSalaryBooks;
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', SA_UserSalaryBooks, 'column', Id;
EXEC sp_addextendedproperty 'MS_Description', '用户代码', 'Schema', dbo, 'table', SA_UserSalaryBooks, 'column', UserCode;
EXEC sp_addextendedproperty 'MS_Description', '帐套ID', 'Schema', dbo, 'table', SA_UserSalaryBooks, 'column', BooksId;
GO
--初始化薪酬
-- 初始化工资项
--DELETE FROM SA_SalaryItem
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4170', 1, '基本工资', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4171', 2, '岗位工资', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4172', 3, '奖金', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4173', 4, '扣款', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel, Code) VALUES('F397D994-993E-40BE-B0C3-B10156AF4174', 5, '应纳税所得额', 0, 'Taxable')
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel, Code) VALUES('F397D994-993E-40BE-B0C3-B10156AF4175', 6, '免征额', 0, 'TaxExemption')
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel, Code) VALUES('F397D994-993E-40BE-B0C3-B10156AF4176', 7, '适用税率', 0, 'TaxRate')
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel, Code) VALUES('F397D994-993E-40BE-B0C3-B10156AF4177', 8, '速扣数', 0, 'Deduct')
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel, Code) VALUES('F397D994-993E-40BE-B0C3-B10156AF4178', 9, '个人所得税', 0, 'PersonalTax')
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4179', 10, '电话补助', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4180', 11, '日工价', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4181', 12, '工日', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4183', 13, '应发工资', 0)
INSERT INTO SA_SalaryItem (Id, No, Name, IsAllowDel) VALUES('F397D994-993E-40BE-B0C3-B10156AF4182', 14, '实发工资', 0)
--初始化帐套
--DELETE FROM SA_SalaryBooks 
INSERT INTO SA_SalaryBooks(Id, Name, IsValid, InputUser, InputDate)
VALUES('EB62BE9A-5F51-4B10-8739-5CADCFA51540', '基本月薪帐套', 1, '00000000', GETDATE())
INSERT INTO SA_SalaryBooks(Id, Name, IsValid, InputUser, InputDate)
VALUES('EB62BE9A-5F51-4B10-8739-5CADCFA51541', '日薪帐套', 1, '00000000', GETDATE())
--初始化个人所得税
--DELETE FROM SA_PersonalTax
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F980', 0, 1500, 0.03, 0)
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F981', 1500, 4500, 0.1, 105)
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F982', 4500, 9000, 0.2, 555)
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F983', 9000, 35000, 0.25, 1005)
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F984', 35000, 55000, 0.3, 2755)
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F985', 55000, 80000, 0.35, 5505)
INSERT INTO SA_PersonalTax(Id, FloorLevel, TopLevel, TaxRate, Deduct)
VALUES('875A8208-0828-4F63-8F79-C3CECED3F986', 80000, 99999999999999, 0.45, 13505)
--工资项明细
--DELETE FROM SA_SalaryBooksItem
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650421',1,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4170',2500.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650422',2,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4171',0.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650426',3,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4172',0.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650424',4,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4173',0.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650427',5,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4175',3500.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650423',6,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4176',0.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650420',7,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4177',0.000,0,NULL,1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650425',8,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4178',0.000,1,N'[371048b2-99db-4210-948c-f14c2f4b4b80]*[1afa4822-ecb3-4771-8e38-0ea644650423]-[1afa4822-ecb3-4771-8e38-0ea644650420]',1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'371048b2-99db-4210-948c-f14c2f4b4b80',9,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4174',0.000,1,N'[1afa4822-ecb3-4771-8e38-0ea644650428]-[1afa4822-ecb3-4771-8e38-0ea644650427]',1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650428',10,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4183',0.000,1,N'[1afa4822-ecb3-4771-8e38-0ea644650421]+[1afa4822-ecb3-4771-8e38-0ea644650422]+[1afa4822-ecb3-4771-8e38-0ea644650426]-[1afa4822-ecb3-4771-8e38-0ea644650424]',1)
INSERT INTO SA_SalaryBooksItem(Id,No,BooksId,ItemId,DefaultValue,IsFormula,Formula,IsShow) VALUES(N'1afa4822-ecb3-4771-8e38-0ea644650429',11,N'EB62BE9A-5F51-4B10-8739-5CADCFA51540',N'F397D994-993E-40BE-B0C3-B10156AF4182',0.000,1,N'[1afa4822-ecb3-4771-8e38-0ea644650428]-[1afa4822-ecb3-4771-8e38-0ea644650425]',1)
GO


--添加Res_Mapping表         Bery        2012-12-21 16:48
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Res_Mapping') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Res_Mapping
GO
CREATE TABLE Res_Mapping ( 
	Id nvarchar(200) NOT NULL,    -- ID 
	ResourceId nvarchar(200) NOT NULL,    -- 资源ID 
	ParentId nvarchar(200) NOT NULL    -- 父资源ID 
)
GO
ALTER TABLE Res_Mapping ADD CONSTRAINT PK_Res_Mapping 
	PRIMARY KEY CLUSTERED (Id)
GO
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', Res_Mapping, 'column', Id
GO
EXEC sp_addextendedproperty 'MS_Description', '资源ID', 'Schema', dbo, 'table', Res_Mapping, 'column', ResourceId
GO
EXEC sp_addextendedproperty 'MS_Description', '父资源ID', 'Schema', dbo, 'table', Res_Mapping, 'column', ParentId
GO
--把周计划月计划的普通审核改为标准审核    lhy     2012-12-25 09:00
--修改老的周计划月计划审核状态
UPDATE pm_workplan_weekplan SET WkpIsTrue=1
GO
--周计划
INSERT dbo.WF_BusinessCode VALUES('130','周计划审核','WkpId',
			'pm_workplan_weekplan','WkpId','WkpIsTrue',
			'/oa/WorkPlanAndSummary/ExplainPlan_1.aspx?Action=View&planType=0',null,
			'28',NULL,'WkpUserCode')
INSERT dbo.WF_Business_Class VALUES('130','001','周计划审核',NEWID())
--月计划
INSERT dbo.WF_BusinessCode VALUES('131','月计划审核','WkpId',
			'pm_workplan_weekplan','WkpId','WkpIsTrue',
			'/oa/WorkPlanAndSummary/ExplainPlan_1.aspx?Action=View&planType=1',null,
			'28',NULL,'WkpUserCode')
INSERT dbo.WF_Business_Class VALUES('131','001','月计划审核',NEWID())
GO

-- 修改项目小组成员         Bery        2012-12-25 09:04
IF OBJECT_ID('V_GetAllPrjMembers') IS NOT NULL 
DROP VIEW V_GetAllPrjMembers
GO
CREATE VIEW V_GetAllPrjMembers
AS
SELECT Member.PrjMemberId  
 ,Member.PrjGuid  
 ,v_xm AS MemberName --姓名  
 ,Member.InputDate   --录入时间  
 ,Member.Post        --岗位   
 ,Member.EducationalBackground   --学历和专业  
 ,Member.Technical               -- 职称  
 ,Member.PostAndCompetency       --资格证书  
 ,Member.PastPerformance        --AS 以往工作表现  
 ,Member.TrainingInformation            --上岗培训情况(只是培训中的培训课程)  
 FROM PT_PrjMember AS Member  
LEFT JOIN PT_yhmc AS Yh ON Member.MemberCode=Yh.v_yhdm  
GO


--修改公告管理表部门字段 fyy 2012-12-25 09:38:11.760
ALTER TABLE pt_Bulletin_Main ALTER COLUMN CorpCode nvarchar(4000)
ALTER TABLE pt_Bulletin_Main ALTER COLUMN DeptRange nvarchar(4000)
GO

--物资需求计划和物资采购计划增加备注列		lhy		2012-12-25	17:30
--在物资需求计划中增加备注
ALTER TABLE Sm_Wantplan_Stock ADD Remark nvarchar(MAX)
--在物资采购计划中增加备注
ALTER TABLE Sm_Purchaseplan_Stock ADD Remark nvarchar(MAX)
GO
--在模块管理里面添加我的邮箱 fyy 2012-12-26 09:30:57.887
INSERT [PT_MK] ( [C_MKDM] , [V_MKMC] , [V_CDLJ] , [C_BS] , [I_XH] , [V_IMG] , [I_ID] , [i_ChildNum] , [IsBasic] , [IsMaintainable] , [helppath] , [Isdisplay] ) VALUES ( '2819' , '我的邮箱' , '/OA2/Mail/MailFrame.aspx' , 'y' , 6 , '' , 2558 , 0 , '0' , '0' , '' , '1' )
GO

--增加材料汇总		lhy		2012-12-26 17:30
INSERT INTO dbo.PT_MK VALUES
('880610','材料汇总','/StockManage/Report/StuffSummarizing.aspx','y',10,'',2476,0,'0','0','','1')
UPDATE PT_MK SET i_ChildNum=i_ChildNum+1 WHERE C_MKDM='8806'
GO
--仓库添加是否作为对比库
ALTER TABLE Sm_Treasury ADD IsContrast BIT
GO

--删除跟周（月）计划有关的外键约束，并重新建立外键约束及级联删除  lhy  2012-12-27  14:00
alter table Pm_WorkPlan_Plansummary drop constraint FK_PlanSummary_REFERENCE_WeekPlan
ALTER TABLE Pm_WorkPlan_Plansummary ADD CONSTRAINT FK_PlanSummary_REFERENCE_WeekPlan 
	FOREIGN KEY (WkpId) REFERENCES Pm_WorkPlan_WeekPlan (WkpId)
	ON DELETE CASCADE;
alter table Pm_WorkPlan_WeekPlanDetails drop constraint FK_WeekPlanDetails_REFERENCE_WeekPlan
ALTER TABLE Pm_WorkPlan_WeekPlanDetails ADD CONSTRAINT FK_WeekPlanDetails_REFERENCE_WeekPlan 
	FOREIGN KEY (WkpId) REFERENCES Pm_WorkPlan_WeekPlan (WkpId)
	ON DELETE CASCADE;
alter table Pm_WorkPlan_WeekSummary drop constraint FK_WeekSummary_REFERENCE_WeekPlanDetails
ALTER TABLE Pm_WorkPlan_WeekSummary ADD CONSTRAINT FK_WeekSummary_REFERENCE_WeekPlanDetails 
	FOREIGN KEY (WkpDetailsId) REFERENCES Pm_WorkPlan_WeekPlanDetails (WkpDetailsId)
	ON DELETE CASCADE;
GO

-- 添加资源映射权限菜单             Bery        2012-12-28 09:01
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '991011')
	INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
        VALUES('991011','资源映射','BudgetManage/Resource/ResMapList.aspx','y',5,'',2474,0,'0','0','','1')
GO

