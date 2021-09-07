using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("clasemodulo")]
    public partial class Clasemodulo
    {
        [Key]
        [Column("ID_CLASMOD", TypeName = "int(11)")]
        public int IdClasmod { get; set; }
        [Column("ID_PREGUNTA", TypeName = "int(11)")]
        public int? IdPregunta { get; set; }
        [Column("NOMBRE_CLASMOD", TypeName = "varchar(50)")]
        public string NombreClasmod { get; set; }
        [Column("VIDEO_CLASMOD", TypeName = "varchar(50)")]
        public string VideoClasmod { get; set; }
        [Column("VALOR_CLASMOD")]
        public bool? ValorClasmod { get; set; }
        [Column("ESTADO_CLASMOD", TypeName = "char(10)")]
        public string EstadoClasmod { get; set; }
    }
}
