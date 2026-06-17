using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalR.DtoLayer.BasketDtos
{
    public class CreateBasketDto
    {
        public decimal Price { get; set; }
        public int Count { get; set; }
        public decimal TotalCount { get; set; }
        public int ProductId { get; set; }
        public int OrderTableId { get; set; }
    }
}
