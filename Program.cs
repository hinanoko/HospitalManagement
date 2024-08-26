using Assignment1;
using System;

namespace HospitalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();
        }

        public static void Login()
        {
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                           Login                          |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.Write("\n\t\t\t Enter ID: ");
            int id = int.Parse(Console.ReadLine());

            // 输入密码
            Console.Write("\t\t\t Enter Password: ");
            string password = ReadPassword();

            PatientMapper mapper = new PatientMapper();
            var (isValid, userType, message) = mapper.ValidateUser(id, password);
            Console.WriteLine(message);
            if (isValid)
            {
                Patient patient = mapper.GetPatientById(id); // 获取 Patient 对象
                Console.WriteLine($"User type: {userType}");
                if (userType == 1)
                {
                    PatientPage page = new PatientPage(patient); // 传递 Patient 对象
                    page.patientMainPage();
                }
                else if (userType == 2)
                {
                }
                else if (userType == 3)
                {
                }
            }

        }

        private static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Ignore any key that's not a backspace or a valid character
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
            // Continue until Enter is pressed
            while (key.Key != ConsoleKey.Enter);

            Console.WriteLine();
            return password;
        }
    }
}