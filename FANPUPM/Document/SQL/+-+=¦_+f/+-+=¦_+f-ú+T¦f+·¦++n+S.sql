USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Config]    脚本日期: 04/18/2012 16:27:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fund_Config](
	[ID] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ParaName] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[ParaValue] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[Notes] [nvarchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_fund_CONFIG] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'键' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Config', @level2type=N'COLUMN', @level2name=N'ParaName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'值' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Config', @level2type=N'COLUMN', @level2name=N'ParaValue'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'说明' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Config', @level2type=N'COLUMN', @level2name=N'Notes'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Plan_MonthDetail]    脚本日期: 04/18/2012 16:27:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Plan_MonthDetail](
	[UID] [uniqueidentifier] NOT NULL,
	[MonthPlanID] [uniqueidentifier] NULL,
	[ContractID] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[Plansubject] [varchar](30) COLLATE Chinese_PRC_CI_AS NULL,
	[PlanMoney] [decimal](18, 2) NULL,
	[OldBalance] [decimal](18, 2) NULL,
	[OrderID] [int] NULL,
	[ReMark] [nvarchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[ThisBalance]  AS (isnull([PlanMoney],(0))+isnull([OldBalance],(0))),
 CONSTRAINT [PK_Fund_Plan_MonthDetail_1] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'月计划ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthDetail', @level2type=N'COLUMN', @level2name=N'MonthPlanID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'依据合同' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthDetail', @level2type=N'COLUMN', @level2name=N'ContractID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上期结余' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthDetail', @level2type=N'COLUMN', @level2name=N'OldBalance'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthDetail', @level2type=N'COLUMN', @level2name=N'OrderID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本期结余' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthDetail', @level2type=N'COLUMN', @level2name=N'ThisBalance'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Plan_MonthMain]    脚本日期: 04/18/2012 16:27:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Plan_MonthMain](
	[MonthPlanID] [uniqueidentifier] NOT NULL,
	[PrjGuid] [uniqueidentifier] NOT NULL,
	[PlanMonth] [int] NOT NULL,
	[PlanYear] [int] NOT NULL,
	[FlowState] [int] NULL,
	[Remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[OperatorCode] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[OperateTime] [datetime] NULL,
	[PlanType] [varchar](10) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PlanDate]  AS (CONVERT([datetime],((CONVERT([varchar](4),[PlanYear],(0))+'-')+CONVERT([varchar](2),[PlanMonth],(0)))+'-01',(0))),
 CONSTRAINT [PK_Fund_Plan_MonthMain] PRIMARY KEY CLUSTERED 
(
	[PrjGuid] ASC,
	[PlanMonth] ASC,
	[PlanYear] ASC,
	[PlanType] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [IX_Fund_Plan_MonthMain] UNIQUE NONCLUSTERED 
(
	[MonthPlanID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属项目' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthMain', @level2type=N'COLUMN', @level2name=N'PrjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划月份' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthMain', @level2type=N'COLUMN', @level2name=N'PlanMonth'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划年' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthMain', @level2type=N'COLUMN', @level2name=N'PlanYear'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthMain', @level2type=N'COLUMN', @level2name=N'FlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划类型 支出：payout；收入：income' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_MonthMain', @level2type=N'COLUMN', @level2name=N'PlanType'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Plan_Summary_Detail]    脚本日期: 04/18/2012 16:27:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Plan_Summary_Detail](
	[DID] [uniqueidentifier] NOT NULL,
	[MID] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PlanID] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PrjID] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[LastPlanMoney] [decimal](18, 2) NULL,
	[LastActualMoney] [decimal](18, 2) NULL,
	[CurrPlanMoney] [decimal](18, 2) NULL,
	[InputPeople] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[InputTime] [datetime] NULL,
	[Remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_FUND_PLAN_SUMMARY_DETAIL] PRIMARY KEY CLUSTERED 
(
	[DID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'DID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Main表Id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'MID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'对应月计划Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'PlanID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'PrjID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上期计划金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'LastPlanMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上期实际发生金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'LastActualMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'本期计划金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'CurrPlanMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'InputPeople'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'录入时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'InputTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail', @level2type=N'COLUMN', @level2name=N'Remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fund_Plan_Summary_Detail计划汇总详细表' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Detail'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Plan_Summary_Main]    脚本日期: 04/18/2012 16:27:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Plan_Summary_Main](
	[MID] [uniqueidentifier] NOT NULL,
	[PlanName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Reporter] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ReportTime] [datetime] NULL,
	[FlowState] [int] NULL,
	[remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[PlanType] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_FUND_PLAN_SUMMARY_MAIN] PRIMARY KEY CLUSTERED 
(
	[MID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'MID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划月份' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'PlanName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上报人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'Reporter'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'上报日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'ReportTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'FlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划类型 支出：payout；收入：income ' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main', @level2type=N'COLUMN', @level2name=N'PlanType'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Fund_Plan_Summary_Main计划汇总主表' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Plan_Summary_Main'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Prj_Accoun]    脚本日期: 04/18/2012 16:27:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Prj_Accoun](
	[AccountID] [uniqueidentifier] NOT NULL,
	[PrjGuid] [varchar](max) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[accountNum] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[acountName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[creatDate] [datetime] NULL,
	[createMan] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[activeDate] [datetime] NULL,
	[activeMan] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[authorizer] [varchar](5000) COLLATE Chinese_PRC_CI_AS NULL,
	[closeDate] [datetime] NULL,
	[closeMan] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[initialFund] [decimal](18, 2) NULL,
	[AccountState] [int] NULL,
	[FlowState] [int] NULL,
	[moneyRate] [decimal](18, 4) NULL,
	[IncomeFund] [decimal](18, 2) NULL,
	[PayoutFund] [decimal](18, 2) NULL,
	[CurrentFund] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Fund_Prj_Accoun] PRIMARY KEY CLUSTERED 
(
	[AccountID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属项目' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'PrjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户编号：默认格式' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'accountNum'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户别名：默认项目名称' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'acountName'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'creatDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'createMan'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'激活日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'activeDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'激活人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'activeMan'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'授权成员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'authorizer'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注销日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'closeDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'注销人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'closeMan'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'Remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目启动资金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'initialFund'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'0:未激活；1激活；2注销' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'AccountState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态：主要审核项目启动资金，通过后才能进行启动注销' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'FlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'利率（预留）' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'moneyRate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入资金(预留字段)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'IncomeFund'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支出资金(预留字段)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'PayoutFund'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'账户当前可用资金(预留字段)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun', @level2type=N'COLUMN', @level2name=N'CurrentFund'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Prj_Accoun_Income]    脚本日期: 04/18/2012 16:27:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Prj_Accoun_Income](
	[InUid] [uniqueidentifier] NOT NULL,
	[PrjGuid] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[InCode] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[PlanUid] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[ContractID] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[Subject] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[GetDate] [datetime] NULL,
	[GetMoney] [decimal](18, 2) NULL,
	[InMoney] [decimal](18, 2) NULL,
	[InPeople] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[InDate] [datetime] NULL,
	[Remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[FlowState] [int] NULL,
 CONSTRAINT [PK_FUND_PRJ_ACCOUN_INCOME] PRIMARY KEY CLUSTERED 
(
	[InUid] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主键   Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'InUid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目uid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'PrjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账单号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'InCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划单uid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'PlanUid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'合同uid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'ContractID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收入类型  其他收入:2；启动资金：1；合同入账：0' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'Subject'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'到款日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'GetDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'收款金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'GetMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'InMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'InPeople'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'InDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'Remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income', @level2type=N'COLUMN', @level2name=N'FlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账管理' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Income'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Prj_Accoun_Payout]    脚本日期: 04/18/2012 16:27:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Prj_Accoun_Payout](
	[PayOutGuid] [uniqueidentifier] NOT NULL,
	[PayOutCode] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[prjGuid] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[RPGuid] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[PayOutMoney] [decimal](18, 2) NULL,
	[PayOutPeople] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[PayOutTime] [datetime] NULL,
	[Handler] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[FloeState] [int] NULL,
	[Remark] [varchar](max) COLLATE Chinese_PRC_CI_AS NULL,
	[UpdateUser] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[UpdateTime] [datetime] NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_FUND_PRJ_ACCOUN_PAYOUT] PRIMARY KEY CLUSTERED 
(
	[PayOutGuid] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账单Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'PayOutGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账单号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'PayOutCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'项目Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'prjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'关联单据Guid' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'RPGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'PayOutMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'PayOutPeople'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'PayOutTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经手人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'Handler'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'FloeState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'Remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'UpdateUser'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'最后操作时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'UpdateTime'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'入账类型：直接费用，0.间接费用：1；' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout', @level2type=N'COLUMN', @level2name=N'Type'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'支出入账' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Accoun_Payout'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Prj_Loan]    脚本日期: 04/18/2012 16:28:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Prj_Loan](
	[LoanID] [uniqueidentifier] NOT NULL,
	[LoanCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[PrjGuid] [uniqueidentifier] NOT NULL,
	[LoanDate] [datetime] NULL,
	[LoanMan] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[LoanFund] [decimal](18, 2) NULL,
	[LoanCause] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[PlanReturnDate] [datetime] NULL,
	[LoanRate] [decimal](18, 4) NULL,
	[Remark] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[FlowState] [int] NULL,
	[ReturnDate] [datetime] NULL,
	[ReturnMan] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Returnteller] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ReturnRemark] [varchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[ReturnFlowState] [int] NULL,
	[ReturnState] [int] NULL,
 CONSTRAINT [PK_Fund_Prj_Loan] PRIMARY KEY CLUSTERED 
(
	[LoanID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款单号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'LoanCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属项目：选择账户带过老的' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'PrjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'LoanDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款人员' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'LoanMan'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'LoanFund'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款用途' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'LoanCause'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'计划归还日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'PlanReturnDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款利率' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'LoanRate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款审核状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'FlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'ReturnDate'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还人员(默认借款人预留)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'ReturnMan'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还经手人（出纳-操作者）' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'Returnteller'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'ReturnRemark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款审核状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'ReturnFlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款状态 0 未还清 1 已还清' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan', @level2type=N'COLUMN', @level2name=N'ReturnState'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_Prj_Loan_Return]    脚本日期: 04/18/2012 16:28:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_Prj_Loan_Return](
	[FR_id] [uniqueidentifier] NOT NULL,
	[LoanID] [uniqueidentifier] NOT NULL,
	[FR_name] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[FR_data] [datetime] NULL,
	[FR_Money] [decimal](18, 2) NULL,
	[FR_interest] [decimal](18, 2) NULL,
	[FR_deduct] [decimal](18, 2) NULL,
	[FR_remark] [varchar](1000) COLLATE Chinese_PRC_CI_AS NULL,
	[FR_flowState] [int] NULL,
	[FR_user] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[FR_Time] [datetime] NULL,
	[FR_Code] [nchar](14) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Fund_Return] PRIMARY KEY CLUSTERED 
(
	[FR_id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'还款记录id' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_id'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'借款记录编号' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'LoanID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还人(经手人)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_name'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还日期' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_data'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'归还本金' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_Money'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'利息' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_interest'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'扣款' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_deduct'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'备注' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_remark'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'流程状态(预留)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_flowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'记录填报人' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_user'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'操作时间' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_Prj_Loan_Return', @level2type=N'COLUMN', @level2name=N'FR_Time'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_RequestPay_Detail]    脚本日期: 04/18/2012 16:28:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_RequestPay_Detail](
	[RPayMainID] [uniqueidentifier] NULL,
	[RPayUID] [uniqueidentifier] NOT NULL,
	[PlanUID] [uniqueidentifier] NULL,
	[ContractID] [nvarchar](64) COLLATE Chinese_PRC_CI_AS NULL,
	[RPaysubject] [varchar](30) COLLATE Chinese_PRC_CI_AS NULL,
	[RPayMoney] [money] NULL,
	[ReMark] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
	[RPayUser] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[OrderID] [int] NULL,
	[RPayCause] [text] COLLATE Chinese_PRC_CI_AS NULL,
	[isInterest] [bit] NULL,
 CONSTRAINT [PK_Fund_RequestPayment] PRIMARY KEY CLUSTERED 
(
	[RPayUID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'主表ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'RPayMainID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'明细ID' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'RPayUID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'依据合同' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'ContractID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请款金额' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'RPayMoney'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'经手人(直接填)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'RPayUser'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'序号（预留）' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'OrderID'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请款原因(预留)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'RPayCause'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'是否利息(预留)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Detail', @level2type=N'COLUMN', @level2name=N'isInterest'

USE [pm2_dev]
GO
/****** 对象:  Table [dbo].[Fund_RequestPay_Main]    脚本日期: 04/18/2012 16:28:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Fund_RequestPay_Main](
	[RPayMainID] [uniqueidentifier] NOT NULL,
	[RPayCode] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[PrjGuid] [uniqueidentifier] NOT NULL,
	[FlowState] [int] NULL,
	[RPayUserCode] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[RPayDate] [datetime] NULL,
	[ReMark] [varchar](100) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Fund_RequestPay_Main] PRIMARY KEY CLUSTERED 
(
	[RPayMainID] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请款单编号(按时间自动生成，允许调整)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Main', @level2type=N'COLUMN', @level2name=N'RPayCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'所属项目' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Main', @level2type=N'COLUMN', @level2name=N'PrjGuid'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'审核状态' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Main', @level2type=N'COLUMN', @level2name=N'FlowState'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请款人代码' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Main', @level2type=N'COLUMN', @level2name=N'RPayUserCode'

GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'请款时间(提交审核前允许调整)' ,@level0type=N'SCHEMA', @level0name=N'dbo', @level1type=N'TABLE', @level1name=N'Fund_RequestPay_Main', @level2type=N'COLUMN', @level2name=N'RPayDate'
