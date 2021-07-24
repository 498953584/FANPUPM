
---------内蒙古内容移植 日常办公-工作计划
------------------------科技进步管理-施工日志
------ slm   2011 10 14


/****** 工作日志表:  Table [dbo].[pm_Construction_Log]    脚本日期: 09/14/2009 10:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[pm_Construction_Log](
	[logID] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ProjectId] [varchar](50) NULL,
	[code] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[part] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[attendance] [int] NULL,
	[temperature] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[amweather] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[pmweather] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[operations] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[thisDate] [datetime] NULL,
	[daycontent] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[design] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[acceptance] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[beton] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[datum] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[product] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[situation] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_pm_Construction_log] PRIMARY KEY CLUSTERED 
(
	[logID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]


USE [JinB_8030]
GO
/****** 对象:  Table [dbo].[Pm_WorkPlan_WeekPlan]    脚本日期: 10/14/2011 10:27:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pm_WorkPlan_WeekPlan](
	[WkpId] [uniqueidentifier] NOT NULL,
	[WkpUserCode] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpDeptId] [varchar](500) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpRecordDate] [datetime] NOT NULL,
	[WkpReportUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpCheckerUser] [varchar](4000) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpIsReport] [tinyint] NOT NULL,
	[WkpReportType] [tinyint] NOT NULL,
	[WkpIsTrue] [int] NULL,
	[WkpRegisterUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_PM_WORKPLAN_WEEKPLAN] PRIMARY KEY CLUSTERED 
(
	[WkpId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户输入的编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpUserCode'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填写部门编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpDeptId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填报日期' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpRecordDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'填报人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpReportUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划查看人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpCheckerUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否上报' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpIsReport'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上报类型(周、月)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpReportType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否合格' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpIsTrue'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'当前登录用户' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan', @level2type=N'COLUMN',@level2name=N'WkpRegisterUser'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作计划—计划单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlan'




/****** 对象:  Table [dbo].[Pm_WorkPlan_WeekPlanDetails]    脚本日期: 10/14/2011 11:34:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pm_WorkPlan_WeekPlanDetails](
	[WkpDetailsId] [uniqueidentifier] NOT NULL,
	[WkpId] [uniqueidentifier] NOT NULL,
	[WkpContents] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpStartTime] [datetime] NOT NULL,
	[WkpEndTime] [datetime] NOT NULL,
	[WkpStandard] [nvarchar](2000) COLLATE Chinese_PRC_CI_AS NULL,
	[WkpPersons] [nvarchar](80) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpChief] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
 CONSTRAINT [PK_PM_WORKPLAN_WEEKPLANDETAILS] PRIMARY KEY CLUSTERED 
(
	[WkpDetailsId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划单信息编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpDetailsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划单主要内容' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpContents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本条计划开始时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpStartTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本条计划的结束时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpEndTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成标准' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpStandard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'责任人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpPersons'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'负责人' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails', @level2type=N'COLUMN',@level2name=N'WkpChief'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作计划—周计划详细单' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekPlanDetails'
GO
ALTER TABLE [dbo].[Pm_WorkPlan_WeekPlanDetails]  WITH CHECK ADD  CONSTRAINT [FK_WeekPlanDetails_REFERENCE_WeekPlan] FOREIGN KEY([WkpId])
REFERENCES [dbo].[Pm_WorkPlan_WeekPlan] ([WkpId])
GO
ALTER TABLE [dbo].[Pm_WorkPlan_WeekPlanDetails] CHECK CONSTRAINT [FK_WeekPlanDetails_REFERENCE_WeekPlan]



/****** 对象:  Table [dbo].[Pm_WorkPlan_PlanSummary]    脚本日期: 10/14/2011 11:51:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pm_WorkPlan_PlanSummary](
	[WkpPSId] [uniqueidentifier] NOT NULL,
	[WkpId] [uniqueidentifier] NOT NULL,
	[WkpRemarks] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[WkpSummary] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[WkpSelfScore] [tinyint] NULL,
	[WkpSummaryDate] [datetime] NULL,
 CONSTRAINT [PK_PM_WORKPLAN_PLANSUMMARY] PRIMARY KEY CLUSTERED 
(
	[WkpPSId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明与总结编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary', @level2type=N'COLUMN',@level2name=N'WkpPSId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划单编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary', @level2type=N'COLUMN',@level2name=N'WkpId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary', @level2type=N'COLUMN',@level2name=N'WkpRemarks'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划总结' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary', @level2type=N'COLUMN',@level2name=N'WkpSummary'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'总结自评分' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary', @level2type=N'COLUMN',@level2name=N'WkpSelfScore'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周总结说明时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary', @level2type=N'COLUMN',@level2name=N'WkpSummaryDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划的说明与总结' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_PlanSummary'
GO
ALTER TABLE [dbo].[Pm_WorkPlan_PlanSummary]  WITH CHECK ADD  CONSTRAINT [FK_PlanSummary_REFERENCE_WeekPlan] FOREIGN KEY([WkpId])
REFERENCES [dbo].[Pm_WorkPlan_WeekPlan] ([WkpId])
GO
ALTER TABLE [dbo].[Pm_WorkPlan_PlanSummary] CHECK CONSTRAINT [FK_PlanSummary_REFERENCE_WeekPlan]



/****** 对象:  Table [dbo].[Pm_WorkPlan_CheckInfo]    脚本日期: 10/14/2011 11:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Pm_WorkPlan_CheckInfo](
	[WkpCheckId] [int] IDENTITY(1,1) NOT NULL,
	[WkpId] [uniqueidentifier] NOT NULL,
	[WkpCheckerCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpCheckDate] [datetime] NOT NULL,
	[WkpCheckResult] [tinyint] NOT NULL,
 CONSTRAINT [PK_Pm_WorkPlan_CheckInfo] PRIMARY KEY CLUSTERED 
(
	[WkpCheckId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF



/****** 对象:  Table [dbo].[Pm_WorkPlan_WeekSummary]    脚本日期: 10/14/2011 12:04:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pm_WorkPlan_WeekSummary](
	[WkpSmId] [int] IDENTITY(1,1) NOT NULL,
	[WkpDetailsId] [uniqueidentifier] NOT NULL,
	[WkpSmContents] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[WkpPercent] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[WkpRecordDate] [datetime] NULL,
	[wkpid] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_PM_WORKPLAN_WEEKSUMMARY] PRIMARY KEY CLUSTERED 
(
	[WkpSmId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周总结编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekSummary', @level2type=N'COLUMN',@level2name=N'WkpSmId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周计划单信息编码' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekSummary', @level2type=N'COLUMN',@level2name=N'WkpDetailsId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'周总结情况' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekSummary', @level2type=N'COLUMN',@level2name=N'WkpSmContents'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'完成百分比' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekSummary', @level2type=N'COLUMN',@level2name=N'WkpPercent'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'单据录入时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekSummary', @level2type=N'COLUMN',@level2name=N'WkpRecordDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'工作计划—周总结' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Pm_WorkPlan_WeekSummary'
GO
ALTER TABLE [dbo].[Pm_WorkPlan_WeekSummary]  WITH CHECK ADD  CONSTRAINT [FK_WeekSummary_REFERENCE_WeekPlanDetails] FOREIGN KEY([WkpDetailsId])
REFERENCES [dbo].[Pm_WorkPlan_WeekPlanDetails] ([WkpDetailsId])
GO
ALTER TABLE [dbo].[Pm_WorkPlan_WeekSummary] CHECK CONSTRAINT [FK_WeekSummary_REFERENCE_WeekPlanDetails]

