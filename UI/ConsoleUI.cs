using System;
using System.Collections.Generic;
using System.Globalization;
using GarageApp.Models;
using GarageApp.Services;

namespace GarageApp.UI;

public class ConsoleUI
{
    // Program llama a este método para iniciar la interfaz.
    public void Run()
    {
        try
        {
            RunGarage();
        }
        catch (OperationCanceledException)
        {
            Console.WriteLine();
            Console.WriteLine("Application closed.");
        }
    }

    private void RunGarage()
    {
        Console.WriteLine("Welcome to Garage 2.0!");

        int capacity = ReadInteger("Enter the garage capacity: ", 1);

        // Crea un garaje con una lista vacía y la capacidad indicada.
        Garage garage = new Garage(capacity);

        LoadSampleVehicles(garage);

        bool running = true;

        while (running)
        {
            // GetVehicles devuelve una lista: utilizamos Count.
            int occupiedSpaces = garage.GetVehicles().Count;

            Console.WriteLine();
            Console.WriteLine("=== Garage 2.0 ===");
            Console.WriteLine($"Capacity: {garage.Capacity}");
            Console.WriteLine($"Occupied spaces: {occupiedSpaces}");
            Console.WriteLine(
                $"Free spaces: {garage.Capacity - occupiedSpaces}");

            Console.WriteLine("1. List parked vehicles");
            Console.WriteLine("2. Park a vehicle");
            Console.WriteLine("3. Remove a vehicle");
            Console.WriteLine("4. Show vehicle counts by type");
            Console.WriteLine("5. Search vehicles");
            Console.WriteLine("0. Exit");

            string choice = ReadText("Choose an option: ");

            switch (choice)
            {
                case "1":
                    ShowVehicles(garage.GetVehicles());
                    break;

                case "2":
                    ParkVehicle(garage);
                    break;

                case "3":
                    RemoveVehicle(garage);
                    break;

                case "4":
                    ShowVehicleCounts(garage);
                    break;

                case "5":
                    SearchMenu(garage);
                    break;

                case "0":
                    running = false;
                    Console.WriteLine("Goodbye!");
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private void LoadSampleVehicles(Garage garage)
    {
        string answer;

        while (true)
        {
            answer = ReadText("Load sample vehicles? (y/n): ");

            if (answer.Equals("y", StringComparison.OrdinalIgnoreCase) ||
                answer.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            Console.WriteLine("Enter y or n.");
        }

        if (answer.Equals("n", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        // Este array está en ConsoleUI, no dentro de Garage.
        // Por eso puede conservarse según el enunciado.
        Vehicle[] samples =
        {
            new Car("CAR123", "Black", 4, "Gasoline"),
            new Motorcycle("MOT123", "Pink", 3, 500),
            new Airplane("AIR123", "White", 3, 2),
            new Bus("BUS123", "Red", 6, 40),
            new Boat("BOA123", "Blue", 0, 6.5m)
        };

        int added = 0;

        foreach (Vehicle vehicle in samples)
        {
            if (garage.TryPark(vehicle))
            {
                added++;
            }
        }

        Console.WriteLine($"Sample vehicles parked: {added}.");

        // samples es un array: aquí utilizamos Length.
        if (added < samples.Length)
        {
            Console.WriteLine(
                "Some sample vehicles could not be parked " +
                "because there were not enough spaces.");
        }
    }

    private void ParkVehicle(Garage garage)
    {
        if (garage.GetVehicles().Count >= garage.Capacity)
        {
            Console.WriteLine("The garage is full.");
            return;
        }

        Console.WriteLine();
        Console.WriteLine("=== Select vehicle type ===");
        Console.WriteLine("1. Car");
        Console.WriteLine("2. Motorcycle");
        Console.WriteLine("3. Airplane");
        Console.WriteLine("4. Bus");
        Console.WriteLine("5. Boat");
        Console.WriteLine("0. Back");

        int choice = ReadInteger("Choose a vehicle type: ", 0);

        if (choice == 0)
        {
            return;
        }

        if (choice > 5)
        {
            Console.WriteLine("Invalid vehicle type.");
            return;
        }

        string registrationNumber = ReadText("Registration number: ");

        if (garage.FindByRegistrationNumber(registrationNumber) != null)
        {
            Console.WriteLine(
                "A vehicle with this registration number is already parked.");
            return;
        }

        string color = ReadText("Color: ");
        int numberOfWheels = ReadInteger("Number of wheels: ", 0);

        try
        {
            // Apuntará al vehículo creado en el switch.
            Vehicle vehicle;

            switch (choice)
            {
                case 1:
                    string fuelType = ReadText(
                        "Fuel type (for example, Gasoline or Diesel): ");

                    vehicle = new Car(
                        registrationNumber,
                        color,
                        numberOfWheels,
                        fuelType);
                    break;

                case 2:
                    int cylinderVolume = ReadInteger(
                        "Cylinder volume in cc: ", 1);

                    vehicle = new Motorcycle(
                        registrationNumber,
                        color,
                        numberOfWheels,
                        cylinderVolume);
                    break;

                case 3:
                    int numberOfEngines = ReadInteger(
                        "Number of engines: ", 1);

                    vehicle = new Airplane(
                        registrationNumber,
                        color,
                        numberOfWheels,
                        numberOfEngines);
                    break;

                case 4:
                    int numberOfSeats = ReadInteger(
                        "Number of seats: ", 1);

                    vehicle = new Bus(
                        registrationNumber,
                        color,
                        numberOfWheels,
                        numberOfSeats);
                    break;

                case 5:
                    decimal length = ReadPositiveDecimal(
                        "Length in meters (use a decimal point): ");

                    vehicle = new Boat(
                        registrationNumber,
                        color,
                        numberOfWheels,
                        length);
                    break;

                default:
                    return;
            }

            if (garage.TryPark(vehicle))
            {
                Console.WriteLine("Vehicle parked successfully.");
            }
            else if (garage.GetVehicles().Count >= garage.Capacity)
            {
                Console.WriteLine("Could not park: the garage is full.");
            }
            else
            {
                Console.WriteLine(
                    "Could not park: the registration number is already parked.");
            }
        }
        catch (ArgumentException error)
        {
            Console.WriteLine(error.Message);
        }
    }

    private void RemoveVehicle(Garage garage)
    {
        Console.WriteLine();
        Console.WriteLine("=== Remove a vehicle ===");

        string registrationNumber = ReadText("Registration number: ");

        if (garage.TryRemove(registrationNumber))
        {
            Console.WriteLine("Vehicle removed successfully.");
        }
        else
        {
            Console.WriteLine(
                "No vehicle with that registration number was found.");
        }
    }

    // Ahora recibe una lista, igual que la que devuelve Garage.
    private void ShowVehicles(List<Vehicle> vehicles)
    {
        if (vehicles.Count == 0)
        {
            Console.WriteLine("No vehicles found.");
            return;
        }

        foreach (Vehicle vehicle in vehicles)
        {
            ShowVehicle(vehicle);
        }
    }

    private void ShowVehicle(Vehicle vehicle)
    {
        Console.WriteLine();
        Console.WriteLine($"Type: {vehicle.GetType().Name}");
        Console.WriteLine($"Registration: {vehicle.RegistrationNumber}");
        Console.WriteLine($"Color: {vehicle.Color}");
        Console.WriteLine($"Wheels: {vehicle.NumberOfWheels}");

        // Identifica el tipo concreto para mostrar su propiedad propia.
        if (vehicle is Car car)
        {
            Console.WriteLine($"Fuel type: {car.FuelType}");
        }
        else if (vehicle is Motorcycle motorcycle)
        {
            Console.WriteLine(
                $"Cylinder volume: {motorcycle.CylinderVolume} cc");
        }
        else if (vehicle is Airplane airplane)
        {
            Console.WriteLine(
                $"Number of engines: {airplane.NumberOfEngines}");
        }
        else if (vehicle is Bus bus)
        {
            Console.WriteLine($"Number of seats: {bus.NumberOfSeats}");
        }
        else if (vehicle is Boat boat)
        {
            Console.WriteLine($"Length: {boat.Length} meters");
        }
    }

    private void ShowVehicleCounts(Garage garage)
    {
        Console.WriteLine();
        Console.WriteLine("=== Vehicle counts ===");
        Console.WriteLine($"Cars: {garage.CountByType("Car")}");
        Console.WriteLine(
            $"Motorcycles: {garage.CountByType("Motorcycle")}");
        Console.WriteLine($"Airplanes: {garage.CountByType("Airplane")}");
        Console.WriteLine($"Buses: {garage.CountByType("Bus")}");
        Console.WriteLine($"Boats: {garage.CountByType("Boat")}");
    }

    private void SearchMenu(Garage garage)
    {
        bool searching = true;

        while (searching)
        {
            Console.WriteLine();
            Console.WriteLine("=== Search vehicles ===");
            Console.WriteLine("1. Search by registration number");
            Console.WriteLine("2. Search with combined filters");
            Console.WriteLine("0. Back");

            string choice = ReadText("Choose an option: ");

            switch (choice)
            {
                case "1":
                    string registrationNumber = ReadText(
                        "Registration number: ");

                    Vehicle? vehicle = garage.FindByRegistrationNumber(
                        registrationNumber);

                    if (vehicle == null)
                    {
                        Console.WriteLine("Vehicle not found.");
                    }
                    else
                    {
                        ShowVehicle(vehicle);
                    }

                    break;

                case "2":
                    SearchWithFilters(garage);
                    break;

                case "0":
                    searching = false;
                    break;

                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    private void SearchWithFilters(Garage garage)
    {
        Console.WriteLine();
        Console.WriteLine("Leave a filter empty to skip it.");

        string registrationNumber = ReadText(
            "Registration number: ", true);

        string color = ReadText("Color: ", true);

        // null significa que no se aplica el filtro de ruedas.
        int? numberOfWheels = null;

        while (true)
        {
            string wheelsInput = ReadText("Number of wheels: ", true);

            if (wheelsInput == "")
            {
                break;
            }

            if (int.TryParse(wheelsInput, out int wheels) && wheels >= 0)
            {
                numberOfWheels = wheels;
                break;
            }

            Console.WriteLine(
                "Enter a whole number greater than or equal to zero, " +
                "or leave it empty.");
        }

        string vehicleType;

        while (true)
        {
            vehicleType = ReadText(
                "Type (Car, Motorcycle, Airplane, Bus, Boat): ", true);

            if (vehicleType == "" ||
                vehicleType.Equals("Car", StringComparison.OrdinalIgnoreCase) ||
                vehicleType.Equals("Motorcycle", StringComparison.OrdinalIgnoreCase) ||
                vehicleType.Equals("Airplane", StringComparison.OrdinalIgnoreCase) ||
                vehicleType.Equals("Bus", StringComparison.OrdinalIgnoreCase) ||
                vehicleType.Equals("Boat", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            Console.WriteLine("Enter one of the listed types.");
        }

        // Search devuelve una lista de coincidencias.
        List<Vehicle> results = garage.Search(
            registrationNumber,
            color,
            numberOfWheels,
            vehicleType);

        Console.WriteLine();
        Console.WriteLine($"Vehicles found: {results.Count}");

        ShowVehicles(results);
    }

    private string ReadText(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write(prompt);

            string? input = Console.ReadLine();

            // La entrada cerrada termina la aplicación de forma controlada.
            if (input == null)
            {
                throw new OperationCanceledException();
            }

            input = input.Trim();

            if (allowEmpty || input != "")
            {
                return input;
            }

            Console.WriteLine("This field cannot be empty.");
        }
    }

    private int ReadInteger(string prompt, int minimum)
    {
        while (true)
        {
            string input = ReadText(prompt);

            // TryParse evita una excepción si se escriben letras.
            if (int.TryParse(input, out int number) &&
                number >= minimum)
            {
                return number;
            }

            Console.WriteLine(
                $"Enter a whole number greater than or equal to {minimum}.");
        }
    }

    private decimal ReadPositiveDecimal(string prompt)
    {
        while (true)
        {
            string input = ReadText(prompt);

            // Usa punto decimal, independientemente del idioma del sistema.
            if (decimal.TryParse(
                input,
                NumberStyles.AllowDecimalPoint |
                NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture,
                out decimal number) &&
                number > 0)
            {
                return number;
            }

            Console.WriteLine(
                "Enter a number greater than zero. Use a decimal point.");
        }
    }
}