using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
	class ReceptionistMapper: BaseMapper
	{
        public Receptionist GetReceptionistById(int id)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("User file not found.");
            }

            string[] lines = File.ReadAllLines(filePath);

            var userLine = lines.FirstOrDefault(line =>
            {
                string[] parts = line.Split(',');
                return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
            });

            if (userLine == null)
            {
                return null;
            }

            string[] userParts = userLine.Split(',');
            return new Receptionist(
                int.Parse(userParts[0]),
                userParts[1], // Password
                userParts[3]  // Name
            );
        }

    }
}
