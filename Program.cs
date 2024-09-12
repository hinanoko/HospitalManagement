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

        public delegate bool ValidateInput(int id);

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
            string inputId = Console.ReadLine();

            // 检查输入的ID是否是数字
            if (!inputId.IsNumeric())
            {
                Console.WriteLine("\nID 必须是一个数字！按任意键重新输入...");
                Console.ReadKey(); // 等待用户按键
                StartApplication(); // 重新启动应用程序
                return; // 结束当前方法，避免继续执行
            }

            // 检查输入的ID是否为5位数字
            if (inputId.Length != 5)
            {
                Console.WriteLine("\nID 格式不正确！ID 必须是五位数字。按任意键重新输入...");
                Console.ReadKey(); // 等待用户按键
                StartApplication(); // 重新启动应用程序
                return; // 结束当前方法
            }

            try
            {
                // 使用 long.Parse 来处理较大的数字
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
                    else if(userType == 4)
                    {
                        ReceptionistMapper receptionistMapper = new ReceptionistMapper();
                        Receptionist receptionist = receptionistMapper.GetReceptionistById(id);
                        ReceptionistPage page = new ReceptionistPage(receptionist);
                        page.receptionistMainPage();
                    }

                    // 在完成某些操作后强制垃圾回收
                    Console.WriteLine("Forcing Garbage Collection...");
                    GC.Collect(); // 手动触发垃圾回收
                    GC.WaitForPendingFinalizers(); // 确保所有终结器都完成
                    Console.WriteLine("Garbage Collection completed.");
                }
                else
                {
                    // 登录失败，返回主程序或重新登录
                    StartApplication();
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("\nID 输入无效！按任意键重新输入...");
                Console.ReadKey(); // 等待用户按键
                StartApplication(); // 重新启动应用程序
            }
            catch (OverflowException)
            {
                Console.WriteLine("\nID 超出范围！请输入有效的数字。按任意键重新输入...");
                Console.ReadKey(); // 等待用户按键
                StartApplication(); // 重新启动应用程序
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
        // 扩展 string 类，添加一个方法来检查字符串是否为数字
        public static bool IsNumeric(this string str)
        {
            return int.TryParse(str, out _);
        }
    }

}
