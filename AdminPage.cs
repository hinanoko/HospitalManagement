using HospitalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class AdminPage
    {
        private Admin _admin;

        public AdminPage(Admin admin)
        {
            _admin = admin;
        }

        public void adminMainPage()
        {
            bool exit = false;

            while (!exit)
            {

                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         Admin Menu                       |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {_admin.Name}!");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List all doctors");
                Console.WriteLine("2. Check doctor details");
                Console.WriteLine("3. List all patients");
                Console.WriteLine("4. Check patient details");
                Console.WriteLine("5. Add doctor");
                Console.WriteLine("6. Add patient");
                Console.WriteLine("7. Logout");
                Console.WriteLine("8. Exit");

                // 处理用户输入
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListAllDoctors();
                        break;
                    case "2":
                        CheckDoctorDetails();
                        break;
                    case "3":
                        ListAllPatients();
                        break;
                    case "4":
                        CheckPatientDetails();
                        break;
                    case "5":
                        registerDoctor();
                        break;
                    case "6":
                        registerPatient();
                        break;
                    case "7":
                        exit = true; // 退出到登录界面
                        Console.Clear(); // 清屏
                        Program.StartApplication(); // 重新启动登录界面
                        break;
                    case "8":
                        Environment.Exit(0); // 退出系统
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }

            }


        }

        private void registerPatient()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                        Add Patient                       |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Street Number: ");
            string streetNumber = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("State: ");
            string state = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // 创建新的 Patient 对象
            Patient newPatient = new Patient(password, $"{firstName} {lastName}", email, phone, streetNumber, street, city, state);

            PatientMapper mapper = new PatientMapper();
            mapper.SavePatient(newPatient);

            Console.WriteLine("Patient registered successfully! Press any key to return to the menu.");
            Console.ReadKey(); // Wait for the user to press a key
        }

        private void registerDoctor()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                        Add Doctor                        |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            Console.Write("Email: ");
            string email = Console.ReadLine();

            Console.Write("Phone: ");
            string phone = Console.ReadLine();

            Console.Write("Street Number: ");
            string streetNumber = Console.ReadLine();

            Console.Write("Street: ");
            string street = Console.ReadLine();

            Console.Write("City: ");
            string city = Console.ReadLine();

            Console.Write("State: ");
            string state = Console.ReadLine();

            Console.Write("Enter Password: ");
            string password = Console.ReadLine();

            // 创建新的 Patient 对象
            Doctor newDoctor = new Doctor(password, $"{firstName} {lastName}", email, phone, streetNumber, street, city, state);

            DoctorMapper mapper = new DoctorMapper();
            mapper.SaveDoctor(newDoctor);

            Console.WriteLine("Doctor registered successfully! Press any key to return to the menu.");
            Console.ReadKey(); // Wait for the user to press a key
        }


        public void ListAllDoctors()
        {
            AdminMapper adminMapper = new AdminMapper();
            List<Doctor> doctors = adminMapper.ListAllDoctors();

            Console.Clear();
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                       List of Doctors                    |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            if (doctors.Count == 0)
            {
                Console.WriteLine("No doctors found.");
            }
            else
            {
                foreach (var doctor in doctors)
                {
                    Console.WriteLine($"ID: {doctor.Id}");
                    Console.WriteLine($"Name: {doctor.Name}");
                    Console.WriteLine($"Email: {doctor.Email}");
                    Console.WriteLine($"Phone: {doctor.Phone}");
                    string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";
                    Console.WriteLine($"Address: {fullAddress}");
                    Console.WriteLine("--------------------------------------------------------");
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        // Other methods like adminMainPage, registerDoctor, etc.
        public void ListAllPatients()
        {
            AdminMapper adminMapper = new AdminMapper();
            List<Patient> patients = adminMapper.ListAllPatients();

            Console.Clear();
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                       List of Patients                   |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found.");
            }
            else
            {
                foreach (var patient in patients)
                {
                    Console.WriteLine($"ID: {patient.Id}");
                    Console.WriteLine($"Name: {patient.Name}");
                    Console.WriteLine($"Email: {patient.Email}");
                    Console.WriteLine($"Phone: {patient.Phone}");
                    string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                    Console.WriteLine($"Address: {fullAddress}");
                    Console.WriteLine("--------------------------------------------------------");
                }
            }
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        public void CheckDoctorDetails()
        {
                AdminMapper adminMapper = new AdminMapper();

                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                    Check Doctor Details                  |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.Write("Please enter the Doctor ID: ");

                if (int.TryParse(Console.ReadLine(), out int doctorId))
                {
                    Doctor doctor = adminMapper.GetDoctorById(doctorId);

                    if (doctor != null)
                    {
                        Console.WriteLine("\nDoctor Details:");
                        Console.WriteLine($"ID: {doctor.Id}");
                        Console.WriteLine($"Name: {doctor.Name}");
                        Console.WriteLine($"Email: {doctor.Email}");
                        Console.WriteLine($"Phone: {doctor.Phone}");
                    // Print other fields as necessary
                    string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";
                    Console.WriteLine($"Address: {fullAddress}");
                }
                    else
                    {
                        Console.WriteLine("No doctor found with the provided ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();

        }


        public void CheckPatientDetails()
        {
                AdminMapper adminMapper = new AdminMapper();

                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                    Check Patient Details                 |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.Write("Please enter the Patient ID: ");

                if (int.TryParse(Console.ReadLine(), out int patientId))
                {
                    Patient patient = adminMapper.GetPatientById(patientId);

                    if (patient != null)
                    {
                        Console.WriteLine("\nPatient Details:");
                        Console.WriteLine($"ID: {patient.Id}");
                        Console.WriteLine($"Name: {patient.Name}");
                        Console.WriteLine($"Email: {patient.Email}");
                        Console.WriteLine($"Phone: {patient.Phone}");

                        List<Appointment> appointments = adminMapper.GetAppointmentsByPatientId(patientId);

                        if (appointments.Count > 0)
                        {
                            Console.WriteLine("\nAssociated Doctors:");

                            foreach (var appointment in appointments)
                            {
                                Doctor doctor = adminMapper.GetDoctorById(appointment.DoctorId);

                                if (doctor != null)
                                {
                                    Console.WriteLine($"\nDoctor ID: {doctor.Id}");
                                    Console.WriteLine($"Doctor Name: {doctor.Name}");
                                    Console.WriteLine($"Doctor Email: {doctor.Email}");
                                    Console.WriteLine($"Doctor Phone: {doctor.Phone}");
                                    Console.WriteLine($"Illness Description: {appointment.IllnessDescription}");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nNo associated doctors found for this patient.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No patient found with the provided ID.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid ID format. Please enter a numeric ID.");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();
        }


    }
}
