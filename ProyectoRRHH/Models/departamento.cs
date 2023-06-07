using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class departamento
{
    public int id { get; set; }

    public string departamento1 { get; set; }

    public virtual ICollection<candidato> candidatos { get; set; } = new List<candidato>();

    public virtual ICollection<empleado> empleados { get; set; } = new List<empleado>();
}
