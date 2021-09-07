using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("modulos")]
    public partial class Modulos
    {
        [Key]
        [Column("ID_MODULO", TypeName = "int(11)")]
        public int IdModulo { get; set; }
        [Column("ID_CLASMOD", TypeName = "int(11)")]
        public int? IdClasmod { get; set; }
        [Column("TITULO_MODULO", TypeName = "varchar(50)")]
        public string TituloModulo { get; set; }
        [Column("PROGRESO_MODULO")]
        public bool? ProgresoModulo { get; set; }
        [Column("ESTADO_MODULO")]
        public bool? EstadoModulo { get; set; }
    }
}
