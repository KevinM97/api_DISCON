using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("curso_modulo")]
    public partial class CursoModulo
    {
        [Key]
        [Column("ID_CURSOMOD", TypeName = "int(11)")]
        public int IdCursomod { get; set; }
        [Column("ID_MODULO", TypeName = "int(11)")]
        public int? IdModulo { get; set; }
        [Column("ID_CURSO", TypeName = "int(11)")]
        public int? IdCurso { get; set; }
    }
}
