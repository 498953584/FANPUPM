--整理预算数据      Bery    2014-01-03 14:43
WITH cttBudTC AS
(
	SELECT ROW_NUMBER() over(partition BY PrjId ORDER BY Version DESC) AS rn, *
	FROM Bud_TaskChange
)
DELETE FROM cttBudTC WHERE rn > 1
GO
UPDATE Bud_TaskChange SET Version = 1 WHERE Version != 1
GO


--修改损耗系数      Bery    2014-01-07 10:21
ALTER TABLE Bud_ModifyTaskRes DROP COLUMN LossCoefficient
GO
ALTER TABLE Bud_ModifyTaskRes ADD LossCoefficient decimal(18,3) DEFAULT(1.0) NOT NULL
GO


--预算表中添加字段Total2，汇总节点资源配置的合计        Bery    2014-01-08 17:15
ALTER TABLE Bud_Task ADD Total2 decimal(18, 3) DEFAULT(0.0)
GO

--预算变更表中添加字段Total2，汇总节点资源配置的合计        Bery    2014-01-08 17:15
ALTER TABLE Bud_ModifyTask ADD Total2 decimal(18, 3) DEFAULT(0.0)
GO

--预算变更表中添加项目Id（PrjId）           Bery    2014-01-08 17:15
ALTER TABLE Bud_ModifyTask ADD PrjId2 nvarchar(200) 
GO

--处理遗留数据，预算变更表中添加项目Id（PrjId）     Bery    2014-01-08 17:15
UPDATE Bud_ModifyTask SET PrjId2 = Bud_Modify.PrjId
FROM Bud_ModifyTask, Bud_Modify
WHERE Bud_Modify.ModifyId = Bud_ModifyTask.ModifyId
GO

--预算变更表中添加上级任务Id（ParentId）        Bery    2014-01-08 17:15
ALTER TABLE Bud_ModifyTask ADD ParentId nvarchar(200) 
GO
--处理遗留数据, 预算变更表中添加上级任务Id（ParentId）
--清单外变更，要要变更的TaskId既为新节点的父节点
UPDATE Bud_ModifyTask SET ParentId = TaskId WHERE ModifyType = '0'
GO
--清单外变更
UPDATE Bud_ModifyTask SET ParentId = Bud_Task.ParentId
FROM Bud_ModifyTask, Bud_Task
WHERE Bud_ModifyTask.TaskId = Bud_Task.TaskId AND ModifyType = '1'		--清单外变更
GO


--添加配置信息          Bery    2014-01-08 17:58
INSERT INTO Basic_Config VALUES('46662919-D363-46FA-B6CC-3BFC75794DA2', 'IsClearTaskTotal2', '0', '是否处理过Total2')
GO

--添加资源差价表菜单 sxk 2014-01-11 14:39
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '290724')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
VALUES('290724','资源价差表','/StockManage/ProjectFrame.aspx?path=rptResPriceDiff','y',24,'',25015,0,'0','0','','1')
GO


--修改产值上班审核到合同计量        Bery    2014-01-21 10:54
UPDATE WF_BusinessCode SET C_MKDM = '05', BusinessName = '合同计量' WHERE BusinessCode = '133' 
GO
UPDATE WF_Business_Class SET BusinessClassName = '合同计量' WHERE BusinessCode = '133'
GO

--间接成本日记账  添加间接成本编号        gmd     2014-01-21 10:00
IF NOT EXISTS(SELECT * FROM information_schema.columns
	WHERE table_name = 'Bud_IndirectDiaryCost' AND column_name = 'IndireCode')
ALTER TABLE Bud_IndirectDiaryCost ADD IndireCode nvarchar(200)
GO

--间接成本明细记账  添加间接成本明细编号  gmd     2014-01-21 10:00
IF NOT EXISTS(SELECT * FROM information_schema.columns
    WHERE table_name='Bud_IndirectDiaryDetails' AND column_name='IndetailsCode')
ALTER TABLE Bud_IndirectDiaryDetails ADD IndetailsCode	nvarchar(200)
GO

--组织机构间接成本记账 添加组织机构编号   gmd     2014-01-21 10:00
IF NOT EXISTS (SELECT * FROM information_schema.columns
    WHERE table_name='Bud_OrgDiaryCost' AND column_name='OrgdiaryCode')
ALTER TABLE Bud_OrgDiaryCost ADD OrgdiaryCode nvarchar(200)
GO

