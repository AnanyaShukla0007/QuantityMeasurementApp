using QuantityMeasurementApp.Controller.Interface;
using QuantityMeasurementApp.Controller.Menu;
using QuantityMeasurementApp.Controller.Factory;

namespace QuantityMeasurementApp.Controller
{
    class Program
    {
        static void Main()
        {
            ServiceFactory factory = new ServiceFactory();

            IMenu menu = new MainMenu(factory);

            menu.Display();
        }
    }
}