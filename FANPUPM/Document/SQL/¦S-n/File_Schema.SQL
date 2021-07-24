/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2010-11-25 16:39:28                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('F_FileInfo') and o.name = 'FK_F_FILEIN_REFERENCE_F_FILEIN')
alter table F_FileInfo
   drop constraint FK_F_FILEIN_REFERENCE_F_FILEIN
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('F_PersonalFile') and o.name = 'FK_F_PERSON_REFERENCE_F_PERSON')
alter table F_PersonalFile
   drop constraint FK_F_PERSON_REFERENCE_F_PERSON
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_FileInfo')
            and   type = 'U')
   drop table F_FileInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_FileType')
            and   type = 'U')
   drop table F_FileType
go

if exists (select 1
            from  sysobjects
           where  id = object_id('F_PersonalFile')
            and   type = 'U')
   drop table F_PersonalFile
go

/*==============================================================*/
/* Table: F_FileInfo                                            */
/*==============================================================*/
create table F_FileInfo (
   Id                   nvarchar(64)         not null,
   FileName             nvarchar(Max)        null,
   FileNewName          nvarchar(Max)        null,
   FileSize             nvarchar(64)         null,
   FileOwner            nvarchar(64)         null,
   ParentId             nvarchar(64)         null,
   IsValid				bit					 null default(1),
   FileType             nvarchar(64)         null,
   UserCodes            nvarchar(Max)        null,
   CreateTime           dateTime             null,
   constraint PK_F_FILEINFO primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公共文件信息表',
   'user', @CurrentUser, 'table', 'F_FileInfo'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件ID',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件名称',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'FileName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件新名称',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'FileNewName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件大小',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'FileSize'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件所有人',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'FileOwner'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件父节点',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'ParentId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件文件夹类型0,文件1.文件夹3.类型文件夹',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'FileType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '可操作人(权限)',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'UserCodes'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'F_FileInfo', 'column', 'CreateTime'
go

/*==============================================================*/
/* Table: F_FileType                                            */
/*==============================================================*/
create table F_FileType (
   TypeId               nvarchar(64)         not null,
   TypeName             nvarchar(64)         null,
   TypeImg              nvarchar(200)        null,
   TypeSuffix           nvarchar(64)         null,
   constraint PK_F_FILETYPE primary key (TypeId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件类型表',
   'user', @CurrentUser, 'table', 'F_FileType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型ID',
   'user', @CurrentUser, 'table', 'F_FileType', 'column', 'TypeId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型名称',
   'user', @CurrentUser, 'table', 'F_FileType', 'column', 'TypeName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型图标',
   'user', @CurrentUser, 'table', 'F_FileType', 'column', 'TypeImg'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '类型扩展名',
   'user', @CurrentUser, 'table', 'F_FileType', 'column', 'TypeSuffix'
go

/*==============================================================*/
/* Table: F_PersonalFile                                        */
/*==============================================================*/
create table F_PersonalFile (
   Id                   nvarchar(64)         not null,
   FileName             nvarchar(Max)        null,
   FileNewName          nvarchar(Max)        null,
   FileSize             nvarchar(64)         null,
   FileOwner            nvarchar(64)         null,
   ParentId             nvarchar(64)         null,
   FileType             nvarchar(64)         null,
   ShareUsers           nvarchar(max)        null,
   CreateTime           dateTime             null,
   constraint PK_F_PERSONALFILE primary key (Id)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '个人文件信息表',
   'user', @CurrentUser, 'table', 'F_PersonalFile'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件ID',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'Id'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件名称',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'FileName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件新名称',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'FileNewName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件大小',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'FileSize'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件所有人',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'FileOwner'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件父节点',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'ParentId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '文件文件夹类型0,文件1.文件夹3.类型文件夹',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'FileType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '共享用户',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'ShareUsers'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'F_PersonalFile', 'column', 'CreateTime'
go

alter table F_FileInfo
   add constraint FK_F_FILEIN_REFERENCE_F_FILEIN foreign key (ParentId)
      references F_FileInfo (Id)
go

alter table F_PersonalFile
   add constraint FK_F_PERSON_REFERENCE_F_PERSON foreign key (ParentId)
      references F_PersonalFile (Id)
go



--Add by bery 2010-12-30 
--递归查询指定Id下的文件和文件夹
IF OBJECT_ID('usp_GetFiles','P') IS NOT NULL
DROP PROC usp_GetFiles
GO
CREATE PROC usp_GetFiles 
	@fileId NVARCHAR(64)
AS
--定义当前文件夹的父文件夹的Id
DECLARE @parentId NVARCHAR(64);
SELECT @parentId = ParentId FROM F_FileInfo WHERE Id =@fileId
IF(@parentId = @fileId)
	BEGIN
		--如果当前文件夹的Id = ParentId
		--表明当前文件夹是根文件夹
		SELECT * FROM F_FileInfo 
	END
ELSE
	BEGIN
		--如果当前文件夹的Id != ParentId
		--递归查询所有子文件夹
		WITH FileCTE AS
		(
			SELECT * FROM F_FileInfo
			WHERE Id = @fileId
			UNION ALL
			SELECT F_FileInfo.* FROM FileCTE
			INNER JOIN F_FileInfo ON FileCTE.Id = F_FileInfo.ParentId
		)
		SELECT * FROM FileCTE
	END

--Add by lz 2010-12-30 
--递归查询指定Id下的文件和文件夹(个人文件的查询)
IF OBJECT_ID('usp_GetPersonFiles','P') IS NOT NULL
DROP PROC usp_GetPersonFiles
GO
CREATE PROC usp_GetPersonFiles 
	@fileId NVARCHAR(64)
AS
--定义当前文件夹的父文件夹的Id
DECLARE @parentId NVARCHAR(64);
SELECT @parentId = ParentId FROM F_PersonalFile WHERE Id =@fileId
IF(@parentId = @fileId)
	BEGIN
		--如果当前文件夹的Id = ParentId
		--表明当前文件夹是根文件夹
		SELECT * FROM F_PersonalFile
	END
ELSE
	BEGIN
		--如果当前文件夹的Id != ParentId
		--递归查询所有子文件夹
		WITH FileCTE AS
		(
			SELECT * FROM F_PersonalFile
			WHERE Id = @fileId
			UNION ALL
			SELECT F_PersonalFile.* FROM FileCTE
			INNER JOIN F_PersonalFile ON FileCTE.Id = F_PersonalFile.ParentId
		)
		SELECT * FROM FileCTE
	END
