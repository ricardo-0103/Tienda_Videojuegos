using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TiendaVideojuegos_reto.Models
{
    [Table("Cliente")]
    public partial class Cliente
    {
        public Cliente()
        {
            Alquilers = new HashSet<Alquiler>();
        }

        [Key]
        [Column("idCliente")]
        public int IdCliente { get; set; }
        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [Column("apellidos")]
        [StringLength(50)]
        [Unicode(false)]
        public string Apellidos { get; set; } = null!;
        [Column("fecha_nacimiento", TypeName = "date")]
        public DateTime FechaNacimiento { get; set; }
        [Column("cedula")]
        [StringLength(50)]
        [Unicode(false)]
        public string Cedula { get; set; } = null!;
        [Column("telefono")]
        [StringLength(50)]
        [Unicode(false)]
        public string Telefono { get; set; } = null!;
        [Column("email")]
        [StringLength(50)]
        [Unicode(false)]
        public string Email { get; set; } = null!;

        [JsonIgnore]
        [InverseProperty(nameof(Alquiler.IdClienteNavigation))]
        public virtual ICollection<Alquiler> Alquilers { get; set; }
    }
}
