using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{

    public class Receptionist
    {
        public int Id { get; set; } // Unique identifier for the receptionist
        public string Password { get; set; } // Password for authentication
        public string Name { get; set; } // Full name of the receptionist
        public string Email { get; set; } // Email address of the receptionist
        public string Phone { get; set; } // Phone number of the receptionist
        public string StreetNumber { get; set; } // Street number of the receptionist's address
        public string Street { get; set; } // Street name of the receptionist's address
        public string City { get; set; } // City where the receptionist resides
        public string State { get; set; } // State where the receptionist resides

        // Default constructor
        public Receptionist() { }

        // Parameterized constructor to initialize a receptionist with specific details
        public Receptionist(int id, string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
        {
            Id = id;
            Password = password;
            Name = name;
            Email = email;
            Phone = phone;
            StreetNumber = streetNumber;
            Street = street;
            City = city;
            State = state;
        }

        // Method to display receptionist's details
        public string ToString(string format = "full")
        {
            if (format == "full")
            {
                return $"ID: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}, Address: {StreetNumber} {Street}, {City}, {State}";
            }
            else
            {
                return $"Name: {Name}, Email: {Email}";
            }
        }
    }

}
