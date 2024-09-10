using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Appointment
    {
        public int AppointmentId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string IllnessDescription { get; set; }

        public Appointment() { }

        public Appointment(int appointmentId, int patientId, int doctorId, string illnessDescription)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            IllnessDescription = illnessDescription;
        }

        private const string APPOINTMENT_FILE_NAME = "appointment.txt";

        private string GetUserFilePath(string fileName)
        {
            string executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string projectDir = Directory.GetParent(executingDir).Parent.Parent.FullName;
            return Path.Combine(projectDir, fileName);
        }

        public int getNextAppointmentId()
        {
            string filePath = GetUserFilePath(APPOINTMENT_FILE_NAME);

            if (!File.Exists(filePath))
            {
                // 文件不存在，返回第一个预约ID
                return 10001;
            }

            string[] lines = File.ReadAllLines(filePath);
            if (lines.Length == 0)
            {
                return 10001;
            }

            // 获取最后一行数据
            string lastLine = lines.Last();
            string[] parts = lastLine.Split(',');

            if (parts.Length > 0 && int.TryParse(parts[0], out int lastId))
            {
                return lastId + 1;
            }

            // 如果最后一行数据有问题，从10001开始
            return 10001;
        }

        public override string ToString()
        {
            return $"Appointment ID: {AppointmentId}, Patient ID: {PatientId}, Doctor ID: {DoctorId}, Illness: {IllnessDescription}";
        }

    }
}
