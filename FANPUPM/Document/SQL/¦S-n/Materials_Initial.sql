
----------------------------------------------------------增加库存基本设置记录20100610 zxd 
delete from Sm_Set
insert into Sm_Set values(newid(),'DepotType','TotalMode','ParallelMode表示平行模式，TotalMode表示总分模式')
insert into Sm_Set values(newid(),'DepotBindType','UnBind','')
insert into Sm_Set values(newid(),'ProjectAlarm','Alarm','Alarm表示预警，UnAlarm表示拒绝')
insert into Sm_Set values(newid(),'ProjectTransparentSet','UnTransparent','')
insert into Sm_Set values(newid(),'IsLowAlarm','0','0表示不预警，1表示预警')
insert into Sm_Set values(newid(),'IsHighPlanAlarm','1','0表示高物资需求计划不预警，1表示高物资需求计划预警')
insert into Sm_Set values(newid(),'HighPlanAlarmLimit','30','高计划预警限度')
insert into Sm_Set values(newid(),'IsMidPlanAlarm','1','是否中物资需求计划预警')
insert into Sm_Set values(newid(),'MidPlanAlarmUpperLimit','20','中计划预警上限')
insert into Sm_Set values(newid(),'MidPlanAlarmLowerLimit','10','中计划预警下限')
insert into Sm_Set values(newid(),'IsLowPlanAlarm','1','是否低物资需求计划预警')
insert into Sm_Set values(newid(),'LowPlanAlarmUpperLimit','10','低计划预警上限')
insert into Sm_Set values(newid(),'LowPlanAlarmLowerLimit','0','低计划预警下限')
insert into Sm_Set values(newid(),'IsContainPending','0','是否包含待审数据')
go

----------------------------------------------------------物资模块流程审核 

--配置采购计划审核流程 2010-7-19 李真
insert dbo.WF_BusinessCode values('072','采购计划','ppid','dbo.Sm_Purchaseplan','ppid','flowstate','/StockManage/SmPurchaseplan/ViewSmPurchaseplan.aspx',null,'03')
insert dbo.WF_Business_Class values('072','001','采购计划审核')

--出库管理审核流程 2010-7-19 李真
insert dbo.WF_BusinessCode values('076','出库管理','orid','dbo.Sm_OutReserve','orid','flowstate','/StockManage/SmOutReserve/ViewReserve.aspx',null,'03')
insert dbo.WF_Business_Class values('076','001','出库管理审核')

--退库管理审核流程 2010-7-19 李真
insert dbo.WF_BusinessCode values('077','退库管理','rid','dbo.Sm_Refunding','rid','flowstate','/StockManage/Refunding/ViewRefunding.aspx',null,'03')
insert dbo.WF_Business_Class values('077','001','退库管理审核')

--物资需求计划审核流程 2010-7-19  马百利
insert dbo.WF_BusinessCode values('071','物资需求计划','swid','Sm_Wantplan','swid','flowstate','/StockManage/basicset/WantplanView.aspx',null,'03')
insert dbo.WF_Business_Class values('071','001','物资需求计划审核')

--采购单审核流程 2010-7-19  马百利
insert dbo.WF_BusinessCode values('073','采购管理','pid','Sm_Purchase','pid','flowstate','/StockManage/Purchase/PurchaseView.aspx',null,'03')
insert dbo.WF_Business_Class values('073','001','采购管理审核')

--入库单审核流程 2010-7-19  马百利
insert dbo.WF_BusinessCode values('074','入库管理','sid','Sm_Storage','sid','flowstate','/StockManage/Storage/StorageView.aspx',null,'03')
insert dbo.WF_Business_Class values('074','001','入库管理审核')

--调拨单管理审核流程 2010-7-21  何亚坤
insert dbo.WF_BusinessCode values('075','调拨单管理','aid','Sm_Allocation','aid','flowstate','/StockManage/Allocation/AuditPage.aspx',null,'03')
insert dbo.WF_Business_Class values('075','001','调拨单审核')
go


----------------------------------------------------------物资模块添加附件


--采购单添加附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1801','Purchase','采购单',8388608,'*','/UploadFiles/StockManage/Purchase/',8)

--采购计划添加附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1756','SmPurchaseplan','采购计划',8388608,'*','/UploadFiles/StockManage/SmPurchaseplan/',8)

--入库单添加附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1802','Storage','入库单',8388608,'*','/UploadFiles/StockManage/Storage/',8)

--物资需求计划添加附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1803','WantPlan','物资需求计划',8388608,'*','/UploadFiles/StockManage/WantPlan/',8)

--出库单添加附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1804','OutReserve','出库单',8388608,'*','/UploadFiles/StockManage/OutReserve/',8)

--退库单添加附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('1805','Refunding','退库单',8388608,'*','/UploadFiles/StockManage/Refunding/',8)
----调拨单附件
insert into XPM_Basic_AnnexSettings(ModuleID,ModuleCode,ModuleName,FileSize,ExtName,FilePath,FileNumber)
values('89','Allocation','调拨单',8388608,'*','/UploadFiles/StockManage/Allocation/',8)
go


----------------------------------------------------------预警提示添加相应的类型 2010-7-19 李真
insert into dbo.PT_D_TXLX(V_LXBM,V_LXMC,V_TPLJ,V_DBLJ,C_OPENFLAG,FilterField)
values('021','预警提醒','new_Mail.gif','StockManage/basicset/ShowView.aspx',1,'StockManage')
go

----------------------------------------------------------向表Sm_Resource_PriceType插入数据 此数据是默认的数据，特殊处理
INSERT INTO	Sm_Resource_PriceType(rptid,rptcode,rptname,rptIsShow,isdefault)
values('24898687-9BE9-4E0B-AE8D-3BB54492BC9C','00000000','预算',1,1)
go