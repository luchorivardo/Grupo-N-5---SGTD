using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.LoginDto;
using MVC.Models.DTOs.ProductoDto;
using MVC.Models.DTOs.RolDto;
using MVC.Models.DTOs.RubroDto;
using MVC.Models.DTOs.UsuarioDto;
using MVC.Models.Entity;
using System.Data;
using System.Text.Json;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsuariosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "usuario";
        private readonly string _apiRolUrl = "rol";

        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UsuariosApi");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiBaseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuarios = JsonSerializer.Deserialize<List<UsuarioReadDTO>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return View(usuarios);
                }
            }
            catch (Exception ex)
            {
                // Log error si es necesario
                ViewBag.Error = "Error al cargar los usuarios";
            }

            return View(new List<UsuarioReadDTO>());     
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiRolUrl);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var roles = JsonSerializer.Deserialize<List<RolReadDTO>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ViewBag.Roles = roles;
                }
                else
                {
                    ViewBag.Roles = new List<RolReadDTO>();
                }
                using var http = new HttpClient();
                var provinciasResponse = await http.GetAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre");
                if (provinciasResponse.IsSuccessStatusCode)
                {
                    var provinciasContent = await provinciasResponse.Content.ReadAsStringAsync();

                    // DTO auxiliar
                    var provinciasWrapper = JsonSerializer.Deserialize<ProvinciaWrapper>(provinciasContent,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ViewBag.Provincias = provinciasWrapper?.Provincias
                        .OrderBy(p => p.Nombre)
                        .ToList() ?? new List<ProvinciaDTO>();
                }
                else
                {
                    ViewBag.Provincias = new List<ProvinciaDTO>();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Roles = new List<RolReadDTO>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UsuarioCreateDTO usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, usuario);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    var content = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(content);

                    ModelState.AddModelError("", "Error al crear el usuario");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error de conexión con la API");
                }
            }
            return View(usuario);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // Obtener el usuario
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
           if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var content = await response.Content.ReadAsStringAsync();
            var usuario = JsonSerializer.Deserialize<UsuarioUpdateDTO>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Cargar roles
            var rolResponse = await _httpClient.GetAsync(_apiRolUrl);
            if (rolResponse.IsSuccessStatusCode)
            {
                var rolContent = await rolResponse.Content.ReadAsStringAsync();
                var roles = JsonSerializer.Deserialize<List<RolReadDTO>>(rolContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                ViewBag.Roles = roles;
            }

            // Cargar Provincias
            using var http = new HttpClient();
            var provinciasResponse = await http.GetAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre");
            if (provinciasResponse.IsSuccessStatusCode)
            {
                var provinciasContent = await provinciasResponse.Content.ReadAsStringAsync();
                var provinciasWrapper = JsonSerializer.Deserialize<ProvinciaWrapper>(provinciasContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.Provincias = provinciasWrapper?.Provincias.OrderBy(p => p.Nombre).ToList();
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UsuarioUpdateDTO usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", usuario);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "Error al actualizar el usuario");
                }
                catch
                {
                    ModelState.AddModelError("", "Error de conexión con la API");
                }
            }

            // recargar combos si algo falla
            var rolesResponse = await _httpClient.GetAsync(_apiRolUrl);
            if (rolesResponse.IsSuccessStatusCode)
            {
                var rolesContent = await rolesResponse.Content.ReadAsStringAsync();
                var roles = JsonSerializer.Deserialize<List<RolReadDTO>>(rolesContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.Roles = roles;
            }

            using var http = new HttpClient();
            var provinciasResponse = await http.GetAsync("https://apis.datos.gob.ar/georef/api/provincias?campos=id,nombre");
            if (provinciasResponse.IsSuccessStatusCode)
            {
                var provinciasContent = await provinciasResponse.Content.ReadAsStringAsync();
                var provinciasWrapper = JsonSerializer.Deserialize<ProvinciaWrapper>(provinciasContent,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                ViewBag.Provincias = provinciasWrapper?.Provincias.OrderBy(p => p.Nombre).ToList();
            }

            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }

                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error al eliminar el usuario");
            }
        }
    }
}
