using HospitalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class PatientPage
    {
        private Patient _patient;

        public PatientPage(Patient patient)
        {
            _patient = patient;
        }

        public void patientMainPage()
        {
            bool exit = false;

            while (!exit)
            {
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                        Patient Menu                      |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {_patient.Name}!");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List patient details");
                Console.WriteLine("2. List my doctor details");
                Console.WriteLine("3. List all appointments");
                Console.WriteLine("4. Book appointment");
                Console.WriteLine("5. Exit to login");
                Console.WriteLine("6. Exit System");

                // 处理用户输入
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListPatientDetails();
                        break;
                    case "2":
                        ListDoctorDetails();
                        break;
                    case "3":
                        ListAllAppointments();
                        break;
                    case "4":
                        BookAppointment();
                        break;
                    case "5":
                        exit = true; // 退出到登录界面
                        Console.Clear(); // 清屏
                        Program.StartApplication(); // 重新启动登录界面
                        break;
                    case "6":
                        Environment.Exit(0); // 退出系统
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        }

        private void ListPatientDetails()
        {
            PatientMapper patientMapper = new PatientMapper();
            Patient patient = patientMapper.displayPatientDetails(_patient.Id);  // 传入当前患者的 ID

            Console.Clear(); // 清屏，方便查看
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                         My Details                       |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.WriteLine("\n");

            Console.WriteLine($"{_patient.Name}'s Details");

            if (patient != null)
            {
                // 输出病人的所有信息
                Console.WriteLine($"Patient ID: {patient.Id}");
                Console.WriteLine($"Full Name: {patient.Name}");
                string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                Console.WriteLine($"Address: {fullAddress}");
                Console.WriteLine($"Email: {patient.Email}");
                Console.WriteLine($"Phone: {patient.Phone}");
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();  // 等待用户按键返回菜单
        }

        private void ListDoctorDetails()
        {
            PatientMapper patientMapper = new PatientMapper();
            DoctorMapper doctorMapper = new DoctorMapper();

            List<int> doctorIds = patientMapper.GetDoctorIdsByPatientId(_patient.Id);

            if (doctorIds.Count == 0)
            {
                Console.WriteLine("No doctors found for the patient.");
                Console.ReadKey(); // 等待用户按键返回菜单
                return;
            }

            List<Doctor> doctors = doctorMapper.GetDoctorsByIds(doctorIds);

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctor details found.");
            }
            else
            {
                Console.WriteLine("Doctor Details:");
                foreach (var doctor in doctors)
                {
                    Console.WriteLine(doctor.ToString()); // This assumes a ToString method in the Doctor class that formats the doctor's details
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(); // 等待用户按键返回菜单
        }

        private void ListAllAppointments()
        {
            PatientMapper patientMapper = new PatientMapper();
            DoctorMapper doctorMapper = new DoctorMapper();

            List<Appointment> patientAppointments = patientMapper.GetAppointmentsByPatientId(_patient.Id);

            if (patientAppointments.Count == 0)
            {
                Console.WriteLine("No past appointments found.");
            }
            else
            {
                Console.WriteLine("Past Appointments:");
                foreach (var appointment in patientAppointments)
                {
                    string patientName = patientMapper.GetPatientNameById(appointment.PatientId);
                    string doctorName = doctorMapper.GetDoctorNameById(appointment.DoctorId);

                    Console.WriteLine($"Appointment ID: {appointment.AppointmentId}");
                    Console.WriteLine($"Patient Name: {patientName}");
                    Console.WriteLine($"Doctor Name: {doctorName}");
                    Console.WriteLine($"Illness Description: {appointment.IllnessDescription}");
                    Console.WriteLine("-------------------------------------------------");
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(); // 等待用户按键返回菜单
        }


        private void BookAppointment()
        {
            // 获取 PatientMapper 实例
            PatientMapper mapper = new PatientMapper();

            // 从 appointment 文件中查找当前病人的预约信息
            var existingAppointments = mapper.GetAppointmentsByPatientId(_patient.Id);

            Doctor selectedDoctor;

            if (existingAppointments.Count == 0)
            {
                // 如果病人之前没有预约过医生，列出所有医生
                var doctors = mapper.GetAllDoctors();

                if (doctors.Count == 0)
                {
                    Console.WriteLine("No doctors available.");
                    return;
                }

                Console.WriteLine("Select a doctor by entering the number:");
                for (int i = 0; i < doctors.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {doctors[i].ToString()}");
                }

                int selectedDoctorIndex = int.Parse(Console.ReadLine()) - 1;
                selectedDoctor = doctors[selectedDoctorIndex];
            }
            else
            {
                // 如果病人已经有预约记录，直接获取之前绑定的医生
                int doctorId = existingAppointments[0].DoctorId; // 假设一个病人只绑定一个医生
                selectedDoctor = new DoctorMapper().GetDoctorById(doctorId);

                if (selectedDoctor == null)
                {
                    Console.WriteLine("Associated doctor not found.");
                    return;
                }

                Console.WriteLine($"You are currently associated with Dr. {selectedDoctor.Name}");
            }

            // 输入病症描述
            Console.WriteLine("Enter a description of your illness:");
            string illnessDescription = Console.ReadLine();

            // 创建新的 Appointment 对象并保存到文件
            Appointment appointment = new Appointment();
            int nextAppointmentId = appointment.getNextAppointmentId();
            appointment = new Appointment(nextAppointmentId, _patient.Id, selectedDoctor.Id, illnessDescription);
            mapper.SaveAppointment(appointment);

            Console.WriteLine("Appointment booked successfully!");
        }


    }
}
