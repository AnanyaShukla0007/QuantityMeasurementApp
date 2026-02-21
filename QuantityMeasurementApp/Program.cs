using QuantityMeasurementApp.Menu;

namespace QuantityMeasurementApp
{
    /// <summary>
    /// Application entry point that initializes and runs the console menu.
    /// </summary>
    internal class Program
    {
        static void Main(string[] args)
        {
            // Instantiate menu
            var menu = new ConsoleMenu();

            // Run application
            menu.Run();
        }
    }
}