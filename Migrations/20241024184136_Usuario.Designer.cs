﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace SubastaWeb.Migrations
{
    [DbContext(typeof(SubastaWebContext))]
    [Migration("20241024184136_Usuario")]
    partial class Usuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SubastaWeb.Models.Producto.Producto", b =>
                {
                    b.Property<int>("IdProducto")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdProducto"));

                    b.Property<string>("Categoria")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EnLiquidacion")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaCaducidad")
                        .HasColumnType("datetime2");

                    b.Property<string>("ImgUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("PrecioFinal")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<decimal>("PrecioInicial")
                        .HasColumnType("decimal(10, 2)");

                    b.Property<string>("ProductoTipo")
                        .IsRequired()
                        .HasMaxLength(21)
                        .HasColumnType("nvarchar(21)");

                    b.Property<string>("TipoDeSubasta")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProducto");

                    b.ToTable("Productos");

                    b.HasDiscriminator<string>("ProductoTipo").HasValue("Producto");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("SubastaWeb.Models.Producto.ProductoAlimento", b =>
                {
                    b.HasBaseType("SubastaWeb.Models.Producto.Producto");

                    b.HasDiscriminator().HasValue("Alimento");
                });

            modelBuilder.Entity("SubastaWeb.Models.Producto.ProductoElectronica", b =>
                {
                    b.HasBaseType("SubastaWeb.Models.Producto.Producto");

                    b.HasDiscriminator().HasValue("Electronica");
                });

            modelBuilder.Entity("SubastaWeb.Models.Producto.ProductoModelDBO", b =>
                {
                    b.HasBaseType("SubastaWeb.Models.Producto.Producto");

                    b.HasDiscriminator().HasValue("ProductoModelDBO");
                });

            modelBuilder.Entity("SubastaWeb.Models.Producto.ProductoRopa", b =>
                {
                    b.HasBaseType("SubastaWeb.Models.Producto.Producto");

                    b.HasDiscriminator().HasValue("Ropa");
                });
#pragma warning restore 612, 618
        }
    }
}
