using Microsoft.AspNetCore.Mvc;
using PetShops.Data;
using PetShops.DTOs;
using PetShops.Interfaces;
using PetShops.Models;
using PetShops.Services;
using Microsoft.AspNetCore.Authorization;

namespace PetShops.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    public class BlogController : ControllerBase
    {
        private readonly PetShopDbContext _context;
        private readonly IBlogRepository _blogRepository;
        public BlogController(PetShopDbContext context, IBlogRepository blogRepository)
        {
            _context = context;
            _blogRepository = blogRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBlog()
        {
            var blogs = await _blogRepository.GetBlogsAsync();
            if (blogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No blog found matching the criteria.");
            }
            return StatusCode(StatusCodes.Status200OK, blogs);
        }

        [HttpGet("{id}")]
        [Authorize(Roles ="Read")]
        public async Task<IActionResult> GetBlog(int id)
        {
            var blog = await _blogRepository.GetBlogById(id);
            if (blog == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No blog found for id: {id}");
            }
            return StatusCode(StatusCodes.Status200OK, blog);
        }

        [HttpPost]
        public async Task<ActionResult<BlogPost>> AddBlog(BlogDTO blogDTO)
        {
            var result = await _blogRepository.AddBlog(blogDTO);

            if (result == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add blog.");
            }

            return StatusCode(StatusCodes.Status200OK, "blog added successfully");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBlog(int id, [FromBody] BlogDTO blogDTO)
        {
            var result = await _blogRepository.UpdateBlog(id, blogDTO);

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var result = await _blogRepository.DeleteBlog(id);

            if (!result)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, "blog deleted successfully");
        }
    }
}
