using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment1
{
    class Patient : Person
    {
        private PatientMapper mapper;

        // Default constructor that initializes the mapper
        public Patient() : base()
        {
            mapper = new PatientMapper();
        }

        // Parameterized constructor that initializes the patient details and generates a unique ID
        public Patient(string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
            : base(password, name, email, phone, streetNumber, street, city, state)
        {
            mapper = new PatientMapper();
            Id = generateUniqueID(); // Generate a unique ID for each patient
        }

        // Method to generate a unique patient ID
        private int generateUniqueID()
        {
            Random rnd = new Random();
            int newId;

            // Generate a random 5-digit number starting with 1, and ensure it doesn't already exist in the file
            do
            {
                // Generate a 5-digit number starting from 10000 to 19999
                newId = rnd.Next(10000, 20000);
            } while (mapper.IdExistsInFile(newId)); // Check if the ID already exists

            return newId; // Return the unique ID
        }

        // Override the ToString method to return patient details as a string
        public override string ToString()
        {
            // Use the base ToString method to include the basic person details
            return base.ToString();
        }
    }
}
