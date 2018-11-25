
namespace No8.Solution.Printers
{
    /// <summary>
    /// Class of Canon printer.
    /// </summary>
    public sealed class EpsonPrinter : Printer
    {
        /// <summary>
        /// Epson printer constructor.
        /// </summary>
        /// <param name="name">Name of the printer.</param>
        /// <param name="model">Model of the printer.</param>
        public EpsonPrinter(string name, string model) : base(name, model)
        {
        }
    }
}
