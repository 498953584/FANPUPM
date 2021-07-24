--目标成本
ALTER TABLE Bud_Task ADD ModifyId nvarchar(64)  
GO
ALTER TABLE Bud_Task ADD ModifyType nvarchar(1)
GO
ALTER TABLE Bud_Task ADD CONSTRAINT FK_BudModify_ModifyId FOREIGN KEY(ModifyId) REFERENCES Bud_Modify(ModifyId)
GO
--合同预算
ALTER TABLE Bud_ContractTask ADD ModifyId nvarchar(64)  
GO
ALTER TABLE Bud_ContractTask ADD ModifyType nvarchar(1)
GO
ALTER TABLE Bud_ContractTask ADD CONSTRAINT FK_BudConModify_ModifyId FOREIGN KEY(ModifyId) REFERENCES Bud_ConModify(ModifyId)
GO

---------------------------------------------------------------
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

-- 获取分部分项下子节点个数
ALTER FUNCTION [dbo].[fn_GetCount](@TaskId nvarchar(200))
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
	SELECT @modifyChildCount=COUNT(*) FROM Bud_ModifyTask mainModifyTask JOIN Bud_Modify mainBudModify 
	ON mainModifyTask.ModifyId=mainBudModify.ModifyId
	JOIN 
	(
		SELECT modifyTask.*,budModify.PrjId from Bud_ModifyTask modifyTask 
		JOIN Bud_Modify budModify ON modifyTask.ModifyId=budModify.ModifyId
		WHERE ModifyTaskId=@TaskId+'-'+modifyTask.OrderNumber
	) modifyTaskIdInfo ON mainBudModify.PrjId=modifyTaskIdInfo.PrjId
	AND mainModifyTask.OrderNumber LIKE modifyTaskIdInfo.OrderNumber+'%'
	AND mainModifyTask.ModifyTaskId <> modifyTaskIdInfo.ModifyTaskId
	AND mainBudModify.FlowState=1
	RETURN @Count+@modifyChildCount
END

GO
---------------------------------------------------------------

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
go

--获取分部分项的total
ALTER FUNCTION [dbo].[fn_GetTotal](@TaskId nvarchar(200))
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
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.TaskId)) total ,OrderNumber
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId,modifyTask.OrderNumber
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
			SELECT modifyTask.TaskId,SUM(dbo.fn_GetModifyTotal(modifyTask.TaskId)) total ,OrderNumber
			from Bud_ModifyTask modifyTask JOIN 
			(
				SELECT Bud_Modify.* FROM Bud_Modify INNER JOIN Bud_Task 
				ON Bud_Modify.prjId=Bud_Task.PrjId 
				WHERE Bud_Task.TaskId = @TaskId
			) budMOdify ON modifyTask.ModifyId=budModify.ModifyId 
			WHERE budModify.FlowState=1 AND modifyType=0 
			GROUP BY modifyTask.TaskId,modifyTask.OrderNumber
		) OutBudModifyInfo ON T2.TaskId=OutBudModifyInfo.TaskId
	END
	RETURN @Total
END
GO
----------------------------------------------------------------------
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
GO


--创建读取变更任务的小计
ALTER FUNCTION [dbo].[fn_GetModifyTotal](@TaskId nvarchar(200))
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
		WHERE TaskId=@TaskId
	) currentModifyTask ON allModifyTask.prjId=currentModifyTask.PrjId
	AND allModifyTask.OrderNumber like currentModifyTask.OrderNumber+'%'
	LEFT JOIN 
	(
		--清单内的变更金额
		SELECT Bud_ModifyTask.TaskId,SUM(Bud_ModifyTask.Total) Total FROM Bud_ModifyTask 
		JOIN Bud_Modify ON Bud_ModifyTask.ModifyId=Bud_Modify.ModifyId
		WHERE FlowState=1 AND ModifyType=1 group by Bud_ModifyTask.TaskId
	) InBudModiyInfo ON allModifyTask.TaskId=InBudModiyInfo.TaskId
	RETURN @Total
END

GO
-----------------------------------------------------------------------
--存储过程的修改usp_GetAllTaskByPrj
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[usp_GetAllTaskByPrj]
	@prjId nvarchar(200)
AS
BEGIN
	SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No, *,(Total2/NULLIF(Quantity,0.0)) AS UnitPrice FROM (
		SELECT ParentId, TaskId, OrderNumber, TaskCode, TaskName, Unit, 
             Quantity,
             StartDate, EndDate, Note, Total, --Total2,
			dbo.fn_GetTotal(TaskId) AS Total2,            
			ConstructionPeriod, FeatureDescription, ModifyType,dbo.fn_GetCount(TaskId) AS SubCount
		FROM Bud_Task 
		WHERE PrjId = @prjId AND TaskType = ''		
	) AS T
	ORDER BY No
END
GO
------------------------------------------------------------------------
--修改存储过程usp_GetAllTaskByParent
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER PROC [dbo].[usp_GetAllTaskByParent]
	@taskId nvarchar(200)
