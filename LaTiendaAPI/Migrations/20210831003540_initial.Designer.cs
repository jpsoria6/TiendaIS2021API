﻿// <auto-generated />
using System;
using LaTienda.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace LaTienda.API.Migrations
{
    [DbContext(typeof(TiendaContext))]
    [Migration("20210831003540_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("LaTienda.Model.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CondicionTributaria")
                        .HasColumnType("int");

                    b.Property<string>("Cuit")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Domicilio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("LaTienda.Model.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Colores");
                });

            modelBuilder.Entity("LaTienda.Model.Comprobante", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumeroComprobante")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comprobantes");
                });

            modelBuilder.Entity("LaTienda.Model.Empleado", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Autenticado")
                        .HasColumnType("bit");

                    b.Property<int>("Legajo")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pass")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SucursalId")
                        .HasColumnType("int");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SucursalId");

                    b.ToTable("Empleados");
                });

            modelBuilder.Entity("LaTienda.Model.LineaDeVenta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<double>("Precio")
                        .HasColumnType("float");

                    b.Property<int?>("StockId")
                        .HasColumnType("int");

                    b.Property<int?>("VentaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockId");

                    b.HasIndex("VentaId");

                    b.ToTable("LineaDeVentas");
                });

            modelBuilder.Entity("LaTienda.Model.Marca", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Marcas");
                });

            modelBuilder.Entity("LaTienda.Model.Producto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Codigo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Costo")
                        .HasColumnType("float");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Iva")
                        .HasColumnType("float");

                    b.Property<double>("MargenGanancia")
                        .HasColumnType("float");

                    b.Property<double>("NetoGravado")
                        .HasColumnType("float");

                    b.Property<double>("PrecioVenta")
                        .HasColumnType("float");

                    b.Property<int?>("RubroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RubroId");

                    b.ToTable("Producto");
                });

            modelBuilder.Entity("LaTienda.Model.Rubro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Rubros");
                });

            modelBuilder.Entity("LaTienda.Model.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad")
                        .HasColumnType("int");

                    b.Property<int?>("ColorId")
                        .HasColumnType("int");

                    b.Property<int?>("MarcaId")
                        .HasColumnType("int");

                    b.Property<int?>("ProductoId")
                        .HasColumnType("int");

                    b.Property<int?>("SucursalId")
                        .HasColumnType("int");

                    b.Property<int?>("TalleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ColorId");

                    b.HasIndex("MarcaId");

                    b.HasIndex("ProductoId");

                    b.HasIndex("SucursalId");

                    b.HasIndex("TalleId");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("LaTienda.Model.Sucursal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TiendaId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TiendaId");

                    b.ToTable("Sucursal");
                });

            modelBuilder.Entity("LaTienda.Model.Talle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Talles");
                });

            modelBuilder.Entity("LaTienda.Model.Tienda", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CondicionTributaria")
                        .HasColumnType("int");

                    b.Property<string>("DomicilioLegal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RazonSocial")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tiendas");
                });

            modelBuilder.Entity("LaTienda.Model.Venta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmpleadoId")
                        .HasColumnType("int");

                    b.Property<int?>("FacturaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaHora")
                        .HasColumnType("datetime2");

                    b.Property<double>("Monto")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("EmpleadoId");

                    b.HasIndex("FacturaId");

                    b.ToTable("Ventas");
                });

            modelBuilder.Entity("LaTienda.Model.Empleado", b =>
                {
                    b.HasOne("LaTienda.Model.Sucursal", null)
                        .WithMany("Empleados")
                        .HasForeignKey("SucursalId");
                });

            modelBuilder.Entity("LaTienda.Model.LineaDeVenta", b =>
                {
                    b.HasOne("LaTienda.Model.Stock", "Stock")
                        .WithMany()
                        .HasForeignKey("StockId");

                    b.HasOne("LaTienda.Model.Venta", null)
                        .WithMany("LineaDeVentas")
                        .HasForeignKey("VentaId");

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("LaTienda.Model.Producto", b =>
                {
                    b.HasOne("LaTienda.Model.Rubro", "Rubro")
                        .WithMany("Productos")
                        .HasForeignKey("RubroId");

                    b.Navigation("Rubro");
                });

            modelBuilder.Entity("LaTienda.Model.Stock", b =>
                {
                    b.HasOne("LaTienda.Model.Color", "Color")
                        .WithMany()
                        .HasForeignKey("ColorId");

                    b.HasOne("LaTienda.Model.Marca", "Marca")
                        .WithMany()
                        .HasForeignKey("MarcaId");

                    b.HasOne("LaTienda.Model.Producto", "Producto")
                        .WithMany()
                        .HasForeignKey("ProductoId");

                    b.HasOne("LaTienda.Model.Sucursal", "Sucursal")
                        .WithMany()
                        .HasForeignKey("SucursalId");

                    b.HasOne("LaTienda.Model.Talle", "Talle")
                        .WithMany()
                        .HasForeignKey("TalleId");

                    b.Navigation("Color");

                    b.Navigation("Marca");

                    b.Navigation("Producto");

                    b.Navigation("Sucursal");

                    b.Navigation("Talle");
                });

            modelBuilder.Entity("LaTienda.Model.Sucursal", b =>
                {
                    b.HasOne("LaTienda.Model.Tienda", null)
                        .WithMany("Sucursales")
                        .HasForeignKey("TiendaId");
                });

            modelBuilder.Entity("LaTienda.Model.Venta", b =>
                {
                    b.HasOne("LaTienda.Model.Empleado", "Empleado")
                        .WithMany("Ventas")
                        .HasForeignKey("EmpleadoId");

                    b.HasOne("LaTienda.Model.Comprobante", "Factura")
                        .WithMany()
                        .HasForeignKey("FacturaId");

                    b.Navigation("Empleado");

                    b.Navigation("Factura");
                });

            modelBuilder.Entity("LaTienda.Model.Empleado", b =>
                {
                    b.Navigation("Ventas");
                });

            modelBuilder.Entity("LaTienda.Model.Rubro", b =>
                {
                    b.Navigation("Productos");
                });

            modelBuilder.Entity("LaTienda.Model.Sucursal", b =>
                {
                    b.Navigation("Empleados");
                });

            modelBuilder.Entity("LaTienda.Model.Tienda", b =>
                {
                    b.Navigation("Sucursales");
                });

            modelBuilder.Entity("LaTienda.Model.Venta", b =>
                {
                    b.Navigation("LineaDeVentas");
                });
#pragma warning restore 612, 618
        }
    }
}
