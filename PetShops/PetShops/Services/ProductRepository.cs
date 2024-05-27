using Microsoft.EntityFrameworkCore;
using PetShops.Data;
using PetShops.DTOs;
using PetShops.Models;
using PetShops.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetShops.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly PetShopDbContext _context;

        public ProductRepository(PetShopDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> AddProduct(ProductDTO productDTO)
        {
            try
            {
                var category = await _context.Categories.FindAsync(productDTO.CategoryId);
                var petType = await _context.PetTypes.FindAsync(productDTO.PetTypeId);

                if (category == null || petType == null)
                {
                    return false;
                }

                var product = new Product
                {
                    ProductName = productDTO.ProductName,
                    ProductDescription = productDTO.ProductDescription,
                    ProductPrice = productDTO.ProductPrice,
                    ProductImage = productDTO.ProductImage,
                    IsSaling = productDTO.IsSaling,
                    Category = category,
                    PetType = petType
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                return false;
            }
        }

        public async Task<bool> UpdateProduct(int id, ProductDTO productDTO)
        {
            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return false;
                }

                product.ProductName = productDTO.ProductName;
                product.ProductDescription = productDTO.ProductDescription;
                product.ProductPrice = productDTO.ProductPrice;
                product.ProductImage = productDTO.ProductImage;
                product.IsSaling = productDTO.IsSaling;

                if (productDTO.CategoryId != null)
                {
                    var category = await _context.Categories.FindAsync(productDTO.CategoryId);
                    if (category == null)
                    {
                        return false;
                    }
                    product.Category = category;
                }

                if (productDTO.PetTypeId != null)
                {
                    var petType = await _context.PetTypes.FindAsync(productDTO.PetTypeId);
                    if (petType == null)
                    {
                        return false;
                    }
                    product.PetType = petType;
                }

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                // Log or handle exception
                return false;
            }
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AddEmailSubscription(EmailSubscribeDTO emailDto)
        {
            try
            {
                var existingEmail = await _context.EmailSubscribe.FirstOrDefaultAsync(e => e.Email == emailDto.Email);
                if (existingEmail != null)
                {
                    return true; 
                }

                var newEmail = new EmailSubscribe
                {
                    Email = emailDto.Email
                };
                _context.EmailSubscribe.Add(newEmail);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }



    }
}
