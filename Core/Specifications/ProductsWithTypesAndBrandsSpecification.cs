using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specification;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInculde( x=> x.ProductType);
            AddInculde( x=> x.ProductBrand);   
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            :base(x => x.Id == id)
        {

            AddInculde( x=> x.ProductType);
            AddInculde( x=> x.ProductBrand);  
        }
    }
}