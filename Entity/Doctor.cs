using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    // Doctor class inherits from the Person class
    class Doctor : Person
    {
        private DoctorMapper mapper;

        // Default constructor that initializes the DoctorMapper instance
        public Doctor() : base()
        {
            mapper = new DoctorMapper();
        }

        // Parameterized constructor that sets properties inherited from Person and assigns a unique ID to the doctor
        public Doctor(string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
            : base(password, name, email, phone, streetNumber, street, city, state)
        {
            mapper = new DoctorMapper();
            Id = generateUniqueID(); // Generate and assign a unique ID for the doctor
        }

        // Method to generate a unique 5-digit ID starting with 2 (between 20000 and 30000)
        private int generateUniqueID()
        {
            Random rnd = new Random();
            int newId;

            do
            {
                // Generate a random number between 20000 and 30000 (5 digits starting with 2)
                newId = rnd.Next(20000, 30000);
            }
            // Ensure the generated ID doesn't already exist in the file
            while (mapper.IdExistsInFile(newId));

            return newId;
        }

        // Override the ToString method to return the base class' string representation (if needed, can add custom formatting)
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
