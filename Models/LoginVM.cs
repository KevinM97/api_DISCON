using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Models
{
    public class LoginVM
    {
        [Required(ErrorMessage = "El usuario es obligatorio.")]
        public string nombreUsuario { get; set; }
        [Required(ErrorMessage = "El clave es obligatorio.")]
        public string passUsuario { get; set; }
    }
}
