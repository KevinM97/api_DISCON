using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("productos")]
    public partial class Productos
    {
        [Key]
        [Column("ID_PRODUCTO", TypeName = "int(11)")]
        public int IdProducto { get; set; }
        [Column("NOMBRE_PRODUCTO", TypeName = "varchar(50)")]
        public string NombreProducto { get; set; }
        [Column("PRECIO_PRODUCTO", TypeName = "decimal(10,2)")]
        public decimal? PrecioProducto { get; set; }
        [Column("DESCRIPCION_PROD", TypeName = "varchar(500)")]
        public string DescripcionProd { get; set; }
        [Column("ESTADO_PROD")]
        public bool? EstadoProd { get; set; }
    }
}
