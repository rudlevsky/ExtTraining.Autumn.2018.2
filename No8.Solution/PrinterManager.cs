using No8.Solution.Models;
using No8.Solution.Printers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace No8.Solution
{
    /// <summary>
    /// Manager of all printers.
    /// </summary>
    public class PrinterManager
    {
        /// <summary>
        /// Constructor which creates printer list.
        /// </summary>
        public PrinterManager()
        {
            printers = new List<Printer>();
        }

        /// <summary>
        /// Event for getting messages.
        /// </summary>
        public event EventHandler<DataEventArgs> OnPrinted = delegate { };

        /// <summary>
        /// List of all printers.
        /// </summary>
        private List<Printer> printers;

        /// <summary>
        /// Adds printer in the printer list.
        /// </summary>
        /// <param name="printer">Printer for adding.</param>
        public void Add(Printer printer)
        {
            if (!Contains(printer))
            {
                printers.Add(printer);
                printer.OnPrint += PrintInfo;
            }
        }

        /// <summary>
        /// Shows all printers according printer type.
        /// </summary>
        /// <param name="type">Type of the showing printers.</param>
        /// <returns>String pepresentation of the printers.</returns>
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

        /// <summary>
        /// Gets printed data prom the printer and writes info in the logger.
        /// </summary>
        /// <param name="type">Type of the printer.</param>
        /// <param name="stream">Stream for detting data.</param>
        /// <param name="model">Model of the printer.</param>
        /// <returns>String pepresentation of the printing.</returns>
        public string PrintLog(Type type, FileStream stream, string model)
        {
            string data = string.Empty;

            using (var fileStream = new FileStream("log.txt", FileMode.Append))
            {
                using (var streamWriter = new StreamWriter(fileStream))
                {
                    streamWriter.WriteLine("Printing started.");
                    data = Print(type, stream, model);
                    streamWriter.WriteLine("Printing finished.");

                }
            }

            return data;
        }

        /// <summary>
        /// Gets printed data prom the printer.
        /// </summary>
        /// <param name="type">Type of the printer.</param>
        /// <param name="stream">Stream for detting data.</param>
        /// <param name="model">Model of the printer.</param>
        /// <returns>String pepresentation of the printing.</returns>
        public string Print(Type type, FileStream stream, string model)
        {
            string data = string.Empty;

            foreach (var printer in printers)
            {
                if (printer.GetType() == type && printer.Model == model)
                {
                    data = printer.Print(stream);
                    break;
                }
            }

            return data;
        }

        private bool Contains(Printer printer)
        {
            foreach (var item in printers)
            {
                if(printer.Equals(item))
                {
                    return true;
                }
            }

            return false;
        }

        private void PrintInfo(object manager, DataEventArgs e)
        =>  OnPrinted(this, e);
    }
}
