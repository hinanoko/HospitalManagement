using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment1
{
    class Admin : Person
    {
        // 继承了 Person 类的属性: Id, Password, Name 等

        public Admin(int id, string password, string name)
            : base(password, name, "", "", "", "", "", "")
        {
            Id = id;
            Password = password;
            Name = name;
        }

        // 你可以选择重写 Person 类中的方法，例如 ToString
        public override string ToString()
        {
            return $"Admin ID: {Id}, Name: {Name}";
        }
    }
}

