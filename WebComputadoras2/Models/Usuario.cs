using System;
using System.Collections.Generic;

namespace WebComputadoras2.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public int IdRol { get; set; }

    public string Nombres { get; set; } = null!;

    public string Apellidos { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Clave { get; set; } = null!;

    public string? Telefono { get; set; }

    public DateTime? FechaNacimiento { get; set; }

    public string UsuarioRegistro { get; set; } = null!;

    public DateTime FechaRegistro { get; set; }

    public short Estado { get; set; }

    public virtual Rol IdRolNavigation { get; set; } = null!;

    public virtual ICollection<Ventum> Venta { get; set; } = new List<Ventum>();
}
