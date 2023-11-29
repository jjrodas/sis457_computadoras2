using System;
using System.Collections.Generic;

namespace WebComputadoras2.Models;

public partial class Ventum
{
    public int Id { get; set; }

    public int IdUsuario { get; set; }

    public DateTime Fecha { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Usuario IdUsuarioNavigation { get; set; } = null!;

    public virtual ICollection<VentaDetalle> VentaDetalles { get; set; } = new List<VentaDetalle>();
}
