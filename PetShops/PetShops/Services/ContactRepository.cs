using Microsoft.EntityFrameworkCore;
using PetShops.Data;
using PetShops.Interfaces;
using PetShops.Models;
using PetShops.DTOs;
namespace PetShops.Services
{
    public class ContactRepository : IContactRepository
    {
        private readonly PetShopDbContext _dbContext;
        public ContactRepository(PetShopDbContext context)
        {
            _dbContext = context;
        }
        public void AddContact(ContactDTO contactDto)
        {
            try
            {
                var contact = new Contact
                {
                    Name = contactDto.Name,
                    Email = contactDto.Email,
                    Message = contactDto.Message
                };

                _dbContext.Contacts.Add(contact);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the contact", ex);
            }
        }

        public IEnumerable<Contact> GetAllContacts()
        {
            return _dbContext.Contacts.ToList();
        }
        public IEnumerable<EmailSubscribe> GetAllSubscriber()
        {
            return _dbContext.EmailSubscribe.ToList();
        }
    }
}
