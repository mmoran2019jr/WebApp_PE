using DataAccess.BsnLogic.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BsnLogic
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Formula> Formulas { get; set; }
        public DbSet<FormulaMateriales> FormulaMateriales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //Definicion de relaciones y claves
            modelBuilder.Entity<Formula>()
                .HasKey(f => f.IdFormula);

            modelBuilder.Entity<Formula>()
                .HasOne(f => f.Producto)
                .WithMany()
                .HasForeignKey(f => f.IdProducto)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Formula>()
                .Property(f => f.Cantidad)
                .HasPrecision(18, 2);

            modelBuilder.Entity<FormulaMateriales>()
                .HasKey(fm => fm.Id); 

            modelBuilder.Entity<FormulaMateriales>()
                .HasOne(fm => fm.Formula)
                .WithMany(f => f.Materiales)
                .HasForeignKey(fm => fm.IdFormula)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<FormulaMateriales>()
                .HasOne(fm => fm.Producto)
                .WithMany()
                .HasForeignKey(fm => fm.IdProducto)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FormulaMateriales>()
                .Property(fm => fm.Cantidad)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Producto>()
                .HasKey(p => p.IdProducto);

            modelBuilder.Entity<Grupo>()
                .HasKey(g => g.IdGrupo);

            modelBuilder.Entity<Usuario>()
                .HasKey(u => u.IdUsuario);

            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Grupo)
                .WithMany(g => g.Usuarios)
                .HasForeignKey(u => u.IdGrupo);
        }
    }
}
