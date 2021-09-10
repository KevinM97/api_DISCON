using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("modulo_clasemod")]
    public partial class ModuloClasemod
    {
        [Key]
        [Column("ID_MODCLAS", TypeName = "int(11)")]
        public int IdModclas { get; set; }
        [Column("ID_MODULO", TypeName = "int(11)")]
        public int? IdModulo { get; set; }
        [Column("ID_CLASMOD", TypeName = "int(11)")]
        public int? IdClasmod { get; set; }
    }
}
