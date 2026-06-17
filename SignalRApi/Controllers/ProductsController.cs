using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.ProductDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var values = _mapper.Map<List<ResultProductDto>>(_productService.TGetAll());
            return Ok(values);
        }

        [HttpGet("ProductListWithCategory")]
        public IActionResult ProductListWithCategory()
        {
            var values = _mapper.Map<List<ResultProductByCategoryDto>>(_productService.TGetProductsByCategories());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            _productService.TAdd(_mapper.Map<Product>(createProductDto));
            return Ok("Ürün bilgisi eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var value = _productService.TGetById(id);
            _productService.TDelete(value);
            return Ok("Ürün bilgisi silindi.");
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            _productService.TUpdate(_mapper.Map<Product>(updateProductDto));
            return Ok("Ürün bilgisi güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdProduct(int id)
        {
            var value = _productService.TGetById(id);
            var dto = _mapper.Map<UpdateProductDto>(value);
            return Ok(dto);
        }

        [HttpGet("ProductCount")]
        public IActionResult GetProductCount()
        {
            var value = _productService.TProductCount();
            return Ok(value);
        }

        [HttpGet("ProductCountByCategoryNameDrinks")]
        public IActionResult ProductCountByCategoryNameDrinks()
        {
            return Ok(_productService.TProductCountByCategoryNameDrinks());
        }

        [HttpGet("ProductCountByCategoryNameHamburger")]
        public IActionResult ProductCountByCategoryNameHamburger()
        {
            return Ok(_productService.TProductCountByCategoryNameHamburger());
        }

        [HttpGet("AverageProductPrice")]
        public IActionResult AverageProductPrice()
        {
            return Ok(_productService.TAverageProductPrice());
        }

        [HttpGet("LowestPriceProductName")]
        public IActionResult LowestPriceProductName()
        {
            return Ok(_productService.TLowestPriceProductName());
        }

        [HttpGet("HighestPriceProductName")]
        public IActionResult HighestPriceProductName()
        {
            return Ok(_productService.THighestPriceProductName());
        }

        [HttpGet("AveragePriceByCategoryNameHamburger")]
        public IActionResult AveragePriceByCategoryNameHamburger()
        {
            return Ok(_productService.TAveragePriceByCategoryNameHamburger());
        }

        [HttpGet("GetLast9ProductsListWithCategory")]
        public IActionResult GetLast9ProductsListWithCategory()
        {
            var values = _mapper.Map<List<ResultProductByCategoryDto>>(_productService.TGetLast9ProductsListWithCategory());
            return Ok(values);
        }
    }
}
