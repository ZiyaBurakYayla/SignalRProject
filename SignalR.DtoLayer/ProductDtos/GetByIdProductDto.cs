namespace SignalR.DtoLayer.ProductDtos
{
    public class GetByIdProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductImageUrl { get; set; }
        public decimal ProductPrice { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryId { get; set; }
    }
}
