using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("secciones")]
    public partial class Secciones
    {
        [Key]
        [Column("ID_SECCION", TypeName = "int(11)")]
        public int IdSeccion { get; set; }
        [Column("NOMBRE_SECCION", TypeName = "varchar(50)")]
        public string NombreSeccion { get; set; }
        [Column("ESTADO_SECCION")]
        public bool? EstadoSeccion { get; set; }
    }
}
