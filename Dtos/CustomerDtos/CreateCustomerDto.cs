namespace MongoTestPro.Dtos.CustomerDtos
{
    public class CreateCustomerDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public bool Status { get; set; }
    }
}