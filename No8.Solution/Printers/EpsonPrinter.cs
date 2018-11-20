using No8.Solution.Interfaces;
using System;
using System.IO;
using System.Text;

namespace No8.Solution.Printers
{
    public class EpsonPrinter : IPrinter
    {
        public EpsonPrinter(string name, string model)
        {
            Name = name;
            Model = model;
        }

        public string Name { get; set; }

        public string Model { get; set; }

        public string Print(FileStream fs)
        {
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);

            var builder = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i] + Environment.NewLine);
            }

            return builder.ToString();
        }
    }
}
