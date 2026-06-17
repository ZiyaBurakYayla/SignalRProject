using AutoMapper;
using SignalR.DtoLayer.OrderTableDtos;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Mapping
{
    public class OrderTableMapping : Profile
    {
        public OrderTableMapping()
        {
            CreateMap<OrderTable, ResultOrderTableDto>().ReverseMap();
            CreateMap<OrderTable, CreateOrderTableDto>().ReverseMap();
            CreateMap<OrderTable, UpdateOrderTableDto>().ReverseMap();
            CreateMap<OrderTable, GetByIdOrderTableDto>().ReverseMap();
        }
    }
}
