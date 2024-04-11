use master 
go

DROP DATABASE IF EXISTS ET_V1
go
create database ET_V1
go
use ET_V1
go

declare @GuidUsuarioInsert uniqueidentifier;
set @GuidUsuarioInsert = Newid(); 

declare @GuidSedeOlimpicaChorrillosInsert uniqueidentifier;
set @GuidSedeOlimpicaChorrillosInsert = Newid(); 

IF OBJECT_ID('dbo.Usuario', 'U') IS NOT NULL  DROP TABLE [dbo].[Usuario]; 
create table [dbo].[Usuario] -- select * from [dbo].[Usuario]
(
	Id uniqueidentifier primary key not null,
	Nombres varchar(500) not null,
	Apellidos varchar(500) not null,
	Email varchar(500) not null unique,
	PasswordHash varchar(max) not null,
	PasswordSalt varchar(max) not null,
	[Perfil] varchar(200) not null,
	UsuarioCreador uniqueidentifier not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

Insert into [dbo].[Usuario] values (@GuidUsuarioInsert,'Danilo','Ramos','D4niloRamos@hotmail.com','VKM8pU0kY4yi+pWhfSP91E/gNKM1Kg3A7w3JI9CigiXoCpWJWQjFt2IIoxtkR88mljbD/BcY7Lb0+L6IVkmfuw==','jHRSqxS9/D3wZFq/ikOZy4RXOujJ18ybBq3HU1yzQdIa3nus5XpY84Xu2xrN+TGkOb74S6ZTl4gKgPrkmwCcJ3QU6jMiVIlmTDeFdwTLLnTaaqZkv/18du6uD9uGyvKvD3IeV2aQAMKCqB0yZIwfeI6F1TY2py61iAB5kugPs5o=','Admin',@GuidUsuarioInsert,GETDATE(),null,null,0)

IF OBJECT_ID('dbo.SedeOlimpica', 'U') IS NOT NULL  DROP TABLE [dbo].[SedeOlimpica]; 
create table [dbo].[SedeOlimpica] -- select * from [dbo].[SedeOlimpica]
(
	Id uniqueidentifier primary key not null,
	Nombre varchar(200) not null unique,
	NumeroComplejos int default(0) , 
	Presupuesto decimal(10,2)not null, 
	UsuarioCreador uniqueidentifier not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

Insert into [dbo].[SedeOlimpica] values (@GuidSedeOlimpicaChorrillosInsert,'Sede Chorrillos',1,100.3232,@GuidUsuarioInsert,GETDATE(),null,null,0)
Insert into [dbo].[SedeOlimpica] values (newid(),'Sede Surco',0,818.332,@GuidUsuarioInsert,GETDATE(),null,null,0)

IF OBJECT_ID('dbo.ComplejoDeportivo', 'U') IS NOT NULL  DROP TABLE [dbo].[ComplejoDeportivo]; 
create table [dbo].[ComplejoDeportivo] -- select * from [dbo].[ComplejoDeportivo]
(
	Id uniqueidentifier primary key not null,
	IdSedeOlimpica uniqueidentifier foreign key references [dbo].[SedeOlimpica](Id),
	Nombre varchar(200) not null,
	Localizacion varchar(200) not null,
	JefeOrganizacion varchar(200) not null,
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

Insert into [dbo].[ComplejoDeportivo] values (newid(),@GuidSedeOlimpicaChorrillosInsert,'complejo Matellini en Chorrillos','centro','Juan Rosas',@GuidUsuarioInsert,GETDATE(),null,null,0)

IF OBJECT_ID('dbo.TipoDeporte', 'U') IS NOT NULL  DROP TABLE [dbo].[TipoDeporte]; 
create table [dbo].[TipoDeporte] -- select * from [dbo].TipoDeporte
(
	Id uniqueidentifier primary key not null,
	Descripcion varchar(200) not null,
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

Insert into [dbo].[TipoDeporte] values (newid(),'Unico Deporte',@GuidUsuarioInsert,GETDATE(),null,null,0)
Insert into [dbo].[TipoDeporte] values (newid(),'Polideportivo',@GuidUsuarioInsert,GETDATE(),null,null,0)

IF OBJECT_ID('dbo.Deporte', 'U') IS NOT NULL  DROP TABLE [dbo].[Deporte]; 
create table [dbo].[Deporte] -- select * from [dbo].[Deporte]
(
	Id uniqueidentifier primary key not null,
	IdComplejoDeportivo uniqueidentifier foreign key references [dbo].[ComplejoDeportivo](Id),
	IdTipoDeporte uniqueidentifier foreign key references [dbo].[TipoDeporte](Id),
	Nombre varchar(200) not null,
	Ubicacion varchar(200) not null,
	Area varchar(200) not null,
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

IF OBJECT_ID('dbo.Evento', 'U') IS NOT NULL  DROP TABLE [dbo].[Evento]; 
create table [dbo].[Evento] -- select * from [dbo].[Evento]
(
	Id uniqueidentifier primary key not null,
	IdDeporte uniqueidentifier foreign key references [dbo].[Deporte](Id),
	Fecha varchar(200) not null,
	Duracion varchar(200) not null,
	NumeroParticipantes int not null,
	NumeroComisarios int not null,
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

IF OBJECT_ID('dbo.EventoComisario', 'U') IS NOT NULL  DROP TABLE [dbo].[EventoComisario]; 
create table [dbo].[EventoComisario] -- select * from [dbo].[EventoComisario]
(
	Id uniqueidentifier primary key not null,
	IdEvento uniqueidentifier foreign key references [dbo].[Evento](Id),
	Nombre varchar(200) not null,
	TipoComisario varchar(200) not null,
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

IF OBJECT_ID('dbo.Equipamiento', 'U') IS NOT NULL  DROP TABLE [dbo].[Equipamiento]; 
create table [dbo].[Equipamiento] -- select * from [dbo].[Equipamiento]
(
	Id uniqueidentifier primary key not null,
	Nombre varchar(200) not null,
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)

Insert into [dbo].[Equipamiento] values (newid(),'Arcos',@GuidUsuarioInsert,GETDATE(),null,null,0)
Insert into [dbo].[Equipamiento] values (newid(),'Pértigas',@GuidUsuarioInsert,GETDATE(),null,null,0)
Insert into [dbo].[Equipamiento] values (newid(),'Barras Paralelas',@GuidUsuarioInsert,GETDATE(),null,null,0)
Insert into [dbo].[Equipamiento] values (newid(),'Pesas',@GuidUsuarioInsert,GETDATE(),null,null,0)

IF OBJECT_ID('dbo.EventoEquipamiento', 'U') IS NOT NULL  DROP TABLE [dbo].[EventoEquipamiento]; 
create table [dbo].[EventoEquipamiento] -- select * from [dbo].[EventoEquipamiento]
(
	Id uniqueidentifier primary key not null,
	IdEvento uniqueidentifier foreign key references [dbo].[Evento](Id),
	IdEquipamiento uniqueidentifier foreign key references [dbo].[Equipamiento](Id),
	UsuarioCreador uniqueidentifier	not null,
	FechaCreacion datetime not null,
	UsuarioModificador uniqueidentifier,
	FechaModificacion datetime,
	Eliminado bit default(0)
)
go

-- Procedimientos y Triggers
CREATE or alter TRIGGER [dbo].[ActualizarNumeroComplejosSede]
ON [dbo].[ComplejoDeportivo]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    UPDATE so SET so.NumeroComplejos = (SELECT COUNT(*) FROM [dbo].[ComplejoDeportivo] cd where cd.Eliminado = 0 and cd.IdSedeOlimpica = so.Id) from SedeOlimpica so
END;

go

create or alter procedure [dbo].[FindUsuarioByEmail]
@Email varchar(100)
as
begin

select [Id]
      ,[Nombres]
      ,[Apellidos]
      ,[Email]
      ,[PasswordHash]
      ,[PasswordSalt]
      ,[Perfil]
      ,[UsuarioCreador]
      ,[FechaCreacion]
  FROM [dbo].[Usuario] where Email = @Email and Eliminado = 0

end

go

Create or alter procedure [dbo].[GetSedeOlimpica]
AS
BEGIN
	select 
		[Id]
		,[Nombre]
		,[NumeroComplejos]
		,[Presupuesto]
		,[UsuarioCreador]
		,[FechaCreacion]
	 from [dbo].[SedeOlimpica] where Eliminado = 0 order by FechaCreacion desc
end
go
Create or alter procedure [dbo].[FindSedeOlimpica]
@Id uniqueidentifier
AS
BEGIN
	select 
		[Id]
		,[Nombre]
		,[NumeroComplejos]
		,[Presupuesto]
		,[UsuarioCreador]
		,[FechaCreacion]
	 from [dbo].[SedeOlimpica] where Eliminado = 0 and Id=@id 
end

GO

Create or alter procedure [dbo].[InsertSedeOlimpica]
@Nombre varchar(200)
,@Presupuesto decimal(10,2)
,@UsuarioCreador uniqueidentifier
,@FechaCreacion datetime
,@Eliminado bit
,@idSedeOlimpicaReturn  uniqueidentifier output
,@codErrorReturn  int output
,@desErrorReturn  varchar(8000) output
AS
BEGIN

	SET NOCOUNT ON
	SET XACT_ABORT ON
	SET DATEFORMAT dmy
	SET LANGUAGE spanish
	SET @codErrorReturn = 0
	SET @desErrorReturn = ''

	BEGIN TRY
	PRINT 'INIT [dbo].[FindSedeOlimpica]'
	SET @idSedeOlimpicaReturn = NEWID()
		INSERT INTO [dbo].[SedeOlimpica]
					([id]
					,[Nombre]
					,[Presupuesto]
					,[UsuarioCreador]
					,[FechaCreacion]
					,[Eliminado])

	   VALUES		(@idSedeOlimpicaReturn
					,@Nombre
					,@Presupuesto
					,@UsuarioCreador
					,@FechaCreacion
					,@Eliminado)		

	PRINT 'FIN [dbo].[FindSedeOlimpica]'
	END TRY
	BEGIN CATCH
			SET @codErrorReturn = ERROR_NUMBER()
			SET @desErrorReturn = ERROR_MESSAGE()
			PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_NUMBER() as varchar))
			PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_MESSAGE() as varchar))
	END CATCH
end

go 

create or alter PROCEDURE [dbo].[UpdateSedeOlimpica]    
(    
 @Id     uniqueidentifier    
,@Nombre varchar(200)
,@Presupuesto decimal(10,2)
,@UsuarioModificador uniqueidentifier
,@FechaModificacion datetime
,@codErrorReturn  int output
,@desErrorReturn  varchar(8000) output
)     
 AS    
 BEGIN    
    
 SET NOCOUNT ON    
 SET XACT_ABORT ON    
 SET DATEFORMAT dmy    
 SET LANGUAGE spanish    
 SET @codErrorReturn = 0    
 SET @desErrorReturn = ''    
    
 BEGIN TRY    
 PRINT 'INIT [dbo].[UpdateSedeOlimpica]'    
  UPDATE [dbo].[SedeOlimpica]    
      SET     
      Nombre= @Nombre,    
      Presupuesto=@Presupuesto,    
      usuarioModificador=@usuarioModificador,    
      fechaModificacion= @FechaModificacion
	  where id = @id
 PRINT 'FIN [dbo].[UpdateSedeOlimpica]'    
          
 END TRY    
 BEGIN CATCH    
    SET @codErrorReturn = ERROR_NUMBER()    
    SET @desErrorReturn = ERROR_MESSAGE()    
    PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_NUMBER() as varchar))    
    PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_MESSAGE() as varchar))    
 END CATCH    
