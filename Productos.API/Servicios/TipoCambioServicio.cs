using Abstracciones.Interfaces.Servicios;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Servicios
{
    public class TipoCambioServicio : ITipoCambioServicio
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public TipoCambioServicio(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<decimal> ObtenerTipoCambioVenta()
        {
            var urlBase = _configuration["BancoCentralCR:UrlBase"];
            var token = _configuration["BancoCentralCR:Token"];

            string fecha = DateTime.Now.ToString("yyyy/MM/dd");
            string urlFinal = $"{urlBase}?fechaInicio={fecha}&fechaFin={fecha}&idioma=ES";

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync(urlFinal);

            if (!response.IsSuccessStatusCode)
                return 0;

            var contenido = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(contenido);

            var valor = doc.RootElement
                .GetProperty("datos")[0]
                .GetProperty("indicadores")[0]
                .GetProperty("series")[0]
                .GetProperty("valorDatoPorPeriodo")
                .GetDecimal();

            return valor;
        }
    }
}