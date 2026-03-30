-- =============================================
-- Author:		Sebastian Rojas Vargas
-- Create date: 1/19/26
-- Description:	ObtenerProducto
-- =============================================
CREATE PROCEDURE [dbo].[ObtenerProducto] 
	-- Add the parameters for the stored procedure here
	@Id uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Producto.Id, Producto.IdSubCategoria, Producto.Nombre, Producto.Descripcion, Producto.Precio, Producto.Stock, Producto.CodigoBarras, Categorias.Nombre AS Categoria, SubCategorias.Nombre AS SubCategoria
	FROM     Producto INNER JOIN
                  Categorias ON Producto.Id = Categorias.Id INNER JOIN
                  SubCategorias ON Categorias.Id = SubCategorias.IdCategoria
	WHERE  (Producto.Id = @Id)
END