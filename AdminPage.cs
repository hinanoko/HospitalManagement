using System;
using System.Collections.Generic;
using System.Linq;
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
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit System");

            // 处理用户输入
            string choice = Console.ReadLine();


        }
    }
}
