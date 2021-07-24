----计划流程




insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('090','资金收入计划','MonthPlanID','Fund_Plan_MonthMain','MonthPlanID','FlowState','/Fund/MonthPlan/MonthPlanView.aspx',Null,'31','PrjGuid','convert(varchar(7),[PlanDate],120)')
insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('091','资金支出计划','MonthPlanID','Fund_Plan_MonthMain','MonthPlanID','FlowState','/Fund/MonthPlan/MonthPlanView.aspx',Null,'31','PrjGuid','convert(varchar(7),[PlanDate],120)')

insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('090','001','资金收入计划')
insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('091','001','资金支出计划')


----入账流程



insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('093','资金收入入账管理','Inuid','Fund_Prj_Accoun_Income','Inuid','FlowState','/Fund/AccounIncome/AccounIncomeView.aspx',Null,'31','Inuid','Incode')

insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('093','001','资金收入入账管理')


----入账流程



insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('098','资金支出入账管理','PayOutGuid','Fund_Prj_Accoun_Payout','PayOutGuid','FloeState','/Fund/AccountPayOut/AccountPayoutView.aspx',Null,'31','Inuid','Incode')

insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('098','001','资金支出入账管理')


----入账流程



insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('094','资金间接费用入账管理','PayOutGuid','Fund_Prj_Accoun_Payout','PayOutGuid','FloeState','/Fund/AccountPayOut/AccountPayoutView.aspx',Null,'31','Inuid','Incode')

insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('094','001','资金间接费用入账管理')




------支出计划汇总入账


insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('104','支出计划汇总审核','MID','Fund_Plan_Summary_Main','MID','FlowState','/Fund/PlanSummary/PlanSummaryView.aspx',Null,'31',Null,'PlanName')

insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('104','001','支出计划汇总审核')

------收入计划汇总审核


insert into WF_BusinessCode ( Businesscode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,C_MKDM,ProjectField,NameField )
values('105','收入计划汇总审核','MID','Fund_Plan_Summary_Main','MID','FlowState','/Fund/PlanSummary/PlanSummaryView.aspx',Null,'31',Null,'PlanName')

insert into WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName)
values('105','001','收入计划汇总审核')





----视图



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create VIEW [dbo].[cont_sum_paid]
AS
SELECT     ContractID, ContractName, BName, ContractMoney + ModifiedMoney AS contMoney,
                          (SELECT     SUM(PaymentMoney) AS Expr1
                            FROM          dbo.Con_Payout_Payment
                            WHERE      (FlowState = 1) AND (ContractID = c.ContractID)) AS PaymentMoney,
                          (SELECT     SUM(BalanceMoney) AS Expr1
                            FROM          dbo.Con_Payout_Balance
                            WHERE      (FlowState = 1) AND (ContractID = c.ContractID)) AS BalanceMoney
FROM         dbo.Con_Payout_Contract AS c
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create VIEW [dbo].[cont_sum_income]
AS
SELECT     ContractID, ContractName, Subscriber AS BName, ContractPrice AS contMoney,
                          (SELECT     SUM(CllectionPrice) AS Expr1
                            FROM          dbo.Con_Incomet_Payment
                            WHERE      (ContractID = c.ContractID)) AS PaymentMoney,
                          (SELECT     SUM(ClearingPrice) AS Expr1
                            FROM          dbo.Con_Incomet_Balance
                            WHERE      (ContractID = c.ContractID)) AS BalanceMoney
FROM         dbo.Con_Incomet_Contract AS c
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO


