namespace Bank.Entities
{
    public class Address
    {
        private static int _lastId = 0;

        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public string BuildingNr { get; set; }
        public string AppartmentNr { get; set; }

        public Address()
        {
            Id = _lastId;
            _lastId++;
        }

        public Address( string country, string city, string postalCode, 
            string street, string buildingNr, string appartmentNr) : this()
        {
            Country = country;
            City = city;
            PostalCode = postalCode;
            Street = street;
            BuildingNr = buildingNr;
            AppartmentNr = appartmentNr;
        }
    }
}