using System.Linq.Expressions;
using Light_Shop.API.Data;
using Light_Shop.API.Models;
using Light_Shop.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Light_Shop.API.Services.Implementations
{
    public class BrandService(ApplicationDbContext context) : IBrandService
    {

        ApplicationDbContext _context = context;

        public IEnumerable<Brand> GetAll()
        {
            var brands = _context.Brands.ToList();
            return brands;
        }

        public Brand? Get(Expression<Func<Brand, bool>> expression)
        {
            var brand = _context.Brands.FirstOrDefault(expression);
            return brand;
        }

        public Brand Add(Brand brand)
        {
            _context.Brands.Add(brand);
            _context.SaveChanges();
            return brand;
        }

        public bool Edit(int id, Brand brand)
        {
            Brand? brandInDb = _context.Brands.AsNoTracking().FirstOrDefault(b => b.Id == id);
            if (brandInDb == null) return false;
            brand.Id = brandInDb.Id;
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return true;
        }

        public bool Remove(int id)
        {
            var brandInDb = _context.Brands.FirstOrDefault(b => b.Id == id);    
            if (brandInDb == null) return false;
            _context.Brands.Remove(brandInDb);
            _context.SaveChanges();
            return true;
        }
    }
}
