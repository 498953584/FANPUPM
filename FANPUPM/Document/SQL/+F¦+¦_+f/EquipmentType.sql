--修改带有外键的主键的类型 先删除外键,再删除主键,修改类型,依次追加主键,外键
alter table Equ_Equipment drop constraint FK_TypeId;
alter table Equ_EquipmentType drop constraint FK_ParentId;
alter table Equ_EquipmentType drop constraint PK_EquipmentType;
alter table Equ_EquipmentType alter column ParentId nvarchar(64);
alter table Equ_EquipmentType alter column Id nvarchar(64) NOT NULL;
alter table Equ_Equipment alter column TypeId nvarchar(64) NOT NULL;
alter table Equ_EquipmentType add constraint PK_EquipmentType primary key (Id);
alter table Equ_EquipmentType add constraint FK_ParentId FOREIGN KEY(ParentId) REFERENCES Equ_EquipmentType(Id);
alter table Equ_Equipment add constraint FK_TypeId FOREIGN KEY(TypeId) REFERENCES Equ_EquipmentType(Id);
--修改列的类型
alter table Equ_EquipmentType alter column Name nvarchar(300) NOT NULL;


