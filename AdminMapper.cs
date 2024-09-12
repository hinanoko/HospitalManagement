using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Assignment1
{
    class AdminMapper : BaseMapper
    {
        public Admin GetAdminById(int id)
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
                    return null;
                }

                string[] userParts = userLine.Split(',');
                return new Admin(
                    int.Parse(userParts[0]),
                    userParts[1], // Password
                    userParts[3]  // Name
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching admin by ID: {ex.Message}");
                throw;
            }
        }

        public List<Doctor> ListAllDoctors()
        {
            return ListAllUsers<Doctor>("2");
        }

        public List<Patient> ListAllPatients()
        {
            return ListAllUsers<Patient>("1");
        }

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
                            typeof(T).GetProperty("Street")?.SetValue(user, parts[6]);
                            typeof(T).GetProperty("City")?.SetValue(user, parts[7]);
                            typeof(T).GetProperty("State")?.SetValue(user, parts[8]);
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

        public Doctor GetDoctorById(int doctorId)
        {
            return GetUserById<Doctor>(doctorId, "2");
        }

        public Patient GetPatientById(int patientId)
        {
            return GetUserById<Patient>(patientId, "1");
        }

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
                            typeof(T).GetProperty("Street")?.SetValue(user, parts[6]);
                            typeof(T).GetProperty("City")?.SetValue(user, parts[7]);
                            typeof(T).GetProperty("State")?.SetValue(user, parts[8]);
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

        public string GetDoctorNameByPatientId(int patientId)
        {
            string appointmentFilePath = GetUserFilePath(APPOINTMENT_FILE_NAME);
            string userFilePath = GetUserFilePath(USER_FILE_NAME);

            try
            {
                // 1. 查找病人ID对应的医生ID
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
                            doctorId = foundDoctorId; // 找到对应的医生ID
                            break;
                        }
                    }
                }

                // 如果没有找到医生ID，返回提示
                if (!doctorId.HasValue)
                {
                    return "No doctor found for the given patient ID.";
                }

                // 2. 根据医生ID在用户文件中查找对应的医生名字
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
                        return parts[3]; // 返回医生名字 (假设名字是第4个字段)
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
