using Microsoft.EntityFrameworkCore;
using PetShops.Data;
using PetShops.DTOs;
using PetShops.Interfaces;
using PetShops.Models;
namespace PetShops.Services
{
    public class BlogRepository : IBlogRepository
    {
        private readonly PetShopDbContext _context;

        public BlogRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<BlogPost>> GetBlogsAsync()
        {
            return await _context.Blogs.ToListAsync();
        }

        public async Task<BlogPost> GetBlogById(int id)
        {
            return await _context.Blogs.FindAsync(id);
        }

        public async Task<bool> AddBlog(BlogDTO blogDTO)
        {
            try
            {
                
                var blog = new BlogPost
                {
                   Title = blogDTO.Title,
                   ImgUrl = blogDTO.ImgUrl,
                   Content = blogDTO.Content,
                   ContentDetail= blogDTO.ContentDetail,
                   Author = blogDTO.Author,
                   CreatedAt = blogDTO.CreatedAt,
                   IsHot = blogDTO.IsHot,
                };

                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> UpdateBlog(int id, BlogDTO blogDTO)
        {
            try
            {
                var blog = await _context.Blogs.FindAsync(id);
                if (blog == null)
                {
                    return false;
                }
                blog.Title = blogDTO.Title;
                blog.ImgUrl = blogDTO.ImgUrl;
                blog.Content = blogDTO.Content;
                blog.ContentDetail = blogDTO.ContentDetail;
                blog.Author = blogDTO.Author;
                blog.CreatedAt = blogDTO.CreatedAt;
                blog.IsHot = blogDTO.IsHot;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteBlog(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return false;
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
