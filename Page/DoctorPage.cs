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
        private Doctor _doctor;  // Holds the information about the currently logged-in doctor

        public DoctorPage(Doctor doctor)
        {
            _doctor = doctor;  // Initialize the DoctorPage with the given doctor
        }

        // Delegate to check a patient's details by patient ID
        public delegate void CheckPatientDelegate(int patientId);

        // The main menu for the doctor, offering different functionalities
        public void doctorMainPage()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    // Clear the console to make the menu more readable
                    Console.Clear();
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

                    // Process user input
                    string choice = Console.ReadLine();

                    // Handle the user's selection
                    switch (choice)
                    {
                        case "1":
                            ListDoctorDetails();  // View doctor's own details
                            break;
                        case "2":
                            ListPatients();  // View a list of patients assigned to this doctor
                            break;
                        case "3":
                            ListAppointments();  // View the doctor's appointments
                            break;
                        case "4":
                            CheckParticularPatient();  // Check details of a specific patient
                            break;
                        case "5":
                            ListAppointmentsWithPatient();  // List appointments between the doctor and a specific patient
                            break;
                        case "6":
                            exit = true;  // Exit to the login screen
                            Console.Clear();
                            Program.StartApplication();  // Restart the login process
                            break;
                        case "7":
                            Environment.Exit(0);  // Exit the entire system
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

        // Method to display the details of the currently logged-in doctor
        private void ListDoctorDetails()
        {
            try
            {
                DoctorMapper doctorMapper = new DoctorMapper();
                Doctor doctor = doctorMapper.displayDoctorDetails(_doctor.Id);  // Get doctor's details by ID

                Console.Clear();  // Clear the screen for better readability
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         My Details                       |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\n");

                // Check if the doctor details are found
                if (doctor != null)
                {
                    Console.WriteLine("Name                | Email Address          | Phone       | Address");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");

                    // Format the doctor's full address
                    string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";

                    // Display the doctor's details in a formatted manner
                    Console.WriteLine($"{doctor.Name,-19} | {doctor.Email,-22} | {doctor.Phone,-11} | {fullAddress,-35}");
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();  // Wait for the user to press a key before returning to the menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor details: {ex.Message}");
            }
        }

        // Method to list all patients assigned to the doctor
        private void ListPatients()
        {
            try
            {
                Console.Clear();  // Clear the screen for better readability
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         My Patients                      |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\n");

                // Fetch the list of patients assigned to the doctor
                DoctorMapper doctorMapper = new DoctorMapper();
                List<Patient> patients = doctorMapper.GetPatientsByDoctorId(_doctor.Id);
                Console.WriteLine($"Patients assigned to {_doctor.Name}");
                Console.WriteLine("\n");

                // If no patients are found, inform the user
                if (patients.Count == 0)
                {
                    Console.WriteLine("No patients found for this doctor.");
                }
                else
                {
                    string doctorName = doctorMapper.GetDoctorNameById(_doctor.Id);
                    Console.WriteLine("Patient             |Doctor                | Email Address          | Phone      | Address");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");

                    // Display patient details, along with the doctor's name
                    foreach (var patient in patients)
                    {
                        string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                        Console.WriteLine($"{patient.Name,-19} | {doctorName,-20} | {patient.Email,-22} | {patient.Phone,-11} | {fullAddress,-35}");
                    }
                }

                Console.WriteLine("\nPress any key to return to the menu...");
                Console.ReadKey();  // Wait for the user to press a key before returning to the menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patients: {ex.Message}");
            }
        }


        public void ListAppointments()
        {
            try
            {
                // Clear the screen for better visibility
                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                      All Appointments                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                // Create mappers to interact with the database
                DoctorMapper doctorMapper = new DoctorMapper();
                PatientMapper patientMapper = new PatientMapper();

                // Get the list of appointments for the logged-in doctor
                List<Appointment> appointments = doctorMapper.GetAppointmentsByDoctorId(_doctor.Id);

                if (appointments.Count == 0)
                {
                    Console.WriteLine("No appointments found for this doctor.");
                }
                else
                {
                    Console.WriteLine("Appointments involving the doctor:");
                    Console.WriteLine("Doctor              | Patient              | Description");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");

                    // Iterate through each appointment and display the details
                    foreach (var appointment in appointments)
                    {
                        string patientName = patientMapper.GetPatientNameById(appointment.PatientId);
                        string doctorName = doctorMapper.GetDoctorNameById(appointment.DoctorId);

                        Console.WriteLine($"{doctorName,-19} | {patientName,-20} | {appointment.IllnessDescription,-22} ");
                    }
                }

                // Wait for the user to press any key to return to the menu
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointments: {ex.Message}");
            }
        }

        public void CheckParticularPatient()
        {
            try
            {
                // Clear the screen for better visibility
                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                   Check Patient Details                  |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                // Prompt the user to enter the patient ID
                Console.Write("Enter the ID of the patient to check: ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    // Define a delegate to check patient details by ID
                    CheckPatientDelegate checkPatient = delegate (int patientId)
                    {
                        try
                        {
                            // Retrieve patient details from the database
                            PatientMapper patientMapper = new PatientMapper();
                            Patient newPatient = patientMapper.GetPatientById(patientId);

                            if (newPatient != null)
                            {
                                // Display patient details
                                Console.WriteLine("Patient             | Doctor                | Email Address          | Phone      | Address");
                                Console.WriteLine("----------------------------------------------------------------------------------------------");

                                string fullAddress = $"{newPatient.StreetNumber} {newPatient.Street}, {newPatient.City}, {newPatient.State}";
                                Console.WriteLine($"{newPatient.Name,-19} | {_doctor.Name,-20} | {newPatient.Email,-22} |{newPatient.Phone,-11} | {fullAddress,-35}");
                            }
                            else
                            {
                                Console.WriteLine("Patient not found.");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error retrieving patient: {ex.Message}");
                        }
                    };

                    // Call the delegate to check the patient's details
                    checkPatient(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid patient ID.");
                }

                // Wait for the user to press any key to return to the menu
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking patient: {ex.Message}");
            }
        }

        public void ListAppointmentsWithPatient()
        {
            try
            {
                // Clear the screen for better visibility
                Console.Clear();
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                     Appointments With                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                // Prompt the user to enter the patient ID
                Console.Write("Enter the ID of the patient you would like to view appointments for: ");
                string patientInput = Console.ReadLine();

                // Validate the input and parse it into an integer
                if (int.TryParse(patientInput, out int patientId))
                {
                    // Create mappers to interact with the database
                    PatientMapper patientMapper = new PatientMapper();
                    DoctorMapper doctorMapper = new DoctorMapper();

                    // Retrieve the patient and doctor names
                    string patientName = patientMapper.GetPatientNameById(patientId);
                    string doctorName = doctorMapper.GetDoctorNameById(_doctor.Id);

                    // Retrieve appointments between the doctor and the patient
                    List<Appointment> appointments = doctorMapper.GetAppointmentsByDoctorAndPatientId(_doctor.Id, patientId);

                    if (appointments.Count > 0)
                    {
                        // Display the appointments
                        Console.WriteLine("Doctor              | Patient              | Description");
                        Console.WriteLine("----------------------------------------------------------------------------------------------");

                        foreach (var appointment in appointments)
                        {
                            Console.WriteLine($"{doctorName,-19} | {patientName,-20} | {appointment.IllnessDescription,-22} ");
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

                // Wait for the user to press any key to return to the menu
                Console.WriteLine("Press any key to return to menu...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointments with patient: {ex.Message}");
            }
        }
    }
}

