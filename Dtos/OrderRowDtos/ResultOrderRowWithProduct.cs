namespace MongoTestPro.Dtos.OrderRowDtos
{
    public class ResultOrderRowWithProduct
    {
        public string OrderRowId { get; set; }
        public string ProductId { get; set; }
        public int ProductCount { get; set; }
        public string OrderId { get; set; }
        public Decimal RowPrice { get; set; }
        public Decimal ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string ProductImgUrl { get; set; }
    }
}
