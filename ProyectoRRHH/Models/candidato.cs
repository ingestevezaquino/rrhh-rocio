using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class candidato
{
    public int id { get; set; }

    public string cedula { get; set; }

    public string nombre { get; set; }

    public string puestoaspira { get; set; }

    public string departamento { get; set; }

    public string salarioaspira { get; set; }

    public string explaboral { get; set; }

    public string empresa { get; set; }

    public string puestoocupado { get; set; }

    public DateOnly? fechadesde { get; set; }

    public DateOnly? fechahasta { get; set; }

    public string salario { get; set; }

    public string recomendadopor { get; set; }

    public virtual ICollection<capacitacione> capacitaciones { get; set; } = new List<capacitacione>();

    public virtual departamento departamentoNavigation { get; set; }

    public virtual empleado empleado { get; set; }

    public virtual puesto puestoaspiraNavigation { get; set; }

    public virtual ICollection<competencia> competencia { get; set; } = new List<competencia>();

    public virtual ICollection<idioma> idiomas { get; set; } = new List<idioma>();
}
