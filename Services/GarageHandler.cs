using System.Collections.Generic;
using GarageApp.Interfaces;
using GarageApp.Models;

namespace GarageApp.Services;

public class GarageHandler : IHandler
//     ↑ CLASE: la reconoces por class.
//                         ↑ Implementa IHandler:
//                           ofrece las operaciones que exige la interfaz.
{
    private readonly Garage<Vehicle> _garage;
    // ↑ CAMPO PRIVADO: permite acceder al garaje desde esta clase.
    // Garage<Vehicle>: acepta Vehicle y sus subclases.
    // readonly: este campo no podrá apuntar a otro garaje
    // después de terminar el constructor.

    public GarageHandler(int capacity)
    //     ↑ CONSTRUCTOR:
    //       Se llama GarageHandler, igual que la clase.
    //       Recibe la capacidad inicial.
    {
        _garage = new Garage<Vehicle>(capacity);
        // ↑ Crea el garaje con la capacidad recibida.
        // Su constructor comprueba que sea mayor que cero.
    }

    public int Capacity
    // ↑ PROPIEDAD: permite consultar la capacidad.
    {
        get
        {
            return _garage.Capacity;
            // ↑ Consulta el garaje y entrega su capacidad.
        }
    }

    public int OccupiedSpaces
    // ↑ PROPIEDAD: calcula cuántos vehículos hay al consultarla.
    {
        get
        {
            int count = 0;

            foreach (Vehicle vehicle in _garage)
            // ↑ Recorre directamente el garaje.
            // Esto funciona porque Garage<T> implementa IEnumerable<T>.
            {
                count++;
                // ↑ Suma uno por cada vehículo encontrado.
            }

            return count;
        }
    }

    public bool TryPark(Vehicle vehicle)
    // ↑ MÉTODO: recibe el vehículo enviado por el menú.
    {
        return _garage.TryPark(vehicle);
        // ↑ Pide al garaje que lo estacione.
        // Devuelve al menú el resultado: true o false.
    }

    public bool TryRemove(string registrationNumber)
    {
        return _garage.TryRemove(registrationNumber);
        // ↑ Entrega la matrícula al garaje.
        // Devuelve true si retiró el vehículo o false si no lo encontró.
    }

    public Vehicle? FindByRegistrationNumber(string registrationNumber)
    {
        return _garage.FindByRegistrationNumber(registrationNumber);
        // ↑ Pide buscar la matrícula.
        // Entrega el vehículo encontrado o null.
    }

    public List<Vehicle> GetVehicles()
    {
        return _garage.GetVehicles();
        // ↑ Entrega la copia de la lista que prepara Garage.
    }

    public int CountByType(string vehicleType)
    {
        return _garage.CountByType(vehicleType);
        // ↑ Pide contar un tipo, por ejemplo Car o Boat.
        // Entrega el número obtenido.
    }

    public List<Vehicle> Search(
        string registrationNumber,
        string color,
        int? numberOfWheels,
        string vehicleType)
    {
        return _garage.Search(
            registrationNumber,
            color,
            numberOfWheels,
            vehicleType);
        // ↑ Envía los cuatro filtros al garaje.
        // Entrega la lista de coincidencias.
    }
}