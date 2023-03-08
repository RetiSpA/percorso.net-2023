using Esercizio5.Models;

namespace Esercizio5.DAL.Interfaces
{
    public interface IPhoneService
    {
        public IEnumerable<Phone> GetPhones();

        public IEnumerable<Phone> GetPhones(Func<Phone, bool> filter);

        public Phone GetPhoneById(int id);

        public void AddPhone(Phone phone);

        public void DeletePhone(Phone phone);
    }
}