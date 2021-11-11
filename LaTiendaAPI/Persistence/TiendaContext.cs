using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaTienda.Model;
using Microsoft.EntityFrameworkCore;

namespace LaTienda.API.Persistence
{
    public class TiendaContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Talle> Talles { get; set; }
        public DbSet<Color> Colores { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<Comprobante> Comprobantes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<LineaDeVenta> LineaDeVentas { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Producto> Productos { get; set; }


        public TiendaContext(DbContextOptions<TiendaContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



        }
    }
}