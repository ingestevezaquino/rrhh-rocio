using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class puesto
{
    public int id { get; set; }

    public string nombre { get; set; }

    public string nivelriesgo { get; set; }

    public string salariomin { get; set; }

    public string salariomax { get; set; }

    public bool? estado { get; set; }

    public virtual ICollection<candidato> candidatos { get; set; } = new List<candidato>();

    public virtual ICollection<empleado> empleados { get; set; } = new List<empleado>();
}
