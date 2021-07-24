
---市场和投(竞)标信息清除
DELETE FROM [dbo].[Ent_Ept_CheckInfo]
DELETE FROM [dbo].[Ent_Ept_Equipments]
DELETE FROM [dbo].[Ent_Ept_Maintain]
DELETE FROM [Ent_Ept_MaintainFittings]
DELETE FROM dbo.Ent_Ept_Plan
DELETE FROM dbo.Ent_Ept_PlanDetail
DELETE FROM dbo.Ent_Quality_File
DELETE FROM dbo.Ent_Quality_Goal
DELETE FROM dbo.Ent_Quality_Model
DELETE FROM dbo.Ent_Safty_Measure
DELETE FROM dbo.EPM_Bid_BidCorp
DELETE FROM dbo.EPM_Bid_BidList
DELETE FROM dbo.EPM_Bid_InviteBid
DELETE FROM dbo.EPM_Bid_OpenBid
IF OBJECT_ID('EPM_Book_ConstructTask') IS NOT NULL
	DELETE FROM dbo.EPM_Book_ConstructTask
IF OBJECT_ID('EPM_Book_Resource') IS NOT NULL
	DELETE FROM dbo.EPM_Book_Resource
----合同
DELETE FROM dbo.EPM_Con_ContractAskPay
DELETE FROM dbo.EPM_Con_ContractCount
DELETE FROM dbo.EPM_Con_ContractCountDetail
DELETE FROM dbo.EPM_Con_ContractMain
DELETE FROM dbo.EPM_Con_ContractStockBill
DELETE FROM dbo.EPM_Con_ContractTask
DELETE FROM dbo.EPM_Con_ContractUpdate
DELETE FROM dbo.EPM_Con_UpdateTaskList
DELETE FROM dbo.EPM_Con_ContractStockBill
DELETE FROM dbo.EPM_Con_ContractTask
DELETE FROM dbo.EPM_Con_ContractUpdate
DELETE FROM dbo.EPM_Con_UpdateTaskList
----成本
DELETE FROM dbo.EPM_Cost_Cbs
DELETE FROM dbo.EPM_Cost_Cbs_HypotaxisTable
DELETE FROM dbo.EPM_Cost_MonthCostApprove
/*----dbo.EPM_CostApprove_CBS_Analyze
----dbo.EPM_CostApprove_CBS_BaseNode*/
DELETE FROM dbo.EPM_CostApprove_CBS_Fee
----流水日记帐
DELETE FROM dbo.EPM_CostImport
DELETE FROM dbo.EPM_CostImportChild
----质量安全
--DELETE FROM dbo.EPM_Datum_Affair
--/*----dbo.EPM_Datum_Class*/
--DELETE FROM dbo.EPM_Datum_Criterion
--DELETE FROM dbo.EPM_Datum_Data
----收支
DELETE FROM dbo.EPM_Fund_Application
DELETE FROM dbo.EPM_Fund_ContractPay

DELETE FROM dbo.EPM_Prj_DocumentBase
DELETE FROM dbo.EPM_Prj_Forepart

DELETE FROM dbo.EPM_ProjectCost_MonthProductValue
DELETE FROM dbo.EPM_ProjectCost_MonthProductValueDetail
----企业资源库
/*----dbo.EPM_Res_Category
----dbo.EPM_Res_PriceItem
----dbo.EPM_Res_Unit*/
--DELETE FROM dbo.EPM_Res_Gauge
--DELETE FROM dbo.EPM_Res_PriceRelations
--DELETE FROM dbo.EPM_Res_Resource
--DELETE FROM dbo.EPM_Res_TempResource

DELETE FROM dbo.EPM_Sch_TempSchedule
----物资
DELETE FROM dbo.EPM_Stuff_ApplyBill
DELETE FROM dbo.EPM_Stuff_ApplyDetails
DELETE FROM dbo.EPM_Stuff_ApplyDetails_Temp
DELETE FROM dbo.EPM_Stuff_Attemper
DELETE FROM dbo.EPM_Stuff_AttemperDetail
DELETE FROM dbo.EPM_Stuff_BackBill
DELETE FROM dbo.EPM_Stuff_BackDetails
DELETE FROM dbo.EPM_Stuff_MaterialIn
DELETE FROM dbo.EPM_Stuff_MaterialInDetial
DELETE FROM dbo.EPM_Stuff_MaterialOut
DELETE FROM dbo.EPM_Stuff_MaterialOutDetial
DELETE FROM dbo.EPM_Stuff_PartyABill
DELETE FROM dbo.EPM_Stuff_PartyADetail
DELETE FROM dbo.EPM_Stuff_Stock
DELETE FROM dbo.EPM_Stuff_StockCheck_Detail
DELETE FROM dbo.EPM_Stuff_StockCheck_Main
DELETE FROM dbo.EPM_Stuff_SupplyPlan_Detail
DELETE FROM dbo.EPM_Stuff_SupplyPlan_Main
----任务
IF OBJECT_ID('EPM_Task_Resource') IS NOT NULL
	DELETE FROM dbo.EPM_Task_Resource
