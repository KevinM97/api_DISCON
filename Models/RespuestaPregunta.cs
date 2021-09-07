using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("respuesta_pregunta")]
    public partial class RespuestaPregunta
    {
        [Column("ID_RESPUESTA", TypeName = "int(11)")]
        public int? IdRespuesta { get; set; }
        [Column("ID_PREGUNTA", TypeName = "int(11)")]
        public int? IdPregunta { get; set; }
    }
}
