using System;
using System.IO;
using System.Linq;
using System.Reflection; // Importing the required namespace for getting the executing assembly location

namespace Assignment1
{
    // Base class for handling file operations, such as reading and writing to text files
    public class BaseMapper
    {
        // File name constants used for user and appointment data storage
        protected const string USER_FILE_NAME = "datatext.txt"; // Stores user data
        protected const string APPOINTMENT_FILE_NAME = "appointment.txt"; // Stores appointment data

        // Method to get the full file path based on the file name
        protected string GetUserFilePath(string fileName)
        {
            // Get the directory where the executable is running from
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Traverse up to the root directory of the project (three levels up)
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;

            // Return the combined file path
            return Path.Combine(projectDir, fileName);
        }

        // Virtual method to check if an ID exists in the user file (can be overridden by subclasses)
        public virtual bool IdExistsInFile(int id)
        {
            // Get the file path of the user data file
            string filePath = GetUserFilePath(USER_FILE_NAME);

            // If the file does not exist, return false (ID not found)
            if (!File.Exists(filePath)) return false;

            // Read all lines from the user file
            string[] lines = File.ReadAllLines(filePath);

            // Check if any line has an ID that matches the input ID
            return lines.Any(line => line.Split(',')[0] == id.ToString());
        }
    }
}
