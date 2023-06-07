using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class competencia
{
    public int id { get; set; }

    public string descripcion { get; set; }

    public bool? estado { get; set; }

    public virtual ICollection<candidato> candidatos { get; set; } = new List<candidato>();
}
