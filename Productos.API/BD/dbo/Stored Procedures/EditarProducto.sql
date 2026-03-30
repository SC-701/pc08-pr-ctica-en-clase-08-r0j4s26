-- =============================================
-- Author:		Sebastian Rojas Vargas
-- Create date: 1/19/26
-- Description:	EditarProducto
-- =============================================
CREATE PROCEDURE [dbo].[EditarProducto] 
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
       UPDATE [dbo].[Producto]
       SET [IdSubCategoria] = @IdSubCategoria
          ,[Nombre] = @Nombre
          ,[Descripcion] = @Descripcion
          ,[Precio] = @Precio
          ,[Stock] = @Stock
          ,[CodigoBarras] = @CodigoBarras
       WHERE Id = @Id
       SELECT @Id
    COMMIT TRANSACTION
END