using PetShopsMVC.DTOs;
using PetShopsMVC.Models;

namespace PetShopsMVC.ViewModel
{
    public class HomeVM
    {
        public IEnumerable<Products> Products { get; set; }
        public Products Product { get; set; }
        public IEnumerable<Blogs> Blogs { get; set; }
        public Blogs Blog { get; set; }
        public RegisterRequestDTO Register { get; set; }
    }

    public class BlogVM
    {
        public IEnumerable<Blogs> Blogs { get; set; }
        public Blogs Blog { set; get; }
    }
    public class ContactSubscriberViewModel
    {
        public decimal TotalRevenue { get; set; }
        public int TotalUsers { get; set; }
        public List<Contacts> Contacts { get; set; }
        public List<EmailSubscribe> Subscribers { get; set; }
        public int TotalQuantityInOrderDetails { get; set; }
    }

}
