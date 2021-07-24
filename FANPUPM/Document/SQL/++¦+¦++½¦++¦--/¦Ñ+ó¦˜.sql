--最后更新时间：        2014-02-13 16:26


--添加PT_PrjInfo时添加项目小组人员审核信息PT_PrjMemberWF
IF OBJECT_ID('trig_Insert_PT_PrjInfo', 'TR') IS NOT NULL
	DROP TRIGGER trig_Insert_PT_PrjInfo
GO
CREATE TRIGGER trig_Insert_PT_PrjInfo
	ON PT_PrjInfo AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @PrjGuid nvarchar(200)
	SELECT @PrjGuid = PrjGuid FROM INSERTED
	-- 如果已经存在了，则不添加
	IF (SELECT COUNT(*) FROM PT_PrjMemberWF WHERE PrjGuid = @PrjGuid) = 0
		INSERT INTO PT_PrjMemberWF(PrjGuid, FlowState) VALUES(@PrjGuid, -1)
END
GO


--删除项目的触发器
IF OBJECT_ID('TR_Drop_PrjInfo', 'TR') IS NOT NULL
	DROP TRIGGER TR_Drop_PrjInfo
GO
CREATE TRIGGER TR_Drop_PrjInfo ON PT_PrjInfo
AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON
    DELETE PT_PrjInfo_ZTB_User FROM PT_PrjInfo_ZTB_User ZU, DELETED WHERE ZU.PrjGuid = DELETED.PrjGuid        --PT_PrjInfo_ZTB_User
	DELETE Sm_Wantplan FROM Sm_Wantplan, DELETED WHERE procode = CONVERT(nvarchar(200), DELETED.PrjGuid) --需求计划
	DELETE Sm_Purchaseplan FROM Sm_Purchaseplan, DELETED WHERE project = CONVERT(nvarchar(200), DELETED.PrjGuid) --采购计划
	DELETE Sm_Purchase FROM Sm_Purchase, DELETED WHERE Project = CONVERT(nvarchar(200), DELETED.PrjGuid) --采购
	DELETE Sm_Storage FROM Sm_Storage, DELETED WHERE project = CONVERT(nvarchar(200), DELETED.PrjGuid) --入库
	DELETE Sm_OutReserve FROM Sm_OutReserve, DELETED WHERE procode = CONVERT(nvarchar(200), DELETED.PrjGuid) --出库
	DELETE Sm_Refunding FROM Sm_Refunding, DELETED WHERE procode = CONVERT(nvarchar(200), DELETED.PrjGuid) --退库
	DELETE Con_Payout_Contract FROM Con_Payout_Contract, DELETED WHERE Con_Payout_Contract.PrjGuid = CONVERT(nvarchar(200), DELETED.PrjGuid) --支出合同
	DELETE Con_Incomet_Contract FROM Con_Incomet_Contract, DELETED WHERE Project = CONVERT(nvarchar(200), DELETED.PrjGuid) --收入合同
	DELETE Bud_PrjTaskInfo FROM Bud_PrjTaskInfo, DELETED WHERE PrjId = CONVERT(nvarchar(200), DELETED.PrjGuid) --预算锁定信息
	DELETE Bud_Task FROM Bud_Task, DELETED WHERE PrjId = CONVERT(nvarchar(200), DELETED.PrjGuid) --预算WBS节点
    IF (OBJECT_ID('Bud_TaskChange')) IS NOT NULL            --预算变更表
        DELETE Bud_TaskChange FROM Bud_TaskChange, DELETED WHERE PrjId =  CONVERT(nvarchar(200), DELETED.PrjGuid)
	DELETE Bud_ConsReport FROM Bud_ConsReport, DELETED WHERE PrjId = CONVERT(nvarchar(200), DELETED.PrjGuid) --施工报量
	DELETE Bud_ContractTask FROM Bud_ContractTask, DELETED WHERE PrjId = CONVERT(nvarchar(200), DELETED.PrjGuid) --中标预算
	DELETE Bud_ContractConsReport FROM Bud_ContractConsReport, DELETED WHERE PrjId = CONVERT(nvarchar(200), DELETED.PrjGuid)	--中标预算施工报量
	DELETE Bud_IndirectBudget FROM Bud_IndirectBudget, DELETED WHERE ProjectId = CONVERT(nvarchar(200), DELETED.PrjGuid) --间接成本预算
	DELETE Bud_IndirectDiaryCost FROM Bud_IndirectDiaryCost, DELETED WHERE ProjectId = CONVERT(nvarchar(200), DELETED.PrjGuid) --简介成本日记账
	DELETE PT_PrjMemberWF FROM PT_PrjMemberWF, DELETED WHERE PT_PrjMemberWF.PrjGuid = DELETED.PrjGuid				--项目小组成员审核
	DELETE plus_progress FROM plus_progress, DELETED WHERE plus_progress.PrjId = DELETED.PrjGuid					--删除进度计划
	DELETE PT_StartReport FROM PT_StartReport, DELETED WHERE PT_StartReport.PrjGuid = DELETED.PrjGuid				--删除开工信息
	DELETE Pt_StopMsg FROM Pt_StopMsg, DELETED WHERE Pt_StopMsg.PrjGuid = DELETED.PrjGuid				--删除停工信息
	DELETE PT_RetMsg FROM PT_RetMsg, DELETED WHERE PT_RetMsg.PrjGuid = DELETED.PrjGuid				--删除复工信息
END
GO



--detete PT_PrjInfo_ZTB_Detail
IF OBJECT_ID('tr_Delete_PT_PrjInfo_ZTB_Detail') IS NOT NULL
	DROP TRIGGER tr_Delete_PT_PrjInfo_ZTB_Detail
