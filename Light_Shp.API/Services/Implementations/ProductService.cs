﻿using Light_Shop.API.Data;
using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Light_Shop.API.Services.Implementations
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        ApplicationDbContext _context = context;


        public IEnumerable<Product> GetAll(string? query, int page, int limit)
        {
            IQueryable<Product> products = _context.Products;
            if(query != null)
            {
                products = products.Where(product=> product.Name.Contains(query) || product.Description.Contains(query));
            }
            if(limit <= 0 ) limit = 10;
            if(page <= 0 ) page = 1;

            products = products.Skip( (page-1) * limit ).Take(limit);

            return products;
        }

        public Product? Get(Expression<Func<Product, bool>> expression)
        {
            return _context.Products.FirstOrDefault(expression);
        }

        public Product Add(ProductRequest productRequest)
        {
            var file = productRequest.MainImage;
            var product = productRequest.Adapt<Product>();

            if (file != null && file.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", fileName);
                using (var stream = File.Create(filePath))
                {
                    file.CopyTo(stream);
                }
                product.MainImage = fileName;

            }
            else
            {
                product.MainImage = null;
            }
            _context.Products.Add(product);
            _context.SaveChanges();
            
            return product;
        }

        public bool Edit(int id, ProductUpdateRequest prodcutRequest)
        {
            var productInDb = _context.Products.AsNoTracking().FirstOrDefault(p=>p.Id==id);
            if (productInDb == null) return false;

            var updatedProduct = prodcutRequest.Adapt<Product>();
            var file = prodcutRequest.MainImage;


            if (file != null && file.Length > 0)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", productInDb.MainImage);
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }

                var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(prodcutRequest.MainImage.FileName);
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", newFileName);

                using (var stream = File.Create(newFilePath))
                {
                    prodcutRequest.MainImage.CopyTo(stream);
                }
                productInDb.MainImage = newFileName;
            }
            
            updatedProduct.MainImage = productInDb.MainImage;
            updatedProduct.Id = id;
            context.Products.Update(updatedProduct);
            _context.SaveChanges();
            return true;
        }

        public bool Remove(int id)
        {
            
            var product = _context.Products.Find(id);
            if (product == null) return false;

            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", product.MainImage);
            if(File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }


    }
}
