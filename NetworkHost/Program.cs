using System;
using System.ServiceModel;

namespace NetworkHost
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (ServiceHost Host = new ServiceHost(typeof(NetworkChat.ServiceChat)))
            {
                Host.Open();
                Console.WriteLine("Хост приложения запущен!");
                Console.WriteLine("Для остановки хоста нажмите любую клавишу");
                Console.ReadLine();
            }
        }
    }
}
