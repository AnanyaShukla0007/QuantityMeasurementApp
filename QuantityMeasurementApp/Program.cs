using QuantityMeasurementApp.Menu;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            var menu = new ConsoleMenu();
            menu.Run();
        }
    }
}