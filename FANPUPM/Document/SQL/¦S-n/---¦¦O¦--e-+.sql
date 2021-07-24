
--唯一键约束， 提示已经存在可继续执行下面的SQL
IF OBJECT_ID('UQ_PrjGuid', 'UQ') IS NOT NULL
	ALTER TABLE PT_PrjInfo DROP CONSTRAINT UQ_PrjGuid
GO
	ALTER TABLE PT_PrjInfo ADD CONSTRAINT UQ_PrjGuid UNIQUE(PrjGuid)
GO

--流程审核添加项目关联字段
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
	WHERE TABLE_NAME = 'WF_BusinessCode' AND COLUMN_NAME = 'ProjectField') = 0
BEGIN
	ALTER TABLE WF_BusinessCode ADD ProjectField varchar(50) NULL
END

----人力资源添加以往工作表现
IF (SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS 
	WHERE TABLE_NAME = 'PT_yhmc' AND COLUMN_NAME = 'PastPerformance') = 0
BEGIN
	ALTER TABLE PT_yhmc ADD PastPerformance nvarchar(200)
END 

--新投标、项目模块 Bery 2012-01-13 09:16
--增加项目投标表
CREATE TABLE PT_PrjInfo_ZTB_Detail
(
    PrjGuid uniqueidentifier not null PRIMARY KEY, --项目Id
    ProjPeopleName nvarchar(200) null,--立项人姓名
    ProjPeopleSex char(1) null, --立项人性别 1：男  0：女
    ProjPeopleAge int null,  --立项人年龄
    ProjPeopleTel nvarchar(200) null, --立项人联系方式
    ProjInfoOrigin nvarchar(500) null, --信息来源
    ProjElseRequest nvarchar(max) null, --其他特殊要求
    ProjApplyDate datetime null, --报名日期
    ProjTenderDate datetime null, --投标日期
    ProjApprovalDate datetime null, --预审日期
    ProjRegistDeadline int null, --登记期限
    ProgAgent varchar(8) null ,--REFERENCES PT_yhmc(v_yhdm),-- ON DELETE SET NULL, --经办人
	BuildingTypeNo int null, --施工类型级别

    --启动信息
    ProjStartDate datetime null,--项目启动时间
    BusinessManager varchar(8) null ,--REFERENCES PT_yhmc(v_yhdm),-- ON DELETE SET NULL, --业务经理(跟进人) 
    ProjStartRemark nvarchar(Max) null, --项目启动备注
    
    --投标信息
    ProjTenderBeginDate datetime null, --项目投标开标时间
    --开标情况
    TenderCeilingPrice decimal(18,3) null, --最高限价
    TenderUnit nvarchar(200) null, --单位
    TenderQuote decimal(18,3)  null, --报价
    TenderAppraiseMethod nvarchar(500), --评比方法
    TenderAverage decimal(18,3), --平均价 
    ProjTenderCostContent nvarchar(500) null, --项目投标现场费内容
    ProjTenderAnswerDate datetime null, --项目投标答疑时间
    ProjTenderEarnestMoney decimal(18,3) null, --项目投标保证金
    ProjTenderPayWay nvarchar(50) null, --保证金缴纳方式
    ProjTenderContent nvarchar(500) null, --项目投标标书内容
    ProjTenderRemark nvarchar(Max) null, --项目投标备注

    --中标信息
    SuccessBidDate datetime null,--中标时间
    SuccessBidPrice decimal(18,3) null, --中标价格
    SuccessBidRemark nvarchar(Max) null, --中标备注

    --落标信息
    OutBidDate datetime null, --落标时间
    OutBidIsReturn bit null, --落标保证金是否退取
    OutBidRemark nvarchar(Max) null, --落标备注

    ProjFlowSate int null, --流程审核状态
	EngineeringType nvarchar(20), --工程类型
	EngineeringSubType nvarchar(20),--工程类型子项
	Grade nvarchar(64), --电压等级
	Telephone nvarchar(64), --项目经理联系方式
	IsTender bit NOT NULL, --是否投标项目
    OwnerLinkMan nvarchar(30), --建设单位联系人
    OwnerLinkPhone nvarchar(30), --建设单位联系方式
	ActualRunDate datetime, --实际开工日期

	PrjDutyPerson nvarchar(30), --项目责任人
	PrjApprovalOf nvarchar(30), --项目审核情况
	PrjFundWorkable nvarchar(200), --资金落实情况
	ForecastProfitRate decimal(18,3), --预测利润率
	EngineeringEstimates nvarchar(30), --工程量估算 
	--管理参数
	ManagementMargin decimal(18,3), --管理保证金
	MigrantQualityMarginRate decimal(18,3), --民工质量保证金率
	WithholdingTaxRate decimal(18,3), --预扣税率
	PerformanceBond decimal(18,3), --履约保证金
	ElseMargin decimal(18,3), --其他（保证金）
	
	CompletedFlowState int DEFAULT(-1), --竣工流程状态
	CompletedDate datetime, --竣工日期
	CompletedNote nvarchar(max), --竣工备注

    MemberFlowState int, --项目小组成员审核
	InputUser nvarchar(20), --录入人
	InputDate datetime DEFAULT(GETDATE()), --录入时间  
	
	ProjPeopleDep nvarchar(200),--立项人部门 
	ProjPeopleDuty nvarchar(200), --立项人岗位
	OwnerAddress nvarchar(200) --建设单位联系地址
) 
GO
----修改立项人联系方式字长
--ALTER TABLE PT_PrjInfo_ZTB_Detail ALTER COLUMN ProjPeopleTel nvarchar(200) 
----添加立项人部门 2012-01-06
--ALTER TABLE PT_PrjInfo_ZTB_Detail ADD ProjPeopleDep nvarchar(200)
----添加立项人岗位 2012-01-06
--ALTER TABLE PT_PrjInfo_ZTB_Detail ADD ProjPeopleDuty nvarchar(200)
----添加建设单位联系地址 2012-01-06
--ALTER TABLE PT_PrjInfo_ZTB_Detail ADD OwnerAddress nvarchar(200)
----添加竣工备注 2012-01-09
--ALTER TABLE PT_PrjInfo_ZTB_Detail ADD CompletedNote nvarchar(max)

--项目权限表
CREATE TABLE PT_PrjInfo_ZTB_User
(
    Id nvarchar(500) not null PRIMARY KEY, --Id
    PrjGuid uniqueidentifier null, --项目Id,
    UserCode varchar(8) not null-- REFERENCES PT_yhmc(v_yhdm) ON DELETE CASCADE --用户编码
)
GO

--投标项目主键修改 
IF OBJECT_ID('PT_PrjInfo_ZTB','U') IS NOT NULL
BEGIN
	IF OBJECT_ID('PK_ENT_PRJPLAN_PRJINFO','PK') IS NOT NULL
	 BEGIN
		ALTER TABLE PT_PrjInfo_ZTB DROP CONSTRAINT PK_ENT_PRJPLAN_PRJINFO
        ALTER TABLE PT_PrjInfo_ZTB ADD CONSTRAINT UQ_ProCode UNIQUE(PrjCode)
        ALTER TABLE PT_PrjInfo_ZTB ALTER COLUMN PrjGuid UNIQUEIDENTIFIER  NOT NULL
		ALTER TABLE PT_PrjInfo_ZTB ADD CONSTRAINT FK_PT_PrjInfo_ZTB PRIMARY KEY(PrjGuid)  
	 END
END
GO

--添加投标审核流程设置
IF ((SELECT COUNT(*) FROM WF_BusinessCode WHERE BusinessCode ='089') = 0)
BEGIN
	INSERT dbo.WF_BusinessCode VALUES('089','立项信息审核','PrjGuid','PT_PrjInfo_ZTB_Detail','PrjGuid','ProjFlowSate','/TenderManage/InfoView.aspx',null,'70','PrjGuid','PrjGuid')
	INSERT dbo.WF_Business_Class VALUES('089','001','立项信息审核')
END
GO

--Basic_CodeType表
IF OBJECT_ID('Basic_CodeType', 'U') IS NOT NULL
	DROP TABLE Basic_CodeType
