using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("curso")]
    public partial class Curso
    {
        [Key]
        [Column("ID_CURSO", TypeName = "int(11)")]
        public int IdCurso { get; set; }
        [Column("ID_MODULO", TypeName = "int(11)")]
        public int? IdModulo { get; set; }
        [Column("ID_USUARIO", TypeName = "int(11)")]
        public int? IdUsuario { get; set; }
        [Column("NOMBRE_CURSO", TypeName = "varchar(20)")]
        public string NombreCurso { get; set; }
        [Column("DESCRIPCION_CURSO", TypeName = "varchar(20)")]
        public string DescripcionCurso { get; set; }
        [Column("IMAGEN_CURSO", TypeName = "varchar(50)")]
        public string ImagenCurso { get; set; }
        [Column("ESTADO_CURSO")]
        public bool? EstadoCurso { get; set; }
    }
}
