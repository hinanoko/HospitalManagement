using Assignment1;

namespace HospitalSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            // 启动应用程序
            StartApplication();
        }

        public static void StartApplication()
        {
            // 启动登录
            Login();
        }

        public static void Login()
        {
            Console.Clear(); // 清屏
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                           Login                          |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.Write("\n\t\t\t Enter ID: ");
            int id = int.Parse(Console.ReadLine());

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
                    Patient patient = mapper.GetPatientById(id); // 需要用实际数据替换
                    PatientPage page = new PatientPage(patient);
                    page.patientMainPage(); // 启动病人菜单
                }
                else if (userType == 2)
                {
                    // 处理其他用户类型
                    DoctorMapper doctorMapper = new DoctorMapper();
                    Doctor doctor = doctorMapper.GetDoctorById(id);
                    DoctorPage page = new DoctorPage(doctor);
                    page.doctorMainPage();
                }
                else if (userType == 3)
                {
                    // 处理其他用户类型
                    AdminMapper adminMapper = new AdminMapper();
                    Admin admin = adminMapper.GetAdminById(id);
                    AdminPage page = new AdminPage(admin);
                    page.adminMainPage();
                }
            }
            else
            {
                // 登录失败，返回主程序或重新登录
                StartApplication();
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
}