AS
BEGIN
	SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No, * ,(Total2/NULLIF(Quantity,0.0)) AS UnitPrice FROM (
		SELECT T.ParentId, T.TaskId, T.OrderNumber, T.TaskCode, T.TaskName, T.Unit, 
            (SELECT ISNULL( SUM(Quantity), 0.0) FROM Bud_ModifyTask AS MT
		        WHERE MT.TaskId = T.TaskId AND MT.ModifyType = '1') + T.Quantity AS Quantity,
			 T.StartDate, T.EndDate, T.Note, T.Total, --Total2,         
			dbo.fn_GetTotal(T.TaskId) AS Total2, 
			T.ConstructionPeriod, T.FeatureDescription, 'T' AS btype,
			'1' AS ModifyType,dbo.fn_GetCount(T.TaskId) AS SubCount
		FROM Bud_Task T
		JOIN Bud_Task T2 ON T.PrjId = T2.PrjId
		WHERE T2.TaskId = @taskId AND T.OrderNumber LIKE T2.OrderNumber + '%' AND T.TaskType = ''
	) AS T
END
GO
------------------------------------------------------------------------
--获取合同预算分项子节点个数
set ANSI_NULLS ON
set QUOTED_IDENTIFIER ON
go

ALTER FUNCTION [dbo].[fn_GetContractTaskCount] (@TaskId nvarchar(200))
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
	RETURN @Count
END
GO
------------------------------------------------------------------------

--清单内  支出合同
UPDATE Bud_Task SET Bud_Task.Quantity=Bud_Task.Quantity+BMT.Quantity,
Bud_Task.Total2=Bud_Task.Total2+BMT.Total2,
Bud_Task.UnitPrice=ISNULL(Bud_Task.Total2/NULLIF(Bud_Task.Quantity,0.0),0.0)
FROM 
(SELECT SUM(Quantity) AS Quantity,SUM(Total2) AS Total2,TaskId FROM Bud_ModifyTask 
GROUP BY  TaskId)  BMT
WHERE Bud_Task.TaskId=BMT.TaskId 

--清单外
INSERT INTO  Bud_Task (TaskId,ParentId,OrderNumber,Version,PrjId,TaskCode,TaskName,Unit,Quantity,
UnitPrice,StartDate,EndDate,Note,IsValid,InputUser,InputDate,Total,Modified,ConstructionPeriod,TaskType,
FeatureDescription,ContractId,Total2,ModifyType,ModifyId)
SELECT * FROM
(
SELECT TaskId,ParentId,OrderNumber,1 AS Version,PrjId2,ModifyTaskCode,ModifyTaskContent,Unit,Quantity,
UnitPrice,StartDate,EndDate,BTK.Note,
CASE BM.FlowState WHEN 1 THEN 1 ELSE 0 END AS IsValid ,
InputUser,InputDate,0 AS Total,null AS Modified,ConstructionPeriod,'' AS TaskType,
FeatureDescription,ContractId,Total2,ModifyType,BTK.ModifyId 
FROM Bud_ModifyTask BTK
LEFT JOIN  Bud_Modify BM
ON  BTK.ModifyId=BM.ModifyId
WHERE  BTK.ModifyType=0 
) T 
 
--清单内  收入合同
UPDATE Bud_ContractTask SET Bud_ContractTask.Quantity=Bud_ContractTask.Quantity+BMT.Quantity,
Bud_ContractTask.Total=Bud_ContractTask.Total+BMT.Total,
Bud_ContractTask.MainMaterial=Bud_ContractTask.MainMaterial+BMT.MainMaterial,
Bud_ContractTask.SubMaterial=Bud_ContractTask.SubMaterial+BMT.SubMaterial,
Bud_ContractTask.Labor=Bud_ContractTask.Labor+BMT.Labor,
Bud_ContractTask.UnitPrice=ISNULL(Bud_ContractTask.Total/NULLIF(Bud_ContractTask.Quantity,0.0),0.0)
FROM 
(SELECT SUM(Quantity) AS Quantity,SUM(Total) AS Total,
SUM(MainMaterial) AS MainMaterial,SUM(SubMaterial)AS SubMaterial,SUM(Labor)AS Labor,TaskId FROM Bud_ConModifyTask 
GROUP BY  TaskId)  BMT
WHERE Bud_ContractTask.TaskId=BMT.TaskId 
--清单外
INSERT INTO  Bud_ContractTask (TaskId,ParentId,OrderNumber,PrjId,TaskCode,TaskName,Unit,Quantity,
UnitPrice,Total,StartDate,EndDate,Note,IsValid,InputUser,InputDate,ConstructionPeriod,TaskType,
ContractId,MainMaterial,SubMaterial,Labor,FeatureDescription,ModifyId,ModifyType)
SELECT * FROM
(
SELECT TaskId,ParentId,OrderNumber,PrjId2,ModifyTaskCode,ModifyTaskContent,Unit, SUM(Quantity) AS Quantity,
SUM(Total)/NULLIF(SUM(Quantity),0.0) AS UnitPrice,SUM(Total) AS Total,StartDate,EndDate,BTK.Note,
CASE BM.FlowState WHEN 1 THEN 1 ELSE 0 END AS IsValid ,
InputUser,InputDate,ConstructionPeriod,'' AS TaskType,
ContractId,SUM(MainMaterial) AS MainMaterial,SUM(SubMaterial)AS SubMaterial,SUM(Labor)AS Labor,FeatureDescription,BTK.ModifyId,ModifyType
FROM Bud_ConModifyTask BTK
LEFT JOIN  Bud_ConModify BM
ON  BTK.ModifyId=BM.ModifyId
WHERE  BTK.ModifyType=0  
GROUP BY TaskId,ParentId,OrderNumber,PrjId2,ModifyTaskCode,ModifyTaskContent,Unit,UnitPrice,StartDate,EndDate,BTK.Note,
InputUser,InputDate,ConstructionPeriod,ContractId,FeatureDescription,BTK.ModifyId,ModifyType,BM.FlowState
) T 
--------------------------------------------------------------------------------