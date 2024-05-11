using PetShops.DTOs;
using PetShops.Models;

namespace PetShops.Interfaces
{
    public interface IBlogRepository
    {
        Task<List<BlogPost>> GetBlogsAsync();
        Task<BlogPost> GetBlogById(int id);
        Task<bool> AddBlog(BlogDTO blogDTO);
        Task<bool> UpdateBlog(int id, BlogDTO blogDTO);
        Task<bool> DeleteBlog(int id);
    }
}
