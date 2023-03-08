using Esercizio2.Models;

namespace Esercizio2.Utility
{
    public class StoreWarehouse
    {
        public static List<Phone> Phones { get; set; }

        static StoreWarehouse()
        {
            Phones = new List<Phone>
            {
                new Phone
                {
                    Id = 1,
                    Color = "White",
                    Description = "Fast just got faster with Nexus S.",
                    Name = "Nexus S",
                    Price = 700
                },

                new Phone
                {
                    Id = 2,
                    Color = "Black",
                    Description = "The Next, Next Generation phone.",
                    Name = "MOTOROLA XOOM™",
                    Price = 150
                },

                new Phone
                {
                    Id = 3,
                    Color = "Gray",
                    Description = "Iphone 7, something special",
                    Name = "IPhone 7",
                    Price = 800

                },

                new Phone
                {
                    Id = 4,
                    Color = "Gray",
                    Description = "Free your smartphone",
                    Name = "Galaxy S8",
                    Price = 740
                }
            };
        }
    }
}
