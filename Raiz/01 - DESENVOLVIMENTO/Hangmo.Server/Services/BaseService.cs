using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hangmo.Repository.Data.DAO.Interfaces;

namespace Hangmo.Repository.Services
{
    public class BaseService<T> where T : class
    {
        protected readonly IBaseDAO<T> _dao;

        public BaseService(IBaseDAO<T> dao)
        {
            _dao = dao;
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dao.ListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dao.GetByIdAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dao.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            await _dao.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            await _dao.DeleteAsync(id);
        }
    }
}