INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('30','档案管理','','y','92','MenuIco/13.gif','25003','6','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3001','档案分类','/CommonWindow/MultiClasses/ClassList.aspx?ctc=012&f=1&cn=公司档案分类','y','1','','25004','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3002','档案管理','/oa/eFile/ManagerFileMain.aspx?ctc=012','y','2','','25005','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3003','分类归档','/oa/eFile/PigeonholeFileList.aspx','y','3','','25006','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3004','借阅办理','/oa/eFile/FileLendList.aspx','y','4','','25007','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3005','员工查询借阅','/oa/eFile/BorrowManageMain.aspx','y','5','','25008','0','0','0','','1'); 
--INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3006','领导查询授权','/oa/eFile/AuthorizationList.aspx','y','6','','25009','0','0','0','','1'); 

INSERT INTO WF_BusinessCode (BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,c_mkdm) 
VALUES ('010','电子档案','RecordID','OA_eFile_Lend','RecordID','AuditState','/oa/eFile/eFileLock.aspx','None','30'); 
GO

INSERT INTO WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName) VALUES ('010','001','档案借阅'); 
GO
