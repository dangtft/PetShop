using Microsoft.AspNetCore.Mvc;
using PetShops.Interfaces;
using PetShops.Models;
using PetShops.DTOs;
using System.Collections.Generic;

namespace PetShops.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        [HttpPost]
        public IActionResult AddContact([FromBody] ContactDTO contact)
        {
            if (contact == null)
            {
                return BadRequest("Contact is null.");
            }

            _contactRepository.AddContact(contact);
            return Ok("Contact added successfully.");
        }

        [HttpGet]
        public ActionResult<IEnumerable<Contact>> GetAllContacts()
        {
            var contacts = _contactRepository.GetAllContacts();
            return Ok(contacts);
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmailSubscribe>> GetAllSubscribers()
        {
            var subscribers = _contactRepository.GetAllSubscriber();
            return Ok(subscribers);
        }
    }
}
