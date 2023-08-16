namespace DapperCRUD.Models
{
    public class Address : BaseEntity
    {
        public string AddressName { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
