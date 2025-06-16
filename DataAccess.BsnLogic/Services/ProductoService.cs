using DataAccess.BsnLogic.Interfaces;
using DataAccess.BsnLogic.Models;
using DataAccess.BsnLogic.Repositories;
using DataAccess.BsnLogic.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _repository;

        public ProductoService(IProductoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductoListadoViewModel>> ObtenerTodosAsync()
        {
            var productos = await _repository.ObtenerTodosAsync();
            return productos.Select(p => new ProductoListadoViewModel
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre ?? string.Empty,
                Unidad = p.Unidad ?? string.Empty
            }).ToList();
        }

        public async Task<ProductoListadoViewModel?> ObtenerPorIdAsync(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            if (producto == null) return null;

            return new ProductoListadoViewModel
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre ?? string.Empty,
                Unidad = producto.Unidad ?? string.Empty
            };
        }

        public async Task CrearAsync(ProductoViewModel model)
        {
            var producto = new Producto
            {
                Nombre = model.Nombre,
                Unidad = model.Unidad
            };

            await _repository.CrearAsync(producto);
        }

        public async Task ActualizarAsync(int id, ProductoViewModel model)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            if (producto == null)
                throw new Exception("Producto no encontrado.");

            producto.Nombre = model.Nombre;
            producto.Unidad = model.Unidad;

            await _repository.ActualizarAsync(producto);
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _repository.ObtenerPorIdAsync(id);
            if (producto == null)
                throw new Exception("Producto no encontrado.");

            await _repository.EliminarAsync(producto);
        }
    }
}
