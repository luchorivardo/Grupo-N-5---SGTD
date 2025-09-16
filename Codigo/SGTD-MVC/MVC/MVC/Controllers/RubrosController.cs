using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.RubroDto;
using MVC.Models.Entity;

namespace MVC.Controllers
{
    public class RubrosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "rubro";

        public RubrosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RubrosApi");
        }

        // Trae los rubros y devuelve la vista parcial (para el modal)
        public async Task<IActionResult> GetRubros()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return PartialView("Modals/_TablaRubros", new List<RubroReadDTO>());

            response.EnsureSuccessStatusCode();

            var rubros = await response.Content.ReadFromJsonAsync<List<RubroReadDTO>>();
            return PartialView("Modals/_TablaRubros", rubros ?? new List<RubroReadDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> CrearRubro(RubroCreateDTO rubro)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos inválidos");

            var postResp = await _httpClient.PostAsJsonAsync(_apiBaseUrl, rubro);

            if (!postResp.IsSuccessStatusCode)
                return BadRequest("Error al crear rubro");

            // Traigo la lista actualizada
            var getResp = await _httpClient.GetAsync(_apiBaseUrl);
            if (getResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                return PartialView("Modals/_TablaRubros", new List<RubroReadDTO>());

            getResp.EnsureSuccessStatusCode();
            var rubros = await getResp.Content.ReadFromJsonAsync<List<RubroReadDTO>>() ?? new List<RubroReadDTO>();

            return PartialView("Modals/_TablaRubros", rubros);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarRubro(RubroUpdateDTO rubro)
        {
            if (!ModelState.IsValid) return BadRequest("Datos inválidos");

            var putResp = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{rubro.Id}", rubro);
            if (!putResp.IsSuccessStatusCode) return BadRequest("Error al modificar rubro");

            // Devolver tabla actualizada
            var getResp = await _httpClient.GetAsync(_apiBaseUrl);
            var rubros = await getResp.Content.ReadFromJsonAsync<List<RubroReadDTO>>() ?? new List<RubroReadDTO>();
            return PartialView("Modals/_TablaRubros", rubros);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarRubro(int id)
        {
            var delResp = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (!delResp.IsSuccessStatusCode) return BadRequest("Error al eliminar rubro");

            var getResp = await _httpClient.GetAsync(_apiBaseUrl);
            var rubros = await getResp.Content.ReadFromJsonAsync<List<RubroReadDTO>>() ?? new List<RubroReadDTO>();
            return PartialView("Modals/_TablaRubros", rubros);
        }
    }
}
