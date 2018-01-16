
namespace Bank.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNr { get; set; }
        public string AppartmentNr { get; set; }

        public Address()
        {
        }

        public Address( string country, string city, string postalCode, 
            string street, string buildingNr, string appartmentNr)
        {
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNr = buildingNr;
            AppartmentNr = appartmentNr;
        }

        public override string ToString()
        {
            return Country + ", " + PostalCode + " " + City + ", " + Street + " " + BuildingNr + " m." + AppartmentNr;
        }
    }
}