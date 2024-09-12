using System;

class Person
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

    public Person() { }

    public Person(string password, string name, string email, string phone, string streetNumber, string street, string city, string state)
    {
        Password = password;
        Name = name;
        Email = email;
        Phone = phone;
        StreetNumber = streetNumber;
        Street = street;
        City = city;
        State = state;
    }

    // 重载 ToString 方法
    public virtual string ToString(string format = "full")
    {
        if (format == "full")
        {
            return $"ID: {Id}, Name: {Name}, Email: {Email}, Phone: {Phone}, Address: {StreetNumber} {Street}, {City}, {State}";
        }
        else
        {
            return $"Name: {Name}, Email: {Email}";
        }
    }
}