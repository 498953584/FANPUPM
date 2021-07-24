----------------------------sql涵数 创建人-李真 创建时间-2010-7-30--------------------------------
--部门相关涵数 主要作用:--传进部门ID根据该部门ID
				        --查出该部门下的所有子部
				        --门包括该部门返回该部门和所有子部门的ID
--查询方法:select i_bmdm from f_cid('42')
CREATE FUNCTION f_cid(@id int)
RETURNS @t TABLE(i_bmdm int,i_sjdm int,lev int)
as
begin
     declare @lev int
     set @lev=1
     --当@ID = 0 时，为顶层部门

     --当子节点获取父节点时，将where i_bmdm = @id改为where i_sjdm = @id即可

      if @id <> 0
       insert @t select i_bmdm,i_sjdm,@lev from  dbo.PT_d_bm where i_bmdm = @id
     else
       insert @t select i_bmdm,i_sjdm,@lev from  dbo.PT_d_bm where i_sjdm = 0
     while(@@rowcount>0)
     begin
          set @lev=@lev+1
          insert @t select a.i_bmdm,a.i_sjdm,@lev from dbo.PT_d_bm a,@t b
          where a.i_sjdm = b.i_bmdm and b.lev=@lev-1
     end
     return 
   end