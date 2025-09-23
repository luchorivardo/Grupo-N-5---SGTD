using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.LoginDto;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

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

        /*
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
        } */

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

                // Creamos los claims (info que viaja en la cookie)
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.Nombre),
            new Claim(ClaimTypes.Role, usuario.RolUsuarioId.ToString()) 
            // podés usar "Admin"/"Empleado" en lugar de Id si preferís
        };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = true, // para mantener la cookie aunque cierre el navegador
                    ExpiresUtc = DateTimeOffset.UtcNow.AddHours(2) // expira en 2 horas
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Credenciales inválidas");
            return View(loginDto);
        }
         /*
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
         */

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }
    }

}
