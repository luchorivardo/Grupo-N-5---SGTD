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
    public class UsuariosController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "usuario";
        private readonly string _apiRolUrl = "rol";

        public UsuariosController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UsuariosApi");
        }

        public async Task<IActionResult> Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("UsuarioId")))
                return RedirectToAction("Login", "Auth");
            else
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
              
        }
        [HttpPost]

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

                    ModelState.AddModelError("", "Error al crear el usuario");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error de conexión con la API");
                }
            }
            return View(usuario);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuario = JsonSerializer.Deserialize<Usuario>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return View(usuario);
                }
            }
            catch (Exception ex)
            {
                // Log error
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Usuario usuario)
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
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error de conexión con la API");
                }
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
