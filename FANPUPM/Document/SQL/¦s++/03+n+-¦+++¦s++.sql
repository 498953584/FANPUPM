--SELECT * FROM sysobjects where xtype = 'U' AND name LIKE 'Sm_%'
--物资模块清空数据
DELETE FROM Sm_AlarmNum
DELETE FROM Sm_Allocation_Stock
DELETE FROM Sm_Allocation
DELETE FROM Sm_back_Stock
DELETE FROM Sm_out_Stock
DELETE FROM Sm_OutReserve

DELETE FROM Con_Balance_Stock  --

DELETE FROM Sm_Purchase_Stock
DELETE FROM Sm_Purchase
DELETE FROM Sm_Purchaseplan_Stock
DELETE FROM Sm_Purchaseplan
DELETE FROM Sm_Refunding
DELETE FROM Sm_Storage_Stock
DELETE FROM Sm_Storage
DELETE FROM Sm_Treasury_Permit
--DELETE FROM Sm_Treasury_Project
DELETE FROM Sm_Treasury_Stock
DELETE FROM Sm_Treasury
DELETE FROM Sm_Wantplan_Stock
DELETE FROM Sm_Wantplan
--盘点结存
DELETE FROM Sm_Stocktake
DELETE FROM Sm_Stocktake_Detail

----现场收货模块
delete from dbo.sm_receiveNote
delete from dbo.sm_receiveGoods
delete from dbo.sm_sendGoods
delete from dbo.sm_SendNote