using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class idioma
{
    public int id { get; set; }

    public string nombre { get; set; }

    public string nivel { get; set; }

    public virtual ICollection<candidato> candidatos { get; set; } = new List<candidato>();
}
