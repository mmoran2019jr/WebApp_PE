using DataAccess.BsnLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic
{
    public static class DbInitializer
    {
        public static void Seed(ApplicationDbContext context)
        {
            // Asegura que la base de datos esté creada
            context.Database.EnsureCreated();

            // Si no hay grupos, agrega uno
            if (!context.Grupos.Any())
            {
                var grupo = new Grupo
                {
                    Nombre = "Administrador"
                };
                context.Grupos.Add(grupo);
                context.SaveChanges();
            }

            // Si no hay usuarios, agrega uno
            if (!context.Usuarios.Any())
            {
                var grupoExistente = context.Grupos.First();

                var usuario = new Usuario
                {
                    Nombre = "admin",
                    ContraseñaHash = BCrypt.Net.BCrypt.HashPassword("admin123"), // Contraseña encriptada
                    IdGrupo = grupoExistente.IdGrupo
                };

                context.Usuarios.Add(usuario);
                context.SaveChanges();
            }
        }
    }
}
