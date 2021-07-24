--Begin
alter table Equ_Equipment drop constraint PK_Equ_Equipment;
alter table Equ_Equipment alter column Id nvarchar(64) NOT NULL;
alter table Equ_Equipment add constraint PK_Equ_Equipment primary key (Id);
exec sp_rename 'Equ_Equipment.EquipmentCode','EquCode','column';
alter table Equ_Equipment alter column EquCode nvarchar(64) NOT NULL;
alter table Equ_Equipment drop column Accuracy;
alter table Equ_Equipment alter column FactoryNumber nvarchar(100);
alter table Equ_Equipment add EquProperty int default 0 NOT NULL,ReceiptNo nvarchar(50),
							  ShipWidth decimal(18,3),ShipLong decimal(18,3),ShipCapaticy decimal(18,3),
                              OtherShipInfo nvarchar(500),MiddleInspectDate datetime,YearInspectDate datetime,
                              OtherCredentials nvarchar(500),InputUser varchar(8) default '00000000' not null,
                              InputDate datetime default getdate() not null;
alter table Equ_Equipment add EquName nvarchar(100);
go
--End
--更新设备名称，
--Begin
select row_number() over (order by Id) as Num, ResourceId into #mytempTable from Equ_Equipment
go
declare @no int
declare @maxNo int
declare @resourceCode nvarchar(500)
declare @equName nvarchar(100)
set @no = 1
select @maxNo=IsNull(max(Num), 0) from #mytempTable
begin
 while @no <= @maxNo
  begin
    select @resourceCode=ResourceId from #mytempTable where Num=@no
    select @equName=ResourceName from Res_Resource where ResourceId=@resourceCode
    update Equ_Equipment set EquName=@equName where ResourceId=@resourceCode
    set @no=@no + 1
  end
end
drop table #mytempTable;
--End
alter table Equ_Equipment alter column EquName nvarchar(100) not null;
alter table Equ_Equipment drop column ResourceId;
go