GO
CREATE TRIGGER tr_Delete_PT_PrjInfo_ZTB_Detail
ON PT_PrjInfo_ZTB_Detail AFTER DELETE
AS
BEGIN
	SET NOCOUNT ON
	DELETE FROM PT_PrjInfo_ZTB WHERE PrjGuid IN (SELECT PrjGuid FROM DELETED);
	DELETE FROM PT_PrjInfo WHERE PrjGuid IN (SELECT PrjGuid FROM DELETED);
END 
GO

--级联删除资源类型的触发器
IF OBJECT_ID('trig_DeleteResourceType') IS NOT NULL
	DROP TRIGGER trig_DeleteResourceType
GO
CREATE TRIGGER trig_DeleteResourceType ON Res_ResourceType
INSTEAD OF DELETE
AS
BEGIN
	SET NOCOUNT ON
	
	DELETE Res_ResourceType
	FROM Res_ResourceType, deleted
	WHERE Res_ResourceType.ParentId = deleted.ResourceTypeId
	
	DELETE Res_ResourceType
	FROM Res_ResourceType, deleted
	WHERE Res_ResourceType.ResourceTypeId = deleted.ResourceTypeId 
END
GO


--新增模块时，添加管理员权限
IF OBJECT_ID('trig_AddAdminPrivilege') IS NOT NULL
	DROP TRIGGER trig_AddAdminPrivilege
GO
CREATE TRIGGER trig_AddAdminPrivilege
	ON PT_MK AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	IF ((SELECT COUNT(*) FROM PT_YHMC WHERE v_yhdm = '00000000') = 1)
	INSERT INTO PT_YHMC_Privilege(V_YHDM, C_MKDM, IsBasic, IsHaveOp)
		SELECT '00000000',INSERTED.C_MKDM, INSERTED.IsBasic, 0
FROM INSERTED
END
GO

-- 该项目下第一次配置WBS时, 向Bud_TaskChange添加数据   
IF OBJECT_ID('trigInsertBudTask') IS NOT NULL
	DROP TRIGGER trigInsertBudTask
GO
CREATE TRIGGER trigInsertBudTask
	ON Bud_Task AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	IF ((SELECT COUNT(*) FROM Bud_TaskChange,inserted 
			WHERE Bud_TaskChange.PrjId = inserted.PrjId) = 0)
		INSERT INTO Bud_TaskChange
		SELECT NEWID(), PrjId, 1, 'V1.0', -1, '', NULL, GETDATE() FROM inserted 
END
GO

--该项目下第一次配置合同预算时, 向Bud_ContractTaskAudit添加数据 
IF OBJECT_ID('trigInsertContractTask') IS NOT NULL
	DROP TRIGGER trigInsertContractTask
GO
CREATE TRIGGER trigInsertContractTask
ON Bud_ContractTask AFTER INSERT
AS
	BEGIN
		SET NOCOUNT ON
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

-- 创建投标项目权限管理删除触发器        lhy  
IF OBJECT_ID('trig_delete_prjinfo') IS NOT NULL
	DROP TRIGGER trig_delete_prjinfo
GO
CREATE TRIGGER trig_delete_prjinfo
ON [PT_PrjInfo_ZTB_User] AFTER INSERT
AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @user AS nvarchar(4000)
		DECLARE @prjguid AS nvarchar(200) 
		SELECT @prjguid = PrjGuid FROM DELETED

		SELECT @user= ISNULL(@user + ',' ,',') + UserCode 
		FROM (
			SELECT DISTINCT UserCode FROM PT_PrjInfo_ZTB_User 
			WHERE PrjGuid = @prjguid
		)AS T
		UPDATE PT_PrjInfo SET Podepom = @user 
		WHERE PrjGuid = @prjguid
	END
GO

--  创建投标项目权限管理添加触发器		lhy
IF OBJECT_ID('trig_insert_prjinfo') IS NOT NULL
	DROP TRIGGER trig_insert_prjinfo
GO
CREATE TRIGGER trig_insert_prjinfo
ON [PT_PrjInfo_ZTB_User] AFTER INSERT
AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @user AS nvarchar(4000)
		DECLARE @prjguid AS nvarchar(200) 
		SELECT @prjguid = PrjGuid FROM INSERTED

		SELECT @user= ISNULL(@user + ',' ,',') + UserCode 
		FROM (
			SELECT DISTINCT UserCode FROM PT_PrjInfo_ZTB_User 
			WHERE PrjGuid = @prjguid
		)AS T
		UPDATE PT_PrjInfo SET Podepom = @user 
		WHERE PrjGuid = @prjguid
	END
GO



--修改资源配置
IF OBJECT_ID('trig_insert_update_BudTaskResource') IS NOT NULL
	DROP TRIGGER trig_insert_update_BudTaskResource
GO



--修改资源配置
IF OBJECT_ID('trig_insert_update_BudModifyTaskRes ') IS NOT NULL
	DROP TRIGGER trig_insert_update_BudModifyTaskRes
GO

--修改合同变更触发器    Bery
IF OBJECT_ID('Approval', 'TR') IS NOT NULL
	DROP TRIGGER Approval
GO
IF OBJECT_ID('trig_update_ConPayoutModify', 'TR') IS NOT NULL
	DROP TRIGGER trig_update_ConPayoutModify
GO
CREATE TRIGGER trig_update_ConPayoutModify
	ON Con_Payout_Modify AFTER UPDATE
AS
DECLARE @state int
SELECT @state = FlowState FROM INSERTED
IF @state = 1
BEGIN
	SET NOCOUNT ON
	UPDATE Con_Payout_Contract SET ModifiedMoney = ModifiedMoney + i.ModifyMoney
	FROM Con_Payout_Contract AS c
	JOIN INSERTED AS i ON c.ContractID = i.ContractID
END
GO

