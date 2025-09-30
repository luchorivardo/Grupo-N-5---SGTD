using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.RolDto;

namespace MVC.Controllers
{
    public class RolesController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "rol";

        public RolesController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("RolesApi");
        }

        // Trae los rubros y devuelve la vista parcial (para el modal)
        public async Task<IActionResult> GetRoles()
        {
            var response = await _httpClient.GetAsync(_apiBaseUrl);

            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return PartialView("Modals/_TablaRoles", new List<RolReadDTO>());

            response.EnsureSuccessStatusCode();

            var roles = await response.Content.ReadFromJsonAsync<List<RolReadDTO>>();
            return PartialView("Modals/_TablaRoles", roles ?? new List<RolReadDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> CrearRol(RolCreateDTO rol)
        {
            if (!ModelState.IsValid)
                return BadRequest("Datos inválidos");

            var postResp = await _httpClient.PostAsJsonAsync(_apiBaseUrl, rol);

            if (!postResp.IsSuccessStatusCode)
                return BadRequest("Error al crear rubro");

            // Traigo la lista actualizada
            var getResp = await _httpClient.GetAsync(_apiBaseUrl);
            if (getResp.StatusCode == System.Net.HttpStatusCode.NoContent)
                return PartialView("Modals/_TablaRoles", new List<RolReadDTO>());

            getResp.EnsureSuccessStatusCode();
            var rubros = await getResp.Content.ReadFromJsonAsync<List<RolReadDTO>>() ?? new List<RolReadDTO>();

            return PartialView("Modals/_TablaRoles", rubros);
        }

        [HttpPost]
        public async Task<IActionResult> ModificarRol(RolUpdateDTO rol)
        {
            if (!ModelState.IsValid) return BadRequest("Datos inválidos");

            var putResp = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{rol.Id}", rol);
            if (!putResp.IsSuccessStatusCode) return BadRequest("Error al modificar rubro");

            // Devolver tabla actualizada
            var getResp = await _httpClient.GetAsync(_apiBaseUrl);
            var rubros = await getResp.Content.ReadFromJsonAsync<List<RolReadDTO>>() ?? new List<RolReadDTO>();
            return PartialView("Modals/_TablaRoles", rubros);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarRol(int id)
        {
            var delResp = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");
            if (!delResp.IsSuccessStatusCode) return BadRequest("Error al eliminar rubro");

            var getResp = await _httpClient.GetAsync(_apiBaseUrl);
            var rubros = await getResp.Content.ReadFromJsonAsync<List<RolReadDTO>>() ?? new List<RolReadDTO>();
            return PartialView("Modals/_TablaRoles", rubros);
        }
    }
}
