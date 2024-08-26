using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
    class PatientMapper
    {
        private const string USER_FILE_NAME = "datatext.txt";

        private string GetUserFilePath()
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;
            return Path.Combine(projectDir, USER_FILE_NAME);
        }

        public (bool isValid, int userType, string message) ValidateUser(int userid, string password)
        {
            string filePath = GetUserFilePath();

            try
            {
                if (!File.Exists(filePath))
                {
                    return (false, -1, $"User file not found at {filePath}");
                }

                string[] lines = File.ReadAllLines(filePath);

                // 首先查找用户名是否存在
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int id) && id == userid;
                });

                if (userLine == null)
                {
                    return (false, -1, "User does not exist.");
                }

                // 用户存在，验证密码
                string[] userParts = userLine.Split(',');
                if (userParts[1] == password)
                {
                    int userType = int.Parse(userParts[2]);
                    return (true, userType, "Login successful.");
                }
                else
                {
                    return (false, -1, "User exists, but password is incorrect.");
                }
            }
            catch (Exception ex)
            {
                return (false, -1, $"An error occurred: {ex.Message}");
            }
        }

        public bool IdExistsInFile(int id)
        {
            string filePath = GetUserFilePath();

            if (!File.Exists(filePath))
            {
                return false;
            }

            string[] lines = File.ReadAllLines(filePath);
            return lines.Any(line =>
            {
                string[] parts = line.Split(',');
                return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
            });
        }

        public Patient GetPatientById(int id)
        {
            string filePath = GetUserFilePath();

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
            return new Patient(
                int.Parse(userParts[0]),
                userParts[1],
                userParts[3]
            );
        }
    }
}