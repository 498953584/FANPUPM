-- 九个里程碑菜单   gmd     2014-01-03   18:00
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '75')
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
VALUES('75','九个里程碑','','y',4,'MenuIco/13.gif',25010,1,'0','0','','1')
GO

--九个里程碑        gmd     2014-01-03    18:00
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '7501')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
VALUES('7501','九个里程碑','/PrjManager/PrjMilestoneList.aspx','y',1,'',25011,0,'0','0','','1')
GO

--工程项目进度及销售预测汇总表（人员）   gmd   2014-01-03   18:00
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '7502')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) 
VALUES('7502','工程项目进度及销售预测汇总表（人员）','/PrjManager/UserPrjMilestoneQuery.aspx','y',2,'',25012,0,'0','0','','1')
GO

--工程项目进度及销售预测汇总表（部门）    gmd   2014-01-03   18:00
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '7503')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
 VALUES('7503','工程项目进度及销售预测汇总表（部门）','/PrjManager/depmentPrjMilestoneQuery.aspx','y',2,'',25013,0,'0','0','','1')
 GO

 --工程项目进度变动表                     gmd    2014-01-03  18:00
IF NOT EXISTS(SELECT * FROM PT_MK WHERE C_MKDM = '7504')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay)
 VALUES('7504','工程项目进度变动表','/PrjManager/RptDatePrjMilestoneQuery.aspx','y',4,'',25014,0,'0','0','','1')
 GO

--修改九个里程碑菜单  wdd 2014-05-23     10:30

DECLARE @I_ID int
SET @I_ID=(SELECT MAX(I_ID)+1 FROM pt_mk);

BEGIN 
delete from PT_Role_Privilege where ModuleCode = '7502'
delete from PT_YHMC_Privilege where c_mkdm ='7502'
delete from PT_Manager_Privilege where C_MKDM = '7502'
delete from pt_mk where c_mkdm='7502'
update pt_mk set i_childNum = 
(select count(1) from pt_mk 
where (c_mkdm like '75%') and (len(c_mkdm)= 4)) where c_mkdm = '75'

delete from PT_Role_Privilege where ModuleCode = '7503'
delete from PT_YHMC_Privilege where c_mkdm ='7503'
delete from PT_Manager_Privilege where C_MKDM = '7503'
delete from pt_mk where c_mkdm='7503'
update pt_mk set i_childNum = 
(select count(1) from pt_mk 
where (c_mkdm like '75%') and (len(c_mkdm)= 4)) where c_mkdm = '75'

delete from PT_Role_Privilege where ModuleCode = '7504'
delete from PT_YHMC_Privilege where c_mkdm ='7504'
delete from PT_Manager_Privilege where C_MKDM = '7504'
delete from pt_mk where c_mkdm='7504'
update pt_mk set i_childNum = 
(select count(1) from pt_mk 
where (c_mkdm like '75%') and (len(c_mkdm)= 4)) where c_mkdm = '75'

INSERT INTO pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('7502','工程项目进度变动表','','y',2,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '75%') and (len(c_mkdm)= 4)) WHERE c_mkdm = '75' 

INSERT INTO pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750201','个人','/PrjManager/PrjMilestoneList.aspx','y',1,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7502%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7502' 

INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750202','公司月度','/PrjManager/DepMonthMileStoneList.aspx','y',2,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7502%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7502' 
 
INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750203','部门年度','/PrjManager/DepYearMileStoneList.aspx','y',3,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7502%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7502' 
 
INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750204','公司年度','/PrjManager/ComYearMileStoneList.aspx','y',4,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7502%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7502' 

INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('7503','工程项目进度及销售预测汇总表','','y',3,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '75%') and (len(c_mkdm)= 4)) WHERE c_mkdm = '75' 

INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750301','个人汇总','/PrjManager/UserPrjMilestoneQuery.aspx','y',1,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7503%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7503' 

INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750302','部门汇总','/PrjManager/depmentPrjMilestoneQuery.aspx','y',2,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7503%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7503' 

INSERT INTO  pt_mk (c_mkdm,v_mkmc,v_cdlj,c_bs,i_xh,i_id,v_img,i_childnum,IsBasic,IsMaintainable,helpPath) 
VALUES ('750303','公司汇总','/PrjManager/CompanyPrjMilestoneQuery.aspx','y',3,@I_ID,'',0,'0','0','') 
UPDATE pt_mk SET i_childNum = (SELECT count(1) FROM pt_mk WHERE (c_mkdm like '7503%') and (len(c_mkdm)= 6)) WHERE c_mkdm = '7503' 

END

GO