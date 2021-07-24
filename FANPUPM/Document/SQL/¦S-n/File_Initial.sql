--清空数据
delete from dbo.F_FileInfo
delete from dbo.F_PersonalFile
delete from dbo.F_FileType

--文件类型信息 2010-9-9 李真
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'文件夹','/images/tree/FileInfo/folder.gif','.folder')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'word文档','/images/tree/FileInfo/word.gif','.doc')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'Excel文档','/images/tree/FileInfo/excel.gif','.xls')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'PPT文档','/images/tree/FileInfo/ppt.gif','.ppt')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'access文档','/images/tree/FileInfo/access.gif','.mdb')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'文本文档','/images/tree/FileInfo/note.gif','.txt')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'pdf阅读器','/images/tree/FileInfo/pdf.gif','.pdf')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'程序文件','/images/tree/FileInfo/exe.gif','.exe')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'html文件','/images/tree/FileInfo/html.gif','.html')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'影音文件','/images/tree/FileInfo/media.gif','.rmvb')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'压缩文件','/images/tree/FileInfo/zip.gif','.rar')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'画图文件','/images/tree/FileInfo/bmp.gif','.bmp')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'flash文件','/images/tree/FileInfo/flash.png','.swf')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'php文件','/images/tree/FileInfo/php.png','.php')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'Excel文档','/images/tree/FileInfo/xlsx.png','.xlsx')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'word文档','/images/tree/FileInfo/docx.png','.docx')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'PPT文档','/images/tree/FileInfo/pptx.png','.pptx')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'jpg图片','/images/tree/FileInfo/jpg.png','.jpg')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'gif图片','/images/tree/FileInfo/gif.png','.gif')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'png图片','/images/tree/FileInfo/png.png','.png')
insert into dbo.F_FileType(typeId,typeName,typeImg,typeSuffix) values(newid(),'其他','/images/tree/FileInfo/other.gif','.other')


--公共文件顶级目录
insert into dbo.F_FileInfo(id,FileName,FileSize,FileOwner,ParentId,FileType,UserCodes,CreateTime)
                     values('D3140694-0545-4657-BE60-4DD3C6945B63','目录','','00000000','D3140694-0545-4657-BE60-4DD3C6945B63','2','00000000',getDate())
                
--菜单
DELETE FROM PT_MK WHERE C_MKDM LIKE '04%'
GO
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('04','资料管理','','y',91,'MenuIco/13.gif',2012,3,'1','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('0401','资料管理','/FileInfoManager/FileInfoList.aspx','y',1,'',2013,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('0402','资料目录设置','/FileInfoManager/FileTypeList.aspx','y',2,'',2014,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('0403','个人资料管理','/FileInfoManager/PersonalFileList.aspx','y',3,'',2023,0,'0','0','','1')
INSERT INTO PT_MK(C_MKDM,V_MKMC,V_CDLJ,C_BS,I_XH,V_IMG,I_ID,i_ChildNum,IsBasic,IsMaintainable,helppath,Isdisplay) VALUES('0404','个人资料管理','/FileInfoManager/FileDirectory.aspx','y',4,'',2024,0,'0','0','','1')