GO
CREATE Table Basic_CodeType
(
	TypeCode nvarchar(20) PRIMARY KEY,
	TypeName nvarchar(20) NOT NULL
)
GO
--Basic_CodeList
IF OBJECT_ID('Basic_CodeList', 'U') IS NOT NULL
	DROP TABLE Basic_CodeList
GO
CREATE TABLE Basic_CodeList
(
	TypeCode nvarchar(20) REFERENCES Basic_CodeType(TypeCode) ON DELETE CASCADE,
	ItemCode int NOT NULL,
	ItemName nvarchar(20) NOT NULL,
	PRIMARY KEY(TypeCode, ItemCode)
)
GO



INSERT INTO Basic_CodeType(TypeCode, TypeName) 
	VALUES('ProjectState', '项目状态')
--DELETE FROM Basic_CodeList WHERE TypeCode = 'ProjectState'
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '1', '信息预立项')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '2', '信息立项')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '3', '启动')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '4', '投标')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '5', '中标')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '6', '落标')
--INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
--	VALUES('ProjectState', '7', '未开工')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '7', '在建')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '8', '停工')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '9', '验收')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '10', '竣工')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '11', '保内')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '12', '保外')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ProjectState', '13', '解除')
    
INSERT INTO Basic_CodeType(TypeCode, TypeName) 
	VALUES('ConstructType', '施工类型')
--DELETE FROM Basic_CodeList WHERE TypeCode = 'ConstructType'
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ConstructType', '101', '装饰装修')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ConstructType', '102', '建筑幕墙')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ConstructType', '103', '消防设施')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ConstructType', '104', '建筑智能化')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ConstructType', '105', '机电设备安装')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('ConstructType', '106', '城市及道路照明')

INSERT INTO Basic_CodeType(TypeCode, TypeName) 
	VALUES('DesignType', '设计类型')
--DELETE FROM Basic_CodeList WHERE TypeCode = 'DesignType'
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('DesignType', '201', '建筑装饰')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('DesignType', '202', '建筑幕墙')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('DesignType', '203', '消防设施')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('DesignType', '204', '建筑智能化')
INSERT INTO Basic_CodeList(TypeCode, ItemCode, ItemName)
	VALUES('DesignType', '205', '照明工程')
GO

--  创建投标项目权限管理添加触发器		lhy
IF OBJECT_ID('trig_insert_prjinfo', 'TR') IS NOT NULL
	DROP TRIGGER trig_insert_prjinfo
GO
CREATE TRIGGER trig_insert_prjinfo
ON [PT_PrjInfo_ZTB_User] AFTER INSERT
AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @user AS nvarchar(4000)
		DECLARE @prjguid AS nvarchar(200) 
		SELECT @prjguid = PrjGuid FROM INSERTED

		SELECT @user= ISNULL(@user + ',' ,',') + UserCode 
		FROM (
			SELECT DISTINCT UserCode FROM PT_PrjInfo_ZTB_User 
			WHERE PrjGuid = @prjguid
		)AS T
		UPDATE PT_PrjInfo SET Podepom = @user 
		WHERE PrjGuid = @prjguid
	END
GO


-- 创建投标项目权限管理删除触发器        lhy  
IF OBJECT_ID('trig_delete_prjinfo', 'TR') IS NOT NULL
	DROP TRIGGER trig_delete_prjinfo
GO
CREATE TRIGGER trig_delete_prjinfo
ON [PT_PrjInfo_ZTB_User] AFTER INSERT
AS
	BEGIN
		SET NOCOUNT ON
		DECLARE @user AS nvarchar(4000)
		DECLARE @prjguid AS nvarchar(200) 
		SELECT @prjguid = PrjGuid FROM DELETED

		SELECT @user= ISNULL(@user + ',' ,',') + UserCode 
		FROM (
			SELECT DISTINCT UserCode FROM PT_PrjInfo_ZTB_User 
			WHERE PrjGuid = @prjguid
		)AS T
		UPDATE PT_PrjInfo SET Podepom = @user 
		WHERE PrjGuid = @prjguid
	END
GO


--项目小组成员
IF OBJECT_ID('PT_PrjMember') IS NOT NULL
    DROP TABLE PT_PrjMember
GO
CREATE TABLE PT_PrjMember
(
	PrjMemberId nvarchar(200) PRIMARY KEY,
	PrjGuid uniqueidentifier,
	MemberCode varchar(8) REFERENCES PT_yhmc(v_yhdm), --用户编码
	Post nvarchar(200), --岗位 
	EducationalBackground nvarchar(200), --学历及专业
	Technical nvarchar(200), --职称
	PostAndCompetency nvarchar(200), --资格证书
	TrainingInformation nvarchar(max), --上岗培训情况
	PastPerformance nvarchar(max), --以往工作表现
	Note nvarchar(max), --备注
	InputDate DATETIME DEFAULT(GETDATE())   --录入时间 用于排序
)
GO
--添加项目工程类型
IF OBJECT_ID('PT_PrjInfo_EngineeringType') IS NOT NULL
	DROP TABLE PT_PrjInfo_EngineeringType;
GO
CREATE TABLE PT_PrjInfo_EngineeringType
(
	ID nvarchar(200) PRIMARY KEY,
	PrjGuid uniqueidentifier, --工程类型
		--REFERENCES PT_PrjInfo_ZTB_Detail(PrjGuid) ON DELETE CASCADE,--项目GUID
	EngineeringType nvarchar(20),
	EngineeringSubType int,--工程类型子项
	Grade nvarchar(64) --等级
)
GO
--项目小组成员审核流程 
IF ((SELECT COUNT(*) FROM WF_BusinessCode WHERE BusinessCode ='100') = 0)
BEGIN
	INSERT dbo.WF_BusinessCode VALUES('100','项目小组成员审核','PrjGuid','PT_PrjInfo_ZTB_Detail','PrjGuid','MemberFlowState','/PrjManager/PrjMemberView.aspx',null,'74','PrjGuid','PrjGuid')
	INSERT dbo.WF_Business_Class VALUES('100','001','项目小组成员审核')
END
GO
--项目超级删除
INSERT INTO WF_supperDelete VALUES ('089','001',1,'PT_PrjInfo_ZTB','PrjGuid','PrjGuid','PT_PrjInfo_ZTB_Detail')
INSERT INTO WF_supperDelete VALUES ('089','001',1,'PT_PrjInfo_ZTB_User','PrjGuid','PrjGuid','PT_PrjInfo_ZTB')
INSERT INTO WF_supperDelete VALUES ('089','001',1,'PT_PrjInfo','PrjGuid','PrjGuid','PT_PrjInfo_ZTB_Detail')
INSERT INTO WF_supperDelete VALUES ('089','001',1,'PT_PrjInfo_EngineeringType','PrjGuid','PrjGuid','PT_PrjInfo_ZTB_Detail')
GO

--部门视图
IF OBJECT_ID('v_pt_d_bm') IS NOT NULL
	DROP VIEW v_pt_d_bm
GO
CREATE VIEW v_pt_d_bm
AS
(
	SELECT * FROM PT_d_bm
)
GO

--项目小组成员       zfj
IF OBJECT_ID ('V_GetAllPrjMembers', 'V') IS NOT NULL
DROP VIEW V_GetAllPrjMembers ;
GO
CREATE VIEW V_GetAllPrjMembers 
AS
SELECT Member.PrjMemberId
	,Member.PrjGuid
	,v_xm AS MemberName --姓名
	,Member.InputDate   --录入时间
	,Member.Post        --岗位 
	,Member.EducationalBackground   --学历和专业
	,Member.Technical               -- 职称
	,Member.PostAndCompetency       --资格证书
	,Member.PastPerformance        --AS 以往工作表现
	,Member.TrainingInformation            --上岗培训情况(只是培训中的培训课程)
 FROM PT_PrjMember AS Member
LEFT JOIN PT_yhmc AS Yh ON Member.MemberCode=Yh.v_yhdm
LEFT JOIN HR_Personnel_Train AS Train ON Train.UserCode=Yh.v_yhdm
GO



