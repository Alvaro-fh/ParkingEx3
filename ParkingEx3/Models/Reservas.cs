using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingEx3.Models
{
    [Table("reservas")]
    public class Reservas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("Usuario")]
        [Column("idusu")]
        public int UsuarioId { get; set; }

        [ForeignKey("Plaza")]
        [Column("idplaza")]
        public int PlazaId { get; set; }

        [Column("fecini")]
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        [Column("fecfin")]
        public DateTime? FechaFin { get; set; }
        [Required]
        [Column("horas")]
        public int Horas { get; set; }

        [Column("precio")]
        public decimal Precio { get; set; }

        [Column("prefin")]
        public decimal? PrecioFinal { get; set; }

        [Column("estado")]
        [StringLength(50)]
        public string? Estado { get; set; }

        public virtual Usuarios? Usuario { get; set; }
        public virtual Plazas? Plaza { get; set; }
    }
}
