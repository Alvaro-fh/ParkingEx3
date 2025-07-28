using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ParkingEx3.Models
{
    [Table("plazas")]
    public class Plazas
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("estado")]
        [StringLength(100)]
        public string Estado { get; set; }

        public ICollection<Reservas> Reservas { get; set; }
    }
}
