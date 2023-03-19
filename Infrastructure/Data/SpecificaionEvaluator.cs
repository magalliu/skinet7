using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificaionEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,//static does nt create an Instance
        ISpecification<TEntity> spec)
        {
                var query =inputQuery;

                if  (spec.Criteria !=null)
                {
                    query=query.Where(spec.Criteria);//p=>p.ProductTupeId==id

                }

                query=spec.Includes.Aggregate(query, (current,include) => current.Include(include));
                return query;
        }
    }
}