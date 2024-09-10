using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class DoctorMapper
    {
        private const string USER_FILE_NAME = "datatext.txt";
        private const string APPOINTMENT_FILE_NAME = "appointment.txt";

        private string GetUserFilePath(string fileName)
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;
            return Path.Combine(projectDir, fileName);
        }

        public Doctor GetDoctorById(int id)
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

        public void SaveDoctor(Doctor doctor)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // 生成一行文本来表示Patient对象
                string doctorData = $"{doctor.Id},{doctor.Password},2,{doctor.Name},{doctor.Email},{doctor.Phone},{doctor.StreetNumber},{doctor.Street},{doctor.City},{doctor.State}";

                // 将Patient数据写入文件
                using (StreamWriter writer = new StreamWriter(filePath, append: true))
                {
                    writer.WriteLine();
                    writer.Write(doctorData);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while saving the patient: {ex.Message}");
            }
        }

        public string GetDoctorNameById(int doctorId)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Doctor file not found.");
                return null;
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

            Console.WriteLine("Doctor not found.");
            return null;
        }


        public List<Doctor> GetDoctorsByIds(List<int> doctorIds)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Doctor file not found.");
                return new List<Doctor>();
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

        public Doctor displayDoctorDetails(int id)
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
                Console.WriteLine("Doctor not found.");
                return null;
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

        public List<Patient> GetPatientsByDoctorId(int doctorId)
        {
            List<Patient> patients = new List<Patient>();
            string appointmentFilePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            if (!File.Exists(appointmentFilePath))
            {
                Console.WriteLine("Appointment file not found.");
                return patients;
            }

            string[] appointmentLines = File.ReadAllLines(appointmentFilePath);

            foreach (var line in appointmentLines)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 3 && int.TryParse(parts[2], out int foundDoctorId) && foundDoctorId == doctorId)
                {
                    int patientId = int.Parse(parts[1]);
                    Patient patient = GetPatientById(patientId);
                    if (patient != null)
                    {
                        patients.Add(patient);
                    }
                }
            }

            return patients;
        }

        private Patient GetPatientById(int patientId)
        {
            string patientFilePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(patientFilePath))
            {
                Console.WriteLine("Patient file not found.");
                return null;
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

            return null;
        }

        public List<Appointment> GetAppointmentsByDoctorId(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Appointment file not found.");
                return appointments;
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

        public List<Appointment> GetAppointmentsByDoctorAndPatientId(int doctorId, int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            string[] lines = File.ReadAllLines(GetUserFilePath(APPOINTMENT_FILE_NAME));

            foreach (string line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length >= 4 && int.TryParse(parts[1], out int currentPatientId) && int.TryParse(parts[2], out int currentDoctorId))
                {
                    if (currentPatientId == patientId && currentDoctorId == doctorId)
                    {
                        int appointmentId = int.Parse(parts[0]);
                        string illnessDescription = parts[3];
                        appointments.Add(new Appointment(appointmentId, patientId, doctorId, illnessDescription));
                    }
                }
            }

            return appointments;
        }

       
    }
}
