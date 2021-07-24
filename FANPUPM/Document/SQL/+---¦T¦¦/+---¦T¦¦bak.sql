ALTER TABLE EPM_Datum_Affair ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE EPM_Datum_Affair ADD filesType INT --归档到类型下
UPDATE EPM_Datum_Affair SET mark =0; 
--EPM_Datum_Criterion 2011年9月27日 lpw
ALTER TABLE EPM_Datum_Criterion ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE EPM_Datum_Criterion ADD filesType INT --归档到类型下                                               
UPDATE EPM_Datum_Criterion SET mark =0;    
------施工组织设计添加归档功能  slm 20110929
ALTER TABLE Prj_TechnologyConstructOrganize ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE Prj_TechnologyConstructOrganize ADD filesType INT --归档到类型下                                               
UPDATE Prj_TechnologyConstructOrganize SET mark =2;  

-----专项方案添加自动归档

ALTER TABLE Prj_ExpertSchemeManage ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE Prj_ExpertSchemeManage ADD filesType INT --归档到类型下                                               
UPDATE Prj_ExpertSchemeManage SET mark =2;  

-- 2011年9月29日 lpw
ALTER TABLE Ent_Quality_Goal ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE Ent_Quality_Goal ADD filesType INT --归档到类型下
UPDATE Ent_Quality_Goal SET mark =0;

--2011年10月8日 lpw 归档
ALTER TABLE Prj_TechnologyManage ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE Prj_TechnologyManage ADD filesType INT --归档到类型下
UPDATE Prj_TechnologyManage SET mark =0;

ALTER TABLE EPM_Datum_Affair ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE EPM_Datum_Affair ADD filesType INT --归档到类型下
UPDATE EPM_Datum_Affair SET mark =0;
ALTER TABLE Files ADD sid_ColumnName VARCHAR(50)
ALTER TABLE Files ADD sid_ColumnType INT
ALTER TABLE Files ADD prjcode varchar(64)


----技术总结归档  slm  2011-10-9
ALTER TABLE Prj_Summary ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE Prj_Summary ADD filesType INT --归档到类型下                                               
UPDATE Prj_Summary SET mark =2;  


ALTER TABLE Prj_TechnologyCriterion ADD mark INT -- 标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
ALTER TABLE Prj_TechnologyCriterion ADD filesType INT --归档到类型下                                               
UPDATE Prj_TechnologyCriterion SET mark =2;  



ALTER TABLE Prj_ProgressPlan ADD filesType int
ALTER TABLE Prj_ProgressPlan ADD mark INT --标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
UPDATE Prj_ProgressPlan SET mark =0; 
ALTER TABLE Prj_ItemInspect ADD filesType int
ALTER TABLE Prj_ItemInspect ADD mark INT
UPDATE Prj_ItemInspect SET mark =0;

ALTER TABLE Prj_ItemProg ADD filesType int
ALTER TABLE Prj_ItemProg ADD mark INT --标示状态,1:已归档 2:未选择作为归档资料 3:选择为归档资料
UPDATE Prj_ItemProg SET mark =2;



SELECT * FROM EPM_Datum_Affair


SELECT * FROM v_Quality_Goal


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[v_Quality_Goal]
AS
SELECT     TOP (100) PERCENT dbo.Ent_Quality_Goal.i_id, dbo.Ent_Quality_Goal.PrjCode, dbo.Ent_Quality_Goal.ScheduleCode, dbo.Ent_Quality_Goal.QualityGoal, 
                      dbo.Ent_Quality_Goal.Remark, dbo.EPM_Task_TaskList.TaskName, dbo.PT_PrjInfo.PrjName, dbo.Ent_Quality_Goal.mark, dbo.Ent_Quality_Goal.filesType
FROM         dbo.Ent_Quality_Goal INNER JOIN
                      dbo.EPM_Task_TaskList ON dbo.Ent_Quality_Goal.PrjCode = dbo.EPM_Task_TaskList.ProjectCode AND 
                      dbo.Ent_Quality_Goal.ScheduleCode = dbo.EPM_Task_TaskList.TaskCode INNER JOIN
                      dbo.PT_PrjInfo ON dbo.Ent_Quality_Goal.PrjCode = dbo.PT_PrjInfo.PrjGuid
ORDER BY dbo.Ent_Quality_Goal.ScheduleCode
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





