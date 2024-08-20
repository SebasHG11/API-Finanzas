using ApiFinanzas.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiFinanzas.Data {
    public class ApiContext: DbContext {
        public ApiContext(DbContextOptions<ApiContext> options): base(options) { }

        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Ingreso>(entity =>{
                entity.ToTable("Ingreso");
                entity.HasKey(i => i.Id);
                entity.Property(i => i.Concepto).IsRequired(true).HasMaxLength(80);
                entity.Property(i => i.Monto).IsRequired(true);
                entity.Property(i => i.Fecha).IsRequired(true);
                entity.HasOne(i => i.Categoria)
                .WithMany(c => c.Ingresos)
                .HasForeignKey(i => i.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Gasto>(entity =>{
                entity.ToTable("Gasto");
                entity.HasKey(g => g.Id);
                entity.Property(g => g.Concepto).IsRequired(true).HasMaxLength(80);
                entity.Property(g => g.Monto).IsRequired(true);
                entity.Property(g => g.Fecha).IsRequired(true);
                entity.HasOne(g => g.Categoria)
                .WithMany(c => c.Gastos)
                .HasForeignKey(g => g.CategoriaId)
                .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Categoria>(entity =>{
                entity.ToTable("Categoria");
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Nombre).IsRequired(true).HasMaxLength(80);
                entity.Property(c => c.Tipo).IsRequired(true);
                entity.Property(c => c.Presupuesto).IsRequired(false);
            });
        }
    }
}