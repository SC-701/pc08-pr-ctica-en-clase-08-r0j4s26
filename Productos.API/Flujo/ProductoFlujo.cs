using Abstracciones.Interfaces.DA;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Interfaces.Reglas;
using Abstracciones.Modelos;
namespace Flujo
{
    public class ProductoFlujo : IProductoFlujo
    {
        private readonly IProductoDA _productoDA;
        private readonly IProductoReglas _productoReglas;

        public ProductoFlujo(IProductoDA productoDA, IProductoReglas productoReglas)
        {
            _productoDA = productoDA;
            _productoReglas = productoReglas;
        }

        public async Task<IEnumerable<ProductoResponse>> Obtener()
        {
            return await _productoDA.Obtener();
        }

        public async Task<ProductoResponse> Obtener(Guid Id)
        {
            var producto = await _productoDA.Obtener(Id);

            if (producto != null)
            {

                producto.PrecioUSD = await _productoReglas.CalcularPrecioUSD(producto.Precio);
            }

            return producto;
        }

        public Task<Guid> Agregar(ProductoRequest vehiculo) => _productoDA.Agregar(vehiculo);
        public Task<Guid> Editar(Guid Id, ProductoRequest vehiculo) => _productoDA.Editar(Id, vehiculo);
        public Task<Guid> Eliminar(Guid Id) => _productoDA.Eliminar(Id);
    }
}
