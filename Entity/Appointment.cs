using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Appointment
    {
        public int AppointmentId { get; set; } // Stores the ID of the appointment
        public int PatientId { get; set; } // Stores the ID of the patient
        public int DoctorId { get; set; } // Stores the ID of the doctor
        public string IllnessDescription { get; set; } // Describes the patient's illness

        // Default constructor
        public Appointment() { }

        // Parameterized constructor to initialize an appointment
        public Appointment(int appointmentId, int patientId, int doctorId, string illnessDescription)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            IllnessDescription = illnessDescription;
        }

        // Constant for the file name where appointment data is stored
        private const string APPOINTMENT_FILE_NAME = "appointment.txt";

        // Method to get the file path of the appointment file
        private string GetUserFilePath(string fileName)
        {
            // Get the directory of the executing assembly
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Get the parent directory of the project (going up three levels)
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;

            // Combine the project directory with the file name
            return Path.Combine(projectDir, fileName);
        }

        // Method to get the next available appointment ID
        public int getNextAppointmentId()
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            // If the appointment file doesn't exist, start from ID 10001
            if (!File.Exists(filePath))
            {
                return 10001;
            }

            // Read all lines from the appointment file
            string[] lines = File.ReadAllLines(filePath);

            // If the file is empty, start from ID 10001
            if (lines.Length == 0)
            {
                return 10001;
            }

            // Get the last line of the file
            string lastLine = lines.Last();

            // Split the line by commas to extract the appointment data
            string[] parts = lastLine.Split(',');

            // If the last line contains valid data, parse the last appointment ID
            if (parts.Length > 0 && int.TryParse(parts[0], out int lastId))
            {
                return lastId + 1; // Return the next appointment ID
            }

            // If there was an issue with the last line, start from ID 10001
            return 10001;
        }

        // Override the ToString method to display the appointment information
        public override string ToString()
        {
            return $"Appointment ID: {AppointmentId}, Patient ID: {PatientId}, Doctor ID: {DoctorId}, Illness: {IllnessDescription}";
        }
    }
}
