-- =============================================
-- Author:		Sebastian Rojas Vargas
-- Create date: 1/19/26
-- Description:	AgregarProducto
-- =============================================
CREATE PROCEDURE [dbo].[AgregarProducto] 
	-- Add the parameters for the stored procedure here
       @Id AS uniqueidentifier
       ,@IdSubCategoria AS uniqueidentifier
       ,@Nombre AS varchar(MAX)
       ,@Descripcion AS varchar(MAX)
       ,@Precio AS decimal(18,0)
       ,@Stock AS int
       ,@CodigoBarras AS varchar(MAX)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	BEGIN TRANSACTION
        INSERT INTO [dbo].[Producto]
               ([Id]
               ,[IdSubCategoria]
               ,[Nombre]
               ,[Descripcion]
               ,[Precio]
               ,[Stock]
               ,[CodigoBarras])
         VALUES
               (@Id 
                ,@IdSubCategoria
                ,@Nombre 
                ,@Descripcion
                ,@Precio
                ,@Stock 
                ,@CodigoBarras) 
        SELECT @Id
    COMMIT TRANSACTION
END