/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2005                    */
/* Created on:     2012/10/20 12:55:30                          */
/*==============================================================*/


if exists (select 1
          from sysobjects
          where id = object_id('dbo.AssWorkOrderUpadte')
          and type = 'TR')
   drop trigger dbo.AssWorkOrderUpadte
go

if exists (select 1
          from sysobjects
          where id = object_id('dbo.T_User')
          and type = 'TR')
   drop trigger dbo.T_User
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.TestFlow') and o.name = 'FK_TestFlow_workOrderList')
alter table dbo.TestFlow
   drop constraint FK_TestFlow_workOrderList
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('dbo.workOrderList') and o.name = 'FK_workOrderList_projectlist')
alter table dbo.workOrderList
   drop constraint FK_workOrderList_projectlist
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.IMEI_MII')
            and   type = 'U')
   drop table dbo.IMEI_MII
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.Mobinfo')
            and   name  = 'idx_mob_sn'
            and   indid > 0
            and   indid < 255)
   drop index dbo.Mobinfo.idx_mob_sn
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.Mobinfo')
            and   name  = 'idx_mob_mii'
            and   indid > 0
            and   indid < 255)
   drop index dbo.Mobinfo.idx_mob_mii
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.Mobinfo')
            and   name  = 'idx_mob_imei'
            and   indid > 0
            and   indid < 255)
   drop index dbo.Mobinfo.idx_mob_imei
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Mobinfo')
            and   type = 'U')
   drop table dbo.Mobinfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.MobinfoEditData')
            and   type = 'U')
   drop table dbo.MobinfoEditData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.PreMobinfo')
            and   type = 'U')
   drop table dbo.PreMobinfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TestData')
            and   type = 'U')
   drop table dbo.TestData
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TestFlow')
            and   type = 'U')
   drop table dbo.TestFlow
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.TestInfo')
            and   name  = 'idx_testinfo_sn'
            and   indid > 0
            and   indid < 255)
   drop index dbo.TestInfo.idx_testinfo_sn
go

if exists (select 1
            from  sysindexes
           where  id    = object_id('dbo.TestInfo')
            and   name  = 'idx_testinfo_name'
            and   indid > 0
            and   indid < 255)
   drop index dbo.TestInfo.idx_testinfo_name
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.TestInfo')
            and   type = 'U')
   drop table dbo.TestInfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.Userinfo')
            and   type = 'U')
   drop table dbo.Userinfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.c3p0TestTable')
            and   type = 'U')
   drop table dbo.c3p0TestTable
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.projectlist')
            and   type = 'U')
   drop table dbo.projectlist
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.repairinfo')
            and   type = 'U')
   drop table dbo.repairinfo
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.sysdiagrams')
            and   type = 'U')
   drop table dbo.sysdiagrams
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.usermanagment')
            and   type = 'U')
   drop table dbo.usermanagment
go

if exists (select 1
            from  sysobjects
           where  id = object_id('dbo.workOrderList')
            and   type = 'U')
   drop table dbo.workOrderList
go

execute sp_revokedbaccess dbo
go

/*==============================================================*/
/* User: dbo                                                    */
/*==============================================================*/
execute sp_grantdbaccess dbo
go

