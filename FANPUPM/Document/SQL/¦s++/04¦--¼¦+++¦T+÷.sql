
--SELECT * FROM sysobjects where xtype = 'U' AND name LIKE 'Con_%'


--请客合同模块
DELETE FROM Con_ContractPayend
DELETE FROM Con_Incomet_Balance
DELETE FROM Con_Incomet_Contract
DELETE FROM Con_Incomet_Modify
DELETE FROM Con_Incomet_Payment
DELETE FROM Con_Incomet_Invoice --收入合同发票
DELETE FROM Con_PayendFeedback
DELETE FROM Con_Payout_Balance
DELETE FROM Con_Payout_Contract
DELETE FROM Con_Payout_Modify
DELETE FROM Con_Payout_Payment
DELETE FROM Con_Payout_Invoice --支出合同发票

DELETE FROM Con_ContractType
DELETE FROM Con_Payout_Target --控制指标

DELETE FROM Con_Income_PaymentApply --资金支付
