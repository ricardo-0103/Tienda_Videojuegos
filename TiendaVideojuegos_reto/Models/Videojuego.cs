using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TiendaVideojuegos_reto.Models
{
    [Table("Videojuego")]
    public partial class Videojuego
    {
        public Videojuego()
        {
            Alquilers = new HashSet<Alquiler>();
            PrecioVideojuegos = new HashSet<PrecioVideojuego>();
        }

        [Key]
        [Column("idJuego")]
        public int IdJuego { get; set; }
        [Column("nombre")]
        [StringLength(50)]
        [Unicode(false)]
        public string Nombre { get; set; } = null!;
        [Column("año")]
        public int Año { get; set; }
        [Column("protagonistas")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Protagonistas { get; set; }
        [Column("director")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Director { get; set; }
        [Column("productor")]
        [StringLength(50)]
        [Unicode(false)]
        public string? Productor { get; set; }
        [Column("plataforma")]
        public int Plataforma { get; set; }

        [JsonIgnore]
        [InverseProperty(nameof(Alquiler.IdJuegoNavigation))]
        public virtual ICollection<Alquiler> Alquilers { get; set; }
        [JsonIgnore]
        [InverseProperty(nameof(PrecioVideojuego.IdJuegoNavigation))]
        public virtual ICollection<PrecioVideojuego> PrecioVideojuegos { get; set; }
    }
}
