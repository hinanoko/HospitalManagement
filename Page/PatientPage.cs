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

        // Main page for patient interactions
        public void patientMainPage()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.Clear(); // Clear the console for better visibility
                    Console.WriteLine("\t\t\t ============================================================");
                    Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                    Console.WriteLine("\t\t\t |                                                          |");
                    Console.WriteLine("\t\t\t ------------------------------------------------------------");
                    Console.WriteLine("\t\t\t |                        Patient Menu                      |");
                    Console.WriteLine("\t\t\t |                                                          |");
                    Console.WriteLine("\t\t\t ============================================================");
                    Console.WriteLine($"Welcome to DOTNET Hospital Management System, {_patient.Name}!");
                    Console.WriteLine("Please choose an option:");
                    Console.WriteLine("1. List patient details");
                    Console.WriteLine("2. List my doctor details");
                    Console.WriteLine("3. List all appointments");
                    Console.WriteLine("4. Book appointment");
                    Console.WriteLine("5. Exit to login");
                    Console.WriteLine("6. Exit System");

                    // Handle user input
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
                            exit = true; // Exit to login screen
                            Console.Clear(); // Clear the console
                            Program.StartApplication(); // Restart login interface
                            break;
                        case "6":
                            Environment.Exit(0); // Exit the system
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

        // Method to list patient details
        private void ListPatientDetails()
        {
            try
            {
                PatientMapper patientMapper = new PatientMapper();
                Patient patient = patientMapper.displayPatientDetails(_patient.Id); // Retrieve details using the patient's ID

                Console.Clear(); // Clear the console for better visibility
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
                    // Display all patient information
                    Console.WriteLine($"Patient ID: {patient.Id}");
                    Console.WriteLine($"Full Name: {patient.Name}");
                    string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                    Console.WriteLine($"Address: {fullAddress}");
                    Console.WriteLine($"Email: {patient.Email}");
                    Console.WriteLine($"Phone: {patient.Phone}");
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();  // Wait for user input to return to menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving patient details: {ex.Message}");
                Console.ReadKey(); // Wait for user input to return to menu
            }
        }

        // Method to list details of the patient's doctor
        private void ListDoctorDetails()
        {
            try
            {
                Console.Clear(); // Clear the console for better visibility
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

                List<int> doctorIds = patientMapper.GetDoctorIdsByPatientId(_patient.Id); // Get doctors associated with the patient

                if (doctorIds.Count == 0)
                {
                    Console.WriteLine("No doctors found for the patient.");
                    Console.ReadKey(); // Wait for user input to return to menu
                    return;
                }

                List<Doctor> doctors = doctorMapper.GetDoctorsByIds(doctorIds); // Fetch doctor details by IDs

                if (doctors.Count == 0)
                {
                    Console.WriteLine("No doctor details found.");
                }
                else
                {
                    foreach (var doctor in doctors)
                    {
                        string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";

                        // Format output for doctor information
                        Console.WriteLine($"{doctor.Name,-19} | {doctor.Email,-22} | {doctor.Phone,-11} | {fullAddress,-35}");
                    }
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey(); // Wait for user input to return to menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor details: {ex.Message}");
                Console.ReadKey(); // Wait for user input to return to menu
            }
        }

        // Method to list all appointments for the patient
        private void ListAllAppointments()
        {
            try
            {
                Console.Clear(); // Clear the console for better visibility
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

                List<Appointment> patientAppointments = patientMapper.GetAppointmentsByPatientId(_patient.Id); // Get appointments for the patient

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
                        string patientName = patientMapper.GetPatientNameById(appointment.PatientId); // Get patient name by ID
                        string doctorName = doctorMapper.GetDoctorNameById(appointment.DoctorId); // Get doctor name by ID

                        Console.WriteLine($"{doctorName,-21} | {patientName,-19} | {appointment.IllnessDescription,-11} ");
                    }
                }

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey(); // Wait for user input to return to menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving appointments: {ex.Message}");
                Console.ReadKey(); // Wait for user input to return to menu
            }
        }

        // Method to book a new appointment
        private void BookAppointment()
        {
            try
            {
                Console.Clear(); // Clear the console for better visibility
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                      Book Appointment                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");
                // Create an instance of PatientMapper
                PatientMapper mapper = new PatientMapper();

                // Check for existing appointments for the current patient
                var existingAppointments = mapper.GetAppointmentsByPatientId(_patient.Id);

                Doctor selectedDoctor;

                if (existingAppointments.Count == 0)
                {
                    // If the patient has not booked a doctor before, list all available doctors
                    var doctors = mapper.GetAllDoctors();

                    if (doctors.Count == 0)
                    {
                        Console.WriteLine("No doctors available.");
                        return;
                    }

                    Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with:");
                    for (int i = 0; i < doctors.Count; i++)
                    {
                        string fullAddress = $"{doctors[i].StreetNumber} {doctors[i].Street}, {doctors[i].City}, {doctors[i].State}";
                        Console.WriteLine($"{i + 1} {doctors[i].Name} | {doctors[i].Email} | {doctors[i].Phone} | {fullAddress} ");
                    }
                    Console.WriteLine("Please choose a doctor: ");
                    int selectedDoctorIndex = int.Parse(Console.ReadLine()) - 1; // Get user input for doctor selection
                    selectedDoctor = doctors[selectedDoctorIndex];
                    Console.WriteLine($"You are booking a new appointment with {selectedDoctor.Name}.");
                }
                else
                {
                    // If the patient has existing appointments, retrieve the associated doctor
                    int doctorId = existingAppointments[0].DoctorId; // Assume one doctor per patient
                    selectedDoctor = new DoctorMapper().GetDoctorById(doctorId);

                    if (selectedDoctor == null)
                    {
                        Console.WriteLine("Associated doctor not found.");
                        return;
                    }

                    Console.WriteLine($"You are booking a new appointment with Dr. {selectedDoctor.Name}.");
                }

                // Input the description of the illness
                Console.WriteLine("Description of the appointment:");
                string illnessDescription = Console.ReadLine();

                // Create a new Appointment object and save it to file
                Appointment appointment = new Appointment();
                int nextAppointmentId = appointment.getNextAppointmentId(); // Get the next available appointment ID
                appointment = new Appointment(nextAppointmentId, _patient.Id, selectedDoctor.Id, illnessDescription);
                mapper.SaveAppointment(appointment); // Save the appointment

                Console.WriteLine("The appointment has been booked successfully.");

                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey(); // Wait for user input to return to menu
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error booking appointment: {ex.Message}");
            }
        }
    }
}
