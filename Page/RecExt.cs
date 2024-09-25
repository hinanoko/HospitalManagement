using System;

namespace Assignment1
{
    // Define an extension method for the Receptionist class
    public static class ReceptionistExtensions
    {
        // Extension method to display full contact details of a receptionist
        public static void DisplayContactInfo(this Receptionist receptionist)
        {
            Console.WriteLine($"Contact Information for {receptionist.Name}:");
            Console.WriteLine($"Email: {receptionist.Email}");
            Console.WriteLine($"Phone: {receptionist.Phone}");
            Console.WriteLine($"Address: {receptionist.StreetNumber} {receptionist.Street}, {receptionist.City}, {receptionist.State}");
        }
    }
}
