using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DotNetGuide.Application.Interface
{
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// This is the default select method. It will return all the records from the database.
        /// </summary>
        /// <param name="filter">This will be automatically passed along by EF Core.</param>
        /// <param name="includeProperties">Any navigational property added into Entity, we can pass multiple values in separated by comma. </param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        /// <summary>
        /// This method will return a single record from the database based on the passed Id.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <param name="tracked"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false);
        /// <summary>
        /// This method will add a new record into the database.
        /// </summary>
        /// <param name="entity">Any model of viewmodel constructed to match the Entity.</param>
        /// <returns></returns>
        Task AddAsync(T entity);
        /// <summary>
        /// This method will delete the record in the database. If you want to make it soft delete, you can make changes in the base repository.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);

        /// <summary>
        /// This method will check if the record exists in the database or not and return true or false.
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> filter);
        

        // Optional: A SaveChangesAsync method if you want to control commits at the repository level
        Task SaveChangesAsync();

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);

    }
}
