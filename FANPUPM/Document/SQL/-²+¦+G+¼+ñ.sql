--最后更新时间：2012-04-20 
--项目添加唯一键约束
IF OBJECT_ID('UQ_PrjGuid', 'UQ') IS NOT NULL
	ALTER TABLE PT_PrjInfo DROP CONSTRAINT UQ_PrjGuid
GO
ALTER TABLE PT_PrjInfo ADD CONSTRAINT UQ_PrjGuid UNIQUE(PrjGuid)
GO


--删除组织机构的触发器
IF OBJECT_ID('TR_Drop_PT_d_bm', 'TR') IS NOT NULL
	DROP TRIGGER TR_Drop_PT_d_bm
GO
CREATE TRIGGER TR_Drop_PT_d_bm ON PT_d_bm AFTER DELETE
AS
BEGIN
	DELETE Bud_OrganizationBudget
	FROM Bud_OrganizationBudget, DELETED
	WHERE OrganizationBudgetId = DELETED.i_bmdm
END
GO
---------------------------------------物资---------------------------------
--删除新物资无效表
IF OBJECT_ID('FK_SM_RESOU_PRICETYPE', 'F') IS NOT NULL
	ALTER TABLE Sm_Resource_Price DROP CONSTRAINT FK_SM_RESOU_PRICETYPE
GO
IF OBJECT_ID('Sm_Resource_Price', 'U') IS NOT NULL
	DROP TABLE Sm_Resource_Price
GO
IF OBJECT_ID('Sm_Resource_PriceType', 'U') IS NOT NULL
	DROP TABLE Sm_Resource_PriceType
GO

--需求计划外键约束
IF OBJECT_ID('FK_SM_WANTP_STOCK', 'F') IS NOT NULL
	ALTER TABLE Sm_Wantplan_Stock DROP CONSTRAINT FK_SM_WANTP_STOCK
GO
ALTER TABLE Sm_Wantplan_Stock 
ADD CONSTRAINT FK_SM_WANTP_STOCK 
	FOREIGN KEY(wpcode) REFERENCES Sm_Wantplan(swcode)
	ON DELETE CASCADE
GO

--采购计划外键约束
IF OBJECT_ID('FK_SM_PURCH_STOCK', 'F') IS NOT NULL
	ALTER TABLE Sm_Purchaseplan_Stock DROP CONSTRAINT FK_SM_PURCH_STOCK
GO
ALTER TABLE Sm_Purchaseplan_Stock 
ADD CONSTRAINT FK_SM_PURCH_STOCK
	FOREIGN KEY(ppcode) REFERENCES Sm_Purchaseplan(ppcode)
	ON DELETE CASCADE ON UPDATE CASCADE


--采购外键约束
IF OBJECT_ID('FK_SM_PURCHASE', 'F') IS NOT NULL
	ALTER TABLE Sm_Purchase_Stock DROP CONSTRAINT FK_SM_PURCHASE
GO
ALTER TABLE Sm_Purchase_Stock 
ADD CONSTRAINT FK_SM_PURCHASE
	FOREIGN KEY(pscode) REFERENCES Sm_Purchase(pcode)
	ON DELETE CASCADE ON UPDATE CASCADE

--入库外键约束
IF OBJECT_ID('FK_SM_STORAGE', 'F') IS NOT NULL
	ALTER TABLE Sm_Storage_Stock DROP CONSTRAINT FK_SM_STORAGE
GO
ALTER TABLE Sm_Storage_Stock
ADD CONSTRAINT FK_SM_STORAGE
	FOREIGN KEY(stcode) REFERENCES Sm_Storage(scode)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--出库外键约束
IF OBJECT_ID('FK_SM_OUTRE', 'F') IS NOT NULL
	ALTER TABLE Sm_out_Stock DROP CONSTRAINT FK_SM_OUTRE
GO
ALTER TABLE Sm_out_Stock 
	ADD CONSTRAINT FK_SM_OUTRE 
	FOREIGN KEY(orcode) REFERENCES Sm_OutReserve(orcode)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--退库外键约束
IF OBJECT_ID('FK_SM_REFUN', 'F') IS NOT NULL
	ALTER TABLE Sm_back_Stock DROP CONSTRAINT FK_SM_REFUN
