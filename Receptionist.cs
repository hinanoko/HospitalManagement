using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
	class Receptionist: Person
	{
        public Receptionist(int id, string password, string name)
           : base(password, name, "", "", "", "", "", "")
        {
            Id = id;
            Password = password;
            Name = name;
        }
        public override string ToString()
        {
            return $"Receptionist ID: {Id}, Name: {Name}";
        }
    }
}
