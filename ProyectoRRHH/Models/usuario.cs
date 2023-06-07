using System;
using System.Collections.Generic;

namespace ProyectoRRHH.Models;

public partial class usuario
{
    public int id { get; set; }

    public string email { get; set; }

    public string emailnormalizado { get; set; }

    public string password { get; set; }
}
