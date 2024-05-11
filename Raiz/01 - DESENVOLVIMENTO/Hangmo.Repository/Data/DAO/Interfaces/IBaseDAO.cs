using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hangmo.Repository.Data.DAO.Interfaces
{
    public interface IBaseDAO<T> where T : class
    {
        Task<List<T>> ListAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}