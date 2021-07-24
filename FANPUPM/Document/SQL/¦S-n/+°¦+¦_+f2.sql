--进度管理普加甘特图  Zhang Fujun  2012-2-27 

--资源表
IF OBJECT_ID ('plus_resource','U') IS NOT NULL
DROP TABLE plus_resource
GO
CREATE TABLE [dbo].[plus_resource] (
	[UID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[NAME_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,	
	[TYPE_] [int] NULL ,
	[MAXUNITS_] [int] NULL ,
	[PROJECTUID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
GO

--任务节点
IF OBJECT_ID('plus_task','U') IS NOT NULL
DROP TABLE plus_task
GO
CREATE TABLE [dbo].[plus_task] (
	[UID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ID_] [int] NULL ,
	[NAME_] [varchar] (200) COLLATE Chinese_PRC_CI_AS NULL ,
	[START_] [datetime] NULL ,
	[FINISH_] [datetime] NULL ,
	[DURATION_] [int] NULL ,
	[WORK_] [int] NULL ,
	[PERCENTCOMPLETE_] [int] NULL ,
	[WEIGHT_] [int] NULL ,
	[CONSTRAINTTYPE_] [int] NULL ,
	[CONSTRAINTDATE_] [datetime] NULL ,
	[MILESTONE_] [int] NULL ,
	[SUMMARY_] [int] NULL ,
	[CRITICAL_] [int] NULL ,
	[PRIORITY_] [int] NULL ,
	[NOTES_] [text] COLLATE Chinese_PRC_CI_AS NULL ,	
	[DEPARTMENT_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PRINCIPAL_] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	[PREDECESSORLINK_] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL ,
	[FIXEDDATE_] [int] NULL ,
	[PARENTTASKUID_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PROJECTUID_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ACTUALSTART_] [datetime] NULL ,
	[ACTUALFINISH_] [datetime] NULL ,
	[ACTUALDURATION_] [int] NULL ,
	[ASSIGNMENTS_] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
	[WBS_] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
	[CRITICAL2_] [int] NULL
) ON [PRIMARY]
GO

--项目试图（普加甘特图所需要的数据结构）
--修改：Zhang Fujun 2013-3-2
--      日历只保留标准日历
IF OBJECT_ID('plus_project') IS NOT NULL
	DROP VIEW plus_project
GO
CREATE VIEW plus_project
AS
(
	SELECT PrjGuid AS UID_,						--GUID
		PrjName AS NAME_,						--项目名称
		StartDate AS STARTDATE_,				--开始时间
		ISNULL(EndDate, GETDATE()) AS FINISHDATE_,	--结束时间
		GETDATE() AS LASTSAVED_,				--最后保存时间
		'[{"WeekDays":[{"DayWorking":0,"DayType":1},{"DayWorking":1,"DayType":2,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":3,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":4,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":5,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":6,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":0,"DayType":7}],"Name":"标准","UID":"1","BaseCalendarUID":"-1","IsBaseCalendar":1,"Exceptions":[]}]'
			AS CALENDARS_,						--日历数据(JSON字符串)
		1 AS CALENDARUID_						--项目日历UID
	FROM PT_PrjInfo
)
GO

--创建备份进度任务版本
IF OBJECT_ID('plus_BackTask','U') IS NOT NULL
DROP TABLE plus_BackTask
GO
CREATE TABLE [dbo].[plus_BackTask] (
	[UID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ID_] [int] NULL ,
	[NAME_] [varchar] (200) COLLATE Chinese_PRC_CI_AS NULL ,
	[START_] [datetime] NULL ,
	[FINISH_] [datetime] NULL ,
	[DURATION_] [int] NULL ,
	[WORK_] [int] NULL ,
	[PERCENTCOMPLETE_] [int] NULL ,
	[WEIGHT_] [int] NULL ,
	[CONSTRAINTTYPE_] [int] NULL ,
	[CONSTRAINTDATE_] [datetime] NULL ,
	[MILESTONE_] [int] NULL ,
	[SUMMARY_] [int] NULL ,
	[CRITICAL_] [int] NULL ,
	[PRIORITY_] [int] NULL ,
	[NOTES_] [text] COLLATE Chinese_PRC_CI_AS NULL ,	
	[DEPARTMENT_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PRINCIPAL_] [nvarchar] (500) COLLATE Chinese_PRC_CI_AS NULL ,
	[PREDECESSORLINK_] [nvarchar] (1000) COLLATE Chinese_PRC_CI_AS NULL ,
	[FIXEDDATE_] [int] NULL ,
	[PARENTTASKUID_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[PROJECTUID_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[ACTUALSTART_] [datetime] NULL ,
	[ACTUALFINISH_] [datetime] NULL ,
	[ACTUALDURATION_] [int] NULL ,
	[ASSIGNMENTS_] [nvarchar] (2000) COLLATE Chinese_PRC_CI_AS NULL,
	[WBS_] [nvarchar] (200) COLLATE Chinese_PRC_CI_AS NULL,
	[CRITICAL2_] [int] NULL,
    [Version] [int] NOT NULL
) ON [PRIMARY]
GO



--添加菜单
IF (SELECT COUNT(*) FROM PT_MK WHERE C_MKDM = '77') > 0
	DELETE FROM PT_MK WHERE C_MKDM like '77%'
GO
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
	VALUES('77','进度管理','','y',7,'MenuIco/13.gif',2361,5,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
	VALUES('7701','进度计划编制','/ProgressManage/Schema.aspx','y',1,'',2362,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
	VALUES('7702','总体进度视图','/ProgressManage/View.aspx','y',3,'',2363,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
	VALUES('7703','实际进度上报','/ProgressManage/Real.aspx','y',2,'',2365,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
	VALUES('7704','进度预警','/ProgressManage/Warn.aspx','y',5,'',2366,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
	VALUES('7705','期间进度视图','/ProgressManage/AreaView.aspx','y',4,'',2367,0,'0','0','','1')
GO
--进度管理预警信息插入 
IF OBJECT_ID('uspProgressWarning','P') IS NOT NULL
DROP PROCEDURE uspProgressWarning
GO
CREATE PROCEDURE [dbo].[uspProgressWarning]
AS
BEGIN
    --清空未查看的预警信息
    DELETE FROM PT_Warning WHERE IsValid=1;
	--插入预警信息
	WITH PrjInfo AS(
	SELECT Limit.PrjGuid,Limit.UserCode,PrjName FROM PT_PrjInfo_ZTB_User AS Limit
	LEFT JOIN PT_PrjInfo AS Project ON Project.PrjGuid =Limit.PrjGuid
	WHERE Limit.PrjGuid IN (SELECT  DISTINCT PROJECTUID_ FROM plus_task 
		WHERE (GETDATE()> START_ AND(ACTUALSTART_>START_ OR ACTUALSTART_ IS NULL)) 
		OR ( GETDATE()> FINISH_ AND( ACTUALFINISH_>FINISH_ OR ACTUALFINISH_ IS NULL)))
    )

	INSERT INTO PT_Warning(WarningTitle,WarningContent,UserCode,RelationsTable,RelationsColumn,RelationsKey
		,URI,IsValid,InputDate) 
		SELECT '项目：'+PrjName+' 进度预警','项目：'+PrjName+' 实际日期与计划日期不符',UserCode,'PT_PrjInfo','PrjGuid',PrjGuid
		,'/ProgressManage/Warn.aspx?prjId='+CONVERT(VARCHAR(100),PrjGuid),1,GETDATE() FROM PrjInfo
END
GO

--项目日历信息表 自定义项目的工作日和例外日期
--此表废除ToDo
IF OBJECT_ID('plus_ProjectCalendars','U') IS NOT NULL
DROP TABLE plus_ProjectCalendars
GO
CREATE TABLE plus_ProjectCalendars
(
	ProjectGuid VARCHAR(100) PRIMARY KEY,
	Calendars VARCHAR(MAX) NOT NULL
)
GO
--历史版本的项目信息
IF OBJECT_ID('plus_BackProject','U') IS NOT NULL
DROP TABLE plus_BackProject
GO
CREATE TABLE plus_BackProject
(
	ProjectGuid VARCHAR(100) NOT NULL,
	Start DATETIME NOT NULL,
	Finish DATETIME NOT NULL,
	Calendars VARCHAR(MAX) NOT NULL,
	Version INT NOT NULL
)
GO
--移除plus_resource    Zhang Fujun  2012-03-14
IF OBJECT_ID ('plus_resource','U') IS NOT NULL
	DROP TABLE plus_resource
GO
--移除plus_ProjectCalendars  Zhang Fujun  2012-03-14
IF OBJECT_ID('plus_ProjectCalendars','U') IS NOT NULL
	DROP TABLE plus_ProjectCalendars
GO


======================================================================================
--进度计划	Bery 2010-03-16
--DROP TABLE plus_progress
CREATE TABLE plus_progress ( 
	ProgressId nvarchar(50) NOT NULL,	--计划ID
	PrjId uniqueidentifier,				--项目ID
	ProgressName nvarchar(200),			--计划名称
	FlowState int,						--流程状态
	InputUser varchar(8),				--录入人(Code)
	InputDate datetime,					--录入时间
	Note nvarchar(max)					--备注
)
GO
ALTER TABLE plus_progress ADD CONSTRAINT PK_plus_progress 
	PRIMARY KEY CLUSTERED (ProgressId)
GO
ALTER TABLE plus_progress ADD CONSTRAINT FK_plus_progress_InputUser
	FOREIGN KEY(InputUser) REFERENCES PT_yhmc(v_yhdm) ON DELETE SET NULL
GO

--进度计划权限	Bery 2010-03-16
--DROP TABLE plus_progress_privilege
CREATE TABLE plus_progress_privilege ( 
	PrivilegeId nvarchar(50) NOT NULL,		--计划权限
	ProgressId nvarchar(50) NOT NULL,		--计划ID
	UserCode varchar(8)	NOT NULL			--用户编码
)
GO
ALTER TABLE plus_progress_privilege ADD CONSTRAINT PK_plus_progress_privilege 
	PRIMARY KEY CLUSTERED (PrivilegeId)
GO
ALTER TABLE plus_progress_privilege ADD CONSTRAINT FK_privilege_ProgressId
	FOREIGN KEY(ProgressId) REFERENCES plus_progress(ProgressId) ON DELETE CASCADE
GO
ALTER TABLE plus_progress_privilege ADD CONSTRAINT FK_privilege_UserCode
	FOREIGN KEY(UserCode) REFERENCES PT_yhmc(v_yhdm) ON DELETE CASCADE
GO

--计划版本	Bery 2010-03-16
--DROP TABLE plus_progress_version
CREATE TABLE plus_progress_version ( 
	ProgressVersionId nvarchar(50) NOT NULL,	--计划版本Id
	ProgressId nvarchar(50) NOT NULL,			--计划Id
	ParentVersionId nvarchar(50),				--上个版本ID
	VersionName nvarchar(200),					--版本名称
	VersionCode nvarchar(50),					--版本号, 第一个版本默认"v1.0"
	FlowState int,								--审核状态
	IsLatest bit,								--是否最新版本
	InputUser varchar(8),						--变更人
	InputDate datetime,							--变更时间
	Note nvarchar(max)							--变更原因
)
GO
ALTER TABLE plus_progress_version ADD CONSTRAINT PK_plus_progress_version 
	PRIMARY KEY CLUSTERED (ProgressVersionId)
GO
ALTER TABLE plus_progress_version ADD CONSTRAINT FK_progress_version_ProgressId
	FOREIGN KEY(ProgressId) REFERENCES plus_progress(ProgressId) ON DELETE CASCADE
GO
ALTER TABLE plus_progress_version ADD CONSTRAINT FK_progress_version_InputUser
	FOREIGN KEY(InputUser) REFERENCES PT_yhmc(v_yhdm) ON DELETE SET NULL
GO
ALTER  TABLE plus_BackProject ALTER COLUMN Version INT NULL
GO
--进度计划流程审核
INSERT INTO dbo.WF_BusinessCode(BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl
,LookUrl,C_MKDM,ProjectField) VALUES('107','进度计划审核','ProgressId','plus_progress','ProgressId','FlowState',
'/ProgressManagement/Plan/PlanView.aspx',null,'78','PrjId')
INSERT INTO dbo.WF_Business_Class VALUES('107','001','进度计划审核')
GO
--进度计划调整申请流程审核
INSERT INTO dbo.WF_BusinessCode(BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl
,LookUrl,C_MKDM) VALUES('108','进度计划调整审核','ProgressVersionId','plus_progress_version','ProgressVersionId','FlowState',
'/ProgressManagement/Modify/ApplyView.aspx',null,'78')
INSERT INTO dbo.WF_Business_Class VALUES('108','001','进度计划调整审核')
GO
--进度计划版本触发器，删除日历信息和进度WBS,进度提醒
IF OBJECT_ID('Tr_ProgressVersion_Del','TR') IS NOT NULL
DROP TRIGGER Tr_ProgressVersion_Del
GO
CREATE TRIGGER Tr_ProgressVersion_Del 
ON plus_progress_version AFTER DELETE
AS
BEGIN
	DECLARE @verId VARCHAR(100)
	SELECT @verId=ProgressVersionId FROM DELETED
	DELETE FROM Plus_BackProject WHERE ProjectGuid=@verId
	DELETE FROM Plus_task WHERE PROJECTUID_=@verId
	DELETE FROM PT_Warning WHERE RelationsKey=@verId
END
GO
--Begin Bery 2012-3-29
--存储过程, 进度计划滞后分析, 以项目为单位, 该项目下的所有进度计划的可执行版本
--查询结果集
IF OBJECT_ID('uspLagAnalysis') IS NOT NULL
	DROP PROC uspLagAnalysis
GO
CREATE PROC uspLagAnalysis 
	@userCode nvarchar(20),		--用户编码
	@prjName nvarchar(100),     --项目名称
	@pageIndex int,				--页码
	@pageSize int				--每页多少条记录    
AS
BEGIN
	WITH cteDateDiff AS (
		SELECT PrjId, ProgressVersionId, PROJECTUID_, START_, FINISH_, 
			ACTUALSTART_, ACTUALFINISH_, CONSTRAINTTYPE_, CONSTRAINTDATE_,
			DATEDIFF(DAY, ACTUALSTART_, START_) AS StartDiff,		--开始时间差
			DATEDIFF(DAY, ACTUALFINISH_, FINISH_) AS FinishDiff,	--结束时间差
			CASE CONSTRAINTTYPE_
				WHEN 2 THEN DATEDIFF(DAY, ACTUALSTART_ , CONSTRAINTDATE_)
				WHEN 5 THEN DATEDIFF(DAY, ACTUALSTART_ , CONSTRAINTDATE_)
			END AS ConsStartDiff,									--最迟开始时间差
			CASE CONSTRAINTTYPE_									
				WHEN 3 THEN DATEDIFF(DAY, ACTUALFINISH_, CONSTRAINTDATE_)
				WHEN 7 THEN DATEDIFF(DAY, ACTUALFINISH_ , CONSTRAINTDATE_)
			END AS ConsFinishDiff									--最迟结束时经常
		FROM plus_task
		LEFT JOIN plus_progress_version AS V ON V.ProgressVersionId = plus_task.PROJECTUID_
		LEFT JOIN plus_progress AS P ON P.ProgressId = V.ProgressId
		WHERE V.IsLatest = 1
	), cteLagCount AS (
		SELECT D.PrjId, 
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE StartDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagStart,					--晚于计划开始的数量			
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE FinishDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagFinish,					--晚于计划结束的数量
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE ConsStartDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagConsStart,				--晚于最迟开始的数量
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE ConsFinishDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagConsFinish,				--晚于最迟结束的数量
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE D2.PrjId = D.PrjId) 
				AS WBSCount												--当前项目下的所有节点的数量
		FROM cteDateDiff AS D
		GROUP BY PrjId
	),TResult AS(
		SELECT Prj.PrjName, L.PrjId,  ROW_NUMBER() OVER(ORDER BY Prj.RecordDate DESC) AS No,
			L.LagStart,														--晚于计划开始的数量
			CAST(L.LagStart AS decimal(18,6)) / WBSCount AS LagStartRate,	--晚于计划开始的比例
			L.LagFinish,													--晚于计划结束的数量
			CAST(L.LagFinish AS decimal(18,6)) / WBSCount AS LagFinishRate,	--晚于计划结束的比例
			L.LagConsStart,													--晚于最迟开始的数量
			CAST(L.LagConsStart AS decimal(18,6)) / WBSCount AS LagConsStartRate,	--晚于最迟开始的比例
			L.LagConsFinish,														--晚于最迟结束的数量
			CAST(L.LagConsFinish AS decimal(18,6)) / WBSCount AS LagConsFinishRate	--晚于最迟结束的比例
		FROM cteLagCount AS L
		INNER JOIN PT_PrjInfo AS Prj ON Prj.PrjGuid = L.PrjId
		INNER JOIN (SELECT PrjGuid,UserCode FROM PT_PrjInfo_ZTB_User GROUP BY PrjGuid,UserCode) AS PtUser ON Prj.PrjGuid=PtUser.PrjGuid
		WHERE PrjName LIKE '%'+@prjName+'%' AND PtUser.UserCode =@userCode
	)
	SELECT * FROM TResult  WHERE No BETWEEN (@pageIndex-1)*@pageSize+1 AND @pageIndex*@pageSize
END
GO
--滞后分析记录个数
IF OBJECT_ID('uspLagAnalysisCount') IS NOT NULL
	DROP PROC uspLagAnalysisCount
GO
CREATE PROC uspLagAnalysisCount 
	@userCode nvarchar(20),		--用户编码
	@prjName nvarchar(100)    --项目名称
AS
BEGIN
	WITH cteDateDiff AS (
		SELECT PrjId, ProgressVersionId, PROJECTUID_, START_, FINISH_, 
			ACTUALSTART_, ACTUALFINISH_, CONSTRAINTTYPE_, CONSTRAINTDATE_,
			DATEDIFF(DAY, ACTUALSTART_, START_) AS StartDiff,		--开始时间差
			DATEDIFF(DAY, ACTUALFINISH_, FINISH_) AS FinishDiff,	--结束时间差
			CASE CONSTRAINTTYPE_
				WHEN 2 THEN DATEDIFF(DAY, ACTUALSTART_ , CONSTRAINTDATE_)
				WHEN 5 THEN DATEDIFF(DAY, ACTUALSTART_ , CONSTRAINTDATE_)
			END AS ConsStartDiff,									--最迟开始时间差
			CASE CONSTRAINTTYPE_									
				WHEN 3 THEN DATEDIFF(DAY, ACTUALFINISH_, CONSTRAINTDATE_)
				WHEN 7 THEN DATEDIFF(DAY, ACTUALFINISH_ , CONSTRAINTDATE_)
			END AS ConsFinishDiff									--最迟结束时经常
		FROM plus_task
		LEFT JOIN plus_progress_version AS V ON V.ProgressVersionId = plus_task.PROJECTUID_
		LEFT JOIN plus_progress AS P ON P.ProgressId = V.ProgressId
		WHERE V.IsLatest = 1
	), cteLagCount AS (
		SELECT D.PrjId, 
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE StartDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagStart,					--晚于计划开始的数量			
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE FinishDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagFinish,					--晚于计划结束的数量
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE ConsStartDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagConsStart,				--晚于最迟开始的数量
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE ConsFinishDiff < 0 
				AND D2.PrjId = D.PrjId) AS LagConsFinish,				--晚于最迟结束的数量
			(SELECT COUNT(*) FROM cteDateDiff AS D2 WHERE D2.PrjId = D.PrjId) 
				AS WBSCount												--当前项目下的所有节点的数量
		FROM cteDateDiff AS D
		GROUP BY PrjId
	)

	SELECT COUNT(*) FROM cteLagCount AS L
	INNER JOIN PT_PrjInfo AS Prj ON Prj.PrjGuid = L.PrjId
	INNER JOIN (SELECT PrjGuid,UserCode FROM PT_PrjInfo_ZTB_User GROUP BY PrjGuid,UserCode) AS PtUser ON Prj.PrjGuid=PtUser.PrjGuid
	WHERE PrjName LIKE '%'+@prjName+'%' AND PtUser.UserCode =@userCode
END
GO
--End Bery 2012-3-29
--进度预警存储过程修改  Zhang Fujun 2012-3-31
--Begin
IF OBJECT_ID('uspProgressWarning','P') IS NOT NULL
DROP PROCEDURE uspProgressWarning
GO
CREATE PROCEDURE [dbo].[uspProgressWarning]
AS
BEGIN
    --清空未查看的预警信息
    DELETE FROM PT_Warning WHERE IsValid=1;
	--插入预警信息
	WITH Progress AS(
		SELECT PrjId,PrjName,ProgressVersionId,VersionName,VersionCode,P.UserCode,P.ProgressId
		FROM plus_progress_privilege AS P 
		INNER JOIN plus_progress AS P1 ON P.ProgressId=P1.ProgressId
		INNER JOIN plus_progress_version AS V ON P.ProgressId=V.ProgressId
		INNER JOIN PT_PrjInfo AS Prj ON P1.PrjId=Prj.PrjGuid
		WHERE V.IsLatest='1'
	),WarnProgress AS(
		SELECT PrjId,PrjName,ProgressId,ProgressVersionId,VersionName,VersionCode,UserCode,
			DATEDIFF(DAY, ACTUALSTART_, START_) AS StartDiff,		--开始时间差
			DATEDIFF(DAY, ACTUALFINISH_, FINISH_) AS FinishDiff,	--结束时间差
			CASE CONSTRAINTTYPE_
				WHEN 2 THEN DATEDIFF(DAY, ACTUALSTART_ , CONSTRAINTDATE_)
				WHEN 5 THEN DATEDIFF(DAY, ACTUALSTART_ , CONSTRAINTDATE_)
			END AS ConsStartDiff,									--最迟开始时间差
			CASE CONSTRAINTTYPE_									
				WHEN 3 THEN DATEDIFF(DAY, ACTUALFINISH_, CONSTRAINTDATE_)
				WHEN 7 THEN DATEDIFF(DAY, ACTUALFINISH_ , CONSTRAINTDATE_)
			END AS ConsFinishDiff	
		 FROM Progress 
		INNER JOIN plus_task AS T ON ProgressVersionId=T.PROJECTUID_
	)

	INSERT INTO PT_Warning(WarningTitle,WarningContent,UserCode,RelationsTable,RelationsColumn,RelationsKey
		,URI,IsValid,InputDate) 
		SELECT '项目['+PrjName+']的进度计划['+VersionName +'  '+VersionCode+']进度预警',
			   '项目['+PrjName+']的进度计划['+VersionName +'  '+VersionCode+']'+'实际日期与计划日期不符',UserCode,'plus_progress_version','ProgressVersionId',ProgressVersionId
		,'/ProgressManagement/Actual/PlanWarnDetail.aspx?verId='+ProgressVersionId,1,GETDATE() FROM WarnProgress
END
GO
--End Zhang Fujun 2012-3-31
--添加桌面进度柱状图模块
INSERT [frame_Desktop_ModelInfo] ([ModelID],[moreSrc]) VALUES ( '780501','/ProgressManagement/Analysis/Analysis.aspx')
GO
--进度菜单
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('78','进度管理','','y',8,'MenuIco/13.gif',2378,6,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7801','进度计划','','y',1,'',2379,2,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780101','计划编制','/ProgressManagement/Plan/Plan.aspx','y',1,'',2384,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780102','计划审核','/ProgressManagement/Plan/PlanRatify.aspx','y',2,'',2385,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7802','进度调整','','y',2,'',2380,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780201','调整申请','/ProgressManagement/Modify/Apply.aspx','y',1,'',2390,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780202','调整审核','/ProgressManagement/Modify/Ratify.aspx','y',2,'',2391,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780203','进度版本查询','/ProgressManagement/Modify/QueryVersion.aspx','y',3,'',2392,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7803','实际进度','','y',3,'',2381,2,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780301','实际进度上报','/ProgressManagement/Actual/Actual.aspx','y',1,'',2388,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780302','进度预警','/ProgressManagement/Actual/PlanWarn.aspx','y',2,'',2389,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7804','进度跟踪','','y',4,'',2382,4,'0','0','','1')
--INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780401','月度报告','/ProgressManagement/Track/Month.aspx','y',1,'',2396,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780402','总体进度','/ProgressManagement/Track/Track.aspx','y',2,'',2397,0,'0','0','','1')
--INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780403','里程碑','/ProgressManagement/Track/Milestone.aspx','y',3,'',2398,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780404','滞后分析','/ProgressManagement/Track/LagAnalysis.aspx','y',4,'',2399,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7805','进度分析','','y',5,'',2383,1,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780501','进度柱状图','/ProgressManagement/Analysis/Analysis.aspx','y',1,'',2402,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7806','计划权限','','y',6,'',2400,1,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('780601','进度计划权限','/ProgressManagement/Privilege/Privilege.aspx','y',1,'',2401,0,'0','0','','1')

--进度计划添加主计划标识    Bery    2012-04-06 08:48
ALTER TABLE plus_progress ADD IsMain bit NOT NULL DEFAULT(0)
GO
--进度管理-实际进度上报流程审核 By Zhang Fujun  Date：2012-05-15 10:39:40.127
--1.进度上报单据
IF OBJECT_ID('plus_report','U') IS NOT NULL
BEGIN 
  --删除引用表
	IF OBJECT_ID('FK_plus_report','F') IS NOT NULL
	ALTER TABLE plus_reportDetail DROP CONSTRAINT FK_plus_report

	DROP TABLE plus_report
END
GO
CREATE TABLE plus_report
(
	Id NVARCHAR(50) PRIMARY KEY, --主键Id
	FlowState NVARCHAR(2) DEFAULT('-1'),--流程审核状态
	InputDate DATETIME,--填报日期
	InputUser VARCHAR(8),--填报人编码
	Note NVARCHAR(2000),--上报说明
	ProVersionId NVARCHAR(50) --进度计划Id
)
GO
IF OBJECT_ID('FK_plus_progress_version','F') IS NOT NULL
ALTER TABLE plus_report DROP CONSTRAINT [FK_plus_progress_version]
GO
ALTER TABLE plus_report WITH CHECK ADD CONSTRAINT [FK_plus_progress_version] FOREIGN KEY  (ProVersionId) 
REFERENCES  [plus_progress_version](ProgressVersionId)
ON UPDATE CASCADE
ON DELETE CASCADE
GO
--##end

--2.进度上报单据明细
IF OBJECT_ID('plus_reportDetail') IS NOT NULL
DROP TABLE plus_reportDetail
GO
CREATE TABLE plus_reportDetail
(
	Id NVARCHAR(50) PRIMARY KEY,--主键Id
	ReportId NVARCHAR(50),--进度上报单据Id
	TaskUID NVARCHAR(20),--进度计划中任务的UID_
	Start DATETIME,--上报实际开始日期
	Finish DATETIME,--上报实际结束日期
	Completed TINYINT,--上报完成的（进度）百分比
	Note NVARCHAR(2000)--说明
)
GO
IF OBJECT_ID('FK_plus_report','F') IS NOT NULL
ALTER TABLE plus_reportDetail DROP CONSTRAINT FK_plus_report
GO
ALTER TABLE plus_reportDetail WITH CHECK ADD CONSTRAINT  [FK_plus_report] FOREIGN KEY(ReportId) 
REFERENCES [plus_report](Id)
ON UPDATE CASCADE
ON DELETE CASCADE
GO
--##end

--3.更新父节点的完成百分比
IF OBJECT_ID('trig_update_completed','TR') IS NOT NULL
DROP TRIGGER trig_update_completed
GO
CREATE TRIGGER trig_update_completed
ON plus_task AFTER UPDATE
AS
BEGIN
	IF (UPDATE(PERCENTCOMPLETE_) AND UPDATE(ACTUALSTART_) AND UPDATE(ACTUALFINISH_))
	BEGIN
		DECLARE @parentCompleted INT,@parentUID VARCHAR(50),@projectuid VARCHAR(100)
		SELECT @parentUID=PARENTTASKUID_,@projectuid=PROJECTUID_ FROM INSERTED
		SELECT @parentCompleted=SUM(PERCENTCOMPLETE_/100.00*DURATION_)/SUM(DURATION_)*100 
			FROM plus_task WHERE PARENTTASKUID_=@parentUID AND PROJECTUID_=@projectuid
		IF(@parentUID!='-1')
			UPDATE plus_task SET  PERCENTCOMPLETE_=ROUND(@parentCompleted,0),ACTUALSTART_=ACTUALSTART_,
								  ACTUALFINISH_=ACTUALFINISH_
				WHERE UID_=@parentUID AND PROJECTUID_=@projectuid 
	END
END
--##end

--4.添加实际进度上报流程
IF NOT EXISTS(SELECT * FROM WF_BusinessCode WHERE BusinessCode='111')
BEGIN
	INSERT INTO dbo.WF_BusinessCode(BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl
	,LookUrl,C_MKDM,ProjectField) VALUES('111','实际进度上报审核','Id','plus_report','Id','FlowState',
	'/ProgressManagement/Actual/reportView.aspx',null,'78','PrjId')
	INSERT INTO dbo.WF_Business_Class VALUES('111','001','实际进度上报审核')
END

--5.添加实际进度上报菜单 
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM='780303')
	INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
		VALUES('780303','实际进度上报','/ProgressManagement/Actual/ActualReport.aspx','y',1,'',2408,0,'0','0','','1')
--此菜单添加管理员权限
IF NOT EXISTS(SELECT * FROM PT_YHMC_Privilege WHERE C_MKDM='780303' AND V_YHDM='00000000')
	INSERT INTO PT_YHMC_Privilege(V_YHDM,C_MKDM,IsBasic,IsHaveOp) 
		VALUES('00000000','780303','0','0')
--删除原来的实际进度上报菜单
DELETE FROM PT_MK WHERE C_MKDM='780301' 
GO
--修改字段长度  By Zhang Fujun  Date:2012-05-15 14:51:50.597
ALTER TABLE plus_reportDetail ALTER COLUMN TaskUID NVARCHAR(50)



