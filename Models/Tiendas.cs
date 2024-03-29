﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("tiendas")]
    public partial class Tiendas
    {
        [Key]
        [Column("ID_TIENDA", TypeName = "int(11)")]
        public int IdTienda { get; set; }
        [Column("NOMBRE_TIENDA", TypeName = "varchar(50)")]
        public string NombreTienda { get; set; }
        [Column("ESTADO_TIENDA")]
        public bool? EstadoTienda { get; set; }
    }
}
