using PetShops.Models;
using PetShops.DTOs;

namespace PetShops.Interfaces
{
    public interface IContactRepository
    {
        public void AddContact(ContactDTO contact);
        IEnumerable<Contact> GetAllContacts();
        IEnumerable<EmailSubscribe> GetAllSubscriber();
    }
}
