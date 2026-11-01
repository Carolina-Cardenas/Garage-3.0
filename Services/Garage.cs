using System;
using System.Collections.Generic;
using GarageApp.Models;

namespace GarageApp.Services;

public class Garage
{
    // Lista privada: contiene los vehículos estacionados.
    private List<Vehicle> _vehicles;

    // Máximo de vehículos permitidos en el garaje.
    public int Capacity { get; }

    public Garage(int capacity)
    {
        if (capacity <= 0)
        {
            throw new ArgumentException(
                "Garage capacity must be greater than zero.");
        }

        // Guardamos el límite y creamos una lista vacía.
        Capacity = capacity;
        _vehicles = new List<Vehicle>();
    }

    public Vehicle? FindByRegistrationNumber(string registrationNumber)
    {
        // Evita llamar a Trim sobre un texto nulo.
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return null;
        }

        foreach (Vehicle vehicle in _vehicles)
        {
            // Compara las matrículas sin distinguir mayúsculas.
            if (string.Equals(
                vehicle.RegistrationNumber,
                registrationNumber.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                return vehicle;
            }
        }

        // No encontramos el vehículo.
        return null;
    }

    public bool TryPark(Vehicle vehicle)
    {
        if (vehicle == null)
        {
            return false;
        }

        // Count indica cuántos vehículos están estacionados.
        if (_vehicles.Count >= Capacity)
        {
            return false;
        }

        // Rechaza una matrícula que ya existe en el garaje.
        if (FindByRegistrationNumber(vehicle.RegistrationNumber) != null)
        {
            return false;
        }

        // Add añade el vehículo y aumenta Count automáticamente.
        _vehicles.Add(vehicle);

        return true;
    }

    public bool TryRemove(string registrationNumber)
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return false;
        }

        for (int i = 0; i < _vehicles.Count; i++)
        {
            Vehicle vehicle = _vehicles[i];

            if (string.Equals(
                vehicle.RegistrationNumber,
                registrationNumber.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                // Elimina el elemento situado en el índice i.
                // Count disminuye automáticamente.
                _vehicles.RemoveAt(i);

                return true;
            }
        }

        return false;
    }

    public List<Vehicle> GetVehicles()
    {
        // Devuelve una copia para no exponer la lista privada.
        return new List<Vehicle>(_vehicles);
    }

    public int CountByType(string vehicleType)
    {
        if (string.IsNullOrWhiteSpace(vehicleType))
        {
            return 0;
        }

        // Aquí sí necesitamos un contador:
        // contamos solo los vehículos del tipo solicitado.
        int count = 0;

        foreach (Vehicle vehicle in _vehicles)
        {
            // Obtiene el nombre del tipo real: Car, Bus, Boat...
            string typeName = vehicle.GetType().Name;

            if (string.Equals(
                typeName,
                vehicleType.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                count++;
            }
        }

        return count;
    }

    public List<Vehicle> Search(
        string registrationNumber,
        string color,
        int? numberOfWheels,
        string vehicleType)
    {
        // Empieza vacía; añadiremos las coincidencias con Add.
        List<Vehicle> results = new List<Vehicle>();

        foreach (Vehicle vehicle in _vehicles)
        {
            // Texto vacío: no aplicar el filtro de matrícula.
            if (!string.IsNullOrWhiteSpace(registrationNumber))
            {
                if (!string.Equals(
                    vehicle.RegistrationNumber,
                    registrationNumber.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    // No coincide: pasa al siguiente vehículo.
                    continue;
                }
            }

            // Texto vacío: no aplicar el filtro de color.
            if (!string.IsNullOrWhiteSpace(color))
            {
                if (!string.Equals(
                    vehicle.Color,
                    color.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }

            // null: no filtrar por ruedas.
            // Cero: buscar vehículos con cero ruedas.
            if (numberOfWheels.HasValue)
            {
                if (vehicle.NumberOfWheels != numberOfWheels.Value)
                {
                    continue;
                }
            }

            // Texto vacío: no aplicar el filtro de tipo.
            if (!string.IsNullOrWhiteSpace(vehicleType))
            {
                if (!string.Equals(
                    vehicle.GetType().Name,
                    vehicleType.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
            }

            // Solo llega aquí si cumple todos los filtros activos.
            results.Add(vehicle);
        }

        // La lista ya contiene exactamente las coincidencias.
        return results;
    }
}