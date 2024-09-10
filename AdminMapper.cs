using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class AdminMapper
    {
        private const string USER_FILE_NAME = "datatext.txt";
        private const string APPOINTMENT_FILE_NAME = "appointment.txt";

        private string GetUserFilePath(string fileName)
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;
            return Path.Combine(projectDir, fileName);
        }

        public Admin GetAdminById(int id)
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
            return new Admin(
                int.Parse(userParts[0]),
                userParts[1],
                userParts[3]
            );
        }
        public List<Doctor> ListAllDoctors()
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("User file not found.");
            }

            string[] lines = File.ReadAllLines(filePath);
            List<Doctor> doctors = new List<Doctor>();

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length > 0 && parts[0].StartsWith("2"))
                {
                    Doctor doctor = new Doctor
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[3], // Assuming the name is in the fourth position
                        Email = parts[4], // Assuming the email is in the fifth position
                        Phone = parts[5], // Assuming the phone is in the sixth position
                        // Additional fields can be added as needed
                        Street = parts[6],
                        City = parts[7],
                        State = parts[8],
                    };
                    doctors.Add(doctor);
                }
            }

            return doctors;
        }


        public List<Patient> ListAllPatients()
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("User file not found.");
            }

            string[] lines = File.ReadAllLines(filePath);
            List<Patient> patients = new List<Patient>();

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length > 0 && parts[0].StartsWith("1"))
                {
                    Patient patient = new Patient
                    {
                        Id = int.Parse(parts[0]),
                        Name = parts[3], // Assuming the name is in the fourth position
                        Email = parts[4], // Assuming the email is in the fifth position
                        Phone = parts[5], // Assuming the phone is in the sixth position
                        // Additional fields can be added as needed
                        Street = parts[6],
                        City = parts[7],
                        State = parts[8],
                    };
                    patients.Add(patient);
                }
            }

            return patients;
        }

        public Doctor GetDoctorById(int doctorId)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("User file not found.");
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == doctorId && parts[0].StartsWith("2"))
                {
                    return new Doctor
                    {
                        Id = id,
                        Name = parts[3], // Assuming the name is in the fourth position
                        Email = parts[4], // Assuming the email is in the fifth position
                        Phone = parts[5], // Assuming the phone is in the sixth position
                        // Add other fields as necessary
                        Street = parts[6],
                        City = parts[7],
                        State = parts[8],
                    };
                }
            }

            return null;
        }

        public Patient GetPatientById(int patientId)
        {
            string filePath = GetUserFilePath(USER_FILE_NAME);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("User file not found.");
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (var line in lines)
            {
                string[] parts = line.Split(',');

                if (parts.Length > 0 && int.TryParse(parts[0], out int id) && id == patientId && parts[0].StartsWith("1"))
                {
                    return new Patient
                    {
                        Id = id,
                        Name = parts[3], // Assuming the name is in the fourth position
                        Email = parts[4], // Assuming the email is in the fifth position
                        Phone = parts[5], // Assuming the phone is in the sixth position
                        // Add other fields as necessary
                    };
                }
            }

            return null;
        }

        public List<Appointment> GetAppointmentsByPatientId(int patientId)
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

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
    }
}
