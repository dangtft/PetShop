using Microsoft.EntityFrameworkCore;
using PetShops.Data;
using PetShops.Interfaces;
using PetShops.Models;

namespace PetShops.Services
{
    public class OrderRepository : IOrderRepository
    {
        private PetShopDbContext dbContext;
        private IShoppingCartRepository shoppingCartRepository;
        public OrderRepository(PetShopDbContext dbContext, IShoppingCartRepository shoppingCartRepository)
        {
            this.dbContext = dbContext;
            this.shoppingCartRepository = shoppingCartRepository;
        }

        public void PlaceOrder(Order order)
        {
            var shoppingCartItems = shoppingCartRepository.GetAllShoppingCartItems();
            order.OrderDetails = new List<OrderDetail>();
            foreach (var item in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Quantity = item.Qty,
                    ProductId = item.Product.ProductId,
                    Price = (decimal)item.Product.ProductPrice
                };
                order.OrderDetails.Add(orderDetail);
            }
            order.OrderPlaced = DateTime.Now;
            order.OrderTotal = shoppingCartRepository.GetShoppingCartTotal();
            dbContext.Orders.Add(order);

            dbContext.SaveChanges();
        }
        public IEnumerable<Order> GetAllOrders()
        {
            return dbContext.Orders.ToList();
        }
        public IEnumerable<Order> GetCompletedOrders(string userId)
        {
            return dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .Where(o => o.Email == userId && o.OrderPlaced < DateTime.Now).ToList();

        }
        public Order GetOrderById(int orderId)
        {
            return dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.Id == orderId);
        }

        public void DeleteOrder(int orderId)
        {
            var orderToDelete = dbContext.Orders.Find(orderId);

            if (orderToDelete != null)
            {
                dbContext.Orders.Remove(orderToDelete);
                dbContext.SaveChanges();
            }
        }
    }
}
