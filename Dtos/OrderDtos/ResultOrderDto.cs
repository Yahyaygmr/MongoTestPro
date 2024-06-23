namespace MongoTestPro.Dtos.OrderDtos
{
    public class ResultOrderDto
    {
        public string OrderId { get; set; }
        public string OrderAddress { get; set; }
        public string OrderDescription { get; set; }
        public DateTime OrderDate { get; set; }
        public bool Status { get; set; }
        public string CustomerId { get; set; }
        public decimal OrderTotalPrice { get; set; }
    }
}