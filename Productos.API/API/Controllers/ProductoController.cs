using Abstracciones.Interfaces.API;
using Abstracciones.Interfaces.Flujo;
using Abstracciones.Modelos;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase, IProductoController
    {
        private readonly IProductoFlujo _productoFlujo;
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(IProductoFlujo productoFlujo, ILogger<ProductoController> logger)
        {
            _productoFlujo = productoFlujo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Agregar([FromBody] ProductoRequest producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _productoFlujo.Agregar(producto);
                return CreatedAtAction(nameof(Obtener), new { Id = resultado }, producto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al agregar producto en Moto Repuestos Rojas");
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }



        [HttpPut("{Id}")]
        public async Task<IActionResult> Editar(Guid Id, [FromBody] ProductoRequest producto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var resultado = await _productoFlujo.Editar(Id, producto);
                return Ok(new { mensaje = "Producto actualizado con éxito", id = resultado });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al editar producto {Id}");
                return NotFound(new { error = ex.Message });
            }
        }


        [HttpDelete("{Id}")]
        public async Task<IActionResult> Eliminar(Guid Id)
        {
            try
            {
                await _productoFlujo.Eliminar(Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar producto {Id}");
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Obtener()
        {
            var resultado = await _productoFlujo.Obtener();
            if (resultado == null || !resultado.Any())
                return NoContent();

            return Ok(resultado);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> Obtener(Guid Id)
        {
            try
            {
                var resultado = await _productoFlujo.Obtener(Id);
                if (resultado == null) return NotFound();
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}