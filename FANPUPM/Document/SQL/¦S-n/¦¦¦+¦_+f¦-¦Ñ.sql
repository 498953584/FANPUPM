INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('30','��������','','y','92','MenuIco/13.gif','25003','6','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3001','��������','/CommonWindow/MultiClasses/ClassList.aspx?ctc=012&f=1&cn=��˾��������','y','1','','25004','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3002','��������','/oa/eFile/ManagerFileMain.aspx?ctc=012','y','2','','25005','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3003','����鵵','/oa/eFile/PigeonholeFileList.aspx','y','3','','25006','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3004','���İ���','/oa/eFile/FileLendList.aspx','y','4','','25007','0','0','0','','1'); 
INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3005','Ա����ѯ����','/oa/eFile/BorrowManageMain.aspx','y','5','','25008','0','0','0','','1'); 
--INSERT INTO PT_MK (C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES ('3006','�쵼��ѯ��Ȩ','/oa/eFile/AuthorizationList.aspx','y','6','','25009','0','0','0','','1'); 

INSERT INTO WF_BusinessCode (BusinessCode,BusinessName,KeyWord,LinkTable,PrimaryField,StateField,DoWithUrl,LookUrl,c_mkdm) 
VALUES ('010','���ӵ���','RecordID','OA_eFile_Lend','RecordID','AuditState','/oa/eFile/eFileLock.aspx','None','30'); 
GO

INSERT INTO WF_Business_Class (BusinessCode,BusinessClass,BusinessClassName) VALUES ('010','001','��������'); 
GO
