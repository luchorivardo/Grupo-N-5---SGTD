using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    using global::MVC.Models.Entity;
    using System.Text.Json;

    namespace MVC.Controllers
    {
        public class ProveedoresController : Controller
        {
            private readonly HttpClient _httpClient;
            private readonly string _apiBaseUrl = "proveedor"; // endpoint base de tu API

            public ProveedoresController(IHttpClientFactory httpClientFactory)
            {
                _httpClient = httpClientFactory.CreateClient("ProveedoresApi");
            }

            public async Task<IActionResult> Index()
            {
                try
                {
                    var response = await _httpClient.GetAsync(_apiBaseUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var proveedores = JsonSerializer.Deserialize<List<Proveedor>>(content,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return View(proveedores);
                    }
                }
                catch (Exception ex)
                {
                    // Log error si es necesario
                    ViewBag.Error = "Error al cargar los proveedores";
                }

                return View(new List<Proveedor>());
            }

            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Create(Proveedor proveedor)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, proveedor);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                        ModelState.AddModelError("", "Error al crear el proveedor");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error de conexión con la API");
                    }
                }
                return View(proveedor);
            }

            public async Task<IActionResult> Edit(int id)
            {
                try
                {
                    var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");

                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var proveedor = JsonSerializer.Deserialize<Proveedor>(content,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                        return View(proveedor);
                    }
                }
                catch (Exception ex)
                {
                    // Log error
                }

                return NotFound();
            }

            [HttpPost]
            public async Task<IActionResult> Edit(int id, Proveedor proveedor)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", proveedor);

                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction(nameof(Index));
                        }

                        ModelState.AddModelError("", "Error al actualizar el proveedor");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", "Error de conexión con la API");
                    }
                }
                return View(proveedor);
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
                    return StatusCode(500, "Error al eliminar el proveedor");
                }
            }
        }
    }
}
