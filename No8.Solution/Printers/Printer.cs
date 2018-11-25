using No8.Solution.Models;
using System;
using System.IO;
using System.Text;

namespace No8.Solution.Printers
{
    /// <summary>
    /// Class of ther printer.
    /// </summary>
    public class Printer : IEquatable<Printer>
    {
        /// <summary>
        /// Event for getting messages.
        /// </summary>
        public event EventHandler<DataEventArgs> OnPrint = delegate { };

        /// <summary>
        /// Constructor of the printer.
        /// </summary>
        /// <param name="name">Name of the printer.</param>
        /// <param name="model">Model of the printer.</param>
        public Printer(string name, string model)
        {
            Name = name;
            Model = model;
        }

        /// <summary>
        /// Name of the printer.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Model of the printer.
        /// </summary>
        public string Model { get; private set; }

        /// <summary>
        /// Method for getting equals result.
        /// </summary>
        /// <param name="other">Name of the comparing printer.</param>
        /// <returns>Boolean result of  comparing.</returns>
        public bool Equals(Printer other)
        => (GetType() == other.GetType() && Name == other.Name && Model == other.Model) ?
            true : false;

        /// <summary>
        /// Method for getting equals result.
        /// </summary>
        /// <param name="obj">Name of the comparing printer.</param>
        /// <returns>Boolean result of  comparing.</returns>
        public override bool Equals(object obj)
        => (obj as Printer) == null ? false : Equals(obj as Printer);

        /// <summary>
        /// Method for getting hash code.
        /// </summary>
        /// <returns>Result of getting.</returns>
        public override int GetHashCode()
        => Name.Length + Model.Length;
        
        /// <summary>
        /// Method prints a data according a filestream..
        /// </summary>
        /// <param name="fs">File stream of the data.</param>
        /// <returns>Data in string representation.</returns>
        public virtual string Print(FileStream fs)
        {
            OnPrint?.Invoke(this, new DataEventArgs { Message = $"Printing is in a process...(Name: {Name}, Model: {Model})" });

            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, bytes.Length);

            var builder = new StringBuilder();

            foreach (var item in bytes)
            {
                builder.Append(item + Environment.NewLine);
            }

            OnPrint?.Invoke(this, new DataEventArgs { Message = $"Printing ended. (Name: {Name}, Model: {Model})" });

            return builder.ToString();
        }
    }
}
