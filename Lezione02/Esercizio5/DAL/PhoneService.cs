using Esercizio5.DAL.Interfaces;
using Esercizio5.Models;

namespace Esercizio5.DAL
{
    public class PhoneService : IPhoneService
    {
        private List<Phone> Phones { get; set; }

        public PhoneService()
        {
            Phones = new List<Phone>
            {
                new Phone
                {
                    Id = 1,
                    Colour = "White",
                    Description = "Fast just got faster with Nexus S.",
                    Name = "Nexus S",
                    Price = 700
                },
                new Phone
                {
                    Id = 2,
                    Colour = "Black",
                    Description = "The Next, Next Generation phone.",
                    Name = "MOTOROLA XOOM™",
                    Price = 150
                },
                new Phone
                {
                    Id = 3,
                    Colour = "Gray",
                    Description = "Iphone 7, something special",
                    Name = "IPhone 7",
                    Price = 800
                },
                new Phone
                {
                    Id = 4,
                    Colour = "Gray",
                    Description = "Free your smartphone",
                    Name = "Galaxy S8",
                    Price = 740
                }
            };
        }

        public IEnumerable<Phone> GetPhones()
        {
            return Phones;
        }

        public IEnumerable<Phone> GetPhones(Func<Phone, bool> filter)
        {
            return Phones.Where(filter);
        }

        public Phone GetPhoneById(int id)
        {
            return Phones.FirstOrDefault(r => r.Id == id);
        }

        public void AddPhone(Phone phone)
        {
            Phones.Add(phone);
        }

        public void DeletePhone(Phone phone)
        {
            Phones.Remove(phone);
        }
    }
}