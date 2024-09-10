using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
    class PatientMapper
    {
        private const string USER_FILE_NAME = "datatext.txt";
        private const string APPOINTMENT_FILE_NAME = "appointment.txt";

        private string GetUserFilePath(string fileName)
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;
            return Path.Combine(projectDir, fileName);
        }

        public (bool isValid, int userType, string message) ValidateUser(int userid, string password)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

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
            string filePath = GetUserFilePath(USER_FILE_NAME);

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
                Id = int.Parse(userParts[0]) // 设置 ID
            };
        }

        public void SavePatient(Patient patient)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // 生成一行文本来表示Patient对象
                string patientData = $"{patient.Id},{patient.Password},1,{patient.Name},{patient.Email},{patient.Phone},{patient.StreetNumber},{patient.Street},{patient.City},{patient.State}";

                // 将Patient数据写入文件
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
        }

        public Patient displayPatientDetails(int id)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("User file not found.");
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

            var userLine = lines.FirstOrDefault(line =>
            {
                string[] parts = line.Split(',');
                return parts.Length > 0 && int.TryParse(parts[0], out int existingId) && existingId == id;
            });

            if (userLine == null)
            {
                Console.WriteLine("Patient not found.");
                return null;
            }

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

        public List<Doctor> GetAllDoctors()
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);
            List<Doctor> doctors = new List<Doctor>();

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Doctor file not found.");
                return doctors;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');
                if (parts.Length > 2 && (parts[0].StartsWith("2") || parts[2] == "2"))
                {
                    int id = int.Parse(parts[0]);
                    string password = parts[1];
                    string name = parts[2];
                    string email = parts[3];
                    string phone = parts[4];
                    string streetNumber = parts[5];
                    string street = parts[6];
                    string city = parts[7];
                    string state = parts[8];

                    Doctor doctor = new Doctor(password, name, email, phone, streetNumber, street, city, state);
                    doctor.Id = id; // 直接赋值ID，而不是生成新的ID
                    Console.WriteLine(doctor);  
                    doctors.Add(doctor);
                }
            }

            return doctors;
        }

        public void SaveAppointment(Appointment appointment)
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            try
            {
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

       

            public List<Appointment> GetAppointmentsByPatientId(int patientId)
            {
                List<Appointment> appointments = new List<Appointment>();

                string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

                if (!File.Exists(filePath))
                {
                    Console.WriteLine("No appointments file found.");
                    return appointments;
                }

                string[] lines = File.ReadAllLines(filePath);

                foreach (var line in lines)
                {
                    string[] parts = line.Split(',');

                    if (parts.Length >= 4 && int.TryParse(parts[1], out int currentPatientId) &&
                        currentPatientId == patientId)
                    {
                        int appointmentId = int.Parse(parts[0]);
                        int doctorId = int.Parse(parts[2]);
                        string illnessDescription = parts[3];
                        appointments.Add(new Appointment(appointmentId, patientId, doctorId, illnessDescription));
                    }
                }

                return appointments;
            }


        public string GetPatientNameById(int patientId)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Patient file not found.");
                return null;
            }

            string[] lines = File.ReadAllLines(filePath);

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


        public List<int> GetDoctorIdsByPatientId(int patientId)
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Appointment file not found.");
                return new List<int>();
            }

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

    }
}