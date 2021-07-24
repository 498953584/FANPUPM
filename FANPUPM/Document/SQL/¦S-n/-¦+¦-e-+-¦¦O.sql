
--更新实施人，存储名称换成存储编码
UPDATE PT_PrjInfo_ZTB SET linkMan = (
	SELECT PT_yhmc.v_yhdm 
	FROM PT_yhmc
	WHERE PT_yhmc.v_xm = PT_PrjInfo_ZTB.LinkMan
)
WHERE PrjGuid IN(
	SELECT PrjGuid FROM PT_PrjInfo_ZTB
	LEFT JOIN PT_yhmC ON PT_yhmC.v_xm = PT_PrjInfo_ZTB.LinkMan
	WHERE v_yhdm IS NOT NULL
)
GO
--更新项目经理，存储编码-名称换成存储编码
--UPDATE PT_PrjInfo_ZTB SET PrjManager = (
--	SELECT PT_yhmc.v_yhdm 
--	FROM PT_yhmc		
--	WHERE PT_yhmc.v_yhdm = SUBSTRING(PT_PrjInfo_ZTB.PrjManager, 1, 8)	--按编码
--)
--WHERE PrjGuid IN(
--	SELECT PrjGuid FROM PT_PrjInfo_ZTB
--	LEFT JOIN PT_yhmC ON PT_yhmC.v_yhdm = SUBSTRING(PT_PrjInfo_ZTB.PrjManager, 1, 8)
--	WHERE v_yhdm IS NOT NULL
--)
GO
UPDATE PT_PrjInfo_ZTB SET PrjManager = (
	SELECT PT_yhmc.v_yhdm 
	FROM PT_yhmc
	WHERE PT_yhmc.v_xm = SUBSTRING(PrjManager,10,3)
)
WHERE PrjGuid IN(
	SELECT PrjGuid FROM PT_PrjInfo_ZTB
	LEFT JOIN PT_yhmC ON PT_yhmC.v_xm = SUBSTRING(PrjManager,10,3)   --按名称
	WHERE v_yhdm IS NOT NULL
)
GO
--将信息立项和投标数据添加到PT_PrjInfo_ZTB_Detail(孙新华更改 IsTender 1表示是投标转来的，0表示不是投标转来的)
INSERT INTO PT_PrjInfo_ZTB_Detail(PrjGuid,Grade,Telephone,BusinessManager,ProjFlowSate,IsTender)
SELECT ZTB.PrjGuid, NULLIF(Stock.grade,'') AS Grade
	,NULLIF(Stock.telephone,'') AS Telephone ,yh3.v_yhdm AS BusinessManCode, '-1','1'
FROM PT_PrjInfo_ZTB AS ZTB
LEFT JOIN PT_PrjInfo_ZTB_Stock AS Stock ON ZTB.PrjGuid = Stock.PrjGuid
LEFT JOIN PT_yhmc AS yh3 ON LEFT(Stock.businessman,8)= yh3.v_yhdm
WHERE ZTB.PrjGuid NOT IN (SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail)
	AND ZTB.PrjState IN ('-2', '-3')
GO

--投标项目添加管理员权限
INSERT INTO PT_PrjInfo_ZTB_User(Id, PrjGuid, UserCode)
SELECT NEWID(), PrjGuid, '00000000'
FROM PT_PrjInfo_ZTB_Detail
WHERE PrjGuid NOT IN(
	SELECT PrjGuid FROM PT_PrjInfo_ZTB_User
	WHERE UserCode = '00000000'
)
GO
--给投标中的实施人添加项目权限
INSERT INTO PT_PrjInfo_ZTB_User(Id, PrjGuid, UserCode)
SELECT NEWID(), PrjGuid, LinkMan 
FROM PT_PrjInfo_ZTB AS ZTB
WHERE LinkMan IN (SELECT v_yhdm FROM PT_yhmc)
	AND NOT EXISTS (
		SELECT * FROM PT_PrjInfo_ZTB_User AS U
		WHERE U.PrjGuid = ZTB.PrjGuid AND U.UserCode = ZTB.LinkMan 
	)
	AND ZTB.PrjState IN ('-2', '-3') 
GO
--项目经理添加权限
INSERT INTO PT_PrjInfo_ZTB_User(Id, PrjGuid, UserCode)
SELECT NEWID(), PrjGuid, PrjManager 
FROM PT_PrjInfo_ZTB AS ZTB
WHERE PrjManager IN (SELECT v_yhdm FROM PT_yhmc)
	AND NOT EXISTS (
		SELECT * FROM PT_PrjInfo_ZTB_User AS U
		WHERE U.PrjGuid = ZTB.PrjGuid AND U.UserCode = ZTB.LinkMan
	)
	AND ZTB.PrjState IN ('-2', '-3') 
GO

----更新信息立项状态
--UPDATE PT_PrjInfo_ZTB SET PrjState = 2 WHERE PrjState = -3
----更新启动状态
--UPDATE PT_PrjInfo_ZTB SET PrjState = 3 WHERE PrjState = -2
--GO



