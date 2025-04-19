﻿using Light_Shop.API.Models;
using System.Linq.Expressions;
namespace Light_Shop.API.Services.Interfaces
{
    public interface ICategoryService
    {
        // I use IEnumerable to use categories as multi types of collections
        IEnumerable<Category> GetAll();
        Category? Get(Expression <Func<Category,bool>> expression);
        Task<Category> AddAsync(Category category, CancellationToken cancellationToken = default);
        Task<bool> EditAsync(int id, Category category, CancellationToken cancellationToken = default);
        Task<bool> UpdateStatusAsync(int id, Category category, CancellationToken cancellationToken);
        Task<bool> RemoveAsync(int id, CancellationToken cancellationToken);
    }
}