--查询成员之前登记的培训信息和基本信息  zfj
IF OBJECT_ID ('V_GetMembersOldInfo', 'V') IS NOT NULL
DROP VIEW V_GetMembersOldInfo ;
GO
CREATE VIEW [dbo].[V_GetMembersOldInfo] 
AS
SELECT Yh.v_yhdm,EducationalBackground       --AS 学历
,(SELECT RoleTypeName FROM PT_D_Role
	WHERE RoleTypeCode = (SELECT DutyCode FROM PT_DUTY
		WHERE I_DUTYID = (SELECT I_DUTYID FROM PT_yhmc WHERE v_yhdm = Yh.v_yhdm)) 
)AS Post                     --岗位
,Specialty                   --AS 专业
,(SELECT PostAndRank FROM HR_Org_PostLevel
	WHERE RecordID = (SELECT PostAndRank FROM PT_yhmc WHERE v_yhdm = Yh.v_yhdm)
) AS Technical          -- 职称
,PostAndCompetency      --AS 资格证书
,PastPerformance        --AS 以往工作表现
--上岗培训情况
,Train.Courses          --AS 培训课程
,Train.Hour             --AS 课程时间
,Train.StartDate        --AS 培训开始时间
,Train.EndDate          --AS 培训结束时间
,Train.TrainOrgan       --AS 培训机构
,Train.Remark           --AS 培训评语
FROM PT_yhmc AS Yh 
LEFT JOIN HR_Personnel_Train AS Train ON Train.UserCode=Yh.v_yhdm
GO

--afl 的执行到这个地方结束，没有竣工管理  孙新华  2012-2-29  15:14
--存储过程 返回指定的项目的竣工单详细信息
--参数：@prjGuid(项目的GUID)
IF OBJECT_ID('uspPrjCompleted')	IS NOT NULL
	DROP PROCEDURE uspPrjCompleted
GO
CREATE PROCEDURE uspPrjCompleted
	@prjGuid nvarchar(200)
AS
BEGIN
	SELECT PC.* ,
		PCD.PrepareStatus, PCD.UncompletedTrans, PCD.Rectification,
		PCD.AnnexAddress
	FROM PT_Prj_Completed AS PC
	LEFT JOIN PT_Prj_Completed_Detail AS PCD ON PCD.PrjCompletedID = PC.ID
		AND PCD.PrjGuid = @prjGuid
	ORDER BY InputDate DESC
END
GO


--工程竣工管理 Bery 2012-01-13 09:26
--验收条目
IF OBJECT_ID('PT_Prj_Completed') IS NOT NULL
	DROP TABLE PT_Prj_Completed
GO
CREATE TABLE PT_Prj_Completed
(
	ID nvarchar(200) PRIMARY KEY, 
	Name nvarchar(200) NOT NULL UNIQUE, --名称
	InputUser varchar(8) REFERENCES PT_yhmc(v_yhdm), --录入人
	InputDate datetime NOT NULL DEFAULT(GETDATE()), --录入时间
)
GO
--验收项目明细
IF OBJECT_ID('PT_Prj_Completed_Detail') IS NOT NULL
	DROP TABLE PT_Prj_Completed_Detail
GO

CREATE TABLE PT_Prj_Completed_Detail
(
	ID nvarchar(200) PRIMARY KEY,
	PrjGuid uniqueidentifier REFERENCES PT_PrjInfo(PrjGuid), --外键 关联PT_PrjInfo
	PrjCompletedID nvarchar(200) REFERENCES PT_Prj_Completed(ID), --外键，关联PT_Prj_Completed
	PrepareStatus nvarchar(max), --准备情况
	UncompletedTrans nvarchar(max), --未完成事项
	Rectification nvarchar(max), --整改措施
	AnnexAddress nvarchar(64) REFERENCES F_FileInfo(Id), --附件目录地址
	InputUser varchar(8) REFERENCES PT_yhmc(v_yhdm), --录入人
	InputDate datetime NOT NULL DEFAULT(GETDATE()), --录入时间
)
GO
--项目验收附件地址
IF OBJECT_ID('PT_Prj_Completed_Annex') IS NOT NULL
	DROP TABLE PT_Prj_Completed_Annex
GO
CREATE TABLE PT_Prj_Completed_Annex
(
	ID nvarchar(200) PRIMARY KEY,
	DetailID nvarchar(200) REFERENCES PT_Prj_Completed_Detail(ID) 
		ON DELETE CASCADE, --外建 关联PT_Prj_Completed_Detail
	AnnexAddress nvarchar(64) REFERENCES F_FileInfo(Id), --附件目录地址
)
GO
--竣工审核
INSERT dbo.WF_BusinessCode VALUES('106','项目竣工验收','PrjGuid','PT_PrjInfo_ZTB_Detail','PrjGuid','CompletedFlowState','/PrjManager/Completed/PrjCompletedQuery.aspx',null,'96','PrjGuid','PrjGuid')
	INSERT dbo.WF_Business_Class VALUES('106','001','项目竣工验收审核')

--PT_Prj_Completed中的初始化信息
DELETE FROM PT_Prj_Completed;
GO
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '工程技术资料', '00000000', GETDATE());
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '工程缺陷的修复整改', '00000000', GETDATE() - 1);
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '现场准备', '00000000', GETDATE() - 2);
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '验收必备的工具仪器', '00000000', GETDATE() - 3);
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '陪检人员安排', '00000000', GETDATE() - 4);
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '后勤工作', '00000000', GETDATE() - 5);
INSERT INTO PT_Prj_Completed(ID, Name, InputUser, InputDate)
	VALUES(NEWID(), '其它', '00000000', GETDATE() - 6);
GO

