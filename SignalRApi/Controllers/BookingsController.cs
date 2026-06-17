using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SignalR.BusinessLayer.Abstract;
using SignalR.DtoLayer.BookingDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateBookingDto> _validator;
        public BookingsController(IBookingService bookingService, IMapper mapper, IValidator<CreateBookingDto> validator)
        {
            _bookingService = bookingService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet]
        public IActionResult BookingList()
        {
            var values = _mapper.Map<List<ResultBookingDto>>(_bookingService.TGetAll());
            return Ok(values);
        }

        [HttpPost]
        public IActionResult CreateBooking(CreateBookingDto createBookingDto)
        {
            var validationResult = _validator.Validate(createBookingDto);
            if(!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }
            if (string.IsNullOrWhiteSpace(createBookingDto.Description))
                createBookingDto.Description = "Rezervasyon Beklemede";
            _bookingService.TAdd(_mapper.Map<Booking>(createBookingDto));
            return Ok("Rezervasyon bilgisi eklendi.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBooking(int id)
        {
            var value = _bookingService.TGetById(id);
            _bookingService.TDelete(value);
            return Ok("Rezervasyon bilgisi silindi.");
        }

        [HttpPut]
        public IActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            _bookingService.TUpdate(_mapper.Map<Booking>(updateBookingDto));
            return Ok("Rezervasyon bilgisi güncellendi.");
        }

        [HttpGet("{id}")]
        public IActionResult GetByIdBooking(int id)
        {
            var value = _mapper.Map<GetByIdBookingDto>(_bookingService.TGetById(id));
            return Ok(value);
        }

        [HttpGet("BookingStatusApproved")]
        public IActionResult BookingStatusApproved(int id)
        {
            _bookingService.TBookingStatusApproved(id);
            return Ok("Rezervasyon durumu güncellendi");
        }

        [HttpGet("BookingStatusCancelled")]
        public IActionResult BookingStatusCancelled(int id)
        {
            _bookingService.TBookingStatusCancelled(id);
            return Ok("Rezervasyon durumu güncellendi");
        }

    }
}
