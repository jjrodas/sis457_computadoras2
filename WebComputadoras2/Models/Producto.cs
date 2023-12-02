using System;
using System.Collections.Generic;

namespace WebComputadoras2.Models;

public partial class Producto
{
    public int Id { get; set; }

    public int IdCategoria { get; set; }

    public int IdMarca { get; set; }

    public string Descripcion { get; set; } = null!;

    public string UrlImagen { get; set; } = null!;

    public int PrecioVenta { get; set; }

    public int Stock { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual ICollection<CompraDetalle> CompraDetalles { get; set; } = new List<CompraDetalle>();

    public virtual Categorium IdCategoriaNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
