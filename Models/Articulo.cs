using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("articulo")]
    public partial class Articulo
    {
        [Key]
        [Column("ID_ARTICULO", TypeName = "int(11)")]
        public int IdArticulo { get; set; }
        [Column("TITULO_ARTICULO", TypeName = "varchar(50)")]
        public string TituloArticulo { get; set; }
        [Column("IMAGEN_ARTICULO", TypeName = "varchar(50)")]
        public string ImagenArticulo { get; set; }
        [Column("TEXTO_ARTICULO", TypeName = "varchar(10000)")]
        public string TextoArticulo { get; set; }
        [Column("ESTADO_ARTICULO")]
        public bool? EstadoArticulo { get; set; }
    }
}
