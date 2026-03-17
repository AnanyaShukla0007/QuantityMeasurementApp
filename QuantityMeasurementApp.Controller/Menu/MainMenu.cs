using System;
using QuantityMeasurementApp.Controller.Factory;
using QuantityMeasurementApp.Controller.Interface;
using QuantityMeasurementApp.Menu;

namespace QuantityMeasurementApp.Controller.Menu
{
    public class MainMenu :IMenu
    {

        private readonly MeasurementMenu _advancedMenu;
        private readonly ConsoleMenu _classicMenu;

        public MainMenu(ServiceFactory factory)
        {
            _advancedMenu = new MeasurementMenu(factory);
            _classicMenu = new ConsoleMenu();   // UC1–UC14
        }

        public void Display()
        {
            while (true)
            {
                
                DisplayHeader();

                // Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║                        MAIN MENU                       ║");
                // Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                // Console.WriteLine("║                                                        ║");
                Console.WriteLine("║    1.  Classic Measurement System                     ║");
                Console.WriteLine("║    2.  Advanced Measurement System                    ║");
                Console.WriteLine("║    3.  Exit                                           ║");
                // Console.WriteLine("║                                                        ║");
                // Console.WriteLine("╚════════════════════════════════════════════════════════╝");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        _classicMenu.Run();
                        break;

                    case "2":
                        _advancedMenu.Display();
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("\nInvalid option.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private void DisplayHeader()
        {
            

            //Console.WriteLine("╔════════════════════════════════════════════════════════╗");
            Console.WriteLine("║         QUANTITY MEASUREMENT SYSTEM                   ║");
            //Console.WriteLine("╚════════════════════════════════════════════════════════╝");

            Console.ResetColor();
            Console.WriteLine();
        }
    }
}