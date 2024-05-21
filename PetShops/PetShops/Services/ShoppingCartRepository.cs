using Microsoft.EntityFrameworkCore;
using PetShops.Data;
using PetShops.Interfaces;
using PetShops.Models;

namespace PetShops.Services
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private PetShopDbContext dbContext;
        public ShoppingCartRepository(PetShopDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Cart>? ShoppingCartItems { get; set; }
        public string? ShoppingCartId { set; get; }
        public static ShoppingCartRepository GetCart(IServiceProvider services)
        {
            ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;
            PetShopDbContext context = services.GetService<PetShopDbContext>() ?? throw new Exception("Error initializing PetShopDbContext");
            string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString(); session?.SetString("CartId", cartId);
            return new ShoppingCartRepository(context) { ShoppingCartId = cartId };
        }
        public void AddToCart(Product product)
        {
            var shoppingCartItem = dbContext.Carts.FirstOrDefault(s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new Cart
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Qty = 1,
                };
                dbContext.Carts.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Qty++;
            }
            dbContext.SaveChanges();
        }
        public void ClearCart()
        {
            var cartItems = dbContext.Carts.Where(s => s.ShoppingCartId == ShoppingCartId);
            dbContext.Carts.RemoveRange(cartItems);
            dbContext.SaveChanges();
        }
        public List<Cart> GetAllShoppingCartItems()
        {
            return ShoppingCartItems ??= dbContext.Carts.Where(s => s.ShoppingCartId == ShoppingCartId).Include(p => p.Product).ToList();
        }
        public decimal GetShoppingCartTotal()
        {
            var totalCost = dbContext.Carts.Where(s => s.ShoppingCartId == ShoppingCartId).Select(s => s.Product.ProductPrice * s.Qty).Sum();
            return (decimal)totalCost;
        }
        public int RemoveFromCart(Product product)
        {
            var shoppingCartItem = dbContext.Carts.FirstOrDefault(s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);
            var quantity = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Qty > 1)
                {
                    shoppingCartItem.Qty--;
                    quantity = shoppingCartItem.Qty;
                }
                else
                {
                    dbContext.Carts.Remove(shoppingCartItem);
                }
            }
            dbContext.SaveChanges();
            return quantity;
        }


    }
}