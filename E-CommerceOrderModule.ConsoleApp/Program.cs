using System;
using System.Linq;
using E_CommerceOrderModule.ConsoleApp.Work;
using E_CommerceOrderModule.ConsoleApp.RabbitMQ;


namespace E_CommerceOrderModule.ConsoleApp
{
    class Program
    {
        private static string[] operations = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16" };

        private static string PrintModeTable()
        {
            Console.Clear();

            int tableLeft = 40;
            int tableRight = 30;
            Console.WriteLine("Console Version : 1\n\n");
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", getSeperator(tableLeft), getSeperator(tableRight)));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "Çalışma Modu", "Girilmesi Gereken Değer"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", getSeperator(tableLeft), getSeperator(tableRight)));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", "Consumer", "1"));
            Console.WriteLine(String.Format("|{0," + tableLeft + "}|{1," + tableRight + "}|", getSeperator(tableLeft), getSeperator(tableRight)));

            Console.WriteLine("\n\nLütfen çalışma modu için tablodan bir değer giriniz:");
            return Console.ReadLine();
        }

        public static string getSeperator(int length)
        {
            string seperator = string.Empty;
            for (int i = 0; i < length; i++)
            {
                seperator += "-";
            }
            return seperator;
        }

        static void Main(string[] args)
        {
            Worker Worker = null;
            Console.Title = "RabbitMQ";
            string workMode = string.Empty;
            do
            {
                workMode = PrintModeTable();
            }
            while (!operations.Contains(workMode));

            Console.Clear();
            switch (workMode)
            {
                case "1": Console.Title = "Order Consumer"; Worker = new Worker(new OrderConsumer()); break;
            }

            Worker.Work();
            Console.ReadKey();
        }

    }

}
