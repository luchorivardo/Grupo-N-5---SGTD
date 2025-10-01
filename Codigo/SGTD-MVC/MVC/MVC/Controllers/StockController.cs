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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                // Obtener el producto por id desde la API
                var response = await _httpClient.GetAsync($"{_apiBaseUrl}/{id}");
                if (!response.IsSuccessStatusCode)
                    return NotFound();

                var content = await response.Content.ReadAsStringAsync();
                var producto = JsonSerializer.Deserialize<ProductoUpdateDTO>(content,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (producto == null)
                    return NotFound();

                // Traer listas de disciplinas y proveedores
                var disciplinaResponse = await _httpClient.GetAsync(_apiDisciplinaUrl);
                ViewBag.Disciplinas = disciplinaResponse.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<List<DisciplinaReadDTO>>(await disciplinaResponse.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    : new List<DisciplinaReadDTO>();

                var proveedorResponse = await _httpClient.GetAsync(_apiProveedorUrl);
                ViewBag.Proveedores = proveedorResponse.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<List<ProveedorReadDTO>>(await proveedorResponse.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    : new List<ProveedorReadDTO>();

                // Mapear a UpdateDTO para enviar al POST
                var productoUpdate = new ProductoUpdateDTO
                {
                    Nombre = producto.Nombre,
                    Cantidad = producto.Cantidad,
                    Precio = producto.Precio,
                    EstadoId = producto.EstadoId,
                    DisciplinaId = producto.DisciplinaId,
                    ProveedorIds = producto.ProveedorIds
                };

                return View(productoUpdate);
            }
            catch
            {
                return StatusCode(500, "Error al cargar el producto");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ProductoUpdateDTO producto)
        {
            if (!ModelState.IsValid)
            {
                // Si hay error de validación, volver a traer listas para el form
                var disciplinaResponse = await _httpClient.GetAsync(_apiDisciplinaUrl);
                ViewBag.Disciplinas = disciplinaResponse.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<List<DisciplinaReadDTO>>(await disciplinaResponse.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    : new List<DisciplinaReadDTO>();

                var proveedorResponse = await _httpClient.GetAsync(_apiProveedorUrl);
                ViewBag.Proveedores = proveedorResponse.IsSuccessStatusCode
                    ? JsonSerializer.Deserialize<List<ProveedorReadDTO>>(await proveedorResponse.Content.ReadAsStringAsync(),
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                    : new List<ProveedorReadDTO>();

                return View(producto);
            }

            try
            {
                var response = await _httpClient.PutAsJsonAsync($"{_apiBaseUrl}/{id}", producto);

                if (response.IsSuccessStatusCode)
                    return RedirectToAction(nameof(Index));

                var errorContent = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", $"Error al actualizar el producto: {errorContent}");
            }
            catch
            {
                ModelState.AddModelError("", "Error de conexión con la API");
            }

            return View(producto);
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
