using PetShops.Models;

namespace PetShops.Interfaces
{
    public interface IShoppingCartRepository
    {
        void AddToCart(Product product);
        int RemoveFromCart(Product product);
        List<Cart> GetAllShoppingCartItems();
        void ClearCart();
        decimal GetShoppingCartTotal();
        public List<Cart> ShoppingCartItems { get; set; }
    }
}
