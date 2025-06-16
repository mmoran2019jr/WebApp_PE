using DataAccess.BsnLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Usuario? ValidarUsuario(string nombre, string contraseña)
        {
            var usuario = _context.Usuarios
                .Include(u => u.Grupo)
                .FirstOrDefault(u => u.Nombre == nombre);

            if (usuario == null)
                return null;

            // Verifica si la contraseña ingresada coincide con el hash guardado usando BCrypt
            bool contraseñaValida = BCrypt.Net.BCrypt.Verify(contraseña, usuario.ContraseñaHash);

            return contraseñaValida ? usuario : null;
        }

        // Utilidad para generar hashes en nuevos registros
        public string CalcularHash(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
    }
}
