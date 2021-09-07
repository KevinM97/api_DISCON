using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_DISCON.Models
{
    public class infoUser
    {
        public int idCreden { get; set; }
        public int IdUsuario { get; set; }
        public string username { get; set; }
        public string NombreUsuario { get; set; }
        public string EmailUsuario { get; set; }
        public string EstadoUsuario { get; set; }
        public string tokenUsuario { get; set; }
    }
}