--组织机构间接成本明细记账  添加组织机构编号  gmd     2014-01-21 10:00
IF NOT EXISTS (SELECT * FROM information_schema.columns
    WHERE table_name='Bud_OrgDiaryDetails' AND column_name='OrgdetailsCode')
ALTER TABLE Bud_OrgDiaryDetails ADD OrgdetailsCode nvarchar(200)
GO


--修改字段长度      Bery        2014-01-23 10:28
ALTER TABLE PT_PrjInfo_ZTB_Detail ALTER COLUMN OwnerLinkMan nvarchar(200)
ALTER TABLE PT_PrjInfo_ZTB_Detail ALTER COLUMN OwnerLinkPhone nvarchar(200)
GO

--与SAP对接，SAP回传的数据表    gmd   2014-01-23 15:14
IF OBJECT_ID('MiddleTable', 'U') IS NOT NULL
    DROP TABLE MiddleTable
GO
CREATE TABLE MiddleTable(
	PrjCode        nvarchar(48),   --项目编码
    ContractCode   nvarchar(64),   --合同编号
	SerialNumber   nvarchar(64),   --流水号
	Belnr          nvarchar(30),   --会计凭证编号
	MoneyAmount    decimal(18,3), --凭证货币金额
	Tag            nvarchar(3),    --凭证类型（A=发票，B=报销，C=付款，D=收款，E=借款，F=销帐）
	Hkont          nvarchar(200),   --科目（总分类帐帐目）
	Flag           bit ,          --是否已处理
	Buzet          int,           --序列号
	F_date         datetime,      --SAP数据回传时间
	Txt            nvarchar(4000)  --文本
)
GO


--删除无效的存储过程        Bery        2014-02-10 09:13 
IF OBJECT_ID('usp_BudTaskAndModify') IS NOT NULL
	DROP PROC usp_BudTaskAndModify
GO


--添加SignCode              Bery        2014-02-10 10:27
ALTER TABLE XPM_Basic_CodeList ADD SignCode2 nvarchar(200)
GO
UPDATE XPM_Basic_CodeList SET SignCode2 = XPM_Basic_CodeType.SignCode
FROM XPM_Basic_CodeList, XPM_Basic_CodeType
WHERE XPM_Basic_CodeList.TypeID = XPM_Basic_CodeType.TypeID
GO


--预算变更添加合同Id字段    Bery    2014-02-10 15:00
ALTER TABLE Bud_ModifyTask ADD ContractId nvarchar(200)
GO


--合同变更表中添加预算变更Id    Bery    2014-02-12 14:44
ALTER TABLE Con_Payout_Modify ADD BudModifyId nvarchar(200)
GO


--添加菜单“合同预算变更工程量清单”        Bery        2014-02-14 08:55
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('050612','合同预算变更工程量清单','/StockManage/ProjectFrame.aspx?path=BudgetCompletedList&prjId=','y','12','','25016','0','0','0','','1'); 
GO

--添加周工作计划录入时间           gmd             2014-02-14  16:50
IF NOT EXISTS (SELECT * FROM information_schema.columns 
   WHERE table_name='pm_workplan_weekplan' AND column_name='InputTime')
ALTER TABLE pm_workplan_weekplan ADD InputTime datetime
GO

--添加菜单“周计划汇总报表”      gmd             2014-02-14  16:51 
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
VALUES('282103','周计划汇总报表','/oa/WorkPlanAndSummary/WorkPlanAllList.aspx','y',5,'',25017,0,'0','0','','1')
GO


--合同预算变更节点表中添加PrjId2        Bery        2014-02-17 11:02
ALTER TABLE Bud_ConModifyTask ADD PrjId2 nvarchar(200)
GO
--处理遗留数据
UPDATE Bud_ConModifyTask SET PrjId2 = Bud_ConModify.PrjId
FROM Bud_ConModifyTask, Bud_ConModify
WHERE Bud_ConModify.ModifyId = Bud_ConModifyTask.ModifyId
GO

--合同预算变更表中添加ParentId          Bery        2014-02-17 11:02
ALTER TABLE Bud_ConModifyTask ADD ParentId nvarchar(200)
GO
--处理遗留数据
--清单外变更，要要变更的TaskId既为新节点的父节点
UPDATE Bud_ConModifyTask SET ParentId = TaskId WHERE ModifyType = '0'
GO
--清单内变更
UPDATE Bud_ConModifyTask SET ParentId = Bud_ContractTask.ParentId
FROM Bud_ConModifyTask, Bud_ContractTask
WHERE Bud_ConModifyTask.TaskId = Bud_ContractTask.TaskId AND ModifyType = '1'
GO


