namespace SignalR.DtoLayer.DiscountDtos
{
    public class UpdateDiscountDto
    {
        public int DiscountId { get; set; }
        public string Title { get; set; }
        public string DiscountAmount { get; set; }
        public string DiscountDescription { get; set; }
        public string DiscountImageUrl { get; set; }
        public bool Status { get; set; }
    }
}
