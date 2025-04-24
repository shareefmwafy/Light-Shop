using Light_Shop.API.Data;
using Light_Shop.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Light_Shop.API.Services.IService
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Service(ApplicationDbContext context) 
        {
            this._context = context;
            _dbSet = _context.Set<T>();

        }
        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _context.AddAsync(entity, cancellationToken);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T?> GetOneAsync(Expression<Func<T, bool>> expression = null, Expression<Func<T, object>>?[] include = null, bool isTracked = true)
        {
            var all = await GetAsync(expression, include, isTracked);
            return all.FirstOrDefault();
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>>? expression = null, Expression<Func<T, object>>?[] include = null, bool isTracked = true)
        {
            IQueryable<T> entites = _dbSet;
            if (expression is not null)
            {
                entites = entites.Where(expression);
            }
            if (include is not null)
            {
                foreach (var item in include)
                {
                    entites = entites.Include(item);
                }
            }
            if (!isTracked)
            {
                entites = entites.AsNoTracking();
            }

            return await entites.ToListAsync();
        }

        public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
        {
            T? entityInDb = _dbSet.Find(id);
            if (entityInDb == null) return false;

            _dbSet.Remove(entityInDb);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }

    }
}
