using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.DTOs.DisciplinaDto;
using MVC.Models.DTOs.ProductoDto;
using MVC.Models.DTOs.ProveedorDto;
using MVC.Models.DTOs.RubroDto;
using MVC.Models.Entity;
using System.Text.Json;

namespace MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StockController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl = "producto";
        private readonly string _apiDisciplinaUrl = "disciplina";
        private readonly string _apiProveedorUrl = "proveedor";


        public StockController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ProductosApi");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync(_apiBaseUrl);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var productos = JsonSerializer.Deserialize<List<ProductoReadDTO>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    return View(productos);
                }
            }
            catch (Exception ex)
            {
                // Log error si es necesario
                ViewBag.Error = "Error al cargar los productos";
            }

            return View(new List<ProductoReadDTO>());
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var disciplinaResponse = await _httpClient.GetAsync(_apiDisciplinaUrl);
                if (disciplinaResponse.IsSuccessStatusCode)
                {
                    var content = await disciplinaResponse.Content.ReadAsStringAsync();
                    var disciplinas = JsonSerializer.Deserialize<List<DisciplinaReadDTO>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ViewBag.Disciplinas = disciplinas;
                }
                else
                {
                    ViewBag.Disciplinas = new List<DisciplinaReadDTO>();
                }

                var proveedorResponse = await _httpClient.GetAsync(_apiProveedorUrl);
                if (proveedorResponse.IsSuccessStatusCode)
                {
                    var content = await proveedorResponse.Content.ReadAsStringAsync();
                    var proveedores = JsonSerializer.Deserialize<List<ProveedorReadDTO>>(content,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    ViewBag.Proveedores = proveedores;
                }
                else
                {
                    ViewBag.Proveedores = new List<ProveedorReadDTO>();
                }
            }
            catch (Exception ex)
            {
                ViewBag.Disciplinas = new List<DisciplinaReadDTO>();
                ViewBag.Proveedores = new List<ProveedorReadDTO>();
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductoCreateDTO producto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var response = await _httpClient.PostAsJsonAsync(_apiBaseUrl, producto);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ModelState.AddModelError("", "Error al crear el producto");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Error de conexión con la API");
                }
            }
            return View(producto);
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
