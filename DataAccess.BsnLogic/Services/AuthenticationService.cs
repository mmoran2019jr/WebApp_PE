using DataAccess.BsnLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic.Services
{
    public class AuthenticationService
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationService(ApplicationDbContext context)
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

            // Verifica que la contraseña ingresada coincida con el hash guardado
            bool contraseñaValida = BCrypt.Net.BCrypt.Verify(contraseña, usuario.ContraseñaHash);

            return contraseñaValida ? usuario : null;
        }

        private string CalcularHash(string input)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(input);
            var hashBytes = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}
