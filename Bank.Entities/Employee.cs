using Bank.Entities.Enums;

namespace Bank.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Pesel { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public AuthLevel AuthLevel { get; set; }
    }
}
