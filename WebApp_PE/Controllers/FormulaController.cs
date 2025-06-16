using DataAccess.BsnLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.ViewModels;

namespace WebApp_PE.Controllers
{
    [Authorize]
    public class FormulaController : Controller
    {
        private readonly IFormulaService _formulaService;

        public FormulaController(IFormulaService formulaService)
        {
            _formulaService = formulaService;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var productos = _formulaService.ObtenerProductos();
            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");
            ViewBag.ProductosJson = JsonSerializer.Serialize(productos.Select(p => new
            {
                p.IdProducto,
                p.Nombre
            }));

            var formulas = _formulaService.ObtenerFormulas();
            ViewBag.Formulas = formulas;

            return View(new FormulaViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Crear(FormulaViewModel model)
        {
            var productos = _formulaService.ObtenerProductos();
            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");
            ViewBag.ProductosJson = JsonSerializer.Serialize(productos.Select(p => new
            {
                p.IdProducto,
                p.Nombre
            }));

            ViewBag.Formulas = _formulaService.ObtenerFormulas();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _formulaService.CrearFormulaAsync(model);
                TempData["Success"] = "Fórmula guardada correctamente";
                return RedirectToAction("Crear");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocurrió un error al guardar la fórmula: " + ex.Message);
                return View(model);
            }
        }
    }
}