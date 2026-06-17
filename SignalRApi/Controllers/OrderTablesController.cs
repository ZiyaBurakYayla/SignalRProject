using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.OrderTableDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderTablesController : ControllerBase
    {
        private readonly IOrderTableService _orderTableService;
        private readonly IMapper _mapper;

        public OrderTablesController(IOrderTableService orderTableService, IMapper mapper)
        {
            _orderTableService = orderTableService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult OrderTableList()
        {
            var values = _mapper.Map<List<ResultOrderTableDto>>(_orderTableService.TGetAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdOrderTable(int id)
        {
            var value = _mapper.Map<GetByIdOrderTableDto>(_orderTableService.TGetById(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateOrderTable(CreateOrderTableDto createOrderTableDto)
        {
            _orderTableService.TAdd(_mapper.Map<OrderTable>(createOrderTableDto));
            return Ok("Masa eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateOrderTable(UpdateOrderTableDto updateOrderTableDto)
        {
            _orderTableService.TUpdate(_mapper.Map<OrderTable>(updateOrderTableDto));
            return Ok("Masa güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrderTable(int id)
        {
            var value = _orderTableService.TGetById(id);
            _orderTableService.TDelete(value);
            return Ok("Masa silindi.");
        }

        [HttpGet("OrderTableCount")]
        public IActionResult OrderTableCount()
        {
            return Ok(_orderTableService.TOrderTableCount());
        }

        [HttpGet("ChangeOrderTableStatusToTrue/{id}")]
        public IActionResult ChangeOrderTableStatusToTrue(int id)
        {
            _orderTableService.TChangeOrderTableStatusToTrue(id);
            return Ok("Masa durumu güncellendi.");
        }

        [HttpGet("ChangeOrderTableStatusToFalse/{id}")]
        public IActionResult ChangeOrderTableStatusToFalse(int id)
        {
            _orderTableService.TChangeOrderTableStatusToFalse(id);
            return Ok("Masa durumu güncellendi.");
        }
    }
}
