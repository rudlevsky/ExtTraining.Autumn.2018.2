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
                WriteLine("2:Add new canon");
                WriteLine("3:Add new epson");
                WriteLine("4:Print on printer");
                WriteLine("5:Print on canon");
                WriteLine("6:Print on epson");
                WriteLine("0:Exit");

                int choice = int.Parse(ReadLine());

                if (choice == 0) break;

                switch (choice)
                {
                    case 1:
                        GetData();
                        manager.Add(new StandartPrinter(name, model));
                        break;
                    case 2:
                        GetData();
                        manager.Add(new CanonPrinter(name, model));
                        break;
                    case 3:
                        GetData();
                        manager.Add(new EpsonPrinter(name, model));
                        break;
                    case 4:
                        Print(typeof(StandartPrinter), false);
                        break;
                    case 5:
                        Print(typeof(CanonPrinter), false);
                        break;
                    case 6:
                        Print(typeof(EpsonPrinter), true);
                        break;
                    default:
                        break;
                }
            }
        }

        private static void Print(Type type, bool logger)
        {
            Write(manager.ShowPrinters(type));
            WriteLine("Enter a number (starts from 1):");

            int userChoise = 0;

            try
            {
                userChoise = int.Parse(ReadLine());
            }
            catch (FormatException)
            {
                Write("Please, input a number.");
                return;
            }

            var o = new OpenFileDialog();
            o.ShowDialog();

            try
            {
                var file = File.OpenRead(o.FileName);

                if (logger)
                {
                    Write(manager.PrintLog(type, file, userChoise));
                }
                else
                {
                    Write(manager.Print(type, file, userChoise));
                }
            }
            catch(ArgumentException)
            {
                WriteLine("File path can't be equal to null.");
            }
        }

        private static void GetData()
        {
            WriteLine("Enter printer name");
            name = ReadLine();
            WriteLine("Enter printer model");
            model = ReadLine();
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
