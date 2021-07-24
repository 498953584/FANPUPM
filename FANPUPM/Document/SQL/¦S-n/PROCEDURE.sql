

-- =============================================
-- Author:		<Author,,李真>
-- Create date: <Create Date,2010-8-19,>
-- Description:判断收款合同表是否有子表用户删除
-- 收标歀合同将提醒 如果返回2说明有数据不能删，否则可删
-- =============================================
IF OBJECT_ID ( 'p_contract_existchild', 'P' ) IS NOT NULL 
    Drop PROCEDURE p_contract_existchild;
GO
CREATE PROCEDURE p_contract_existchild
	-- Add the parameters for the stored procedure here
	@contractid nvarchar(64),
    @i_Ret int=0  output
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(exists(select * from Con_Incomet_Contract where FCode=@contractid))
		 set @i_Ret=2 --收入合同补充协议
	if exists(select * from Con_Incomet_Modify where ContractID=@contractid)                 -----判断是否有合同结算子表
        set @i_Ret=2 --收入合同变更
	if exists(select * from Con_Incomet_Balance where ContractID=@contractid)                 -----判断是否有合同结算子表
        set @i_Ret=2 --收入合同结算
	if exists(select * from Con_Incomet_Payment where ContractID=@contractid)                 -----判断是否有合同结算子表
        set @i_Ret=2 --收入合同收款
	if exists(select * from Con_ContractPayend where ContractID=@contractid)                 -----判断是否有合同结算子表
        set @i_Ret=2 --合同交底
    if exists(select * from Con_PayendFeedback where ContractID=@contractid)
		set @i_Ret=2 --交底反馈
END
GO
