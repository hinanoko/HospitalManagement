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
    class Patient
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string StreetNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }





        private PatientMapper mapper;

        public Patient()
        {
            // Initialize mapper here
            mapper = new PatientMapper();
        }

        public Patient(string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
        {
            mapper = new PatientMapper();

            Id = generateUniqueID();
            Password = password;
            Name = name;
            Email = email;
            Phone = phone;
            StreetNumber = streetNumber;
            Street = street;
            City = city;
            State = state;
        }

        public int generateUniqueID()
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

        public override string ToString()
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}, Address: {StreetNumber} {Street}, {City}, {State}";
        }
    }

    
}
