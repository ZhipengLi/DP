using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class MainClass
    {
        static void Main(string[] args)
        {
            var pb = Person.New.Called("test").WorkAsA("Engineer").Born(DateTime.Now)._person;
            Console.WriteLine(pb);
            Console.ReadKey();
        }
    }

    class Person
    { 
        public string Name { get; set; }
        public string Position { get; set; }
        public DateTime DateOfBirth { get; set; }

        public static Builder New => new Builder();
        public override string ToString()
        {
            return $"Name: {Name}, Position: {Position}, Date of Birth: {DateOfBirth}";
        }
    }
    class Builder : PersonBirthdateBuilder<Builder>
    {
    }
    class PersonBuilder
    {
        public Person _person;
        public PersonBuilder()
        {
            this._person = new Person();
        }
        public Person Build()
        {
            return this._person;
        }
    }
    class PersonInfoBuilder<T> : PersonBuilder where T : PersonInfoBuilder<T>
    {
        public T Called(string name)
        {
            this._person.Name = name;
            return (T)this;
        }
    }
    class PersonJobBuilder<T> : PersonInfoBuilder<PersonJobBuilder<T>> where T : PersonJobBuilder<T>
    {
        public T WorkAsA(string job)
        {
            this._person.Position = job;
            return (T)this;
        }
    }
    class PersonBirthdateBuilder<T> : PersonJobBuilder<PersonBirthdateBuilder<T>>  where T: PersonBirthdateBuilder<T>
    {
        public T Born(DateTime date)
        {
            this._person.DateOfBirth = date;
            return (T)this;
        }
    }
}
