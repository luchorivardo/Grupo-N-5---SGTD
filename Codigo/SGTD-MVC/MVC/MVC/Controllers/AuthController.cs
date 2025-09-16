using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.LoginDto;
using System.Text.Json;

namespace MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "usuario/login"; 

        public AuthController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("UsuariosApi");
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return View(loginDto);

            var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, loginDto);

            if (response.IsSuccessStatusCode)
            {
             
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonSerializer.Deserialize<UsuarioSessionDTO>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                // Guardar en session (para manejar roles)
                HttpContext.Session.SetString("UsuarioId", usuario.Id.ToString());
                HttpContext.Session.SetString("RolUsuarioId", usuario.RolUsuarioId.ToString());
                HttpContext.Session.SetString("Nombre", usuario.Nombre);

            
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Credenciales inválidas");
            return View(loginDto);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }

}
