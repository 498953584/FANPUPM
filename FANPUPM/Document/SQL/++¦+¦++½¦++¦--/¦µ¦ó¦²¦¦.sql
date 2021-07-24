--最后更新时间  2014-04-18

--uspGetProject
IF OBJECT_ID('uspGetProject') IS NOT NULL
    DROP PROC uspGetProject
GO
CREATE PROC uspGetProject
	@userCode nvarchar(20),		--用户编码
	@isTender nvarchar(1),		--是否投标项目
	@columns nvarchar(1000),	--返回字段
	@condition nvarchar(1000),	--其他条件
	@pageIndex int,				--页码
	@pageSize int,				--每页多少条记录
	@rowCount int OUTPUT		--输出参数，共多少行
AS
BEGIN
	DECLARE @sql nvarchar(max);
	DECLARE @sql2 nvarchar(max);
	DECLARE @sql3 nvarchar(max);
	DECLARE @sql4 nvarchar(max);
	SET @sql = ' 
		DECLARE @primitPrjGuid table(PrjGuid uniqueidentifier);
		DECLARE @project table (
			PrjGuid uniqueidentifier, TypeCode nvarchar(20)
		);

		INSERT INTO	@primitPrjGuid
		SELECT DISTINCT PrjGuid FROM PT_PrjInfo_ZTB_User
		WHERE UserCode = ''' + @userCode +''';

        --根据角色查找项目
        INSERT INTO	@primitPrjGuid
        SELECT BusiDataId FROM Priv_BusiDataRole
	    WHERE TableName=''PT_PrjInfo_ZTB_Detail''
        AND RoleId IN(SELECT DISTINCT RoleId FROM Priv_UserRole WHERE UserCode=''' + @userCode +''')
        AND BusiDataId NOT IN(SELECT PrjGuid FROM @primitPrjGuid);
        
		--根据项目属性查找项目
        INSERT INTO @primitPrjGuid
        SELECT prjGuid FROM PT_PrjInfo_ZTB_Detail 
		WHERE prjProperty IN
		(
			   SELECT codeID FROM XPM_Basic_CodeList 
			   where NoteID IN 
			  (
					SELECT DISTINCT RelationsKey
					FROM Basic_Privilege
					WHERE RelationsKey IN
						(
						SELECT NoteID FROM XPM_Basic_CodeList
						JOIN XPM_Basic_CodeType ON SignCode=''ProjectProperty''
						WHERE XPM_Basic_CodeList.TypeID=XPM_Basic_CodeType.TypeID
						)
					 AND UserCode='''+@userCode+''' AND RelationsTable=''XPM_Basic_CodeList''
				)
		)   
		INSERT INTO @project
		SELECT P.PrjGuid, P.TypeCode 
		FROM vProject AS P
		WHERE P.PrjState IN(''5'',''7'',''8'',''9'',''10'',''11'',''12'',''13'',''17'') 
        AND P.PrjGuid IN (SELECT PrjGuid FROM @primitPrjGuid) AND SetUpFlowState=1 ' + @condition +''

	SET @sql2 = '			
			SELECT C.Primit, vProject.* 
			FROM (
				SELECT pro.*, 1 AS Primit 
				FROM @project AS pro
				UNION
				SELECT P.PrjGuid, P.TypeCode, 
					(SELECT COUNT(*) FROM @primitPrjGuid AS PP WHERE PP.PrjGuid = P.PrjGuid) AS Primit		
				FROM PT_PrjInfo AS P
				WHERE P.TypeCode IN (
					SELECT DISTINCT LEFT(TypeCode, 5) ParentTypeCode
					FROM @project AS pro
					WHERE LEN(pro.TypeCode) = 10
				)
			) AS C
			INNER JOIN vProject ON vProject.PrjState IN(''5'',''7'',''8'',''9'',''10'',''11'',''12'',''13'',''17'') AND vProject.PrjGuid = C.PrjGuid '
	IF (@isTender = '1')
		SET @sql2 = @sql2 + ' WHERE IsTender = ''1'''
	ELSE IF (@isTender = '0')
		SET @sql2 = @sql2 + ' WHERE IsTender = ''0'''
	SET @sql3 = '
		SELECT *, ROW_NUMBER() OVER(ORDER BY Date DESC, TypeCode ASC) AS No
		FROM(
			SELECT ' +@columns + ',
				CASE LEN(TypeCode) 
					WHEN 10 THEN (
						SELECT InputDate FROM vProject AS V1 WHERE V1.TypeCode = SUBSTRING(D.TypeCode, 1, 5)
					)
					WHEN 5 THEN InputDate
				END AS Date
			FROM (' + @sql2 + ') AS D 
		) AS E'

	SET @sql4 = '
		SELECT TOP(' + CAST(@pageSize AS nvarchar(20)) + ') * 
		FROM (' + @sql3 + ' ) AS F 
		WHERE No > ' + CAST( @pageSize * (@pageIndex - 1) AS nvarchar(20))+ '
		ORDER BY No ';
	EXEC(@sql + @sql4)

	EXEC(@sql + @sql3)
	SET @rowCount = @@ROWCOUNT
END
GO

--uspGetAllProject
IF OBJECT_ID('uspGetAllProject') IS NOT NULL
    DROP PROC uspGetAllProject
GO

