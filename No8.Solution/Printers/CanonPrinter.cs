
namespace No8.Solution.Printers
{
    /// <summary>
    /// Class of Canon printer.
    /// </summary>
    public sealed class CanonPrinter : Printer
    {
        /// <summary>
        /// Canon printer constructor.
        /// </summary>
        /// <param name="name">Name of the printer.</param>
        /// <param name="model">Model of the printer.</param>
        public CanonPrinter(string name, string model) : base(name, model)
        {
        }
    }
}
