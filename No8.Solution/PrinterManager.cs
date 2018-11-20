using No8.Solution.Interfaces;
using No8.Solution.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace No8.Solution
{
    public class PrinterManager
    {
        public PrinterManager()
        {
            printers = new List<IPrinter>();
        }

        public event EventHandler<DataEventArgs> OnPrinted;
        private List<IPrinter> printers;

        public void Add(IPrinter printer)
        {
            if (!Contains(printer))
            {
                printers.Add(printer);
            }
        }

        private bool Contains(IPrinter printer)
        {
            foreach (var item in printers)
            {
                if(item.Name == printer.Name && item.Model == printer.Model)
                {
                    return true;
                }
            }

            return false;
        }

        public string ShowPrinters(Type type)
        {
            var builder = new StringBuilder();

            foreach (var item in printers)
            {
                if (item.GetType() == type)
                {
                    builder.Append(item.Name + " - " + item.Model + Environment.NewLine);
                }
            }

            return builder.ToString();
        }

        public string PrintLog(Type type, FileStream stream, int number)
        {
            string data = string.Empty;

            using (var fileStream = new FileStream("log.txt", FileMode.Append))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine("Print started");
                    data = Print(type, stream, number);
                    streamWriter.WriteLine("Print finished");

                }
            }
                
            return data;
        }

        public string Print(Type type, FileStream stream, int number)
        {
            OnPrinted?.Invoke(this, new DataEventArgs { Message = "Printing is in a process... " });

            int count = 0;
            string data = string.Empty;

            foreach (var printer in printers)
            {
                if(printer.GetType() == type)
                {
                    if (count == number - 1)
                    {
                        data = printer.Print(stream);
                        break;
                    }

                    count++;
                }                   
            }

            return data;
        }
    }
}
