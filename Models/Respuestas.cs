using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("respuestas")]
    public partial class Respuestas
    {
        [Key]
        [Column("ID_RESPUESTA", TypeName = "int(11)")]
        public int IdRespuesta { get; set; }
        [Column("TITULO_RESPUESTA", TypeName = "varchar(50)")]
        public string TituloRespuesta { get; set; }
        [Column("VALOR_RESPUESTA")]
        public bool? ValorRespuesta { get; set; }
        [Column("ESTADO_RESPUESTA")]
        public bool? EstadoRespuesta { get; set; }
    }
}
