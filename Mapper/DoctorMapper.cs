using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment1
{
    // DoctorMapper class handles the retrieval and storage of Doctor data
    class DoctorMapper : BaseMapper
    {
        // Retrieves a Doctor by their unique ID
        public Doctor GetDoctorById(int id)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);

                // Find the first line that matches the ID
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                if (userLine == null)
                {
                    throw new Exception("Doctor not found.");
                }

                // Parse the doctor data
                string[] userParts = userLine.Split(',');
                return new Doctor(
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
                    Id = int.Parse(userParts[0]) // Set the doctor ID
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
                return null;
            }
        }

        // Overrides BaseMapper method to check if an ID already exists in the file
        public override bool IdExistsInFile(int id)
        {
            return base.IdExistsInFile(id);
        }

        // Saves doctor information to the file
        public int SaveDoctor(Doctor doctor)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // Create a string representation of the doctor object to save in the file
                string doctorData = $"{doctor.Id},{doctor.Password},2,{doctor.Name},{doctor.Email},{doctor.Phone},{doctor.StreetNumber},{doctor.Street},{doctor.City},{doctor.State}";

                // Append the doctor's data to the file
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine();
                    writer.Write(doctorData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the doctor: {ex.Message}");
            }
            return doctor.Id;
        }

        // Retrieves the doctor's name by their unique ID
        public string GetDoctorNameById(int doctorId)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Doctor file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);

                // Loop through lines to find a matching doctor ID
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 1 && int.TryParse(parts[0], out int currentDoctorId) && currentDoctorId == doctorId)
                    {
                        return parts[3]; // Name is stored at index 3
                    }
                }

                throw new Exception("Doctor not found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor name by ID: {ex.Message}");
                return null;
            }
        }

        // Retrieves a list of doctors based on a list of IDs
        public List<Doctor> GetDoctorsByIds(List<int> doctorIds)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Doctor file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);
                List<Doctor> doctors = new List<Doctor>();

                // Loop through lines to find matching doctors based on IDs
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 0 && int.TryParse(parts[0], out int doctorId) && doctorIds.Contains(doctorId))
                    {
                        Doctor doctor = new Doctor
                        {
                            Id = doctorId,
                            Password = parts[1],
                            Name = parts[3],
                            Email = parts[4],
                            Phone = parts[5],
                            StreetNumber = parts[6],
                            Street = parts[7],
                            City = parts[8],
                            State = parts[9]
                        };

                        doctors.Add(doctor);
                    }
                }

                return doctors;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctors by IDs: {ex.Message}");
                return new List<Doctor>();
            }
        }

        // Retrieves a list of patients that are associated with a particular doctor based on the doctor's ID
        public List<Patient> GetPatientsByDoctorId(int doctorId)
        {
            List<Patient> patients = new List<Patient>();
            HashSet<int> addedPatientIds = new HashSet<int>(); // Keeps track of patient IDs that are already added
            string appointmentFilePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                if (!File.Exists(appointmentFilePath))
                {
                    throw new FileNotFoundException("Appointment file not found.");
                }

                string[] appointmentLines = File.ReadAllLines(appointmentFilePath);

                // Loop through appointments to find patients associated with the doctor
                foreach (var line in appointmentLines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 3 && int.TryParse(parts[2], out int foundDoctorId) && foundDoctorId == doctorId)
                    {
                        int patientId = int.Parse(parts[1]);
                        // Ensure that the patient is not added twice
                        if (!addedPatientIds.Contains(patientId))
                        {
                            Patient patient = GetPatientById(patientId);
                            if (patient != null)
                            {
                                patients.Add(patient);
                                addedPatientIds.Add(patientId);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patients by doctor ID: {ex.Message}");
            }

            return patients;
        }

        // Retrieves a list of appointments based on the doctor's ID
        public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Appointment file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);

                // Loop through lines to find appointments associated with the doctor
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4 && int.TryParse(parts[2], out int docId) && docId == doctorId)
                    {
                        Appointment appointment = new Appointment
                        {
                            AppointmentId = int.Parse(parts[0]),
                            PatientId = int.Parse(parts[1]),
                            DoctorId = docId,
                            IllnessDescription = parts[3]
                        };
                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointments by doctor ID: {ex.Message}");
            }

            return appointments;
        }

        // Displays the list of appointments for a particular doctor
        public void ListAppointmentsByDoctorId(int doctorId)
        {
            List<Appointment> appointments = GetAppointmentsByDoctorId(doctorId);

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found for this doctor.");
            }
            else
            {
                Console.WriteLine("Appointments involving the doctor:");
                foreach (var appointment in appointments)
                {
                    Console.WriteLine(appointment.ToString());
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        // Displays details of a doctor by ID
        public Doctor displayDoctorDetails(int id)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("User file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);

                // Find the doctor line matching the ID
                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                if (userLine == null)
                {
                    throw new Exception("Doctor not found.");
                }

                // Parse the doctor details
                string[] userParts = userLine.Split(',');

                return new Doctor
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
                Console.WriteLine($"Error displaying doctor details: {ex.Message}");
                return null;
            }
        }

        // Helper method to retrieve a patient by their ID
        private Patient GetPatientById(int patientId)
        {
            string patientFilePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                if (!File.Exists(patientFilePath))
                {
                    throw new FileNotFoundException("Patient file not found.");
                }

                string[] patientLines = File.ReadAllLines(patientFilePath);

                // Loop through lines to find the matching patient
                foreach (var line in patientLines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 0 && int.TryParse(parts[0], out int foundPatientId) && foundPatientId == patientId)
                    {
                        return new Patient
                        {
                            Id = foundPatientId,
                            Name = parts[3],
                            Email = parts[4],
                            Phone = parts[5],
                            StreetNumber = parts[6],
                            Street = parts[7],
                            City = parts[8],
                            State = parts[9]
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patient by ID: {ex.Message}");
            }

            return null;
        }

        // Retrieves appointments for a specific doctor and patient combination
        public List<Appointment> GetAppointmentsByDoctorAndPatientId(int doctorId, int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                if (!File.Exists(filePath))
                {
                    throw new FileNotFoundException("Appointment file not found.");
                }

                string[] lines = File.ReadAllLines(filePath);

                // Loop through lines to find matching doctor and patient appointments
                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4 && int.TryParse(parts[1], out int patId) && int.TryParse(parts[2], out int docId) && patId == patientId && docId == doctorId)
                    {
                        Appointment appointment = new Appointment
                        {
                            AppointmentId = int.Parse(parts[0]),
                            PatientId = patId,
                            DoctorId = docId,
                            IllnessDescription = parts[3]
                        };

                        appointments.Add(appointment);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointments by doctor and patient IDs: {ex.Message}");
            }

            return appointments;
        }
    }
}