--投标管理，项目立项，竣工管理的菜单 Bery 2012-01-13 09:38
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('70','投标管理','','y',3,'MenuIco/13.gif',2288,6,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7011','信息立项','','y',1,'',2302,4,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701101','信息立项申请','/TenderManage/InfoSetUp.aspx','y',1,'',2308,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701102','立项信息一览','/TenderManage/ApprovalQuery.aspx','y',2,'',2309,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701103','驳回信息一览','/TenderManage/RejectQuery.aspx','y',3,'',2310,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701114','立项申请一览','/TenderManage/InfoQuery.aspx','y',4,'',2311,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7012','项目启动','','y',2,'',2303,2,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701201','启动管理','/TenderManage/InitiateManage.aspx','y',1,'',2312,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701202','启动项目一览','/TenderManage/InitiateQuery.aspx','y',2,'',2313,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7013','项目投标','','y',3,'',2304,2,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701301','投标管理','/TenderManage/BidManage.aspx','y',1,'',2314,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701302','在投项目一览','/TenderManage/BidQuery.aspx','y',2,'',2315,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7014','中标管理','','y',4,'',2305,4,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701401','中落标管理','/TenderManage/AllbidManage.aspx','y',1,'',2316,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701402','中标项目一览','/TenderManage/WinbidQuery.aspx','y',2,'',2317,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701403','落标项目一览','/TenderManage/OutbidQuery.aspx','y',3,'',2318,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701404','投标情况一览','/TenderManage/AllbidQuery.aspx','y',4,'',2319,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7015','状态变更管理','','y',5,'',2306,1,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701501','立项状态变更','/TenderManage/SetPrjState.aspx','y',1,'',2320,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7016','统计分析','','y',6,'',2307,1,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701601','项目市场状态一览','/TenderManage/TenderStateReport.aspx','y',1,'',2321,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('74','项目立项','','y',3,'MenuIco/13.gif',2299,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7411','立项管理','','y',1,'',2324,2,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741101','立项','/PrjManager/PrjInfoList.aspx','y',1,'',2325,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741102','立项信息','/PrjManager/PrjInfoQuery.aspx','y',2,'',2326,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7412','招投标项目管理','','y',2,'',2327,2,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741201','招投标项目管理','/PrjManager/TenderInfo.aspx','y',1,'',2328,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741202','招投标项目查询','/PrjManager/TenderInfoQuery.aspx','y',2,'',2329,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7413','项目成员管理','','y',3,'',2330,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741301','项目成员申请','/PrjManager/PrjMember.aspx','y',1,'',2331,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741302','项目成员分布','/PrjManager/PrjTreeMemberQuery.aspx','y',2,'',2332,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741303','项目成员分布图','/PrjManager/PrjMembersMap.aspx','y',3,'',2358,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('96','竣工管理','','y',86,'MenuIco/13.gif',2347,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('9601','竣工验收申请','/PrjManager/Completed/PrjCompletedList.aspx','y',1,'',2348,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('9602','竣工验收信息','/PrjManager/Completed/CompletedQuery.aspx','y',2,'',2349,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('9603','竣工管理','/PrjManager/Completed/CompletedManage.aspx','y',3,'',2357,0,'0','0','','1')
GO

--添加“项目经理要求”，“技术负责人要求”，“资格预审要求”   Bery  2012-03-05
IF ((SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS
	WHERE TABLE_NAME = 'PT_PrjInfo_ZTB_Detail' 
		AND COLUMN_NAME = 'PrjManagerRequire') = 0)
BEGIN
	ALTER TABLE PT_PrjInfo_ZTB_Detail ADD PrjManagerRequire nvarchar(200)--项目经理要求
	ALTER TABLE PT_PrjInfo_ZTB_Detail ADD TechnicalLeaderRequire nvarchar(200)--技术负责人要求
	ALTER TABLE PT_PrjInfo_ZTB_Detail ADD PrequalificationRequire nvarchar(200)--资格预审要求
END
GO

--添加项目状态   孙新华  2012-03-07 
INSERT INTO Basic_CodeList VALUES ('ProjectState',14,'资格预审')
INSERT INTO Basic_CodeList VALUES ('ProjectState',15,'预审通过')
INSERT INTO Basic_CodeList VALUES ('ProjectState',16,'预审失败')
GO

--添加预审结果字段	Bery  2012-03-08
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD QualificationPassDate datetime	--资格预审通过日期
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD QualificationPassReason nvarchar(max)	--通过说明
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD QualificationFailData datetime	--资格预审未通过日期
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD QualificationFailReason nvarchar(max)	--未通过理由	
GO

--添加项目所在省份和城市	Bery	2012-03-13
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD Province nvarchar(50)
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD City nvarchar(50)
GO

--修改菜单   Bery  2012-03-23 12:04
--资格预审
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7017','资格预审','','y',3,'',2372,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701701','资格预审登记','/TenderManage/PrequalificationManage.aspx','y',1,'',2373,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701702','资格审查','/TenderManage/Prequalification.aspx','y',2,'',2376,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('701703','资格预审项目一览','/TenderManage/PrequalificationQuery.aspx','y',3,'',2377,0,'0','0','','1')
--投标权限管理
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7018','权限管理','/TenderManage/SetPrjPrivilege.aspx','y',7,'',2386,0,'0','0','','1')

--项目管理
DELETE FROM PT_Role_Privilege
WHERE ModuleCode IN 
(
	SELECT C_MKDM FROM PT_MK  WHERE C_MKDM like '74%'
)
GO
DELETE FROM PT_MK  WHERE C_MKDM like '74%'
GO
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('74','项目立项','','y',3,'MenuIco/13.gif',2299,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7411','立项管理','','y',1,'',2324,4,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741101','项目立项','/PrjManager/PrjInfoList.aspx','y',1,'',2325,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741102','手工立项信息查询','/PrjManager/PrjInfoQuery.aspx','y',3,'',2326,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741103','项目信息','/PrjManager/PrjTotalQuery.aspx','y',2,'',2375,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741104','投标立项信息查询','/PrjManager/TenderInfoQuery.aspx','y',4,'',2374,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7413','项目成员管理','','y',3,'',2330,3,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741301','项目成员申请','/PrjManager/PrjMember.aspx','y',1,'',2331,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741302','项目成员分布','/PrjManager/PrjTreeMemberQuery.aspx','y',2,'',2332,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('741303','项目成员分布图','/PrjManager/PrjMembersMap.aspx','y',3,'',2358,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('7414','权限管理','/PrjManager/SetPrjPrivilege.aspx','y',3,'',2387,0,'0','0','','1')
GO


--修改数据类型      Bery        2012-04-10 10:00
ALTER TABLE PT_PrjInfo_ZTB ALTER COLUMN OtherStatement nvarchar(max)
ALTER TABLE PT_PrjInfo_ZTB_Detail ALTER COLUMN ProjInfoOrigin nvarchar(max)
GO
--项目立项-添加流程审核 By Zhang Fujun  Date：2012-05-15 10:39:40.127
--1.添加审核流程字段
IF NOT EXISTS(SELECT COLUMN_NAME 
              FROM   information_schema.COLUMNS 
              WHERE  TABLE_NAME = 'PT_PrjInfo_ZTB_Detail' 
                     AND COLUMN_NAME = 'SetUpFlowState') 
  ALTER TABLE PT_PrjInfo_ZTB_Detail  
    ADD SetUpFlowState INT DEFAULT(-1)
GO
--2.项目立项添加流程，已存在的项目更改为已审核通过
UPDATE PT_PrjInfo_ZTB_Detail 
SET    SetUpFlowState = 1 
FROM   PT_PrjInfo_ZTB_Detail AS T1 
       INNER JOIN PT_PrjInfo AS T2 
         ON T1.PrjGuid = T2.PrjGuid 
GO

--3.添加流程设置
IF NOT EXISTS(SELECT * FROM WF_BusinessCode WHERE BusinessCode='122')
BEGIN
	INSERT dbo.WF_BusinessCode(BusinessCode,BusinessName,KeyWord,LinkTable,
		PrimaryField,StateField,DoWithUrl,C_MKDM,ProjectField) 
	VALUES('122','项目立项审核','PrjGuid', 'PT_PrjInfo_ZTB_Detail',
		'PrjGuid','SetUpFlowState','/PrjManager/PrjInfoView.aspx?Type=View', '74','PrjGuid')
	INSERT dbo.WF_Business_Class 
	VALUES('122','001','项目立项审核')	
END
GO

--删除对应关系错误的项目信息  by Zhang Fujun Date：2012-05-17 11:25:13.903
----##1.删除PT_PrjInfo_ZTB_Detail
--DELETE FROM PT_PrjInfo_ZTB_Detail WHERE PrjGuid IN
--(
--SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail 
--WHERE PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo)
--	AND PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo_ZTB)
--)
--GO
----##2.删除PT_PrjInfo_ZTB
--DELETE FROM PT_PrjInfo_ZTB WHERE PrjGuid IN 
--(
--SELECT  PrjGuid FROM PT_PrjInfo_ZTB 
--WHERE PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo)
--	 AND PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail)
--)
--GO
----##3.删除PT_PrjInfo
--DELETE FROM PT_PrjInfo WHERE PrjGuid IN
--(
--SELECT PrjGuid FROM PT_PrjInfo
--WHERE PrjGuid NOT IN (SELECT  PrjGuid FROM PT_PrjInfo_ZTB)
--	 AND PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail)
--)
--END 
GO
--------------by  Zhang Fujun  Date:2012-05-22 17:22:50.483------------------------
--查询项目（可以查出项目立项未审核的）
IF OBJECT_ID('uspGetAllProject','P') IS NOT NULL
DROP PROCEDURE uspGetAllProject
GO
CREATE PROCEDURE [dbo].[uspGetAllProject]
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

		INSERT INTO @project
		SELECT P.PrjGuid, P.TypeCode 
		FROM vProject AS P
		WHERE P.PrjGuid IN (SELECT PrjGuid FROM @primitPrjGuid) ' + @condition +'
	'
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
			INNER JOIN vProject ON vProject.PrjGuid = C.PrjGuid '
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

--查询项目（过滤掉项目立项未审核的条件）
IF OBJECT_ID('uspGetProject','P') IS NOT NULL
DROP PROCEDURE uspGetProject
GO
CREATE PROCEDURE [dbo].[uspGetProject]
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

		INSERT INTO @project
		SELECT P.PrjGuid, P.TypeCode 
		FROM vProject AS P
		WHERE P.PrjGuid IN (SELECT PrjGuid FROM @primitPrjGuid) AND SetUpFlowState=1 ' + @condition +'
	'
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
			INNER JOIN vProject ON vProject.PrjGuid = C.PrjGuid '
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
--by Zhang Fujun   2012-05-24 08:32:30.267
--新项目应用视图(质量目标选择WBS任务节点时的应用)
IF OBJECT_ID('v_Quality_Goal_New','V') IS NOT NULL
DROP VIEW v_Quality_Goal_New
GO
CREATE VIEW [dbo].[v_Quality_Goal_New]
AS
SELECT     TOP (100) PERCENT dbo.Ent_Quality_Goal.i_id, dbo.Ent_Quality_Goal.PrjCode, dbo.Ent_Quality_Goal.ScheduleCode, dbo.Ent_Quality_Goal.QualityGoal, 
                      dbo.Ent_Quality_Goal.Remark, dbo.Bud_Task.TaskName, dbo.PT_PrjInfo.PrjName, dbo.Ent_Quality_Goal.mark, dbo.Ent_Quality_Goal.filesType
FROM         dbo.Ent_Quality_Goal INNER JOIN
                      dbo.Bud_Task ON dbo.Ent_Quality_Goal.PrjCode = dbo.Bud_Task.PrjId AND 
                      dbo.Ent_Quality_Goal.ScheduleCode = dbo.Bud_Task.TaskId INNER JOIN
                      dbo.PT_PrjInfo ON dbo.Ent_Quality_Goal.PrjCode = dbo.PT_PrjInfo.PrjGuid
ORDER BY dbo.Ent_Quality_Goal.ScheduleCode
GO
--修改项目关联删除的项目竣工
ALTER TABLE [dbo].[PT_Prj_Completed_Detail]  WITH CHECK ADD FOREIGN KEY([PrjGuid])
REFERENCES [dbo].[PT_PrjInfo] ([PrjGuid]) ON DELETE CASCADE
GO
--1.PT_PrjInfo_ZTB 修改项目经理：PrjManager 长度 
ALTER TABLE PT_PrjInfo_ZTB 
	ALTER COLUMN PrjManager VARCHAR(100)
--1.1 编码替换名称
UPDATE PT_PrjInfo_ZTB 
SET    PrjManager = T2.V_XM 
FROM   PT_PrjInfo_ZTB AS T1 
       INNER JOIN PT_YHMC AS T2 
         ON T1.PrjManager = T2.V_YHDM 
GO
--2.PT_PrjInfo_ZTB 修改施工单位：WorkUnit 长度
ALTER TABLE PT_PrjInfo_ZTB 
  ALTER COLUMN WorkUnit VARCHAR(200) 
--2.1 编码替换名称
UPDATE PT_PrjInfo_ZTB 
SET    WorkUnit = T2.CorpName 
FROM   PT_PrjInfo_ZTB AS T1 
       INNER JOIN XPM_Basic_ContactCorp AS T2 
         ON T1.WorkUnit = CONVERT(VARCHAR(50),T2.CorpID )
GO
--3.PT_PrjInfo 项目经理  编码替换名称
UPDATE PT_PrjInfo 
SET    PrjManager = T2.V_XM 
FROM   PT_PrjInfo AS T1 
       INNER JOIN PT_YHMC AS T2 
         ON T1.PrjManager = T2.V_YHDM 
--4.PT_PrjInfo 施工单位 编码替换名称
UPDATE PT_PrjInfo
SET    WorkUnit = T2.CorpName 
FROM   PT_PrjInfo AS T1 
       INNER JOIN XPM_Basic_ContactCorp AS T2 
         ON T1.WorkUnit = CONVERT(VARCHAR(50),T2.CorpID ) 
GO
--by Zhang Fujun  Date：2012-05-28 16:30:49.383
--项目小组成员申请流程审核表
IF OBJECT_ID('PT_PrjMemberWF','U') IS NOT NULL
DROP TABLE PT_PrjMemberWF
GO
CREATE TABLE PT_PrjMemberWF
(
  PrjGuid UNIQUEIDENTIFIER UNIQUE NOT NULL,
  FlowState INT DEFAULT(-1)
)
GO
IF OBJECT_ID('FK_PT_PrjMemberWF_PT_PrjInfo','F') IS NOT NULL
ALTER TABLE PT_PrjMemberWF DROP CONSTRAINT FK_PT_PrjMemberWF_PT_PrjInfo
GO
ALTER TABLE PT_PrjMemberWF WITH CHECK ADD CONSTRAINT FK_PT_PrjMemberWF_PT_PrjInfo
FOREIGN KEY (PrjGuid) REFERENCES [PT_PrjInfo](PrjGuid) ON DELETE CASCADE
GO

--项目小组成员流程审核状态更新到PT_PrjMemberWF
INSERT INTO PT_PrjMemberWF(PrjGuid,FlowState)
SELECT T1.PrjGuid,T2.MemberFlowState FROM PT_PrjInfo AS T1
INNER JOIN PT_PrjInfo_ZTB_Detail AS T2 ON T1.PrjGuid=T2.PrjGuid
WHERE T1.PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjMemberWF) 
GO
--修改PT_PrjMember PrjGuid 外键
IF OBJECT_ID('FK_PT_PrjMember_PT_PrjMemberWF','F') IS NOT NULL
	ALTER TABLE PT_PrjMember DROP CONSTRAINT FK_PT_PrjMember_PT_PrjMemberWF
GO
ALTER TABLE PT_PrjMember WITH NOCHECK ADD CONSTRAINT FK_PT_PrjMember_PT_PrjMemberWF
FOREIGN KEY(PrjGuid) REFERENCES [PT_PrjMemberWF](PrjGuid) ON DELETE CASCADE 
GO
--添加项目时，添加小组成员信息流程状态
IF OBJECT_ID('tr_Insert_PT_PrjInfo','TR') IS NOT NULL
	DROP TRIGGER tr_Insert_PT_PrjInfo
GO
CREATE TRIGGER tr_Insert_PT_PrjInfo
ON PT_PrjInfo AFTER INSERT
AS
BEGIN
	INSERT INTO PT_PrjMemberWF(PrjGuid) SELECT PrjGuid FROM INSERTED
END
GO
--修改项目小组成员流程审核设置
UPDATE WF_BusinessCode SET LinkTable ='PT_PrjMemberWF',StateField='FlowState' WHERE BusinessCode='100'
GO
--创建删除PT_PrjInfo_ZTB_Detailc触发器
IF OBJECT_ID('tr_Delete_PT_PrjInfo_ZTB_Detail','TR') IS NOT NULL
DROP TRIGGER tr_Delete_PT_PrjInfo_ZTB_Detail
GO
CREATE TRIGGER tr_Delete_PT_PrjInfo_ZTB_Detail
ON PT_PrjInfo_ZTB_Detail AFTER DELETE
AS
BEGIN
	DELETE FROM PT_PrjInfo_ZTB WHERE PrjGuid IN (SELECT PrjGuid FROM DELETED);
	DELETE FROM PT_PrjInfo WHERE PrjGuid IN (SELECT PrjGuid FROM DELETED);
END

--by Zhang Fujun  Date:2012-05-29 14:34:48.330
--安全管理安全目标管理 查询视图
IF Object_id('v_Safty_Measure_New', 'V') IS NOT NULL 
  DROP VIEW v_Safty_Measure_New 

go 
--新投标项目应用到的[安全管理安全目标管理 查询视图]
/*
 *新投标关联的bud_task中的TaskId
*/
CREATE VIEW v_Safty_Measure_New 
AS 
  SELECT TOP (100) PERCENT dbo.ent_safty_measure.i_id, 
                           dbo.ent_safty_measure.prjcode, 
                           dbo.pt_prjinfo.prjname, 
                           dbo.ent_safty_measure.schedulecode, 
                           dbo.ent_safty_measure.saftymeasure, 
                           dbo.ent_safty_measure.remark, 
                           dbo.bud_task.taskname, 
                           dbo.ent_safty_measure.mark, 
                           dbo.ent_safty_measure.filestype 
  FROM   dbo.ent_safty_measure 
         INNER JOIN dbo.bud_task 
                 ON dbo.ent_safty_measure.prjcode = dbo.bud_task.prjid 
                    AND dbo.ent_safty_measure.schedulecode = dbo.bud_task.taskid 
         INNER JOIN dbo.pt_prjinfo 
                 ON dbo.ent_safty_measure.prjcode = dbo.pt_prjinfo.prjguid 
  ORDER  BY dbo.ent_safty_measure.schedulecode 
go

--by Feng Yuanyuan  Date:2012-07-23
--修改项目基本内容表中"项目经理要求"字段和"技术负责人要求"字段的长度
ALTER TABLE PT_PrjInfo_ZTB_Detail ALTER COLUMN PrjManagerRequire nvarchar(max)
ALTER TABLE PT_PrjInfo_ZTB_Detail ALTER COLUMN TechnicalLeaderRequire nvarchar(max)
GO

--投标添加字段  Bery    2012-08-06 09:12
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD PrjProperty nvarchar(200)		--项目属性
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD PrjReadOne nvarchar(200)		--项目阅知人
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD QualificationMargin decimal(18,3) NOT NULL DEFAULT(0.0)	--预审保证金
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD QualificationReadOne nvarchar(200)	--预审阅知人
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD TenderProspect datetime		--现场勘察时间
ALTER TABLE PT_PrjInfo_ZTB_Detail ADD TenderReadOne nvarchar(200)	--投标阅知人
GO

--项目类型表    Bery    2012-08-06 09:12
CREATE TABLE PT_PrjInfo_Kind
(
	KindId nvarchar(200) PRIMARY KEY,		--主键
	PrjGuid uniqueidentifier REFERENCES PT_PrjInfo_ZTB_Detail(PrjGuid) ON DELETE CASCADE NOT NULL ,	-- 项目GUID
	PrjKind nvarchar(200),					--项目类型
	ProfessionalCost decimal(18,3) NOT NULL DEFAULT(0.0)				--专业造价
)

--资质要求表    Bery    2012-08-06 09:12
CREATE TABLE PT_PrjInfo_Rank
(
	RankId nvarchar(200) PRIMARY KEY,		--主键
	PrjGuid uniqueidentifier REFERENCES PT_PrjInfo_ZTB_Detail(PrjGuid) NOT NULL,	-- 项目GUID
	PrjRank nvarchar(200),					--资质要求
	RankLevel nvarchar(200)					--资质等级
)
GO


--编码库添加项目属性    Bery    2012-08-06 10:54
INSERT INTO XPM_Basic_CodeType(TypeName,IsVisible, IsValid, Remark, SignCode, Owner, VersionTime, ContractCropType) 
	VALUES('项目属性', 1, 1, '项目属性', 'ProjectProperty', '00000000', GETDATE(), NEWID())
GO
--编码库添加评标方法    Bery    2012-08-09 09:33
INSERT INTO XPM_Basic_CodeType(TypeName,IsVisible, IsValid, Remark, SignCode, Owner, VersionTime, ContractCropType) 
	VALUES('评标方法', 1, 1, '评标方法', 'TenderAppraiseMethod', '00000000', GETDATE(), NEWID())
GO
--编码库添加缴费方式    Bery    2012-08-09 09:50
INSERT INTO XPM_Basic_CodeType(TypeName,IsVisible, IsValid, Remark, SignCode, Owner, VersionTime, ContractCropType) 
	VALUES('缴费方式', 1, 1, '缴费方式', 'PaymentMethods', '00000000', GETDATE(), NEWID())
GO


--####项目小组成员超级删除--Zhang Fujun 2012-08-29 13:29:51.270------------------------------------------------------

--删除外键约束
IF  EXISTS (SELECT * FROM sys.foreign_keys 
WHERE object_id = OBJECT_ID(N'[dbo].[FK_PT_PrjMember_PT_PrjMemberWF]') 
AND parent_object_id = OBJECT_ID(N'[dbo].[PT_PrjMember]'))
ALTER TABLE [dbo].[PT_PrjMember] DROP CONSTRAINT [FK_PT_PrjMember_PT_PrjMemberWF]
GO
--删除外键约束
IF  EXISTS (SELECT * FROM sys.foreign_keys 
WHERE object_id = OBJECT_ID(N'[dbo].[FK_PT_PrjMemberWF_PT_PrjInfo]') 
AND parent_object_id = OBJECT_ID(N'[dbo].[PT_PrjMemberWF]'))
ALTER TABLE [dbo].[PT_PrjMemberWF] DROP CONSTRAINT [FK_PT_PrjMemberWF_PT_PrjInfo]
GO
--删除主键自动生成的唯一约束
DECLARE @PrjMemberWFUQ NVARCHAR(100)
SELECT @PrjMemberWFUQ=[name] FROM sys.indexes 
	WHERE object_id = OBJECT_ID(N'[dbo].[PT_PrjMemberWF]') 
	AND is_unique_constraint = N'1'
IF(LEN( @PrjMemberWFUQ)>0)
BEGIN
	DECLARE @sql NVARCHAR(1000)
	SET @sql=N'ALTER TABLE [dbo].[PT_PrjMemberWF] DROP CONSTRAINT '+@PrjMemberWFUQ
	EXEC sp_executesql @sql
END
GO
--修改数据类型  Fyy    2012-08-31 16:45:11.250
ALTER TABLE PT_PrjInfo ALTER COLUMN OtherStatement nvarchar(max)
GO
--项目小组成员编制出现的未审核状态数据修改  Zhang Fujun  2012-09-10 10:25:27.153
INSERT INTO PT_PrjMemberWF(PrjGuid,FlowState) 
SELECT PrjGuid,-1 FROM vProject 
WHERE PrjGuid NOT IN(SELECT PrjGuid FROM PT_PrjMemberWF)
GO


--去掉外键      Bery        2012-09-24 10:38
ALTER TABLE PT_PrjInfo_Kind Drop CONSTRAINT FK__PT_PrjInf__PrjGu__0313E4B1
ALTER TABLE PT_PrjInfo_Rank Drop CONSTRAINT FK__PT_PrjInf__PrjGu__06E47595

--创建PT_PrjInfo_Kind外键 并加级联删除       lhy 2012-09-26  15:45
ALTER TABLE PT_PrjInfo_Kind  WITH CHECK ADD FOREIGN KEY(PrjGuid)
REFERENCES PT_PrjInfo_ZTB_Detail ([PrjGuid])  ON DELETE CASCADE

--开、停、复工管理模块数据表   lhy     2012-09-29 18:00
--创建开工报告表
IF OBJECT_ID('PT_StartReport', 'U') IS NOT NULL
	DROP TABLE PT_StartReport
GO
CREATE TABLE PT_StartReport
(
    ReportId nvarchar(200) NOT NULL PRIMARY KEY, --开工报告Id
    PrjGuid nvarchar(200) not null UNIQUE, --项目Id
    SingleProjectName nvarchar(MAX) NULL, --单项工a程名称
    ProjectPlace nvarchar(MAX) NULL,  --工程地点
    ConstructionUnit nvarchar(MAX) NULL, --施工单位
    ApplyStartDate datetime NULL, --申请开工日期
    RealityStartDate datetime NULL, --实际开工日期
    ParentPrjId nvarchar(200) NULL, --上级项目
    ImplDep nvarchar(50) NULL, --实施项目部  
    MainContent nvarchar(MAX) NULL, --开工项目的主要工程内容
    PrepareCondition nvarchar(MAX) NULL, --准备情况
    ExistenceProblem nvarchar(MAX) NULL, --存在问题
    SupervisorUnitOpinion nvarchar(MAX) NULL, --监理单位意见
    BuildUnitOpinion nvarchar(MAX) NULL, --建设单位意见
    ApplyUnit nvarchar(MAX) NULL, --申请单位
    AuditUnit nvarchar(MAX) NULL, --审批单位
    FlowState int NULL, --流程状态
    InputUser nvarchar(20) NOT NULL, --录入人
    InputDate datetime NOT NULL --录入时间
)
GO

--停工通知单
IF OBJECT_ID('PT_StopMsg', 'U') IS NOT NULL
	DROP TABLE PT_StopMsg
GO
CREATE TABLE PT_StopMsg
(
    StopMsgId nvarchar(200) NOT NULL PRIMARY KEY, --停工通知单Id
    PrjGuid nvarchar(200) not null, --项目Id
    ConstArea nvarchar(MAX) NULL, --施工地段
    ConstUnit nvarchar(MAX) NULL, --施工单位
    ProjectMileage nvarchar(MAX) NULL, --工程里程
    StopDate datetime NOT NULL, --停工日期
	StopReason nvarchar(MAX) NULL, --停工原因
    MainContent nvarchar(MAX) NULL, --停工主要内容
    ProjectProblem nvarchar(MAX) NULL, --工程产生的问题
    ProblemReason nvarchar(MAX) NULL, --产生问题的原因
	ImpactLossDegree nvarchar(MAX) NULL, --影响及预计损失程度
    RemedialMeasure nvarchar(MAX) NULL, --建议补救措施
    SupervisorSign nvarchar(50) NULL, --监理工程师签名
    SupervisorSignDate datetime NULL, --监理工程师签名日期
	GeneralSign nvarchar(50) NULL, --总监理工程师签名
    GeneralSignDate datetime NULL, --总监理工程师签名日期
	FlowState int NULL, --流程状态
	InputUser nvarchar(50) NOT NULL, --录入人
    InputDate datetime NOT NULL --录入时间
)
GO

--复工通知单
IF OBJECT_ID('PT_RetMsg', 'U') IS NOT NULL
	DROP TABLE PT_RetMsg
GO
CREATE TABLE PT_RetMsg
(
    RetMsgId nvarchar(200) NOT NULL PRIMARY KEY, --复工通知单Id
	StopMsgId nvarchar(200) NOT NULL REFERENCES PT_StopMsg(StopMsgId) ON DELETE CASCADE,  --对应的停工通知单
    PrjGuid nvarchar(200) not null , --项目Id
    ConstArea nvarchar(MAX) NULL, --施工地段
    ConstUnit nvarchar(MAX) NULL, --施工单位
    ProjectMileage nvarchar(MAX) NULL, --工程里程
    RetDate datetime NOT NULL, --复工日期
    MainContent nvarchar(MAX) NULL, --复工内容
    SupervisorSign nvarchar(50) NULL, --监理工程师签名
    SupervisorSignDate datetime NULL, --监理工程师签名日期
	GeneralSign nvarchar(50) NULL, --总监理工程师签名
    GeneralSignDate datetime NULL, --总监理工程师签名日期
	BuildUnitOpinion nvarchar(MAX) NULL, --建设单位意见
	BuildUnitPerson nvarchar(50) NULL, --建设单位负责人
	BuildUnitSignDate datetime NULL, --建设单位签字日期
	FlowState int NULL, --流程状态
	InputUser nvarchar(50) NOT NULL, --录入人
    InputDate datetime NOT NULL --录入时间
)
GO



--开、停、复工管理		lhy		2012-09-29 17:20
--增加项目状态
INSERT INTO Basic_CodeList VALUES('ProjectState',17,'待开工')
GO
--添加开、停、复工管理 模块
INSERT INTO PT_MK VALUES
('72','开、停、复工管理','','y',3,'MenuIco/13.gif',2457,3,'0','0','','1')
INSERT INTO PT_MK VALUES
('7201','开工管理','/StartStopReturnWork/StartWorkReportList.aspx','y',1,'',2458,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('7202','停工管理','/StartStopReturnWork/StopMsgList.aspx','y',2,'',2459,0,'0','0','','1')
INSERT INTO PT_MK VALUES
('7203','复工管理','/StartStopReturnWork/RetMsgList.aspx','y',3,'',2460,0,'0','0','','1')
GO
--开工管理审核
INSERT INTO WF_BusinessCode VALUES ('126','开工管理审核','ReportId','PT_StartReport','ReportId','FlowState',
                                    '/StartStopReturnWork/StartWorkReportView.aspx',null,'72',null,'(SELECT''查看'')')
									
IF((select count(name) from syscolumns where id=object_id('WF_Business_Class') and name='ID')=0)
	EXEC('INSERT INTO WF_Business_Class VALUES (''126'',''001'',''开工管理审核'')')
else
	EXEC('INSERT INTO WF_Business_Class VALUES (''126'',''001'',''开工管理审核'',NEWID())')
GO
--停工管理审核
INSERT INTO WF_BusinessCode VALUES ('127','停工管理审核','StopMsgId','Pt_StopMsg','StopMsgId','FlowState',
                                    '/StartStopReturnWork/StopMsgView.aspx',null,'72','PrjGuid','(SELECT''查看'')')
IF((select count(name) from syscolumns where id=object_id('WF_Business_Class') and name='ID')=0)
	EXEC('INSERT INTO WF_Business_Class VALUES (''127'',''001'',''停工管理审核'')')
else
	EXEC('INSERT INTO WF_Business_Class VALUES (''127'',''001'',''停工管理审核'',NEWID())')
GO
--复工管理审核
INSERT INTO WF_BusinessCode VALUES ('128','复工管理审核','RetMsgId','PT_RetMsg','RetMsgId','FlowState',
                                    '/StartStopReturnWork/RetMsgView.aspx',null,'72','PrjGuid','(SELECT''查看'')')
IF((select count(name) from syscolumns where id=object_id('WF_Business_Class') and name='ID')=0)
	EXEC('INSERT INTO WF_Business_Class VALUES (''128'',''001'',''复工管理审核'')')
else
	EXEC('INSERT INTO WF_Business_Class VALUES (''128'',''001'',''复工管理审核'',NEWID())')
GO
--给已停工的项目添加停工通知单
WITH StopPrjInfo AS
(
select  Info.PrjGuid  from PT_PrjInfo AS Info
INNER JOIN  PT_PrjInfo_ZTB_Detail AS Detail ON Info.PrjGuid=Detail.PrjGuid
where prjState='8' AND Detail.SetUpFlowState=1
)
INSERT INTO PT_StopMsg (StopMsgId,PrjGuid,StopDate,FlowState,InputUser,InputDate) 
SELECT NewId(),PrjGuid,GETDATE(),1,'00000000',GETDATE() FROM  StopPrjInfo
GO

--增加开工模块查看报告  lhy 2012-10-19 17:30
INSERT INTO PT_MK VALUES
('7204','查看报告','/StartStopReturnWork/QueryReportList.aspx','y',4,'',2462,0,'0','0','','1')
GO

--增加企业技术管理  FYY 2012-10-19 17:30
INSERT INTO PT_MK VALUES
('911101','技术标准类型','EPC/17/Entpm/ScienceInnovate/EntStandardFrame.aspx?type=2','y',1,'',2458,0,'0','0','','1')
GO

--开工中添加实际负责人	Bery    2012-11-30 10:15
IF ((SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'PT_StartReport') = 1
	AND
	(SELECT COUNT(*) FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'PT_StartReport' AND COLUMN_NAME = 'ActualPrincipal') = 0)
	ALTER TABLE PT_StartReport ADD ActualPrincipal nvarchar(50)
GO


-- 插入PT_PrjInfo时插入PT_PrjMemberWF           Bery        2012-12-03 14:35
IF OBJECT_ID('tr_Insert_PT_PrjInfo', 'TR') IS NOT NULL
	DROP TRIGGER tr_Insert_PT_PrjInfo
GO
CREATE TRIGGER tr_Insert_PT_PrjInfo
ON PT_PrjInfo AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @PrjGuid nvarchar(200)
	SELECT @PrjGuid = PrjGuid FROM INSERTED
	-- 如果已经存在了，则不添加
	IF (SELECT COUNT(*) FROM PT_PrjMemberWF WHERE PrjGuid = @PrjGuid) = 0
		INSERT INTO PT_PrjMemberWF(PrjGuid) VALUES(@PrjGuid)
END


-- 插入PT_PrjInfo_ZTB时插入PT_PrjMemberWF       Bery        2012-12-03 14:59
IF OBJECT_ID('tr_Insert_PT_PrjInfo', 'TR') IS NOT NULL
	DROP TRIGGER tr_Insert_PT_PrjInfo
GO
CREATE TRIGGER tr_Insert_PT_PrjInfo
ON PT_PrjInfo_ZTB AFTER INSERT
AS
BEGIN
	SET NOCOUNT ON
	DECLARE @PrjGuid nvarchar(200)
	SELECT @PrjGuid = PrjGuid FROM INSERTED
	-- 如果已经存在了，则不添加
	IF (SELECT COUNT(*) FROM PT_PrjMemberWF WHERE PrjGuid = @PrjGuid) = 0
		INSERT INTO PT_PrjMemberWF(PrjGuid) VALUES(@PrjGuid)
END



--添加视图vTender       Bery        2012-12-04 15:57
IF OBJECT_ID('vTender') IS NOT NULL
	DROP VIEW vTender
GO
CREATE VIEW vTender
AS
SELECT z.PrjGuid, z.PrjCode, z.PrjName,                --项目GUID、编号、名称 
    z.PrjState, z.StartDate, z.EndDate,             --状态、开始日期、结束日期
	z.Duration, z.PrjCost,                          --工期、工程总造价
    zd.ProjFlowSate, zd.SuccessBidPrice,            --流程状态、中标价格
	zd.InputDate, zd.ProjPeopleName,                --立项时间、立项申请人
    zd.ProjPeopleDep, zd.PrjProperty,               --立项部门、项目属性
    zd.InputDate AS Date,
    z.PrjManager AS PrjMangerName,					--项目经理名称
	(SELECT TOP(1) CodeName FROM XPM_Basic_CodeList xl
		JOIN XPM_Basic_CodeType xt ON xl.TypeID  = xt.TypeID
		WHERE xt.SignCode = 'ProjectProperty' 
			AND zd.PrjProperty = xl.CodeID) as PropertyName,				--项目属性名称
	cc.CorpName AS WorkUnitName,											--建设单位
	ISNULL((c.ItemName + '(' + c2.ItemName + ')'), c.ItemName) AS StateText,	--项目状态
	(SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjMember LEFT JOIN PT_yhmc ON v_yhdm=MemberCode  WHERE PrjGuid=A.PrjGuid FOR XML PATH('')),1,1,'')
		FROM PT_PrjMember AS A
		WHERE PrjGuid= z.PrjGuid
		GROUP BY [PrjGuid]) AS MemberNames,
	(SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjInfo_ZTB_User LEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm=PT_PrjInfo_ZTB_User.UserCode  WHERE PrjGuid=ZU.PrjGuid FOR XML PATH('')),1,1,'')
		FROM PT_PrjInfo_ZTB_User AS ZU
		WHERE PrjGuid= z.PrjGuid
		GROUP BY [PrjGuid]) AS PrivilegeNames,
	Mwf.FlowState AS MemberFlowState
