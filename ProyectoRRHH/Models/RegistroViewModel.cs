using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using Xunit.Sdk;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ProyectoRRHH.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage="El campo {0} es requerido")]
        [EmailAddress(ErrorMessage ="El campo debe ser un correo electronico válido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [DataType(DataType.Password)]
        public string Passsword { get; set; }
    }
}
