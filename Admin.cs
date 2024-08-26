using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Admin
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }


        //private PatientMapper mapper;

        public Admin(int id, string password, string name)
        {
            Id = id;
            Password = password;
            Name = name;
        }
    }
}
