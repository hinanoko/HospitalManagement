using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1
{
    class Admin : Person
    {
        // Inherits properties from the Person class: Id, Password, Name, etc.

        public Admin(int id, string password, string name)
            : base(password, name, "", "", "", "", "", "")
        {
            Id = id;
            Password = password;
            Name = name;
        }

        // You can choose to override methods from the Person class, such as ToString
        public override string ToString()
        {
            return $"Admin ID: {Id}, Name: {Name}";
        }
    }
}
