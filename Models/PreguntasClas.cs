using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("preguntas_clas")]
    public partial class PreguntasClas
    {
        [Key]
        [Column("ID_PREGUNTA", TypeName = "int(11)")]
        public int IdPregunta { get; set; }
        [Column("ID_RESPUESTA", TypeName = "int(11)")]
        public int IdRespuesta { get; set; }

        [Column("TITULO_PREGUNTA", TypeName = "varchar(100)")]
        public string TituloPregunta { get; set; }
        [Column("VALOR_PREGUNTA")]
        public bool ValorPregunta { get; set; }
    }
}
