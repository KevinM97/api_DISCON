using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("productos_tiendas")]
    public partial class ProductosTiendas
    {
        [Key]
        [Column("id_prodtiend", TypeName = "int(11)")]
        public int IdProdtiend { get; set; }
        [Column("ID_TIENDA", TypeName = "int(11)")]
        public int? IdTienda { get; set; }
        [Column("ID_PRODUCTO", TypeName = "int(11)")]
        public int? IdProducto { get; set; }
    }
}