END    

go

create or alter procedure [dbo].[DeleteSedeOlimpica]
@Id uniqueidentifier
as
begin
update [dbo].[SedeOlimpica] set Eliminado = 1 where id=@Id
end

Go

-- Complejo Deportivo

Create or alter procedure [dbo].[GetComplejoDeportivo]
AS
BEGIN
	select 
		*
	 from [dbo].[ComplejoDeportivo] cd 
	 inner join [dbo].[SedeOlimpica] so on so.Id=cd.IdSedeOlimpica
	 where cd.Eliminado = 0 and so.Eliminado = 0
	 order by cd.FechaCreacion desc
end
go
Create or alter procedure [dbo].[FindComplejoDeportivo]
@Id uniqueidentifier
AS
BEGIN
	select 
		*
	from [dbo].[ComplejoDeportivo] cd 
	 inner join [dbo].[SedeOlimpica] so on so.Id=cd.IdSedeOlimpica
	 where cd.Eliminado = 0 and so.Eliminado = 0 and cd.Id=@id 
end

GO

Create or alter procedure [dbo].[InsertComplejoDeportivo]
@IdSedeOlimpica uniqueidentifier
,@Nombre varchar(200)
,@Localizacion varchar(200)
,@JefeOrganizacion varchar(200)
,@UsuarioCreador uniqueidentifier
,@FechaCreacion datetime
,@Eliminado bit
,@idComplejoDeportivoReturn  uniqueidentifier output
,@codErrorReturn  int output
,@desErrorReturn  varchar(8000) output
AS
BEGIN

	SET NOCOUNT ON
	SET XACT_ABORT ON
	SET DATEFORMAT dmy
	SET LANGUAGE spanish
	SET @codErrorReturn = 0
	SET @desErrorReturn = ''

	BEGIN TRY
	PRINT 'INIT [dbo].[FindComplejoDeportivo]'
	SET @idComplejoDeportivoReturn = NEWID()
		INSERT INTO [dbo].[ComplejoDeportivo]
					([id]
					,[IdSedeOlimpica]
					,[Nombre]
					,[Localizacion]
					,[JefeOrganizacion]
					,[UsuarioCreador]
					,[FechaCreacion]
					,[Eliminado])

	   VALUES		(@idComplejoDeportivoReturn
					,@IdSedeOlimpica
					,@Nombre
					,@Localizacion
					,@JefeOrganizacion
					,@UsuarioCreador
					,@FechaCreacion
					,@Eliminado)		

	PRINT 'FIN [dbo].[FindComplejoDeportivo]'
	END TRY
	BEGIN CATCH
			SET @codErrorReturn = ERROR_NUMBER()
			SET @desErrorReturn = ERROR_MESSAGE()
			PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_NUMBER() as varchar))
			PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_MESSAGE() as varchar))
	END CATCH
