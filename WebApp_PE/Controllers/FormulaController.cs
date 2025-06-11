using DataAccess.BsnLogic.Models;
using DataAccess.BsnLogic;
using Microsoft.AspNetCore.Mvc;
using WebApp_PE.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace WebApp_PE.Controllers
{
    [Authorize]
    public class FormulaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FormulaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");
            ViewBag.ProductosJson = JsonSerializer.Serialize(productos.Select(p => new
            {
                p.IdProducto,
                p.Nombre
            }));

            // Obtener fórmulas con sus materiales
            var formulas = _context.Formulas
                .Include(f => f.Producto)
                .Include(f => f.Materiales)
                    .ThenInclude(m => m.Producto)
                .ToList();

            ViewBag.Formulas = formulas;

            ViewBag.Formulas = formulas;

            return View(new FormulaViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Crear(FormulaViewModel model)
        {
            var productos = _context.Productos.ToList();
            ViewBag.Productos = new SelectList(productos, "IdProducto", "Nombre");
            ViewBag.ProductosJson = JsonSerializer.Serialize(productos.Select(p => new
            {
                p.IdProducto,
                p.Nombre
            }));

            if (!ModelState.IsValid)
            {
                return View(model);
            }


            //Creacion de la formula y sus materiales
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var formula = new Formula
                {
                    IdProducto = model.IdProducto,
                    Nombre = model.Nombre,
                    Cantidad = model.Cantidad,
                    Materiales = model.Materiales.Select(m => new FormulaMateriales
                    {
                        IdProducto = m.IdProducto,
                        Nombre = m.Nombre, // puedes omitir esto si quieres usar solo FK
                        Cantidad = m.Cantidad
                    }).ToList()
                };

                _context.Formulas.Add(formula);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                TempData["Success"] = "Fórmula guardada correctamente";
                return RedirectToAction("Crear");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                ModelState.AddModelError("", "Ocurrió un error al guardar la fórmula: " + ex.Message);
                return View(model);
            }
        }
    }
}