SELECT * FROM v_Safty_Measure


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[v_Safty_Measure]
AS
SELECT     TOP (100) PERCENT dbo.Ent_Safty_Measure.i_id, dbo.Ent_Safty_Measure.PrjCode, dbo.PT_PrjInfo.PrjName, dbo.Ent_Safty_Measure.ScheduleCode, 
                      dbo.Ent_Safty_Measure.SaftyMeasure, dbo.Ent_Safty_Measure.Remark, dbo.EPM_Task_TaskList.TaskName, dbo.Ent_Safty_Measure.mark, 
                      dbo.Ent_Safty_Measure.filesType
FROM         dbo.Ent_Safty_Measure INNER JOIN
                      dbo.EPM_Task_TaskList ON dbo.Ent_Safty_Measure.PrjCode = dbo.EPM_Task_TaskList.ProjectCode AND 
                      dbo.Ent_Safty_Measure.ScheduleCode = dbo.EPM_Task_TaskList.TaskCode INNER JOIN
                      dbo.PT_PrjInfo ON dbo.Ent_Safty_Measure.PrjCode = dbo.PT_PrjInfo.PrjGuid
ORDER BY dbo.Ent_Safty_Measure.ScheduleCode
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




SELECT * FROM Prj_V_ScienceInnovate


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Prj_V_ScienceInnovate]  
AS
SELECT dbo.Prj_TechnologyConstructOrganize.* 
FROM dbo.Prj_TechnologyConstructOrganize
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO




SELECT * FROM Prj_V_ExpertProject


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Prj_V_ExpertProject]
AS
SELECT dbo.Prj_ExpertSchemeManage.*, dbo.PT_D_BM.V_BMMC, 
      dbo.Prj_ProjectInfo.PrjName,
          (SELECT dbo.pt_yhmc.v_xm
         FROM dbo.pt_yhmc
         WHERE dbo.pt_yhmc.v_yhdm = dbo.Prj_ExpertSchemeManage.WeavePeople) 
      AS Weavemc,
          (SELECT dbo.pt_yhmc.v_xm
         FROM dbo.pt_yhmc
         WHERE dbo.pt_yhmc.v_yhdm = dbo.Prj_ExpertSchemeManage.FillPeople) 
      AS fillmc
FROM dbo.Prj_ExpertSchemeManage INNER JOIN
      dbo.Prj_ProjectInfo ON 
      dbo.Prj_ExpertSchemeManage.PrejectName = dbo.Prj_ProjectInfo.PrjCode INNER JOIN
      dbo.PT_D_BM ON dbo.Prj_ExpertSchemeManage.PrjCode = dbo.PT_D_BM.i_bmdm
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





SELECT * FROM Prj_TechnologyCriterion





SELECT * FROM Prj_TechnologyManage

SELECT * FROM Prj_V_TechnologyJD



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER VIEW [dbo].[Prj_V_TechnologyJD]
AS
SELECT     MainID AS Expr1, PrjCode AS Expr2, SerialNumber AS Expr3, FillDate AS Expr4, FillPeople AS Expr5, PrejectName AS Expr6, ConstructionUnit AS Expr7, 
                      ByTellUnit AS Expr8, TellPeople AS Expr9, ByTellPeople AS Expr10, TellLocus AS Expr11, TellDate AS Expr12, TellContentAbstract AS Expr13, Remark AS Expr14,
                          (SELECT     v_xm
                            FROM          dbo.PT_yhmc
                            WHERE      (v_yhdm = dbo.Prj_TechnologyTell.FillPeople)) AS FillName,
                          (SELECT     v_xm
                            FROM          dbo.PT_yhmc AS PT_yhmc_2
                            WHERE      (v_yhdm = dbo.Prj_TechnologyTell.TellPeople)) AS TellName,
                          (SELECT     v_xm
                            FROM          dbo.PT_yhmc AS PT_yhmc_1
                            WHERE      (v_yhdm = dbo.Prj_TechnologyTell.ByTellPeople)) AS ByTellName, dbo.Prj_TechnologyTell.*, mark AS Expr15, filesType AS Expr16
FROM         dbo.Prj_TechnologyTell
--2011年11月10日 更新补充 项目奖罚 视图SQL语句 lpw
GO

SET ANSI_NULLS OFF
GO
SET QUOTED_IDENTIFIER OFF
GO





SELECT * FROM Prj_Summary 

SELECT * FROM Prj_ItemInspect

SELECT * FROM Prj_ItemProg 


