GO
ALTER TABLE Sm_back_Stock
ADD CONSTRAINT FK_SM_REFUN
	FOREIGN KEY(rcode) REFERENCES Sm_Refunding(rcode)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

---------------------------------------合同---------------------------------
--合同类型表删除项目Id
IF ((SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
	WHERE TABLE_NAME = 'Con_ContractType' 
	AND COLUMN_NAME = 'PrjGuid') > 0)
	ALTER TABLE Con_ContractType DROP COLUMN PrjGuid
GO

--支出合同变更外键约束
IF OBJECT_ID('FK_CON_PAYOUT_REFERENCE_MODIFY', 'F') IS NOT NULL
	ALTER TABLE Con_Payout_Modify DROP CONSTRAINT FK_CON_PAYOUT_REFERENCE_MODIFY
GO
ALTER TABLE Con_Payout_Modify 
ADD CONSTRAINT FK_CON_PAYOUT_REFERENCE_MODIFY
	FOREIGN KEY(ContractId) REFERENCES Con_Payout_Contract(ContractId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--支出合同结算外键约束
IF OBJECT_ID('FK_CON_PAYOUT_REFERENCE_BALANCE', 'F') IS NOT NULL
	ALTER TABLE Con_Payout_Balance DROP CONSTRAINT FK_CON_PAYOUT_REFERENCE_BALANCE
GO
ALTER TABLE Con_Payout_Balance ADD CONSTRAINT FK_CON_PAYOUT_REFERENCE_BALANCE
	FOREIGN KEY(ContractId) REFERENCES Con_Payout_Contract(ContractId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--支出合同付款外键约束
IF OBJECT_ID('FK_CON_PAYOUT_REFERENCE_PAYMENT', 'F') IS NOT NULL	
	ALTER TABLE Con_Payout_Payment DROP CONSTRAINT FK_CON_PAYOUT_REFERENCE_PAYMENT
GO
ALTER TABLE Con_Payout_Payment ADD CONSTRAINT FK_CON_PAYOUT_REFERENCE_PAYMENT
	FOREIGN KEY(ContractId) REFERENCES Con_Payout_Contract(ContractId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--支出合同发票外键约束
IF OBJECT_ID('FK_CON_PAYO_REFERENCE_CON_PAYO', 'F') IS NOT NULL
	ALTER TABLE Con_Payout_Invoice DROP CONSTRAINT FK_CON_PAYO_REFERENCE_CON_PAYO
GO
ALTER TABLE Con_Payout_Invoice ADD CONSTRAINT FK_CON_PAYO_REFERENCE_CON_PAYO
	FOREIGN KEY(ContractId) REFERENCES Con_Payout_Contract(ContractId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--支出合同控制指标外键约束
IF OBJECT_ID('FK__Con_Payou__Contr__25FC62E4', 'F') IS NOT NULL
	ALTER TABLE Con_Payout_Target DROP CONSTRAINT FK__Con_Payou__Contr__25FC62E4
GO
ALTER TABLE Con_Payout_Target ADD CONSTRAINT FK__Con_Payou__Contr__25FC62E4
	FOREIGN KEY(ContractId) REFERENCES Con_Payout_Contract(ContractId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--收入合同交底外键约束
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_PAYEND', 'F') IS NOT NULL
	ALTER TABLE Con_ContractPayend DROP CONSTRAINT FK_CON_INCOME_REFERENCE_PAYEND
GO
ALTER TABLE Con_ContractPayend ADD CONSTRAINT FK_CON_INCOME_REFERENCE_PAYEND
	FOREIGN KEY(ContractId) REFERENCES Con_Incomet_Contract(ContractId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--收入合同反馈外键约束
IF OBJECT_ID('FK_CON_PAYEND_REFERENCE_FEEDBACK', 'F') IS NOT NULL
	ALTER TABLE Con_PayendFeedback DROP CONSTRAINT FK_CON_PAYEND_REFERENCE_FEEDBACK
GO
ALTER TABLE Con_PayendFeedback 
ADD CONSTRAINT FK_CON_PAYEND_REFERENCE_FEEDBACK
    FOREIGN KEY(ContractId) REFERENCES Con_Incomet_Contract(ContractId)
    ON DELETE CASCADE ON UPDATE CASCADE
GO

--收入合同变更外键约束
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_MODIFY', 'F') IS NOT NULL
    ALTER TABLE Con_Incomet_Modify 
    DROP CONSTRAINT FK_CON_INCOME_REFERENCE_MODIFY
GO
ALTER TABLE Con_Incomet_Modify
ADD CONSTRAINT FK_CON_INCOME_REFERENCE_MODIFY
    FOREIGN KEY(ContractId) REFERENCES Con_Incomet_Contract(ContractId)
    ON DELETE CASCADE ON UPDATE CASCADE
GO

--收入合同结算外键约束
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_BALANCE','F') IS NOT NULL
    ALTER TABLE Con_Incomet_Balance
    DROP CONSTRAINT FK_CON_INCOME_REFERENCE_BALANCE
GO
ALTER TABLE Con_Incomet_Balance
ADD CONSTRAINT FK_CON_INCOME_REFERENCE_BALANCE
    FOREIGN KEY(ContractId) REFERENCES Con_Incomet_Contract(ContractId)
    ON DELETE CASCADE ON UPDATE CASCADE
GO

--收入合同收款外键约束
IF OBJECT_ID('FK_CON_INCOME_REFERENCE_PAYMENT', 'F') IS NOT NULL
    ALTER TABLE Con_Incomet_Payment 
    DROP CONSTRAINT FK_CON_INCOME_REFERENCE_PAYMENT
GO
ALTER TABLE Con_Incomet_Payment
ADD CONSTRAINT FK_CON_INCOME_REFERENCE_PAYMENT
    FOREIGN KEY(ContractId) REFERENCES Con_Incomet_Contract(ContractId)
    ON DELETE CASCADE ON UPDATE CASCADE
GO

--收入合同发票外键约束
IF OBJECT_ID('FK_CON_INCO_REFERENCE_CON_INCO', 'F') IS NOT NULL
    ALTER TABLE Con_Incomet_Invoice 
    DROP CONSTRAINT FK_CON_INCO_REFERENCE_CON_INCO
GO
ALTER TABLE Con_Incomet_Invoice
ADD CONSTRAINT FK_CON_INCO_REFERENCE_CON_INCO
    FOREIGN KEY(ContractId) REFERENCES Con_Incomet_Contract(ContractId)
    ON DELETE CASCADE ON UPDATE CASCADE
GO

---------------------------------------预算---------------------------------
--删除WBS节点类型
IF OBJECT_ID('Bud_TaskType', 'U') IS NOT NULL
	DROP TABLE Bud_TaskType
GO

--预算WBS资源配置表外键约束
IF OBJECT_ID('FK__Bud_TaskR__TaskI__0D9AC96E', 'F') IS NOT NULL
	ALTER TABLE Bud_TaskResource DROP CONSTRAINT FK__Bud_TaskR__TaskI__0D9AC96E
GO
ALTER TABLE Bud_TaskResource ADD CONSTRAINT FK__Bud_TaskR__TaskI__0D9AC96E
	FOREIGN KEY(TaskId) REFERENCES Bud_Task(TaskId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO
IF OBJECT_ID('FK__Bud_TaskR__Resou__0E8EEDA7', 'F') IS NOT NULL
	ALTER TABLE Bud_TaskResource DROP CONSTRAINT FK__Bud_TaskR__Resou__0E8EEDA7
GO
ALTER TABLE Bud_TaskResource ADD CONSTRAINT FK__Bud_TaskR__Resou__0E8EEDA7
	FOREIGN KEY(ResourceId) REFERENCES Res_Resource(ResourceId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--施工报量节点资源表外键约束
IF OBJECT_ID('FK__Bud_ConsT__ConsT__65ACD3A5', 'F') IS NOT NULL
	ALTER TABLE Bud_ConsTaskRes	DROP CONSTRAINT FK__Bud_ConsT__ConsT__65ACD3A5
GO
ALTER TABLE Bud_ConsTaskRes ADD CONSTRAINT FK__Bud_ConsT__ConsT__65ACD3A5
	FOREIGN KEY(ConsTaskId) REFERENCES Bud_ConsTask(ConsTaskId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO
IF OBJECT_ID('FK__Bud_ConsT__Resou__66A0F7DE', 'F') IS NOT NULL
	ALTER TABLE Bud_ConsTaskRes DROP CONSTRAINT FK__Bud_ConsT__Resou__66A0F7DE
GO
ALTER TABLE Bud_ConsTaskRes ADD CONSTRAINT FK__Bud_ConsT__Resou__66A0F7DE
	FOREIGN KEY(ResourceId) REFERENCES Res_Resource(ResourceId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--施工报量节点表外键约束
IF OBJECT_ID('FK__Bud_ConsT__ConsR__61DC42C1', 'F') IS NOT NULL
	ALTER TABLE Bud_ConsTask DROP CONSTRAINT FK__Bud_ConsT__ConsR__61DC42C1
GO
ALTER TABLE Bud_ConsTask ADD CONSTRAINT FK__Bud_ConsT__ConsR__61DC42C1
	FOREIGN KEY(ConsReportId) REFERENCES Bud_ConsReport(ConsReportId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO
IF OBJECT_ID('FK__Bud_ConsT__TaskI__62D066FA', 'F') IS NOT NULL
	ALTER TABLE Bud_ConsTask DROP CONSTRAINT FK__Bud_ConsT__TaskI__62D066FA
GO
ALTER TABLE Bud_ConsTask ADD CONSTRAINT FK__Bud_ConsT__TaskI__62D066FA
	FOREIGN KEY(TaskId) REFERENCES Bud_Task(TaskId)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

------------------------------成本管理-------------------------
--间接成本预算外键约束
IF OBJECT_ID('FK_IndirectBud_CostAccounting', 'F') IS NOT NULL
	ALTER TABLE Bud_IndirectBudget DROP CONSTRAINT FK_IndirectBud_CostAccounting
GO
ALTER TABLE Bud_IndirectBudget ADD CONSTRAINT FK_IndirectBud_CostAccounting
	FOREIGN KEY(CBSCode) REFERENCES Bud_CostAccounting(CBSCode)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--间接成本月度预算外键
IF OBJECT_ID('FK__Bud_Indir__Indir__0801EBA9', 'F') IS NOT NULL
	ALTER TABLE Bud_IndirectMonthBudget DROP CONSTRAINT FK__Bud_Indir__Indir__0801EBA9
GO
ALTER TABLE Bud_IndirectMonthBudget ADD CONSTRAINT FK__Bud_Indir__Indir__0801EBA9
	FOREIGN KEY(IndirectBudget) REFERENCES Bud_IndirectBudget(Id)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--间接成本日记账明细外键
IF OBJECT_ID('FK_IndirectDetails_CostAccounting', 'F') IS NOT NULL
	ALTER TABLE Bud_IndirectDiaryDetails DROP CONSTRAINT FK_IndirectDetails_CostAccounting
GO
ALTER TABLE Bud_IndirectDiaryDetails ADD CONSTRAINT FK_IndirectDetails_CostAccounting
	FOREIGN KEY(CBSCode) REFERENCES Bud_CostAccounting(CBSCode)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--组织机构预算外键
IF OBJECT_ID('FK_OrgBud_CostAccounting', 'F') IS NOT NULL
	ALTER TABLE Bud_OrganizationBudget DROP CONSTRAINT FK_OrgBud_CostAccounting
GO
ALTER TABLE Bud_OrganizationBudget ADD CONSTRAINT FK_OrgBud_CostAccounting
	FOREIGN KEY(CBSCode) REFERENCES Bud_CostAccounting(CBSCode)
	ON DELETE CASCADE ON UPDATE CASCADE
GO

--编码库添加唯一约束
IF OBJECT_ID('UQ_SignCode', 'UQ') IS NOT NULL
    ALTER TABLE XPM_Basic_CodeType DROP CONSTRAINT UQ_SignCode
GO
ALTER TABLE XPM_Basic_CodeType ADD CONSTRAINT UQ_SignCode UNIQUE(SignCode)
GO
