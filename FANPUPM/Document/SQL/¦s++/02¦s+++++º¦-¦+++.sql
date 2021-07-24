
DELETE FROM dbo.WF_Template where BusinessCode<>'999'
DELETE FROM dbo.WF_TemplateNode
DELETE FROM WF_Business_Class  where BusinessCode=999 AND BusinessClass != '010'
IF ((SELECT COUNT(*) FROM WF_Business_Class WHERE BusinessCode = '999') = 0)
	INSERT INTO WF_Business_Class VALUES(999,'010','国际工程公司')

--邮件日志
DELETE  FROM pt_dzyj_sj 
DELETE FROM pt_dzyj_yj
delete from pt_dzyj_fj  
delete from pt_dzyj_tempfj

/************************************
清空除管理员以外的用户
************************************/
DELETE FROM [dbo].[HR_Leave_Application]
DELETE FROM [dbo].[HR_Leave_Stat]
DELETE FROM [dbo].[HR_Org_Adjust]
DELETE FROM [dbo].[HR_Org_AveragePay]
DELETE FROM [dbo].[HR_Org_ManpowerPlan]
DELETE FROM [dbo].[HR_Org_PostLevel]
DELETE FROM [dbo].[HR_Personnel_Education]
DELETE FROM [dbo].[HR_Personnel_Experience]
DELETE FROM [dbo].[HR_Personnel_FamilyMembers]
DELETE FROM [dbo].[HR_Personnel_RewardsAndPunishment]
DELETE FROM [dbo].[HR_Personnel_Train]
DELETE FROM dbo.EPM_Hr_Credit
DELETE FROM dbo.EPM_Hr_Penalize
DELETE FROM  dbo.EPM_Hr_People
DELETE FROM  dbo.EPM_Hr_Society
DELETE FROM dbo.EPM_Hr_Station
DELETE FROM dbo.EPM_Hr_Study
DELETE FROM dbo.EPM_Hr_Work
----DELETE FROM 
DELETE FROM [dbo].[PT_LOG]
DELETE FROM [dbo].[pt_loginRtxInit]
DELETE FROM [dbo].[PT_LOGIN]
      WHERE v_yhdm<>'00000000'
DELETE FROM [dbo].[PT_YHMC_Privilege]
      WHERE v_yhdm<>'00000000' 
DELETE FROM [dbo].[PT_XTRZ]

DELETE FROM [dbo].[PT_DBSJ]
DELETE FROM [dbo].[PT_DBSJ_Today]
DELETE FROM PT_Manager_Privilege
DELETE FROM PT_Prj_Completed_Detail
DELETE FROM PT_Prj_Completed
DELETE FROM PT_PrjMember
DELETE FROM Con_Incomet_Contract
DELETE FROM [dbo].[PT_yhmc]
      WHERE v_yhdm<>'00000000' 
--DELETE 语句与 REFERENCE 约束"FK__PT_PrjMem__Membe__40071901"冲突。该冲突发生于数据库"pm2_bery"，表"dbo.PT_PrjMember", column 'MemberCode'。
/************************************
清空部门
************************************/


update [dbo].[PT_yhmc] set i_bmdm=1
DELETE FROM [dbo].[PT_DUTY]
delete from PT_Role_Privilege
DELETE FROM [dbo].[PT_d_bm] where i_bmdm<>1 
DELETE FROM [dbo].[pt_RtxDeptInit]
DELETE FROM [dbo].[PT_d_CorpCode] where CorpCode<>'00'
delete from pt_role
delete from PT_d_role

/*     
*/

--初始化登录次数
UPDATE PT_D_CS SET ptcs=1 WHERE id=1

