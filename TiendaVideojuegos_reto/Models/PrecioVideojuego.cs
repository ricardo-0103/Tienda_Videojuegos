using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TiendaVideojuegos_reto.Models
{
    [Table("Precio_videojuego")]
    public partial class PrecioVideojuego
    {
        [Key]
        [Column("idPrecio")]
        public int IdPrecio { get; set; }
        [Column("idJuego")]
        public int IdJuego { get; set; }
        [Column("precio_dia")]
        public int PrecioDia { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdJuego))]
        [InverseProperty(nameof(Videojuego.PrecioVideojuegos))]
        public virtual Videojuego? IdJuegoNavigation { get; set; } = null!;
    }
}