CREATE PROC [dbo].[uspGetAllProject]
	@userCode nvarchar(20),		--用户编码
	@isTender nvarchar(1),		--是否投标项目
	@columns nvarchar(1000),	--返回字段
	@condition nvarchar(1000),	--其他条件
	@pageIndex int,				--页码
	@pageSize int,				--每页多少条记录
	@rowCount int OUTPUT		--输出参数，共多少行
AS
BEGIN
	DECLARE @sql nvarchar(max);
	DECLARE @sql2 nvarchar(max);
	DECLARE @sql3 nvarchar(max);
	DECLARE @sql4 nvarchar(max);
	SET @sql = ' 
		DECLARE @primitPrjGuid table(PrjGuid uniqueidentifier);
		DECLARE @project table (
			PrjGuid uniqueidentifier, TypeCode nvarchar(20)
		);

		INSERT INTO	@primitPrjGuid
		SELECT DISTINCT PrjGuid FROM PT_PrjInfo_ZTB_User
		WHERE UserCode = ''' + @userCode +''';
        --根据角色查找项目
        INSERT INTO	@primitPrjGuid
        SELECT BusiDataId FROM Priv_BusiDataRole
	    WHERE TableName=''PT_PrjInfo_ZTB_Detail''
        AND RoleId IN(SELECT DISTINCT RoleId FROM Priv_UserRole WHERE UserCode=''' + @userCode +''')
        AND BusiDataId NOT IN(SELECT PrjGuid FROM @primitPrjGuid);
        --根据项目属性查找项目
        INSERT INTO @primitPrjGuid
        SELECT prjGuid FROM PT_PrjInfo_ZTB_Detail 
		WHERE prjProperty IN
		(
			   SELECT codeID FROM XPM_Basic_CodeList 
			   where NoteID IN 
			  (
					SELECT DISTINCT RelationsKey
					FROM Basic_Privilege
					WHERE RelationsKey IN
						(
						SELECT NoteID FROM XPM_Basic_CodeList
						JOIN XPM_Basic_CodeType ON SignCode=''ProjectProperty''
						WHERE XPM_Basic_CodeList.TypeID=XPM_Basic_CodeType.TypeID
						)
					 AND UserCode='''+@userCode+''' AND RelationsTable=''XPM_Basic_CodeList''
				)
		)        

		INSERT INTO @project
		SELECT P.PrjGuid, P.TypeCode 
		FROM vProject AS P
		WHERE P.PrjState IN(''5'',''7'',''8'',''9'',''10'',''11'',''12'',''13'',''17'') AND P.PrjGuid IN (SELECT PrjGuid FROM @primitPrjGuid) ' + @condition +''
	SET @sql2 = '			
			SELECT C.Primit, vProject.* 
			FROM (
				SELECT pro.*, 1 AS Primit 
				FROM @project AS pro
				UNION
				SELECT P.PrjGuid, P.TypeCode, 
					(SELECT COUNT(*) FROM @primitPrjGuid AS PP WHERE PP.PrjGuid = P.PrjGuid) AS Primit		
				FROM PT_PrjInfo AS P
				WHERE P.TypeCode IN (
					SELECT DISTINCT LEFT(TypeCode, 5) ParentTypeCode
					FROM @project AS pro
					WHERE LEN(pro.TypeCode) = 10
				)
			) AS C
			INNER JOIN vProject ON vProject.PrjGuid = C.PrjGuid AND vProject.PrjState in(''5'',''7'',''8'',''9'',''10'',''11'',''12'',''13'',''17'')'
	IF (@isTender = '1')
		SET @sql2 = @sql2 + ' WHERE IsTender = ''1'''
	ELSE IF (@isTender = '0')
		SET @sql2 = @sql2 + ' WHERE IsTender = ''0'''
	SET @sql3 = '
		SELECT *, ROW_NUMBER() OVER(ORDER BY Date DESC, TypeCode ASC) AS No
		FROM(
			SELECT ' +@columns + ',
				CASE LEN(TypeCode) 
					WHEN 10 THEN (
						SELECT InputDate FROM vProject AS V1 WHERE V1.TypeCode = SUBSTRING(D.TypeCode, 1, 5)
					)
					WHEN 5 THEN InputDate
				END AS Date
			FROM (' + @sql2 + ') AS D 
		) AS E'

	SET @sql4 = '
		SELECT TOP(' + CAST(@pageSize AS nvarchar(20)) + ') * 
		FROM (' + @sql3 + ' ) AS F 
		WHERE No > ' + CAST( @pageSize * (@pageIndex - 1) AS nvarchar(20))+ '
		ORDER BY No ';
	EXEC(@sql + @sql4)

	EXEC(@sql + @sql3)
	SET @rowCount = @@ROWCOUNT
END
GO


--获取所有预算节点，包括审核通过对预算变更        wdd        2014-06-13
IF OBJECT_ID('usp_GetAllTaskByPrj') IS NOT NULL
	DROP PROC usp_GetAllTaskByPrj
GO
CREATE PROC usp_GetAllTaskByPrj
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
		WHERE PrjId = @prjId AND TaskType = '' AND IsValid=1		
	) AS T
	ORDER BY No
END
GO


--获取指定节点下的所有子节点，包括审核通过对预算变更       WDD     2014-05-06
IF OBJECT_ID('usp_GetAllTaskByParent') IS NOT NULL
	DROP PROC usp_GetAllTaskByParent
GO
CREATE PROC usp_GetAllTaskByParent
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
		WHERE T2.TaskId = @taskId AND T.OrderNumber LIKE T2.OrderNumber + '%' AND T.TaskType = '' AND T.IsValid=1	
	) AS T
END
GO