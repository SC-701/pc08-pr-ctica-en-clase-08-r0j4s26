using Abstracciones.Modelos;

namespace Abstracciones.Interfaces.Flujo
{
    public interface IProductoFlujo
    {
        Task<IEnumerable<ProductoResponse>> Obtener();
        Task<ProductoResponse> Obtener(Guid Id);
        Task<Guid> Agregar(ProductoRequest vehiculo);
        Task<Guid> Editar(Guid Id, ProductoRequest vehiculo);
        Task<Guid> Eliminar(Guid Id);
    }
}