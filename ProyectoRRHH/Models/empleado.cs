using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class empleado
{
    public int id { get; set; }

    public string cedula { get; set; }

    public string nombre { get; set; }

    public DateOnly? fechaingreso { get; set; }

    public string departamento { get; set; }

    public string puesto { get; set; }

    public string salariomensual { get; set; }

    public bool? estado { get; set; }

    public virtual candidato cedulaNavigation { get; set; }

    public virtual departamento departamentoNavigation { get; set; }

    public virtual puesto puestoNavigation { get; set; }
}
