using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebComputadoras2.Models;

public partial class FinalComputadoras2Context : DbContext
{
    public FinalComputadoras2Context()
    {
    }

    public FinalComputadoras2Context(DbContextOptions<FinalComputadoras2Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Compra> Compras { get; set; }

    public virtual DbSet<CompraDetalle> CompraDetalles { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedor> Proveedors { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VentaDetalle> VentaDetalles { get; set; }

    public virtual DbSet<Ventum> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=FinalComputadoras2;User ID=usrComputadoras2F;Password=C0MPUMUND0");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Categori__3213E83F372399B7");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");
        });

        modelBuilder.Entity<Compra>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Compra__3213E83FD9930A8C");

            entity.ToTable("Compra");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdProveedor).HasColumnName("idProveedor");
            entity.Property(e => e.Transaccion).HasColumnName("transaccion");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.Compras)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Compra_Proveedor");
        });

        modelBuilder.Entity<CompraDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CompraDe__3213E83F936AABB3");

            entity.ToTable("CompraDetalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("cantidad");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCompra).HasColumnName("idCompra");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("precioUnitario");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CompraDetalle_Compra");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.CompraDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_CompraDetalle_Producto");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Marca__3213E83FD5F4157D");

            entity.ToTable("Marca");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Producto__3213E83F2279C5EE");

            entity.ToTable("Producto");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(700)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdCategoria).HasColumnName("idCategoria");
            entity.Property(e => e.IdMarca).HasColumnName("idMarca");
            entity.Property(e => e.PrecioVenta).HasColumnName("precioVenta");
            entity.Property(e => e.Stock).HasColumnName("stock");
            entity.Property(e => e.UrlImagen)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("urlImagen");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdCategoriaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoria)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Producto_Categoria");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Producto_Marca");
        });

        modelBuilder.Entity<Proveedor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Proveedo__3213E83FA8431BB0");

            entity.ToTable("Proveedor");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nit).HasColumnName("nit");
            entity.Property(e => e.RazonSocial)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("razonSocial");
            entity.Property(e => e.Representante)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("representante");
            entity.Property(e => e.Telefono)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3213E83F940FD43E");

            entity.ToTable("Rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.Nombre)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuario__3213E83FCF5849DB");

            entity.ToTable("Usuario");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Email)
                .HasMaxLength(70)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaNacimiento)
                .HasColumnType("date")
                .HasColumnName("fechaNacimiento");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdRol).HasColumnName("idRol");
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("usuario");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Usuario_Rol");
        });

        modelBuilder.Entity<VentaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__VentaDet__3213E83F7DDB3D6F");

            entity.ToTable("VentaDetalle");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.FechaVenta)
                .HasColumnType("date")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.IdProducto).HasColumnName("idProducto");
            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("total");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VentaDetalle_Producto");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.VentaDetalles)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_VentaDetalle_Venta");
        });

        modelBuilder.Entity<Ventum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Venta__3213E83FA327473B");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estado)
                .HasDefaultValueSql("((1))")
                .HasColumnName("estado");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date")
                .HasColumnName("fecha");
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("fechaRegistro");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.UsuarioRegistro)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(suser_name())")
                .HasColumnName("usuarioRegistro");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Venta_Usuario");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