--添加菜单"合同预算月完成预算统计表"              lxb         2014-02-19 8:59
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
VALUES('050613','合同预算月完成预算统计表','/StockManage/ProjectFrame.aspx?path=BMonthCompletedStatistics&prjId=','y',13,'',25018,'0','0','0','','1')
GO


--施工保留表添加字段合同Id（合同计量）              Bery        2014-02-19 15:36
ALTER TABLE Bud_ConsReport ADD ConstractId nvarchar(100)
GO

--添加菜单"目标成本变更工程量清单"              lxb         2014-02-25 8:54
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
VALUES('050614','目标成本变更工程量清单','/StockManage/ProjectFrame.aspx?path=BTaskReport&prjId=','y',14,'',25020,'0','0','0','','1')
GO
--间接费用汇总表菜单                               gmd          2014-02-28  9:55
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
 VALUES('290405','间接费用汇总表','/BudgetManage/Cost/CostIndirectOrgList.aspx','y',5,'',25021,0,'0','0','','1')
 GO

--PC_PettyCash添加字段          Bery        2014-03-10 14:12
--添加还款金额
ALTER TABLE PC_PettyCash ADD RepayCash decimal(18,3) NOT NULL DEFAULT(0.0)
GO
--添加还款审核状态
ALTER TABLE PC_PettyCash ADD RepayFlowState int NOT NULL DEFAULT(-1)
GO
--添加备用金状态
ALTER TABLE PC_PettyCash ADD State char(1) NOT NULL DEFAULT('0')
GO
--添加清理日期
ALTER TABLE PC_PettyCash ADD CleanDate datetime NOT NULL DEFAULT(GETDATE())
GO
--添加清理备注
ALTER TABLE PC_PettyCash ADD CleanNote nvarchar(max)
GO


--项目预报销添加字段            Bery        2014-03-13 17:48
--添加备用金Id
ALTER TABLE Bud_IndirectDiaryCost ADD PettyCashId nvarchar(200)
GO

--添加经手人
ALTER TABLE	Bud_IndirectDiaryCost ADD InssuedByCode varchar(8) 
GO
--添加填报人
ALTER TABLE	Bud_IndirectDiaryCost ADD InputUserCode varchar(8) 
GO
--添加类型
ALTER TABLE	Bud_IndirectDiaryCost ADD Type char(1) NOT NULL DEFAULT('0')
GO
--添加是否核销
ALTER TABLE	Bud_IndirectDiaryCost ADD IsAudit bit NOT NULL DEFAULT(0)
GO
--添加核销时间
ALTER TABLE	Bud_IndirectDiaryCost ADD AuditDate datetime NOT NULL DEFAULT(GETDATE())
GO
--添加备用金Id
ALTER TABLE Bud_IndirectDiaryDetails ADD PettyCashId nvarchar(200)
GO
--核销金额
ALTER TABLE Bud_IndirectDiaryDetails ADD AuditAmount decimal(18,3) NOT NULL DEFAULT(0.0)
GO


--区分项目和组织机构                    Bery        2014-03-14 10:07
ALTER TABLE Bud_IndirectDiaryCost ADD CostType char(1) NOT NULL DEFAULT 'P'
GO

--预报销添加录入时间                Bery        2014-03-19 13:58
ALTER TABLE Bud_IndirectDiaryCost ADD InputDate2 datetime NOT NULL DEFAULT(GETDATE())
GO
UPDATE Bud_IndirectDiaryCost SET InputDate2 = InputDate
GO


--序号                              Bery        2014-03-20 08:58
ALTER TABLE Bud_IndirectDiaryDetails ADD No int NOT NULL DEFAULT(0)
GO


--处理老数据，组织机构预报销。      Bery        2014-03-24 10:13
INSERT INTO Bud_IndirectDiaryCost(InDiaryId, ProjectId, Name, Department, IssuedBy, FlowState, InputUser, InputDate, 
	IndireCode, Type, IsAudit, CostType, InputDate2)
SELECT OrgDiaryId, OrgId, Name, Department, IssuedBy, FlowState,InputUser, InputDate, OrgdiaryCode, 
	 '0', 0, 'O', InputDate