IF OBJECT_ID('EPM_Task_TaskList') IS NOT NULL
	DELETE FROM dbo.EPM_Task_TaskList
DELETE FROM dbo.EPM_Task_TaskRelation
----日志提醒
DELETE FROM dbo.OA_Calendar_Info
DELETE FROM dbo.OA_Calendar_RemindSet
DELETE FROM dbo.OA_Document_ReceiveFile
DELETE FROM dbo.OA_Document_SendFile
DELETE FROM dbo.OA_Document_SignIn
DELETE FROM dbo.OA_Document_Templet
----档案资料
DELETE FROM dbo.OA_eFile_Info
DELETE FROM dbo.OA_eFile_Lend
DELETE FROM dbo.OA_File_Catalog
DELETE FROM dbo.OA_File_Classes
DELETE FROM dbo.OA_File_Destroy
DELETE FROM dbo.OA_File_DestroyMain

DELETE FROM dbo.OA_File_Lend
DELETE FROM dbo.OA_File_Library
----企业制度
DELETE FROM dbo.OA_System_Info
DELETE FROM InstitutionClass
DELETE FROM Institutions

----投票
DELETE FROM dbo.OA_Voting_Info
DELETE FROM dbo.OA_Voting_Option
DELETE FROM dbo.OA_Voting_Record

DELETE FROM dbo.prj_AlertMessage
DELETE FROM dbo.prj_AlertMessage_Contract
DELETE FROM dbo.Prj_Costsubjects
/*企业技术标准
----dbo.Prj_EnterpriseTechnologyCriterion
dbo.Prj_FilesClass*********/
DELETE FROM dbo.Prj_ExpertSchemeManage
DELETE FROM dbo.Prj_Files
DELETE FROM dbo.Prj_IncomeDevotionPlan
DELETE FROM dbo.Prj_IncomeDevotionPlanChild
----项目检查
DELETE FROM dbo.Prj_ItemInspect
----DELETE FROM dbo.Prj_ItemInspectSort
----dbo.Prj_ItemInspectSort
DELETE FROM dbo.Prj_ItemProg
DELETE FROM dbo.Prj_ItemSupervise
----dbo.prj_presentiment
DELETE FROM dbo.Prj_ProgressPlan
DELETE FROM dbo.Prj_ProgressPlan_Appraise
DELETE FROM dbo.Prj_ProgressPlan_Child
----奖罚类型
---- dbo.Prj_ProgSort
----dbo.prj_prsentiment_options
DELETE FROM dbo.Prj_Report
DELETE FROM dbo.Prj_ScienceEmpolderDevotion
DELETE FROM dbo.Prj_ScienceEmpolderDevotionChild
DELETE FROM dbo.Prj_Summary
DELETE FROM dbo.Prj_TechnologyAdvancementIncome
DELETE FROM dbo.Prj_TechnologyConstructOrganize
DELETE FROM dbo.Prj_TechnologyCriterion
DELETE FROM dbo.Prj_TechnologyManage
DELETE FROM dbo.Prj_TechnologyProposal
DELETE FROM dbo.Prj_TechnologyTell
/*----代办dbo.prj_WaitingJob*/
---审核意见
DELETE FROM dbo.PT_AduitMsg
----公告
DELETE FROM dbo.PT_BULLETIN_MAIN
DELETE FROM dbo.PT_BULLETIN_ANNEX
DELETE FROM dbo.PT_BULLETIN_TOSYS
DELETE FROM dbo.PT_CS
DELETE FROM dbo.PT_DZYJ_USERSET
DELETE FROM dbo.PT_PAL_LIST
DELETE FROM dbo.PT_TXL_GRFZLX
DELETE FROM dbo.PT_TXL_GRTXL
DELETE FROM dbo.PT_TXL_WBGS
DELETE FROM dbo.PT_TXL_WBTXL
DELETE FROM dbo.TMP_ListSource
DELETE FROM dbo.TMP_TaskList
DELETE FROM dbo.Web_News
DELETE FROM dbo.XPM_Basic_AnnexList
DELETE FROM dbo.XPM_Basic_ContactCorp
---审核
DELETE FROM dbo.WF_FlowChart
DELETE FROM dbo.WF_Instance
DELETE FROM dbo.WF_Instance_Main
DELETE FROM dbo.WF_JoinBusiness
DELETE FROM dbo.WF_Message
DELETE FROM dbo.WF_NodeCondition
--DELETE FROM dbo.wf_PrjUser
DELETE FROM dbo.WF_RecieveMsg
DELETE FROM dbo.WF_Role
DELETE FROM dbo.WF_RoleProject
--DELETE FROM dbo.WF_RoleUser
DELETE FROM dbo.WF_RoleUsers
DELETE FROM dbo.WF_SecondUser
DELETE FROM dbo.WF_Template
DELETE FROM dbo.WF_TemplateNode

--项目信息表

delete FROM dbo.PT_PrjInfo where prjcode='200905'
delete FROM dbo.PT_PrjInfo where prjcode='001'
delete FROM dbo.PT_PrjInfo where prjcode='002'

