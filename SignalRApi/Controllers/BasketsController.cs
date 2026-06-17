using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.BasketDtos;
using SignalR.EntityLayer.Entities;
using SignalRApi.Models;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderTableService _orderTableService;
        private readonly SignalRContext _signalRContext;
        private readonly IMapper _mapper;

        public BasketsController(IBasketService basketService, IOrderService orderService,
            IOrderDetailService orderDetailService, IOrderTableService orderTableService,
            SignalRContext signalRContext, IMapper mapper)
        {
            _basketService = basketService;
            _orderService = orderService;
            _orderDetailService = orderDetailService;
            _orderTableService = orderTableService;
            _signalRContext = signalRContext;
            _mapper = mapper;
        }

        [HttpGet("GetBasketByOrderTableNumber")] 
        public IActionResult GetBasketByOrderTableNumber(int tableNumber)
        {
            var baskets = _basketService.TGetBasketByOrderTableNumber(tableNumber);
            if (baskets == null || !baskets.Any())
            {
                return NotFound();
            }
            return Ok(baskets);
        }

        [HttpGet("BasketsListByMenuTableWithProductName")]
        public IActionResult BasketsListByMenuTableWithProductName(int tableNumber)
        {
            var values = _signalRContext.Baskets.Include(y => y.Product).Where(
                a => a.OrderTableId == tableNumber).Select(z => new ResultBasketListWithProducts
                {
                    BasketId = z.BasketId,
                    Count = z.Count,
                    Price = z.Price,
                    TotalCount = z.TotalCount,
                    ProductId = z.ProductId,
                    OrderTableId = z.OrderTableId,
                    ProductName = z.Product.ProductName
                }).ToList();
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBasket(CreateBasketDto createBasketDto)
        {
            var basket = _mapper.Map<Basket>(createBasketDto);
            basket.Count = 1;
            basket.OrderTableId = createBasketDto.OrderTableId;
            basket.Price = _signalRContext.Products
                .Where(x => x.ProductId == createBasketDto.ProductId)
                .Select(y => y.ProductPrice)
                .FirstOrDefault();
            basket.TotalCount = basket.Price * basket.Count;
            _basketService.TAdd(basket);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBasket(int id)
        {
            var value = _basketService.TGetById(id);
            _basketService.TDelete(value);
            return Ok();
        }

        [HttpGet("CompleteOrder/{tableNumber}")]
        public IActionResult CompleteOrder(int tableNumber)
        {
            var baskets = _basketService.TGetBasketByOrderTableNumber(tableNumber);
            if (baskets == null || !baskets.Any())
                return BadRequest("Sepette ürün yok.");

            var order = new Order
            {
                TableNumber = tableNumber.ToString(),
                OrderDate = DateTime.Now,
                TotalPrice = baskets.Sum(x => x.Price * x.Count),
                Description = $"Masa {tableNumber} siparişi"
            };
            _orderService.TAdd(order);

            foreach (var basket in baskets)
            {
                _orderDetailService.TAdd(new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = basket.ProductId,
                    Count = basket.Count,
                    UnitPrice = basket.Price,
                    TotalPrice = basket.Price * basket.Count
                });
                _basketService.TDelete(basket);
            }

            _orderTableService.TChangeOrderTableStatusToFalse(tableNumber);

            return Ok();
        }
    }
}
