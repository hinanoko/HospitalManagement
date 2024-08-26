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

        public void patientMainPage()
        {
            // 使用 _patient 来显示病人信息或执行其他操作
            Console.WriteLine($"Welcome {_patient.Name}!");
            // 其他页面逻辑
        }
    }
}