FROM Bud_OrgDiaryCost
WHERE OrgDiaryId NOT IN ( SELECT InDiaryId FROM Bud_IndirectDiaryCost)
GO

INSERT INTO Bud_IndirectDiaryDetails(InDiaryDetailsId, InDiaryId, Name, Amount, CBSCode, Note, IndetailsCode)
SELECT OrgDiaryDetailsId, OrgDiaryId, Name, Amount, CBSCode, Note, OrgdetailsCode
FROM Bud_OrgDiaryDetails
WHERE OrgDiaryDetailsId NOT IN ( SELECT InDiaryDetailsId FROM Bud_IndirectDiaryDetails)
GO



--修改预报销模块名称                Bery        2014-03-24 16:25
UPDATE PT_MK SET V_MKMC = '预报销' WHERE C_MKDM = '2904'
GO

--处理遗留数据                      Bery        2014-03-25 13:58
UPDATE Bud_IndirectDiaryCost SET InputUserCode = (SELECT TOP(1) v_yhdm FROM PT_YHMC WHERE v_xm = InputUser)
WHERE InputUserCode IS NULL
GO
UPDATE Bud_IndirectDiaryCost SET InssuedByCode = (SELECT TOP(1) v_yhdm FROM PT_YHMC WHERE v_xm = IssuedBy)
WHERE InssuedByCode IS NULL
GO

--添加核销菜单                      Bery        2014-03-25 14:02
INSERT INTO PT_MK (C_MKDM, V_MKMC, V_CDLJ, C_BS, I_XH, V_IMG, I_ID, i_ChildNum, IsBasic, IsMaintainable, helppath, Isdisplay) VALUES ('290406', '核销', '/BudgetManage/Cost/CostVerifyList.aspx', 'y', '7', '', '25022', '0', '0', '0', '', '1'); 
GO
--添加SAP物质中间表                gmd          2014-03-25 16:30
IF OBJECT_ID('Sap_JWSmStock', 'U') IS NOT NULL
    DROP TABLE Sap_JWSmStock

CREATE TABLE Sap_JWSmStock
(
  PrjCode nvarchar(48),      --项目编号
  Tcode nvarchar(512),       --仓库编号
  Scode nvarchar(50),        --物质编号
  Snumber decimal(18,3),     --数量
  Sprice decimal(18,3),      --单价
  InputDate datetime,        --录入时间
  Flag bit                   --是否已处理
)
GO
--删除库房库存表中Type的check约束   gmd         2014-03-25 16:30
ALTER TABLE Sm_Treasury_Stock
DROP CONSTRAINT [C_Type]
GO
--添加库房库存表中Type的check约束   gmd          2014-03-25 16:30
ALTER TABLE Sm_Treasury_Stock 
 ADD  CONSTRAINT [C_Type] CHECK  (([Type]='T' OR [Type]='B' 
OR [Type]='A' OR [Type]='F' OR [Type]='S' OR [Type]='I' OR [Type]='O'))
GO


--添加备用金还款审核            Bery        2014-03-26 11:46
INSERT INTO WF_BusinessCode VALUES('167', '备用金还款', 'Id', 'PC_PettyCash', 'Id', 'RepayFlowState', '/PettyCash/PettyCashRepayDetails.aspx', NULL, '21','PrjTypeCode', '(SELECT ''查看'')' )
GO
INSERT INTO WF_Business_Class(BusinessCode, BusinessClass, BusinessClassName, Id) VALUES('167', '001', '备用金还款审核', '91797465-54C5-453A-AAE1-6D34A7BB8752')
GO


--工作总结添加是否有效字段      wdd         2014-03-27 16:46
ALTER TABLE Pm_WorkPlan_PlanSummary ADD IsValid bit NOT NULL DEFAULT(0)
GO


--添加字段是否还款              Bery        2014-03-28 13:19
IF NOT EXISTS(SELECT * FROM information_schema.columns WHERE table_name = 'PC_PettyCash' AND column_name = 'IsRepay')
	ALTER TABLE PC_PettyCash ADD IsRepay bit NOT NULL DEFAULT(0)
GO


--删除外键约束
IF EXISTS (SELECT * FROM information_schema.TABLE_CONSTRAINTS  WHERE table_name = 'Bud_IndirectDiaryCost' AND CONSTRAINT_NAME = 'FK_PCPettyCash_Id')
	ALTER TABLE Bud_IndirectDiaryCost DROP FK_PCPettyCash_Id