---清空资源信息
---Delete from EPM_Res_Unit

--delete from EPM_Res_Category where CategoryName not in 
--(select CategoryName from EPM_Res_Category where CategoryName='人工' or CategoryName='材料' or CategoryName='机械')
--
--
--delete from EPM_Res_Gauge
--delete from EPM_Res_PriceItem
--delete from EPM_Res_PriceRelations
--delete from EPM_Res_Resource
--delete from EPM_Res_TempResource
---清空往来单位信息
--delete from XPM_Basic_CodeList
delete from XPM_Basic_ContactCorp


---删除质量文档 安全文档
delete From EPM_Datum_Criterion
delete from Prj_EnterpriseTechnologyCriterion
---清空联系人
delete from dbo.PT_TXL_NBTXL
---清空成本结构CBS
delete from EPM_CostApprove_CBS_BaseNode 
delete from dbo.EPM_CostApprove_CBS_Analyze              

--清空投标和项目
BEGIN TRY
	BEGIN TRAN
		DELETE FROM dbo.PT_PrjInfo_Kind
		DELETE FROM dbo.PT_PrjInfo_Rank        
		DELETE FROM dbo.PT_Prj_Completed_Annex
		DELETE FROM dbo.PT_Prj_Completed_Detail
		--DELETE FROM dbo.PT_Prj_Completed          不应该清空
		DELETE FROM dbo.PT_PrjInfo_ZTB_Detail
		DELETE FROM dbo.PT_PrjInfo_ZTB_User
		DELETE FROM dbo.PT_PrjMember
		DELETE FROM dbo.PT_PrjInfo_EngineeringType
		DELETE FROM dbo.PT_PrjInfo
		DELETE FROM dbo.PT_PrjInfo_ZTB_Stock
		DELETE FROM dbo.PT_PrjInfo_ZTB
	COMMIT TRAN
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	PRINT ERROR_MESSAGE()
END CATCH
GO


--清空销假信息 休假信息
delete from HR_Leave_Application
delete from dbo.HR_Leave_Stat

----删除除精科以外的工作流和流节点
delete from dbo.WF_Template where TemplateName not like'%精科%'
delete from dbo.WF_TemplateNode where TemplateID in
(select TemplateID from dbo.WF_Template where TemplateName not like '%精科%')


--好友分组
DELETE FROM EMS_MaileGroup

--隐藏编码库里的仓库名称
UPDATE XPM_Basic_CodeType SET IsVisible = 0 WHERE TypeID = 21


--资料管理
--清空数据
delete from dbo.F_FileInfo
--DELETE 语句与 REFERENCE 约束"FK__PT_Prj_Co__Annex__33C13DAD"冲突。该冲突发生于数据库"pm2_bery"，表"dbo.PT_Prj_Completed_Annex", column 'AnnexAddress'。
delete from dbo.F_PersonalFile
delete from dbo.F_FileType

DELETE FROM PT_Warning		-- 删除提醒
DELETE FROM Basic_Privilege	-- 删除权限

--文件类型信息 2010-9-9 李真
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'文件夹','/images/tree/FileInfo/folder.gif','.folder')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'word文档','/images/tree/FileInfo/word.gif','.doc')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'Excel文档','/images/tree/FileInfo/excel.gif','.xls')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'PPT文档','/images/tree/FileInfo/ppt.gif','.ppt')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'access文档','/images/tree/FileInfo/access.gif','.mdb')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'文本文档','/images/tree/FileInfo/note.gif','.txt')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'pdf阅读器','/images/tree/FileInfo/pdf.gif','.pdf')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'程序文件','/images/tree/FileInfo/exe.gif','.exe')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'html文件','/images/tree/FileInfo/html.gif','.html')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'影音文件','/images/tree/FileInfo/media.gif','.rmvb')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'压缩文件','/images/tree/FileInfo/zip.gif','.rar')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'画图文件','/images/tree/FileInfo/bmp.gif','.bmp')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'flash文件','/images/tree/FileInfo/flash.png','.swf')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'php文件','/images/tree/FileInfo/php.png','.php')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'Excel文档','/images/tree/FileInfo/xlsx.png','.xlsx')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'word文档','/images/tree/FileInfo/docx.png','.docx')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'PPT文档','/images/tree/FileInfo/pptx.png','.pptx')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'jpg图片','/images/tree/FileInfo/jpg.png','.jpg')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'gif图片','/images/tree/FileInfo/gif.png','.gif')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'png图片','/images/tree/FileInfo/png.png','.png')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'其他','/images/tree/FileInfo/other.gif','.other')


--公共文件顶级目录
--违反了 PRIMARY KEY 约束 'PK_F_FILEINFO'。不能在对象 'dbo.F_FileInfo' 中插入重复键。
insert into dbo.F_FileInfo(id,FileName,FileSize,FileOwner,ParentId,FileType,UserCodes,CreateTime)
                     values('D3140694-0545-4657-BE60-4DD3C6945B63','目录','','00000000','D3140694-0545-4657-BE60-4DD3C6945B63','2','00000000',getDate())
                









