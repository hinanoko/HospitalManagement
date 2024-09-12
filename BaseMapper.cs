using System;
using System.IO;
using System.Linq;
using System.Reflection; // 添加这一行

namespace Assignment1
{
    public class BaseMapper
    {
        protected const string USER_FILE_NAME = "datatext.txt";
        protected const string APPOINTMENT_FILE_NAME = "appointment.txt";

        protected string GetUserFilePath(string fileName)
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;
            return Path.Combine(projectDir, fileName);
        }

        public virtual bool IdExistsInFile(int id)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);
            if (!File.Exists(filePath)) return false;

            string[] lines = File.ReadAllLines(filePath);
            return lines.Any(line => line.Split(',')[0] == id.ToString());
        }
    }

}