GO


--添加备用金还款菜单            Bery        2014-03-28 16:45
INSERT INTO PT_MK (C_MKDM, V_MKMC, V_CDLJ, C_BS, I_XH, V_IMG, I_ID, i_ChildNum, IsBasic, IsMaintainable, helppath, Isdisplay) VALUES ('2105', '备用金还款', '/PettyCash/PettyCashRepayList.aspx', 'y', '5', '', '25023', '0', '0', '0', '', '1'); 
GO


--调整审核          Bery            2014-04-02 10:02
UPDATE WF_BusinessCode SET KeyWord = 'InDiaryId', LinkTable = 'Bud_IndirectDiaryCost', PrimaryField = 'InDiaryId',
	DoWithUrl = '/BudgetManage/Cost/CostVerifyRecord.aspx'
WHERE BusinessCode = '088'
GO


--支出合同计量添加计算Id        Bery        2014-04-03 16:49
ALTER TABLE Bud_ConsReport ADD BalanceId nvarchar(200)
GO

--添加备用金清理菜单            Bery        2014-04-04 16:10
INSERT INTO PT_MK (C_MKDM, V_MKMC, V_CDLJ, C_BS, I_XH, V_IMG, I_ID, i_ChildNum, IsBasic, IsMaintainable, helppath, Isdisplay) VALUES ('2106', '备用金清理', '/PettyCash/PettyCashClearList.aspx', 'y', '6', '', '25024', '0', '0', '0', '', '1'); 
GO


--支出合同计量添加字段          Bery        2014-04-08 13:39
ALTER TABLE Bud_ConsReport ADD Type nvarchar(2) NOT NULL DEFAULT(0)
GO


--图纸自会审添加审核            Bery        2014-04-17 13:39
IF NOT EXISTS(SELECT * FROM WF_BusinessCode WHERE BusinessCode = '116')
    INSERT INTO WF_BusinessCode (BusinessCode, BusinessName, KeyWord, LinkTable, PrimaryField, 
        StateField, DoWithUrl, LookUrl, C_MKDM, ProjectField, NameField) 
    VALUES ('116', '图纸审批', 'TechGuid', 'Prj_TechnologyManage', 'TechGuid', 'FlowState', 
        '/EPC/17/Ppm/ScienceInnovate/MeasureDataEdit.aspx?Type=View&bs=2&sm=10', 
        'None', '91', 'PrjCode', 'ItemName'); 
GO

IF NOT EXISTS(SELECT * FROM WF_Business_Class WHERE BusinessCode = '116')
	INSERT INTO WF_Business_Class VALUES('116', '001', '图纸自会审批','5FD4D710-D965-4D51-A711-6E4E56BA7DF3')
GO


--支出合同添加签订人            Bery        2014-04-21 09:13
ALTER TABLE Con_Payout_Contract ADD SignPerson varchar(8)
GO


--修改清单外变更明细TaskId      Wdd         2014-04-28 13:14
UPDATE Bud_ModifyTask SET TaskId=TaskId+'-'+OrderNumber WHERE ModifyType=0
GO


---修改合同计量业务分类名称   Ldd     2014-04-29  15:42
UPDATE WF_Business_Class SET BusinessClassName = '合同计量' WHERE BusinessCode = '123' and businessclass='001'
GO
UPDATE WF_BusinessCode SET BusinessName = '合同计量' WHERE BusinessCode = '123' 
GO


--禁用菜单 “合同预算变更”和“预算变更”       Bery        2014-05-09 15:06
UPDATE PT_MK SET IsDisplay = '0' WHERE C_MKDM = '130103'
GO
UPDATE PT_MK SET IsDisplay = '0' WHERE C_MKDM = '130705'
GO


--全局流水号        Bery            2014-05-13 17:07
CREATE TABLE Basic_SerialNumber
(
	No nvarchar(20) PRIMARY KEY,
	TableName nvarchar(30) NOT NULL,
	KeyValue nvarchar(200) NOT NULL,
	InTime datetime NOT NULL DEFAULT(GETDATE())
)
GO


----备用金申请查看页面添加Payee 字段    Ldd   2014-05-15 16:34

ALTER TABLE PC_PettyCash Add Payee nvarchar(200)
GO
