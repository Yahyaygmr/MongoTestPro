namespace MongoTestPro.Dtos.OrderRowDtos
{
    public class GetByIdOrderRowDto
    {
        public string OrderRowId { get; set; }
        public string ProductId { get; set; }
        public int ProductCount { get; set; }
        public string OrderId { get; set; }
    }
}