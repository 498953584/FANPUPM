
--预算部分
--SELECT * FROM sys.tables WHERE name LIKE 'Bud_%' 

BEGIN TRY
	BEGIN TRAN
		DELETE Bud_PrjTaskInfo	
		DELETE Bud_TaskResource	
		DELETE FROM dbo.Res_ResourceTemp
		DELETE FROM dbo.Bud_ContractTask
		DELETE FROM dbo.Bud_ContractResource
		DELETE FROM dbo.Bud_ConsTaskRes
		DELETE FROM dbo.Bud_ConsTask
		DELETE FROM dbo.Bud_ConsReport

		DELETE Bud_TemplateResource
		DELETE Bud_TemplateItem
		DELETE Bud_Template
		DELETE Bud_TemplateType	

        DELETE FROM dbo.Bud_IndirectDiaryDetails
        DELETE FROM dbo.Bud_IndirectDiaryCost
		DELETE FROM dbo.Bud_IndirectMonthBudget
		DELETE FROM dbo.Bud_IndirectBudget

        DELETE FROM dbo.Bud_OrgDiaryDetails
        DELETE FROM dbo.Bud_OrgDiaryCost
        DELETE FROM dbo.Bud_OrganizationMonthBudget
		DELETE FROM dbo.Bud_OrganizationBudget
		DELETE FROM dbo.Bud_CostAccounting WHERE LEN(CBSCode)>6 AND CBSCode NOT IN('001001000','001001001','001001002','001001003','001001999')
		DELETE FROM Con_Payout_Target
		DELETE FROM dbo.Bud_Task
        IF OBJECT_ID('Bud_TaskChange') IS NOT NULL      --预算变更表
            DELETE FROM Bud_TaskChange 
	COMMIT TRAN 
END TRY
BEGIN CATCH
	ROLLBACK TRAN
	PRINT ERROR_MESSAGE()
END CATCH