FROM PT_PrjInfo_ZTB z
LEFT JOIN PT_PrjInfo p ON z.PrjGuid = p.PrjGuid
LEFT JOIN PT_PrjInfo_ZTB_Detail zd ON z.PrjGuid = zd.PrjGuid
LEFT JOIN XPM_Basic_ContactCorp AS cc ON z.OwnerCode = CONVERT(VARCHAR(20),cc.CorpID)
LEFT JOIN Basic_CodeList c ON z.PrjState = c.ItemCode AND c.TypeCode = 'ProjectState'
LEFT JOIN Basic_CodeList c2 ON p.PrjState = c2.ItemCode AND c.TypeCode = 'ProjectState'
LEFT JOIN PT_PrjMemberWF AS Mwf ON z.PrjGuid=Mwf.PrjGuid
GO

--修改视图vProject		lhy		2013-01-16 14:30
IF OBJECT_ID('vProject','V') IS NOT NULL
DROP VIEW vProject
GO
CREATE VIEW [dbo].[vProject]
AS
(
	SELECT P.TypeCode, P.IsValid, P.UserCode, P.RecordDate, P.i_ChildNum, 
		P.PrjCode, P.PrjGuid, P.PrjName, P.StartDate, P.EndDate, 
		ISNULL(CAST(P.PrjCost AS decimal(18, 3)), 0.000) AS PrjCost, P.ContractSum, P.Duration, 
		P.QualityClass, P.Area, P.PrjKindClass, P.PrjPlace, P.Remark1, 
		Corp.CorpName AS Owner, P.Counsellor, P.Designer, P.Inspector, P.PrjInfo, 
		P.PrjState, P.OwnerCode, P.Rank, P.BudgetWay, P.ContractWay, 
		P.PayCondition, P.TenderWay, P.PayWay, P.KeyPart, P.WorkUnit, 
		P.LinkMan, P.PrjManager, P.BuildingType, P.TotalHouseNum, 
		P.BuildingArea, P.UsegrounArea, P.UndergroundArea, P.PrjFundInfo, 
		P.OtherStatement, P.Podepom, P.IsConfirm, P.PrjStateRemark, 
		P.xmgroup, P.grade, P.businessman, P.telephone,
		ISNULL(PZD.IsTender, 0) AS IsTender ,PZD.InputDate, PZD.ActualRunDate,
		Mwf.FlowState AS MemberFlowState, PZD.CompletedFlowState, PZD.CompletedDate, PZD.CompletedNote,
		PZD.PrjDutyPerson, yh2.v_xm AS PrjDutyName,	--项目责任人
		PZD.SetUpFlowState,  --项目立项是否审核
		P.PrjManager AS PrjMangerName, 
		CL.ItemName AS PrjStateName, BC.CodeName AS PrjKindName,ProjPeopleDep,
	(SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjMember LEFT JOIN PT_yhmc ON v_yhdm=MemberCode  WHERE PrjGuid=A.PrjGuid FOR XML PATH('')),1,1,'')
		FROM PT_PrjMember AS A
		WHERE PrjGuid= P.PrjGuid
		GROUP BY [PrjGuid]) AS MemberNames,
	 (SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjInfo_ZTB_User LEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm=PT_PrjInfo_ZTB_User.UserCode  WHERE PrjGuid=Z.PrjGuid FOR XML PATH('')),1,1,'')
		FROM PT_PrjInfo_ZTB_User AS Z
		WHERE PrjGuid= P.PrjGuid
		GROUP BY [PrjGuid]) AS PrivilegeNames,
	 PZD.prjProperty,
	 ISNULL(PrjProperty.CodeName,'') prjPropertyName
	FROM PT_PrjInfo AS P
	LEFT JOIN PT_PrjInfo_ZTB_Detail AS PZD ON PZD.PrjGuid = P.PrjGuid
	--LEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm = P.PrjManager
	LEFT JOIN PT_yhmc AS yh2 ON yh2.v_yhdm = PZD.PrjDutyPerson
	LEFT JOIN Basic_CodeList AS CL ON CL.ItemCode = P.PrjState AND	CL.TypeCode = 'ProjectState'
	LEFT JOIN XPM_Basic_CodeList AS BC ON CAST(BC.CodeId AS nvarchar(100)) = P.PrjKindClass AND BC.TypeID = '3'
    LEFT JOIN PT_PrjMemberWF AS Mwf ON P.PrjGuid=Mwf.PrjGuid
    LEFT JOIN XPM_Basic_ContactCorp AS Corp ON P.OwnerCode =Corp.CorpID
	LEFT JOIN (SELECT codeList.* FROM XPM_Basic_CodeList codeList
			   LEFT JOIN XPM_Basic_CodeType codeType ON codeList.TypeId=codeType.TypeId
			   WHERE SignCode='ProjectProperty' AND codeType.IsValid=1 
			   AND codeList.IsValid=1 ) PrjProperty ON PZD.prjProperty=PrjProperty.CodeId
	WHERE P.IsValid = '1' 
)
