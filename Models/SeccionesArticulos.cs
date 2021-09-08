using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("secciones_articulos")]
    public partial class SeccionesArticulos
    {
        [Key]
        [Column("id_seccart", TypeName = "int(11)")]
        public int IdSeccart { get; set; }
        [Column("ID_ARTICULO", TypeName = "int(11)")]
        public int? IdArticulo { get; set; }
        [Column("ID_SECCION", TypeName = "int(11)")]
        public int? IdSeccion { get; set; }
    }
}
