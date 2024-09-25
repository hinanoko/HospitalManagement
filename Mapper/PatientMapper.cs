using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
    class PatientMapper : BaseMapper
    {
        // Method to validate user credentials based on the provided user ID and password
        public (bool isValid, int userType, string message) ValidateUser(int userid, string password)
        {
            // Get the path of the user file using the base method
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // Check if the user file exists
                if (!File.Exists(filePath))
                {
                    return (false, -1, $"User file not found at {filePath}");
                }

                // Read all lines from the user file
                string[] lines = File.ReadAllLines(filePath);

                // Find the line corresponding to the given user ID
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int id) && id == userid;
                });

                // If no user is found, return a failure message
                if (userLine == null)
                {
                    return (false, -1, "User does not exist.");
                }

                // Split the found user line into its components
                string[] userParts = userLine.Split(',');

                // Validate the password and return the user type if successful
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
                // Return an error message in case of an exception
                return (false, -1, $"An error occurred: {ex.Message}");
            }
        }

        // Method to retrieve a patient by their unique ID
        public Patient GetPatientById(int id)
        {
            // Get the path of the user file using the base method
            string filePath = GetUserFilePath(USER_FILE_NAME);
            try
            {
                // Check if the user file exists
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                // Read all lines from the user file
                string[] lines = File.ReadAllLines(filePath);

                // Find the line corresponding to the given patient ID
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                // If no patient is found, return null
                if (userLine == null)
                {
                    return null;
                }

                // Split the found patient line into its components and return a new Patient object
                string[] userParts = userLine.Split(',');
                return new Patient(
                    userParts[1], // Password
                    userParts[3], // Name
                    userParts[4], // Email
                    userParts[5], // Phone
                    userParts[6], // StreetNumber
                    userParts[7], // Street
                    userParts[8], // City
                    userParts[9]  // State
                )
                {
                    Id = int.Parse(userParts[0]) // Set the ID
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting patient by ID: {ex.Message}");
                return null;
            }
        }

        // Method to save patient information to a file
        public int SavePatient(Patient patient)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // Format patient data as a CSV line
                string patientData = $"{patient.Id},{patient.Password},1,{patient.Name},{patient.Email},{patient.Phone},{patient.StreetNumber},{patient.Street},{patient.City},{patient.State}";

                // Write the data to the file, appending a new line
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine();
                    writer.Write(patientData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the patient: {ex.Message}");
            }

            return patient.Id;
        }

        // Method to display patient details by their ID
        public Patient displayPatientDetails(int id)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // Check if the user file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("User file not found.");
                    return null;
                }

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Find the line corresponding to the given patient ID
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                // If no patient is found, return null
                if (userLine == null)
                {
                    Console.WriteLine("Patient not found.");
                    return null;
                }

                // Return a new Patient object based on the data found in the file
                string[] userParts = userLine.Split(',');
                return new Patient
                {
                    Id = int.Parse(userParts[0]),
                    Name = userParts[3],
                    Email = userParts[4],
                    Phone = userParts[5],
                    StreetNumber = userParts[6],
                    Street = userParts[7],
                    City = userParts[8],
                    State = userParts[9]
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying patient details: {ex.Message}");
                return null;
            }
        }

        // Method to retrieve all doctors from the file
        public List<Doctor> GetAllDoctors()
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);
            List<Doctor> doctors = new List<Doctor>();

            try
            {
                // Check if the doctor file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Doctor file not found.");
                    return doctors;
                }

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Loop through the lines and create a Doctor object for each relevant entry
                foreach (string line in lines)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length > 2 && (parts[0].StartsWith("2") || parts[2] == "2"))
                    {
                        int id = int.Parse(parts[0]);
                        string password = parts[1];
                        string name = parts[3];
                        string email = parts[4];
                        string phone = parts[5];
                        string streetNumber = parts[6];
                        string street = parts[7];
                        string city = parts[8];
                        string state = parts[9];

                        Doctor doctor = new Doctor(password, name, email, phone, streetNumber, street, city, state);
                        doctor.Id = id; // Assign the correct ID
                        doctors.Add(doctor);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving all doctors: {ex.Message}");
            }

            return doctors;
        }

        // Method to save appointment information to a file
        public void SaveAppointment(Appointment appointment)
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                // Format appointment data as a CSV line
                string appointmentData = $"{appointment.AppointmentId},{appointment.PatientId},{appointment.DoctorId},{appointment.IllnessDescription}";
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine(appointmentData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the appointment: {ex.Message}");
            }
        }

        // Method to retrieve appointments for a specific patient ID
        public List<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                // Check if the appointments file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No appointments file found.");
                    return appointments;
                }

                // Read all lines from the file and find relevant appointments
                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4 && int.TryParse(parts[1], out int currentPatientId) && currentPatientId == patientId)
                    {
                        int appointmentId = int.Parse(parts[0]);
                        int doctorId = int.Parse(parts[2]);
                        string illnessDescription = parts[3];
                        appointments.Add(new Appointment(appointmentId, patientId, doctorId, illnessDescription));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving appointments: {ex.Message}");
            }

            return appointments;
        }

        // Method to get a patient's name by their ID
        public string GetPatientNameById(int patientId)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // Check if the patient file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Patient file not found.");
                    return null;
                }

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Find the line corresponding to the given patient ID
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 3 && int.TryParse(parts[0], out int currentPatientId) && currentPatientId == patientId)
                    {
                        return parts[3]; // Assuming the name is in the fourth position
                    }
                }

                Console.WriteLine("Patient not found.");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving patient name by ID: {ex.Message}");
                return null;
            }
        }

        // Method to get the IDs of doctors who have appointments with a given patient
        public List<int> GetDoctorIdsByPatientId(int patientId)
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                // Check if the appointment file exists
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Appointment file not found.");
                    return new List<int>();
                }

                // Read all lines from the file and collect doctor IDs
                string[] lines = File.ReadAllLines(filePath);
                HashSet<int> doctorIds = new HashSet<int>();

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 2 && int.TryParse(parts[1], out int currentPatientId) && currentPatientId == patientId)
                    {
                        if (int.TryParse(parts[2], out int doctorId))
                        {
                            doctorIds.Add(doctorId);
                        }
                    }
                }

                return doctorIds.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving doctor IDs by patient ID: {ex.Message}");
                return new List<int>();
            }
        }
    }
}
