using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace api_DISCON.Models
{
    [Table("credenciales")]
    public partial class Credenciales
    {
        [Key]
        [Column("ID_CREDEN", TypeName = "int(11)")]
        public int IdCreden { get; set; }

        [Column("USERNAME_CREDEN", TypeName = "varchar(20)")]
        public string UsernameCreden { get; set; }

        [Column("PASSWORD_CREDEN", TypeName = "varchar(300)")]
        public string PasswordCreden { get; set; }

        [Column("SALT_PASSWORD", TypeName = "varchar(500)")]
        public string SaltPassword { get; set; }
    }
}
