using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment1
{
    class Patient : Person
    {
        private PatientMapper mapper;

        public Patient() : base()
        {
            mapper = new PatientMapper();
        }

        public Patient(string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
            : base(password, name, email, phone, streetNumber, street, city, state)
        {
            mapper = new PatientMapper();
            Id = generateUniqueID();
        }

        private int generateUniqueID()
        {
            Random rnd = new Random();
            int newId;
            do
            {
                // 生成以1开头的5位数字
                newId = rnd.Next(10000, 20000);
            } while (mapper.IdExistsInFile(newId));

            return newId;
        }

        // 重写 ToString 方法
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
