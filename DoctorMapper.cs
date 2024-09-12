using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Assignment1
{
    class DoctorMapper : BaseMapper
    {
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

                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                if (userLine == null)
                {
                    throw new Exception("Doctor not found.");
                }

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
                    Id = int.Parse(userParts[0]) // 设置 ID
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor by ID: {ex.Message}");
                return null;
            }
        }

        public override bool IdExistsInFile(int id)
        {
            // 重用 BaseMapper 中的 IdExistsInFile
            return base.IdExistsInFile(id);
        }

        public void SaveDoctor(Doctor doctor)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // 生成一行文本来表示Doctor对象
                string doctorData = $"{doctor.Id},{doctor.Password},2,{doctor.Name},{doctor.Email},{doctor.Phone},{doctor.StreetNumber},{doctor.Street},{doctor.City},{doctor.State}";

                // 将Doctor数据写入文件
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
        }

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

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length > 1 && int.TryParse(parts[0], out int currentDoctorId) && currentDoctorId == doctorId)
                    {
                        return parts[3]; // Assuming the name is in the second position
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

        public List<Patient> GetPatientsByDoctorId(int doctorId)
        {
            List<Patient> patients = new List<Patient>();
            HashSet<int> addedPatientIds = new HashSet<int>(); // 用于存储已添加的病人ID
            string appointmentFilePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
                if (!File.Exists(appointmentFilePath))
                {
                    throw new FileNotFoundException("Appointment file not found.");
                }

                string[] appointmentLines = File.ReadAllLines(appointmentFilePath);

                foreach (var line in appointmentLines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 3 && int.TryParse(parts[2], out int foundDoctorId) && foundDoctorId == doctorId)
                    {
                        int patientId = int.Parse(parts[1]);
                        // 检查病人ID是否已经添加过
                        if (!addedPatientIds.Contains(patientId))
                        {
                            Patient patient = GetPatientById(patientId);
                            if (patient != null)
                            {
                                patients.Add(patient);
                                addedPatientIds.Add(patientId); // 添加病人ID到HashSet中，防止重复添加
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

                var userLine = lines.FirstOrDefault(line =>
                {
                    string[] parts = line.Split(',');
                    return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
                });

                if (userLine == null)
                {
                    throw new Exception("Doctor not found.");
                }

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
