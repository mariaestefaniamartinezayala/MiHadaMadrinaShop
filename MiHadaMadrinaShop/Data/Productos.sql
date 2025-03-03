/*
   s&aacute;bado, 27 de mayo de 202321:35:13
   User: 
   Server: DESKTOP-S8EQPP8
   Database: MiHadaMadrinaHandMadeDB
   Application: 
*/

/* To prevent any potential data loss issues, you should review this script in detail before running it outside the context of the database designer.*/
BEGIN TRANSACTION
SET QUOTED_IDENTIFIER ON
SET ARITHABORT ON
SET NUMERIC_ROUNDABORT OFF
SET CONCAT_NULL_YIELDS_NULL ON
SET ANSI_NULLS ON
SET ANSI_PADDING ON
SET ANSI_WARNINGS ON
COMMIT
BEGIN TRANSACTION
GO
CREATE TABLE dbo.Tmp_Productos
	(
	IdProducto bigint NOT NULL IDENTITY (1, 1),
	DescripcionCorta nvarchar(200) NULL,
	DescripcionLarga ntext NULL,
	FechaDeEntrada datetime NOT NULL,
	Imagen nvarchar(MAX) NULL,
	Nombre nvarchar(100) NOT NULL,
	PorcentajeDeDescuento int NULL,
	Precio decimal(10, 2) NOT NULL,
	PrecioConDescuento decimal(10, 2) NULL,
	Stock int NULL,
	UrlProductoDigital nvarchar(MAX) NULL,
	EsDigital bit NOT NULL,
	EsActivo bit NOT NULL
	)  ON [PRIMARY]
	 TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE dbo.Tmp_Productos SET (LOCK_ESCALATION = TABLE)
GO
SET IDENTITY_INSERT dbo.Tmp_Productos ON
GO
IF EXISTS(SELECT * FROM dbo.Productos)
	 EXEC('INSERT INTO dbo.Tmp_Productos (IdProducto, DescripcionCorta, DescripcionLarga, FechaDeEntrada, Imagen, Nombre, PorcentajeDeDescuento, Precio, PrecioConDescuento, Stock, UrlProductoDigital)
		SELECT IdProducto, DescripcionCorta, DescripcionLarga, FechaDeEntrada, Imagen, Nombre, PorcentajeDeDescuento, Precio, PrecioConDescuento, Stock, UrlProductoDigital FROM dbo.Productos WITH (HOLDLOCK TABLOCKX)')
GO
SET IDENTITY_INSERT dbo.Tmp_Productos OFF
GO
ALTER TABLE dbo.Productos_Categorias
	DROP CONSTRAINT FK_Productos_Categorias_Productos
GO
ALTER TABLE dbo.Productos_Pedido
	DROP CONSTRAINT FK_Productos_Pedido_Productos
GO
ALTER TABLE dbo.T_Cesta
	DROP CONSTRAINT FK_T_Cesta_Productos
GO
DROP TABLE dbo.Productos
GO
EXECUTE sp_rename N'dbo.Tmp_Productos', N'Productos', 'OBJECT' 
GO
ALTER TABLE dbo.Productos ADD CONSTRAINT
	PK_Productos PRIMARY KEY CLUSTERED 
	(
	IdProducto
	) WITH( STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.T_Cesta ADD CONSTRAINT
	FK_T_Cesta_Productos FOREIGN KEY
	(
	IdProducto
	) REFERENCES dbo.Productos
	(
	IdProducto
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.T_Cesta SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Productos_Pedido ADD CONSTRAINT
	FK_Productos_Pedido_Productos FOREIGN KEY
	(
	IdProducto
	) REFERENCES dbo.Productos
	(
	IdProducto
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Productos_Pedido SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
BEGIN TRANSACTION
GO
ALTER TABLE dbo.Productos_Categorias ADD CONSTRAINT
	FK_Productos_Categorias_Productos FOREIGN KEY
	(
	IdProducto
	) REFERENCES dbo.Productos
	(
	IdProducto
	) ON UPDATE  NO ACTION 
	 ON DELETE  NO ACTION 
	
GO
ALTER TABLE dbo.Productos_Categorias SET (LOCK_ESCALATION = TABLE)
GO
COMMIT
