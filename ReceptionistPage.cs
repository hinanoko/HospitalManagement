using HospitalSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class ReceptionistPage
    {
        private Receptionist _receptionist;

        public ReceptionistPage(Receptionist receptionist)
        {
            _receptionist = receptionist;
        }

        public void receptionistMainPage()
        {
            bool exit = false;

            // 定义一个匿名方法来处理不同的功能
            Action<string> processChoice = delegate (string choice)
            {
                switch (choice)
                {
                    case "1":
                        ListMyDetails();
                        break;
                    case "2":
                        ConductOfReceptionist();
                        break;
                    case "3":
                        exit = true; // 退出到登录界面
                        Console.Clear(); // 清屏
                        Program.StartApplication(); // 重新启动登录界面
                        break;
                    case "4":
                        Environment.Exit(0); // 退出系统
                        break;
                    default:
                        Console.WriteLine("Invalid choice, please try again.");
                        break;
                }
            };

            while (!exit)
            {
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                      Receptionist Menu                   |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine($"Welcome to DOTNET Hospital Management System {_receptionist.Name}!");
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. List my detail");
                Console.WriteLine("2. Code of conduct for receptionists");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Exit");

                // 处理用户输入
                string choice = Console.ReadLine();

                // 使用匿名方法来处理用户的选择
                processChoice(choice);
            }
        }

        public void ListMyDetails()
        {

            Console.Clear();
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                       List My Details                    |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");


                    Console.WriteLine($"ID: {_receptionist.Id}");
                    Console.WriteLine($"Name: {_receptionist.Name}");
                    Console.WriteLine("--------------------------------------------------------");

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        public void ConductOfReceptionist()
        {
            Console.Clear();
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                   Conduct of Receptionist                |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.WriteLine(
                "Rule 1:\r\n\r\nIf you see a woman wearing a 1970s nurse uniform walking towards you with a smile at the reception desk, " +
                "remember not to make eye contact with her. Lower your head, pretend to be busy, and she will slowly disappear. " +
                "If she stops at your desk, don't panic, wait quietly for 3 minutes, she won't appear again." +
                "\r\n\r\n\r\n\r\nRule 2:\r\n\r\nEvery Tuesday at 3am, you will hear a gentle knocking on the door, by which time the hospital gate should have been locked. " +
                "No matter how anxious the people come, remember not to open the door. " +
                "Someone once opened the door, but I never saw them again the next day." +
                "\r\n\r\n\r\n\r\nRule 3:\r\n\r\nIf the clock on the right side of the reception desk stops at 2:17 on a certain night and cannot be restarted three times, " +
                "immediately leave the reception desk and find a doctor to accompany you until dawn. Because every time this time stops, " +
                "a 'patient' will appear within the next 3 minutes, and you should not see him alone.");

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}
