using System.ComponentModel.DataAnnotations;

namespace Apianimales.Models
{
    public class Animal
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Especie { get; set; }

        [Required]
        public string Habitat { get; set; }

        [Required]
        public string Continente { get; set; }

        [Required]
        public string TipoDieta { get; set; }

        [Range(0.1, double.MaxValue)]
        public decimal Peso { get; set; }

        public bool EnPeligroExtincion { get; set; }

        public string Foto { get; set; }
    }
}
