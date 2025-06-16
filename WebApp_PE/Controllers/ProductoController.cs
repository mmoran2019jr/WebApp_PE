using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.Services;
using DataAccess.BsnLogic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApp_PE.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        private readonly IProductoService _service;

        public ProductoController(IProductoService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var productos = await _service.ObtenerTodosAsync(); // devuelve List<Producto>

            var productosViewModel = productos.Select(p => new ProductoListadoViewModel
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre ?? string.Empty,
                Unidad = p.Unidad ?? string.Empty
            }).ToList();

            return View(productosViewModel);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            return View(new ProductoViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                await _service.CrearAsync(model);
                TempData["Success"] = "Producto creado correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al crear el producto: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerProducto(int id)
        {
            ProductoListadoViewModel model;

            if (id == 0)
            {
                model = new ProductoListadoViewModel(); // Crear nuevo
            }
            else
            {
                var producto = await _service.ObtenerPorIdAsync(id);
                if (producto == null)
                    return NotFound();

                model = new ProductoListadoViewModel
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre ?? string.Empty,
                    Unidad = producto.Unidad ?? string.Empty
                };
            }

            return PartialView("_ProductoModal", model);
        }

        [HttpPost]
        public async Task<IActionResult> Guardar(ProductoListadoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return PartialView("_ProductoModal", model);
            }

            try
            {
                if (model.IdProducto == 0)
                {
                    // Crear
                    var nuevoProducto = new ProductoViewModel
                    {
                        Nombre = model.Nombre,
                        Unidad = model.Unidad
                    };

                    await _service.CrearAsync(nuevoProducto);
                }
                else
                {
                    var productoEditado = new ProductoViewModel
                    {
                        Nombre = model.Nombre,
                        Unidad = model.Unidad
                    };

                    await _service.ActualizarAsync(model.IdProducto, productoEditado); // ← CORREGIDO
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al guardar el producto: " + ex.Message);
                return PartialView("_ProductoModal", model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            var producto = await _service.ObtenerPorIdAsync(id);
            if (producto == null)
                return NotFound();

            var model = new ProductoListadoViewModel
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre ?? string.Empty,
                Unidad = producto.Unidad ?? string.Empty
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int id, ProductoListadoViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            try
            {
                var productoEditado = new ProductoViewModel
                {
                    Nombre = model.Nombre,
                    Unidad = model.Unidad
                };

                await _service.ActualizarAsync(id, productoEditado); // ← CORREGIDO

                TempData["Success"] = "Producto actualizado correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error al actualizar el producto: " + ex.Message);
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Eliminar(int id)
        {
            var producto = await _service.ObtenerPorIdAsync(id);
            if (producto == null)
                return NotFound();

            var model = new ProductoListadoViewModel
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre ?? string.Empty,
                Unidad = producto.Unidad ?? string.Empty
            };

            return View(model);
        }

        [HttpPost, ActionName("Eliminar")]
        public async Task<IActionResult> EliminarConfirmado(int id)
        {
            try
            {
                await _service.EliminarAsync(id);
                TempData["Success"] = "Producto eliminado correctamente";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Error al eliminar el producto: " + ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Detalles(int id)
        {
            var producto = await _service.ObtenerPorIdAsync(id);
            if (producto == null)
                return NotFound();

            var model = new ProductoListadoViewModel
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre ?? string.Empty,
                Unidad = producto.Unidad ?? string.Empty
            };

            return View(model);
        }
    }
}
