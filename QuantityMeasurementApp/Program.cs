using QuantityMeasurementApp.Menu;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Entry point that initializes and runs the console menu.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instantiate menu layer
            var menu = new ConsoleMenu();

            // Start application loop
            menu.Run();
        }
    }
}