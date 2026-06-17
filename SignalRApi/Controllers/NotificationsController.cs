using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.NotificationDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly IMapper _mapper;

        public NotificationsController(INotificationService notificationService, IMapper mapper)
        {
            _notificationService = notificationService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult NotificationList()
        {
            var values = _mapper.Map<List<ResultNotificationDto>>(_notificationService.TGetAll());
            return Ok(values);
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdNotification(int id)
        {
            var value = _mapper.Map<GetByIdNotificationDto>(_notificationService.TGetById(id));
            return Ok(value);
        }

        [HttpPost]
        public IActionResult CreateNotification(CreateNotificationDto createNotificationDto)
        {
            _notificationService.TAdd(_mapper.Map<Notification>(createNotificationDto));
            return Ok("Bildirim eklendi.");
        }

        [HttpPut]
        public IActionResult UpdateNotification(UpdateNotificationDto updateNotificationDto)
        {
            _notificationService.TUpdate(_mapper.Map<Notification>(updateNotificationDto));
            return Ok("Bildirim güncellendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteNotification(int id)
        {
            var value = _notificationService.TGetById(id);
            _notificationService.TDelete(value);
            return Ok("Bildirim silindi.");
        }

        [HttpGet("NotificationCountByStatusisFalse")]
        public IActionResult NotificationCountByStatusisFalse()
        {
            return Ok(_notificationService.TNotificationCountByStatusisFalse());
        }

        [HttpGet("GetAllNotificationByFalse")]
        public IActionResult GetAllNotificationByFalse()
        {
            var values = _mapper.Map<List<ResultNotificationDto>>(_notificationService.TGetAllNotificationByFalse());
            return Ok(values);
        }

        [HttpGet("NotificationChangeToTrue/{id}")]
        public IActionResult NotificationChangeToTrue(int id)
        {
            _notificationService.TNotificationChangeToTrue(id);
            return Ok();
        }

        [HttpGet("NotificationChangeToFalse/{id}")]
        public IActionResult NotificationChangeToFalse(int id)
        {
            _notificationService.TNotificationChangeToFalse(id);
            return Ok();
        }
    }
}
