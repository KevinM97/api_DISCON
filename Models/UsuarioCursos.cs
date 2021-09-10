using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("usuario_cursos")]
    public partial class UsuarioCursos
    {
        [Key]
        [Column("ID_USUCU", TypeName = "int(11)")]
        public int IdUsucu { get; set; }
        [Column("ID_USUARIO", TypeName = "int(11)")]
        public int? IdUsuario { get; set; }
        [Column("ID_CURSO", TypeName = "int(11)")]
        public int? IdCurso { get; set; }
    }
}
