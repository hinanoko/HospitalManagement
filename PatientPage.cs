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
            Console.WriteLine("Patient Details:");
            Console.WriteLine($"ID: {_patient.Id}");
            Console.WriteLine($"Name: {_patient.Name}");
            Console.WriteLine($"Password: {_patient.Password}");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private void ListDoctorDetails()
        {
            // 这里你可以实现获取医生详情的逻辑
            Console.WriteLine("Doctor Details:");
            // 假设有一个方法获取医生信息
            Console.WriteLine("Doctor Name: Dr. Smith");
            Console.WriteLine("Specialty: Cardiology");
            Console.WriteLine("Contact: (123) 456-7890");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private void ListAllAppointments()
        {
            // 这里你可以实现获取所有预约的逻辑
            Console.WriteLine("All Appointments:");
            // 假设有一个方法获取预约信息
            Console.WriteLine("Appointment 1: 2024-08-25 10:00 AM with Dr. Smith");
            Console.WriteLine("Appointment 2: 2024-08-30 02:00 PM with Dr. Jones");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }

        private void BookAppointment()
        {
            // 这里你可以实现预约的逻辑
            Console.WriteLine("Booking an Appointment:");
            Console.Write("Enter the date and time (e.g., 2024-08-30 02:00 PM): ");
            string dateTime = Console.ReadLine();
            // 假设有一个方法进行预约
            Console.WriteLine($"Appointment booked for {dateTime}.");
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadKey();
        }
    }
}
