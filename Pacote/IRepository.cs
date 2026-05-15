using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pacote
{
    public interface IRepository<T> where T : IEntity
    {
        Task CreateAsync(T item);
        Task<IReadOnlyCollection<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task DeleteAsync(Guid id);
        Task UpdateAsync(T item);
    }
}
