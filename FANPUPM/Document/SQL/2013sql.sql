--修改工资表中的金额列长度			lhy			2013-1-5 16:20
ALTER TABLE SA_MonthSalary ALTER COLUMN Cost DECIMAL(18,3)

-- 添加工资报表菜单             lhy        2013-1-8 17:30
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '0906')
	INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
        VALUES('0906','工资报表','/Salary2/DepartmentFrame.aspx?path=SaMonthReport','y',6,'',2477,0,'0','0','','1')
GO


-- *与正大无关的项目请执行下面语句          Bery        2013-01-14 12:33
--DROP TABLE dbo.C_ContractChange
--DROP TABLE dbo.C_ContractPartyB
--DROP TABLE dbo.C_EmailSendLog
--DROP TABLE dbo.C_ProjectSupplier
--DROP TABLE dbo.C_RealAmount
--DROP TABLE dbo.C_ContractPayment
--DROP TABLE dbo.C_ContractPaymentPlan
--DROP TABLE dbo.C_PaymentCondition
--DROP TABLE dbo.C_Contract
--DROP TABLE dbo.C_ContractType
--DROP TABLE dbo.C_Project
--DROP TABLE dbo.C_Customer
--DROP TABLE dbo.C_Area
--DROP TABLE dbo.C_ProjectType
--DROP TABLE dbo.C_ScoreDepWeight
--DROP TABLE dbo.C_SupplierCredit
--DROP TABLE dbo.C_SupplierScore
--DROP TABLE dbo.C_Credit
--DROP TABLE dbo.C_SupplierScoreProject
--DROP TABLE dbo.C_Supplier
--DROP TABLE dbo.C_SupplierType
--GO


--流程模板节点添加存储部门的字段    Bery    2013-01-17 14:33
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'WF_TemplateNode' AND column_name = 'DepCode')
	ALTER TABLE WF_TemplateNode ADD DepCode nvarchar(max)
GO


--更新流程模板              Bery        2013-01-31 10:16
UPDATE wf_businessCode
SET NameField ='ContractCode' ,ProjectField ='Project'
WHERE BusinessCode='103'
GO


-- 删除多余的流程           Bery        2013-02-02 10:00
IF EXISTS(SELECT * FROM WF_BusinessCode 
	WHERE BusinessCode = '036' AND LinkTable = 'Prj_ProgressPlan')
DELETE FROM WF_Business_Class WHERE BusinessCode = '036'
DELETE FROM WF_Template WHERE BusinessCode = '036'
DELETE FROM WF_BusinessCode
WHERE BusinessCode = '036' AND LinkTable = 'Prj_ProgressPlan'
GO

-- 处理错误数据             Bery        2013-02-03 15:09
IF NOT EXISTS(SELECT * FROM WF_Business_Class WHERE BusinessCode = '072')
	INSERT INTO WF_Business_Class(BusinessCode, BusinessClass, BusinessClassName, Id)
		VALUES('072', '001', '采购计划审核', '43D87CA4-DE2D-4950-86E6-E2AA6C771625')
GO

--目标成本资源增加项目字段，为防止代码中与Bud_Task中的PrjId重复    lhy   2013-02-19 10：00
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'Bud_TaskResource' AND column_name = 'PrjGuid')
	ALTER TABLE Bud_TaskResource ADD PrjGuid nvarchar(500)
GO
--目标成本资源增加版本号字段  为防止代码中与Bud_Task中的Version重复  lhy    
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'Bud_TaskResource' AND column_name = 'Versions')
	ALTER TABLE Bud_TaskResource ADD Versions INT
GO
--将预算分部分项中的项目Id和版本号移到预算资源中
UPDATE Bud_TaskResource SET PrjGuid=PrjId, Versions=Version
FROM Bud_TaskResource taskRes
INNER JOIN Bud_Task Task ON taskRes.TaskId=Task.TaskId
GO

