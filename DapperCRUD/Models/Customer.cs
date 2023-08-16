namespace DapperCRUD.Models
{
    public class Customer: BaseEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone  { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }

        //todo address customer must be table and list<address>
        public List<Address> Addresses { get; set; }
    }
    public class CustomerDto : BaseEntity
    {
        public string Name { get; set; }
        public string Family { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string NationalCode { get; set; }
        public List<Address> Addresses { get; set; }
    }
}
