--最后更新时间  2014-02-17 16:30

--获取指定项目下的所有合同预算，包括合同外变更      Bery    2014-02-17 16:30
IF OBJECT_ID('dbo.ufn_GetAllConTask') IS NOT NULL
	DROP FUNCTION dbo.ufn_GetAllConTask
GO
CREATE FUNCTION dbo.ufn_GetAllConTask(@prjId nvarchar(200))
RETURNS TABLE
AS
RETURN
(
	SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No, * FROM (
		SELECT ParentId, TaskId, OrderNumber, TaskCode, TaskName, Unit, Quantity, 
			UnitPrice, StartDate, EndDate, Note, Total, 
			ConstructionPeriod, FeatureDescription, '' AS ModifyType
		FROM Bud_ContractTask 
		WHERE PrjId = @prjId AND TaskType = ''
		UNION
		SELECT T.ParentId, T.ModifyTaskId, T.OrderNumber,
			T.ModifyTaskCode AS TaskCode, T.ModifyTaskContent AS TaskName, T.Unit ,T.Quantity,
			T.UnitPrice, T.StartDate, T.EndDate, T.Note, T.Total,
			T.ConstructionPeriod, T.FeatureDescription, '0' AS ModifyType
		FROM Bud_ConModifyTask AS T
		LEFT JOIN Bud_ConModify ON T.ModifyId = Bud_ConModify.ModifyId
		WHERE Bud_ConModify.PrjId = @prjId 
			AND Bud_ConModify.FlowState = '1' AND T.ModifyType = '0' 
	) AS T
);
GO
--最后更新时间  2014-06-13 9:46  WDD
IF OBJECT_ID('dbo.fn_GetTotal') IS NOT NULL
	DROP FUNCTION dbo.fn_GetTotal
GO
CREATE FUNCTION dbo.fn_GetTotal(@TaskId nvarchar(200))
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


--最后更新时间  2014-06-13 9:50  WDD
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

--最后更新时间  2014-06-13 09:50  WDD
IF OBJECT_ID('fn_GetContractTaskCount',  N'FN') IS NOT NULL
	DROP FUNCTION fn_GetContractTaskCount
GO

CREATE FUNCTION [dbo].[fn_GetContractTaskCount] (@TaskId nvarchar(200))
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

--最后更新时间  2014-05-09 09:50  WDD
IF OBJECT_ID('dbo.ufn_GetAllConTask') IS NOT NULL
	DROP FUNCTION dbo.ufn_GetAllConTask
GO

CREATE FUNCTION [dbo].[ufn_GetAllConTask](@prjId nvarchar(200))
RETURNS TABLE
AS
RETURN
(
	SELECT ROW_NUMBER() OVER(ORDER BY OrderNumber) AS No, * FROM (
		SELECT ParentId, TaskId, OrderNumber, TaskCode, TaskName, Unit, Quantity, 
			UnitPrice, StartDate, EndDate, Note, Total, 
			ConstructionPeriod, FeatureDescription, '' AS ModifyType
		FROM Bud_ContractTask 
		WHERE PrjId = @prjId AND TaskType = ''
		UNION
		SELECT T.ParentId, T.TaskId, T.OrderNumber,
			T.ModifyTaskCode AS TaskCode, T.ModifyTaskContent AS TaskName, T.Unit ,T.Quantity,
			T.UnitPrice, T.StartDate, T.EndDate, T.Note, T.Total,
			T.ConstructionPeriod, T.FeatureDescription, '0' AS ModifyType
		FROM Bud_ConModifyTask AS T
		LEFT JOIN Bud_ConModify ON T.ModifyId = Bud_ConModify.ModifyId
		WHERE Bud_ConModify.PrjId = @prjId 
			AND Bud_ConModify.FlowState = '1' AND T.ModifyType = '0' 
	) AS T
);

GO

--最后更新时间  2014-06-13 09:50  WDD
IF OBJECT_ID('dbo.fn_GetModifyTotal') IS NOT NULL
	DROP FUNCTION dbo.fn_GetModifyTotal
GO

--创建读取变更任务的小计
CREATE FUNCTION [dbo].[fn_GetModifyTotal](@TaskId nvarchar(200))
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

