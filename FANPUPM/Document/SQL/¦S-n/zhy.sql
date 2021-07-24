/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2011/6/8 10:48:15         新建表   BindBudget   预算绑定  slm
2011.07.01
               */
/*==============================================================*/


if exists (select 1
            from  sysobjects
           where  id = object_id('BindBudget')
            and   type = 'U')
   drop table BindBudget
go

if exists (select 1
            from  sysobjects
           where  id = object_id('Templates')
            and   type = 'U')
   drop table Templates
go

/*==============================================================*/
/* Table: BindBudget                                            */
/*==============================================================*/
create table BindBudget (
   NoteId               int                  identity,
   PrjCode              uniqueidentifier     null,
   Templatescode        varchar(30)          null,
   TaskCode             varchar(30)          null,
   constraint PK_BINDBUDGET primary key (NoteId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '绑定预算',
   'user', @CurrentUser, 'table', 'BindBudget'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'NoteId标识列',
   'user', @CurrentUser, 'table', 'BindBudget', 'column', 'NoteId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '项目guid',
   'user', @CurrentUser, 'table', 'BindBudget', 'column', 'PrjCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模板code',
   'user', @CurrentUser, 'table', 'BindBudget', 'column', 'Templatescode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '预算code',
   'user', @CurrentUser, 'table', 'BindBudget', 'column', 'TaskCode'
go

/*==============================================================*/
/* Table: Templates          
新建表      Templates   模板表    slm
  2011.7.1                             */
/*==============================================================*/
create table Templates (
   TemplatesCode        varchar(30)          not null,
   TemplatesName        varchar(50)          null,
   ParentCode           varchar(30)          null,
   constraint PK_TEMPLATES primary key (TemplatesCode)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模板',
   'user', @CurrentUser, 'table', 'Templates'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '模板code',
   'user', @CurrentUser, 'table', 'Templates', 'column', 'TemplatesCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '任务名称',
   'user', @CurrentUser, 'table', 'Templates', 'column', 'TemplatesName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父级节点',
   'user', @CurrentUser, 'table', 'Templates', 'column', 'ParentCode'
go

