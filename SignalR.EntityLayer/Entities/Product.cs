namespace SignalR.EntityLayer.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }  
        public string ProductDescription { get; set; }  
        public string ProductImageUrl { get; set; }  
        public decimal ProductPrice { get; set; }  
        public bool ProductStatus { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        public List<Basket> Baskets { get; set; }
    } 
}
