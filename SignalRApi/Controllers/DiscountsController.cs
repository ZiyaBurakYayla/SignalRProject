using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.DiscountDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IMapper _mapper;
        public DiscountsController(IDiscountService discountService, IMapper mapper)
        {
            _discountService = discountService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult DiscountList()
        {
            var values = _mapper.Map<List<ResultDiscountDto>>(_discountService.TGetAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateDiscount(CreateDiscountDto createDiscountDto)
        {
            createDiscountDto.Status = false;
            _discountService.TAdd(_mapper.Map<Discount>(createDiscountDto));
            return Ok("İndirim bilgisi eklendi.");
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteDiscount(int id)
        {
            var value = _discountService.TGetById(id);
            _discountService.TDelete(value);
            return Ok("İndirim bilgisi silindi.");
        }

        [HttpPut]
        public IActionResult UpdateDiscount(UpdateDiscountDto updateDiscountDto)
        {
            _discountService.TUpdate(_mapper.Map<Discount>(updateDiscountDto));
            return Ok("İndirim bilgisi güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdDiscount(int id)
        {
            var value = _mapper.Map<GetByIdDiscountDto>(_discountService.TGetById(id));
            return Ok(value);
        }

        [HttpGet("DiscountChangeStatusToTrue")]
        public IActionResult DiscountChangeStatusToTrue(int id)
        {
            _discountService.TDiscountChangeStatusToTrue(id);
            return Ok("İndirim bilgisi güncellendi.");
        }

        [HttpGet("DiscountChangeStatusToFalse")]
        public IActionResult DiscountChangeStatusToFalse(int id)
        {
            _discountService.TDiscountChangeStatusToFalse(id);
            return Ok("İndirim bilgisi güncellendi.");
        }

        [HttpGet("GetListDiscountByStatusTrue")]
        public IActionResult GetListDiscountByStatusTrue()
        {
            return Ok(_discountService.TGetListDiscountByStatusTrue());            
        }
    }
}
