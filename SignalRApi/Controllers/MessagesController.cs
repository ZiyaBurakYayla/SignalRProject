using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.MessageDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _MessageService;
        private readonly IMapper _mapper;
        public MessagesController(IMessageService MessageService, IMapper mapper)
        {
            _MessageService = MessageService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult MessageList()
        {
            var values = _mapper.Map<List<ResultMessageDto>>(_MessageService.TGetAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            createMessageDto.IsRead = false;
            _MessageService.TAdd(_mapper.Map<Message>(createMessageDto));
            return Ok("Mesaj bilgisi eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMessage(int id)
        {
            var value = _MessageService.TGetById(id);
            _MessageService.TDelete(value);
            return Ok("Mesaj bilgisi silindi.");
        }

        [HttpPut]
        public IActionResult UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            _MessageService.TUpdate(_mapper.Map<Message>(updateMessageDto));
            return Ok("Mesaj bilgisi güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdMessage(int id)
        {
            var value = _mapper.Map<GetByIdMessageDto>(_MessageService.TGetById(id));
            return Ok(value);
        }
    }
}
