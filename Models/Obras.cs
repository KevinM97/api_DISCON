using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("obras")]
    public partial class Obras
    {
        [Key]
        [Column("ID_OBRA", TypeName = "int(11)")]
        public int IdObra { get; set; }
        [Column("ID_IMAGENOBRA", TypeName = "int(11)")]
        public int IdImagenobra { get; set; }
        [Column("TITULO_OBRA", TypeName = "varchar(50)")]
        public string TituloObra { get; set; }
    }
}
