
namespace SignalR.EntityLayer.Entities
{
    public class OrderTable
    {
        public int OrderTableId { get; set; }
        public string OrderTableName { get; set; }
        public bool Status { get; set; }
        public List<Basket> Baskets { get; set; }
    }
}
