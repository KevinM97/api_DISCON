using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("imagenes_obra")]
    public partial class ImagenesObra
    {
        [Key]
        [Column("ID_IMAGENOBRA", TypeName = "int(11)")]
        public int IdImagenobra { get; set; }
        [Required]
        [Column("URL_IMAGENOBRA", TypeName = "varchar(50)")]
        public string UrlImagenobra { get; set; }
    }
}
