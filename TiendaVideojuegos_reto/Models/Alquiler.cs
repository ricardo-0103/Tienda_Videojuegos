using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace TiendaVideojuegos_reto.Models
{
    [Table("Alquiler")]
    public partial class Alquiler
    {
        [Key]
        [Column("idAlquiler")]
        public int IdAlquiler { get; set; }
        [Column("idCliente")]
        public int IdCliente { get; set; }
        [Column("idJuego")]
        public int IdJuego { get; set; }
        [Column("fecha_ini", TypeName = "date")]
        public DateTime FechaIni { get; set; }
        [Column("fecha_dev", TypeName = "date")]
        public DateTime FechaDev { get; set; }
        [Column("precio_juego")]
        public int? PrecioJuego { get; set; }
        [Column("precio_total")]
        public int? PrecioTotal { get; set; }
        [Column("estado")]
        public bool? Estado { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(Cliente.Alquilers))]
        public virtual Cliente? IdClienteNavigation { get; set; } = null!;
        [JsonIgnore]
        [ForeignKey(nameof(IdJuego))]
        [InverseProperty(nameof(Videojuego.Alquilers))]
        public virtual Videojuego? IdJuegoNavigation { get; set; } = null!;
    }
}
