using Light_Shop.API.Data;
using Light_Shop.API.DTOs.Requests;
using Light_Shop.API.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Light_Shop.API.Services.ProductServices
{
    public class ProductService(ApplicationDbContext context) : IProductService
    {
        ApplicationDbContext _context = context;


        public IEnumerable<Product> GetAll()
        {
            var products = _context.Products.ToList();
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
                using (var stream = System.IO.File.Create(filePath))
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

        public bool Edit(int id, ProductRequest prodcutRequest)
        {
            var productInDb = _context.Products.AsNoTracking().FirstOrDefault(p=>p.Id==id);
            if (productInDb == null) return false;
            
            if (prodcutRequest != null && prodcutRequest.MainImage.Length > 0)
            {
                var oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", productInDb.MainImage);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                var newFileName = Guid.NewGuid().ToString() + Path.GetExtension(prodcutRequest.MainImage.FileName);
                var newFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Images", newFileName);

                using (var stream = System.IO.File.Create(newFilePath))
                {
                    prodcutRequest.MainImage.CopyTo(stream);
                }
                productInDb.MainImage = newFileName;
            }
            var updatedProduct = prodcutRequest.Adapt<Product>();
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
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return true;
        }


    }
}
