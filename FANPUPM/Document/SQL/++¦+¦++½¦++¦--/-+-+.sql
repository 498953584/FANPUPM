--最后更新时间         2014-04-14 09:55  
--v_CodeList，给XPM_Basic_CodeList表中添加TypeName，SignCode
IF OBJECT_ID('v_CodeList', 'V') IS NOT NULL
	DROP VIEW v_CodeList
GO
CREATE VIEW v_CodeList
AS
SELECT L.NoteID, L.CodeID, L.TypeID, T.TypeName, T.SignCode, L.ParentCodeList, 
    L.CodeName, L.IsValid, L.IsVisible
FROM XPM_Basic_CodeList AS L
LEFT JOIN XPM_Basic_CodeType AS T ON L.TypeID = T.TypeID
GO

--vTender
IF OBJECT_ID('vTender', 'V') IS NOT NULL
	DROP VIEW vTender
GO
CREATE VIEW vTender  
AS 
SELECT z.PrjGuid, z.PrjCode, z.PrjName,				--项目GUID、编号、名称 
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
	ISNULL((c.ItemName ), c2.ItemName) AS StateText,				--项目状态
	(SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjMember 
	   LEFT JOIN PT_yhmc ON v_yhdm=MemberCode  
	   WHERE PrjGuid=A.PrjGuid FOR XML PATH('')),1,1,'')
	   FROM PT_PrjMember AS A
	   WHERE PrjGuid= z.PrjGuid
	   GROUP BY [PrjGuid] ) AS MemberNames,
	Mwf.FlowState AS MemberFlowState
FROM PT_PrjInfo_ZTB z
LEFT JOIN PT_PrjInfo p ON z.PrjGuid = p.PrjGuid
LEFT JOIN PT_PrjInfo_ZTB_Detail zd ON z.PrjGuid = zd.PrjGuid
LEFT JOIN XPM_Basic_ContactCorp AS cc ON z.OwnerCode = CONVERT(VARCHAR(20),cc.CorpID)
LEFT JOIN Basic_CodeList c ON z.PrjState = c.ItemCode AND c.TypeCode = 'ProjectState'
LEFT JOIN Basic_CodeList c2 ON p.PrjState = c2.ItemCode AND c2.TypeCode = 'ProjectState'
LEFT JOIN PT_PrjMemberWF AS Mwf ON z.PrjGuid=Mwf.PrjGuid
GO

--vProject
IF OBJECT_ID('vProject','V') IS NOT NULL
DROP VIEW vProject
GO
CREATE VIEW vProject
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
--	 (SELECT STUFF((SELECT '、'+[v_xm] FROM PT_PrjInfo_ZTB_User LEFT JOIN PT_yhmc ON PT_yhmc.v_yhdm=PT_PrjInfo_ZTB_User.UserCode  WHERE PrjGuid=Z.PrjGuid FOR XML PATH('')),1,1,'')
--		FROM PT_PrjInfo_ZTB_User AS Z
--		WHERE PrjGuid= P.PrjGuid
--		GROUP BY [PrjGuid]) AS PrivilegeNames,
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
GO


--目标成本变更信息
IF OBJECT_ID('v_BudModifyTask') IS NOT NULL
	DROP VIEW v_BudModifyTask
GO
CREATE VIEW v_BudModifyTask
AS
SELECT MT.*, M.FlowState
FROM Bud_ModifyTask AS MT
LEFT JOIN Bud_Modify AS M ON MT.ModifyId = M.ModifyId
GO

--施工报量信息
IF OBJECT_ID('v_BudConsTask') IS NOT NULL
	DROP VIEW v_BudConsTask
GO
CREATE VIEW v_BudConsTask
AS
SELECT CT.*, CR.PrjId, CR.InputDate, CR.IsValid, CR.FlowState
FROM Bud_ConsTask AS CT
LEFT JOIN Bud_ConsReport AS CR ON CT.ConsReportId = CR.ConsReportId
GO
