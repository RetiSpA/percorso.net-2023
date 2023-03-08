using System.ComponentModel.DataAnnotations;

namespace Esercizio5.Models
{
    public class Phone
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Colour { get; set; }

        public decimal Price { get; set; }
    }
}