--移植项目规划
--更新实施人，存储名称换成存储编码
UPDATE PT_PrjInfo_ZTB SET linkMan = (
	SELECT PT_yhmc.v_yhdm 
	FROM PT_yhmc
	WHERE PT_yhmc.v_xm = PT_PrjInfo_ZTB.LinkMan
)
WHERE PrjGuid IN(
	SELECT PrjGuid FROM PT_PrjInfo_ZTB
	LEFT JOIN PT_yhmC ON PT_yhmC.v_xm = PT_PrjInfo_ZTB.LinkMan
	WHERE v_yhdm IS NOT NULL
)
GO
--更新项目经理，存储编码-名称换成存储编码
UPDATE PT_PrjInfo SET PrjManager = (
	SELECT PT_yhmc.v_yhdm 
	FROM PT_yhmc		
	WHERE PT_yhmc.v_yhdm = SUBSTRING(PT_PrjInfo.PrjManager, 1, 8)	--按编码
)
WHERE PrjGuid IN(
	SELECT PrjGuid FROM PT_PrjInfo
	LEFT JOIN PT_yhmC ON PT_yhmC.v_yhdm = SUBSTRING(PT_PrjInfo.PrjManager, 1, 8)
	WHERE v_yhdm IS NOT NULL
)
GO
UPDATE PT_PrjInfo SET PrjManager = (
	SELECT PT_yhmc.v_yhdm 
	FROM PT_yhmc
	WHERE PT_yhmc.v_xm = SUBSTRING(PrjManager,10,3)
)
WHERE PrjGuid IN(
	SELECT PrjGuid FROM PT_PrjInfo
	LEFT JOIN PT_yhmC ON PT_yhmC.v_xm = SUBSTRING(PrjManager,10,3)   --按名称
	WHERE v_yhdm IS NOT NULL
)
GO

--更新项目状态  孙新华 注释掉  2012-03-20
--UPDATE PT_PrjInfo SET PrjState = BL.ItemCode
--FROM PT_PrjInfo AS P
--LEFT JOIN XPM_Basic_CodeList AS XL ON XL.CodeID = P.PrjState
--LEFT JOIN Basic_CodeList AS BL ON BL.itemName = XL.CodeName
--WHERE TypeID = '7' AND BL.TypeCode = 'ProjectState'
--GO
--给PT_PrjInfo_ZTB_Detail 添加原来的项目
INSERT INTO PT_PrjInfo_ZTB_Detail(PrjGuid, InputUser, InputDate, IsTender)
SELECT PrjGuid, UserCode, RecordDate, 0
FROM PT_PrjInfo
WHERE PrjGuid NOT IN(SELECT PrjGuid FROM PT_PrjInfo_ZTB_Detail)
GO
--投标项目添加管理员权限
INSERT INTO PT_PrjInfo_ZTB_User(Id, PrjGuid, UserCode)
SELECT NEWID(), PrjGuid, '00000000'
FROM PT_PrjInfo
WHERE PrjGuid NOT IN(
	SELECT PrjGuid FROM PT_PrjInfo_ZTB_User
	WHERE UserCode = '00000000'
)
GO
--给投标中的实施人添加项目权限
INSERT INTO PT_PrjInfo_ZTB_User(Id, PrjGuid, UserCode)
SELECT NEWID(), PrjGuid, LinkMan 
FROM PT_PrjInfo AS P
WHERE LinkMan IN (SELECT v_yhdm FROM PT_yhmc)
	AND NOT EXISTS (
		SELECT * FROM PT_PrjInfo_ZTB_User AS U
		WHERE U.PrjGuid = P.PrjGuid AND U.UserCode = P.LinkMan 
	)
GO
--项目经理添加权限
INSERT INTO PT_PrjInfo_ZTB_User(Id, PrjGuid, UserCode)
SELECT NEWID(), PrjGuid, PrjManager 
FROM PT_PrjInfo AS P
WHERE PrjManager IN (SELECT v_yhdm FROM PT_yhmc)
	AND NOT EXISTS (
		SELECT * FROM PT_PrjInfo_ZTB_User AS U
		WHERE U.PrjGuid = P.PrjGuid AND U.UserCode = P.LinkMan
	)
GO

--删除重复数据
 DELETE FROM PT_PrjInfo_ZTB
 WHERE PrjGuid IN ( 
	SELECT PrjGuid FROM PT_PrjInfo
 )

--项目经理存储编号改为存储名称  --根据艾芙兰要求修改
UPDATE PT_PrjInfo 
SET    PrjManager = T2.V_XM 
FROM   PT_PrjInfo AS T1 
       INNER JOIN PT_YHMC AS T2 
         ON T1.PrjManager = T2.V_YHDM 


--给项目成员审核添加默认值
UPDATE PT_PrjInfo_ZTB_Detail SET MemberFlowState = '-1'

--项目状态更改   孙新华添加  2012-03-20 9:00
--将老项目的在建状态修改成新项目的在建状态
UPDATE PT_PrjInfo SET PrjState=11
WHERE PrjState =(SELECT CodeID FROM XPM_Basic_CodeList WHERE TypeID='7' AND CodeName='维保')

--验收
UPDATE PT_PrjInfo SET PrjState=9
WHERE PrjState =(SELECT CodeID FROM XPM_Basic_CodeList WHERE TypeID='7' AND CodeName='验收')

--停工
UPDATE PT_PrjInfo SET PrjState=8
WHERE PrjState =(SELECT CodeID FROM XPM_Basic_CodeList WHERE TypeID='7' AND CodeName='停工')

--竣工
UPDATE PT_PrjInfo SET PrjState=10
WHERE PrjState =(SELECT CodeID FROM XPM_Basic_CodeList WHERE TypeID='7' AND CodeName='竣工')

UPDATE PT_PrjInfo SET PrjState=7
WHERE PrjState =(SELECT CodeID FROM XPM_Basic_CodeList WHERE TypeID='7' AND CodeName='在建')


--注：还有一些项目状态值为10的，状态为13的，怎么转


