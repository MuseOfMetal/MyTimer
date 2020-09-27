using System;
namespace MyTimer
{
    class Program
    {
        private static MyTimer myTimer;
        static void Main(string[] args)
        {
            myTimer = new MyTimer();

            myTimer.Notify += MyTimer_Notify;

            myTimer.Start();

            myTimer.AddObject(new Person() { LastName = "Иванов", Name = "Александр"}, DateTime.Now.AddSeconds(1));
            myTimer.AddObject(new Person() { LastName = "Синичкин", Name = "Владимир" }, DateTime.Now.AddSeconds(3));

            Console.ReadLine();
        }
        private static void MyTimer_Notify(object sender, object obj)
        {
            Person person = obj as Person;
            Console.WriteLine("Name = {0}, LastName = {1}", person.Name, person.LastName);
        }
    }
    class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
