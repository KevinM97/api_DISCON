using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("usuarios")]
    public partial class Usuarios
    {
        [Key]
        [Column("ID_USUARIO", TypeName = "int(11)")]
        public int IdUsuario { get; set; }
        [Column("ID_CREDEN", TypeName = "int(11)")]
        public int? IdCreden { get; set; }
        [Column("NOMBRE_USUARIO", TypeName = "varchar(50)")]
        public string NombreUsuario { get; set; }
        [Column("EMAIL_USUARIO", TypeName = "varchar(50)")]
        public string EmailUsuario { get; set; }
        [Column("ESTADO_USUARIO", TypeName = "varchar(50)")]
        public string EstadoUsuario { get; set; }
    }
}
