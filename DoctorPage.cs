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
        private Doctor _doctor;

        public DoctorPage(Doctor doctor)
        {
            _doctor = doctor;
        }

        public delegate void CheckPatientDelegate(int patientId);

        public void doctorMainPage()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {

                    Console.Clear(); // 清屏，方便查看
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

                // 处理用户输入
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ListDoctorDetails();
                        break;
                    case "2":
                        ListPatients();
                        break;
                    case "3":
                        ListAppointments();
                        break;
                    case "4":
                        CheckParticularPatient();
                        break;
                    case "5":
                        ListAppointmentsWithPatient();
                        break;
                    case "6":
                        exit = true; // 退出到登录界面
                        Console.Clear(); // 清屏
                        Program.StartApplication(); // 重新启动登录界面
                        break;
                    case "7":
                        Environment.Exit(0); // 退出系统
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

        private void ListDoctorDetails()
        {
            try
            {
                DoctorMapper doctorMapper = new DoctorMapper();
            Doctor doctor = doctorMapper.displayDoctorDetails(_doctor.Id);  // 传入当前患者的 ID

            Console.Clear(); // 清屏，方便查看
            Console.WriteLine("\t\t\t ============================================================");
            Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ------------------------------------------------------------");
            Console.WriteLine("\t\t\t |                         My Details                       |");
            Console.WriteLine("\t\t\t |                                                          |");
            Console.WriteLine("\t\t\t ============================================================");

            Console.WriteLine("\n");

            //Console.WriteLine($"{_doctor.Name}'s Details");

            if (doctor != null)
            {
                    Console.WriteLine("Name                | Email Address          | Phone       | Address");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");
                    string fullAddress = $"{doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}";

                    // 格式化输出医生信息
                    Console.WriteLine($"{doctor.Name,-19} | {doctor.Email,-22} | {doctor.Phone,-11} | {fullAddress,-35}");
                    // 输出病人的所有信息
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();  // 等待用户按键返回菜单
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving doctor details: {ex.Message}");
            }
        }

        private void ListPatients()
        {
            try
            {
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                         My Patients                      |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");
                DoctorMapper doctorMapper = new DoctorMapper();
            List<Patient> patients = doctorMapper.GetPatientsByDoctorId(_doctor.Id);
                Console.WriteLine($"Patients assigned to {_doctor.Name}");
                Console.WriteLine("\n");

            if (patients.Count == 0)
            {
                Console.WriteLine("No patients found for this doctor.");
            }
            else
            {
                string doctorName = doctorMapper.GetDoctorNameById(_doctor.Id);
                    Console.WriteLine("Patient             |Doctor                | Email Address          | Phone      | Address");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");

                    // 输出病人信息并显示医生名字
                    foreach (var patient in patients)
                {
                        string fullAddress = $"{patient.StreetNumber} {patient.Street}, {patient.City}, {patient.State}";
                        Console.WriteLine($"{patient.Name,-19} | {doctorName,-20} | {patient.Email, -22} |{patient.Phone,-11} | {fullAddress,-35}");
                }
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
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
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                      All Appointments                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");
                DoctorMapper doctorMapper = new DoctorMapper();
            PatientMapper patientMapper = new PatientMapper(); 
            List<Appointment> appointments = doctorMapper.GetAppointmentsByDoctorId(_doctor.Id);

            if (appointments.Count == 0)
            {
                Console.WriteLine("No appointments found for this doctor.");
            }
            else
            {
                Console.WriteLine("Appointments involving the doctor:");

                    Console.WriteLine("Doctor              |Patient               | Description");
                    Console.WriteLine("----------------------------------------------------------------------------------------------");

                    foreach (var appointment in appointments)
                {
                    string patientName = patientMapper.GetPatientNameById(appointment.PatientId);
                    string doctorName = doctorMapper.GetDoctorNameById(appointment.DoctorId);

                        Console.WriteLine($"{doctorName,-19} | {patientName,-20} | {appointment.IllnessDescription,-22} ");
                }
            }

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
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                   Check Patient Details                  |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                Console.Write("Enter the ID of the patient to check: ");
                if (int.TryParse(Console.ReadLine(), out int input))
                {
                    CheckPatientDelegate checkPatient = delegate (int patientId)
                    {
                        try
                        {
                            PatientMapper patientMapper = new PatientMapper();
                            Patient newPatient = patientMapper.GetPatientById(patientId);

                            if (newPatient != null)
                            {
                                Console.WriteLine("Patient             |Doctor                | Email Address          | Phone      | Address");
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

                    checkPatient(input);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid patient ID.");
                }

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
                Console.Clear(); // 清屏，方便查看
                Console.WriteLine("\t\t\t ============================================================");
                Console.WriteLine("\t\t\t |             DOTNET Hospital Management System            |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ------------------------------------------------------------");
                Console.WriteLine("\t\t\t |                     Appointments With                    |");
                Console.WriteLine("\t\t\t |                                                          |");
                Console.WriteLine("\t\t\t ============================================================");

                Console.WriteLine("\n");

                Console.Write("Enter the ID of the patient you would like to view appointments for: ");
            string patientInput = Console.ReadLine();

            if ( int.TryParse(patientInput, out int patientId))
            {
                PatientMapper patientMapper = new PatientMapper();
                DoctorMapper doctorMapper = new DoctorMapper();

                // 获取病人和医生的名字
                string patientName = patientMapper.GetPatientNameById(patientId);
                string doctorName = doctorMapper.GetDoctorNameById(_doctor.Id);

                // 获取预约信息
                List<Appointment> appointments = doctorMapper.GetAppointmentsByDoctorAndPatientId(_doctor.Id, patientId);

                if (appointments.Count > 0)
                {
                        //Console.WriteLine($"Appointments between Dr. {doctorName} and Patient {patientName}:");
                        Console.WriteLine("Doctor              |Patient               | Description");
                        Console.WriteLine("----------------------------------------------------------------------------------------------");
                        foreach (var appointment in appointments)
                    {
                            
                            Console.WriteLine($"{doctorName,-19} | {patientName,-20} | {appointment.IllnessDescription,-22} ");
                            //Console.WriteLine($"Appointment ID: {appointment.AppointmentId}, Illness: {appointment.IllnessDescription}");
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
