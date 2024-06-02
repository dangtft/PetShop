using PetShops.DTOs;
using PetShops.Models;

namespace PetShops.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task<Product> GetProductById(int id);
        Task<bool> AddProduct(ProductDTO productDTO);
        Task<bool> UpdateProduct(int id, ProductDTO productDTO);
        Task<bool> DeleteProduct(int id);
        Task<bool> AddEmailSubscription(EmailSubscribeDTO email);
        IEnumerable<Product> SearchProductsByName(string productName);
    }
}
