
--------------------------------------------------------------------------
--修改预警表的字段类型长度
ALTER TABLE PT_DBSJ ALTER COLUMN V_DBLJ NVARCHAR(200)


--------------------------------------------------------------------------初始化基本设置
DELETE FROM Con_Config
GO
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsPayoutAlarm','0',N'是否付款提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'PayoutAlarmDays','30',N'付款提醒天数')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsIncomeAlarm','0',N'是否回款提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IncomeAlarmDays','30',N'回款提醒天数')

INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsPaymentAlarm','0',N'是否支付提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsHighPayAlarm','0',N'是否高支付提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'HighPayAlarmLimit','30',N'高支付提醒限额')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsMidPayAlarm','0',N'是否中支付提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'MidPayAlarmLowerLimit','30',N'中支付提醒金额下限比例')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'MidPayAlarmUpperLimit','10',N'中支付提醒金额上限比例')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsLowPayAlarm','0',N'是否低支付提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'LowPayAlarmUpperLimit','10',N'低支付提醒金额上限比例')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'LowPayAlarmLowerLimit','0',N'低支付提醒金额下限比例')

INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsBalanceAlarm','0',N'是否结算提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsHighBalanceAlarm','0',N'是否高结算提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'HighBalanceAlarmLimit','30',N'高结算提醒限额')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsMidBalanceAlarm','0',N'是否中结算提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'MidBalanceAlarmUpperLimit','30',N'中结算提醒金额上限比例')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'MidBalanceAlarmLowerLimit','10',N'中结算提醒金额下限比例')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'IsLowBalanceAlarm','0',N'是否低结算提醒，0表示不提醒，1表示提醒')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'LowBalanceAlarmUpperLimit','10',N'低结算提醒金额上限比例')
INSERT INTO Con_Config(ID,ParaName,ParaValue,Notes) 
	VALUES(NEWID(),'LowBalanceAlarmLowerLimit','0',N'低结算提醒金额下限比例')

	
	
---------------------------------------------------------------------------审核流程
--支出合同审核流程  Bery 2010-8-25
insert dbo.WF_BusinessCode values('081','支出合同','ContractID','Con_Payout_Contract','ContractID','FlowState','/ContractManage/PayoutContract/PayoutContractEdit.aspx',null,'05')
insert dbo.WF_Business_Class values('081','001','支出合同审核')

--支出合同变更审核流程  Bery 2010-8-30
insert dbo.WF_BusinessCode values('082','支出合同变更','ModifyID','Con_Payout_Modify','ModifyID','FlowState','/ContractManage/PayoutModify/ModifyEdit.aspx',null,'05')
insert dbo.WF_Business_Class values('082','001','支出合同变更审核')

--支出合同结算审核流程  Bery 2010-8-30
insert dbo.WF_BusinessCode values('083','支出合同结算','BalanceID','Con_Payout_Balance','BalanceID','FlowState','/ContractManage/PayoutBalance/BalanceEdit.aspx',null,'05')
insert dbo.WF_Business_Class values('083','001','支出合同结算审核')

--支出合同付款审核流程  Bery 2010-8-30
insert dbo.WF_BusinessCode values('084','支出合同付款','ID','Con_Payout_Payment','ID','FlowState','/ContractManage/PayoutPayment/PaymentQuery.aspx',null,'05')
insert dbo.WF_Business_Class values('084','001','支出合同付款审核')

---------------------------------------------------------------------------添加附件
--支出合同添加附件 Bery 2010-8-25
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1901','PayoutContract','支出合同',8388608,'*','/UploadFiles/ContractManage/PayoutContract/',8)
--支出合同变更添加附件 Bery 2010-8-30
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1902','PayoutContractModify','支出合同变更',8388608,'*','/UploadFiles/ContractManage/PayoutContractModify/',8)
--支出合同结算添加附件 Bery 2010-8-30
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1903','PayoutContractBalance','支出合同结算',8388608,'*','/UploadFiles/ContractManage/PayoutContractBalance/',8)
--支出合同付款添加附件 Bery 2010-8-30
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1904','PayoutContractPayment','支出合同付款',8388608,'*','/UploadFiles/ContractManage/PayoutContractPayment/',8)
--'收入合同添加附件 2010-8-27 author:li
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1905','IncometContract','收入合同',8388608,'*','/UploadFiles/ContractManage/IncometContract/',8)
--'收入合同结算添加附件 2010-8-27 author:li
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1906','IncometBalance','收入合同结算',8388608,'*','/UploadFiles/ContractManage/IncometBalance/',8)
--'收入合同变更添加附件 2010-8-27 author:li
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1907','IncometModify','收入合同变更',8388608,'*','/UploadFiles/ContractManage/IncometModify/',8)
--'收入合同收款添加附件 2010-8-27 author:li
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1908','IncometPayment','收入合同收款',8388608,'*','/UploadFiles/ContractManage/IncometPayment/',8)
--'收入合同交底添加附件 2010-8-27 author:li
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1909','ContractPayend','收入合同交底',8388608,'*','/UploadFiles/ContractManage/ContractPayend/',8)
--'收入合同交底反馈添加附件 2010-8-27 author:li
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1910','PayendFeedback','收入合同交底反馈',8388608,'*','/UploadFiles/ContractManage/PayendFeedback/',8)
--支出合同发票添加附件 Bery 2010-8-30
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1911','PayoutContractInvoice','支出合同发票',8388608,'*','/UploadFiles/ContractManage/PayoutContractInvoice/',8)
--收入合同发票添加附件 Bery 2010-8-30
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1912','PayoutContractInvoice','收入合同发票',8388608,'*','/UploadFiles/ContractManage/IncometContractInvoice/',8)



--发票类型
INSERT INTO XPM_Basic_CodeType(TypeName,IsVisible,IsValid,Remark,SignCode,Owner,VersionTime,ContractCropType) VALUES('发票类型',1,1,'发票类型','InvoiceType','000000','2010-09-17 15:25:50.470','E2689CA3-AA0D-420A-BEBA-FEBF81A63E52')
INSERT INTO XPM_Basic_CodeList(CodeID,TypeID,ParentCodeID,ParentCodeList,CodeName,ChildNumber,IsFixed,IsDefault,IsValid,IsVisible,Owner,VersionTime) VALUES(1,30,0,',1,','普通发票',0,0,0,1,1,'000000','2010-09-17 15:30:46.777')
INSERT INTO XPM_Basic_CodeList(CodeID,TypeID,ParentCodeID,ParentCodeList,CodeName,ChildNumber,IsFixed,IsDefault,IsValid,IsVisible,Owner,VersionTime) VALUES(2,30,0,',2,','增值发票',0,0,0,1,1,'000000','2010-09-17 15:31:26.093')



--触发器
--支出合同变更审核通过后，修改合同最终金额
IF OBJECT_ID ('Approval', 'TR') IS NOT NULL
   DROP TRIGGER Approval;
GO
CREATE TRIGGER Approval
	ON Con_Payout_Modify 
	AFTER UPDATE
AS
DECLARE @state int
SELECT @state = FlowState FROM INSERTED
IF @state = 1
BEGIN
	UPDATE Con_Payout_Contract SET ModifiedMoney = ModifiedMoney + i.ModifyMoney
	FROM Con_Payout_Contract AS c
	JOIN INSERTED AS i ON c.ContractID = i.ContractID
END

--Bery 2010-10-29 
--支付合同付款添加是否包含待审数据
ALTER TABLE Con_Payout_Payment 
ADD ContainPending BIT DEFAULT(0);
UPDATE Con_Payout_Payment SET ContainPending = 0
