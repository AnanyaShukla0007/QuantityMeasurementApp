using System;
using QuantityMeasurementApp.Controller.Factory;
using QuantityMeasurementApp.Controller.Controllers;
using QuantityMeasurementApp.Controller.Interface;
using QuantityMeasurementApp.Model.DTOs;
using QuantityMeasurementApp.Model.Enums;

namespace QuantityMeasurementApp.Controller.Menu
{
    public class MeasurementMenu : IMenu
    {
        private readonly QuantityMeasurementController _controller;

        public MeasurementMenu(ServiceFactory factory)
        {
            _controller = factory.GetController();
        }

        public void Display()
        {
            while (true)
            {
                

                //Console.WriteLine("╔════════════════════════════════════════════════════════╗");
                Console.WriteLine("║            ADVANCED MEASUREMENT SYSTEM                ║");
                //Console.WriteLine("╠════════════════════════════════════════════════════════╣");
                Console.WriteLine("║ 1. Compare Measurements                               ║");
                Console.WriteLine("║ 2. Convert Units                                      ║");
                Console.WriteLine("║ 3. Add Measurements                                   ║");
                Console.WriteLine("║ 4. Subtract Measurements                              ║");
                Console.WriteLine("║ 5. Divide Measurements                                ║");
                Console.WriteLine("║ 6. Back                                               ║");
                //Console.WriteLine("╚════════════════════════════════════════════════════════╝");

                Console.Write("\nSelect option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Compare();
                        break;

                    case "2":
                        Convert();
                        break;

                    case "3":
                        Add();
                        break;

                    case "4":
                        Subtract();
                        break;

                    case "5":
                        Divide();
                        break;

                    case "6":
                        return;

                    default:
                        Console.WriteLine("Invalid option");
                        Console.ReadKey();
                        break;
                }
            }
        }

        private QuantityDTO ReadQuantity()
        {
            Console.Write("Value: ");
            double value = double.Parse(Console.ReadLine()!);

            Console.Write("Unit: ");
            string unit = Console.ReadLine()!;

            Console.Write("Category (LENGTH/WEIGHT/VOLUME/TEMPERATURE): ");
            var category = Enum.Parse<MeasurementCategory>(Console.ReadLine()!);

            return new QuantityDTO
            {
                Value = value,
                Unit = unit,
                Category = category
            };
        }

        private void Compare()
        {
            Console.WriteLine("\nFirst Quantity");
            var q1 = ReadQuantity();

            Console.WriteLine("\nSecond Quantity");
            var q2 = ReadQuantity();

            var result = _controller.PerformComparison(q1, q2);

            Console.WriteLine(result.FormattedResult);
            Console.ReadKey();
        }

        private void Convert()
        {
            Console.WriteLine("\nSource Quantity");
            var q = ReadQuantity();

            Console.Write("Target Unit: ");
            var target = Console.ReadLine();

            var result = _controller.PerformConversion(q, target!);

            Console.WriteLine(result.FormattedResult);
            Console.ReadKey();
        }

        private void Add()
        {
            Console.WriteLine("\nFirst Quantity");
            var q1 = ReadQuantity();

            Console.WriteLine("\nSecond Quantity");
            var q2 = ReadQuantity();

            var result = _controller.PerformAddition(q1, q2);

            Console.WriteLine(result.FormattedResult);
            Console.ReadKey();
        }

        private void Subtract()
        {
            Console.WriteLine("\nFirst Quantity");
            var q1 = ReadQuantity();

            Console.WriteLine("\nSecond Quantity");
            var q2 = ReadQuantity();

            var result = _controller.PerformSubtraction(q1, q2);

            Console.WriteLine(result.FormattedResult);
            Console.ReadKey();
        }

        private void Divide()
        {
            Console.WriteLine("\nFirst Quantity");
            var q1 = ReadQuantity();

            Console.WriteLine("\nSecond Quantity");
            var q2 = ReadQuantity();

            var result = _controller.PerformDivision(q1, q2);

            Console.WriteLine(result.Interpretation);
            Console.ReadKey();
        }
    }
}