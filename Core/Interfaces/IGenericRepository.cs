using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T: BaseEntity // where :restrictions:these means that only 
                                                            //the entities can be used for these genercic
    {
        Task<T> GetByIdAsync(int id );
        Task<IReadOnlyList<T>> ListAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec );
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    }
}