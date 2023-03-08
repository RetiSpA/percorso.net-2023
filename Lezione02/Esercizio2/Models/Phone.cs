using System.ComponentModel.DataAnnotations;

namespace Esercizio2.Models
{
    public class Phone
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }
    }
}
