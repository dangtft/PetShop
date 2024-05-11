using PetShops.Models;

namespace PetShops.Interfaces
{
    public interface IOrderRepository
    {
        void PlaceOrder(Order order);

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetCompletedOrders(string userId);
        public Order GetOrderById(int orderId);
        public void DeleteOrder(int orderId);
    }
}
