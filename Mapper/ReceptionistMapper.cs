using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
    // Class responsible for mapping Receptionist objects to and from a data source
    class ReceptionistMapper : BaseMapper
    {
        // Method to retrieve a Receptionist by their ID
        // Parameters:
        // - id: The ID of the receptionist to retrieve
        public Receptionist GetReceptionistById(int id)
        {
            // Get the file path for the user data
            string filePath = GetUserFilePath(USER_FILE_NAME);

            // Check if the user file exists; throw an exception if not
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("User file not found.");
            }

            // Read all lines from the user file
            string[] lines = File.ReadAllLines(filePath);

            // Find the line corresponding to the given receptionist ID
            var userLine = lines.FirstOrDefault(line =>
            {
                string[] parts = line.Split(',');
                // Ensure there are parts, parse the ID, and check if it matches the given ID
                return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
            });

            // If no matching line is found, return null
            if (userLine == null)
            {
                return null;
            }

            // Split the found line into parts and create a Receptionist object
            // Split the found line into parts and create a Receptionist object
            string[] userParts = userLine.Split(',');
            return new Receptionist(
                int.Parse(userParts[0]), // ID
                userParts[1],            // Password
                userParts[3],            // Name
                userParts[4],            // Email
                userParts[5],            // Phone
                userParts[6],            // Street Number
                userParts[7],            // Street
                userParts[8],            // City
                userParts[9]             // State
            );

        }
    }
}
