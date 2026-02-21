using System;
using QuantityMeasurementApp.Services;

namespace QuantityMeasurementApp.Menu
{
    /// <summary>
    /// Handles menu-driven console interaction and routes user input to service layer.
    /// </summary>
    public class ConsoleMenu
    {
        // Service dependency
        private readonly QuantityMeasurementService _service;

        // Constructor initializes service
        public ConsoleMenu()
        {
            _service = new QuantityMeasurementService();
        }

        // Runs continuous menu loop
        public void Run()
        {
            while (true)
            {
                try
                {
                    // Display options
                    ShowMenu();

                    // Read user choice
                    string choice = Console.ReadLine();

                    // Route based on choice
                    switch (choice)
                    {
                        case "1":
                            HandleFeetEquality();
                            break;

                        case "2":
                            HandleInchesEquality();
                            break;

                        case "3":
                            return;

                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    // Handle exceptions without crashing application
                    Console.WriteLine($"Error: {ex.Message}");
                }

                // Pause before returning to menu
                Console.WriteLine("\nPress any key to return to menu...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        // Displays menu options
        private void ShowMenu()
        {
            Console.WriteLine("=== Quantity Measurement Menu ===");
            Console.WriteLine("1. Check Feet Equality");
            Console.WriteLine("2. Check Inches Equality");
            Console.WriteLine("3. Exit");
            Console.Write("Enter choice: ");
        }

        // Handles feet equality operation
        private void HandleFeetEquality()
        {
            // Prompt for first value
            Console.Write("Enter first feet value: ");
            double value1 = double.Parse(Console.ReadLine());

            // Prompt for second value
            Console.Write("Enter second feet value: ");
            double value2 = double.Parse(Console.ReadLine());

            // Call service for validation
            bool result = _service.ValidateFeetEquality(value1, value2);

            // Display result
            Console.WriteLine($"Equal ({result})");
        }

        // Handles inches equality operation
        private void HandleInchesEquality()
        {
            // Prompt for first value
            Console.Write("Enter first inches value: ");
            double value1 = double.Parse(Console.ReadLine());

            // Prompt for second value
            Console.Write("Enter second inches value: ");
            double value2 = double.Parse(Console.ReadLine());

            // Call service for validation
            bool result = _service.ValidateInchesEquality(value1, value2);

            // Display result
            Console.WriteLine($"Equal ({result})");
        }
    }
}