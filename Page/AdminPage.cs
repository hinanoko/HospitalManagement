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

            // Define an anonymous method to handle different functionalities
            Action<string> processChoice = delegate (string choice)
            {
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
                        exit = true; // Logout to login screen
                        Console.Clear(); // Clear the screen
                        Program.StartApplication(); // Restart login interface
                        break;
                    case "8":
                        Environment.Exit(0); // Exit the system
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            };

            while (!exit)
            {
                Console.Clear(); // Clear the screen for better visibility
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

                // Process user input
                string choice = Console.ReadLine();

                // Use the anonymous method to handle user choice
                processChoice(choice);
            }
        }

        private void registerPatient()
        {
            try
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

                // Create a new Patient object
                Patient newPatient = new Patient(password, $"{firstName} {lastName}", email, phone, streetNumber, street, city, state);

                PatientMapper mapper = new PatientMapper();
                int nowId = mapper.SavePatient(newPatient);

                Console.WriteLine("Patient registered successfully! Press any key to return to the menu.");
                Console.WriteLine($"Now your ID is: {nowId}");
                Console.ReadKey(); // Wait for the user to press a key
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error registering patient: {ex.Message}");
            }
        }

        private void registerDoctor()
        {
            try
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

                // Create a new Doctor object
                Doctor newDoctor = new Doctor(password, $"{firstName} {lastName}", email, phone, streetNumber, street, city, state);

                DoctorMapper mapper = new DoctorMapper();
                int nowId = mapper.SaveDoctor(newDoctor);

                Console.WriteLine("Doctor registered successfully! Press any key to return to the menu.");
                Console.WriteLine($"Now your ID is: {nowId}");
                Console.ReadKey(); // Wait for the user to press a key
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }


        public void ListAllDoctors()
        {
            try
            {
                AdminMapper adminMapper = new AdminMapper();
                List<Doctor> doctors = adminMapper.ListAllDoctors();

                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         All Doctors                      |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                if (doctors.Count == 0)
                {
                    Console.WriteLine("No doctors found.");
                }
                else
                {
                    Console.WriteLine("All doctors registered to the DOTNET Hospital Management System");
                    Console.WriteLine("\n");
                    Console.WriteLine("Name                | Email Address          | Phone       | Address");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    foreach (var doctor in doctors)
                    {
                        string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";

                        // Format output for doctor information
                        Console.WriteLine($"{doctor.Name,-19} | {doctor.Email,-22} | {doctor.Phone,-11} | {fullAddress,-35}");
                    }
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error listing doctors: {ex.Message}");
            }
        }

        public void ListAllPatients()
        {
            try
            {
                AdminMapper adminMapper = new AdminMapper();
                List<Patient> patients = adminMapper.ListAllPatients();

                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         All Patients                     |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients found.");
                }
                else
                {
                    Console.WriteLine("All patients registered to the DOTNET Hospital Management System");
                    Console.WriteLine("\n");
                    Console.WriteLine("Patient             |Doctor                | Email Address          | Phone      | Address");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    foreach (var patient in patients)
                    {
                        string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                        Console.WriteLine($"{patient.Name,-19} | {adminMapper.GetDoctorNameByPatientId(patient.Id),-20} | {patient.Email,-22} |{patient.Phone,-11} | {fullAddress,-35}");
                    }
                }
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }

        public void CheckDoctorDetails()
        {
            try
            {
                AdminMapper adminMapper = new AdminMapper();

                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                       Doctor Details                     |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("Please enter the ID of the doctor whose details you are checking. Or press 'n' to return to the menu");

                // Input ID on a new line
                string input = Console.ReadLine();

                // Check if 'n' was entered
                if (input.ToLower() == "n")
                {
                    Console.WriteLine("Returning to menu...");
                    return; // Return to the menu
                }

                // Check if the input is a numeric ID
                if (int.TryParse(input, out int doctorId))
                {
                    Doctor doctor = adminMapper.GetDoctorById(doctorId);

                    if (doctor != null)
                    {
                        Console.WriteLine($"\nDetails for {doctor.Name}");
                        Console.WriteLine("Name                | Email Address          | Phone       | Address");
                        Console.WriteLine("----------------------------------------------------------------------------------------------");
                        string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";

                        // Format output for doctor information
                        Console.WriteLine($"{doctor.Name,-19} | {doctor.Email,-22} | {doctor.Phone,-11} | {fullAddress,-35}");
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void CheckPatientDetails()
        {
            try
            {
                AdminMapper adminMapper = new AdminMapper();

                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                       Patient Details                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("Please enter the ID of the patient whose details you are checking. Or press 'n' to return to the menu");

                // Input ID on a new line
                string input = Console.ReadLine();

                // Check if 'n' was entered
                if (input.ToLower() == "n")
                {
                    Console.WriteLine("Returning to menu...");
                    return; // Return to the menu
                }

                if (int.TryParse(input, out int patientId))
                {
                    Patient patient = adminMapper.GetPatientById(patientId);

                    if (patient != null)
                    {
                        Console.WriteLine($"\nDetails for {patient.Name}");
                        Console.WriteLine("\n");
                        Console.WriteLine("Patient             |Doctor                | Email Address          | Phone      | Address");
                        Console.WriteLine("----------------------------------------------------------------------------------------------");
                        string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                        Console.WriteLine($"{patient.Name,-19} | {adminMapper.GetDoctorNameByPatientId(patient.Id),-20} | {patient.Email,-22} |{patient.Phone,-11} | {fullAddress,-35}");

                        List<Appointment> appointments = adminMapper.GetAppointmentsByPatientId(patientId);
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
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patient details: {ex.Message}");
            }
        }
    }
}