end

go 

create or alter PROCEDURE [dbo].[UpdateComplejoDeportivo]    
(    
 @Id     uniqueidentifier    
,@IdSedeOlimpica uniqueidentifier
,@Nombre varchar(200)
,@Localizacion varchar(200)
,@JefeOrganizacion varchar(200)
,@UsuarioModificador uniqueidentifier
,@FechaModificacion datetime
,@codErrorReturn  int output
,@desErrorReturn  varchar(8000) output
)     
 AS    
 BEGIN    
    
 SET NOCOUNT ON    
 SET XACT_ABORT ON    
 SET DATEFORMAT dmy    
 SET LANGUAGE spanish    
 SET @codErrorReturn = 0    
 SET @desErrorReturn = ''    
    
 BEGIN TRY    
 PRINT 'INIT [dbo].[UpdateComplejoDeportivo]'    
  UPDATE [dbo].[ComplejoDeportivo]    
      SET     
      Nombre= @Nombre,    
      IdSedeOlimpica= @IdSedeOlimpica,    
      Localizacion=@Localizacion,    
      JefeOrganizacion=@JefeOrganizacion,    
      usuarioModificador=@usuarioModificador,    
      fechaModificacion= @FechaModificacion
	  where id = @id
 PRINT 'FIN [dbo].[UpdateComplejoDeportivo]'    
          
 END TRY    
 BEGIN CATCH    
    SET @codErrorReturn = ERROR_NUMBER()    
    SET @desErrorReturn = ERROR_MESSAGE()    
    PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_NUMBER() as varchar))    
    PRINT '@codErrorReturn' +  RTRIM(cast(ERROR_MESSAGE() as varchar))    
 END CATCH    
END    

go

create or alter procedure [dbo].[DeleteComplejoDeportivo]
@Id uniqueidentifier
as
begin
update [dbo].[ComplejoDeportivo] set Eliminado = 1 where id=@Id
end