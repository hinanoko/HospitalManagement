using System;

namespace Assignment1
{

    class Person
    {
        public int Id { get; set; } // Unique identifier for the person
        public string Password { get; set; } // Password for authentication
        public string Name { get; set; } // Full name of the person
        public string Email { get; set; } // Email address of the person
        public string Phone { get; set; } // Phone number of the person
        public string StreetNumber { get; set; } // Street number of the person's address
        public string Street { get; set; } // Street name of the person's address
        public string City { get; set; } // City where the person resides
        public string State { get; set; } // State where the person resides

        // Default constructor
        public Person() { }

        // Parameterized constructor for initializing a person with specific details
        public Person(string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
        {
            Password = password; // Set the password
            Name = name; // Set the name
            Email = email; // Set the email
            Phone = phone; // Set the phone number
            StreetNumber = streetNumber; // Set the street number
            Street = street; // Set the street name
            City = city; // Set the city
            State = state; // Set the state
        }

        // Overriding ToString method to provide string representation of the person object
        public virtual string ToString(string format = "full")
        {
            if (format == "full")
            {
                // Return detailed information about the person
                return $"ID: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}, Address: {StreetNumber} {Street}, {City}, {State}";
            }
            else
            {
                // Return basic information about the person
                return $"Name: {Name}, Email: {Email}";
            }
        }
    }
}
