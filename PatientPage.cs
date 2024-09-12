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
                try
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
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }

        private void ListPatientDetails()
        {
            try
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patient details: {ex.Message}");
                Console.ReadKey(); // 等待用户按键返回菜单
            }
        }

        private void ListDoctorDetails()
        {
            try
            {
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         My Doctor                        |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                Console.WriteLine("Your doctor:");
                Console.WriteLine("\n");

                Console.WriteLine("Name                | Email Address          | Phone       | Address");
                Console.WriteLine("----------------------------------------------------------------------------------------------");

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
                foreach (var doctor in doctors)
                {
                        string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";

                        // 格式化输出医生信息
                        Console.WriteLine($"{doctor.Name,-19} | {doctor.Email,-22} | {doctor.Phone,-11} | {fullAddress,-35}");
                    }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(); // 等待用户按键返回菜单
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor details: {ex.Message}");
                Console.ReadKey(); // 等待用户按键返回菜单
            }
        }

        private void ListAllAppointments()
        {
            try
            {

                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                      My Appointments                     |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                Console.WriteLine($"Appointments for {_patient.Name}");
                Console.WriteLine("\n");

                PatientMapper patientMapper = new PatientMapper();
            DoctorMapper doctorMapper = new DoctorMapper();

            List<Appointment> patientAppointments = patientMapper.GetAppointmentsByPatientId(_patient.Id);

            if (patientAppointments.Count == 0)
            {
                Console.WriteLine("No past appointments found.");
            }
            else
            {
                    Console.WriteLine("Doctor                | Patient             | Description");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    foreach (var appointment in patientAppointments)
                {
                    string patientName = patientMapper.GetPatientNameById(appointment.PatientId);
                    string doctorName = doctorMapper.GetDoctorNameById(appointment.DoctorId);

                        Console.WriteLine($"{doctorName,-21} | {patientName,-19} | {appointment.IllnessDescription,-11} ");
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(); // 等待用户按键返回菜单
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointments: {ex.Message}");
                Console.ReadKey(); // 等待用户按键返回菜单
            }
        }


        private void BookAppointment()
        {
            try
            {
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                      Book Appointment                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");
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

                Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");
                for (int i = 0; i < doctors.Count; i++)
                {
                        string fullAddress = $"{doctors[i].StreetNumber} {doctors[i].Street}, {doctors[i].City}, {doctors[i].State}";
                        Console.WriteLine($"{i + 1} {doctors[i].Name} | {doctors[i].Email} | {doctors[i].Phone} | {fullAddress} ");
                }
                    Console.WriteLine("Please choose a doctor: ");
                int selectedDoctorIndex = int.Parse(Console.ReadLine()) - 1;
                selectedDoctor = doctors[selectedDoctorIndex];
                    Console.WriteLine($"You are book a new appointment with {selectedDoctor.Name}");

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

                Console.WriteLine($"You are booking a new appointment with Dr. {selectedDoctor.Name}");
            }

            // 输入病症描述
            Console.WriteLine("Description of the oppintment:");
            string illnessDescription = Console.ReadLine();

            // 创建新的 Appointment 对象并保存到文件
            Appointment appointment = new Appointment();
            int nextAppointmentId = appointment.getNextAppointmentId();
            appointment = new Appointment(nextAppointmentId, _patient.Id, selectedDoctor.Id, illnessDescription);
            mapper.SaveAppointment(appointment);

                Console.WriteLine("The appointment has been booked successfully");

                    Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey(); // 等待用户按键返回菜单
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error booking appointment: {ex.Message}");
            }
        }


    }
}
