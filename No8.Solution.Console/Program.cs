using No8.Solution.Models;
using No8.Solution.Printers;
using System;
using System.IO;
using System.Windows.Forms;
using static System.Console;

namespace No8.Solution.Console
{
    class Program
    {
        private static string name;
        private static string model;
        private static PrinterManager manager = new PrinterManager();

        [STAThread]
        static void Main(string[] args)
        {
            Subscribe();

            while(true)
            {
                WriteLine("Select your choice:");
                WriteLine("1:Add new printer");
                WriteLine("2:Print on canon");
                WriteLine("3:Print on epson");
                WriteLine("0:Exit");

                int choice;

                if (!int.TryParse(ReadLine(), out choice))
                {
                    continue;
                }

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        GetData();
                        break;
                    case 2:
                        Print(typeof(CanonPrinter), manager.Print);
                        break;
                    case 3:
                        Print(typeof(EpsonPrinter), manager.PrintLog);
                        break;
                }
            }
        }

        private static void Print(Type type, Func<Type, FileStream, string, string> writer)
        {
            Write(manager.ShowPrinters(type));
            WriteLine("Enter a model:");

            string model = ReadLine();

            var o = new OpenFileDialog();
            o.ShowDialog();

            try
            {
                var file = File.OpenRead(o.FileName);

                Write(writer(type, file, model));

                file.Dispose();
            }
            catch (ArgumentException)
            {
                WriteLine("File path can't be equal to null.");
            }

            o.Dispose();
        }

        private static void GetData()
        {
            WriteLine("Enter printer name");
            name = ReadLine();
            WriteLine("Enter printer model");
            model = ReadLine();

            AddPrinter(name);
        }

        private static void AddPrinter(string name)
        {
            switch(name)
            {
                case "Epson":
                    manager.Add(new EpsonPrinter(name, model));
                    break;
                case "Canon":
                    manager.Add(new CanonPrinter(name, model));
                    break;
            }
        }

        private static void Subscribe()
        {
            manager.OnPrinted += PrintStatus;
        }

        private static void UnSubscribe()
        {
            manager.OnPrinted -= PrintStatus;
        }

        public static void PrintStatus(object manager, DataEventArgs e)
        {
            WriteLine(e.Message);
        }
    }  
}
