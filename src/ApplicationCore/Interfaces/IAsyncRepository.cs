using System.Linq;
using System.Threading.Tasks;

namespace Forma1Teams.ApplicationCore.Interfaces
{
    public interface IAsyncRepository<T> 
    {
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        IQueryable<T> GetQuery();
    }
}
