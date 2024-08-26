using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment1
{
    class Patient
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }


        private PatientMapper mapper;

        public Patient(int id, string password, string name)
        {
            Id = id;
            Password = password;
            Name = name;
        }

        private int GenerateUniqueID()
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
    }

    
}
