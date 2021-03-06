﻿using Bank.Entities.Enums;

namespace Bank.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Pesel { get; set; }

        public string Email { get; set; }

        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public AuthLevel AuthLevel { get; set; }

        public bool IsSuspended { get; set; }

        public bool IsDeleted { get; set; }

        public string Password { get; set; }

        public Employee()
        {
            IsSuspended = false;
            IsDeleted = false;
        }

        public Employee(string firstName, string lastName, string pesel, string email, 
            int addressId, AuthLevel authLevel, string password) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            Email = email;
            AddressId = addressId;
            AuthLevel = authLevel;
            Password = password;
        }
    }
}
