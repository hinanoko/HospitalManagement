using Assignment1;

namespace HospitalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // Start the application
            StartApplication();
        }

        public static void StartApplication()
        {
            // Start the login process
            Login();
        }

        public delegate bool ValidateInput(int id);

        public static void Login()
        {
            Console.Clear(); // Clear the console
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                           Login                          |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.Write("\n\t\t\t Enter ID: ");
            string inputId = Console.ReadLine();

            // Check if the entered ID is a number
            if (!inputId.IsNumeric())
            {
                Console.WriteLine("\nID must be a number! Press any key to re-enter...");
                Console.ReadKey(); // Wait for user input
                StartApplication(); // Restart the application
                return; // End the current method to avoid further execution
            }

            // Check if the entered ID is a 5-digit number
            if (inputId.Length != 5)
            {
                Console.WriteLine("\nInvalid ID format! ID must be five digits. Press any key to re-enter...");
                Console.ReadKey(); // Wait for user input
                StartApplication(); // Restart the application
                return; // End the current method
            }

            try
            {
                // Use long.Parse to handle larger numbers
                int id = int.Parse(inputId);

                Console.Write("\t\t\t Enter Password: ");
                string password = ReadPassword();

                PatientMapper mapper = new PatientMapper();
                var (isValid, userType, message) = mapper.ValidateUser(id, password);
                Console.WriteLine(message);
                if (isValid)
                {
                    Console.WriteLine($"User type: {userType}");
                    if (userType == 1)
                    {
                        Patient patient = mapper.GetPatientById(id); // Replace with actual data
                        PatientPage page = new PatientPage(patient);
                        page.patientMainPage(); // Start the patient menu
                    }
                    else if (userType == 2)
                    {
                        // Handle other user types
                        DoctorMapper doctorMapper = new DoctorMapper();
                        Doctor doctor = doctorMapper.GetDoctorById(id);
                        DoctorPage page = new DoctorPage(doctor);
                        page.doctorMainPage();
                    }
                    else if (userType == 3)
                    {
                        // Handle other user types
                        AdminMapper adminMapper = new AdminMapper();
                        Admin admin = adminMapper.GetAdminById(id);
                        AdminPage page = new AdminPage(admin);
                        page.adminMainPage();
                    }
                    else if (userType == 4)
                    {
                        ReceptionistMapper receptionistMapper = new ReceptionistMapper();
                        Receptionist receptionist = receptionistMapper.GetReceptionistById(id);
                        ReceptionistPage page = new ReceptionistPage(receptionist);
                        page.receptionistMainPage();
                    }

                    // Force garbage collection after completing certain operations
                    Console.WriteLine("Forcing Garbage Collection...");
                    GC.Collect(); // Manually trigger garbage collection
                    GC.WaitForPendingFinalizers(); // Ensure all finalizers are completed
                    Console.WriteLine("Garbage Collection completed.");
                }
                else
                {
                    // Login failed, return to the main program or retry login
                    StartApplication();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nInvalid ID input! Press any key to re-enter...");
                Console.ReadKey(); // Wait for user input
                StartApplication(); // Restart the application
            }
            catch (OverflowException)
            {
                Console.WriteLine("\nID exceeds range! Please enter a valid number. Press any key to re-enter...");
                Console.ReadKey(); // Wait for user input
                StartApplication(); // Restart the application
            }
        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                if (!char.IsControl(key.KeyChar))
                {
                    password += key.KeyChar;
                    Console.Write("*");
                }
                else if (key.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password = password.Substring(0, password.Length - 1);
                    Console.Write("\b \b");
                }
            }
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }

    public static class StringExtensions
    {
        // Extend the string class with a method to check if the string is numeric
        public static bool IsNumeric(this string str)
        {
            return int.TryParse(str, out _);
        }
    }
}
