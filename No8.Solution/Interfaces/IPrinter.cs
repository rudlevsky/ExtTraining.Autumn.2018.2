using System.IO;

namespace No8.Solution.Interfaces
{
    /// <summary>
    /// Interface for printers.
    /// </summary>
    public interface IPrinter
    {
        /// <summary>
        /// Name of the printer.
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Model of the printer.
        /// </summary>
        string Model { get; set; }

        /// <summary>
        /// Method for printing data.
        /// </summary>
        /// <param name="fs">Stream of data.</param>
        /// <returns>String with all data.</returns>
        string Print(FileStream fs);
    }
}
