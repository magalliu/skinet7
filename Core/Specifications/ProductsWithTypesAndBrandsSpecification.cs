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
        //public ProductsWithTypesAndBrandsSpecification()
        //public ProductsWithTypesAndBrandsSpecification(string sort,int?brandId,int? typeId)
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productParams)
        : base(x =>
        /*
              (!brandId.HasValue|| x.ProductBrandId==brandId) &&
              (!typeId.HasValue||x.ProductTypeId==typeId)
              */

                (string.IsNullOrEmpty(productParams.Search)||x.Name.ToLower().Contains(productParams.Search))&&
                
                (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
                (!productParams.TypeId.HasValue || x.ProductTypeId == productParams.TypeId)

            )
        {
            AddInculde(x => x.ProductType);
            AddInculde(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex -1),productParams.PageSize);
            /*
                        if (!string.IsNullOrEmpty(sort))
                        {
                            switch (sort)
                            {
                                case "priceAsc":
                                    AddOrderBy(p => p.Price);
                                    break;
                                case "priceDesc":
                                    AddOrderByDescending(p => p.Price);
                                    break;
                                default:
                                    AddOrderBy(n => n.Name);
                                    break;
                            }

                        }

                        */

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }

            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id)
            : base(x => x.Id == id)
        {

            AddInculde(x => x.ProductType);
            AddInculde(x => x.ProductBrand);
        }
    }
}