--图纸管理增加的内容 FYY 2013-03-05 11:34:44.443
--创建图纸类型
CREATE TABLE [dbo].[OPM_Business_DataItem](
	[UID] [uniqueidentifier] NOT NULL,
	[CodeID] [uniqueidentifier] NULL,
	[PCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[FlowState] [int] NULL,
	[DesignDate] [datetime] NULL,
	[Remark] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[AddUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AddTime] [datetime] NULL,
	[IsValid] [char](1) COLLATE Chinese_PRC_CI_AS NULL,
	[I_xh] [int] NULL,
 CONSTRAINT [PK_OPM_Business_DataItem] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

--创建图纸计划表
CREATE TABLE [dbo].[OPM_Business_Data](
	[UID] [uniqueidentifier] NOT NULL,
	[BType] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PrjID] [uniqueidentifier] NULL,
	[BCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[BName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[CodeID] [int] NULL,
	[Designer] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[DutyUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[FlowState] [int] NOT NULL,
	[BeginDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Cause] [varchar](1500) COLLATE Chinese_PRC_CI_AS NULL,
	[Remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[AddUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[AddTime] [datetime] NULL,
	[IsValid] [char](1) COLLATE Chinese_PRC_CI_AS NULL,
	[I_xh] [int] NULL,
 CONSTRAINT [PK_OPM] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


insert into WF_BusinessCode values('821','图纸计划审批','UID','OPM_Business_Data','UID','FlowState','/OPM/Business_Data/Business_Data_View.aspx','NULL','27','Prjid','NULL')
GO
insert into WF_BusinessCode values('826','图纸审批','UID','OPM_Business_DataItem','UID','FlowState','/OPM/Business_Data/Business_Data_ItemView.aspx','NULL','27','NULL','NULL')
GO
insert into WF_Business_Class values('821','001','图纸计划审批',newid())
GO
insert into WF_Business_Class values('826','001','图纸审批',newid())
GO
insert into XPM_Basic_AnnexSettings values('2917','ImgBusiness','图纸计划附件','8388608','*','/UploadFiles/OPM/Business_Data/','8','01')
GO
insert into XPM_Basic_AnnexSettings values('2929','ImgManage','图纸管理附件','8388608','*','/UploadFiles/OPM/Business_DataItem/','8','01')
GO
ALTER TABLE XPM_Basic_CodeList ADD I_xh int
GO
UPDATE XPM_Basic_CodeList SET I_xh=0 
GO
INSERT INTO XPM_Basic_CodeType VALUES('图纸类别',1,1,'图纸类别','Img','0','2012-08-09 09:47:32.890',newid())
GO
INSERT INTO PT_MK VALUES(27,'图纸管理','','Y',2,'MenuIco/13.gif',2490,5,1,0,'',1)
GO
INSERT INTO PT_MK VALUES(2701,'图纸类型','/OPM/Business_Data/BasicCodeFrame.aspx?SignCode=Img','Y',1,'',2491,0,0,0,'',1)
GO
INSERT INTO PT_MK VALUES(2702,'图纸计划','StockManage/basicset/SmWantPlanFrame.aspx?businessType=ImgManage&opType=edit','Y',2,'',2492,0,0,0,'',1)
GO
INSERT INTO PT_MK VALUES(2703,'图纸会审','StockManage/basicset/SmWantPlanFrame.aspx?businessType=ImgMag&opType=img','Y',3,'',2493,0,0,0,'',1)
GO
INSERT INTO PT_MK VALUES(2704,'图纸进度一览','StockManage/basicset/SmWantPlanFrame.aspx?businessType=ImgManageView&opType=view','Y',4,'',2494,0,0,0,'',1)
GO
INSERT INTO PT_MK VALUES(2705,'图纸进度总览','/OPM/Business_Data/Business_Data_Schedule.aspx','Y',5,'',24905,0,0,0,'',1)
GO

--收入合同增加丙方			lhy		2013-03-06		9:00
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'Con_Incomet_Contract' AND column_name = 'CParty')
	ALTER TABLE Con_Incomet_Contract ADD CParty NVARCHAR(64)
GO

--修改图纸类别 fyy 2013-03-07
UPDATE PT_MK SET V_CDLJ='/OPM/Business_Data/codelist.aspx?tid=Img&w=0' WHERE C_MKDM='2701'
GO

--修改图纸类别 fyy 2013-03-08
UPDATE PT_MK SET V_CDLJ='/OPM/Business_Data/Business_Data_Schedule.aspx?businessType=Img' WHERE C_MKDM='2705'
GO

--添加表支付控制指标		lhy		 2013-03-08 18:40
IF OBJECT_ID('Con_Payout_PaymentTarget', 'U') IS NOT NULL
    DROP TABLE Con_Payout_PaymentTarget
GO
CREATE TABLE Con_Payout_PaymentTarget
(
    TargetId nvarchar(64) PRIMARY KEY NOT NULL,
    PaymentId nvarchar(64) REFERENCES Con_Payout_Payment(Id) ON DELETE CASCADE,--支付Id
    ConTargetId nvarchar(500) REFERENCES Con_Payout_Target(TargetId),--合同控制指标Id
    PaymentAmount decimal(18,3),				--支付金额
    InputUser nvarchar(50) NOT NULL,		--录入人
    InputDate datetime NOT NULL DEFAULT(GETDATE())	--录入日期
)
GO

--添加备份数据库菜单  FYY  2013-03-19 11:29:45.270
insert into PT_MK values('3811','备份数据库','/backup/BackupList.aspx','y','28','','2489',0,0,0,'',1)


--内部应用表更换主键    Bery    2013-03-21 11:31
ALTER TABLE frame_Desktop_Menulink DROP PK_frame_Desktop_Menulink
GO
ALTER TABLE frame_Desktop_Menulink ADD Id nvarchar(200) NOT NULL DEFAULT(NEWID())
GO
ALTER TABLE frame_Desktop_Menulink ADD CONSTRAINT PK_Desktop_Menulink_Id PRIMARY KEY(Id)
GO


--添加经营情况分析总表菜单             lhy        2013-03-22 17:30
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '051102')
	INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
        VALUES('051102','经营情况分析总表','/ContractManage/ContractForm/OperateSituation.aspx','y',2,'',2496,0,'0','0','','1')
GO

--将流程接收人的长度该为 NVARHCAR(MAX)				lhy    2013-04-02		13:31
ALTER TABLE WF_TemplateNode ALTER Column Operater NVARCHAR(MAX)
GO

--修改流程审核表，去掉LinkTable字段.dbo前缀 fyy 2013-04-03 13:01:49.273
UPDATE WF_BusinessCode SET LinkTable=SUBSTRING(LinkTable,4,len(LinkTable)-3)
WHERE BusinessCode in(
SELECT BusinessCode FROM WF_BusinessCode WHERE LinkTable like '%dbo.%')


--添加模块 FYY 2013-04-07
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '290404')
	INSERT INTO PT_MK VALUES('290404','费用查询(自己)','BudgetManage/Cost/CostDiaryQueryOneself.aspx','y','2','','2498',0,0,0,'',1)
GO

UPDATE PT_MK SET V_MKMC='费用查询(财务)' WHERE C_MKDM='290402'
GO

--添加超级删除密码      Bery   2013-04-08 10:24 
IF (SELECT COUNT(*) FROM Basic_Config WHERE ParaName = 'TheDeletePwd') = 0
	INSERT INTO Basic_Config (Id, ParaName, ParaValue, Note) 
	VALUES('89E0AACC-3DD3-411B-8FAF-672EB26A7330', 'TheDeletePwd', '123', '超级删除密码')
GO

--项目过程管理项目整改表中 整改计划字段修改为1000字符	lhy   2013-04-27 15:00
ALTER TABLE Prj_ItemInspect ALTER Column PrjPlan Nvarchar(1000)

--修改审核开工、停工、复工审核的审核名称	lhy		2013-04-28	11:45
UPDATE WF_BusinessCode SET BusinessName='开工报告审核' WHERE BusinessCode='126'
UPDATE WF_BusinessCode SET BusinessName='停工报告审核' WHERE BusinessCode='127'
UPDATE WF_BusinessCode SET BusinessName='复工报告审核' WHERE BusinessCode='128'
GO
UPDATE WF_Business_Class SET BusinessClassName='开工报告审核' WHERE BusinessCode='126'
UPDATE WF_Business_Class SET BusinessClassName='停工报告审核' WHERE BusinessCode='127'
UPDATE WF_Business_Class SET BusinessClassName='复工报告审核' WHERE BusinessCode='128'
GO


--从修改中标预算        Bery    2013-05-06 09:43
--中标预算资源表添加项目GUID字段
ALTER TABLE Bud_ContractResource ADD PrjGuid nvarchar(100)
GO
--中标预算添加节点类型，标识该节点是总预算还是月度或者年度
--NULL表示总预算，Y表示年度预算，M表示月度预算
ALTER TABLE Bud_ContractTask ADD TaskType char(1)
GO


--修改通用流程设置中的名称      Bery    2013-05-06 14:06
UPDATE WF_Business_Class SET BusinessClassName = '设备计划' WHERE BusinessCode = '032'
UPDATE WF_BusinessCode SET BusinessName = '设备计划' WHERE BusinessCode = '032'
GO


--中标预算添加工期字段          Bery    2013-05-07 14:13
ALTER TABLE Bud_ContractTask ADD ConstructionPeriod int 
GO
EXEC sp_addextendedproperty @name = 'MS_Description', @value = '工期', @level0type = 'SCHEMA', @level0name = 'dbo',
    @level1type = 'TABLE', @level1name = 'Bud_ContractTask', @level2type = 'COLUMN', @level2name = 'ConstructionPeriod'
GO


--将合同预算修改为中标预算      Bery    2013-05-08 09:20
UPDATE PT_MK SET V_MKMC = '中标预算' WHERE C_MKDM = '1307'
UPDATE PT_MK SET V_MKMC = '中标预算导入' WHERE C_MKDM = '130701'
UPDATE PT_MK SET V_MKMC = '中标预算查询' WHERE C_MKDM = '130702'
UPDATE PT_MK SET V_MKMC = '中标预算清单' WHERE C_MKDM = '130703'
UPDATE WF_BusinessCode SET BusinessName = '中标预算审批' WHERE BusinessCode = '121'
UPDATE WF_Business_Class SET BusinessClassName = '中标预算审批' WHERE BusinessCode = '121'
GO

--修改中标预算的类型            Bery    2013-05-10 16:26
ALTER TABLE Bud_ContractTask DROP COLUMN TaskType
GO
ALTER TABLE Bud_ContractTask ADD TaskType char(1) NOT NULL DEFAULT('')
GO

--修改菜单名称                  Bery    2013-05-14 09:05
UPDATE PT_MK SET V_MKMC = '超级删除密码设置' WHERE C_MKDM = '382602'
GO

--修改字段长度                  Bery    2013-05-21 11:46
ALTER TABLE XPM_Basic_CodeType ALTER COLUMN Owner varchar(8)
ALTER TABLE XPM_Basic_CodeList ALTER COLUMN Owner varchar(8)
GO


--预算变更			lhy			2013-05-24  20:00
--新增预算变更菜单
INSERT INTO PT_MK VALUES('130502','预算变更','BudgetManage/Budget/BudModifyList.aspx',
'y','2','','2499',0,0,0,'',1)
GO

--创建表
--变更表
IF OBJECT_ID('Bud_Modify', 'U') IS NOT NULL
    DROP TABLE Bud_Modify
GO
CREATE TABLE Bud_Modify
(
    ModifyId NVARCHAR(64) PRIMARY KEY NOT NULL, --变更Id
	PrjId NVARCHAR(500) NOT NULL,--项目Id
	ModifyCode NVARCHAR(100) NOT NULL, --变更编号
    ModifyContent NVARCHAR(200) NOT NULL, --变更内容
	ModifyFileCode NVARCHAR(100) NOT NULL, --变更文件编号
    BudAmount DECIMAL(18,3) NOT NULL, --预算成本
	ReportAmount DECIMAL(18,3) NOT NULL, --报审价
    ApprovalAmount DECIMAL(18,3) NOT NULL, --核准价
    ApprovalDate DATETIME, --核准时间
    Note NVARCHAR(MAX), --备注
	Flowstate INT NOT NULL, --流程状态
	InputUser NVARCHAR(8) NOT NULL, --录入人
	InputDate DATETIME NOT NULL, --录入时间
	LastModifyUser NVARCHAR(8) NOT NULL, --最终修改人	
	LastModifyDate DATETIME  NOT NULL--最终修改时间
) 
GO

--任务变更表
IF OBJECT_ID('Bud_ModifyTask', 'U') IS NOT NULL
    DROP TABLE Bud_ModifyTask
GO
CREATE TABLE Bud_ModifyTask
(
    ModifyTaskId NVARCHAR(64) PRIMARY KEY NOT NULL,	--任务变更Id
	ModifyId NVARCHAR(64) REFERENCES Bud_Modify(ModifyId) ON DELETE CASCADE, --变更Id
	TaskId NVARCHAR(500) NOT NULL, --变更Id
	ModifyTaskCode NVARCHAR(100) NOT NULL,	--变更编号
    ModifyTaskContent NVARCHAR(200) NOT NULL, --变更内容
	Unit NVARCHAR(500), --单位
    Quantity DECIMAL(18,3) NOT NULL,	--数量
	UnitPrice DECIMAL(18,3) NOT NULL, --单价
    Total DECIMAL(18,3) NOT NULL, --小计
    StartDate DATETIME, --开始时间
	EndDate DATETIME, --结束时间
    OrderNumber NVARCHAR(500), --排序号
    Note NVARCHAR(MAX), --备注
	ModifyType INT --变更类型 0代表清单外 1代表清单外
) 
GO

--资源变更表
IF OBJECT_ID('Bud_ModifyTaskRes', 'U') IS NOT NULL
    DROP TABLE Bud_ModifyTaskRes
GO
CREATE TABLE Bud_ModifyTaskRes
(
	ModifyTaskResId NVARCHAR(64) PRIMARY KEY NOT NULL, --物资变更Id
	ModifyTaskId NVARCHAR(64) REFERENCES Bud_ModifyTask(ModifyTaskId) ON DELETE CASCADE, --任务变更Id
	ModifyId NVARCHAR(64), --变更Id
	ResourceId NVARCHAR(500), --资源Id
	ResourceQuantity DECIMAL(18,3) NOT NULL, --变更数量
	ResourcePrice DECIMAL(18,3) NOT NULL--变更单价	
)
GO

--创建读取变更任务小计的函数
IF OBJECT_ID('fn_GetModifyTotal',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetModifyTotal
GO
--创建读取变更任务的小计
CREATE FUNCTION [fn_GetModifyTotal](@TaskId nvarchar(200))
RETURNS  decimal(18,3)
BEGIN
	DECLARE @Total decimal(18,3)
	SELECT @Total=SUM(ISNULL(allModifyTask.Total,0)+ISNULL(InBudModiyInfo.Total,0))
	FROM 
	(
		--全部清单外变更任务信息
		SELECT Bud_ModifyTask.*,Bud_Modify.PrjId FROM Bud_ModifyTask 
		JOIN Bud_Modify ON Bud_ModifyTask.ModifyId=Bud_Modify.ModifyId
		WHERE FlowState=1 AND ModifyType=0
	) allModifyTask JOIN 
	(
		--参数所属的清单外变更任务信息
		SELECT Bud_ModifyTask.*,Bud_Modify.PrjId FROM Bud_ModifyTask JOIN Bud_Modify 
		ON Bud_ModifyTask.ModifyId=Bud_Modify.ModifyId 
		WHERE ModifyTaskId=@TaskId
	) currentModifyTask ON allModifyTask.prjId=currentModifyTask.PrjId
	AND allModifyTask.OrderNumber like currentModifyTask.OrderNumber+'%'
	LEFT JOIN 
	(
		--清单内的变更金额
		SELECT Bud_ModifyTask.TaskId,SUM(Bud_ModifyTask.Total) Total FROM Bud_ModifyTask 
		JOIN Bud_Modify ON Bud_ModifyTask.ModifyId=Bud_Modify.ModifyId
		WHERE FlowState=1 AND ModifyType=1 group by Bud_ModifyTask.TaskId
	) InBudModiyInfo ON allModifyTask.ModifyTaskId=InBudModiyInfo.TaskId
	RETURN @Total
END
GO 

--创建原预算中读取小计的函数
IF OBJECT_ID('fn_GetTotal',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetTotal
GO
CREATE FUNCTION fn_GetTotal(@TaskId nvarchar(200))
RETURNS int
BEGIN
	DECLARE @Total decimal(18,3),@modifyCount decimal(18,3)
	--原预算金额
	SELECT @Total=SUM(ISNULL(ResourceQuantity*ResourcePrice,0)+ISNULL(InBudModiyInfo.Total,0)
	+ISNULL(OutBudModifyInfo.Total,0))
	FROM Bud_TaskResource AS TR
	RIGHT JOIN (
		SELECT Bud_Task.* FROM Bud_Task
		JOIN 
		(
			SELECT * FROM Bud_Task
			WHERE TaskId = @TaskId
		) AS T ON Bud_Task.PrjId = T.PrjId 
			AND Bud_Task.Version = T.Version
			AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
	) AS T2 ON TR.TaskId = T2.TaskId
	LEFT JOIN 
	(
		--清单内的变更金额
		SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
		JOIN 
		(
			SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
			ON Bud_Modify.prjId=Bud_Task.PrjId 
			WHERE Bud_Task.TaskId = @TaskId
		) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
		where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
	) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
	LEFT JOIN
	(
		--清单外的变更金额
		SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
		from Bud_ModifyTask modifyTask JOIN 
		(
			SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
			ON Bud_Modify.prjId=Bud_Task.PrjId 
			WHERE Bud_Task.TaskId = @TaskId
		) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
		WHERE budModify.FlowState=1 AND modifyType=0 
		GROUP BY modifyTask.TaskId
	) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	RETURN @Total
END
GO

--读取预算中子节点的个数     
IF OBJECT_ID('fn_GetCount',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetCount
GO
CREATE FUNCTION [fn_GetCount](@TaskId nvarchar(200))
RETURNS int
BEGIN
	DECLARE @Count decimal(18,3),@modifyCount decimal(18,3),@modifyChildCount decimal(18,3) 
	--原预算下的子节点个数
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
	--原预算下变更的子节点个数
	SELECT @modifyCount=COUNT(*) 
	FROM Bud_ModifyTask modifyTask JOIN Bud_Modify budModify ON modifyTask.ModifyId=budModify.ModifyId
	JOIN 
	(
		SELECT * FROM Bud_Task where taskId=@TaskId
	) budTask ON modifyTask.orderNumber like budTask.orderNumber+'%'
		AND budModify.PrjId=budTask.PrjId 
		AND budModify.FlowState=1
	--变更中变更的子节点个数
	SELECT @modifyChildCount=COUNT(*) FROM Bud_ModifyTask mainModifyTask JOIN Bud_Modify mainBudModify 
	ON mainModifyTask.ModifyId=mainBudModify.ModifyId
	JOIN 
	(
		SELECT modifyTask.*,budModify.PrjId from Bud_ModifyTask modifyTask 
		JOIN Bud_Modify budModify ON modifyTask.ModifyId=budModify.ModifyId
		WHERE ModifyTaskId=@TaskId
	) modifyTaskIdInfo ON mainBudModify.PrjId=modifyTaskIdInfo.PrjId
	AND mainModifyTask.OrderNumber LIKE modifyTaskIdInfo.OrderNumber+'%'
	AND mainModifyTask.ModifyTaskId <> modifyTaskIdInfo.ModifyTaskId
	AND mainBudModify.FlowState=1
	RETURN @Count+@modifyCount+@modifyChildCount
END
GO 

--创建预算变更审核
INSERT INTO WF_BusinessCode VALUES ('132','预算变更审核','ModifyId','Bud_Modify','ModifyId','Flowstate',
                                    '/BudgetManage/Budget/BudModifyView.aspx',null,'13','PrjId','ModifyCode')
INSERT INTO WF_Business_Class VALUES ('132','001','预算变更审核',newId())
GO

--删除施工报量表和控制指标表中和预算关联的外键
--查询得到Bud_ConsTask与Bud_Task的外键名 ,施工报量
DECLARE @fkName nvarchar(500),@sql nvarchar(500)
SELECT @fkName=fk.Name
FROM sys.foreign_keys AS fk
JOIN sys.objects AS o ON fk.referenced_object_id=o.object_id
where o.Name='Bud_Task' AND OBJECT_NAME(fk.parent_object_id)='Bud_ConsTask'
--删除此外键
--ALTER TABLE Bud_ConsTask DROP CONSTRAINT @fkName
set @sql= N'ALTER TABLE Bud_ConsTask DROP CONSTRAINT ' + @fkName 
EXEC sp_executesql @sql
GO

--查询得到Con_Payout_Target与Bud_Task的外键名，控制指标
DECLARE @fkName nvarchar(500),@sql nvarchar(500)
SELECT @fkName=fk.Name
FROM sys.foreign_keys AS fk
JOIN sys.objects AS o ON fk.referenced_object_id=o.object_id
where o.Name='Bud_Task' AND OBJECT_NAME(fk.parent_object_id)='Con_Payout_Target'
--删除此外键
--ALTER TABLE Bud_ConsTask DROP CONSTRAINT @fkName
set @sql= N'ALTER TABLE Con_Payout_Target DROP CONSTRAINT ' + @fkName 
EXEC sp_executesql @sql
GO


--中标预算施工报量                  Bery    2013-05-27 13:25
IF OBJECT_ID('Bud_ContractConsReport') IS NOT NULL
    DROP TABLE Bud_ContractConsReport
GO
CREATE TABLE Bud_ContractConsReport ( 	RptId nvarchar(200) NOT NULL PRIMARY KEY,           --  ID 
	PrjId nvarchar(200) NOT NULL,           --  项目ID 
	IsValid bit DEFAULT 1 NOT NULL,         --  是否有效 
	FlowState int DEFAULT -1 NOT NULL,      --  流程状态 
	Note nvarchar(max),                     --  备注 
	InputUser nvarchar(20) NOT NULL,        --  录入人 
	InputDate datetime NOT NULL             --  录入时间 
)
GO

IF OBJECT_ID('Bud_ContractConsTask') IS NOT NULL
    DROP TABLE Bud_ContractConsTask
GO
CREATE TABLE Bud_ContractConsTask ( 	ConsTaskId nvarchar(200) NOT NULL PRIMARY KEY,                --  ID 
	RptId nvarchar(200) NOT NULL REFERENCES Bud_ContractConsReport(RptId),  --  施工报量ID 
	TaskId nvarchar(500) NOT NULL REFERENCES Bud_ContractTask (TaskId),     --  分部分项ID 
	Amount decimal(18,3) DEFAULT 0.0 NOT NULL,          --  完成量(元) 
	ApproveAmount decimal(18,3) DEFAULT 0.0 NOT NULL,   --  核准量(元) 
	Note nvarchar(max)                                  --  备注 
)
GO



--中标预算施工报量添加审核          Bery    2013-05-27 13:25
insert dbo.WF_BusinessCode values('133', '中标预算施工报量', 'RptId', 'Bud_ContractConsReport', 'RptId',
	'FlowState', '/BudgetManage/Construct/ContractReportTaskQuery.aspx',null, '13','PrjId','(SELECT''查看'')')
GO
insert dbo.WF_Business_Class values('133','001','中标预算施工报量',NEWID())
GO

--在模块表中增加“项目成本计划分项分类表” 报表菜单			lhy		2013-05-30	11:30
INSERT INTO PT_MK VALUES('290716','项目成本计划分项分类表','BudgetManage/Report/CBSCost.aspx','y',21,'','2501',0,0,0,'',1)
GO

--在模块表中增加“质量亮点” 菜单			lhy		2013-05-30	16:30
INSERT INTO PT_MK VALUES('9308','质量亮点','','y',36,'','2502',2,0,0,'',1)
INSERT INTO PT_MK VALUES('930801','亮点维护','EPC/Frame.aspx?Url=../../EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx&Type=Edit&TypeId=8&PrjState=0',
							'y',1,'','2503',0,0,0,'',1)
INSERT INTO PT_MK VALUES('930802','亮点查看','EPC/Frame.aspx?Url=../../EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx&Type=List&TypeId=8&PrjState=0',
							'y',2,'','2504',0,0,0,'',1)
GO

--在模块表中增加“质量活动” 菜单			lhy		2013-05-30	16:30
INSERT INTO PT_MK VALUES('9307','质量活动','','y',37,'','2505',2,0,0,'',1)
INSERT INTO PT_MK VALUES('930701','活动维护','EPC/Frame.aspx?Url=../../EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx&Type=Edit&TypeId=9&PrjState=0',
							'y',1,'','2506',0,0,0,'',1)
INSERT INTO PT_MK VALUES('930702','活动查看','EPC/Frame.aspx?Url=../../EPC/QuaitySafety/QualityHighlights/HighlightsList.aspx&Type=List&TypeId=9&PrjState=0',
							'y',2,'','2507',0,0,0,'',1)
GO

--照片路径表                 lhy		2013-05-30
IF OBJECT_ID('XPM_Basic_Thumbnai') IS NOT NULL
    DROP TABLE XPM_Basic_Thumbnai
GO
CREATE TABLE XPM_Basic_Thumbnai(
	[ThumbnaiCode] [uniqueidentifier] NOT NULL,
	[ShowContent] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[ThumbnaName] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[ThumbnaImgPath] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[AddDate] [datetime] NULL,
	[ImgPath] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[Remark] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_XPM_Basic_Thumbnai] PRIMARY KEY CLUSTERED 
(
	[ThumbnaiCode] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO

--现场问题登记信息    
IF OBJECT_ID('PM_EPCM_IntendanceInfo') IS NOT NULL
    DROP TABLE PM_EPCM_IntendanceInfo
GO
CREATE TABLE [dbo].[OPM_EPCM_IntendanceInfo](
	[NoteId] [uniqueidentifier] NOT NULL,
	[IntendanceGuid] [uniqueidentifier] NULL,
	[AskQuestionsYhdm] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[AskQuestionsDate] [datetime] NULL,
	[QuestionExplain] [text] COLLATE Chinese_PRC_CI_AS NULL,
	[SettleYhdm] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[SettleDate] [datetime] NULL,
	[SettleExplain] [text] COLLATE Chinese_PRC_CI_AS NULL,
	[QuestionTag] [int] NULL,
	[SettleToPerson] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ToCause] [text] COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_EPM_IntendanceInfo] PRIMARY KEY CLUSTERED 
(
	[NoteId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题/解决记录id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'NoteId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主题id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'IntendanceGuid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题提出人代码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'AskQuestionsYhdm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题提出时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'AskQuestionsDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题描述' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'QuestionExplain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'解决回复人代码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'SettleYhdm'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'解决回复时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'SettleDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'解决说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'SettleExplain'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题回复标识：表示问题是否已回复' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceInfo', @level2type=N'COLUMN', @level2name=N'QuestionTag'
GO

--问题照片表               
IF OBJECT_ID('OPM_EPCM_IntendancePhotoList') IS NOT NULL
    DROP TABLE OPM_EPCM_IntendancePhotoList
GO
CREATE TABLE OPM_EPCM_IntendancePhotoList(
	[NoteID] [uniqueidentifier] NOT NULL,
	[InfoGuid] [uniqueidentifier] NULL,
	[PhotoNumber] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PhotoExplain] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[PhotoPath] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[PhotoName] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[OPyhdm] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[PhotoType] [int] NULL,
 CONSTRAINT [PK_EPM_INTENDANCEPHOTOLIST] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题/解决记录id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendancePhotoList', @level2type=N'COLUMN', @level2name=N'InfoGuid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'照片类别' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendancePhotoList', @level2type=N'COLUMN', @level2name=N'PhotoType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拍照监督从表' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendancePhotoList'
GO

--问题登记               
IF OBJECT_ID('OPM_EPCM_IntendanceMaster') IS NOT NULL
    DROP TABLE OPM_EPCM_IntendanceMaster
GO
--问题登记
CREATE TABLE [dbo].[OPM_EPCM_IntendanceMaster](
	[IntendanceGuid] [uniqueidentifier] ROWGUIDCOL  NOT NULL DEFAULT (newid()),
	[PrjGuid] [uniqueidentifier] NULL,
	[QuestionTitle] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[BookInDate] [datetime] NULL,
	[SettleState] [char](10) COLLATE Chinese_PRC_CI_AS NULL,
	[QuestionTypeId] [int] NULL,
	[SettleYhdm] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[OpYhdm] [nchar](10) COLLATE Chinese_PRC_CI_AS NULL,
	[QuestionTag] [int] NULL,
	[SettleToPerson] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_EPM_INTENDANCEMASTER] PRIMARY KEY CLUSTERED 
(
	[IntendanceGuid] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'监督记录ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceMaster', @level2type=N'COLUMN', @level2name=N'IntendanceGuid'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目GUID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceMaster', @level2type=N'COLUMN', @level2name=N'PrjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题标题' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceMaster', @level2type=N'COLUMN', @level2name=N'QuestionTitle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'拍照时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceMaster', @level2type=N'COLUMN', @level2name=N'BookInDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'处理状态（0未解决、1解决中、2已解决、3已解决并验证）' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceMaster', @level2type=N'COLUMN', @level2name=N'SettleState'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'问题类别' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_EPCM_IntendanceMaster', @level2type=N'COLUMN', @level2name=N'QuestionTypeId'
GO

--代办事宜              
IF OBJECT_ID('OPM_PT_ToDoList') IS NOT NULL
    DROP TABLE OPM_PT_ToDoList
GO
CREATE TABLE [dbo].[OPM_PT_ToDoList](
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[TypeCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[TargetNoteID] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[TipInfo] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Operater] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_PT_TODOLIST] PRIMARY KEY CLUSTERED 
(
	[NoteID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键，记录Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_PT_ToDoList', @level2type=N'COLUMN', @level2name=N'NoteID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'待办事宜类型代码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_PT_ToDoList', @level2type=N'COLUMN', @level2name=N'TypeCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'目标记录ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_PT_ToDoList', @level2type=N'COLUMN', @level2name=N'TargetNoteID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'提示信息' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_PT_ToDoList', @level2type=N'COLUMN', @level2name=N'TipInfo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员代码串（可以多个）' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_PT_ToDoList', @level2type=N'COLUMN', @level2name=N'Operater'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'系统待办事宜' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'OPM_PT_ToDoList'
GO

--添加问题类型
INSERT XPM_Basic_CodeType VALUES('问题类型',1,1,'问题监控','ProblemSupervise',0,GetDate(),newId())
GO

--在模块中增加“质量问题”的菜单
INSERT INTO PT_MK VALUES('9310','质量问题','','y',38,'','2508',5,0,0,'',1)
INSERT INTO PT_MK VALUES('931001','问题类型','/EPC/QuaitySafety/QualityQuestion/CodeList.aspx?w=0&tid=ProblemSupervise&r=n',
'y',1,'','2509',0,0,0,'',1)
INSERT INTO PT_MK VALUES('931002','质量问题管理','/EPC/QuaitySafety/QualityQuestion/PrjFrame.aspx?businessType=PhotoSupervise&opType=edit',
'y',2,'','2510',0,0,0,'',1)
INSERT INTO PT_MK VALUES('931003','质量问题一览','/EPC/QuaitySafety/QualityQuestion/PrjFrame.aspx?businessType=PhotoSuperviseView&opType=edit',
'y',3,'','2511',0,0,0,'',1)
INSERT INTO PT_MK VALUES('931004','待解决质量问题','/EPC/QuaitySafety/QualityQuestion/PhotosCheckIn/PhotosCheckInList2.aspx?pt=3',
'y',4,'','2512',0,0,0,'',1)
INSERT INTO PT_MK VALUES('931005','问题监控展示图','/EPC/QuaitySafety/QualityQuestion/moveBmp2.aspx?PHc=7&Ptype=CheckIn',
'y',5,'','2513',0,0,0,'',1)
GO


--目标成本添加工期字段          Bery    2013-05-31 11:03
ALTER TABLE Bud_Task ADD ConstructionPeriod int 
GO
EXEC sp_addextendedproperty @name = 'MS_Description', @value = '工期', @level0type = 'SCHEMA', @level0name = 'dbo',
    @level1type = 'TABLE', @level1name = 'Bud_Task', @level2type = 'COLUMN', @level2name = 'ConstructionPeriod'
GO

--模块表中删除原预算变更的菜单		lhy		2013-06-03		10:00
DELETE Pt_MK WHERE C_MKDM = '130501'
GO

--目标成本添加预算类型字段          Bery    2013-06-03 11:47
ALTER TABLE Bud_Task ADD TaskType char(1) NOT NULL DEFAULT('')
GO


--删除多余的外键约束                Bery    2013-06-03 15:26
IF OBJECT_ID('FK__Bud_TaskC__Input__292EA0A1') IS NOT NULL
	ALTER TABLE Bud_TaskChange DROP FK__Bud_TaskC__Input__292EA0A1
GO

--删除菜单：二次验证密码设置        Bery    2013-06-05 14:30
DELETE FROM PT_MK WHERE C_MKDM = '480101'


--修改错误数据                      Bery    2013-06-06 13:54
UPDATE WF_BusinessCode SET LinkTable = 'Sm_Purchaseplan' WHERE LinkTable = '.Sm_Purchaseplan'
UPDATE WF_BusinessCode SET LinkTable = 'Sm_Refunding' WHERE LinkTable = '.Sm_Refunding'
GO


--修改菜单                          Bery    2013-06-13 13:56
UPDATE PT_MK SET V_CDLJ = '/oa/Vehicle/Main/VehicleReport.aspx' WHERE C_MKDM = '28400103'
GO


--模块表中增加中标预算施工报量菜单		lhy		2013-06-14		10:00
INSERT INTO PT_MK VALUES('130704','施工报量','BudgetManage/Construct/ContractReport.aspx',
'y',4,'',2500,0,0,0,'',1)


--中标预算施工报量修改为产值上报        Bery    2013-06-17 09:51
UPDATE PT_MK SET V_MKMC = '产值上报' WHERE C_MKDM = '130704'
UPDATE WF_Business_Class SET BusinessClassName= '产值上报' WHERE BusinessCode = '133'
UPDATE WF_BusinessCode SET BusinessName = '产值上报' WHERE BusinessCode = '133'
GO

--调整施工报量位置                      Bery    2013-06-17 09:52
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
    VALUES('130103','预算变更','BudgetManage/Budget/BudModifyList.aspx','y',10,'',2514,0,'0','0','','1')
GO
UPDATE PT_YHMC_Privilege SET C_MKDM = '130103' 
WHERE C_MKDM = '130502' AND V_YHDM != '00000000'
GO
DELETE FROM PT_YHMC_Privilege WHERE C_MKDM = '130502'
DELETE FROM PT_YHMC_Privilege WHERE C_MKDM = '1305'
GO

--读取预算中子节点的个数			lhy		2013-06-18
IF OBJECT_ID('fn_GetCount',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetCount
GO
CREATE FUNCTION [fn_GetCount](@TaskId nvarchar(200))
RETURNS int
BEGIN
	DECLARE @Count decimal(18,3),@modifyCount decimal(18,3),@modifyChildCount decimal(18,3) 
	--原预算下的子节点个数
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
			AND Bud_Task.ParentId=T.TaskId 
	--原预算下变更的子节点个数
	SELECT @modifyCount=COUNT(*) 
	FROM Bud_ModifyTask modifyTask JOIN Bud_Modify budModify ON modifyTask.ModifyId=budModify.ModifyId
	JOIN 
	(
		SELECT * FROM Bud_Task where taskId=@TaskId
	) budTask ON modifyTask.orderNumber like budTask.orderNumber+'%'
		AND budModify.PrjId=budTask.PrjId 
		AND budModify.FlowState=1
		AND modifyTask.TaskId = budTask.TaskId 
	--变更中变更的子节点个数
	SELECT @modifyChildCount=COUNT(*) FROM Bud_ModifyTask mainModifyTask JOIN Bud_Modify mainBudModify 
	ON mainModifyTask.ModifyId=mainBudModify.ModifyId
	JOIN 
	(
		SELECT modifyTask.*,budModify.PrjId from Bud_ModifyTask modifyTask 
		JOIN Bud_Modify budModify ON modifyTask.ModifyId=budModify.ModifyId
		WHERE ModifyTaskId=@TaskId
	) modifyTaskIdInfo ON mainBudModify.PrjId=modifyTaskIdInfo.PrjId
	AND mainModifyTask.OrderNumber LIKE modifyTaskIdInfo.OrderNumber+'%'
	AND mainModifyTask.ModifyTaskId <> modifyTaskIdInfo.ModifyTaskId
	AND mainBudModify.FlowState=1
	RETURN @Count+@modifyCount+@modifyChildCount
END
GO 

--预算变更添加工期字段          
ALTER TABLE dbo.Bud_ModifyTask ADD ConstructionPeriod int 
GO


--添加菜单
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
    VALUES('480101','二次验证密码设置','EPC/WorkFlow/SetAuditPwd.aspx','y',1,'',24907,0,'0','0','','1')
GO

--删除超级删除菜单
DELETE FROM PT_MK WHERE C_MKDM = '382602'
GO


--中标预算变更表			lhy			2013-06-19
IF OBJECT_ID('Bud_ConModify', 'U') IS NOT NULL
    DROP TABLE Bud_ConModify
GO
CREATE TABLE Bud_ConModify
(
    ModifyId nvarchar(64) PRIMARY KEY NOT NULL, --变更Id
	PrjId nvarchar(500) NOT NULL,--项目Id
	ModifyCode nvarchar(100) NOT NULL, --变更编号
    ModifyContent nvarchar(200) NOT NULL, --变更内容
	ModifyFileCode nvarchar(100) NOT NULL, --变更文件编号
    BudAmount decimal(18,3) NOT NULL, --预算成本
	ReportAmount decimal(18,3) NOT NULL, --报审价
    ApprovalAmount decimal(18,3) NOT NULL, --核准价
    ApprovalDate datetime, --核准时间
    Note nvarchar(MAX), --备注
	Flowstate int NOT NULL, --流程状态
	InputUser nvarchar(8) NOT NULL, --录入人
	InputDate datetime NOT NULL, --录入时间
	LastModifyUser nvarchar(8) NOT NULL, --最终修改人	
	LastModifyDate datetime  NOT NULL--最终修改时间
) 
GO
--任务变更表
IF OBJECT_ID('Bud_ConModifyTask', 'U') IS NOT NULL
    DROP TABLE Bud_ConModifyTask
GO
CREATE TABLE Bud_ConModifyTask
(
    ModifyTaskId nvarchar(64) PRIMARY KEY NOT NULL,	--任务变更Id
	ModifyId nvarchar(64) REFERENCES Bud_ConModify(ModifyId) ON DELETE CASCADE, --变更Id
	TaskId nvarchar(500) NOT NULL, --变更Id
	ModifyTaskCode nvarchar(100) NOT NULL,	--变更编号
    ModifyTaskContent nvarchar(200) NOT NULL, --变更内容
	Unit nvarchar(500), --单位
    Quantity decimal(18,3) NOT NULL,	--数量
	UnitPrice decimal(18,3) NOT NULL, --单价
    Total decimal(18,3) NOT NULL, --小计
    StartDate datetime, --开始时间
	EndDate datetime, --结束时间
    OrderNumber nvarchar(500), --排序号
    Note nvarchar(MAX), --备注
	ModifyType int, --变更类型 0代表清单外 1代表清单外
	ConstructionPeriod int  --工期
) 
GO

--模块表中增加中标预算变更菜单
INSERT INTO dbo.PT_MK VALUES
('130705','中标预算变更','BudgetManage/Budget/BudConModifyList.aspx','y',5,'',2515,0,'0','0','','1')
GO

--中标预算审核
insert dbo.WF_BusinessCode values('134','中标预算变更审核','ModifyId',
			'Bud_ConModify','ModifyId','Flowstate',
			'/BudgetManage/Budget/BudConModifyView.aspx',null,
			'13','PrjId','ModifyCode')
insert dbo.WF_Business_Class values('134','001','中标预算变更审核',newId())
GO

--读取中标预算中子节点的个数
IF OBJECT_ID('fn_GetContractTaskCount',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetContractTaskCount
GO
CREATE FUNCTION fn_GetContractTaskCount (@TaskId nvarchar(200))
RETURNS int
BEGIN
	DECLARE  @Count decimal(18,3),@conModifyCount decimal(18,3),@conModifyChildCount decimal(18,3)
	--原预算下的子节点个数
	SELECT @Count = COUNT(1) 
	FROM Bud_ContractTask
	JOIN 
		(
			SELECT * FROM Bud_ContractTask
			WHERE TaskId = @TaskId
		) AS T ON Bud_ContractTask.PrjId = T.PrjId 
			AND Bud_ContractTask.OrderNumber LIKE T.OrderNumber + '%'
			AND Bud_ContractTask.TaskId <> T.TaskId 
			AND Bud_ContractTask.ParentId=T.TaskId 
	--原预算下变更的子节点个数
	SELECT @conModifyCount=COUNT(*) 
	FROM Bud_ConModifyTask conModifyTask JOIN Bud_ConModify budConModify ON conModifyTask.ModifyId=budConModify.ModifyId
	JOIN 
	(
		SELECT * FROM Bud_ContractTask where taskId=@TaskId
	) budConTask ON conModifyTask.orderNumber like budConTask.orderNumber+'%'
		AND budConModify.PrjId=budConTask.PrjId 
		AND budConModify.FlowState=1
		AND conModifyTask.TaskId = budConTask.TaskId 
	--变更中变更的子节点个数
	SELECT @conModifyChildCount=COUNT(*) FROM Bud_ConModifyTask mainConModifyTask 
	JOIN Bud_ConModify mainConBudModify ON mainConModifyTask.ModifyId=mainConBudModify.ModifyId
	JOIN 
	(
		SELECT modifyTask.*,budModify.PrjId from Bud_ConModifyTask modifyTask 
		JOIN Bud_ConModify budModify ON modifyTask.ModifyId=budModify.ModifyId
		WHERE ModifyTaskId=@TaskId
	) conModifyTaskIdInfo ON mainConBudModify.PrjId=conModifyTaskIdInfo.PrjId
	AND mainConModifyTask.OrderNumber LIKE conModifyTaskIdInfo.OrderNumber+'%'
	AND mainConModifyTask.ModifyTaskId <> conModifyTaskIdInfo.ModifyTaskId
	AND mainConBudModify.FlowState=1
	RETURN @Count+@conModifyCount+@conModifyChildCount
END
GO 


--原预算中读取小计的函数		lhy		2013-06-20   20:00
IF OBJECT_ID('fn_GetTotal',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetTotal
GO
CREATE FUNCTION fn_GetTotal(@TaskId nvarchar(200))
RETURNS int
BEGIN
	DECLARE @Total decimal(18,3),@modifyCount decimal(18,3)
	SELECT @Total=SUM(ISNULL(ResourceQuantity*ResourcePrice,0)+ISNULL(InBudModiyInfo.Total,0)
	+ISNULL(OutBudModifyInfo.Total,0))
	FROM Bud_TaskResource AS TR
	RIGHT JOIN (
		SELECT Bud_Task.* FROM Bud_Task
		JOIN 
		(
			SELECT * FROM Bud_Task
			WHERE TaskId = @TaskId
		) AS T ON Bud_Task.PrjId = T.PrjId 
			AND Bud_Task.Version = T.Version
			AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
			AND Bud_Task.TaskType=T.TaskType
	) AS T2 ON TR.TaskId = T2.TaskId
	LEFT JOIN 
	(
		--清单内的变更金额
		SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
		JOIN 
		(
			SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
			ON Bud_Modify.prjId=Bud_Task.PrjId 
			WHERE Bud_Task.TaskId = @TaskId
		) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
		where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
	) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
	LEFT JOIN
	(
		--清单外的变更金额
		SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
		from Bud_ModifyTask modifyTask JOIN 
		(
			SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
			ON Bud_Modify.prjId=Bud_Task.PrjId 
			WHERE Bud_Task.TaskId = @TaskId
		) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
		WHERE budModify.FlowState=1 AND modifyType=0 
		GROUP BY modifyTask.TaskId
	) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	RETURN @Total
END
GO

--原预算中读取小计的函数		lhy		2013-06-24   16:00
IF OBJECT_ID('fn_GetTotal',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetTotal
GO
CREATE FUNCTION fn_GetTotal(@TaskId nvarchar(200))
RETURNS decimal(18,3)
BEGIN
	DECLARE @Total decimal(18,3),@TaskType char(1),@modifyCount decimal(18,3)
	SELECT @TaskType=TaskType FROM Bud_Task WHERE TaskId = @TaskId
	SET @Total=0
	IF(@TaskType!='')
	BEGIN
		SELECT @Total=SUM(ISNULL(ResTotal,0)+ISNULL(InBudModiyInfo.Total,0)
		+ISNULL(OutBudModifyInfo.Total,0))
		FROM 
		(
			SELECT TasRes.TaskId,SUM(ResourceQuantity*ResourcePrice) ResTotal FROM Bud_TaskResource AS TR
			RIGHT JOIN (
				SELECT Bud_Task.* FROM Bud_Task
				JOIN 
				(
					SELECT * FROM Bud_Task
					WHERE TaskId = @TaskId
				) AS T ON Bud_Task.PrjId = T.PrjId 
					AND Bud_Task.Version = T.Version
					AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
					AND Bud_Task.TaskType=T.TaskType
					AND SUBSTRING(Bud_Task.TaskId,1,7)=SUBSTRING(T.TaskId,1,7) 
			) AS TasRes ON TR.TaskId = TasRes.TaskId
			GROUP BY TasRes.TaskId
		) AS T2 
		LEFT JOIN 
		(
			--清单内的变更金额
			SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
			JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
			where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
		) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
		LEFT JOIN
		(
			--清单外的变更金额
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	ELSE
	BEGIN
		SELECT @Total=SUM(ISNULL(ResTotal,0)+ISNULL(InBudModiyInfo.Total,0)
		+ISNULL(OutBudModifyInfo.Total,0))
		FROM 
		(
			SELECT TasRes.TaskId,SUM(ResourceQuantity*ResourcePrice) ResTotal FROM Bud_TaskResource AS TR
			RIGHT JOIN (
				SELECT Bud_Task.* FROM Bud_Task
				JOIN 
				(
					SELECT * FROM Bud_Task
					WHERE TaskId = @TaskId
				) AS T ON Bud_Task.PrjId = T.PrjId 
					AND Bud_Task.Version = T.Version
					AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
					AND Bud_Task.TaskType=T.TaskType
			) AS TasRes ON TR.TaskId = TasRes.TaskId
			GROUP BY TasRes.TaskId
		) AS T2 
		LEFT JOIN 
		(
			--清单内的变更金额
			SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
			JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
			where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
		) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
		LEFT JOIN
		(
			--清单外的变更金额
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	RETURN @Total
END
GO

--模块表中将“中标预算” 修改成 “合同预算”		lhy     2013-07-09	17:00
UPDATE Pt_MK SET V_MKMC='合同预算' WHERE C_MKDM='1307'
UPDATE Pt_MK SET V_MKMC='合同预算导入' WHERE C_MKDM='130701'
UPDATE Pt_MK SET V_MKMC='合同预算查询' WHERE C_MKDM='130702'
UPDATE Pt_MK SET V_MKMC='合同预算清单' WHERE C_MKDM='130703'
UPDATE Pt_MK SET V_MKMC='合同预算变更' WHERE C_MKDM='130705'
GO

--审核表中将“中标预算” 修改成 “合同预算”		lhy    
UPDATE WF_Business_Class SET BusinessClassName='合同预算审批'
WHERE BusinessCode='121'
UPDATE WF_Business_Class SET BusinessClassName='合同预算变更审核'
WHERE BusinessCode='134'
UPDATE WF_BusinessCode SET BusinessName='合同预算审批'
WHERE BusinessCode='121'
UPDATE WF_BusinessCode SET BusinessName='合同预算变更审核'
WHERE BusinessCode='134'
GO

--模块表增加新的报表菜单			lhy			2013-07-22 9:10
INSERT INTO PT_MK VALUES 
('290704','人工费总分析表','BudgetManage/Report/LaborCostAnalysis.aspx','y',22,'',24908,0,0,0,'','1')
INSERT INTO PT_MK VALUES 
('290702','人工明细表','BudgetManage/Report/LaboreDetailAnalysis.aspx','y',23,'',24909,0,0,0,'','1')
INSERT INTO PT_MK VALUES 
('290720','材料明细表','BudgetManage/Report/StuffDetailAnalysis.aspx','y',24,'',24910,0,0,0,'','1')
INSERT INTO PT_MK VALUES 
('290701','机械明细表','BudgetManage/Report/MachineDetailAnalysis.aspx','y',25,'',24911,0,0,0,'','1')
INSERT INTO PT_MK VALUES 
('290722','人材机明细','BudgetManage/Report/LSMDetailAnalysis.aspx','y',26,'',24912,0,0,0,'','1')
GO

--项目投标 添加字段                      2013-7-30  10:55               gmd
ALTER TABLE  PT_PrjInfo_ZTB ADD OldState int                                --原来状态
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD GiveUpTime datetime                         --放弃日期
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD GiveUpReason nvarchar(max)                  --放弃原因
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD GiveUpNote  nvarchar(max)                   --备注
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD Operator varchar(8)                         --操作人
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD GiveUpFlowState int DEFAULT(-1) NOT NULL    --放弃流程审核状态
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD InitiateFlowState int DEFAULT(-1) NOT NULL  --报名流程审核状态
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD PftFlowState int DEFAULT(-1) NOT NULL       --资格预审流程审核状态
GO
ALTER TABLE PT_PrjInfo_ZTB  ADD BidFlowState int DEFAULT(-1) NOT NULL       --中标流程审核状态
GO

--投标状态调整表                       2013-7-30 10:55               gmd
IF OBJECT_ID('PT_PrjInfo_StateChange','U') IS NOT NULL
	DROP TABLE PT_PrjInfo_StateChange
GO
CREATE TABLE PT_PrjInfo_StateChange(
	Id nvarchar(100) PRIMARY KEY ,         ---主键
    PrjId nvarchar(200),                  --项目ID 
	OldState int ,                  	  ---原来状态
	ChangeState int,                	  ---调整状态
	ChangeTime datetime,            	  ---调整时间
	ChangeReason nvarchar(max),     	  ---调整原因
	ChangeUser   nvarchar(8),       	  ---调整人员
	Note        nvarchar(max),      	  ---备注 
	FlowState   int                 	  ---流程状态
)
GO


--投标状态调整表 添加字段  2013-7-30  14:00   gmd
ALTER TABLE PT_PrjInfo_StateChange ADD InputDate datetime         --录入时间   
GO
ALTER TABLE PT_PrjInfo_StateChange ADD InputUserCode varchar(8)   --录入人员
GO
-- 投标中 添加状态变更流程字段  2013-7-30 17:15 gmd
ALTER TABLE PT_PrjInfo_ZTB ADD ChangeFlowSate int               --状态变更的流程状态
GO


--删除命名错误的表      Bery    2013-07-31 09:06
IF OBJECT_ID('Role_Role') IS NOT NULL
    DROP TABLE Role_Role
GO

--角色表                Bery    2013-07-31 09:21
CREATE TABLE Priv_Role
(
    Id nvarchar(200) PRIMARY KEY,
    Name nvarchar(200) NOT NULL
)
GO

--用户角色              Bery    2013-07-31 09:30
CREATE TABLE Priv_UserRole
(
    Id nvarchar(200) PRIMARY KEY,
    UserCode varchar(8),
    RoleId nvarchar(200) REFERENCES Priv_Role(Id) ON DELETE CASCADE
)
GO

--角色权限资源          Bery    2013-07-31 09:34
CREATE TABLE Priv_Resource
(
    Id nvarchar(200) PRIMARY KEY,                                       --主键
    RoleId nvarchar(200) REFERENCES Priv_Role(Id) ON DELETE CASCADE,    --角色Id
    TableName nvarchar(200),                                            --表名
    ResKey nvarchar(200)                                                --资源Id
)
GO

--角色添加排序号字段    Bery    2013-07-31 10:05
ALTER TABLE Priv_Role ADD No int
GO

--修改投标项目状态变更字段录入人    dhw    2013-07-31 13:30
EXEC sp_rename 'PT_PrjInfo_StateChange.[InputUserCode]', 'InputUser', 'column'
GO

--报名管理流程模板                 gmd      2013-8-1 13:15
INSERT INTO WF_BusinessCode VALUES(
135,'报名管理审核','PrjGuid','PT_PrjInfo_ZTB','PrjGuid','InitiateFlowState','/TenderManage/InfoView.aspx',NULL,70,'PrjGuid','PrjGuid'
)
INSERT INTO WF_Business_Class VALUES(135,'001','报名管理审核',NEWID())

GO
--状态变更流程模板               gmd        2013-8-1  13:15
INSERT INTO WF_BusinessCode VALUES(
140,'立项状态变更审核','PrjGuid','PT_PrjInfo_ZTB','PrjGuid','ChangeFlowSate','/TenderManage/StateChangeQuery.aspx',NULL,70,'PrjGuid','PrjGuid'
)
INSERT INTO WF_Business_Class VALUES(140,'001','立项状态变更审核',NEWID())

GO

--在Basic_CodeList 中插入一条项目状态数据     gmd    13:20
INSERT INTO Basic_CodeList VALUES('ProjectState',18,'放弃')
GO

--资格预审审核流程          gmd              2013-8-1  13:30
INSERT INTO WF_BusinessCode VALUES(136,'资格预审审核','PrjGuid','PT_PrjInfo_ZTB','PrjGuid','PftFlowState','/TenderManage/InfoView.aspx',NULL,70,'PrjGuid','PrjGuid')
INSERT INTO WF_Business_Class VALUES(136,'001','资格预审审核',NEWID())
GO


--删除命名错误的表          Bery    2013-08-02 09:07
IF OBJECT_ID('Priv_Resource') IS NOT NULL
	DROP TABLE Priv_Resource
GO

--修改数据类型              Bery    2013-08-02 14:08
ALTER TABLE PT_PrjInfo_ZTB_User ALTER COLUMN PrjGuid nvarchar(200) NOT NULL

--增加固定资产采购内容		lhy    2013-08-02   14:14
--采购单添加采购类型  0 采购单 1 代表固定资产采购 2 代表 办公用品采购
ALTER TABLE Sm_Purchase ADD PurchaseType int DEFAULT(0) NOT NULL 
GO
--修改模块表中采购单的采购及添加新的采购单和固定资产采购单
UPDATE PT_MK SET V_MKMC='采购单管理',V_CDLJ='',I_ChildNum=2 
WHERE C_MKDM='0305'
GO
-- 添加物新的采购单菜单
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '030501')
	INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
        VALUES('030501','采购单','StockManage/basicset/SmWantPlanFrame.aspx?path=purchase','y',1,'',24918,0,'0','0','','1')
GO
--修改桌面采购单菜单ID的关联
UPDATE frame_Desktop_Menulink SET ModelId='030501'
WHERE ModelId='0305'

--增加固定资产采购单审核
INSERT INTO WF_BusinessCode 
VALUES('141','固定资产采购单审核','pid','Sm_Purchase','pid','flowState',
'/StockManage/Purchase/FixedAssets/EquipmentPurchaseView.aspx',NULL,'03',NULL,'pcode')
INSERT INTO WF_Business_Class
VALUES('141','001','固定资产采购单审核',NEWID())

--中标管理审核流程              gmd             2013-8-2   14:15
INSERT INTO WF_BusinessCode VALUES(137,'中标管理审核','PrjGuid','PT_PrjInfo_ZTB','PrjGuid','BidFlowState','/TenderManage/InfoView.aspx',NULL,70,'PrjGuid','PrjGuid')
INSERT INTO WF_Business_Class VALUES (137,'001','中标管理审核',NEWID())
-- 修改投标状态变更原来是‘PrjGuid’      dhw  2013-8-2  16:12
UPDATE WF_BusinessCode SET NameField='PrjName' 
WHERE BusinessCode=140
GO
--上级父类项目编码                        gmd   2013-8-3 13:40
ALTER TABLE PT_PrjInfo_ZTB  ADD ParentTypeCode varchar(24)
GO 


--业务数据角色表        Bery    2013-08-03 14:00
CREATE TABLE Priv_BusiDataRole
(
	Id nvarchar(200) PRIMARY KEY,                                       --主键
    RoleId nvarchar(200) REFERENCES Priv_Role(Id) ON DELETE CASCADE,    --角色Id
    TableName nvarchar(200),                                            --表名
    BusiDataId nvarchar(200)                                            --业务数据Id	
)
GO

--修改投标信息表OldState（放弃状态）   dhw 2013-08-05 09:39
ALTER TABLE PT_PrjInfo_ZTB
	ADD CONSTRAINT DF_OldState DEFAULT 1 FOR OldState
GO

--把模块人员修改为模块权限      Bery    2013-08-05 16:03
UPDATE PT_MK SET V_MKMC = '模块权限' WHERE C_MKDM = '3806'
GO

--修改报名、资格预审、中标管理审核流程的NameField值  dhw   2013-08-06 14:13
UPDATE WF_BusinessCode SET NameField='PrjName' WHERE BusinessCode in('135','136','137')
GO

--添加放弃管理                 gmd                   2013-8-7     18:25
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
VALUES('7009','放弃管理','','y',7,'',24926,1,'0','0','','1')
GO
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
 VALUES('700901','放弃管理','/TenderManage/GiveUpList.aspx','y',1,'',24927,0,'0','0','','1')
 GO
 
--修改项目报名菜单             gmd          2013-08-08 9:10
UPDATE PT_MK SET V_MKMC='项目报名' WHERE C_MKDM='7012'
UPDATE PT_MK SET V_MKMC='报名管理' WHERE C_MKDM='701201'
UPDATE PT_MK SET V_MKMC='报名项目一览' WHERE C_MKDM='701202'
GO
--修改项目状态，把启动改为报名         gmd    2013-08-08     9:20
UPDATE Basic_CodeList SET ItemName='报名' WHERE TypeCode='ProjectState' AND ItemCode=3

-- 新的数据库增加了一些字段，对老的数据库中新添加的流程审核字段进行处理  dhw   2013-08-09 11:51
-- 1:项目预立项 2:项目立项 3:报名 14:资格预审 15:预审通过 16:预审失败  4:投标 5:中标 6:落标  18：放弃 
-- 报名和资格预审时报名审核已通过
UPDATE PT_PrjInfo_ZTB  SET initiateFlowState='1' WHERE PrjState in('3','14')
-- 预审通过和投标时资格审核已通过
UPDATE PT_PrjInfo_ZTB  SET initiateFlowState='1',PftFlowState='1' WHERE PrjState in('15','4')
-- 预审失败状态是在资格审核驳回状态
UPDATE PT_PrjInfo_ZTB   SET initiateFlowState='1',PftFlowState='-2'  WHERE PrjState in('16')
-- 投标状态是中标流程审核已通过
UPDATE PT_PrjInfo_ZTB  SET initiateFlowState='1',PftFlowState='1',BidFlowState='1'  WHERE PrjState in('5')
-- 落标为中标流程审核驳回的项目
UPDATE PT_PrjInfo_ZTB   SET initiateFlowState='1',PftFlowState='1',BidFlowState='-2' WHERE PrjState in('6')
GO

--更新新角色的菜单      Bery    2013-08-09 13:29
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
    VALUES('3812','角色管理','/Priv/RoleList.aspx','y',16,'',24917,0,'0','0','','1')
GO
UPDATE PT_Yhmc_Privilege SET C_MKDM = '3812' WHERE C_MKDM = '3815'
GO

-- 修改投标信息表中状态变更默认值为为提交         dhw    2013-08-09 14:50
ALTER TABLE PT_PrjInfo_ZTB 
	ADD CONSTRAINT DF_ChangeFlowState DEFAULT -1 FOR ChangeFlowSate
GO

-- 修改投标信息表中状态变更字段为空的数据         dhw    2013-08-09 15:01
UPDATE PT_PrjInfo_ZTB SET ChangeFlowSate=-1 WHERE ChangeFlowSate is null
GO

--删除菜单      Bery    2013-08-11 11:24
DELETE FROM PT_Yhmc_Privilege WHERE C_MKDM = '701702'
GO

--合同参数设置表       GMD  2013-8-14 8:40
IF OBJECT_ID('Con_Config_Contract','U') IS NOT NULL
	DROP TABLE Con_Config_Contract
GO
CREATE TABLE Con_Config_Contract(
    Id  nvarchar(200) PRIMARY KEY ,              	--主键
    ContractId   nvarchar(200) NOT NULL,            --合同Id
	PayoutAlarmDays  int ,                       	--付款提醒天数
    IncomeAlarmDays  int ,                       	--回款提醒天数
    HighPayAlarmLimit  decimal(18,3),            	--高支付提醒限额 
	HighBalanceAlarmLimit  decimal(18,3),        	--高结算提醒限额
	MidPayAlarmUpperLimit  decimal(18,3),        	--中支付提醒金额上限比例 
	MidPayAlarmLowerLimit  decimal(18,3),        	--中支付提醒金额下限比例
	MidBalanceAlarmUpperLimit  decimal(18,3),    	--中结算提醒金额上限比例
	MidBalanceAlarmLowerLimit  decimal(18,3),    	--中结算提醒金额下限比例
	LowPayAlarmUpperLimit  decimal(18,3),        	--低支付提醒金额上限比例
	LowPayAlarmLowerLimit  decimal(18,3),        	--低支付提醒金额下限比例
	LowBalanceAlarmUpperLimit decimal(18,3),     	--低结算提醒金额上限比例
	LowBalanceAlarmLowerLimit decimal(18,3),     	--低结算提醒金额下限比例
	IsPayoutAlarm  bit DEFAULT(0) NOT NULL,         --是否付款提醒 
	IsPaymentAlarm  bit DEFAULT(0) NOT NULL ,       --是否支付提醒
	IsIncomeAlarm  bit DEFAULT(0) NOT NULL ,        --是否回款提醒
	IsBalanceAlarm  bit DEFAULT(0) NOT NULL,        --是否结算提醒
)
GO
--合同类型表 添加类型简写字段   GMD  2013-8-14 8:40
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'Con_ContractType' AND column_name = 'TypeShort')
ALTER TABLE Con_ContractType ADD TypeShort nvarchar(100)   --添加合同类型简写
GO

--删除触发器                    Bery    2013-08-15 08:12
IF OBJECT_ID('Basic_CodeType_AspNet_SqlCacheNotification_Trigger') IS NOT NULL
	DROP TRIGGER Basic_CodeType_AspNet_SqlCacheNotification_Trigger
GO

--更换Basic_CodeList的主键      Bery    2013-08-15 10:49
ALTER TABLE Basic_CodeList DROP CONSTRAINT PK__Basic_CodeList__36B2B8F1
GO
ALTER TABLE Basic_CodeList DROP CONSTRAINT FK__Basic_Cod__TypeC__37A6DD2A
GO
ALTER TABLE Basic_CodeList ADD Id nvarchar(200) DEFAULT(NEWID()) PRIMARY KEY
GO

--设备状态保存在Basic_Cod中
INSERT INTO Basic_CodeType(TypeCode, TypeName) VALUES('EquState', '设备状态')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName, Id) VALUES('EquState', 1, '正常运行', NEWID())
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName, Id) VALUES('EquState', 3, '保养维修', NEWID())
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName, Id) VALUES('EquState', 11, '报废', NEWID())
GO


--修改外键，处理项目删除失败问题	Bery		2013-8-16 16:20:48
IF OBJECT_ID('FK__Con_Balan__Purch__1E7C0804') IS NOT NULL
	ALTER TABLE Con_Balance_Stock DROP CONSTRAINT FK__Con_Balan__Purch__1E7C0804
GO

--修改外键，处理项目删除失败问题	Bery        2013-08-20 13:46
IF OBJECT_ID('FK__Con_Balan__Purch__4D21E854') IS NOT NULL
	ALTER TABLE Con_Balance_Stock DROP CONSTRAINT FK__Con_Balan__Purch__4D21E854
GO

--修改项目资金账户项目ID字段长度，处理在项目账户输入过多时报错问题 dhw  2013-08-20 15:37
ALTER TABLE Fund_Prj_Accoun ALTER COLUMN PrjGuid nvarchar(MAX) NOT NULL
GO

--收入合同发票表 添加字段 组织机构代码证件号                      gmd   2013-08-21 14:20 
IF NOT EXISTS(SELECT * FROM information_schema.columns
    WHERE table_name='Con_Incomet_Invoice' AND column_name='OrganizationCode' )
ALTER TABLE Con_Incomet_Invoice ADD OrganizationCode nvarchar(200)  
GO
--支出合同发票表 添加字段 组织机构代码证件号                     gmd    2013-08-21 14:20
IF NOT EXISTS(SELECT * FROM information_schema.columns
   WHERE table_name='Con_Payout_Invoice' AND column_name='OrganizationCode' )
ALTER TABLE Con_Payout_Invoice ADD OrganizationCode nvarchar(200)   
GO


--添加是否放弃      Bery    2013-08-26 13:57
ALTER TABLE PT_PrjInfo_ZTB ADD IsGiveUp bit DEFAULT(0) NOT NULL
GO

--添加合同预算产值上报中级联删除功能		lhy	  2013-08-27   10:00
IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('FK_RptId') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE Bud_ContractConsTask DROP CONSTRAINT FK_RptId
GO
ALTER TABLE Bud_ContractConsTask ADD CONSTRAINT FK_RptId 
	FOREIGN KEY (RptId) REFERENCES Bud_ContractConsReport (RptId)
	ON DELETE CASCADE
GO

--修改物资需求计划页面，更换项目树          Bery    2013-08-27 13:35
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=wantPlan&prjId='
WHERE C_MKDM = '0303'
GO

--处理没有项目小组成员审核信息的项目        Bery    2013-08-27 17:08
INSERT INTO PT_PrjMemberWF(PrjGuid, FlowState)
SELECT PrjGuid, -1 FROM PT_PrjInfo
WHERE PrjGuid NOT IN (
	SELECT PrjGuid FROM PT_PrjMemberWF
)
GO


-- 添加放弃管理菜单    dhw 2013-08-28 16:06
IF  EXISTS(SELECT * FROM PT_MK  WHERE C_MKDM='700902')
DELETE FROM PT_MK WHERE C_MKDM='700902'
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
    VALUES('700902','放弃管理','/TenderManage/EnableList.aspx','y',2,'',24954,0,'0','0','','1');
GO

--增加放弃审核   dhw 2013-08-28 16:06
INSERT INTO WF_BusinessCode 
VALUES('138','放弃管理审核','PrjGuid','PT_PrjInfo_ZTB','PrjGuid','GiveUpFlowState','/TenderManage/InfoView.aspx',NULL,'70','PrjGuid','PrjName');

INSERT INTO WF_Business_Class
VALUES('138','001','放弃管理审核',NEWID());
GO

-- 修改菜单名称为放弃管理为启用管理    dhw 2013-08-28 16:07
IF EXISTS(SELECT * FROM PT_MK WHERE C_MKDM='700901')
   UPDATE PT_MK SET V_MKMC='启用管理',I_XH=3 WHERE C_MKDM='700901'
GO


--修改物资采购计划地址，更换项目树    Bery  2013-08-29 11:13
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=purchasePlan' WHERE C_MKDM = '0304'
GO
--修改采购单地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=purchase' WHERE C_MKDM = '030501'
GO
--修改发货通知单地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=sendNoteList' WHERE C_MKDM = '031001'
GO
--修改收货验收单地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=receiveNoteList' WHERE C_MKDM = '031005'
GO
--修改收货单管理地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=reciveEditList' WHERE C_MKDM = '031006'
GO
--修改作废收货单地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=delReceiveNote' WHERE C_MKDM = '031007'
GO
--修改出库管理地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=outReserve' WHERE C_MKDM = '031101'
GO
--修改确认出库地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qOutReserve' WHERE C_MKDM = '031102'
GO
--修改退库管理地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=refundingList' WHERE C_MKDM = '031301'
GO
--修改确认退库地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qoRefundingList' WHERE C_MKDM = '031302'
GO
--修改图纸计划地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?businessType=ImgManage&opType=edit' WHERE C_MKDM = '2702'
GO
--修改图纸会审地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?businessType=ImgMag&opType=img' WHERE C_MKDM = '2703'
GO
--修改图纸进度一览地址，更换项目树    
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?businessType=ImgManageView&opType=view' WHERE C_MKDM = '2704'
GO

--修改图纸计划和图纸会审审核流程可以查看到相应的审核信息   dhw  2013-09-03 13:00
UPDATE WF_BusinessCode SET ProjectField='UID',NameField='BName' WHERE  BusinessCode='821'
GO
UPDATE WF_BusinessCode SET ProjectField='UID',NameField='PName' WHERE  BusinessCode='826'
GO

--修改合同预算导入地址，更换项目树  Bery    2013-09-03 14:45
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=contractTask' WHERE C_MKDM = '130701'
GO
--合同预算导入          Bery    2013-09-03 14:45
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=contractTask' WHERE C_MKDM = '130701'
GO
--合同预算清单          Bery    2013-09-03 14:45
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=contractTaskList' WHERE C_MKDM = '130703'
GO
--合同预算查询          Bery    2013-09-03 14:45
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=contractTaskQuery' WHERE C_MKDM = '130702'
GO
--产值上报              Bery    2013-09-03 14:45
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=contractTaskRpt' WHERE C_MKDM = '130704'
GO
--合同预算变更          Bery    2013-09-03 14:46
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=contractTaskModify' WHERE C_MKDM = '130705'
GO
--目标成本编制          Bery    2013-09-03 15:29
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=budgetPlait' WHERE C_MKDM = '130101'
GO
--预算变更              Bery    2013-09-03 15:29
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=budgetModify' WHERE C_MKDM = '130103'
GO
--资源配置              Bery    2013-09-03 15:29
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=resDeploy' WHERE C_MKDM = '130104'
GO
--资源映射              Bery    2013-09-03 15:30
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=resMapping' WHERE C_MKDM = '130105'
GO
--目标成本查询          Bery    2013-09-03 15:30
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=budgetQuery' WHERE C_MKDM = '130109'
GO

--计划管理 更换项目树   Bery    2013-09-04 14:32
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planPlait' WHERE C_MKDM = '780101'
GO
--计划审批
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planAduit' WHERE C_MKDM = '780102'
GO
--调整申请
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planModifyApply' WHERE C_MKDM = '780201'
GO
--调整审批
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planModifyAudit' WHERE C_MKDM = '780202'
GO
--进度版本查询
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planVersionQuery' WHERE C_MKDM = '780203'
GO
--实际进度上报
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planActualRpt' WHERE C_MKDM = '780303'
GO
--进度预警
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planWarn' WHERE C_MKDM = '780302'
GO
--月度报告
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planMonth' WHERE C_MKDM = '780401'
GO
--总体进度
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planTrack' WHERE C_MKDM = '780402'
GO
--里程碑
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planMilestone' WHERE C_MKDM = '780403'
GO
--进度柱状图明细
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planChartDetail' WHERE C_MKDM = '780502'
GO
--进度计划权限
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=planPriv' WHERE C_MKDM = '780601'
GO

--  人员表增加离职日期字段			lhy		2013-09-05
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'PT_yhmc' AND column_name = 'LeaveDate')
	ALTER TABLE PT_yhmc ADD LeaveDate DateTime
GO


--更新外键，添加级联删除，解决项目删除失败问题          Bery    2013-09-05 13:43
DECLARE @tableName nvarchar(100)		--表名
DECLARE @columnName nvarchar(100)		--字段名
SET @tableName = 'Bud_ContractConsTask' 
SET @columnName = 'TaskId'
DECLARE @cname nvarchar(100)
DECLARE _cursor CURSOR
FOR 
SELECT 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
FROM information_schema.CONSTRAINT_COLUMN_USAGE
WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName
OPEN _cursor
FETCH NEXT FROM _cursor
INTO @cname
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC(@cname)		--执行删除语句
	FETCH NEXT FROM _cursor INTO @cname
END
CLOSE _cursor
DEALLOCATE _cursor
GO
ALTER TABLE Bud_ContractConsTask ADD CONSTRAINT FK_Bud_ContraintConstTask_TaskId
FOREIGN KEY(TaskId) REFERENCES Bud_ContractTask(TaskId) ON DELETE CASCADE
GO


--删除错误数据          Bery    2013-09-10 12:24
DELETE FROM PT_PrjInfo_Rank
WHERE PrjGuid NOT IN
(
	SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail
)
GO


--更新外键，添加级联删除，解决项目删除失败问题        Bery      2013-09-10 16:34
DECLARE @tableName nvarchar(100)		--表名
DECLARE @columnName nvarchar(100)		--字段名
SET @tableName = 'Con_Balance_Stock' 
SET @columnName = 'PurchaseId'
DECLARE @cname nvarchar(100)
DECLARE _cursor CURSOR
FOR 
SELECT 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
FROM information_schema.CONSTRAINT_COLUMN_USAGE
WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName
OPEN _cursor
FETCH NEXT FROM _cursor
INTO @cname
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC(@cname)		--执行删除语句
	FETCH NEXT FROM _cursor INTO @cname
END
CLOSE _cursor
DEALLOCATE _cursor
GO
ALTER TABLE Con_Balance_Stock ADD CONSTRAINT FK_Con_Balance_Stock_PurchaseId
FOREIGN KEY(PurchaseId) REFERENCES Sm_Purchase_Stock(psid) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_SM_WANTP_STOCK') IS NOT NULL
	ALTER TABLE Sm_Wantplan_Stock DROP CONSTRAINT FK_SM_WANTP_STOCK
GO
ALTER TABLE Sm_Wantplan_Stock ADD CONSTRAINT FK_SM_WANTP_STOCK
FOREIGN KEY(wpcode) REFERENCES Sm_Wantplan(swcode) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_SM_PURCH_STOCK') IS NOT NULL
	ALTER TABLE Sm_Purchaseplan_Stock DROP CONSTRAINT FK_SM_PURCH_STOCK
GO
ALTER TABLE Sm_Purchaseplan_Stock ADD CONSTRAINT FK_SM_PURCH_STOCK
FOREIGN KEY(ppcode) REFERENCES Sm_Purchaseplan(ppcode) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_SM_PURCHASE') IS NOT NULL
	ALTER TABLE Sm_Purchase_Stock DROP CONSTRAINT FK_SM_PURCHASE
GO
ALTER TABLE Sm_Purchase_Stock ADD CONSTRAINT FK_SM_PURCHASE
FOREIGN KEY(pscode) REFERENCES Sm_Purchase(pcode) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_SM_STORAGE') IS NOT NULL
	ALTER TABLE Sm_Storage_Stock DROP CONSTRAINT FK_SM_STORAGE
GO
ALTER TABLE Sm_Storage_Stock ADD CONSTRAINT FK_SM_STORAGE
FOREIGN KEY(stcode) REFERENCES Sm_Storage(scode) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_SM_OUTRE') IS NOT NULL
	ALTER TABLE Sm_out_Stock DROP CONSTRAINT FK_SM_OUTRE
GO
ALTER TABLE Sm_out_Stock ADD CONSTRAINT FK_SM_OUTRE
FOREIGN KEY(orcode) REFERENCES Sm_OutReserve(orcode) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_SM_REFUN') IS NOT NULL
	ALTER TABLE Sm_back_Stock DROP CONSTRAINT FK_SM_REFUN
GO
ALTER TABLE Sm_back_Stock ADD CONSTRAINT FK_SM_REFUN
FOREIGN KEY(rcode) REFERENCES Sm_Refunding(rcode) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_PAYOUT_REFERENCE_PAYMENT') IS NOT NULL
	ALTER TABLE Con_Payout_Payment DROP CONSTRAINT FK_CON_PAYOUT_REFERENCE_PAYMENT
GO
ALTER TABLE Con_Payout_Payment ADD CONSTRAINT FK_CON_PAYOUT_REFERENCE_PAYMENT
FOREIGN KEY(ContractID) REFERENCES Con_Payout_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_PAYOUT_REFERENCE_MODIFY') IS NOT NULL
	ALTER TABLE Con_Payout_Modify DROP CONSTRAINT FK_CON_PAYOUT_REFERENCE_MODIFY
GO
ALTER TABLE Con_Payout_Modify ADD CONSTRAINT FK_CON_PAYOUT_REFERENCE_MODIFY
FOREIGN KEY(ContractID) REFERENCES Con_Payout_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_PAYOUT_REFERENCE_BALANCE') IS NOT NULL
	ALTER TABLE Con_Payout_Balance DROP CONSTRAINT FK_CON_PAYOUT_REFERENCE_BALANCE
GO
ALTER TABLE Con_Payout_Balance ADD CONSTRAINT FK_CON_PAYOUT_REFERENCE_BALANCE
FOREIGN KEY(ContractID) REFERENCES Con_Payout_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_BALANCE') IS NOT NULL
	ALTER TABLE Con_Incomet_Balance DROP CONSTRAINT FK_CON_INCOME_REFERENCE_BALANCE
GO
ALTER TABLE Con_Incomet_Balance ADD CONSTRAINT FK_CON_INCOME_REFERENCE_BALANCE
FOREIGN KEY(ContractID) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_INCO_REFERENCE_CON_INCO') IS NOT NULL
	ALTER TABLE Con_Incomet_Invoice DROP CONSTRAINT FK_CON_INCO_REFERENCE_CON_INCO
GO
ALTER TABLE Con_Incomet_Invoice ADD CONSTRAINT FK_CON_INCO_REFERENCE_CON_INCO
FOREIGN KEY(ContractID) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_MODIFY') IS NOT NULL
	ALTER TABLE Con_Incomet_Modify DROP CONSTRAINT FK_CON_INCOME_REFERENCE_MODIFY
GO
ALTER TABLE Con_Incomet_Modify ADD CONSTRAINT FK_CON_INCOME_REFERENCE_MODIFY
FOREIGN KEY(ContractID) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_PAYMENT') IS NOT NULL
	ALTER TABLE Con_Incomet_Payment DROP CONSTRAINT FK_CON_INCOME_REFERENCE_PAYMENT
GO
ALTER TABLE Con_Incomet_Payment ADD CONSTRAINT FK_CON_INCOME_REFERENCE_PAYMENT
FOREIGN KEY(ContractID) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_PAYEND') IS NOT NULL
	ALTER TABLE Con_ContractPayend DROP CONSTRAINT FK_CON_INCOME_REFERENCE_PAYEND
GO
ALTER TABLE Con_ContractPayend ADD CONSTRAINT FK_CON_INCOME_REFERENCE_PAYEND
FOREIGN KEY(ContractID) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_PAYEND_REFERENCE_FEEDBACK') IS NOT NULL
	ALTER TABLE Con_PayendFeedback DROP CONSTRAINT FK_CON_PAYEND_REFERENCE_FEEDBACK
GO
ALTER TABLE Con_PayendFeedback ADD CONSTRAINT FK_CON_PAYEND_REFERENCE_FEEDBACK
FOREIGN KEY(ContractID) REFERENCES Con_Incomet_Contract(ContractID) ON DELETE CASCADE
GO

--2013-09-10 16:34
IF OBJECT_ID('FK_CON_PAYO_REFERENCE_CON_PAYO') IS NOT NULL
	ALTER TABLE Con_Payout_Invoice DROP CONSTRAINT FK_CON_PAYO_REFERENCE_CON_PAYO
GO
ALTER TABLE Con_Payout_Invoice ADD CONSTRAINT FK_CON_PAYO_REFERENCE_CON_PAYO
FOREIGN KEY(ContractID) REFERENCES Con_Payout_Contract(ContractID) ON DELETE CASCADE
GO


--更新外键，添加级联删除，解决项目删除失败问题          2013-09-10 16:34 
DECLARE @tableName nvarchar(100)		--表名
DECLARE @columnName nvarchar(100)		--字段名
SET @tableName = 'Bud_TaskResource' 
SET @columnName = 'TaskId'
DECLARE @cname nvarchar(100)
DECLARE _cursor CURSOR
FOR 
SELECT 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
FROM information_schema.CONSTRAINT_COLUMN_USAGE
WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName
OPEN _cursor
FETCH NEXT FROM _cursor
INTO @cname
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC(@cname)		--执行删除语句
	FETCH NEXT FROM _cursor INTO @cname
END
CLOSE _cursor
DEALLOCATE _cursor
GO
ALTER TABLE Bud_TaskResource ADD CONSTRAINT FK_Bud_TaskResource_TaskId
FOREIGN KEY(TaskId) REFERENCES Bud_Task(TaskId) ON DELETE CASCADE
GO


--更新外键，添加级联删除，解决项目删除失败问题              2013-09-10 16:34    
DECLARE @tableName nvarchar(100)		--表名
DECLARE @columnName nvarchar(100)		--字段名
SET @tableName = 'Bud_IndirectMonthBudget' 
SET @columnName = 'IndirectBudget'
DECLARE @cname nvarchar(100)
DECLARE _cursor CURSOR
FOR 
SELECT 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
FROM information_schema.CONSTRAINT_COLUMN_USAGE
WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName
OPEN _cursor
FETCH NEXT FROM _cursor
INTO @cname
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC(@cname)		--执行删除语句
	FETCH NEXT FROM _cursor INTO @cname
END
CLOSE _cursor
DEALLOCATE _cursor
GO
ALTER TABLE Bud_IndirectMonthBudget ADD CONSTRAINT FK_Bud_IndirectMonthBudget_IndirectBudget
FOREIGN KEY(IndirectBudget) REFERENCES Bud_IndirectBudget(Id) ON DELETE CASCADE
GO


--更新外键，添加级联删除，解决项目删除失败问题      2013-09-10 16:34  
DECLARE @tableName nvarchar(100)		--表名
DECLARE @columnName nvarchar(100)		--字段名
SET @tableName = 'Con_Payout_Target' 
SET @columnName = 'ContractId'
DECLARE @cname nvarchar(100)
DECLARE _cursor CURSOR
FOR 
SELECT 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
FROM information_schema.CONSTRAINT_COLUMN_USAGE
WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName
OPEN _cursor
FETCH NEXT FROM _cursor
INTO @cname
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC(@cname)		--执行删除语句
	FETCH NEXT FROM _cursor INTO @cname
END
CLOSE _cursor
DEALLOCATE _cursor
GO
ALTER TABLE Con_Payout_Target ADD CONSTRAINT FK_Con_Payout_Target_ContractId
FOREIGN KEY(ContractId) REFERENCES Con_Payout_Contract(ContractID) ON DELETE CASCADE
GO


--更新外键，添加级联删除，解决项目删除失败问题      2013-09-10 16:35    
DECLARE @tableName nvarchar(100)		--表名
DECLARE @columnName nvarchar(100)		--字段名
SET @tableName = 'PT_PrjInfo_Rank' 
SET @columnName = 'PrjGuid'
DECLARE @cname nvarchar(100)
DECLARE _cursor CURSOR
FOR 
SELECT 'ALTER TABLE ' + @tableName + ' DROP CONSTRAINT ' + CONSTRAINT_NAME 
FROM information_schema.CONSTRAINT_COLUMN_USAGE
WHERE TABLE_NAME = @tableName AND COLUMN_NAME = @columnName
OPEN _cursor
FETCH NEXT FROM _cursor
INTO @cname
WHILE @@FETCH_STATUS = 0
BEGIN
	EXEC(@cname)		--执行删除语句
	FETCH NEXT FROM _cursor INTO @cname
END
CLOSE _cursor
DEALLOCATE _cursor
GO
ALTER TABLE PT_PrjInfo_Rank ADD CONSTRAINT FK_PT_PrjInfo_Rank_PrjGuid
FOREIGN KEY(PrjGuid) REFERENCES PT_PrjInfo_ZTB_Detail(PrjGuid) ON DELETE CASCADE
GO


--更换开停复工项目树        Bery        2013-09-11 16:35
--停工管理
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=stopMsg' WHERE C_MKDM = '7202'
GO
--复工管理
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=retMsg' WHERE C_MKDM = '7203'
GO
--查看报告
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=queryRpt' WHERE C_MKDM = '7204'
GO


--更换项目小组成员分布项目树        Bery    2013-09-12 10:45
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=prjMemberQuery' WHERE C_MKDM = '741302'
GO
--月入计划管理                      Bery    2013-09-12 11:27
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=fundPlanIncome' WHERE C_MKDM = '310105'
GO
--月支计划管理                      Bery    2013-09-12 11:27
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=fundPlanPayout' WHERE C_MKDM = '310101'
GO
--间接成本预算
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=indirectBudget' WHERE C_MKDM = '290301'
GO
--间接成本月度预算
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=indirectMonthBudget' WHERE C_MKDM = '290305'
GO
--费用录入
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=costDiary' WHERE C_MKDM = '290401'
GO
--费用查询（财务）
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=costDiaryQuery' WHERE C_MKDM = '290402'
GO
--费用查询（个人）
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=costDiaryQueryOneself' WHERE C_MKDM = '290404'
GO
--间接费用分析
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=indirectCostAnalysis' WHERE C_MKDM = '290403'
GO
--施工报量
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=constructReport' WHERE C_MKDM = '290501'
GO
--施工报量查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=checkConstructReport' WHERE C_MKDM = '290504'
GO
--施工报量查询
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=checkConstructRes' WHERE C_MKDM = '290506'
GO


--数字辅助表        Bery    2013-09-16 09:44
IF OBJECT_ID('dbo.Nums') IS NOT NULL
  DROP TABLE dbo.Nums;
GO
CREATE TABLE dbo.Nums(n INT NOT NULL PRIMARY KEY);
DECLARE @max AS INT, @rc AS INT;
SET @max = 10000;
SET @rc = 1;
INSERT INTO Nums VALUES(1);
WHILE @rc * 2 <= @max
BEGIN
  INSERT INTO dbo.Nums SELECT n + @rc FROM dbo.Nums;
  SET @rc = @rc * 2;
END
INSERT INTO dbo.Nums 
  SELECT n + @rc FROM dbo.Nums WHERE n + @rc <= @max;
GO

--修改和添加项目状态    Bery    2013-09-16 09:51
UPDATE Basic_CodeList SET ItemName = '报名通过' WHERE TypeCode = 'ProjectState' AND ItemCode = '3'
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName) VALUES('ProjectState', 19, '报名不通过')
GO

--Basic_CodeList添加唯一约束    Bery    2013-09-17 09:16
ALTER TABLE Basic_CodeList ADD CONSTRAINT UQ_Basic_CodeList_TypeCode_ItemCode UNIQUE(TypeCode,ItemCode)
GO


--成本报表修改项目树    Bery    2013-09-24 11:07
--三算分析明细
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptBudgetDetail' WHERE C_MKDM = '290705'
GO
--盈亏分析明细
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptEvenAnalysisDatail' WHERE C_MKDM = '290708'
GO
--目标成本执行情况明细
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptCostRptDetail' WHERE C_MKDM = '290709'
GO
--分项工程成本对比表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptSubPrjCmp' WHERE C_MKDM = '290711'
GO
--分项工程成本-工程量差异分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptSubPrjDffCmp' WHERE C_MKDM = '290721'
GO
--机械费分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptJixieAnalysis' WHERE C_MKDM = '290710'
GO
--机械费差异分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptJixieDetail' WHERE C_MKDM = '290712'
GO
--人工分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptLaborAnalysis' WHERE C_MKDM = '290713'
GO
--主材分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptMajorStuffAnalysis' WHERE C_MKDM = '290717'
GO
--主材差异分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptMajorStuffDiffAnalysis' WHERE C_MKDM = '290718'
GO
--辅材分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptMinorStuffAnalysis' WHERE C_MKDM = '290715'
GO
--项目成本计划分项分类表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptCBSCost' WHERE C_MKDM = '290716'
GO
--人工费总分析表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptLaborCostAnalysis' WHERE C_MKDM = '290704'
GO
--人工明细表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptLaborDetailAnalysis' WHERE C_MKDM = '290702'
GO
--材料明细表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptStuffDetailAnalysis' WHERE C_MKDM = '290720'
GO
--机械明细表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptMachineDetailAnalysis' WHERE C_MKDM = '290701'
GO
--人才机明细
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptLSMDetailAnalysis' WHERE C_MKDM = '290722'
GO
--工程汇总表
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=rptPrjSummary' WHERE C_MKDM = '2908'
GO


--质量管理项目树        Bery    2013-09-24 16:18
--事务维护
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsAffairEdit' WHERE C_MKDM = '930603'
GO
--事务查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsAffairList' WHERE C_MKDM = '930606'
GO
--目标维护
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsGoalEdit' WHERE C_MKDM = '930903'
GO
--目标查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsGoalList' WHERE C_MKDM = '930906'
GO
--质量台帐
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsFileUpload' WHERE C_MKDM = '931206'
GO
--质量事故记录
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjSuperviseEditCA2' WHERE C_MKDM = '935001'
GO
--事故查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjSuperviseListCA2' WHERE C_MKDM = '935002'
GO
--质量检查记录
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjSuperviseEditCA1' WHERE C_MKDM = '936001'
GO
--质量检测资料查询
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjSuperviseListCA1' WHERE C_MKDM = '936002'
GO
--工程竣工质量验收资料管理
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjSuperviseEditCA3' WHERE C_MKDM = '937001'
GO
--工程竣工质量验收记录管理
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjSuperviseListCA3' WHERE C_MKDM = '937002'
GO
--亮点维护
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsHighlightsEditTypeID8' WHERE C_MKDM = '930801'
GO
--亮点查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsHighLightsListTypeID8' WHERE C_MKDM = '930802'
GO
--活动维护
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsHighlightsEditTypeID9' WHERE C_MKDM = '930701'
GO
--活动查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsHighLightsListTypeID9' WHERE C_MKDM = '930702'
GO
--质量问题管理
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjFrameEdit' WHERE C_MKDM = '931002'
GO
--质量问题一览
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsPrjFrameView' WHERE C_MKDM = '931003'
GO


--安全管理项目树    Bery    2013-09-24 16:54
--事务维护
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssAffairEdit' WHERE C_MKDM = '940603'
GO
--事务查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssAffairList' WHERE C_MKDM = '940606'
GO
--措施维护
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssPrjSuperivseEdit' WHERE C_MKDM = '940903'
GO
--措施查看
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssPrjSuperivseList' WHERE C_MKDM = '940906'
GO
--安全台帐	
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssFileUpload' WHERE C_MKDM = '941206'
GO
--目标维护	
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssMeasureEdit' WHERE C_MKDM = '942601'
GO
--目标查看	
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssMeasureList' WHERE C_MKDM = '942602'
GO
--安全检查记录		
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssPrjSuperivseEditDitCA4' WHERE C_MKDM = '941601'
GO
--安全记录查看		
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssPrjSuperivseListDitCA4' WHERE C_MKDM = '941602'
GO
--安全事故记录		
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssPrjSuperivseEditDitCA5' WHERE C_MKDM = '941501'
GO
--事故查看		
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssPrjSuperivseListDitCA5' WHERE C_MKDM = '941502'
GO


--科技管理项目树        Bery    2013-09-25 15:28
--施工日志录入 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siBuildDiaryEdit' WHERE C_MKDM = '910201' 
go
--施工日志查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siBuildDiaryView' WHERE C_MKDM = '910202' 
go
--施工组织设计 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siConstructionOrg' WHERE C_MKDM = '910501' 
go
--施工组织查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siConstructionOrgView' WHERE C_MKDM = '910517' 
go
--专项方案管理 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siExpertPrj' WHERE C_MKDM = '910803' 
go
--专项方案查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siExpertPrjView' WHERE C_MKDM = '910820' 
go
--技术标准台帐管理 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siTechnology' WHERE C_MKDM = '911006' 
go
--技术标准台帐审核 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siTechnologyAudit' WHERE C_MKDM = '911036' 
go
--技术标准台帐查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siTechnologyView' WHERE C_MKDM = '911024' 
go
--设计变更资料管理 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPrjLinkEditLevels4' WHERE C_MKDM = '911311' 
go
--设计变更资料查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPrjLinkViewLevels4' WHERE C_MKDM = '911329' 
go
--技术交底 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siTechnologyJd' WHERE C_MKDM = '911508' 
go
--技术交底查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siTechnologyJdView' WHERE C_MKDM = '911526' 
go
--图纸自会审 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siDrawCheckUp' WHERE C_MKDM = '911709' 
go
--图纸自会审查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siDrawCheckUpView' WHERE C_MKDM = '911727' 
go
--中间交接资料 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPrjLinkEditLevels5' WHERE C_MKDM = '911912' 
go
--中间交接资料查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPrjLinkViewLevels5' WHERE C_MKDM = '911930' 
go
--工程联系单 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPrjLinkEditLevels3' WHERE C_MKDM = '912110' 
go
--工程联系单查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPrjLinkViewLevels3' WHERE C_MKDM = '912128' 
go
--工程确认单 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siEngineerConfirmEditLevels7' WHERE C_MKDM = '914001' 
go
--工程确认单查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siEngineerConfirmViewLevels7' WHERE C_MKDM = '914002' 
go
--工程洽商单 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siEngineerConfirmEditLevels8' WHERE C_MKDM = '915001' 
go
--工程洽商单查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siEngineerConfirmViewLevels8' WHERE C_MKDM = '915002' 
go
--测量资料管理 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siMeasureDataTab' WHERE C_MKDM = '912307' 
go
--测量资料管理查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siMeasureDataTabView' WHERE C_MKDM = '912325' 
go
--技术竣工计划 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siFinishTab' WHERE C_MKDM = '910313' 
go
--技术竣工计划查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siFinishTabView' WHERE C_MKDM = '910331' 
go
--技术总结上报 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siSumEdit' WHERE C_MKDM = '913332' 
go
--技术总结查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siSumList' WHERE C_MKDM = '913335' 
go
--技术进步计划申报 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPlanEdit' WHERE C_MKDM = '910401' 
go
--进步计划项目审核 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPlanList' WHERE C_MKDM = '910412' 
go
--技术进步计划审批 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPlanAduit' WHERE C_MKDM = '910411' 
go
--技术进步计划查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siPlanView' WHERE C_MKDM = '910402' 
go
--技术进步项目实施 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siImplEdit' WHERE C_MKDM = '910403' 
go
--技术进步实施评价 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=siImplAudit' WHERE C_MKDM = '910404' 
go


--项目过程管理      Bery        2013-09-25 16:53
--电子归档 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poPrjElecFileEdit' WHERE C_MKDM = '90030501' 
go
--电子档案查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poPrjElecFileList' WHERE C_MKDM = '90030502' 
go
--文本归档 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poProjDoc' WHERE C_MKDM = '90030503' 
go
--文本档案查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poProjDocSearch' WHERE C_MKDM = '90030504' 
go
--检查 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poCheckEdit' WHERE C_MKDM = '90930301' 
go
--整改 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poCheckRectify' WHERE C_MKDM = '90930302' 
go
--验证 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poCheckCertify' WHERE C_MKDM = '90930303' 
go
--项目检查审批 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poCheckAudit' WHERE C_MKDM = '909307' 
go
--项目检查查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poCheckList' WHERE C_MKDM = '909311' 
go
--项目监察 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poSuperviseEdit' WHERE C_MKDM = '909315' 
go
--项目监察审批 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poSuperviseAudit' WHERE C_MKDM = '909317' 
go
--项目监察查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poSuperviseList' WHERE C_MKDM = '909319' 
go
--项目奖罚 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poItemProgEdit' WHERE C_MKDM = '909103' 
go
--项目奖罚查询 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poItemProgList' WHERE C_MKDM = '909111' 
go
--项目奖罚执行 
UPDATE PT_MK SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=poItemProgView' WHERE C_MKDM = '909114' 
go


--删除合同预算模块产量上报表中与合同预算节点关联的外键		lhy		2013-09-27 13:30
ALTER TABLE Bud_ContractConsTask DROP CONSTRAINT FK_Bud_ContraintConstTask_TaskId
GO

--执行下面两句时，先执行document/SQL/其他/设备管理.sql		lhy		2013-09-30	12:00
--物资需求计划增加设备Id列  
ALTER TABLE Sm_Wantplan ADD EquipmentId nvarchar(100)
GO
--出库管理增加设备Id列
ALTER TABLE Sm_OutReserve ADD EquipmentId nvarchar(500)
GO

--删除城市外键   dhw	2013-10-09:14:57
IF OBJECT_ID('FK_Basic_City_ProviceId') IS NOT NULL
      ALTER TABLE Basic_City DROP CONSTRAINT FK_Basic_City_ProviceId
GO
-- 添加区域    dhw	2013-10-09:14:57
IF OBJECT_ID('Basic_Area') IS NOT NULL
DROP TABLE Basic_Area
GO
CREATE TABLE Basic_Area(
Id NVARCHAR(200) primary key,
Code NVARCHAR(200),
Name NVARCHAR(200),
OrderNo INT
)
GO

-- 添加省份  dhw	2013-10-09:14:57
IF OBJECT_ID('Basic_Province') IS NOT NULL
DROP TABLE Basic_Province
GO

CREATE TABLE Basic_Province(
Id NVARCHAR(200) primary key,
AreaId NVARCHAR(200),
Code NVARCHAR(200),
Name NVARCHAR(200),
OrderNo INT,
)
GO

-- 添加城市    dhw	2013-10-09:14:57
ALTER TABLE Basic_Province ADD CONSTRAINT FK_Province_Area_id
FOREIGN KEY(AreaId) REFERENCES Basic_Area(Id) 
GO
IF OBJECT_ID('Basic_City') IS NOT NULL
DROP TABLE Basic_City
CREATE TABLE Basic_City(
Id NVARCHAR(200) primary key,
ProvinceId NVARCHAR(200),
Code NVARCHAR(200),
Name NVARCHAR(200),
OrderNo INT,
)
ALTER TABLE Basic_City ADD CONSTRAINT FK_Province_City_id
FOREIGN KEY(ProvinceId) REFERENCES Basic_Province(Id) 
GO

--修改省份信息   dhw	2013-10-09:14:57
UPDATE PT_PrjInfo_ZTB_Detail 
SET province=SUBSTRING(province,7,len(province)) 
WHERE ISNUMERIC(SUBSTRING(province,1,6))>0 AND province IS NOT NULL
--基础配置添加省市信息菜单  dhw	2013-10-09:14:57
INSERT INTO PT_MK VALUES(9911,'省市信息','EPC/Basic/CityList.aspx','y','11','',24998,0,0,0,'',1)

-- 添加区域信息 dhw	2013-10-09:14:58
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('4e38db17-c0ac-4d4e-95fd-b8aabd9b8f5e', 'HN', '华南', '1'); 
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('233de497-d309-4e48-af36-9956c0261dd0', 'HB', '华北', '2'); 
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('a49158c2-fdc8-4dda-b83e-6703407f48b7', 'XB', '西北', '3'); 
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('f1cf9cff-64ad-448a-a873-03fd8394b1eb', 'HD', '华东', '4'); 
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('d85186ed-a16b-4985-8e94-11f49c926348', 'DB', '东北', '5'); 
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('59f4c979-1358-4ea1-b2a7-27fc3f04fcdb', 'XN', '西南', '6'); 
INSERT INTO Basic_Area(Id, Code, Name, OrderNo) VALUES('f7c3d7ba-f395-4912-af16-675936cdcf42', 'HZ', '华中', '7'); 

-- 添加省份信息 dhw	2013-10-09:14:58
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('43761efb-cad8-4e98-867e-ae72fa9dbab1', '4e38db17-c0ac-4d4e-95fd-b8aabd9b8f5e', 'GD', '广东省', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('b9440b4b-570d-42ca-9658-04cca18f8d99', '4e38db17-c0ac-4d4e-95fd-b8aabd9b8f5e', 'HUN', '湖南省', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('919fb7e6-a627-4d61-a8a4-002f8d1e955e', '4e38db17-c0ac-4d4e-95fd-b8aabd9b8f5e', 'JX', '江西省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('80d11025-1d8e-4d79-88fc-190b9a75a9d6', '4e38db17-c0ac-4d4e-95fd-b8aabd9b8f5e', 'FJ', '福建省', '4'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('28aeedf6-735e-4159-8fae-ae6d7a1cc88c', '4e38db17-c0ac-4d4e-95fd-b8aabd9b8f5e', 'HAN', '海南省', '5'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('4c8a1d58-1ff0-4295-9512-57e04b438195', '233de497-d309-4e48-af36-9956c0261dd0', 'BJ', '北京市', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('671b1bf1-ddba-42b8-ad46-6e464079b2d3', '233de497-d309-4e48-af36-9956c0261dd0', 'TJ', '天津市', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('686b9bf2-35aa-457c-b6be-bc193c5f8483', '233de497-d309-4e48-af36-9956c0261dd0', 'HEB', '河北省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('83e23cbd-0643-40b2-86d2-4ef0a4df04db', '233de497-d309-4e48-af36-9956c0261dd0', 'SD', '山东省', '4'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('95691f4e-0878-4369-bb55-04b83b517b99', '233de497-d309-4e48-af36-9956c0261dd0', 'NM', '内蒙古自治区', '5'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('de29503b-5981-4693-bc86-c1b19cf4c487', 'a49158c2-fdc8-4dda-b83e-6703407f48b7', 'XJ', '新疆维吾尔自治区', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('8b49d4fd-c2f3-4016-8057-7939a31451d7', 'a49158c2-fdc8-4dda-b83e-6703407f48b7', 'QH', '青海省', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('43481603-697b-43f8-b0d7-599b747f4663', 'a49158c2-fdc8-4dda-b83e-6703407f48b7', 'GS', '甘肃省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'a49158c2-fdc8-4dda-b83e-6703407f48b7', 'NX', '宁夏回族自治区', '4'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'f1cf9cff-64ad-448a-a873-03fd8394b1eb', 'SH', '上海市', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('44a5db48-4443-4b67-a23a-032a827b66f6', 'f1cf9cff-64ad-448a-a873-03fd8394b1eb', 'JS', '江苏省', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('595dd9ab-6463-4865-a4aa-cc04a859a606', 'f1cf9cff-64ad-448a-a873-03fd8394b1eb', 'ZJ', '浙江省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'f1cf9cff-64ad-448a-a873-03fd8394b1eb', 'AH', '安徽省', '4'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('05baaa5a-f767-4315-9300-3a3f676a237c', 'd85186ed-a16b-4985-8e94-11f49c926348', 'LN', '辽宁省', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'd85186ed-a16b-4985-8e94-11f49c926348', 'JL', '吉林省', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'd85186ed-a16b-4985-8e94-11f49c926348', 'HLJ', '黑龙江省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', '59f4c979-1358-4ea1-b2a7-27fc3f04fcdb', 'CQ', '重庆市', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', '59f4c979-1358-4ea1-b2a7-27fc3f04fcdb', 'SC', '四川省', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('ec1e722b-3e39-4794-8e80-9f004fb3c7a8', '59f4c979-1358-4ea1-b2a7-27fc3f04fcdb', 'GZ', '贵州省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('0b5764eb-6a9b-4c40-b15f-9e76a4493206', '59f4c979-1358-4ea1-b2a7-27fc3f04fcdb', 'YN', '云南省', '4'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('21584824-5e6c-4025-9482-f95cbc045c03', '59f4c979-1358-4ea1-b2a7-27fc3f04fcdb', 'GX', '广西壮族自治区', '5'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'f7c3d7ba-f395-4912-af16-675936cdcf42', 'HUB', '湖北省', '1'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'f7c3d7ba-f395-4912-af16-675936cdcf42', 'HEN', '河南省', '2'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('bc734453-8f63-43d8-8b3f-1b392dacd447', 'f7c3d7ba-f395-4912-af16-675936cdcf42', 'JX', '山西省', '3'); 
INSERT INTO Basic_Province(Id, AreaId, Code, Name, OrderNo) VALUES('ea4b8283-9565-4368-a9ac-43934ee58539', 'f7c3d7ba-f395-4912-af16-675936cdcf42', 'SX', '陕西省', '4'); 

-- 添加城市信息 dhw	2013-10-09:14:58
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('920cf7ac-eb79-4c1c-b1a8-6b83566759f7', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'GZ', '广州', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f8256fc1-3cfe-44fe-8f6f-703292c444ae', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'SZ', '深圳', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('42ad6130-2d42-4b0e-abec-b717b7678972', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'ZH', '珠海', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0a6f102-16a4-4fc4-9a2d-e2574a44b0d1', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'ST', '汕头', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ad344f54-113b-41bb-9f9d-af66457ca948', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'SG', '韶关', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6b93cf99-13ba-4119-9d88-b4dfef1ede30', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'FS', '佛山', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a0624140-e12b-408f-a715-7a0401002eee', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'JM', '江山', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c7af077f-8fba-4164-868d-01d6bd4bf420', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'ZJ', '湛江', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('20705609-aad2-4c90-9f6c-ee3473b41244', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'MM', '茂名', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('826fa625-9a7e-4bad-9287-4d00e6928587', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'ZQ', '肇庆', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8ce31c38-e513-46bc-a380-b6012f3731a9', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'HZ', '惠州', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ae5be4fc-7127-4f02-9260-c01961d35490', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'LD', '罗定', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('904acbe6-bd49-437f-bd91-339f8ee1063d', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'MZ', '梅州', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8f34c8c4-23f2-4608-b49a-6a991e7ead3c', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'SW', '汕尾', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b9a5dd30-16f3-485f-ab59-7ac4ae2365d3', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'HY', '河源', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0a13df00-1564-4d86-aece-af626c499316', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'YJ', '阳江', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8fc68410-87df-470e-b748-516d00f5b600', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'QY', '清远', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('43799aea-07f6-45c9-9eab-933456a91137', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'DG', '东莞', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('be347023-88e9-4089-9180-11124aab4efd', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'ZS', '中山', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('408662a7-364b-4a4a-9526-9d1eca1780ee', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'CS', '潮州', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b6985223-0331-476b-821e-fbd9f2220c08', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'JY', '揭阳', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f84f9a03-e5b3-43d9-b0b8-7e37ce68ed6c', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'YF', '云浮', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('81af62d3-86ad-4b61-bab4-218bb5868b5c', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'CH', '从化', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f3643979-2cea-42b7-bf6e-3d95df190ee2', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'ZC', '增城', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b064cdd5-a2b3-4fce-b513-2de44531657d', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'LC', '乐昌', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f7643ddf-8e2b-48c8-8d50-9a7cc374193b', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'NX', '南雄', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ee84c428-391b-46ce-afeb-aad55c3020d4', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'TS', '台山', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8fae945b-7abb-4a5b-9f84-fc7aa3cb315e', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'KP', '开平', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('27328143-f6ae-4860-92d7-2d0269b6b502', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'HS', '鹤山', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0539cdde-30b9-4524-bd22-2bc12c4acf6e', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'EP', '恩平', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2fdad394-c70e-4513-95f1-48737440d360', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'LJ', '廉江', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('db94e1c8-32ee-4b1d-aaa2-c6b581481f34', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'LZ', '雷州', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eeef60f2-1822-4b24-87aa-6d3e58fa1a8f', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'WC', '吴川', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dc179ab2-8224-48a6-adb0-b8ac8603a90e', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'GZS', '高州', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('55b2292a-a619-4802-8579-1bef9c2d8f5c', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'HZ', '化州', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1dc84420-1c3a-4d92-8683-f3d70d343e63', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'XY', '信宜', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ab90e38e-ce00-4c52-8380-74e0a0a97a93', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'GY', '高要', '37'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4db7c4e6-631c-46f2-b29e-108b6a9d4a3d', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'SH', '四会', '38'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('320b194d-ac62-4cca-a701-cf82774d7b91', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'XN', '兴宁', '39'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a5e68d94-908c-4f1d-bf18-125a02ee4bff', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'LF', '陆丰', '40'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('72fd1678-f8f5-47ff-aed8-893d4822dd50', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'YC', '阳春', '41'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1424805e-b413-48c5-9ca0-2f4b8ed82d18', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'YD', '英德', '42'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9a345869-2270-4712-8741-cead490a78c3', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'LZ', '连州', '43'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('97ba150b-c241-4aa9-b2aa-1480510612ee', '43761efb-cad8-4e98-867e-ae72fa9dbab1', 'PZ', '普宁', '44'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9d1c39e7-acae-4852-bd4c-3433119598de', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'CSH', '长沙', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f888377c-05a3-455e-9401-a3cd1848ef4f', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'ZZ', '株洲', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('39bd5565-8cd8-4559-9959-c463701b2993', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'XT', '湘潭', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6402c5e5-90e9-4d31-b2d7-57a92870e993', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'JIS', '吉首', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('449c9161-017e-4915-930d-c2027fe9468c', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'ZX', '资兴', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8a48a12d-ebe8-4f17-b9c1-ef2f521611b6', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'YJS', '沅江', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0d914b4d-2fb0-45c6-8d0d-72a411f3b54a', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'ZJJ', '张家界', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4ea864e0-fb5c-4b2a-95f0-24f505c65298', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'SY', '邵阳', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5a6c6bd2-d7ea-4585-b19d-40cf38297c09', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'YY', '岳阳', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f6dc998f-aa9d-47bb-a7bd-a9024c570e37', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LDS', '娄底', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3a2a5c0f-fc07-4eec-acfd-4ea4bf53f060', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'JS', '津市', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('75a42bcf-f769-4e9f-abcd-59eef1e2203e', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'ML', '汩罗', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f95ecf4d-b2ba-4b79-b56c-7f5748a09c84', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LX', '临湘', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('239c7580-5fb4-4c7e-99cc-7e6c170315fc', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'CD', '常德', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fb95ba73-c885-450b-aae7-2658a800395b', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'YIY', '益阳', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('24b9980d-cccf-4a86-a87c-e731f441a736', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'CZ', '郴州', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c760482d-ae7b-4a03-a732-34a058aa7b4a', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'YZ', '永州', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a4947666-6cec-4354-8636-6b28229a1b83', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'WG', '武冈', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2e58b531-e740-434b-b51d-46ead00ac101', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'CN', '常宁', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2d063f6f-3475-427c-9e68-ddc26568a4ad', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LEIY', '耒阳', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5917623f-5d32-45bc-a61e-0d503157fc0f', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'HH', '怀化', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('437ed7a6-576d-49dd-bcd0-a0e3ccaff812', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'HYS', '衡阳', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8340151c-0d3e-4327-aaba-bb175befbf7c', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'SS', '韶山', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8ab01b97-c66a-47fc-9087-dd3d7e626861', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'XX', '湘乡', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('10c7b4ed-2114-44c6-8aae-3255df8779ba', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LL', '醴陵', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('acc5b83f-f515-45b5-bca0-6afa33a2c838', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LY', '浏阳', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dbb1f21f-300e-4986-b922-753884a4bd02', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LIY', '济源', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('23e810ef-98ba-409e-a7ab-b4b7960724d1', 'b9440b4b-570d-42ca-9658-04cca18f8d99', 'LYS', '涟源', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('01431610-b73f-48f8-85ef-0c6c54e0c5c3', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'NC', '南昌', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eda30cfa-1f0d-46aa-aaa7-75852d891227', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'LP', '乐平', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3f04c60c-97c6-4d7a-b769-964bcc3cfa28', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'RC', '瑞昌', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9c480e69-c982-48bf-b433-b3bfc607ec93', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'GX', '贵溪', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b6e4dd4a-e997-461a-af4e-d2c78b6b55ab', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'JGS', '井冈山', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('db350cfc-50ac-437b-805d-8bc53415406e', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'JDZ', '景德镇', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6ec86f6e-6c59-41e0-a07f-f30ae57ea303', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'NK', '南康', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('edc35360-a475-45a8-9d8a-18590d5a3639', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'FC', '丰城', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3696f241-cc7a-4a75-ad0b-73f61c75547f', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'ZSS', '樟树', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f9940632-abf5-48ab-b156-53efe71ba8b7', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'GA', '高安', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b82a4f96-b211-4281-8db7-66f207368f45', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'JJ', '九江', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a28eff4f-dd04-4373-a212-125d0d82fe20', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'PX', '萍乡', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0277c095-db8a-406b-bc53-145a405fe40d', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'XYS', '新余', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('88a6d5e0-4d08-4de6-81a7-872f85f6d289', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'YT', '鹰潭', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ee417e68-ce80-443a-bcfc-069ae8b1f779', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'GZH', '赣州', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('23866ee8-1e84-4ad2-88a6-941e3f8dcee2', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'RJ', '瑞金', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('09d5fd28-1bbb-4597-800e-e6167ed7094d', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'YCS', '宜春', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a3e01ab9-ef41-4591-8b38-fdde47283702', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'SR', '上饶', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9c452809-0eb1-48d2-b59f-02995bdfc340', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'DX', '德兴', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f7765c91-db18-46c8-8c2d-4c3cb51202d7', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'JA', '吉安', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4375d84f-7979-4e09-ba61-82f68e991840', '919fb7e6-a627-4d61-a8a4-002f8d1e955e', 'FZ', '抚州', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c2ecd659-e1b9-4c71-8d7e-f947b6995fed', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'FZS', '福州', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('537e6c6b-17ee-4677-ac55-9354ed962b86', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'XM', '厦门', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('aeffee2e-468f-4031-9094-ba941ec35a27', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'PT', '莆田', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d3d6d520-a01b-4a8e-bf06-1dd401b9af21', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'SM', '三明', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2056f01e-efda-4db7-b144-94d7457b96df', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'QZ', '泉州', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('28bbe097-ae5f-496c-ae33-8105a6dc2f89', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'WYS', '武夷山', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ace241ae-0110-47f8-9164-4054233cfd42', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'NP', '南平', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5e68a13f-0229-4440-bd58-fd7a8ccfc27e', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'LYA', '龙岩', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e60fb1fb-4f55-4095-9ee1-08dc6fa3cd85', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'ND', '宁德', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bccccd61-c71a-42ab-a09d-c9ad84004467', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'FQ', '福清', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('df3bff68-967d-4b39-a483-596fc935c1b5', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'CL', '长乐', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('06c099e8-a17c-4129-bdd1-9058970065e2', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'YA', '永安', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('57eb17ea-7463-417a-ba38-deae06e9e202', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'SSS', '石狮', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d56e9da8-abab-4aea-b2ae-4bd0efe96bd5', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'JJS', '晋江', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('55bb2029-2f47-46b4-b7e7-344546f558e0', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'NA', '南安', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0e7f952-6d83-475e-8717-e015bc81f70f', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'LH', '龙海', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('42235f03-aac8-4a90-bfec-a6f160c5d01a', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'SWS', '邵武', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('264b883b-30b9-4c79-bc00-c1e09c6cb320', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'ZZS', '漳州', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('11591e5c-1553-4ad7-9c48-95675ec7684c', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'JO', '建瓯', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dbea6f2b-d961-4cea-a0b9-5210bb362f1a', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'ZP', '漳平', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1fa4f26a-4576-49d5-a326-7d49ae37048b', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'JYS', '建阳', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d79f17b8-bc0e-4bee-9e62-4de4dbc9c952', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'FA', '福安', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('74307e44-b2c2-4592-948a-7fcf20f529b7', '80d11025-1d8e-4d79-88fc-190b9a75a9d6', 'FD', '福鼎', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3a6b7433-6b8b-4f59-8c4a-178803905dfa', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'HK', '海口', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('813b4840-8822-4500-a08e-c3c9b5d38c69', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'QS', '琼山', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('09fd77f1-46f4-4a51-b3a5-01e6c6444046', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'AD', '安定', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9caffff9-f7fc-4a8e-9aba-b141f787dff5', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'WCS', '文昌', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7b46ff84-06fb-4fe2-a45d-87e94920219c', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'QH', '琼海', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3bf14249-502a-443a-88f9-5bbd376a5009', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'WN', '万宁', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d1b70113-3909-4c78-b9d3-508029c2deca', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'DC', '屯昌', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d92cb0ba-9fac-4e9f-af7d-dcca8485bb14', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'CW', '澄迈', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d999e7df-ff83-4b87-aa91-8696a4dcf28f', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'SYS', '三亚', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f4f41a01-ade9-455b-9b6e-5a1e3765f158', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'TSS', '通什', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d43b0e7e-2208-4ff1-9f33-06505026cff9', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'BT', '保亭', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b82773db-aad1-4874-86dc-63f04edb146e', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'LS', '陵水', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5174db08-6753-4f23-af28-52324e9a38e9', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'LED', '乐东', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('44095de7-5d65-421e-9aaa-7fa6cf84a9e7', '28aeedf6-735e-4159-8fae-ae6d7a1cc88c', 'QZS', '琼中', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7bbf071b-b359-4854-b9df-f6d25f367125', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'BJ', '北京', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7168f64d-28ec-4324-807e-c67783f07b06', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'MY', '密云', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f6458c9f-16f2-424d-a53d-e3b4113704a2', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'CP', '昌平', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f1b9e355-6c5c-441b-818c-b5c5736cd923', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'SY', '顺义', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f7eae671-d02c-4f16-a025-ae11723eff80', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'PT', '平台', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('261e0576-84e1-4c29-809a-c7c7810bc990', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'HR', '怀柔', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0cd0979a-94b0-4322-92e1-c39f1e293707', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'DX', '大兴', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a475defd-1e80-4330-a96d-7fb95727ed4b', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'YQ', '延庆', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('20fabf6e-5500-451c-a6df-31a258714a3a', '4c8a1d58-1ff0-4295-9512-57e04b438195', 'TX', '通县', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4090470c-3736-4560-8c26-073d2d94a89e', '671b1bf1-ddba-42b8-ad46-6e464079b2d3', 'TJ', '天津', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('58eb218f-30e4-49ef-9192-674264fe84dd', '671b1bf1-ddba-42b8-ad46-6e464079b2d3', 'NH', '宁河', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('83078ce0-e4fa-4161-9fe0-18d1803cf286', '671b1bf1-ddba-42b8-ad46-6e464079b2d3', 'JH', '静海', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3f55e948-0b13-43ab-acb1-7571e46e6f07', '671b1bf1-ddba-42b8-ad46-6e464079b2d3', 'JX', '蓟县', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('01843356-912c-4536-8bb7-4fd9ced83d13', '671b1bf1-ddba-42b8-ad46-6e464079b2d3', 'WQ', '武清', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('959ea664-8031-4e09-8c13-04aee78b824b', '671b1bf1-ddba-42b8-ad46-6e464079b2d3', 'BD', '宝坻', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b24263ff-9934-4597-966e-e5a06cf6b609', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'SJZ', '石家庄', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('368e12b3-2108-4fda-9ea7-75898a3d34f8', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'TS', '唐山', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0608e6e8-41b3-4a3b-8e6f-27d9e958782a', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'QHD', '秦皇岛', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fff3db28-24f5-4762-82c9-07ad1046cc9b', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'HD', '邯郸', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3b1d983f-b964-4031-a7fa-f6f16ee1a4db', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'XT', '邢台', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('28e2295f-9149-4e79-af16-921a2a44d6d4', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'BDS', '保定', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8c13432d-72cb-401f-95bc-ae4e9a8f5065', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'ZJK', '张家口', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9fd1438a-1d79-45e8-9f0d-9dca70908eb5', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'GBD', '高碑店', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ecf445f0-f7b5-47bd-aff0-f710ac993dbd', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'CZ', '沧州', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fc158d3e-7540-4d74-b167-7739d966716b', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'LF', '廊坊', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('79bd504b-151d-4fb8-89e4-bc84a94e9860', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'HS', '衡水', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ef1f4089-e310-4223-b2d6-461626ccd0fc', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'XJ', '辛集', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9f8b6ece-c694-45b3-927b-7f47606f9bd6', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'GC', '藁城', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dffe6cf4-3c9e-4646-a41c-c763155d072a', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'JZ', '晋州', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d798a7bb-36d5-4539-aca5-d8470faeb66a', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'XL', '新乐', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9a4ef786-893a-455b-81fd-324e6a6ad8e8', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'LQ', '鹿泉', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('20800f12-e368-4a8b-bccd-5cc54f975bcd', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'ZH', '遵化', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9f9f8742-22b5-4c4c-b784-b0c108b14402', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'QA', '迁安', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('86c40891-6105-41c2-b24d-eccd70789c79', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'WA', '武安', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f29ef823-d3ed-4362-9861-fe86ae1cf1a2', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'NG', '南宫', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0aaeb124-56df-463e-8faa-38fa3ab896c8', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'SH', '沙河', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('76d1e475-100c-4b2d-8c52-4c3a759db732', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'ZZ', '涿州', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2cbee3ec-1182-47bf-9387-e1caad2ea562', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'SZ', '定州', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('465690d5-79d8-4682-aa1d-1d2cc83ec2ba', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'AG', '安国', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('061837f2-d20e-4dfb-9b70-9cbafa6322e9', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'CD', '承德', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b8965771-f448-4e3f-b628-fec70a8b362c', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'BT', '泊头', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bcd79e59-3572-44a7-aca3-9f08ba3f1b63', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'RQ', '任丘', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('26221126-00e5-4f5d-b140-4bf64b203e53', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'HH', '黄骅', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3dfbaf2b-6cca-4552-aea1-a32be7e234d2', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'HJ', '河间', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('30ddaccc-426b-42b8-8e7d-6f841b2f35ed', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'BZ', '霸州', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e2f74c38-a3b0-45ee-82b7-52a9fc25a4e7', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'SHS', '三河', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('20edddb6-89d9-42ec-ae12-f7b981edb778', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'JZH', '冀州', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f698afce-cf05-4072-9502-2550f5c156d4', '686b9bf2-35aa-457c-b6be-bc193c5f8483', 'SZS', '深州', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b97e675b-37a3-44e2-aeb1-e6cfa8619836', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'YT', '烟台', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('05890846-e7ac-44e5-a692-47ce0e49f958', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'ZB', '淄博', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f1174f96-bf3b-4343-8404-fa65688f4c15', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'ZZS', '枣庄', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5c5f806c-1b69-4c24-80ef-114e7569bdfa', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'DY', '东营', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7e684589-4169-42bd-913d-8946d7d7249a', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'WF', '潍坊', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d8d77f9a-1c83-4101-8780-af0504366e7a', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'JN', '济宁', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7f1c35e8-954e-402a-b78a-19fdf5ecff7e', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'TA', '泰安', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8533ee91-8106-46f4-a136-e6a4a6163669', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'WH', '威海', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ae8ddd0c-0be2-4fb2-9877-cc57d2b4d639', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'RZ', '日照', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('67bef15d-a6aa-4f01-a6ee-bc7e3f3801af', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LW', '莱芜', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('16171eb2-4606-4373-a273-d16a14de79ea', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LY', '临沂', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f5e13709-8068-47ae-8bf1-44848cb89bc2', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'DZ', '德州', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('31f4d600-fc28-4f77-8d9c-3af852e8284b', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LC', '聊城', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('34c4cf82-2b63-4795-af26-934cf0bc1588', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'BZS', '滨州', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('25a6fc63-49bf-460a-99a7-4a34611be58c', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'HZ', '菏泽', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b8d9ee4b-5cbb-41ec-99ed-81c5e3519fbf', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'ZQ', '章丘', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('158d3fd5-cbe1-4205-80fa-c0d6911192a8', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'JIN', '胶南', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('47885df3-e7d2-4daf-aade-ab87069fad73', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'JZS', '胶州', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('002e307d-2dc6-4002-b563-dca281bc6aa9', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'PD', '平度', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bdf01d1d-ba48-4136-a0d5-61e2fe524288', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LX', '莱西', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e4b7165c-cfdc-449d-b64c-9d61a94cead6', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'JM', '即墨', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('16497903-101b-4955-ac5a-06cc8958e266', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'TZ', '滕州', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2deb123f-df7b-4c74-98f7-8fd4de5bb851', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LK', '龙口', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dcfe55d4-6e0f-42e1-9506-186eaef213d5', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LYS', '莱阳', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('193bd4bb-3d11-4cc5-bfe0-e162892fa990', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LZ', '莱州', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a19e17c2-b668-440a-8d9a-1084ee45fa44', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'ZY', '招远', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b5052015-4559-49f0-b4dd-66c4aad0a23a', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'PL', '蓬莱', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f248c803-d032-4867-98cc-5a1fb8db95a2', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'QX', '栖霞', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('acf5f872-9b04-4718-808c-bbfa0f2dc87b', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'HY', '海阳', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e7033d71-0e4e-41f4-80f3-0356b89e495c', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'QZ', '青州', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('52679330-10f3-4836-8479-fe8d14fe745c', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'ZC', '诸城', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6c6fb627-987e-404e-ad58-75f6f8593679', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'SG', '寿光', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('889c5a13-dadd-40fb-8c1f-a4aa12a13d2f', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'AQ', '安丘', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e6ce1da4-8ef8-4ec8-a8dc-26644f4fa0d1', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'GM', '高密', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('625d2046-6589-4f12-9394-d6029eda3998', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'CY', '昌邑', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('720a49b4-884c-4c00-af03-a13d66ef5e4e', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'QF', '曲阜', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('afb7af57-dc2a-4276-88c8-aece6fd7a0a4', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'YZ', '兖州', '37'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('670d4751-dd91-4392-9dfb-6a5f7c43c223', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'ZCS', '邹城', '38'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('af4594cc-5375-49d2-abea-2a5c62c8ff7b', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'XTS', '新泰', '39'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e80c5680-7d64-4c71-b85e-2457ba088c1c', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'FC', '肥城', '40'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('810b50da-c48e-456c-be70-c4feefd901e1', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'RS', '乳山', '41'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e1a9fc48-a2b8-4176-af25-efe51b540e70', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'WD', '文登', '42'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d31f9a55-a73d-4658-9689-440f3f8ff3cf', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'RC', '荣成', '43'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('efe409b5-3aa0-4f2a-836e-999f53171636', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LL', '乐陵', '44'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a5c8db11-6ef6-47af-a533-721679efd2cf', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'YC', '禹城', '45'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d4b1c4da-f0ca-4e80-9f72-5fdf8d77569e', '83e23cbd-0643-40b2-86d2-4ef0a4df04db', 'LQS', '临清', '46'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('de099cdb-fe74-4a3c-8cb8-85380a2ae862', '95691f4e-0878-4369-bb55-04b83b517b99', 'HHHT', '呼和浩特', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ff501b13-4b95-4bc4-91cd-186360d1f427', '95691f4e-0878-4369-bb55-04b83b517b99', 'GTS', '包头', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ccb0c78e-507d-4361-9134-91e296784439', '95691f4e-0878-4369-bb55-04b83b517b99', 'WHS', '乌海', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e99a99c9-f570-4026-9322-655136b4a4ab', '95691f4e-0878-4369-bb55-04b83b517b99', 'CF', '赤峰', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('13ad2c52-980f-428f-a9ae-a9d498a0c205', '95691f4e-0878-4369-bb55-04b83b517b99', 'TL', '通辽', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ec0486d4-b74a-4e6d-9298-453ac68ce76b', '95691f4e-0878-4369-bb55-04b83b517b99', 'EEDS', '鄂尔多斯', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6dcdce0d-9ab2-4ae9-be46-4a52ca7b33bd', '95691f4e-0878-4369-bb55-04b83b517b99', 'HLBE', '呼伦贝尔', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6d2f6378-b698-4ab4-b0ad-40cd04bcbf62', '95691f4e-0878-4369-bb55-04b83b517b99', 'BYZE', '巴彦淖尔', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('914c9404-c4f8-4793-9ad3-87f5a633772c', '95691f4e-0878-4369-bb55-04b83b517b99', 'WLCB', '乌兰察布', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8202e10d-ca68-4f39-b348-9e8fe718829f', '95691f4e-0878-4369-bb55-04b83b517b99', 'HLGL', '霍林郭勒', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bbec0810-52cf-4d2f-8aab-c4b1b270ca4e', '95691f4e-0878-4369-bb55-04b83b517b99', 'MZL', '满洲里', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6b553a71-3c2e-4c19-8cf8-0fb66a9fb2d5', '95691f4e-0878-4369-bb55-04b83b517b99', 'YKS', '牙克石', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8cffddc2-3862-4d4a-9b96-f196dc35161d', '95691f4e-0878-4369-bb55-04b83b517b99', 'ZLD', '扎兰屯', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d93d478a-866c-4d4a-aa63-f006c68e6480', '95691f4e-0878-4369-bb55-04b83b517b99', 'GH', '根河', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5f2bb880-4e72-4346-8e9e-71222859ac3e', '95691f4e-0878-4369-bb55-04b83b517b99', 'EEGN', '额尔古纳', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5e3fd7b1-9384-4770-b55d-a0d57128685b', '95691f4e-0878-4369-bb55-04b83b517b99', 'FZ', '丰镇', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5badce67-9f9e-486a-9b78-cdb50e52a61d', '95691f4e-0878-4369-bb55-04b83b517b99', 'XLHT', '锡林浩特', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('174b62c0-9f92-4427-8e6c-2603999d5e05', '95691f4e-0878-4369-bb55-04b83b517b99', 'ELHT', '二连浩特', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('05400a9b-806a-460b-814c-92f2778062be', '95691f4e-0878-4369-bb55-04b83b517b99', 'WLHT', '乌兰浩特', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1ed50282-3a9b-4eef-a769-7c64d0738f71', '95691f4e-0878-4369-bb55-04b83b517b99', 'AES', '阿尔山', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('553c9387-c278-4ee8-a818-ce4bdf961258', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'WLMQ', '乌鲁木齐', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3c558380-8d8d-4546-8150-5e335005c7a5', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'SHZ', '石河子', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c0bb1f3f-be77-497e-839d-1216924b1d4e', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'ALE', '阿拉尔', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2edee3c3-3664-42b9-9f26-f5e974ba24f0', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'TMSK', '图木舒克', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2fbcaa8a-950a-462a-88fc-3aa5603ac54b', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'WJQ', '五家渠', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('47effdfd-ac35-4f2d-9bf1-25dc5096771f', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'ALT', '阿勒泰', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d1c63425-7de9-41f9-879e-00fbe65f29a3', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'TLF', '吐鲁番', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('af226eb1-9c8c-4634-8778-f2ba124ac46e', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'HM', '哈密', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d5c21e48-71e8-4906-938e-c38defb74a00', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'HT', '和田', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bff61d52-cb5b-443e-ad44-d175a75a7acc', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'AKS', '阿克苏', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a528ecbe-c59d-4807-81d7-43a3a8975d69', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'KS', '喀什', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ca69d1c6-909d-411b-81c4-5be5ffd5502a', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'KLMY', '克拉玛依', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('008e9222-fcde-4aa3-9e9a-a5b4c5bfd0d0', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'ATS', '阿图什', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e52352a3-3f88-4c28-844c-5268a8565d2e', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'KEL', '库尔勒', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ae591877-9144-4314-b582-5ac78f27f4ac', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'CJ', '昌吉', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ddc5d31d-ba5a-4b49-a2d4-2425480503d3', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'FK', '阜康', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4a8b30a3-57a2-4c8b-aaf1-ee6f28dd6a60', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'MQ', '米泉', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0f60aac0-08a4-44bb-91a1-bf1364c88dde', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'BL', '博乐', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('facfa801-84ed-45c1-a938-3b7ae800267b', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'YN', '伊宁', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('26d75dbc-a075-4801-b01f-adba6186a1f4', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'KD', '奎屯', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('67217ee5-eefe-4d3d-8930-5ff1816980f5', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'DC', '塔城', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d9676630-eb6f-4a98-8f3c-4be9e1b7e76e', 'de29503b-5981-4693-bc86-c1b19cf4c487', 'WS', '乌苏', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('43e2f254-d2c4-4448-83cd-70c604157b52', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'XN', '西宁', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ba84d750-b216-47f2-8f55-fd769f164ef5', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'HY', '海晏', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('77fa88c4-1647-4139-ba65-93e043b369c2', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'DT', '大通', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5ea5fae6-505f-45bf-82f3-4beea169d8ec', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'DLH', '德令哈', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8c3b7e73-750b-4b79-bf8d-394a435e94d8', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'LYX', '龙羊峡', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3dc70ad9-5ecb-4dc3-a51b-0e304b62ee8e', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'GEM', '格尔木', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('70be36a1-f333-4596-87f0-960cf4311a86', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'QML', '曲麻莱', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('96847250-49ba-4b8d-8162-c40b57f418ba', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'XH', '循化', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8caa8b98-adef-4ef6-bf38-8cff5e3fdd85', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'DCD', '大柴旦', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a2cfc6be-e41a-48d6-92be-d1ec01e7857b', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'LH', '冷湖', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e2241d2b-7ae0-41c0-8b4a-d4cc547ffba9', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'GD', '甘德', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c1b0dadc-6ebb-4e2d-b52d-722b54be600a', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'HYU', '湟源', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2416ea56-050b-41c0-9d58-a465ba3abb6f', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'GH', '共和', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('22de677d-1cc5-410d-a548-e60ce6f27e27', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'MQS', '玛沁', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('deff5ff8-404b-4a5e-9c37-194436e116a5', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'YS', '玉树', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b367db7c-75e4-499a-91b2-9146184575d5', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'HZ', '互助', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e77c0d2c-963b-4026-bb43-c7b3d016cc5f', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'MY', '门源', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cb55ef29-87b5-4b0d-aa28-9d30d660ed10', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'MH', '民和', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7ae2a9a4-821a-4a65-845c-53f402b4dfaa', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'HN', '河南', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c4ef6a59-ce25-4f1b-bee9-30033f517e38', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'JZ', '尖扎', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b439a67c-ee34-4f1a-8c44-d39f6a4a6a4a', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'DR', '达日', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('75f0e018-9997-46c7-b256-7899caa59191', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'ZK', '泽库', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('33b78a86-d480-4949-be67-c6ccd9c2914a', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'WL', '乌兰', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cd13f52c-21b1-4e77-8c1b-b1b2963a7754', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'DL', '都兰', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7996b461-63d1-4d9d-9eb2-32ae0f5841c6', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'TJ', '天骏', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3b976420-5f60-48fe-9a03-0c75ae50513a', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'ZD', '治多', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('03e26c4e-132e-447a-92a4-e287105a473d', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'CD', '称多', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a818f69f-12b2-4410-b3b1-136812d797da', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'NQ', '囊谦', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('babf6edf-1f66-4907-ba4b-e06faee03951', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'ZDS', '杂多', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8899b89e-92dd-4cf6-812e-c30b25375bd8', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'HL', '化隆', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f95ac088-0c1a-405b-afc1-fade993bbc5b', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'MD', '玛多', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7b9f69fa-5f75-4ebb-9d98-9d9b220b5a4c', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'LD', '乐都', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4e917f06-48cc-4d55-b42b-6b947e55eb78', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'MYS', '茫崖', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ded58a76-a2a3-4f8a-b62a-36b3ebe3dd20', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'GDS', '贵德', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c91f54de-1b44-407e-a696-5c36fa0bb737', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'GN', '贵南', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('507728ff-8f82-4b9b-83d8-8fc47cc36136', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'XHS', '兴海', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e7b364d5-4f99-41b1-953d-bac506b91be4', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'TD', '同德', '37'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d8f21fc3-914d-48f5-990d-e9c585bd90fe', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'QL', '祁连', '38'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a313dc98-41ef-4ce1-b0ad-3b8f32430e2e', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'GC', '刚察', '39'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8f393f3f-4410-4f9b-8c2e-2d2ee7e61081', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'HZH', '湟中', '40'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('39a87c53-6076-446b-bd1f-c4384f8c4b6f', '8b49d4fd-c2f3-4016-8057-7939a31451d7', 'BM', '班玛', '41'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2201c5a7-f269-4224-a5c9-73782d643ee1', '43481603-697b-43f8-b0d7-599b747f4663', 'LZ', '兰州', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1c19d6c8-ce00-4525-87ef-5f4cd6c044cd', '43481603-697b-43f8-b0d7-599b747f4663', 'JC', '金昌', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1128c7d0-edb3-4ad3-854a-c2169da164c2', '43481603-697b-43f8-b0d7-599b747f4663', 'TS', '天水', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7e2ef827-be75-49a3-92df-d4a7976c4871', '43481603-697b-43f8-b0d7-599b747f4663', 'JYG', '嘉峪关', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d54fa159-81bf-411e-85aa-4145077b862c', '43481603-697b-43f8-b0d7-599b747f4663', 'WW', '武威', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c663fe50-c3a3-417d-ac58-7dce31e2e64c', '43481603-697b-43f8-b0d7-599b747f4663', 'ZY', '张掖', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f9c08d4f-227c-4c5c-8ad9-8b93c85b9945', '43481603-697b-43f8-b0d7-599b747f4663', 'PL', '平凉', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5412a428-04db-4130-9c02-ced3a56494f7', '43481603-697b-43f8-b0d7-599b747f4663', 'JQ', '酒泉', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('773e131d-5c3c-4323-a882-6310218f5911', '43481603-697b-43f8-b0d7-599b747f4663', 'DX', '定西', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0e2fd6f-c27b-43ff-b02a-f6d41ba7ce79', '43481603-697b-43f8-b0d7-599b747f4663', 'LG', '陇南', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('06c8eaeb-9161-493b-9cb3-f4d3932b0f85', '43481603-697b-43f8-b0d7-599b747f4663', 'YM', '玉门', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eb60dcf5-7658-4e13-ad63-b2c7160020b3', '43481603-697b-43f8-b0d7-599b747f4663', 'DH', '敦煌', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('24fe415f-8db0-4ca0-b8f4-0befb28a91cc', '43481603-697b-43f8-b0d7-599b747f4663', 'LX', '临夏', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1c782fbb-b172-4d5d-975c-c873a434c2a9', '43481603-697b-43f8-b0d7-599b747f4663', 'HZS', '合作', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4b7f1d82-737d-410c-ac34-c279de22ca78', '43481603-697b-43f8-b0d7-599b747f4663', 'QY', '庆阳', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('59155c09-1e68-4586-955e-968cbd7df25c', '43481603-697b-43f8-b0d7-599b747f4663', 'BY', '白银', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('14972a19-27eb-4ef8-b06c-6b1abcf80214', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'YC', '银川', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4d73b4da-6006-4709-8354-bb05bf1f53c1', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'YNS', '永定', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2584d28c-d346-4d54-a6f4-885c4f5ca5da', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'HLS', '贺兰', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('de9ac8c4-df64-42df-bb08-e9fa6d6a7301', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'SZS', '石嘴山', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('52e6d9ba-9478-4903-b154-2d3cfe8e3292', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'QTX', '青铜峡', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e2bc90a3-2fdb-4231-bf0b-4a5df3f81699', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'PLS', '平罗', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ee1863e3-2a15-4f2f-a688-19245d153ee7', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'TL', '陶乐', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('35605f8b-30e9-4279-97cc-c2cc4f0564e8', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'HNS', '惠农', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ba9b0cb1-19c7-4ef0-b936-f78a019dc7fc', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'WZ', '吴忠', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3017be68-a764-4a28-9332-b2444130d8d4', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'LDS', '隆德', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b72e05d5-6929-4f10-a545-049d028a3ec8', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'ZN', '中宁', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ce6cedb4-ea57-40eb-abf4-66ba9e2b7613', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'TX', '同心', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7c94572a-2560-438d-85dd-9370830f1c89', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'LW', '灵武', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9368d3d7-fd4d-418c-bec0-2eb22c488307', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'YCS', '盐池', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2442aced-3ec4-41dc-93df-156be7adc3f1', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'JY', '泾源', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9cecb229-a60c-452a-97bb-09d87dcc1d9c', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'XJ', '西吉', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('17b26fcc-246a-4c85-a684-f4a20901ce4d', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'ZW', '中卫', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('94baeabe-6c32-46a5-99f1-aecc1fb0352a', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'GY', '固原', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a6e60e2b-bfe6-451c-9905-870f6825a9a1', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'HYS', '海原', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('39c1669b-3a7b-4f86-acf3-cbfc4a84cbe9', 'cae5dc14-dd1c-4749-9ceb-9d3542f595d1', 'PY', '澎阳', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b95c85bd-1b8b-454e-a7c3-8576030af926', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'SH', '上海', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f06c4e43-0c94-4cbb-bb22-ede833339502', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'CS', '川沙', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cb3afb22-f913-4270-80a1-71439e9fadce', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'NH', '南汇', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dda3a66f-66ac-49ea-a2c0-7ff69ea3106f', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'JS', '金山', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('16b2ac9b-d0df-4340-b2f2-a5936ab6f37b', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'QP', '青浦', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bad613f8-bdbd-4f51-8cc3-330e5d5c80be', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'JD', '嘉定', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b00bb6d7-901e-4f70-bc4e-9ed32d301003', '10e3c770-7d27-4d6b-b41c-6d37d16cc3cb', 'CM', '崇明', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('684c8c80-6628-40e3-b4e3-6cc9ae577ce9', '44a5db48-4443-4b67-a23a-032a827b66f6', 'WX', '无锡', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8f16f9be-9941-44e1-8d80-0109879af024', '44a5db48-4443-4b67-a23a-032a827b66f6', 'XZ', '徐州', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b00dd982-1194-4e05-95cb-f3550acfdfe0', '44a5db48-4443-4b67-a23a-032a827b66f6', 'CZ', '常州', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5cd4ddb4-c1f1-4436-8522-7ab6379b879f', '44a5db48-4443-4b67-a23a-032a827b66f6', 'SZ', '苏州', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a1f88583-a994-4066-8356-fa8ceae9ef97', '44a5db48-4443-4b67-a23a-032a827b66f6', 'NT', '南通', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1216ef71-5deb-446b-8d21-eda2f3f4e129', '44a5db48-4443-4b67-a23a-032a827b66f6', 'LYG', '连云港', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('81cc17e2-f298-4780-b5d2-810ab994fcef', '44a5db48-4443-4b67-a23a-032a827b66f6', 'WA', '淮安', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9ca6deec-2a2c-4b98-af8f-1360d5c3093f', '44a5db48-4443-4b67-a23a-032a827b66f6', 'YC', '盐城', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2f0d422f-1fe2-44d1-bff9-c765cf9d331c', '44a5db48-4443-4b67-a23a-032a827b66f6', 'YZ', '扬州', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b6b559e3-7d40-4e4c-970d-2ea8505c4206', '44a5db48-4443-4b67-a23a-032a827b66f6', 'ZJG', '张家港', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4c56700f-56ec-406c-9b3b-7520e2bec03c', '44a5db48-4443-4b67-a23a-032a827b66f6', 'TZ', '泰州', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3d83bdb2-d356-4ee7-984d-cc5f2bfcbb07', '44a5db48-4443-4b67-a23a-032a827b66f6', 'SQ', '宿迁', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('757c8e42-07ec-4d8c-aefa-bd6e3bda141a', '44a5db48-4443-4b67-a23a-032a827b66f6', 'JY', '江阴', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('960e92fd-2b05-4e11-b67a-52b973ba2f73', '44a5db48-4443-4b67-a23a-032a827b66f6', 'YX', '宜兴', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a341b191-2534-4c71-bcef-a288ba97baf9', '44a5db48-4443-4b67-a23a-032a827b66f6', 'PZ', '邳州', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c3a6783a-858d-461d-b682-ff4d6f8785c2', '44a5db48-4443-4b67-a23a-032a827b66f6', 'XQ', '新沂', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('561ec3d8-1497-425d-8fcf-1582956701cf', '44a5db48-4443-4b67-a23a-032a827b66f6', 'JT', '金坛', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('be31e471-e65f-4e2a-8e6e-f0c8189ad7a3', '44a5db48-4443-4b67-a23a-032a827b66f6', 'SY', '溧阳', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('863d89e8-cd7b-46e9-a41f-f52e39255576', '44a5db48-4443-4b67-a23a-032a827b66f6', 'CSS', '常熟', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f15a020b-6e30-49a4-b685-bd4390b9fe5f', '44a5db48-4443-4b67-a23a-032a827b66f6', 'ZJ', '镇江', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9e688271-5791-4efb-a338-57eb61362864', '44a5db48-4443-4b67-a23a-032a827b66f6', 'TC', '太仓', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('86dad9a0-f6e1-4357-b722-18773ebdb007', '44a5db48-4443-4b67-a23a-032a827b66f6', 'KS', '昆山', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0f8b32c2-bde4-496a-823d-1fefa0bf21de', '44a5db48-4443-4b67-a23a-032a827b66f6', 'WJ', '吴江', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('47da315f-26b4-4f4e-a42d-e24ab6ae0508', '44a5db48-4443-4b67-a23a-032a827b66f6', 'RG', '如皋', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2efa09bc-0823-4e88-93a0-1da642fe5def', '44a5db48-4443-4b67-a23a-032a827b66f6', 'TZS', '通州', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3ed46350-914b-41bf-ab62-171f6d81491b', '44a5db48-4443-4b67-a23a-032a827b66f6', 'HM', '海门', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('725ca325-29c3-45fe-bdf9-7491ce96bf28', '44a5db48-4443-4b67-a23a-032a827b66f6', 'QD', '启东', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f4abac74-5f8e-4841-9eb3-c076f4133ffa', '44a5db48-4443-4b67-a23a-032a827b66f6', 'DT', '东台', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8bc98927-5141-48a3-b5eb-58097ee7e726', '44a5db48-4443-4b67-a23a-032a827b66f6', 'DF', '大丰', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('88df076a-7d52-44c8-ba0e-2af6884e2ecd', '44a5db48-4443-4b67-a23a-032a827b66f6', 'GY', '高邮', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2037f865-9ea4-428d-8973-2307d1cf2f0d', '44a5db48-4443-4b67-a23a-032a827b66f6', 'JDU', '江都', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c842e2d3-d661-447c-b7e8-acbd33576dfd', '44a5db48-4443-4b67-a23a-032a827b66f6', 'YIZ', '仪征', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('acadfbc2-e6b8-4590-b50d-7780a7c6ee72', '44a5db48-4443-4b67-a23a-032a827b66f6', 'DY', '丹阳', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cb8261fb-d2b7-4a64-82d4-8ca94023ee7d', '44a5db48-4443-4b67-a23a-032a827b66f6', 'YZS', '扬中', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7d7e7d57-900a-47f5-bf9b-4af86fd15aa0', '44a5db48-4443-4b67-a23a-032a827b66f6', 'JR', '句容', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f34a42c7-d9f0-4864-983d-1f7bdb364881', '44a5db48-4443-4b67-a23a-032a827b66f6', 'TX', '泰兴', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7c09d6c7-b7d7-4c6c-b814-243163b9ea39', '44a5db48-4443-4b67-a23a-032a827b66f6', 'JYS', '姜堰', '37'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1be3c719-36b3-40f1-ae9a-b48a295108ef', '44a5db48-4443-4b67-a23a-032a827b66f6', 'JJ', '靖江', '38'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('93d30b26-24ac-47b2-8702-329e3082c7d4', '44a5db48-4443-4b67-a23a-032a827b66f6', 'XH', '兴化', '39'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e6b84856-0186-46ae-9ab0-ebc8b25c9f6d', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'HZ', '杭州', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bad7e3d2-0b2a-4af8-b4a1-ebd311b129f8', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'NB', '宁波', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('88e5588e-a03d-4913-a6bc-7f4df000b785', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'WZ', '温州', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('43004d08-35ed-425d-9a25-7bd155412654', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'JX', '嘉兴', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d997883c-a51f-4090-8b4d-4288fddc9e67', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'HZS', '湖州', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('24cbeff5-4996-40fb-a64a-3123be92ee3b', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'SX', '绍兴', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2c6616d3-b1a3-44c3-9c85-26221397796c', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'JH', '金华', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('281bf724-be95-4e43-a899-eaed6171e872', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'QZ', '衢州', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f5562b2f-201c-4cb0-a1e8-015166c0e5f7', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'TZS', '台州', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('70584bc5-75f4-4976-8de9-ba95c6fd7cb2', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'LS', '丽水', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('32094260-3557-4b8f-a3b9-7419ea327d16', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'JDS', '建德', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('64d6291c-60df-4a31-8659-1b4ee5f9de4c', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'FY', '富阳', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cd8a2a43-4b0a-4c19-9ae9-60f9c0219e6d', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'LA', '临安', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7d4cebbc-eeff-4e39-8599-47bde94ce45e', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'YY', '余姚', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('84868372-0f7d-440c-8d26-c8ca8e96a950', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'CX', '慈溪', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4733474c-f573-4084-8286-a6e10302c4ce', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'FH', '奉化', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ac3dafb1-c50f-4f8a-b225-163a4b4ac4b9', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'LQ', '乐清', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a7c45a8f-2f78-4d49-b0c2-f67ec4465688', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'HN', '海宁', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6a5e1996-8fe7-453f-aa1f-a2e94a8f18d3', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'PH', '平湖', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a917a577-d740-49ff-a3d4-ff1f8c20f7e3', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'TXS', '桐乡', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('18e798ab-f3c0-428b-9cbf-d112ccbb1dd2', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'ZJS', '诸暨', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d5dc856f-e65b-4503-be0b-9efd408864d4', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'SYS', '上虞', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('994804a5-bbcc-48bb-a2ba-01f13b5d5cd5', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'SZH', '嵊州', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('db120f41-8173-445b-aae9-05fc457f6668', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'LX', '兰溪', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b3b2b6f9-df53-47d6-b4a4-5f4f561ebea2', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'DYS', '东阳', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f5891eca-f768-42e2-92c8-2be539794f98', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'YK', '永康', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('564f1bb9-c767-4659-b611-d71ae93bfd7d', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'JSH', '江山', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('44185441-721d-4a9f-a8ac-ddedb53e14db', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'LH', '临海', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('957eaa7d-5162-4488-9a87-7fbcaae5f22a', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'WL', '温岭', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('398173a2-a444-466c-9744-27593659e4b5', '595dd9ab-6463-4865-a4aa-cc04a859a606', 'LQS', '龙泉', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('01bff78b-3268-499f-bee0-5c58418cf1c2', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'WH', '芜湖', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('426d8773-5a7b-4d11-868d-92a5b2a6e715', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'BB', '蚌埠', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('061fca4e-21f1-4a81-8e87-7bea821a41db', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'WN', '淮南', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('071556a4-8d8c-42e6-8565-998d5f953314', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'MAS', '马鞍山', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dc0bddd7-0573-4556-83e4-db15fc7342cf', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'CZS', '滁州', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('499f1fcc-e0ec-4680-bb8b-ba92bfe2da9a', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'BY', '阜阳', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e661015b-e66d-4862-87c9-75c0d0db1710', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'WB', '淮北', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8edd2b72-2b2a-45d4-8e2a-61d7aa9cc20c', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'TL', '铜陵', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('66c75e14-5359-491d-b785-19323b8206ea', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'AQ', '安庆', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('21fb367b-f0e0-43ac-9e4d-c9401dc7be03', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'HS', '黄山', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cf2c2b62-7ee2-466c-9b8b-2cff3e786832', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'SZS', '宿州', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('75338c12-1904-4fee-b67e-5b0877b2f5de', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'CH', '巢湖', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('42db1a57-da59-4439-9bd7-78309b581798', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'LAS', '六安', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('26799c34-219c-43d7-bf8d-2619efdf7651', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'HZS', '毫州', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8d06b4f2-f942-43fc-9ef2-32d8923f686f', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'CZH', '池州', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a539c53c-5182-413b-96f5-bfbe753fc678', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'CX', '宣城', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a65ac709-b820-45d2-b42c-57ecd11ad343', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'TCH', '桐城', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fb8233fa-07cc-4828-a4cd-53987497d07f', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'TCS', '天长', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eb57287a-1fd2-4515-b59a-fdc3fb698fa7', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'MG', '明光', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3e34acaa-9fa5-4bba-be02-267af843fc7a', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'JSS', '界首', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7c5aac22-68bf-48bb-b8ab-4a341956bee4', '1b246e97-2a46-4ac9-b80b-4a3db1819c63', 'NG', '宁国', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0ea48725-a1e5-4dbf-b755-96554b8bfdfd', '05baaa5a-f767-4315-9300-3a3f676a237c', 'SY', '沈阳', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7ff4a552-98ae-48f3-9b78-ae23eed3c321', '05baaa5a-f767-4315-9300-3a3f676a237c', 'HLD', '葫芦岛', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e57f10cd-bb0a-4bc1-b6e8-92de59002f87', '05baaa5a-f767-4315-9300-3a3f676a237c', 'WS', '抚顺', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ed933626-e9ff-41c2-ab22-6f7e52843517', '05baaa5a-f767-4315-9300-3a3f676a237c', 'AS', '鞍山', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9a26ac88-291c-4911-b836-9e1b03ae8795', '05baaa5a-f767-4315-9300-3a3f676a237c', 'WFD', '瓦屋店', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5c663fcb-73dd-4978-9a4a-4c526758b608', '05baaa5a-f767-4315-9300-3a3f676a237c', 'PLD', '普兰店', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f3eff642-f698-4b00-bc91-4754df878926', '05baaa5a-f767-4315-9300-3a3f676a237c', 'DSQ', '大石桥', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('73068e07-d216-470c-bf64-60d0a4fb5240', '05baaa5a-f767-4315-9300-3a3f676a237c', 'DBS', '调兵山', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9238424a-dd9e-4ff2-a7a6-0541c412c93d', '05baaa5a-f767-4315-9300-3a3f676a237c', 'LY', '辽阳', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6e9c34a8-f8e5-4bf7-a387-cd36c4b34228', '05baaa5a-f767-4315-9300-3a3f676a237c', 'PJ', '盘锦', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8c4428ba-9ca5-43a2-822d-d687b9b412ad', '05baaa5a-f767-4315-9300-3a3f676a237c', 'TL', '铁岭', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eb2d1ffc-8629-4e72-9704-638b0e3c4dac', '05baaa5a-f767-4315-9300-3a3f676a237c', 'CY', '朝阳', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bf9da1db-c3dc-4fb8-b5e8-52de5458bc09', '05baaa5a-f767-4315-9300-3a3f676a237c', 'JZ', '锦州', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('00f07f20-5913-4bd5-897d-3fbecfa8597f', '05baaa5a-f767-4315-9300-3a3f676a237c', 'XM', '新民', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8b661f5e-25d8-427a-97f2-1a2de3877e81', '05baaa5a-f767-4315-9300-3a3f676a237c', 'YK', '营口', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1ccad2f1-4bb8-473d-a304-7af3c32e842f', '05baaa5a-f767-4315-9300-3a3f676a237c', 'FX', '阜新', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('891118c7-0f83-49ad-a258-41454a40c5ab', '05baaa5a-f767-4315-9300-3a3f676a237c', 'ZH', '庄河', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('850b2a5a-074c-43f6-9797-3c895c7f4b55', '05baaa5a-f767-4315-9300-3a3f676a237c', 'HC', '海城', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9c65f4db-a370-4b99-b224-3e34a509fe48', '05baaa5a-f767-4315-9300-3a3f676a237c', 'DG', '东港', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d40998df-6e55-4946-8503-e7e0094cb968', '05baaa5a-f767-4315-9300-3a3f676a237c', 'FC', '风城', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('15656a30-ec2c-4574-b38d-a97890a3d9d9', '05baaa5a-f767-4315-9300-3a3f676a237c', 'LH', '凌海', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('febf92e2-0b9f-4b81-9cbc-497959128297', '05baaa5a-f767-4315-9300-3a3f676a237c', 'BZ', '北镇', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1d263029-b244-4eee-839b-1d667c8e42b5', '05baaa5a-f767-4315-9300-3a3f676a237c', 'DD', '丹东', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ac7c60db-79f1-4efe-83f2-5216981c6056', '05baaa5a-f767-4315-9300-3a3f676a237c', 'GZ', '盖州', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9c276a77-04d3-4698-9e68-c513cb438b18', '05baaa5a-f767-4315-9300-3a3f676a237c', 'DT', '灯塔', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c5ba9267-f7ab-4967-82ff-a08fc22e98d5', '05baaa5a-f767-4315-9300-3a3f676a237c', 'BX', '本溪', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('50649324-ed02-4d44-b8de-dc9362965549', '05baaa5a-f767-4315-9300-3a3f676a237c', 'KY', '开源', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c5b161e6-1e56-49a7-a759-feeeca18b373', '05baaa5a-f767-4315-9300-3a3f676a237c', 'LYU', '凌源', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9fb1fa99-4bae-4476-b7ea-27530f30268e', '05baaa5a-f767-4315-9300-3a3f676a237c', 'BP', '北票', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a8a74f4b-207b-4644-a23c-3ea8be60b144', '05baaa5a-f767-4315-9300-3a3f676a237c', 'XC', '兴城', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1d133902-ab5f-4810-8df9-e09fef2186b9', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'CC', '长春', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a4c20ee1-225b-47bc-a04b-3441abf97343', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'JL', '吉林', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('52331c1f-402a-4976-b769-b09cf4a83748', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'SP', '四平', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ccb13b12-dff4-4133-8fbc-d4723f3a2f38', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'LYS', '辽源', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fa84ef6f-1b0c-4e1a-8c53-c1fc0ea6f16a', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'TH', '通化', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('99271aae-61f0-4d70-9ad9-cd265ba37157', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'GZL', '公主岭', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7dc4dfc1-eb96-41d6-9fe6-e9bb585999da', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'MHK', '梅河口', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e5fedb58-6f55-49e3-99ad-859aca8eb9a7', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'BC', '白城', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('82f322d5-7415-42ab-9461-e2d359872e17', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'JT', '九台', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a380b577-3c93-4ef6-970c-fe6decaf5dc5', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'YS', '榆树', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2e2e998f-5535-4237-abd1-876c4c06db8c', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'DH', '德惠', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8f0af95c-2ca8-46f0-86ea-69b5f12071db', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'SLS', '舒兰', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f35ed21e-e8b0-416f-a9d9-c63a9c0fe13e', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'HD', '桦甸', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cb60b1b2-552c-4775-b0eb-75066ae7796f', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'JH', '蛟河', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0153e714-19eb-4fb4-b08d-cd0602ac5dd0', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'PS', '磐石', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2b71890f-328d-49fc-a539-05ea1fb1413d', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'BS', '白山', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5066d803-cc12-40c2-8d1a-29ef005aa960', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'SL', '双辽', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3c2ca76b-58bc-40a0-8064-f0b54983099d', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'SYS', '松原', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('675c64d1-0f8e-414c-bf2d-cdf7bd65b73c', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'JA', '集安', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('93b4d749-defc-44af-a810-c198f0dcabbc', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'LJ', '临江', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3edaf472-984b-4472-96dd-f4baf801d47b', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'DA', '大安', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('14a3c828-7289-4653-98fe-07d26a80457a', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'YN', '洮南', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('163ca67f-d43d-4426-b145-6b7998ca041e', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'YJ', '延吉', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a3020c80-65a0-494c-b23d-8a3b41dd3ebc', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'TM', '图们', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('22d0ffc2-2d0a-4512-9dde-3386a1740071', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'DHS', '敦化', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('98afe0c7-30fe-4b0c-a46a-691a8d91bc4d', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'LJS', '龙井', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d9135afd-aa71-4bc7-9226-6c909489e2e7', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'HCS', '珲春', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2b2ce0c0-0beb-48b9-93bc-c1d00336bf53', 'b2a6bee8-aaa3-4f98-83e8-07551e274d06', 'HL', '和龙', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c4b8c468-b87f-4589-9ab7-73f3f3538902', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'QQHE', '齐齐哈尔', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f4d86bb3-2cc8-48e5-bae8-9e6872ded2b0', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'HG', '鹤岗', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b8d43b90-2871-419e-81f7-3be6f2aec0c7', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'SYS', '双鸭山', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6272f9e0-2129-48f5-a6df-54d6893158f7', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'JX', '鸡西', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('252b4f0b-703e-4873-8d92-f4e622fc70b7', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'WC', '五常', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e5bdbe58-0885-4d39-b36e-6521607ee2b3', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'DQ', '大庆', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c19bdc63-d027-4e8e-9037-1c0c45bb051a', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'YC', '伊春', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fddab939-5f5f-4e4e-944d-b2fe9d847c0a', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'MDJ', '牡丹江', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4b02f185-9d4a-440c-af38-75b3da0f793e', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'JMS', '佳木斯', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c46dbc3d-0486-4232-83e9-4d768988525e', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'QTH', '七台河', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('174c3c69-7883-4d45-b33d-c3ca54f6e7c3', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'HH', '黑河', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('898405c1-2422-44b5-8450-7127afe719f4', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'SH', '绥化', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4cc028b1-60f8-4066-93d8-8398f2a2827d', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'SZ', '尚志', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9283cd20-bff5-47c2-b73e-5bca8dae2f48', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'SC', '双城', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('60ec035b-b9b6-4fef-83cd-1c33959fe0d3', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'NH', '讷河', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('11509986-117b-4386-8de0-457bacfc4067', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'MS', '密山', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f2e09ec1-ccab-492a-8ac2-390536bd2e5b', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'HUL', '虎林', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('55e0a614-48c8-4ecb-8436-09293ffe8356', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'SFH', '绥芬河', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('440d1043-5f59-4b32-b1d5-d1d738a7bc56', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'TLS', '铁力', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bd91b8a5-87fa-491b-9b70-36c3da531982', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'NA', '宁安', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('82e9314b-c11b-4f69-b944-bae577ee07b5', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'HLI', '海林', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fed33ef9-eca6-4ece-a98a-8dc74bd75f11', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'ML', '穆棱', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('644257a4-18d8-4aed-9d53-3b0d1e83e4c8', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'TJ', '同江', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bebcf43c-6dc3-4d95-81e1-bd0ce2a47d6c', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'FJ', '富锦', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4ae5543d-2b78-4a10-85f0-54afe0a4710d', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'BA', '北安', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ba81b2bc-c528-4bbf-ac12-f4ca100de4f2', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'WDLC', '五大连池', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1d8a693d-cf93-4134-a939-968e5b782e42', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'AD', '安达', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('053d9f78-cfed-461d-a224-c85d5ee0ee68', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'ZD', '肇东', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4989650e-8d78-492a-bdbc-3589f020f9fd', '237a10d4-4db3-4d5a-b5a5-b819015fd9c2', 'HLS', '海伦', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4d95b67c-a2fe-4caa-9a6f-d7102adc3b10', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'CQ', '重庆', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('15e29a4b-0452-43a1-afcc-151152c47257', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'BB', '北培', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('25d3e3b9-416a-48a0-a985-d416cb92966e', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'HC', '合川', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('70a866ff-9551-419a-b226-eb73a40c676f', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'YC', '永川', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2d3bf525-ccba-437f-bafe-4b5cadfb1c54', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'ZX', '忠县', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b0f40110-3a5a-48a8-9395-7de324b59ab9', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'LP', '梁平', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('062c60d9-e079-40fa-ba16-35041ecf9b48', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'FD', '丰都', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ff9984ad-85cf-4bba-b7b8-7299abf79145', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'JJ', '黔江', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f71206be-1335-48c4-91e6-320cf275bb3d', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'NT', '南桐', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d77628e1-f4eb-4e91-9e94-39cf32321245', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'SQ', '双桥', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0a9a741d-896b-4c31-95a1-02c39f540ced', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'JJI', '江津', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d7b21124-85aa-49f1-8f77-56867c8fd59e', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'DZ', '大足', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f7bf3188-f13e-4515-886d-93eff3883473', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'YY', '云阳', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c4e24905-ac7a-457d-8081-cdad43f1aeea', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'KX', '开县', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8a1c2c7f-97d2-4d11-897c-0fd2bf76faae', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'DJ', '垫江', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ea85a735-7a50-4754-944e-fcca8e063e50', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'JB', '江北', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5f0a3d6e-990b-4347-b01a-2ffc1135b953', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'CS', '长寿', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e155a653-71f0-497c-bae9-6eed94cd9c30', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'RC', '荣昌', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a373d32d-308a-4a54-9949-82104f186a38', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'TN', '潼南', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7c43ec9f-451e-47c1-b73d-ba867b228e55', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'FJ', '奉节', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c6d79a0c-6489-454a-bbee-caeb767db8eb', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'WX', '巫溪', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8ff611cc-d200-4bd9-9f45-8c193181dbdc', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'WL', '武隆', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('12a67dfc-c95c-400f-be49-08e895ace5fc', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'BX', '巴县', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b61f5a6c-c252-421f-8bbb-d6bcf18cb083', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'QJ', '綦江', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b44cb20e-92da-4786-ac98-86e9f26495a1', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'BS', '璧山', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cdf94d3e-937b-4c99-9956-239ebc11828f', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'WXS', '万县', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('441cf481-28c3-48a1-817f-1ffdc6a74025', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'WS', '巫山', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('94173982-ce58-409a-813d-0e28a742e500', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'CK', '城口', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('753e3936-9e75-494d-ac60-0c690c0c2f47', 'dbdd6f27-545d-49cc-b3f2-1d43a4d7fd38', 'SZ', '石柱', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('342322ad-6973-471b-bd57-d91dc38c52e9', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'CD', '成都', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0a46d8d6-9af8-45f9-ae16-abf005a03390', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'ZG', '自贡', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cdf954c9-8833-4a0a-8d17-a0936ccdfc44', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'PZH', '攀枝花', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c1b54615-5425-4c79-8144-393bc73e92f5', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'DJY', '都江堰', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3fb0d85a-9c81-4f55-9e4d-4773e0d3db4b', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'EMS', '峨眉山', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0d45b141-c028-4f64-813b-7836e7e93414', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'PZ', '彭州', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('47aa5281-acd4-4c6b-b93e-8547aee8091b', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'QL', '邛崃', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9826f07c-0337-4215-832c-40aac2ca5915', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'CZ', '崇州', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a8bff0a3-51e5-421f-bc62-69c148a3368f', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'LZ', '泸州', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('67289b7b-d6a1-4094-a85e-ca4f5d88b7c1', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'DY', '德阳', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3cae1733-418e-4493-80b5-0a231b54803b', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'JY', '绵阳', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('88eea5ee-ef2e-4fa5-8ce3-21c9aeac2ffd', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'GY', '广元', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1914cc1c-36c9-49c7-a69f-91ee6b40c2c0', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'SN', '遂宁', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8b6c9335-54d1-40b0-8d98-6039e2da7859', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'GH', '广汉', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d836bef4-0196-4e24-87b2-6fa7d299625d', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'SF', '什邡', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8485d31e-0e42-42ea-91c7-5e33a2f22164', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'MZ', '绵竹', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1d316323-1d29-4da8-ad74-e3667b8fc1e5', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'NJ', '内江', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ce2e6cd7-6525-4b29-99a4-31315a33aa65', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'LS', '乐山', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('01520135-499f-4dc7-9aad-48e0cf59f0b0', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'NC', '南充', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a4d838b8-03ef-41d0-87e4-4b39c1595606', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'MS', '眉山', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6492e369-40e6-43a1-a1cf-79ad3b33580d', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'YB', '宜宾', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bf88a6db-2ff2-4175-b75d-854691c18276', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'JYO', '江汕', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a59406e9-684a-4c0d-828c-269eb5a1a979', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'LZH', '阆中', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5e0feaa9-ca40-47e3-8d78-88b85dfed756', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'HY', '华蓥', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2b1ad3a7-defe-40b1-af78-77d50687db02', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'GA', '广安', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('61caa726-3f07-481d-af2a-d679d8d4f9d5', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'DZS', '达州', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e8a154d3-e4f4-4f15-a3f9-833515546d19', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'YA', '雅安', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('47eaa173-d9a2-4223-9bca-f537bdca9943', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'BZ', '巴中', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b1ceaafb-04b1-4159-88c7-b66ece8d2ebe', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'ZY', '资阳', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('765eeba0-c05d-4237-a739-33690f816b25', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'WY', '万源', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('84810d98-009e-4174-96e1-3f147a7c7b94', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'JYS', '简阳', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('90c476f4-75e8-440d-b2a6-25d93e10d58f', '3ab565f7-7b6e-41e3-ac22-d51e02fc60e6', 'XC', '西昌', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('96d36fbb-8261-40eb-af26-c1d8845a16e9', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'GYS', '贵阳', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c6b15ad7-1563-4bd9-9bd1-91ba7ec54d21', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'LPS', '六盘水', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6704a50c-c411-43c7-92a1-2d889cf2247c', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'CSS', '赤水', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eaf7c689-5b2f-4c5f-8525-1253e5c8f391', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'AS', '安顺', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f4c609b1-383c-4971-af98-ca606105ff2b', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'QZ', '清镇', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('97e36616-af44-4f3e-80ec-b92bdb7d6023', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'ZYS', '遵义', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2d8ff8b4-1dc1-42c7-9656-99a117c1131b', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'RH', '仁怀', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d92bf067-c255-487a-8330-5d621729ff95', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'TR', '铜仁', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b564c36e-0fa8-427a-96c0-3de2609e58ca', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'BJ', '毕节', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6a063f7a-0b65-4551-855e-a58f28e82eb9', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'XY', '兴义', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8b49a9dd-3933-4f4b-a0b9-38fe7ff68955', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'KL', '凯里', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8276aa99-c511-4dfb-a208-ec8dbad861c7', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'DJS', '都匀', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('83927687-ec85-4491-9a0a-e5623faf001f', 'ec1e722b-3e39-4794-8e80-9f004fb3c7a8', 'FQ', '福泉', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0a40f37-500e-4bd8-8140-01e88641bda4', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'KM', '昆明', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('25ea6585-fc9f-4d00-9fc7-0b839f3678ad', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'QJS', '曲靖', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('88f64301-e3ba-44ec-8eb7-081a8158992d', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'YX', '玉溪', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('161e9a6c-4fb7-41ba-b485-cf17ea04db79', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'BD', '保山', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d5318d3e-4234-4cd1-b0e0-156d1498fc0d', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'RL', '瑞丽', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b915ac74-3898-443b-a53f-6f604b921065', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'ST', '邵通', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('eac06966-9c07-4cbf-8f88-4937feb12f5c', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'LJ', '丽江', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('aa29e888-936f-4155-a9b6-af9d5ac37a39', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'PE', '普洱', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('409acb03-3fd6-44e8-956c-d9aafead7a1f', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'LC', '临沧', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7aaa6879-746f-4c80-b3f7-c1d5979100b6', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'AN', '安宁', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('73534c62-f7c2-470d-a5aa-daa85e202922', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'XW', '宣威', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bee426a7-cd9a-445e-b716-5ae7f3c246c2', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'GJ', '个旧', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8063269b-d696-4f09-a01d-b67eae5dad4a', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'KY', '开远', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f92c075d-1e8d-4e18-b2b6-a657bc9305df', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'JH', '景洪', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('17acca99-7af7-4534-a564-ec74a5a0f90d', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'CX', '楚雄', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('632bb8f8-3338-43d7-ac07-0d8f5c108cd8', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'DL', '大理', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1f118796-365b-4ef7-8f75-51e633b62143', '0b5764eb-6a9b-4c40-b15f-9e76a4493206', 'LX', '潞西', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ecc00086-5ab2-4fea-bd83-83c9b84f2c57', '21584824-5e6c-4025-9482-f95cbc045c03', 'NN', '南宁', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3f75c00b-6f27-49e2-b9c5-e6469ae7f029', '21584824-5e6c-4025-9482-f95cbc045c03', 'LZS', '柳州', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0bb18e1-56b9-42db-a9ac-21f27835c126', '21584824-5e6c-4025-9482-f95cbc045c03', 'GL', '桂林', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('aa3c931c-04a8-47fa-8c37-b25490ad45e3', '21584824-5e6c-4025-9482-f95cbc045c03', 'WZ', '梧州', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d71667a8-f457-4c60-a23d-c9c50d3eb6b1', '21584824-5e6c-4025-9482-f95cbc045c03', 'BH', '北海', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7bbcc98b-023b-4db7-be87-a7245c45067e', '21584824-5e6c-4025-9482-f95cbc045c03', 'FCG', '防城港', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d2d5eb70-82e1-494f-9a35-dd3ad409554c', '21584824-5e6c-4025-9482-f95cbc045c03', 'QZS', '钦州', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('33a786cf-5e14-4d93-896e-24b902fb9ab7', '21584824-5e6c-4025-9482-f95cbc045c03', 'GG', '贵港', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('63195900-aad6-4549-8232-389b7972c526', '21584824-5e6c-4025-9482-f95cbc045c03', 'YL', '玉林', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6079169f-5c9e-46c6-bfa6-74ff91929505', '21584824-5e6c-4025-9482-f95cbc045c03', 'BSS', '白色', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e72991e5-0257-4ab6-9d5d-240c585a2364', '21584824-5e6c-4025-9482-f95cbc045c03', 'GZ', '贺州', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c6548fde-e9c8-46ec-a494-f85dc8c69b3f', '21584824-5e6c-4025-9482-f95cbc045c03', 'HCS', '和池', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('43ee3f32-634d-45f3-8d79-2bac014a56ef', '21584824-5e6c-4025-9482-f95cbc045c03', 'LB', '来宾', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7c71c14e-cd76-457f-bdb2-c6b93567ca9c', '21584824-5e6c-4025-9482-f95cbc045c03', 'CZS', '崇左', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('836ca183-c8fd-4c2c-aec7-edc9bbf87638', '21584824-5e6c-4025-9482-f95cbc045c03', 'CXS', '岑溪', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ae943712-9787-42a6-8e63-0005a748c379', '21584824-5e6c-4025-9482-f95cbc045c03', 'DX', '东兴', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('92a14c21-6398-450f-a331-192efb4cf104', '21584824-5e6c-4025-9482-f95cbc045c03', 'GP', '桂平', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a1c59de3-c0b9-463b-bdb2-a710449ba282', '21584824-5e6c-4025-9482-f95cbc045c03', 'BL', '北流', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a99d2b2b-d0ea-4939-bed3-1a325204eda4', '21584824-5e6c-4025-9482-f95cbc045c03', 'YZ', '宜州', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d8bb2750-5be6-4d34-8f74-0222d9aab52a', '21584824-5e6c-4025-9482-f95cbc045c03', 'HS', '合山', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bd4f3bf1-4f5b-4832-825d-5bae8d236b4d', '21584824-5e6c-4025-9482-f95cbc045c03', 'PX', '凭祥', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7d01afef-0272-4df1-9411-64a8d1f2b74c', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'WH', '武汉', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('00fa9f3a-16f8-46d9-981e-5bee25c45654', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'HS', '黄石', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b10d7650-de40-4538-81c8-32156e9687ff', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'SY', '十堰', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4036e0c6-fce7-4c2d-bcf8-a2517e33fa71', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'JZ', '荆州', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('19b016e6-4635-4f6c-83bb-d187fee2d244', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'YC', '宜昌', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('322062b6-740f-4e44-b2a8-c9f1aa1033ae', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'XF', '襄樊', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b099ffb6-dcd9-467c-be15-21c12378ba38', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'EZ', '鄂州', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5712282f-6913-4267-8055-9c853ddba399', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'DJK', '丹江口', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('72ccf42c-ff2f-4165-b307-36c27e6c37d8', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'LHK', '老河口', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('72b3ff9d-ad01-4836-a42f-dd516278e5f6', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'HG', '黄冈', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f6674f62-f7e8-4549-bee3-adf03c546d22', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'XN', '咸宁', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('715d1ce6-5b32-43fc-8ca6-418194e2e962', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'SZ', '随州', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dc4dcf7d-38e4-45eb-b9d9-0599fa36f913', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'DZ', '大冶', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3ad15a43-4a99-4efc-b0e2-8cac6ba0ba54', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'JM', '荆门', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1ca7d32a-89d0-4da0-a2a2-dcb8cf2a7bb7', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'HH', '洪湖', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9df8e077-8414-4125-88d0-e6be7f30f47e', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'SS', '石首', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7563f9ce-f35a-42f0-8f8d-ccc5762816a9', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'SZI', '松滋', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b586633a-bf54-4cc5-ae5c-c33bc75f3977', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'YD', '宜都', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('24cd1023-3f94-44eb-93f1-20522313c46a', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'DY', '当阳', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6b2a433b-9c18-41cc-b582-7e040c5ef378', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'ZJ', '枝江', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('052482c1-94af-456a-99d1-16e5974ef26a', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'XG', '孝感', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9afe3a65-e7d8-481e-8775-9ef353a87f06', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'ZY', '枣阳', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4d221a72-f6c6-4214-952c-6ae45d608c6a', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'YIC', '宜城', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('858aa3af-acbe-488f-92c2-fad0f0f63080', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'ZX', '钟祥', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('46d02c20-8bb5-45e2-8181-f415990515d7', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'YCH', '应城', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a9713749-4d25-4661-b74c-f9a6fdd44d7c', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'AL', '安陆', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d21366d4-959c-40ad-8108-381c88906c5a', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'HC', '汉川', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fbfff83d-fe51-4865-95e3-7bc91b515d3c', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'MC', '麻城', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5f9e6914-093e-4b20-9f4b-6286256410af', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'WX', '武穴', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b68c75f3-1c50-4e62-ad24-ac41f0fff8d2', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'CB', '赤壁', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('82b53436-f735-48ee-b4e1-a51fb657dc8f', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'GS', '广水', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('92080a75-bce3-47ae-9b0f-92798a5057bf', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'XT', '仙桃', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('507d4d10-dabc-4b05-abff-40bc513c11ec', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'TM', '天门', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0dd5ce9c-20f9-4546-96fe-d7a9b6dab010', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'QJ', '潜江', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9215eb7e-ac42-408a-8f8f-83d5e2608eed', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'ES', '恩施', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('08de2519-998a-4575-bc63-27fe385b09dd', 'c263cc94-ec89-4f2f-8a45-1cea51cd6799', 'LC', '利川', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2039ab99-b42d-468b-9683-df767d8dc5a2', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'ZZ', '郑州', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('30675aa1-3058-43da-b733-7074016252cd', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'LY', '洛阳', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('83463a1e-848e-4b0d-acf3-53361ee612c5', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'KF', '开封', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c732d1cd-0565-4e8f-8157-1c239d87e194', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'PDS', '平顶山', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('13f7de1c-142a-40d4-8bef-2dbdb6b22e23', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'SMX', '三门峡', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0bc37df7-a987-4706-9c7f-c5d9d1662998', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'HB', '鹤壁', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3b3cb029-f273-44be-9388-d427c6c5a7c4', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'ZMD', '驻马店', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('93e450a0-e76b-49f3-a5ea-97b1a069afaf', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'JZS', '焦作', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bf017143-131c-40e0-94e6-242017154f54', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'PY', '濮阳', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c55d8cc0-4884-4207-a1dc-b6260a7f801e', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'LSJ', '冷水江', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cfd3e0c9-20e1-4703-ad79-70061be38437', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'NY', '南阳', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('84a42a7c-288d-4d79-a920-c802fdb0c51d', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'SQ', '商丘', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d6352a30-95c8-42fd-8c0b-3e2c25cfeae1', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XINY', '信阳', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f7e2fd68-3c4c-4890-8816-b9b3381e8eb0', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'ZK', '周口', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('804b0e14-9c17-4198-81be-4afb9ac798a8', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XX', '新乡', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6f88d114-998c-4395-98c4-969dc0bef314', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'GY', '巩义', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('116bd121-7c71-4214-9b63-22342851170a', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XZ', '新郑', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('94bb05e0-50d7-43d2-80eb-ce95caac2df1', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XM', '新密', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('53899c03-9613-452e-9f9c-77261525a48d', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'DF', '登封', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8ecc6110-1406-4b57-aec2-b71ff3f5c53f', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XYS', '荥阳', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e9ac3c4c-253d-4665-a448-ed87d8b4f776', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'LH', '漯河', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('67f7533b-5f1d-4b35-a731-35ca9e8f5948', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'YS', '偃师', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7d301579-c434-4434-81b8-a9ca1e12dc70', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'RZ', '汝州', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ca1e601e-04d5-4aea-83b9-88f7efde1045', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'HJ', '洪江', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('83e08d33-30c3-407b-ac9a-c51daca34f77', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'WG', '舞钢', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3efe10c3-0886-4a7b-9116-38bd9036608b', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'LZ', '林州', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('33ce14e9-252a-47ca-8704-6d025515a2d2', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'WHS', '卫辉', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ef234805-5c68-492c-93b6-f8082f25f9b1', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'HX', '辉县', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a622b81c-51df-4285-b641-a54cc71b1cf2', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'QY', '泌阳', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('be14b187-dffd-4a34-bffe-0635ee3e0209', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'MZ', '孟州', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('aba6fcb8-0a1c-4f76-b284-9d90301027ad', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'CG', '长葛', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('976c7aa5-4055-4553-acd0-4ef12029cf3b', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'YM', '义马', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('60ca21c3-b83d-45e9-853a-57f5fa0070fd', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'LB', '灵宝', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d6ee4c04-b09f-4685-863f-725ad9063b00', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'DZS', '邓州', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('aaeeba2d-83a3-4595-82c2-32e06d447bd5', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'YCHS', '永城', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('95d97133-64bf-46c2-8f4b-24cecfe66769', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XC', '项城', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0bc16f6-8a1f-4380-9aa5-f93a8623bced', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'XCS', '许昌', '37'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('14e1c3b4-5f0a-4365-a7d2-8531b07afc5f', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'AY', '安阳', '38'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2f4d2716-219d-4291-b5e0-2b62c8a4bfb5', 'f866ad3e-575b-42a5-8c16-3cd952bcefc7', 'YZ', '禹州', '39'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f0aa6f62-44c3-42b6-a7e8-ae82527fec18', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YY', '右玉', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1381d0ea-27b5-4b08-8888-ef335529cdb3', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'DTX', '大同县', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('cd643a84-962f-4c88-8195-7a19f09d60d1', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HR', '怀仁', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('89c7f4f6-a854-411d-a227-4d40639866b4', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'SHY', '山阴', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dbe2cfe5-6bc2-44e8-b4f4-cd6e13dece97', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'SZH', '朔州', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('fc3029ae-81c0-49c8-b3cf-d64c44e2a25f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XZS', '忻州', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c283d893-abae-4dfa-8d39-57d264dd4c78', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YP', '原平', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f1c02e20-db61-4d9c-8fd3-9fd97d156425', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'DXS', '代县', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('64251aee-e317-4260-a0b2-14f81b59246f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'FS', '繁峙', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d44ab127-1a2f-4b56-9517-714de87e559f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JL', '静乐', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('770aa48c-ed90-446c-b800-ede1d7d77e34', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'DX', '定襄', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('48698c3a-3992-48f4-a991-5b5cb13b1796', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'CZ', '长治县', '12'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('242784c4-940b-42d4-9c6a-a0dc75824817', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YICH', '翼城', '13'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e924a47a-2018-4601-bdab-8be7170d3d03', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JC', '交城', '14'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2f580730-8ad6-4634-8553-287caa07ab02', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'WS', '文水', '15'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bdac3acd-b424-4393-825d-facb7b96dc2c', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XYI', '孝义', '16'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('887a6eab-6be0-4156-97c9-3529597c0e66', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JK', '交口', '17'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4e269509-cb69-4487-8dd9-1ae34db94f0d', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'SL', '石楼', '18'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('73fb2406-f864-469a-8826-3e87b723ab35', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LS', '离石', '19'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('09b97fa7-dd4a-4a79-8045-71d301cc0ce6', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'FSS', '方山', '20'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0189a003-91e7-44a2-bcaf-cfabe97a2fd1', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LX', '临县', '21'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8a0c0cad-2366-475d-9cd3-e4ef4d532542', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'FY', '汾阳', '22'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c78e6742-6b8f-413f-b07a-8cf11e0c4903', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HJS', '河津', '23'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ab766910-864e-416e-90af-ffbf9ef16de4', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JX', '绛县', '24'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0229be22-56f9-41ac-b701-46690bc0a9c2', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LYS', '临猗', '25'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('68008403-6bc2-42cc-83d2-8549be7ff4ff', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YJ', '永济', '26'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('94f4c904-2fe9-4bcb-ad6a-9f2030aaa26e', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'NW', '宁武', '27'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d8c25692-f0e4-4586-9d8e-dafc1c255f43', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'SC', '神池', '28'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('80108c07-344c-4cba-9c8f-7c13daa72636', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'WZ', '五寨', '29'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e90187d8-5e0a-410e-85b3-8d8179d15e4a', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'KL', '岢岚', '30'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('380b02a9-37b9-47e8-8938-2988b4974d29', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PG', '偏关', '31'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ae5f8d9e-bc0b-4f2d-bc52-8092b133afe5', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HQ', '河曲', '32'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e6003bd5-1d9b-45a2-8c5d-99f9953e8b20', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'BD', '保德', '33'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a7c3aed3-2b3d-417b-b146-772825b9b421', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'TY', '太原', '34'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('49b8d499-7dca-4b07-80d2-310fed6b9ebd', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YQ', '阳曲', '35'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('28593366-5d0c-4862-a444-7aa1aa6b299e', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'GJ', '古交', '36'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b958e210-88c7-43b7-8e5c-0350f90d1436', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LF', '娄烦', '37'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('10e1b948-4bb7-42d2-ac60-73c1f7081ec2', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'QX', '清徐', '38'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('71c53801-4294-4389-846d-eaba285215bb', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LCS', '陵川', '39'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('73dfca3d-f389-4582-aeb3-d715aeaf9149', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XNS', '乡宁', '40'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8a52fb29-e321-4642-a607-cc9905ff30f4', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JIX', '吉县', '41'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9b831167-4e5c-4439-9861-6a1ce8dd2aee', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'DN', '大宁', '42'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5bed5cf7-a628-4527-b09c-69ed8e410d51', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'GX', '古县', '43'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9afdbf7f-a1dc-4445-9b8f-10c2d251577f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'AZ', '安泽', '44'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4055bf17-e15a-4c3f-9bfd-f1f3f26678b7', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'FSH', '浮山', '45'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('62b20c99-01cf-43c9-add1-a419998a4d31', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HM', '侯马', '46'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4fdcd00e-ffac-4877-bf7d-3e66664a51c1', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'QW', '曲沃', '47'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5fa9fd02-0c6a-45cb-9fab-801f807f8b03', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LL', '柳林', '48'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('db39d6c7-d812-4dbe-8694-07ed5e2ebff0', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JS', '稷山', '49'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c89ba819-599a-4966-9ba4-1c4bab479e58', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YQU', '垣曲', '50'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('55e69395-bc20-43d6-aa13-f2f9c245bbb9', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'WR', '万荣', '51'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7e9a0f2f-2279-4349-a617-52ba8426df24', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XAX', '夏县', '52'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('5746d11c-384b-49ff-ad99-ba2d3786db61', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LQ', '灵丘', '53'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d0df8a36-3433-4e38-993e-bef13ab0e6a1', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YX', '应县', '54'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ac9c6c7d-ce3a-4d99-9153-2bbccf4f0276', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HY', '浑源', '55'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('b09a18b9-c8a7-47e5-a834-6c7837d7f981', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'GL', '广灵', '56'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7d7f6a70-8cfb-4a40-8b2c-4798ca69de19', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YG', '阳高', '57'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bf9e82a5-326d-4510-bf29-de5242d0d99f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'TZ', '天镇', '58'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f171c043-c7f4-4fcc-b01f-1b49698c8f97', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'MX', '孟县', '59'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1153f230-c04f-445e-b0bb-e8a0bd47d485', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PD', '平定', '60'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('ee801f48-561f-4b66-91b0-5599675383db', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PQS', '阳泉', '61'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('c0e20d3c-b0ca-40dd-8dc4-560cedaf4734', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YCI', '榆次', '62'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('92de62f1-f807-4a3b-a447-502f377560e4', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'TG', '太古', '63'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a00637fd-81cd-4682-b498-09a858a2065e', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'QIX', '祁县', '64'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('211bed9f-9acb-4620-9c5b-5aae477f056b', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PYS', '平遥', '65'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('050bebdb-8d4c-4c0d-8047-5feaa45cc5f4', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'JXS', '介休', '66'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('99d5fbab-a9ec-41cf-a175-9a7479a01861', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LSS', '灵石', '67'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('f85da0cb-8e86-4110-8a47-77ff0ae814a5', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'SYS', '寿阳', '68'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3734ba01-66bf-4a2e-b384-0476e1545cf1', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HZ', '霍州', '69'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('93959fc9-a999-4874-8c44-1985aa9aa298', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XXS', '隰县', '70'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('54734012-6e98-495f-8e0b-e6484e157754', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YH', '永和', '71'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8539fe5c-96c8-490f-a39f-f79c63986220', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XFS', '襄汾', '72'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3ef82deb-f0c4-4f57-8f16-c1a9fef32089', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'ZYS', '中阳', '73'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a219a198-6ec5-4c62-8381-47f474335d53', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XJ', '新绛', '74'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('57689213-0a57-4011-88f7-b596de972438', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'WXS', '闻喜', '75'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2a84055b-594f-4ea6-8dba-4326d0b096cd', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PL', '平陆', '76'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3b18da13-a1de-4096-869a-48aff44ee35f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'RC', '芮城', '77'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2dfd2751-809f-4088-8931-6a0bd13b62b2', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YSS', '榆社', '78'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('34eb1ca6-b35f-436b-9d63-50ac878ba51b', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'ZQ', '左权', '79'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('df698c49-96b6-4148-81b0-09b0ab486a01', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HSS', '和顺', '80'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('9e0497e0-ebad-488a-8aa8-22713aaa8bd8', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XIY', '昔阳', '81'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('0b0f533c-46be-4b8b-9b19-e28cba230ba5', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'CZS', '长治', '82'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3d2ba5ad-e3a1-47ee-aecb-43ebd0ecad31', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'DL', '屯留', '83'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('1c5cef0b-2846-4ae9-978c-56868b25f3d2', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XYU', '襄垣', '84'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('50ef45b6-c109-44f8-ac22-70b4bd3adbae', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'WXS', '武乡', '85'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a971e9a1-7066-459a-8586-7d3004a261d9', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'QXS', '沁县', '86'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('8e722d4b-2d5a-4130-bc7e-0ce876b23f19', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'QYS', '沁源', '87'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('605a90f1-4e62-4d78-9fb9-ceba204da662', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'CZI', '长子', '88'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('98ca1576-ef39-4c8b-ba2d-d67796cdc000', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'WT', '五台', '89'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('2984d917-52f8-4802-93db-e7bfd881796d', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PS', '平顺', '90'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3b4851b7-2df4-4ad8-b584-d435d1d1d25a', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LCS', '潞城', '91'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('dbd50250-a87e-4c21-8dcd-13e15a6df53d', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LIC', '黎城', '92'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7e072f0e-15b6-4a78-b49a-c59caecbc835', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'FP', '高平', '93'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7dd1ced7-c7e6-46a5-873e-dcd82e8b8491', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YCS', '阳城', '94'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7ff9db7b-353c-4eb3-b283-160efaab2e06', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'FX', '汾西', '95'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('d87ff1a6-b257-4c83-bcf8-c3c7ff331d67', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'HD', '洪洞', '96'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('3f553ab0-ace3-4ed1-ad30-0a690f7aa11f', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LF', '临汾', '97'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('00666690-7e9d-40f5-9eb0-59a4021f6ed8', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'PX', '蒲县', '98'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('520bbf34-af9f-435c-bd32-711a4313cd40', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'LXS', '岚县', '99'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6260a5b1-3b8b-4a5a-9482-ac6deb074411', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'XINGX', '兴县', '100'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('6915db62-2cd7-4ab3-93ab-102585835e47', 'bc734453-8f63-43d8-8b3f-1b392dacd447', 'YC', '运城', '101'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bae785bb-77aa-4bd7-9631-bc954f094719', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'TC', '铜川', '1'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('7d5e3d54-539c-4060-9ce7-3ad9cd7fa266', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'BJ', '宝鸡', '2'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('07510cd6-9ae6-45e6-b3ed-76fc19ca3feb', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'XYA', '咸阳', '3'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('450b0e90-dfc7-46fc-8ea3-c4284e141e25', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'WN', '渭南', '4'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('a7c35cf3-e96d-466b-9394-210f3a09b9af', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'YA', '延安', '5'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('33cf178f-6bc2-47de-bf3d-23873091e9be', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'HZS', '汉中', '6'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('bcbd770a-b59a-4188-96e5-b360d8b6767c', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'YL', '榆林', '7'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('e0a4b858-1fd1-412e-9055-5d741f6cbd6f', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'AK', '安康', '8'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('4ed30682-ef4e-4eec-a5b2-ba2b48c5c647', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'SLS', '商洛', '9'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('170ccff2-167b-4ee8-bd11-30cbd520608a', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'XP', '兴平', '10'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('040f8899-a91a-4b3b-8ce8-2afd619496ef', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'HCS', '韩城', '11'); 
INSERT INTO Basic_City(Id, ProvinceId, Code, Name, OrderNo) VALUES('722f0f5e-ebce-4271-a126-325468423def', 'ea4b8283-9565-4368-a9ac-43934ee58539', 'HYS', '华阴', '12'); 

--删除资格预审登记菜单权限  dhw 2013-10-09 15:21
DELETE FROM PT_YHMC_Privilege WHERE C_MKDM='701701'
--删除资格预审登记菜单 dhw 2013-10-09 15:21
DELETE  FROM PT_MK WHERE C_MKDM='701701'
--添加资格审查登记菜单权限  dhw 2013-10-09 15:21
INSERT INTO PT_YHMC_Privilege(V_YHDM,C_MKDM,IsBasic,IsHaveOp,id) VALUES('00000000','701702',0,NULL,NEWID());


--原预算中读取小计的函数		lhy				2013-10-14   18:00
IF OBJECT_ID('fn_GetTotal',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetTotal
GO
CREATE FUNCTION fn_GetTotal(@TaskId nvarchar(200))
RETURNS decimal(18,3)
BEGIN
	DECLARE @Total decimal(18,3),@TaskType char(1),@modifyCount decimal(18,3)
	SELECT @TaskType=TaskType FROM Bud_Task WHERE TaskId = @TaskId
	SET @Total=0
	IF(@TaskType!='')
	BEGIN
		DECLARE @SubCount int
		IF(@TaskType='Y')
		BEGIN 
			--如果是按年度查询，取TaskId前5位相同的数据
			SET @SubCount=5
		END
		ELSE
		BEGIN
			--如果是按月度查询，取TaskId前7位相同的数据
			SET @SubCount=7
		END
		SELECT @Total=SUM(ISNULL(ResTotal,0)+ISNULL(InBudModiyInfo.Total,0)
		+ISNULL(OutBudModifyInfo.Total,0))
		FROM 
		(
			SELECT TasRes.TaskId,SUM(ResourceQuantity*ResourcePrice) ResTotal FROM Bud_TaskResource AS TR
			RIGHT JOIN (
				SELECT Bud_Task.* FROM Bud_Task
				JOIN 
				(
					SELECT * FROM Bud_Task
					WHERE TaskId = @TaskId
				) AS T ON Bud_Task.PrjId = T.PrjId 
					AND Bud_Task.Version = T.Version
					AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
					AND Bud_Task.TaskType=T.TaskType
					AND SUBSTRING(Bud_Task.TaskId,1,@SubCount)=SUBSTRING(T.TaskId,1,@SubCount) 
			) AS TasRes ON TR.TaskId = TasRes.TaskId
			GROUP BY TasRes.TaskId
		) AS T2 
		LEFT JOIN 
		(
			--清单内的变更金额
			SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
			JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
			where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
		) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
		LEFT JOIN
		(
			--清单外的变更金额
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	ELSE
	BEGIN
		SELECT @Total=SUM(ISNULL(ResTotal,0)+ISNULL(InBudModiyInfo.Total,0)
		+ISNULL(OutBudModifyInfo.Total,0))
		FROM 
		(
			SELECT TasRes.TaskId,SUM(ResourceQuantity*ResourcePrice) ResTotal FROM Bud_TaskResource AS TR
			RIGHT JOIN (
				SELECT Bud_Task.* FROM Bud_Task
				JOIN 
				(
					SELECT * FROM Bud_Task
					WHERE TaskId = @TaskId
				) AS T ON Bud_Task.PrjId = T.PrjId 
					AND Bud_Task.Version = T.Version
					AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
					AND Bud_Task.TaskType=T.TaskType
			) AS TasRes ON TR.TaskId = TasRes.TaskId
			GROUP BY TasRes.TaskId
		) AS T2 
		LEFT JOIN 
		(
			--清单内的变更金额
			SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
			JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
			where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
		) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
		LEFT JOIN
		(
			--清单外的变更金额
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	RETURN @Total
END
GO

--添加记录投标项目变更的时间 dhw   2013-10-21 10:10
IF NOT EXISTS(SELECT * FROM information_schema.columns 
			  WHERE table_name='PT_PrjInfo_ZTB' AND column_name='PrjStateChangeTime')
   ALTER TABLE PT_PrjInfo_ZTB ADD PrjStateChangeTime DATETIME DEFAULT(GETDATE())
GO
--删除放弃管理菜单放弃管理权限  dhw 2013-10-21 10:16
DELETE FROM PT_YHMC_Privilege WHERE C_MKDM='700902'
GO
--删除放弃管理菜单放弃管理菜单 dhw 2013-10-21 15:16
DELETE  FROM PT_MK WHERE C_MKDM='700902'
GO
--工资发放表增加帐套字段		SXK			2013-10-31	11:50
ALTER TABLE SA_Payoff ADD BooksId NVARCHAR(200)
GO


--修改项目树        Bery    2013-11-21 15:12
UPDATE pt_mk SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qsFileUpload' WHERE C_MKDM = '8813'
GO
UPDATE pt_mk SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=qssFileUpload' WHERE C_MKDM = '8814'
GO
UPDATE pt_mk SET V_CDLJ = '/StockManage/ProjectFrame.aspx?path=requirePlanRpt' WHERE C_MKDM = '880905'
GO
--合同预算节点表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_ContractTask
ADD FeatureDescription NVARCHAR(MAX) --项目特征描述
GO
ALTER TABLE Bud_ContractTask
ADD MainMaterial DECIMAL(18,3) --主材
GO
ALTER TABLE Bud_ContractTask
ADD SubMaterial DECIMAL(18,3) --辅材 
GO
ALTER TABLE Bud_ContractTask
ADD Labor DECIMAL(18,3) --人工
GO
--合同预算变更表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_ConModifyTask
ADD FeatureDescription NVARCHAR(MAX) --项目特征描述
GO
ALTER TABLE Bud_ConModifyTask
ADD MainMaterial DECIMAL(18,3) --主材
GO
ALTER TABLE Bud_ConModifyTask
ADD SubMaterial DECIMAL(18,3) --辅材 
GO
ALTER TABLE Bud_ConModifyTask
ADD Labor DECIMAL(18,3) --人工
GO
--添加扩展属性 sxk 2013-11-22 07：57
EXEC sp_addextendedproperty
@name = 'MS_Description',
@value = '项目特征描述',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = FeatureDescription
GO
EXEC sp_addextendedproperty
@name = 'MS_Description',
@value = '主材',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = MainMaterial
GO
EXEC sp_addextendedproperty
@name = 'MS_Description',
@value = '辅材',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = SubMaterial
GO
EXEC sp_addextendedproperty
@name = 'MS_Description',
@value = '人工',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = Labor
GO
--修改扩展属性 sxk 2013-11-22 07：57
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '清单编码',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = TaskCode
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '项目名称',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = TaskName
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '工期(天)',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = ConstructionPeriod
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '合价',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_ContractTask,
@level2type = 'Column', @level2name = Total
GO
--目标成本节点表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_Task
ADD FeatureDescription NVARCHAR(MAX) --项目特征描述
GO
--目标成本变更表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_ModifyTask
ADD FeatureDescription NVARCHAR(MAX) --项目特征描述
GO
--目标成本资源表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_TaskResource
ADD LossCoefficient DECIMAL(4,3) --损耗系数
GO
--目标成本变更资源表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_ModifyTaskRes
ADD LossCoefficient DECIMAL(4,3) --损耗系数
GO
--目标成本模板表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_TemplateItem
ADD FeatureDescription NVARCHAR(MAX) --项目特征描述
GO
--目标成本模板资源表增加字段 sxk 2013-11-22 07：57
ALTER TABLE Bud_TemplateResource
ADD LossCoefficient DECIMAL(4,3) --损耗系数
GO
--添加修改扩展属性 sxk 2013-11-22 07：57
EXEC sp_addextendedproperty
@name = 'MS_Description',
@value = '项目特征描述',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_Task,
@level2type = 'Column', @level2name = FeatureDescription;
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '清单编码',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_Task,
@level2type = 'Column', @level2name = TaskCode;
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '项目名称',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_Task,
@level2type = 'Column', @level2name = TaskName;
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '合价',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_Task,
@level2type = 'Column', @level2name = Total;
GO
EXEC sp_updateextendedproperty
@name = 'MS_Description',
@value = '工期(天)',
@level0type = 'Schema', @level0name = dbo,
@level1type = 'Table',  @level1name = Bud_Task,
@level2type = 'Column', @level2name = ConstructionPeriod;
GO

-- 合同结算项  dhw	2013-11-22:15:49
IF OBJECT_ID('Con_BalanceItem') IS NOT NULL
DROP TABLE Con_BalanceItem
GO

CREATE TABLE Con_BalanceItem ( 
	Id nvarchar(200) NOT NULL,
	BalanceId nvarchar(200) NULL,    -- 结算ID 
	Name nvarchar(200) NULL,    -- 名称 
	Qty decimal(18,3) DEFAULT 0.0 NOT NULL,    -- 数量 
	UnitPrice decimal(18,3) DEFAULT 0.0 NOT NULL,    -- 单价 
	Type varchar(2) DEFAULT 1 NOT NULL,    -- 1管理扣项，2结算增减项，3代扣代缴税金 
	Note nvarchar(max) NULL    -- 备注 
)
GO
ALTER TABLE Con_BalanceItem ADD CONSTRAINT PK_合同结算项 
	PRIMARY KEY CLUSTERED (Id)
GO
EXEC sp_addextendedproperty 'MS_Description', '结算ID', 'Schema', dbo, 'table', Con_BalanceItem, 'column', BalanceId
GO
EXEC sp_addextendedproperty 'MS_Description', '名称', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Name
GO
EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Qty
GO
EXEC sp_addextendedproperty 'MS_Description', '单价', 'Schema', dbo, 'table', Con_BalanceItem, 'column', UnitPrice
GO
EXEC sp_addextendedproperty 'MS_Description', '1管理扣项，2结算增减项，3代扣代缴税金', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Type
GO
EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Note
GO
--施工日志添加字段 sxk 2013-11-25 19:20
ALTER TABLE opm_epcm_builddiary
ADD WaterElec INT NULL --水电
GO
ALTER TABLE opm_epcm_builddiary
ADD Mason INT NULL --泥工
GO
ALTER TABLE opm_epcm_builddiary
ADD Painter INT NULL --油漆
GO
ALTER TABLE opm_epcm_builddiary
ADD Carpentry INT NULL --木工
GO
--合同计量 添加字段 dhw 2013-11-26 12:01


--合同预算表里添加合同ID
ALTER TABLE Bud_ContractTask ADD ContractId nvarchar(200)
GO

--产值上报中添加合同ID
ALTER TABLE Bud_ContractConsReport ADD ContractId nvarchar(200)
GO

--合同预算产值上报中添加结算ID
ALTER TABLE Bud_ContractConsReport ADD BalanceId nvarchar(200)
GO

--目标成本添加合同ID
ALTER TABLE Bud_Task ADD ContractId nvarchar(200)
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Con_BalanceItem') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Con_BalanceItem
GO

CREATE TABLE Con_BalanceItem ( 
	Id nvarchar(200) NOT NULL,
	BalanceId nvarchar(200) NULL,    -- 结算ID 
	Name nvarchar(200) NULL,    -- 名称 
	Qty decimal(18,3) DEFAULT 0.0 NOT NULL,    -- 数量 
	UnitPrice decimal(18,3) DEFAULT 0.0 NOT NULL,    -- 单价 
	Type varchar(2) DEFAULT 1 NOT NULL,    -- 1管理扣项，2结算增减项，3代扣代缴税金 
	Note nvarchar(max) NULL    -- 备注 
)
GO

ALTER TABLE Con_BalanceItem ADD CONSTRAINT PK_合同结算项 
	PRIMARY KEY CLUSTERED (Id)
GO


EXEC sp_addextendedproperty 'MS_Description', '结算ID', 'Schema', dbo, 'table', Con_BalanceItem, 'column', BalanceId
GO

EXEC sp_addextendedproperty 'MS_Description', '名称', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Name
GO

EXEC sp_addextendedproperty 'MS_Description', '数量', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Qty
GO

EXEC sp_addextendedproperty 'MS_Description', '单价', 'Schema', dbo, 'table', Con_BalanceItem, 'column', UnitPrice
GO

EXEC sp_addextendedproperty 'MS_Description', '1管理扣项，2结算增减项，3代扣代缴税金', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Type
GO

EXEC sp_addextendedproperty 'MS_Description', '备注', 'Schema', dbo, 'table', Con_BalanceItem, 'column', Note
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Con_Measure') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Con_Measure
GO

CREATE TABLE Con_Measure ( 
	Id nvarchar(200) NOT NULL,    -- ID 
	ContractId nvarchar(200) NULL,    -- 合同ID 
	WorkCard nvarchar(1000) NULL,    -- 工作记录 
	FlowState int DEFAULT -1 NOT NULL,    -- 流程状态 
	RptUser varchar(8) NOT NULL,    -- 上报人 
	RptDate datetime DEFAULT GETDATE() NOT NULL    -- 上报时间 
)
GO

ALTER TABLE Con_Measure ADD CONSTRAINT PK_Con_Measure 
	PRIMARY KEY CLUSTERED (Id)
GO

EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', Con_Measure, 'column', Id
GO

EXEC sp_addextendedproperty 'MS_Description', '合同ID', 'Schema', dbo, 'table', Con_Measure, 'column', ContractId
GO

EXEC sp_addextendedproperty 'MS_Description', '工作记录', 'Schema', dbo, 'table', Con_Measure, 'column', WorkCard
GO

EXEC sp_addextendedproperty 'MS_Description', '流程状态', 'Schema', dbo, 'table', Con_Measure, 'column', FlowState
GO

EXEC sp_addextendedproperty 'MS_Description', '上报人', 'Schema', dbo, 'table', Con_Measure, 'column', RptUser
GO

EXEC sp_addextendedproperty 'MS_Description', '上报时间', 'Schema', dbo, 'table', Con_Measure, 'column', RptDate
GO



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('Con_MeasureTask') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE Con_MeasureTask
GO

CREATE TABLE Con_MeasureTask ( 
	Id nvarchar(200) NOT NULL,    -- ID 
	MesureId nvarchar(200) NULL,    -- 计量ID 
	TaskId nvarchar(200) NULL,    -- 分项工程ID 
	Qty decimal(18,3) DEFAULT 0.0 NOT NULL,    -- 本次完成量 
	WorkContent nvarchar(1000) NULL    -- 形象进度 
)
GO

ALTER TABLE Con_MeasureTask ADD CONSTRAINT PK_Con_MeasureTask 
	PRIMARY KEY CLUSTERED (Id)
GO

ALTER TABLE Con_MeasureTask ADD CONSTRAINT FK_ConMeasureTask_MesureId 
	FOREIGN KEY (MesureId) REFERENCES Con_Measure (Id)
	ON DELETE CASCADE
GO

EXEC sp_addextendedproperty 'MS_Description', '支出合同计量清单', 'Schema', dbo, 'table', Con_MeasureTask
GO
EXEC sp_addextendedproperty 'MS_Description', 'ID', 'Schema', dbo, 'table', Con_MeasureTask, 'column', Id
GO

EXEC sp_addextendedproperty 'MS_Description', '计量ID', 'Schema', dbo, 'table', Con_MeasureTask, 'column', MesureId
GO

EXEC sp_addextendedproperty 'MS_Description', '分项工程ID', 'Schema', dbo, 'table', Con_MeasureTask, 'column', TaskId
GO

EXEC sp_addextendedproperty 'MS_Description', '本次完成量', 'Schema', dbo, 'table', Con_MeasureTask, 'column', Qty
GO

EXEC sp_addextendedproperty 'MS_Description', '形象进度', 'Schema', dbo, 'table', Con_MeasureTask, 'column', WorkContent
GO

-- 合同管理添加字段
--合同预算变更表中合同ID
if NOT EXISTS(SELECT * FROM information_schema.columns
    where table_name='Bud_ConModifyTask' and column_name='ContractId')
ALTER TABLE Bud_ConModifyTask ADD ContractId nvarchar(200)
GO
--合同预算变更表中添加合同变成ID
if NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
   WHERE TABLE_NAME='Bud_ConModify' AND COLUMN_NAME='ConInModifyID')
ALTER TABLE Bud_ConModify ADD ConInModifyID nvarchar(200)
GO
-- 添加合同计量菜单
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM='050512')
INSERT INTO PT_MK VALUES('050512','合同计量','/ContractManage/IncometContract/ContractMeasure.aspx?spId=spMeasure','Y',5,'',25001,0,0,0,'',1)
GO
--合同预算产值上报表中添加类型
if NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS
   WHERE TABLE_NAME='bud_contractconsReport' AND COLUMN_NAME='Type')
BEGIN
	ALTER TABLE bud_contractconsReport ADD Type nvarchar(2) DEFAULT '0' --0:手动添加,1:合同结算修改申报量和核准量添加
END
GO
-- 更新Type的值
UPDATE bud_contractconsReport SET Type=0 WHERE Type IS NULL
--修改目标成本节点合价统计方式，添加损耗系数 	sxk 2013-11-26 15:23
IF OBJECT_ID('fn_GetTotal',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetTotal
GO
CREATE FUNCTION fn_GetTotal(@TaskId nvarchar(200))
RETURNS decimal(18,3)
BEGIN
	DECLARE @Total decimal(18,3),@TaskType char(1),@modifyCount decimal(18,3)
	SELECT @TaskType=TaskType FROM Bud_Task WHERE TaskId = @TaskId
	SET @Total=0
	IF(@TaskType!='')
	BEGIN
		DECLARE @SubCount int
		IF(@TaskType='Y')
		BEGIN 
			--如果是按年度查询，取TaskId前5位相同的数据
			SET @SubCount=5
		END
		ELSE
		BEGIN
			--如果是按月度查询，取TaskId前7位相同的数据
			SET @SubCount=7
		END
		SELECT @Total=SUM(ISNULL(ResTotal,0)+ISNULL(InBudModiyInfo.Total,0)
		+ISNULL(OutBudModifyInfo.Total,0))
		FROM 
		(
			SELECT TasRes.TaskId,SUM(ResourceQuantity*ResourcePrice*LossCoefficient) ResTotal FROM Bud_TaskResource AS TR
			RIGHT JOIN (
				SELECT Bud_Task.* FROM Bud_Task
				JOIN 
				(
					SELECT * FROM Bud_Task
					WHERE TaskId = @TaskId
				) AS T ON Bud_Task.PrjId = T.PrjId 
					AND Bud_Task.Version = T.Version
					AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
					AND Bud_Task.TaskType=T.TaskType
					AND SUBSTRING(Bud_Task.TaskId,1,@SubCount)=SUBSTRING(T.TaskId,1,@SubCount) 
			) AS TasRes ON TR.TaskId = TasRes.TaskId
			GROUP BY TasRes.TaskId
		) AS T2 
		LEFT JOIN 
		(
			--清单内的变更金额
			SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
			JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
			where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
		) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
		LEFT JOIN
		(
			--清单外的变更金额
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	ELSE
	BEGIN
		SELECT @Total=SUM(ISNULL(ResTotal,0)+ISNULL(InBudModiyInfo.Total,0)
		+ISNULL(OutBudModifyInfo.Total,0))
		FROM 
		(
			SELECT TasRes.TaskId,SUM(ResourceQuantity*ResourcePrice*LossCoefficient) ResTotal FROM Bud_TaskResource AS TR
			RIGHT JOIN (
				SELECT Bud_Task.* FROM Bud_Task
				JOIN 
				(
					SELECT * FROM Bud_Task
					WHERE TaskId = @TaskId
				) AS T ON Bud_Task.PrjId = T.PrjId 
					AND Bud_Task.Version = T.Version
					AND Bud_Task.OrderNumber LIKE T.OrderNumber + '%'
					AND Bud_Task.TaskType=T.TaskType
			) AS TasRes ON TR.TaskId = TasRes.TaskId
			GROUP BY TasRes.TaskId
		) AS T2 
		LEFT JOIN 
		(
			--清单内的变更金额
			SELECT modifyTask.TaskId,SUM(modifyTask.Total) Total from Bud_ModifyTask modifyTask
			JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budMOdify.ModifyId 
			where budModify.FlowState=1 AND modifyType=1 group by modifyTask.TaskId
		) InBudModiyInfo ON T2.TaskId=InBudModiyInfo.TaskId
		LEFT JOIN
		(
			--清单外的变更金额
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.ModifyTaskId)) total 
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	RETURN @Total
END
GO

--隐藏预算管理产值上报菜单 dhw 2013-11-26 18:55
UPDATE PT_MK SET IsDisplay=0 WHERE c_MKDM='130704'
GO
--添加目标成本产出报表菜单 sxk 2013-12-04 11:12
INSERT INTO PT_MK VALUES('290723','目标成本产出报表','/StockManage/ProjectFrame.aspx?path=rptBudGetOutPut','y','23','','25002',0,0,0,'',1)
GO
--更新发票管理-发票管理-发票台帐模块名称 sxk 2013-12-12 17:29
UPDATE pt_mk SET v_mkmc='发票 台帐',v_cdlj='/ContractManage/PayoutInvoice/InvoiceLedger.aspx',i_xh=3,v_img='',IsBasic='0',IsMaintainable='0',helpPath='' WHERE  c_mkdm='060103'  
UPDATE pt_role_privilege SET IsBasic = '0'  
UPDATE pt_yhmc_privilege SET IsBasic = '0'
GO

--修改损坏系数      Bery        2013-12-13 15:44
UPDATE Bud_TaskResource SET LossCoefficient = '1' WHERE LossCoefficient IS NULL 
GO
ALTER TABLE Bud_TaskResource ADD CONSTRAINT DF_LossCoefficient DEFAULT ('1.0') FOR LossCoefficient 
GO

--菜单添加参数      Bery        2013-12-18 14:27
UPDATE PT_MK SET V_CDLJ = V_CDLJ + '&type=3' WHERE C_MKDM = '9907'
GO

--修改合同交底人员字段长度  Bery    2013-12-23 14:18
ALTER TABLE Con_ContractPayend ALTER COLUMN BWasPerson nvarchar(1000)
GO

--出库物资表添加目标成本分部分项Id sxk 2013-12-24 11:31
ALTER TABLE Sm_out_Stock ADD TaskId NVARCHAR(64) NULL
GO

--预算价修改为目标成本价        Bery    2013-12-26 10:18
UPDATE Res_PriceType SET PriceTypeName = '目标成本价', Note = '目标成本价(预算价)，不能删除'
WHERE PriceTypeId = '192340F1-08E3-4B32-B65D-75E785062D05'
GO

--需求计划物资表添加目标成本分部分项Id sxk 2013-12-26 16:47
ALTER TABLE Sm_Wantplan_Stock ADD TaskId NVARCHAR(64) NULL
GO

--退库物资表分部分项Id sxk 2013-12-31 09:47
ALTER TABLE Sm_back_Stock ADD TaskId NVARCHAR(64) NULL DEFAULT ''
GO
