using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
    class AdminMapper : BaseMapper
    {
        // Method to get an admin by ID from a file
        public Admin GetAdminById(int id)
        {
            // Get the file path of the user data file
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // Check if the file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Find the specific line that matches the given admin ID
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                // If no matching line is found, return null
                if (userLine == null)
                {
                    return null;
                }

                // Split the user data and create a new Admin object
                string[] userParts = userLine.Split(',');
                return new Admin(
                    int.Parse(userParts[0]),  // Admin ID
                    userParts[1],             // Password
                    userParts[3]              // Name
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching admin by ID: {ex.Message}");
                throw;
            }
        }

        // Method to list all doctors from the user data file
        public List<Doctor> ListAllDoctors()
        {
            return ListAllUsers<Doctor>("2");  // '2' is the ID prefix for doctors
        }

        // Method to list all patients from the user data file
        public List<Patient> ListAllPatients()
        {
            return ListAllUsers<Patient>("1");  // '1' is the ID prefix for patients
        }

        // Generic method to list all users based on a prefix (doctors or patients)
        private List<T> ListAllUsers<T>(string idPrefix) where T : new()
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);
                List<T> users = new List<T>();

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 0 && parts[0].StartsWith(idPrefix))
                    {
                        T user = new T();
                        typeof(T).GetProperty("Id")?.SetValue(user, int.Parse(parts[0]));
                        typeof(T).GetProperty("Name")?.SetValue(user, parts[3]);
                        typeof(T).GetProperty("Email")?.SetValue(user, parts[4]);
                        typeof(T).GetProperty("Phone")?.SetValue(user, parts[5]);

                        if (typeof(T) == typeof(Doctor) || typeof(T) == typeof(Patient))
                        {
                            typeof(T).GetProperty("StreetNumber")?.SetValue(user, parts[6]);
                            typeof(T).GetProperty("Street")?.SetValue(user, parts[7]);
                            typeof(T).GetProperty("City")?.SetValue(user, parts[8]);
                            typeof(T).GetProperty("State")?.SetValue(user, parts[9]);
                        }

                        users.Add(user);
                    }
                }

                return users;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing users: {ex.Message}");
                throw;
            }
        }

        // Method to get a doctor by their ID
        public Doctor GetDoctorById(int doctorId)
        {
            return GetUserById<Doctor>(doctorId, "2");
        }

        // Method to get a patient by their ID
        public Patient GetPatientById(int patientId)
        {
            return GetUserById<Patient>(patientId, "1");
        }

        // Generic method to fetch user by ID (can be a doctor or patient)
        private T GetUserById<T>(int id, string idPrefix) where T : new()
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 0 && int.TryParse(parts[0], out int userId) && userId == id && parts[0].StartsWith(idPrefix))
                    {
                        T user = new T();
                        typeof(T).GetProperty("Id")?.SetValue(user, userId);
                        typeof(T).GetProperty("Name")?.SetValue(user, parts[3]);
                        typeof(T).GetProperty("Email")?.SetValue(user, parts[4]);
                        typeof(T).GetProperty("Phone")?.SetValue(user, parts[5]);

                        if (typeof(T) == typeof(Doctor) || typeof(T) == typeof(Patient))
                        {
                            typeof(T).GetProperty("StreetNumber")?.SetValue(user, parts[6]);
                            typeof(T).GetProperty("Street")?.SetValue(user, parts[7]);
                            typeof(T).GetProperty("City")?.SetValue(user, parts[8]);
                            typeof(T).GetProperty("State")?.SetValue(user, parts[9]);
                        }

                        return user;
                    }
                }

                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by ID: {ex.Message}");
                throw;
            }
        }

        // Method to get a list of appointments for a given patient ID
        public List<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Appointment file not found.");
                }

                List<Appointment> appointments = new List<Appointment>();
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 0 && int.TryParse(parts[1], out int pid) && pid == patientId)
                    {
                        appointments.Add(new Appointment
                        {
                            AppointmentId = int.Parse(parts[0]),
                            PatientId = pid,
                            DoctorId = int.Parse(parts[2]),
                            IllnessDescription = parts[3],
                        });
                    }
                }

                return appointments;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching appointments for patient {patientId}: {ex.Message}");
                throw;
            }
        }

        // Method to fetch the name of the doctor based on a given patient ID
        public string GetDoctorNameByPatientId(int patientId)
        {
            string appointmentFilePath = GetUserFilePath(APPOINTMENT_FILE_NAME);
            string userFilePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // 1. Find the doctor ID associated with the given patient ID
                if (!File.Exists(appointmentFilePath))
                {
                    throw new FileNotFoundException("Appointment file not found.");
                }

                string[] appointmentLines = File.ReadAllLines(appointmentFilePath);
                int? doctorId = null;

                foreach (var line in appointmentLines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 3 && int.TryParse(parts[1], out int foundPatientId) && foundPatientId == patientId)
                    {
                        if (int.TryParse(parts[2], out int foundDoctorId))
                        {
                            doctorId = foundDoctorId; // Doctor ID found
                            break;
                        }
                    }
                }

                // If no doctor ID was found, return a message
                if (!doctorId.HasValue)
                {
                    return "No doctor found for the given patient ID.";
                }

                // 2. Fetch the doctor's name based on the doctor ID from the user file
                if (!File.Exists(userFilePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                string[] userLines = File.ReadAllLines(userFilePath);

                foreach (var line in userLines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4 && int.TryParse(parts[0], out int foundDoctorId) && foundDoctorId == doctorId)
                    {
                        return parts[3]; // Return the doctor's name (assuming it's in the 4th column)
                    }
                }

                return "Doctor name not found.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching doctor name for patient {patientId}: {ex.Message}");
                throw;
            }
        }
    }
}