/*==============================================================*/
/* Table: IMEI_MII                                              */
/*==============================================================*/
create table dbo.IMEI_MII (
   IMEI                 varchar(50)          collate Chinese_PRC_CI_AS null,
   MII                  varchar(50)          collate Chinese_PRC_CI_AS null,
   Used                 bit                  null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Mobinfo                                               */
/*==============================================================*/
create table dbo.Mobinfo (
   id                   bigint               identity(1, 1) not for replication,
   sn                   varchar(25)          collate Chinese_PRC_CI_AS null,
   IMEI                 varchar(25)          collate Chinese_PRC_CI_AS null,
   IMEI2                varchar(25)          collate Chinese_PRC_CI_AS null,
   IMEI3                varchar(20)          collate Chinese_PRC_CI_AS null,
   IMEI4                varchar(20)          collate Chinese_PRC_CI_AS null,
   MII                  varchar(25)          collate Chinese_PRC_CI_AS null,
   BTMac                varchar(20)          collate Chinese_PRC_CI_AS null,
   WiFiMAC              varchar(20)          collate Chinese_PRC_CI_AS null,
   GPSCode              varchar(20)          collate Chinese_PRC_CI_AS null,
   BoxNumber            varchar(20)          collate Chinese_PRC_CI_AS null,
   PalletNumber         varchar(20)          collate Chinese_PRC_CI_AS null,
   PcbBoxNumber         varchar(20)          collate Chinese_PRC_CI_AS null,
   WorkOrder            varchar(50)          collate Chinese_PRC_CI_AS null,
   AssWorkOrder         varchar(30)          collate Chinese_PRC_CI_AS null,
   TestStation          varchar(20)          collate Chinese_PRC_CI_AS null,
   TestResult           bit                  null,
   TestTime             datetime             null,
   ProductStatus        int                  null,
   PC                   varchar(50)          collate Chinese_PRC_CI_AS null,
   workid               int                  not null,
   time1                datetime             null constraint DF_Mobinfo_time1 default getdate(),
   time2                datetime             null,
   time3                datetime             null,
   mark1                varchar(500)         collate Chinese_PRC_CI_AS null,
   mark2                varchar(500)         collate Chinese_PRC_CI_AS null,
   mark3                varchar(500)         collate Chinese_PRC_CI_AS null,
   mark4                varchar(50)          collate Chinese_PRC_CI_AS null,
   mark5                char(10)             collate Chinese_PRC_CI_AS null,
   MEID                 varchar(50)          collate Chinese_PRC_CI_AS null,
   InsertStatus         int                  null,
   constraint PK_Mobinfo primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_mob_imei                                          */
/*==============================================================*/
create index idx_mob_imei on dbo.Mobinfo (
IMEI ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_mob_mii                                           */
/*==============================================================*/
create index idx_mob_mii on dbo.Mobinfo (
MII ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_mob_sn                                            */
/*==============================================================*/
create index idx_mob_sn on dbo.Mobinfo (
sn ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: MobinfoEditData                                       */
/*==============================================================*/
create table dbo.MobinfoEditData (
   id                   bigint               not null,
   sn                   varchar(25)          collate Chinese_PRC_CI_AS null,
   IMEI                 varchar(25)          collate Chinese_PRC_CI_AS null,
   IMEI2                varchar(25)          collate Chinese_PRC_CI_AS null,
   IMEI3                varchar(20)          collate Chinese_PRC_CI_AS null,
   IMEI4                varchar(20)          collate Chinese_PRC_CI_AS null,
   MII                  varchar(25)          collate Chinese_PRC_CI_AS null,
   BTMac                varchar(20)          collate Chinese_PRC_CI_AS null,
   WiFiMAC              varchar(20)          collate Chinese_PRC_CI_AS null,
   GPSCode              varchar(20)          collate Chinese_PRC_CI_AS null,
   BoxNumber            varchar(20)          collate Chinese_PRC_CI_AS null,
   PalletNumber         varchar(20)          collate Chinese_PRC_CI_AS null,
   PcbBoxNumber         varchar(20)          collate Chinese_PRC_CI_AS null,
   WorkOrder            varchar(50)          collate Chinese_PRC_CI_AS null,
   AssWorkOrder         varchar(30)          collate Chinese_PRC_CI_AS null,
   TestStation          varchar(20)          collate Chinese_PRC_CI_AS null,
   TestResult           bit                  null,
   TestTime             datetime             null,
   ProductStatus        int                  null,
   PC                   varchar(50)          collate Chinese_PRC_CI_AS null,
   workid               int                  not null,
   time1                datetime             null constraint DF_MobinfoEditData_time1 default getdate(),
   time2                datetime             null,
   time3                datetime             null,
   mark1                varchar(500)         collate Chinese_PRC_CI_AS null,
   mark2                varchar(500)         collate Chinese_PRC_CI_AS null,
   mark3                varchar(500)         collate Chinese_PRC_CI_AS null,
   mark4                varchar(50)          collate Chinese_PRC_CI_AS null,
   mark5                char(10)             collate Chinese_PRC_CI_AS null,
   MEID                 varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: PreMobinfo                                            */
/*==============================================================*/
create table dbo.PreMobinfo (
   id                   bigint               not null,
   sn                   varchar(200)         collate Chinese_PRC_CI_AS null,
   IMEI                 varchar(200)         collate Chinese_PRC_CI_AS null,
   IMEI2                varchar(200)         collate Chinese_PRC_CI_AS null,
   IMEI3                varchar(200)         collate Chinese_PRC_CI_AS null,
   IMEI4                varchar(200)         collate Chinese_PRC_CI_AS null,
   MII                  varchar(500)         collate Chinese_PRC_CI_AS null,
   BTMac                varchar(500)         collate Chinese_PRC_CI_AS null,
   WiFiMAC              varchar(500)         collate Chinese_PRC_CI_AS null,
   GPSCode              varchar(500)         collate Chinese_PRC_CI_AS null,
   BoxNumber            varchar(500)         collate Chinese_PRC_CI_AS null,
   PalletNumber         varchar(500)         collate Chinese_PRC_CI_AS null,
   PcbBoxNumber         varchar(500)         collate Chinese_PRC_CI_AS null,
   WorkOrder            varchar(50)          collate Chinese_PRC_CI_AS null,
   AssWorkOrder         varchar(50)          collate Chinese_PRC_CI_AS null,
   TestStation          varchar(500)         collate Chinese_PRC_CI_AS null,
   TestResult           bit                  null,
   TestTime             datetime             null,
   ProductStatus        int                  null,
   PC                   varchar(500)         collate Chinese_PRC_CI_AS null,
   workid               int                  not null,
   time1                datetime             null,
   time2                datetime             null,
   time3                datetime             null,
   mark1                char(500)            collate Chinese_PRC_CI_AS null,
   mark2                char(500)            collate Chinese_PRC_CI_AS null,
   mark3                char(500)            collate Chinese_PRC_CI_AS null,
   mark4                char(10)             collate Chinese_PRC_CI_AS null,
   mark5                char(10)             collate Chinese_PRC_CI_AS null,
   MEID                 varchar(50)          collate Chinese_PRC_CI_AS null,
   NewWorkOrder         varchar(50)          collate Chinese_PRC_CI_AS null,
   NewAssWorkOrder      varchar(50)          collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: TestData                                              */
/*==============================================================*/
create table dbo.TestData (
   id                   bigint               identity(1, 1) not for replication,
   sn                   varchar(500)         collate Chinese_PRC_CI_AS null,
   ItemName             varchar(100)         collate Chinese_PRC_CI_AS null,
   Stationname          varchar(100)         collate Chinese_PRC_CI_AS null,
   TestResult           bit                  null,
   TestData             float                null,
   Testcount            int                  null,
   TestTime             datetime             null,
   PC                   varchar(500)         collate Chinese_PRC_CI_AS null,
   Band                 int                  null,
   PCL                  int                  null,
   TCH                  int                  null,
   workid               int                  null,
   mobid                bigint               null,
   time1                datetime             null constraint DF_TestInfo_time1 default getdate(),
   mark1                varchar(50)          collate Chinese_PRC_CI_AS null,
   mark2                varchar(50)          collate Chinese_PRC_CI_AS null,
   mark3                varchar(50)          collate Chinese_PRC_CI_AS null,
   mark4                varchar(50)          collate Chinese_PRC_CI_AS null,
   mark5                varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK_TestInfo primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: TestFlow                                              */
/*==============================================================*/
create table dbo.TestFlow (
   id                   int                  identity(1, 1) not for replication,
   StationName          varchar(50)          collate Chinese_PRC_CI_AS null,
   StationIndex         int                  null,
   FlowCheck            bit                  null,
   workid               int                  null,
   constraint PK_TestFlow primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: TestInfo                                              */
/*==============================================================*/
create table dbo.TestInfo (
   id                   bigint               identity(1, 1) not for replication,
   sn                   varchar(25)          collate Chinese_PRC_CI_AS null,
   Stationname          varchar(25)          collate Chinese_PRC_CI_AS null,
   TestResult           bit                  null,
   Testcount            int                  null,
   TestTime             datetime             null,
   StationID            int                  null,
   mobid                bigint               null,
   PC                   varchar(50)          collate Chinese_PRC_CI_AS null,
   constraint PK_TestInfoTemp2 primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_testinfo_name                                     */
/*==============================================================*/
create index idx_testinfo_name on dbo.TestInfo (
Stationname ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Index: idx_testinfo_sn                                       */
/*==============================================================*/
create index idx_testinfo_sn on dbo.TestInfo (
sn ASC
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: Userinfo                                              */
/*==============================================================*/
create table dbo.Userinfo (
   id                   int                  identity(1, 1) not for replication,
   username             varchar(50)          collate Chinese_PRC_CI_AS null,
   jobnumber            varchar(50)          collate Chinese_PRC_CI_AS null,
   passwrod             varchar(50)          collate Chinese_PRC_CI_AS null,
   depart               varchar(50)          collate Chinese_PRC_CI_AS null,
   time1                datetime             null constraint DF_User_time1 default getdate(),
   state1               bit                  null constraint DF_Userinfo_state1 default (1),
   mark                 varchar(500)         collate Chinese_PRC_CI_AS null,
   constraint PK_User primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: c3p0TestTable                                         */
/*==============================================================*/
create table dbo.c3p0TestTable (
   a                    char(1)              collate Chinese_PRC_CI_AS null
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: projectlist                                           */
/*==============================================================*/
create table dbo.projectlist (
   id                   int                  identity(1, 1) not for replication,
   projectname          varchar(50)          collate Chinese_PRC_CI_AS null,
   solution             varchar(50)          collate Chinese_PRC_CI_AS null,
   mark                 varchar(100)         collate Chinese_PRC_CI_AS null,
   time1                datetime             null constraint DF_projectlist_time1 default getdate(),
   SNLength             int                  null,
   IMEIPre8             varchar(50)          collate Chinese_PRC_CI_AS null,
   IMEILowerLimit       bigint               null,
   IMEIUpperLimit       bigint               null,
   MIIPre6              varchar(50)          collate Chinese_PRC_CI_AS null,
   MIILowerLimit        bigint               null,
   MIIUpperLimit        bigint               null,
   MacPre6              varchar(50)          collate Chinese_PRC_CI_AS null,
   MacLowerLimit        bigint               null,
   MacUpperLimit        bigint               null,
   MacCurrent           bigint               null,
   MEIDPre8             varchar(50)          collate Chinese_PRC_CI_AS null,
   MEIDLowerLimit       bigint               null,
   MEIDUpperLimit       bigint               null,
   BoxWeight            float                null,
   constraint PK_projectlist primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: repairinfo                                            */
/*==============================================================*/
create table dbo.repairinfo (
   id                   int                  identity(1, 1) not for replication,
   SN                   varchar(100)         collate Chinese_PRC_CI_AS null,
   Description          varchar(200)         collate Chinese_PRC_CI_AS null,
   Reason               varchar(200)         collate Chinese_PRC_CI_AS null,
   PartNumber           varchar(50)          collate Chinese_PRC_CI_AS null,
   Method               varchar(200)         collate Chinese_PRC_CI_AS null,
   Repairer             varchar(50)          collate Chinese_PRC_CI_AS null,
   TestTime             varchar(50)          collate Chinese_PRC_CI_AS null,
   PC                   varchar(100)         collate Chinese_PRC_CI_AS null,
   time1                datetime             null constraint DF_repairinfo_time1 default getdate(),
   worklistid           int                  null,
   WorkOrder            varchar(50)          collate Chinese_PRC_CI_AS null,
   testname             varchar(50)          collate Chinese_PRC_CI_AS null,
   Type                 int                  null,
   RepairTime           datetime             null,
   projectname          varchar(25)          collate Chinese_PRC_CI_AS null,
   constraint PK_repairinfo primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: sysdiagrams                                           */
/*==============================================================*/
create table dbo.sysdiagrams (
   name                 sysname              collate Chinese_PRC_CI_AS not null,
   principal_id         int                  not null,
   diagram_id           int                  identity(1, 1),
   version              int                  null,
   definition           varbinary(Max)       null,
   constraint PK__sysdiagr__C2B05B613C34F16F primary key (diagram_id)
         on "PRIMARY",
   constraint UK_principal_name unique (principal_id, name)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: usermanagment                                         */
/*==============================================================*/
create table dbo.usermanagment (
   id                   int                  identity(1, 1) not for replication,
   username             varchar(50)          collate Chinese_PRC_CI_AS not null,
   password             varchar(50)          collate Chinese_PRC_CI_AS not null,
   level                int                  not null,
   role                 varchar(50)          collate Chinese_PRC_CI_AS not null,
   constraint PK_user_1 primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

/*==============================================================*/
/* Table: workOrderList                                         */
/*==============================================================*/
create table dbo.workOrderList (
   id                   int                  identity(1, 1) not for replication,
   WorkOrder#工单号        varchar(100)         collate Chinese_PRC_CI_AS null,
   OrderCode            varchar(50)          collate Chinese_PRC_CI_AS null,
   OrderQuantity        int                  null,
   BusinessOrder        varchar(100)         collate Chinese_PRC_CI_AS null,
   ShipQuantity         int                  null,
   ProjectName          varchar(100)         collate Chinese_PRC_CI_AS null,
   SWVersion            varchar(100)         collate Chinese_PRC_CI_AS null,
   HWVersion            varchar(100)         collate Chinese_PRC_CI_AS null,
   Productqutity        int                  null,
   Color                varchar(50)          collate Chinese_PRC_CI_AS null,
   EANCode              varchar(50)          collate Chinese_PRC_CI_AS null,
   mark                 varchar(100)         collate Chinese_PRC_CI_AS null,
   projectid            int                  null,
   time1                datetime             null constraint DF_workOrderList_time1 default getdate(),
   PartNumber           varchar(50)          collate Chinese_PRC_CI_AS null,
   Type                 int                  null,
   Closed               bit                  null,
   constraint PK_workOrderList primary key (id)
         on "PRIMARY"
)
on "PRIMARY"
go

alter table dbo.TestFlow
   add constraint FK_TestFlow_workOrderList foreign key (workid)
      references dbo.workOrderList (id)
go

alter table dbo.workOrderList
   add constraint FK_workOrderList_projectlist foreign key (projectid)
      references dbo.projectlist (id)
go


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE TRIGGER [dbo].[AssWorkOrderUpadte]
   ON  dbo.Mobinfo
   AFTER Update
AS 
Begin
declare @AssWorkordecount int,
@AssWorkOrder varchar(50),
@NewAssWorkOrder varchar(50),
@id int
set @AssWorkordecount=(select count(*) from inserted where (inserted.AssWorkOrder like '73%' or inserted.AssWorkOrder like '74%'))
set @AssWorkOrder=(select AssWorkorder from deleted)
set @NewAssWorkOrder=(select AssWorkorder from inserted)
set @id=(select id from inserted)

if @AssWorkordecount>0   --Update工单为返工工单时
begin
if @AssWorkOrder <> @NewAssWorkOrder  --修改的工单号与以前不同时
begin
insert into PreMobinfo([id],[sn] ,[IMEI],[IMEI2] ,[IMEI3] ,[IMEI4] ,[MII],[BTMac],[WiFiMAC],[GPSCode],[BoxNumber],[PalletNumber],[PcbBoxNumber],[WorkOrder],[AssWorkOrder],[TestStation],[TestResult]
      ,[TestTime],[ProductStatus],[PC],[workid],[time1],[time2],[time3],[mark1],[mark2],[mark3],[mark4],[mark5]) SELECT [id],[sn] ,[IMEI]
      ,[IMEI2] ,[IMEI3] ,[IMEI4] ,[MII],[BTMac],[WiFiMAC],[GPSCode],[BoxNumber],[PalletNumber],[PcbBoxNumber],[WorkOrder],[AssWorkOrder],[TestStation]
      ,[TestResult],[TestTime] ,[ProductStatus],[PC],[workid],[time1],[time2],[time3],[mark1],[mark2],[mark3],[mark4],[mark5]FROM deleted
update PreMobinfo set [NewAssWorkOrder]=@NewAssWorkOrder where id=@id and PreMobinfo.[AssWorkOrder]=@AssWorkOrder          --将旧的工单插入到PreMobino中
END
end
end
go


create TRIGGER [T_User]
ON [dbo].[usermanagment]
FOR INSERT,UPDATE
AS
BEGIN    
    IF (UPDATE(password))
    BEGIN
SELECT LEFT(hashbytes('MD5',A.password),100)
FROM inserted AS A
            INNER JOIN usermanagment AS B
            ON A.id=B.id

        UPDATE B
        SET password=LEFT(hashbytes('MD5',A.password),100)
        FROM inserted AS A
            INNER JOIN usermanagment AS B
            ON A.id=B.id
    END
END
go

