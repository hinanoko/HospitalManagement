using HospitalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class DoctorPage
    {
        private Doctor _doctor;

        public DoctorPage(Doctor doctor)
        {
            _doctor = doctor;
        }

        public void doctorMainPage()
        {
            bool exit = false;

            while (!exit)
            {

                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         Doctor Menu                      |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {_doctor.Name}!");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List doctor details");
                Console.WriteLine("2. List patients");
                Console.WriteLine("3. List appointments");
                Console.WriteLine("4. Check particular patient");
                Console.WriteLine("5. List appointments with patient");
                Console.WriteLine("6. Exit to login");
                Console.WriteLine("7. Exit System");

                // 处理用户输入
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListDoctorDetails();
                        break;
                    case "2":
                        ListPatients();
                        break;
                    case "3":
                        ListAppointments();
                        break;
                    case "4":
                        CheckParticularPatient();
                        break;
                    case "5":
                        ListAppointmentsWithPatient();
                        break;
                    case "6":
                        exit = true; // 退出到登录界面
                        Console.Clear(); // 清屏
                        Program.StartApplication(); // 重新启动登录界面
                        break;
                    case "7":
                        Environment.Exit(0); // 退出系统
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            }
        
        }

        private void ListDoctorDetails()
        {
            DoctorMapper doctorMapper = new DoctorMapper();
            Doctor doctor = doctorMapper.displayDoctorDetails(_doctor.Id);  // 传入当前患者的 ID

            Console.Clear(); // 清屏，方便查看
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                         My Details                       |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.WriteLine("\n");

            Console.WriteLine($"{_doctor.Name}'s Details");

            if (doctor != null)
            {
                // 输出病人的所有信息
                Console.WriteLine($"Patient ID: {doctor.Id}");
                Console.WriteLine($"Full Name: {doctor.Name}");
                string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";
                Console.WriteLine($"Address: {fullAddress}");
                Console.WriteLine($"Email: {doctor.Email}");
                Console.WriteLine($"Phone: {doctor.Phone}");
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();  // 等待用户按键返回菜单
        }

        private void ListPatients()
        {
            DoctorMapper doctorMapper = new DoctorMapper();
            List<Patient> patients = doctorMapper.GetPatientsByDoctorId(_doctor.Id);

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found for this doctor.");
            }
            else
            {
                string doctorName = doctorMapper.GetDoctorNameById(_doctor.Id);

                // 输出病人信息并显示医生名字
                foreach (var patient in patients)
                {
                    Console.WriteLine($"Doctor: {doctorName}, {patient.ToString()}");
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        public void ListAppointments()
        {
            DoctorMapper doctorMapper = new DoctorMapper();
            PatientMapper patientMapper = new PatientMapper(); 
            List<Appointment> appointments = doctorMapper.GetAppointmentsByDoctorId(_doctor.Id);

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found for this doctor.");
            }
            else
            {
                Console.WriteLine("Appointments involving the doctor:");

                foreach (var appointment in appointments)
                {
                    string patientName = patientMapper.GetPatientNameById(appointment.PatientId);
                    string doctorName = doctorMapper.GetDoctorNameById(appointment.DoctorId);

                    Console.WriteLine($"Appointment ID: {appointment.AppointmentId}");
                    Console.WriteLine($"Patient Name: {patientName}");
                    Console.WriteLine($"Doctor Name: {doctorName}");
                    Console.WriteLine($"Illness Description: {appointment.IllnessDescription}");
                    Console.WriteLine(); // Adding a blank line between appointments for readability
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }


        public void CheckParticularPatient()
        {
            Console.Write("Enter the ID of the patient to check: ");

            // 读取用户输入
            int input = int.Parse(Console.ReadLine());
            //string input = Console.ReadLine();
            PatientMapper patientMapper = new PatientMapper();

            Patient newPatient = patientMapper.GetPatientById(input);

            if (newPatient != null)
            {
                // 输出病人的所有信息
                Console.WriteLine($"Patient ID: {newPatient.Id}");
                Console.WriteLine($"Full Name: {newPatient.Name}");
                string fullAddress = $"{newPatient.StreetNumber} {newPatient.Street}, {newPatient.City}, {newPatient.State}";
                Console.WriteLine($"Address: {fullAddress}");
                Console.WriteLine($"Email: {newPatient.Email}");
                Console.WriteLine($"Phone: {newPatient.Phone}");
            }


            Console.ReadKey();
        }

        public void ListAppointmentsWithPatient()
        {


            Console.Write("Enter the ID of the patient: ");
            string patientInput = Console.ReadLine();

            if ( int.TryParse(patientInput, out int patientId))
            {
                PatientMapper patientMapper = new PatientMapper();
                DoctorMapper doctorMapper = new DoctorMapper();

                // 获取病人和医生的名字
                string patientName = patientMapper.GetPatientNameById(patientId);
                string doctorName = doctorMapper.GetDoctorNameById(_doctor.Id);

                // 获取预约信息
                List<Appointment> appointments = doctorMapper.GetAppointmentsByDoctorAndPatientId(_doctor.Id, patientId);

                if (appointments.Count > 0)
                {
                    Console.WriteLine($"Appointments between Dr. {doctorName} and Patient {patientName}:");
                    foreach (var appointment in appointments)
                    {
                        Console.WriteLine($"Appointment ID: {appointment.AppointmentId}, Illness: {appointment.IllnessDescription}");
                    }
                }
                else
                {
                    Console.WriteLine("No appointments found for this doctor-patient pair.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID(s). Please enter valid integer IDs.");
            }

            Console.WriteLine("Press any key to return to menu...");
            Console.ReadKey();
        }


    }
}
