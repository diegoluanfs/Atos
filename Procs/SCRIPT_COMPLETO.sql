
--create database Report
	
--create login adm_report with password='adm_report';
--create user adm_report from login adm_report;
--exec sp_addrolemember 'DB_DATAREADER', 'adm_report';
--exec sp_addrolemember 'DB_DATAWRITER', 'adm_report'

	--CREATE TABLE [dbo].[USERS_STATUS](
	--	[ID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	--	[NAME] [varchar](50) NOT NULL
	--)
	
	--INSERT INTO dbo.[USERS_STATUS](NAME)VALUES('AGUARDANDO CONFIRMAÇÃO')
	--INSERT INTO dbo.[USERS_STATUS](NAME)VALUES('INATIVO')
	--INSERT INTO dbo.[USERS_STATUS](NAME)VALUES('ATIVO')
	--INSERT INTO dbo.[USERS_STATUS](NAME)VALUES('BLOQUEADO')

	--SELECT * FROM USERS_STATUS
	
	--CREATE TABLE [dbo].[USERS](
	--	[ID] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	--	[LOGIN] [varchar](50) NOT NULL,
	--	[PASS] [varchar](100) NOT NULL,
	--	[TOKEN] [varchar](64) NOT NULL,
	--	[CREATED] [datetime] NOT NULL,
	--	[UPDATED] [datetime] NOT NULL,
	--	[CREATED_BY] [int] NOT NULL,
	--	[UPDATED_BY] [int] NOT NULL,
	--	[FK_STATUS] [int] NOT NULL,
	--	[ID_HASH] [uniqueidentifier] NULL,
	--	CONSTRAINT fk_StatusUser FOREIGN KEY (FK_STATUS) REFERENCES [dbo].[USERS_STATUS] (ID)
	--)
	
	--INSERT INTO [dbo].[USERS]
	--		   ([LOGIN]
	--		   ,[PASS]
	--		   ,[TOKEN]
	--		   ,[CREATED]
	--		   ,[UPDATED]
	--		   ,[CREATED_BY]
	--		   ,[UPDATED_BY]
	--		   ,[FK_STATUS]
	--		   ,[ID_HASH])
	--	 VALUES
	--		   ('teste@teste.com'
	--		   ,'123'
	--		   ,'1234'
	--		   ,'2022-11-22'
	--		   ,'2022-11-22'
	--		   ,1
	--		   ,1
	--		   ,3
	--		   ,NEWID())

	--select * from dbo.[user]

	--CREATE TABLE [dbo].[ACCESS_KEY_STATUS](
	--	[ID] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	--	[NAME] [varchar](50) NOT NULL
	--)
	
	--INSERT INTO [dbo].[ACCESS_KEY_STATUS]([NAME]) VALUES('ATIVO')
	--INSERT INTO [dbo].[ACCESS_KEY_STATUS]([NAME]) VALUES('INATIVO')
	
	--CREATE TABLE [dbo].[ACCESS_KEY_CLIENT](
	--	[CREATED] [datetime] NOT NULL,
	--	[FK_USER] [int] NOT NULL,
	--	[HASH] [uniqueidentifier] NOT NULL,
	--	[KEY] [varchar](256) NOT NULL,
	--	[EXPIRE] [datetime] NOT NULL,
	--	[FK_STATUS] [int] NOT NULL,
	--	[IP] [varchar](15) NOT NULL,
	--	CONSTRAINT fk_AccessUser FOREIGN KEY (FK_USER) REFERENCES [dbo].[USERS] (ID),
	--	CONSTRAINT fk_AccessStatus FOREIGN KEY (FK_STATUS) REFERENCES [dbo].[ACCESS_KEY_STATUS] (ID),
	--)
	






