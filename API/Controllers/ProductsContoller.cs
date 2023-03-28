
using API.Dtos;
using API.Errors;
using API.Helpers;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    //public class ProductsController : ControllerBase

    public class ProductsController : BaseApiController
    {

        //private readonly IProductRepository _repo;

        /*public ProductsController(IProductRepository repo)
        {
            _repo = repo;
    
        }
*/
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductBrand> _productBrandRepo;
        private readonly IGenericRepository<ProductType> _productTypeRepo;

        private readonly IMapper _mapper;
        public ProductsController(IGenericRepository<Product> productsRepo,
                                  IGenericRepository<ProductBrand> productBrandRepo,
                                  IGenericRepository<ProductType> productTypeRepo,
                                  IMapper mapper)
        {
            _productsRepo = productsRepo;
            _productBrandRepo = productBrandRepo;
            _productTypeRepo = productTypeRepo;
            _mapper=mapper;


        }
/*

        [HttpGet]

        public async Task<ActionResult<List<Product>>> GetProducts()
        {

            var products = await _productsRepo.ListAllAsync();
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProducts(int id)
        {

            return await _productsRepo.GetByIdAsync(id);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {

            return Ok(await _productTypeRepo.ListAllAsync());
        }

*/



        [HttpGet]

        //public async Task<ActionResult<List<Product>>> GetProducts()
        //public async Task<ActionResult<List<ProductToReturnDto>>> GetProducts()
        //public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        //public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort)-sorting

        //public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string sort,int?brandId,int? typeId) //sorting and filtring
        
        //public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(
            //string sort, int?brandId, int? typeId
            public async Task<ActionResult<IReadOnlyList<Pagination<ProductToReturnDto>>>> GetProducts(
                        [FromQuery]ProductSpecParams productParams)                //Paging orting and filtring
        {
        //var spec =new ProductsWithTypesAndBrandsSpecification();
            //var spec =new ProductsWithTypesAndBrandsSpecification(sort);-sorting
            //var spec =new ProductsWithTypesAndBrandsSpecification(sort,brandId,typeId);//sorting and filtring
            var spec =new ProductsWithTypesAndBrandsSpecification(productParams);
            var countSpec=new ProductWithFiltersForCountSpecification(productParams);
            var totalItems=await _productsRepo.CountAsync(countSpec);
            var products = await _productsRepo.ListAsync(spec);
            var data =_mapper
            .Map<IReadOnlyList<Product>,IReadOnlyList<ProductToReturnDto>>(products);
            //return Ok(products);
            /*
            return products.Select(product=>new ProductToReturnDto
            {Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                PictureUrl =product.PictureUrl,
                Price =product.Price,
                ProductType=product.ProductType.Name,
                ProductBrand =product.ProductBrand.Name

            }).ToList();
            */
            /*
            return Ok(_mapper
            .Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
            */
            return Ok(new Pagination <ProductToReturnDto>(productParams.PageIndex,productParams.PageSize,totalItems,data));
        }


        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse),StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<Product>> GetProducts(int id)
        public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
        {

             var spec = new ProductsWithTypesAndBrandsSpecification(id);

             //return await _productsRepo.GetEntityWithSpec(spec);
             var product =await _productsRepo.GetEntityWithSpec(spec);

             if (product==null) return NotFound(new ApiResponse(404));
             /*
             return new ProductToReturnDto
             {
                Id=product.Id,
                Name=product.Name,
                Description=product.Description,
                PictureUrl =product.PictureUrl,
                Price =product.Price,
                ProductType=product.ProductType.Name,
                ProductBrand =product.ProductBrand.Name

             };*/
             return _mapper.Map<Product, ProductToReturnDto>(product);
        }


        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {

            return Ok(await _productBrandRepo.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {

            return Ok(await _productTypeRepo.ListAllAsync());
        }

    }
}