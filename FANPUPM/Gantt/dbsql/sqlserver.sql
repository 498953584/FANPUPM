CREATE TABLE [dbo].[plus_project] (
	[UID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[NAME_] [nvarchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[STARTDATE_] [datetime] NULL ,
	[FINISHDATE_] [datetime] NULL ,
	[LASTSAVED_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[CALENDARS_] [text] COLLATE Chinese_PRC_CI_AS NULL ,
	[CALENDARUID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[plus_resource] (
	[UID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,
	[NAME_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL ,	
	[TYPE_] [int] NULL ,
	[MAXUNITS_] [int] NULL ,
	[PROJECTUID_] [varchar] (50) COLLATE Chinese_PRC_CI_AS NULL 
) ON [PRIMARY]
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
		'[{"WeekDays":[{"DayWorking":0,"DayType":1},{"DayWorking":1,"DayType":2,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":3,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":4,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":5,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":6,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":0,"DayType":7}],"Name":"标准","UID":"1","BaseCalendarUID":"-1","IsBaseCalendar":1,"Exceptions":[]},{"WeekDays":[{"DayWorking":0,"DayType":1},{"DayWorking":0,"DayType":2},{"DayWorking":1,"DayType":3,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":4,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":5,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":1,"DayType":6,"WorkingTimes":[{"FromTime":"08:00:00","ToTime":"12:00:00"},{"FromTime":"13:00:00","ToTime":"17:00:00"}]},{"DayWorking":0,"DayType":7}],"Name":"新日历","UID":"11","BaseCalendarUID":"-1","IsBaseCalendar":1,"Exceptions":[]},{"WeekDays":[{"DayWorking":0,"DayType":1},{"DayWorking":1,"DayType":2,"WorkingTimes":[{"FromTime":"23:00:00","ToTime":"00:00:00"}]},{"DayWorking":1,"DayType":3,"WorkingTimes":[{"FromTime":"00:00:00","ToTime":"03:00:00"},{"FromTime":"04:00:00","ToTime":"08:00:00"},{"FromTime":"23:00:00","ToTime":"00:00:00"}]},{"DayWorking":1,"DayType":4,"WorkingTimes":[{"FromTime":"00:00:00","ToTime":"03:00:00"},{"FromTime":"04:00:00","ToTime":"08:00:00"},{"FromTime":"23:00:00","ToTime":"00:00:00"}]},{"DayWorking":1,"DayType":5,"WorkingTimes":[{"FromTime":"00:00:00","ToTime":"03:00:00"},{"FromTime":"04:00:00","ToTime":"08:00:00"},{"FromTime":"23:00:00","ToTime":"00:00:00"}]},{"DayWorking":1,"DayType":6,"WorkingTimes":[{"FromTime":"00:00:00","ToTime":"03:00:00"},{"FromTime":"04:00:00","ToTime":"08:00:00"},{"FromTime":"23:00:00","ToTime":"00:00:00"}]},{"DayWorking":1,"DayType":7,"WorkingTimes":[{"FromTime":"00:00:00","ToTime":"03:00:00"},{"FromTime":"04:00:00","ToTime":"08:00:00"}]}],"Name":"夜班","UID":"12","BaseCalendarUID":"-1","IsBaseCalendar":1,"Exceptions":[]}]'
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
