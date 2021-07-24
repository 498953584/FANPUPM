--最后更新时间     2014-06-04 11:13 

--流程存储过程 p_wf_beginflow 在提交审核时使用
IF OBJECT_ID('p_wf_beginflow','P') IS NOT NULL
	DROP PROC p_wf_beginflow
GO
CREATE procedure [dbo].p_wf_beginflow
	@businessclass varchar(3),   --业务分类代码  001    
	@businesscode varchar(50),   --业务代码      071    
	@instancecode uniqueidentifier, --具体业务Guid   f232131d-c100-47fc-9d43-35b18b8d088c
	@templateid int,   --模板ID                      351
	@nodeid int,	   --分支后第一个节点ID          931
	@offsetorder int,  --分支后第一个节点序号        1
	@projectCode varchar(50),  --项目代码            c79d1bc0-3460-4187-8fca-7c24c2b2b48e
	@userCode varchar(10)     --用户代码             00000000
AS
BEGIN
	SET NOCOUNT ON
	--定义变量
	DECLARE @tablename varchar(50)
	DECLARE @fieldname varchar(50)
	DECLARE @primaryname varchar(50)
	DECLARE @sql nvarchar(500)
	DECLARE @sp nvarchar(500)
	DECLARE @i int
	DECLARE @id int
	DECLARE @instanceid int
	DECLARE @nodeid1 int 
	DECLARE @nodeid2 int 
	DECLARE @offsetorder1 int
	DECLARE @offsetorder2 int
	--节点临时表变量
	DECLARE @templatenode table  
	(
		templateid int,
		NodeID int NOT NULL ,
		NodeName varchar(50)  ,
		operatertype char(1)  ,
		Operater nvarchar(Max) ,
		FrontNode varchar(100) ,
		IsSendMsg char(1),
		IsAllPass char(1),
		During int,
		theOrder decimal(3,1)
	)

	BEGIN TRAN
	SET @nodeid1 = @nodeid
	SET @nodeid2 = @nodeid
	SET @offsetorder1 = @offsetorder 
	SET @offsetorder2 = @offsetorder   
	--将当前节点信息插入临时表变量中(插入的是第一个审批节点的数据）
	INSERT INTO @templatenode 
		SELECT @templateid,nodeid,nodename,auditortype,operater,frontnode,issendmsg,isallpass,during,@offsetorder
		FROM wf_templatenode WHERE templateid=@templateid AND nodeid=@nodeid 
		
	IF @offsetorder > 1  --分支后第一个节点序号大于1时  
	BEGIN
		--将该节点前各节点插入临时表变量中
		WHILE @offsetorder1 > 0 
		BEGIN
			SELECT @nodeid1 = nodeid FROM wf_templatenode 
            WHERE nodeid= (SELECT cast(frontnode AS int) FROM wf_templatenode WHERE templateid=@templateid AND nodeid=@nodeid1 )
            
			INSERT INTO @templatenode 
			    SELECT @templateid,nodeid,nodename,auditortype,operater,frontnode,issendmsg,isallpass,during,@offsetorder1 
                FROM wf_templatenode 
			    WHERE nodeid = @nodeid1 AND templateid=@templateid 
			SET @offsetorder1 = @offsetorder1 -1

		END 
		--将后序各节点插入临时表变量中
		WHILE EXISTS 
        (
            SELECT 1 FROM wf_templatenode 
            WHERE charindex(','+cast(@nodeid2 AS varchar(15))+',',','+frontnode+',') > 0 AND templateid=@templateid
        )
		BEGIN
			SELECT @nodeid2 = nodeid FROM wf_templatenode 
            WHERE charindex(','+cast(@nodeid2 AS varchar(15))+',',','+frontnode+',') > 0 AND templateid=@templateid
			SET @offsetorder2 = @offsetorder2 + 1
			INSERT INTO @templatenode 
				SELECT @templateid,nodeid,nodename,auditortype,operater,frontnode,issendmsg,isallpass,during,@offsetorder2 
                FROM wf_templatenode WHERE nodeid = @nodeid2 AND templateid=@templateid 
		END 
	END 
	ELSE --分支后第一个节点序号等于1时,将后序各节点插入临时表变量中  
	BEGIN
		WHILE EXISTS (
            SELECT 1 FROM wf_templatenode 
            WHERE charindex(','+cast(@nodeid AS varchar(15))+',',','+frontnode+',') > 0 
            AND templateid=@templateid
        )
		BEGIN
			SELECT @nodeid = nodeid FROM wf_templatenode 
            WHERE charindex(','+cast(@nodeid AS varchar(15))+',',','+frontnode+',') > 0 
            AND templateid=@templateid  
			SET @offsetorder = @offsetorder + 1 
			INSERT INTO @templatenode 
				SELECT @templateid,nodeid,nodename,auditortype,(case when IsSelReceiver='1' then '' when IsSelReceiver='0' then operater END ) AS operater,frontnode,issendmsg,isallpass,during,@offsetorder 
                FROM wf_templatenode WHERE nodeid = @nodeid AND templateid=@templateid 
		END 
	END 
	--SELECT * FROM @templatenode
	COMMIT TRAN

--创建审核节点的临时表
BEGIN TRAN
DECLARE @Instance TABLE (
	[NoteID] [int] IDENTITY(1,1) NOT NULL,
	[ID] [int] NULL,
	[NodeID] [int] NULL,
	[NodeName] [varchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Operator] [varchar](8) COLLATE Chinese_PRC_CI_AS NULL,
	[TheOrder] [decimal](3, 1) NULL,
	[Sing] [int] NULL,
	[AuditDate] [datetime] NULL,
	[AuditResult] [bit] NULL,
	[AuditInfo] [varchar](200) COLLATE Chinese_PRC_CI_AS NULL,
	[IsSendMsg] [char](1) COLLATE Chinese_PRC_CI_AS NULL,
	[IsAllPass] [char](1) COLLATE Chinese_PRC_CI_AS NULL,
	[ArriveTime] [datetime] NULL,
	[During] [int] NULL,
	[OutOfTime] [datetime] NULL,
	[IsInsertedFrontNode] [char](1)  NULL
	)
	
--创建完成
COMMIT TRAN
	SELECT @tablename = LinkTable,@fieldname=StateField,@primaryname=PrimaryField FROM WF_BusinessCode WHERE businesscode=@businesscode
	IF 1 = 1
	BEGIN
		BEGIN TRAN
		--向工作流转主表插入数据
		INSERT INTO WF_Instance_Main VALUES(@businesscode,@businessclass,@templateid,@instancecode,@userCode,getdate())
		SELECT @id=@@IDENTITY 
		IF @@error <> 0 
		BEGIN
			ROLLBACK TRAN
			RETURN
		END 
		--定义节点游标
		DECLARE cur_node cursor for
		SELECT nodeid,nodename,operater,operatertype,IsSendMsg,IsAllPass,During,theOrder 
        FROM @templatenode WHERE templateid=@templateid order by theOrder asc
		DECLARE @nodename varchar(50)
		DECLARE @issendmsg char(1)
		DECLARE @isallpass char(1)
		DECLARE @during int
		DECLARE @theorder int
		DECLARE @state int              
		DECLARE @operater nvarchar(Max)
		DECLARE @operatertype int
		DECLARE @roletype int
		DECLARE @userCodes varchar(1000)
		DECLARE @tempUser varchar(1000) 
		SET @i = 0
		OPEN cur_node
		FETCH NEXT FROM cur_node INTO @nodeid,@nodename,@operater,@operatertype,@issendmsg,@isallpass,@during,@theorder
		WHILE @@fetch_status = 0			
		BEGIN
			IF @i=0
			BEGIN
				IF @theorder = 1
					SET @state = 0
				ELSE
					SET @state = -1
					
				IF @operatertype = 1 --单人
				BEGIN
					IF EXISTS (SELECT SecondUser FROM wf_agent WHERE TemplateID=@templateid AND MainUser=@operater AND TempNodeId=@nodeid AND bt_Use=1)
				    BEGIN
						--存在代办人
						DECLARE @AgentUser varchar(1000)
						SELECT @AgentUser=SecondUser FROM wf_agent WHERE TemplateID=@templateid AND MainUser=@operater AND TempNodeId=@nodeid AND bt_Use=1

						INSERT INTO @Instance
							VALUES(@id,@nodeid,@nodename+'(代办)',@AgentUser,cast(@theorder AS decimal(3,1)),@state,
								null,'','',@issendmsg,'0',getdate(),@during,dateadd(hour,@during,getdate()),'0')
						SELECT @instanceid=@@IDENTITY 
						
						INSERT INTO @Instance
							VALUES(@id,@nodeid,@nodename,@operater,cast(@theorder AS decimal(3,1)),@state,
								null,'','',@issendmsg,'0',getdate(),@during,dateadd(hour,@during,getdate()),'0')
						SELECT @instanceid=@@IDENTITY
					END
					ELSE
				    BEGIN	 
						--不存在代办人，向工作流程流转表中插入数据
						INSERT INTO @Instance
							VALUES(@id,@nodeid,@nodename,@operater,cast(@theorder AS decimal(3,1)),@state,
								null,'','',@issendmsg,@isallpass,getdate(),@during,dateadd(hour,@during,getdate()),'0')
						SELECT @instanceid=@@IDENTITY
					 END 
					IF @@error <> 0 
					BEGIN
						ROLLBACK TRAN
						RETURN
					END
				END
				ELSE
				BEGIN
				  IF @operater=''
				  BEGIN
						--向工作流程流转表中插入数据
						INSERT INTO @Instance  VALUES(@id,@nodeid,@nodename,@operater,cast(@theorder AS decimal(3,1)),@state
							,null,'','',@issendmsg,@isallpass,getdate(),@during,dateadd(hour,@during,getdate()),'0')
						SELECT @instanceid=@@IDENTITY
				  END
				  ELSE
				  BEGIN  
					IF @operatertype = 2 --多人
					BEGIN 
						DECLARE cur_user3 cursor for
						SELECT v_yhdm FROM pt_yhmc WHERE charindex(v_yhdm,@operater) > 0 
						OPEN cur_user3
						FETCH NEXT FROM cur_user3 INTO @tempuser
						WHILE @@fetch_status = 0 
						BEGIN
							--向工作流程流转表中插入数据
							INSERT INTO @Instance  VALUES(@id,@nodeid,@nodename,@tempuser,cast(@theorder AS decimal(3,1)),@state
                                ,null,'','',@issendmsg,@isallpass,getdate(),@during,dateadd(hour,@during,getdate()),'0')
							SELECT @instanceid=@@IDENTITY
							IF @@error <> 0 
							BEGIN
								ROLLBACK TRAN
								RETURN
							END
							FETCH NEXT FROM cur_user3 INTO @tempuser
						END 
						CLOSE cur_user3
						DEALLOCATE cur_user3
					END 
					ELSE	--角色
					BEGIN
						SELECT @roletype = roletype FROM wf_role WHERE ltrim(rtrim(str(roleid)))=@operater 
						DECLARE @tempuser1 varchar(10)
						IF @roletype = 1	--项目相关
						BEGIN							
							DECLARE cur_user cursor for									
							SELECT distinct usercode FROM dbo.WF_RoleProject WHERE ltrim(rtrim(str(roleid)))=@operater 
							AND charindex(','+cast((SELECT  i_xh FROM pt_prjinfo WHERE prjguid=@projectCode) AS varchar(15)) + 
                                ',',','+Projectcodes+',') > 0
							SET @userCodes = ''
							OPEN cur_user
							FETCH NEXT FROM cur_user INTO @tempuser1
							WHILE @@fetch_status = 0
							BEGIN
								SET @userCodes = @userCodes + @tempuser1 + ','
								--向工作流程流转表中插入数据
								INSERT INTO @Instance
								    VALUES(@id,@nodeid,@nodename,@tempuser1,cast(@theorder AS decimal(3,1)),@state,
                                    null,'','',@issendmsg,@isallpass,getdate(),@during,dateadd(hour,@during,getdate()),'0')
								SELECT @instanceid=@@IDENTITY
								IF @@error <> 0 
								BEGIN
									ROLLBACK TRAN
									RETURN
								END
								FETCH NEXT FROM cur_user INTO @tempuser1
							END
							CLOSE cur_user
							DEALLOCATE cur_user
						END
						IF @roletype = 2	--群组
						BEGIN
							DECLARE cur_user1 cursor for
							SELECT distinct usercode FROM wf_roleusers WHERE ltrim(rtrim(str(roleid)))=@operater
							SET @userCodes = ''
							OPEN cur_user1
							FETCH NEXT FROM cur_user1 INTO @tempuser1
							WHILE @@fetch_status = 0
							BEGIN
								SET @userCodes = @userCodes + @tempuser1 + ','
								--向工作流程流转表中插入数据
								INSERT INTO @Instance 
                                VALUES(@id,@nodeid,@nodename,@tempuser1,cast(@theorder AS decimal(3,1)),
                                    @state,null,'','',@issendmsg,@isallpass,getdate(),@during,dateadd(hour,@during,getdate()),'0')
								SELECT @instanceid=@@IDENTITY
								IF @@error <> 0 
								BEGIN
									ROLLBACK TRAN
									RETURN
								END 
								FETCH NEXT FROM cur_user1 INTO @tempuser1
							END
							CLOSE cur_user1
							DEALLOCATE cur_user1
						END
						IF @roletype=3--部门相关
						BEGIN
                            DECLARE @corp varchar(10)
                            SELECT @corp=i_bmdm FROM PT_yhmc WHERE v_yhdm=@userCode
							DECLARE cur_user2 cursor for
							SELECT distinct usercode FROM wf_roleusers 
                            WHERE charindex(','+@corp+',',','+CorpCode+',')>0 
								AND  ltrim(rtrim(str(roleid)))=@operater	
							SET @userCodes = ''
							OPEN cur_user2
							FETCH NEXT FROM cur_user2 INTO @tempuser1
							WHILE @@fetch_status = 0
							BEGIN
								SET @userCodes = @userCodes + @tempuser1 + ','
								--向工作流程流转表中插入数据
								INSERT INTO @Instance 
                                VALUES(@id,@nodeid,@nodename,@tempuser1,cast(@theorder AS decimal(3,1)),
                                    @state,null,'','',@issendmsg,@isallpass,getdate(),@during,dateadd(hour,@during,getdate()),'0')
								SELECT @instanceid=@@IDENTITY
								IF @@error <> 0 
								BEGIN
									ROLLBACK TRAN
									RETURN
								END 
								FETCH NEXT FROM cur_user2 INTO @tempuser1
							END
							CLOSE cur_user2
							DEALLOCATE cur_user2
						END
						
						IF len(@userCodes) = 0
						BEGIN
							SET @i = @i+1
							break
						END
					END
				 END
				END
		    END
			FETCH NEXT FROM cur_node INTO @nodeid,@nodename,@operater,@operatertype,@issendmsg,@isallpass,@during,@theorder
		END
		CLOSE cur_node
		DEALLOCATE cur_node
			--插入到审核节点表;将插入的审核节点重新排序;并向审核人发送消息(将临时表数据插入wf_instance中）
			--SELECT * FROM @instance
			INSERT INTO wf_instance SELECT [ID] ,[NodeID] ,[NodeName],[Operator],[TheOrder],[Sing],[AuditDate]
                ,[AuditResult],[AuditInfo],[IsSendMsg],[IsAllPass],[ArriveTime],[During],[OutOfTime]
                ,[IsInsertedFrontNode],'' FROM @instance order by theOrder asc			 
			DECLARE @itheOrder INT
			DECLARE @oldinNodeID INT
			SET @oldinNodeID =0
			SET @itheOrder = 1
			DECLARE newtheOrder cursor for
			SELECT theOrder,[Operator],IsAllPass,[ID],[NoteID],[NodeID] FROM  wf_instance WHERE ID = @id  order by theOrder asc
			DECLARE @oldtheOrder INT
			DECLARE @MainUser VARCHAR(50)			
			DECLARE @inIsAllPass CHAR(1)
			DECLARE @inID INT
			DECLARE @inNoteID INT
			DECLARE @inNodeID INT			
			OPEN newtheOrder
			--                               theOrder     原审核人   是否全部通过 id     NoteId   NodeId
			FETCH NEXT FROM newtheOrder INTO @oldtheOrder,@MainUser,@inIsAllPass,@inID,@inNoteID,@inNodeID
			WHILE @@fetch_status = 0
			BEGIN				
				IF @itheOrder = 1
				BEGIN	
					IF(@oldinNodeID <> @inNodeID)
					BEGIN				
						UPDATE wf_instance SET theOrder = @itheOrder ,sing=0 WHERE ID = @id AND theOrder = @oldtheOrder
						-- 为审核人发消息
						IF EXISTS (SELECT SecondUser FROM wf_agent WHERE TemplateID=@templateid AND MainUser=@MainUser)
						BEGIN
							SELECT @MainUser = @MainUser +  ','+SecondUser FROM wf_agent WHERE TemplateID=@templateid AND MainUser=@MainUser
							INSERT INTO wf_message VALUES(@inID,@inNoteID,@inNodeID,cast(@itheOrder AS decimal(3,1)),@inIsAllPass,@MainUser)
							IF @@error <> 0 
							BEGIN
								ROLLBACK TRAN
								RETURN
							END
						END
					END
				END
				ELSE
				BEGIN	
					IF(@oldinNodeID <> @inNodeID)
					BEGIN
						UPDATE wf_instance SET theOrder = @itheOrder WHERE ID = @id AND theOrder = @oldtheOrder
					END
				END				
				IF(@oldinNodeID <> @inNodeID)
				BEGIN
					SET @itheOrder = @itheOrder+1
					SET @oldinNodeID = @inNodeID				
				END
				
				FETCH NEXT FROM newtheOrder INTO @oldtheOrder,@MainUser,@IsAllPass,@inID,@inNoteID,@inNodeID
			END			
			CLOSE newtheOrder
			DEALLOCATE newtheOrder

		IF @i <> 0
		BEGIN
			RAISERROR ('流程中有节点审核人为空！', 16, 1)
	   		ROLLBACK TRAN
			RETURN
		END
	
		SET @sql = 'UPDATE '+@tablename+' SET '+@fieldname+' =0 WHERE '+@primaryname+' = @pv'
		--print @sql
		SET @sp = N'@pv uniqueidentifier'
		EXECUTE sp_executesql @sql,@sp, @instancecode
		COMMIT TRAN
	END
	ELSE
	BEGIN
		BEGIN TRAN
		UPDATE wf_instance SET sing =0 
        WHERE theorder = 1 AND sing = -1 
        AND  id = (
            SELECT id FROM wf_instance_main 
            WHERE instancecode = @instancecode AND BusinessCode =
            @businesscode AND BusinessClass = @businessclass
        )
		SET @sql = 'UPDATE '+@tablename+' SET '+@fieldname+' =0 WHERE '+@primaryname+' = @pv'
		SET @sp = N'@pv uniqueidentifier'
		EXECUTE sp_executesql @sql,@sp, @instancecode
		COMMIT TRAN
	END
	SET nocount off
END
GO


--主要是为了解决流程代办人问题   孙新华  （需要更改WF_Agent表）     2012-04-20 16:27
--向WF_Instance_Main和WF_Instance中插入数据
IF (
	(SELECT COUNT(*) FROM information_schema.columns
	WHERE TABLE_NAME = 'WF_Agent' AND COLUMN_NAME = 'TempNodeId') = 0)
ALTER TABLE WF_Agent ADD   TempNodeId  int 
GO

--主要是为了解决后插审核人后又选择下一个审核人错误    孙新华   2011/12/22 13:29

IF OBJECT_ID('p_wf_SelNextOperateflow', 'P') IS NOT NULL
	DROP PROC p_wf_SelNextOperateflow
GO
CREATE procedure [dbo].[p_wf_SelNextOperateflow]
    @noteid int,--3266
    @operates nvarchar(500),--33
    @operatetype int,--3
    @userCode varchar(10),--00000000
    @projectCode varchar(50)
as
begin 
  declare @tempUser varchar(10) -- 多人、群组等存放临时的用户名
  declare @id  int
  declare @userCodes varchar(1000)
  declare @i int
  set @i=0  
  if @operatetype=1 --单人
   begin 
     update WF_Instance set Operator=@operates  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
   end
  if @operatetype=2 --多人
   begin 
     declare cur_user  cursor for 
     select v_yhdm from pt_yhmc where charindex(v_yhdm,@operates) > 0 --相当于把06000009,06000011,拆成两条数据
     open cur_user
     fetch next  from cur_user into @tempUser
     while @@fetch_status = 0 
     begin  
       if @i=0
         begin
           update WF_Instance set Operator=@tempUser  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
           set @i = @i+1
         end 
       else
         begin 
           insert into WF_Instance 
           select top 1 ID,NodeID,NodeName,@tempUser,TheOrder,Sing,AuditDate,AuditResult,AuditInfo,IsSendMsg,IsAllPass,ArriveTime,During,OutOfTime,IsInsertedFrontNode,AuditRemark
           from WF_Instance  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
           select @id=@@IDENTITY
         end
      if @@error <> 0 
		begin
		  rollback tran
		  return
		end
      fetch next from cur_user into @tempUser
    end 
    close cur_user
    deallocate cur_user
  end
if @operatetype=3 --群组
  begin 
    declare cur_user1 cursor for
    select distinct usercode from wf_roleusers where ltrim(rtrim(str(roleid)))=@operates 
    set @userCodes=''
    open  cur_user1
    fetch next from cur_user1 into @tempuser
    while @@fetch_status =0
    begin 
      set @userCodes=@userCodes+@tempuser+','
      if @i=0
        begin 
          update WF_Instance set Operator=@tempuser  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
          set @i=@i+1
        end
      else
        begin
          insert into WF_Instance 
           select top 1 ID,NodeID,NodeName,@tempUser,TheOrder,Sing,AuditDate,AuditResult,AuditInfo,IsSendMsg,IsAllPass,ArriveTime,During,OutOfTime,IsInsertedFrontNode,AuditRemark
           from WF_Instance  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
           select @id=@@IDENTITY
        end
      if @@error <> 0 
		begin
		  rollback tran
		  return
		end 
		fetch next from cur_user1 into @tempuser
	end
	close cur_user1
	deallocate cur_user1
  end
if @operatetype=4 --项目相关
  begin
    declare cur_user2 cursor for
    select distinct usercode from dbo.WF_RoleProject where ltrim(rtrim(str(roleid)))=@operates --取出项目相关的人员
	and charindex(','+cast((select  i_xh from pt_prjinfo where prjguid=@projectCode) as varchar(15)) + 
    ',',','+Projectcodes+',') > 0
    set @userCodes=''
    open cur_user2
    fetch next from cur_user2 into @tempuser
    while @@fetch_status =0
    begin
      set @userCodes=@userCodes+@tempuser+','
    if @i=0
        begin 
          update WF_Instance set Operator=@tempuser  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
          set @i=@i+1
        end
      else
        begin
          insert into WF_Instance 
           select top 1 ID,NodeID,NodeName,@tempUser,TheOrder,Sing,AuditDate,AuditResult,AuditInfo,IsSendMsg,IsAllPass,ArriveTime,During,OutOfTime,IsInsertedFrontNode,AuditRemark
           from WF_Instance  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
           select @id=@@IDENTITY
        end
      if @@error <> 0 
		begin
		  rollback tran
		  return
		end 
		fetch next from cur_user2 into @tempuser 
    end
	close cur_user2
	deallocate cur_user2
  end
if @operatetype=5 --部门相关
  begin
    declare @corp varchar(10)
    select @corp=i_bmdm from PT_yhmc where v_yhdm=@userCode
    declare cur_user3 cursor for
    select distinct usercode from wf_roleusers 
    where charindex(','+@corp+',',','+CorpCode+',')>0 
    and  ltrim(rtrim(str(roleid)))=@operates 
    set @userCodes=''
    open cur_user3
    fetch next from cur_user3 into @tempuser
    while @@fetch_status =0
    begin
      set @userCodes=@userCodes+@tempuser+',' 
    if @i=0
        begin 
          update WF_Instance set Operator=@tempuser  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
          set @i=@i+1
        end
     else
        begin
          insert into WF_Instance 
           select top 1  ID,NodeID,NodeName,@tempUser,TheOrder,Sing,AuditDate,AuditResult,AuditInfo,IsSendMsg,IsAllPass,ArriveTime,During,OutOfTime,IsInsertedFrontNode,AuditRemark
           from WF_Instance  where ID in (select ID from WF_Instance where NoteID=@noteid) and ( TheOrder=(select TheOrder+1.0 from WF_Instance where NoteID=@noteid) or TheOrder=(select TheOrder+0.5 from WF_Instance where NoteID=@noteid))
           select @id=@@IDENTITY
        end
      if @@error <> 0 
		begin
		  rollback tran
		  return
		end 
		fetch next from cur_user3 into @tempuser
    end
	close cur_user3
	deallocate cur_user3
  end
  if(@operatetype!= 1)
  begin 
    if len(@userCodes) = 0
	  begin
	   raiserror ('流程中有节点审核人为空！', 16, 1)
       return
	  end
  end 
end
GO

-- 流程状态处理（包含重报）  孙新华 2012-1-29  17:19
ALTER              procedure [dbo].[p_wf_processflow] 
					@id int ,		        --工作流程流转ID  4798
					@isallpass varchar(1),          --是否全部通过才通过 ""
					@userCode varchar(10),          --用户代码  04100002
					@AuditResult int,               --审核结果  -2 
					@AuditInfo varchar(2000)         --审核意见  ""
as
begin
	declare @tablename varchar(50)
	declare @fieldname varchar(50)
	declare @primaryname varchar(50)
	declare @instancecode uniqueidentifier
	declare @theorder decimal(3,1)
	declare @neworder decimal(3,1)
	declare @templateid int
	declare @i int 
	declare @instanceid int 
	select @tablename = LinkTable,@fieldname=StateField,@primaryname=PrimaryField from WF_BusinessCode 
	where BusinessCode=(select a.BusinessCode from wf_instance_main a,wf_instance b where a.ID = b.ID and b.NoteID=@id)
	
	--取审核节点的序号
	select @theorder = theorder from wf_instance where NoteID=@id
	--取模板ID和具体业务记录Guid
	select @templateid = templateid,@instancecode = InstanceCode from wf_instance_main where id = (select id from WF_Instance where noteid=@id)
    declare @sql nvarchar(500)
    declare @sp nvarchar(500)
	if @AuditResult = 1
	begin
		update wf_instance
		 set Operator = @userCode,
			Sing = 1,
			AuditResult = @AuditResult,
			AuditInfo = @AuditInfo,
			AuditDate = getdate()
		where NoteID=@id
	    set @i = 0
		if @isallpass = '1'  --该节点设置为全部通过才通过时
		begin
			--该节点所有审核人都审核过
			if not exists(select 1 from wf_instance where theOrder = @theOrder and sing = 0 and id=(select id from WF_Instance where noteid=@id) )
			begin
			    --不存在下一个审核人
				if not exists(select 1 from wf_instance where theorder= floor(@theorder+1) and id=(select id from WF_Instance where noteid=@id))
				begin
					--该节点下不存在审核不通过的审核人
				    if not exists ( select 1 from WF_Instance where TheOrder=@theOrder and ID=(select id from WF_Instance where noteid=@id) AND AuditResult <> '1')
				    begin
					    SET @sql = 'update '+@tablename+' set '+@fieldname+' =1 where '+@primaryname+' = @pv'
					    set @sp = N'@pv uniqueidentifier'
					    execute sp_executesql @sql ,@sp, @instancecode
					    set @i = @i + 1
					end
                    --该节点下存在审核不通过的审核人
					else
                    begin
                      --该节点下存在驳回的情况,最后的流程状态为驳回
                      if exists ( select 1 from WF_Instance where TheOrder=@theOrder and ID=(select id from WF_Instance where noteid=@id) AND AuditResult ='-2')
                      begin 
					    SET @sql = 'update '+@tablename+' set '+@fieldname+' =-2 where '+@primaryname+' = @pv'
				        set @sp = N'@pv uniqueidentifier'
				        execute sp_executesql @sql,@sp, @instancecode
                      end
                      --该节点下不存在驳回的情况,最后的流程状态为重报
                      else
                      begin 
					    SET @sql = 'update '+@tablename+' set '+@fieldname+' =-3 where '+@primaryname+' = @pv'
				        set @sp = N'@pv uniqueidentifier'
				        execute sp_executesql @sql,@sp, @instancecode
                      end
                   end
				end
				--存在下一个审核人
                else
				begin
				    --该节点下不存在审核不通过的审核人,sing=0表示成功到达下一个审核人
				    if not exists (select  1 from WF_Instance where TheOrder=@theOrder  and ID=(select id from WF_Instance where noteid=@id) AND AuditResult <> '1')
				    begin
					    update wf_instance
					    set Sing = 0,
						ArriveTime = getdate(),
						OutOfTime = dateadd(hour,During,getdate())
					    where theorder= floor(@theorder+1) and id=(select id from WF_Instance where noteid=@id)
					    --为发送消息neworder置值
					    set @neworder =  floor(@theorder+1)
					    SET @sql = 'update '+@tablename+' set '+@fieldname+' =0 where '+@primaryname+' = @pv'
					    set @sp = N'@pv uniqueidentifier'
					    execute sp_executesql @sql,@sp, @instancecode
				   end
                   --该节点下存在审核不通过的审核人
				   else
				   begin
				       update wf_instance
					   set Sing = -1
					   where theorder= floor(@theorder+1) and id=(select id from WF_Instance where noteid=@id)
                       --该节点下存在驳回的情况,最后的流程状态为驳回
                       if exists ( select 1 from WF_Instance where TheOrder=@theOrder and ID=(select id from WF_Instance where noteid=@id) AND AuditResult ='-2')
                       begin
					     SET @sql = 'update '+@tablename+' set '+@fieldname+' =-2 where '+@primaryname+' = @pv'
				         set @sp = N'@pv uniqueidentifier'
				         execute sp_executesql @sql,@sp, @instancecode
                       end
                       --该节点下不存在驳回的情况,最后的流程状态为重报
                       else
                       begin 
					     SET @sql = 'update '+@tablename+' set '+@fieldname+' =-3 where '+@primaryname+' = @pv'
				         set @sp = N'@pv uniqueidentifier'
				         execute sp_executesql @sql,@sp, @instancecode
                       end
				   end     
				--删除当前审核人的消息信息
				delete from wf_message where InstanceID=@id and theorder=@theorder and charindex(@userCode,MsgRecievers) > 0
			    end
            end
			else  --存在未审核的审核人
			begin
				return
			end
		end
		else  -- 一个通过即通过时
		begin
            --该节点下的其他人处理为（sing=2)未处理已通过
			update wf_instance set Sing = 2 where theOrder = @theOrder and sing = 0 and id=(select id from WF_Instance where noteid=@id)
            --不存在下一个审核人
			if not exists(select 1 from wf_instance where theorder= floor(@theorder+1) and id=(select id from WF_Instance where noteid=@id))
			begin
				SET @sql = 'update '+@tablename+' set '+@fieldname+' =1 where '+@primaryname+' = @pv'
				set @sp = N'@pv uniqueidentifier'
				execute sp_executesql @sql ,@sp, @instancecode
				set @i = @i + 1
			end
            --存在下一个审核人
			else
			begin
				update wf_instance
				set Sing = 0,
					ArriveTime = getdate(),
					OutOfTime = dateadd(hour,During,getdate())
				where theorder= floor(@theorder+1) and id=(select id from WF_Instance where noteid=@id)
				--为发送消息neworder置值
				set @neworder = @theorder+1
				SET @sql = 'update '+@tablename+' set '+@fieldname+' =0 where '+@primaryname+' = @pv'
				set @sp = N'@pv uniqueidentifier'
				execute sp_executesql @sql,@sp, @instancecode
			end
			--删除当前节点全部审核人的消息信息
			delete from wf_message where theorder=@theorder and InstanceMainID=(select id from WF_Instance where noteid=@id)
		end
	end
	else  --驳回或者重报时
	begin
--		declare @isInsertFrontNode varchar(2)
--		select @isInsertFrontNode = IsInsertedFrontNode from wf_instance where theOrder = floor(@theOrder+1) and id=(select id from WF_Instance where noteid=@id)
--		if @isInsertFrontNode = '1' --当前节点为前插节点时，不允许驳回操作
--		begin
--			return 
--		end 
--		else
--		begin
          update wf_instance
			 set Operator = @userCode,
				Sing = 1,
				AuditResult = @AuditResult,
				AuditInfo = @AuditInfo,
				AuditDate = getdate()
			where noteid = @id

              --该节点所有审核人都审核过
			  if not exists(select 1 from wf_instance where theOrder = @theOrder and sing = 0 and id=(select id from WF_Instance where noteid=@id) )
			  begin
                --该节点下存在驳回的情况,最后的流程状态为驳回
                if exists ( select 1 from WF_Instance where TheOrder=@theOrder and ID=(select id from WF_Instance where noteid=@id) AND AuditResult ='-2')
                begin
                  SET @sql = 'update '+@tablename+' set '+@fieldname+' =-2 where '+@primaryname+' = @pv'
				  set @sp = N'@pv uniqueidentifier'
				  execute sp_executesql @sql,@sp, @instancecode
                end
                --该节点下不存在驳回的情况,最后的流程状态为重报
                else
                begin 
				  SET @sql = 'update '+@tablename+' set '+@fieldname+' =-3 where '+@primaryname+' = @pv'
				  set @sp = N'@pv uniqueidentifier'
				  execute sp_executesql @sql,@sp, @instancecode
                end
              end
              else --该节点有人没有审核过（更改单据的状态为审核中）
              begin
                SET @sql = 'update '+@tablename+' set '+@fieldname+' =0 where '+@primaryname+' = @pv'
				set @sp = N'@pv uniqueidentifier'
				execute sp_executesql @sql,@sp, @instancecode 
              end
--         end     
    end
	if @i = 0 
	begin
		--为下一审核节点发送消息
		declare @operater varchar(10)
		--declare @id int
		declare @nodeid int
		declare cur_node cursor for
		select id,NoteID,nodeid,Operator,isallpass from wf_instance where theorder = @neworder and id = (select id from WF_Instance where noteid=@id)
		open cur_node
		fetch next from cur_node into @id,@instanceid,@nodeid,@operater,@isallpass
		while @@fetch_status = 0
		begin
			if exists (select SecondUser from wf_agent where TemplateID=@templateid and MainUser=@operater)
				select @operater = @operater +  ','+SecondUser from wf_agent where TemplateID=@templateid and MainUser=@operater
			insert into wf_message values(@id,@instanceid,@nodeid,@neworder,@isallpass,@operater)
			fetch next from cur_node into @id,@instanceid,@nodeid,@operater,@isallpass
		end
		close cur_node
		deallocate cur_node
	end					
end
GO

--主要为了解决流程状态显示（数据有错误）的问题  孙新华   2012-3-16  15:34
ALTER                 procedure [dbo].[p_wf_queryflowchart]
				@instancecode uniqueidentifier, --具体业务Guid  06ad2794-5569-41b2-bdc5-c83fedbcbe04
				@BusinessCode varchar(10),--084
				@BusinessClass varchar(10)--001
as
begin
	set nocount on
	declare @maxorder decimal(3,1)
	declare @sqlstring varchar(max)
	declare @theorder decimal(3,1)
	declare @operater varchar(10)
	declare @image	varchar(4)
	declare @sing	int
	declare @count int 
	declare @row int
	declare @i int
	declare @xh int 
    --加一个@id（取得最新重报的ID号），主要是为了解决重报多次，流程状态显示错误问题
    declare @id  int 
	--定义临时表变量
	declare @str table
	(
		rowid int ,
		sqlstring varchar(max)
	)
    select @id=ID from wf_instance_main where  BusinessCode = @BusinessCode and BusinessClass = @BusinessClass and InstanceCode=@instancecode and StartTime=(select  max(StartTime)  from wf_instance_main where BusinessCode = @BusinessCode and BusinessClass = @BusinessClass and InstanceCode=@instancecode)
	--取工作流转表中的最多审核人作为行
	select @row = max(sl) from (select count(*) as sl from wf_instance where id=@id  group by theorder) a
	set @i = 1
	while @i <= @row
	begin
		insert into @str values(@i,'')
		set @i = @i + 1
	end
	if exists (select 1 from wf_instance_main where ID=@id)
	begin
		select @operater=b.v_xm from wf_instance_main a,pt_yhmc b where a.Organiger = b.v_yhdm and  a.ID=@id
		update @str set sqlstring = ''''+'00;'+@operater+''''+' as Column1' where rowid= 1 --00;陈国峰  发起人
		set @i = 2
		while @i <=@row
		begin 
			update @str set sqlstring = 'null as Column1' where rowid = @i
			set @i=@i+1
		end
	end
	--取工作流转表的最大序号
	select @maxorder = max(theorder) from wf_instance where ID=@id --最大版本号
	set @xh = 2
	set @theorder = 0.5
	while @theorder <= @maxorder
	begin
		if exists(select 1 from wf_instance where TheOrder=@theorder and id=@id)
		begin
			select @count = count(*) from wf_instance where TheOrder=@theorder and id=@id
			if @count = 1 
			begin
				select @operater=ISNULL(b.v_xm,''),@sing=a.sing from wf_instance a left join pt_yhmc b on b.v_yhdm=a.operator  where  TheOrder=@theorder and a.id =@id 
				update @str set sqlstring = sqlstring + ','+'7 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2)) where rowid = @count	
				set @i = @count
			end 	
			else
			begin
				set @i = 0 
				declare cur_node cursor for
				select ISNULL (b.v_xm,''),a.sing from wf_instance a  left join pt_yhmc b on b.v_yhdm=a.operator  where  TheOrder=@theorder and a.id = @id
				open cur_node
				fetch next from cur_node into @operater,@sing
				while @@fetch_status = 0 
				begin
					set @i = @i + 1
					if @i = 1
					begin
						if @theorder = @maxorder
							update @str set sqlstring = sqlstring + ','+'13 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2)) where rowid = @i
						else
							update @str set sqlstring = sqlstring + ','+'13 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2))+','+'13 as Column'+cast(@xh+2 as varchar(2)) where rowid = @i		
					end
					else
					begin	
						if @i = @count
						begin
							if @theorder = @maxorder
								update @str set sqlstring = sqlstring + ','+'10 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2)) where rowid = @i
							else
								update @str set sqlstring = sqlstring + ','+'10 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2))+','+'12 as Column'+cast(@xh+2 as varchar(2)) where rowid = @i
							end
						else
						begin
							if @theorder = @maxorder
								update @str set sqlstring = sqlstring + ','+'9 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2)) where rowid = @i
							else
								update @str set sqlstring = sqlstring + ','+'9 as Column'+cast(@xh as varchar(2))+','+ ''''+cast(@sing as varchar(2))+';'+@operater+ ''''+' as Column'+cast(@xh+1 as varchar(2))+','+'11 as Column'+cast(@xh+2 as varchar(2)) where rowid = @i		
						end 	
					end
					fetch next from cur_node into @operater,@sing 
				end
				close cur_node
				deallocate cur_node
			end
		end
		set @sqlstring = ''
		set @i = @i +1
		while @i <=@row
		begin 
			if @count = 1
			begin
				update @str set sqlstring = sqlstring + ','+'null as Column'+cast(@xh as varchar(2))+','+ 'null as Column'+cast(@xh+1 as varchar(2)) where rowid = @i
			end
			else
			begin		
				if @theorder = @maxorder
					update @str set sqlstring = sqlstring + ','+'null as Column'+cast(@xh as varchar(2))+','+ 'null as Column'+cast(@xh+1 as varchar(2)) where rowid = @i
				else
					update @str set sqlstring = sqlstring + ','+'null as Column'+cast(@xh as varchar(2))+','+ 'null as Column'+cast(@xh+1 as varchar(2))+','+'null as Column'+cast(@xh+2 as varchar(2)) where rowid = @i
			end
			set @i=@i+1
		end
		if @theorder < @maxorder and @count > 1
			set @xh=@xh+3
		else
			set @xh=@xh+2
		set @theorder = @theorder + 0.5
	end
	set @i =1
	declare @sql varchar(max)
	set @sql = ''

	while @i <= @row
	begin
		select @sqlstring = sqlstring from @str where rowid = @i
		set @sql = @sql + 'select ' +cast(@i as varchar(2))+' as id,' +@sqlstring
		if @i <@row
			set @sql = @sql + ' union '

		set @i = @i +1
	end
	if @sql = ''
		begin
			exec (@sql) 
			
		end
	else
		begin
			exec (@sql+' order by id asc ')  
			
		end          

	
	set nocount off
end
GO
    
            
  














 

