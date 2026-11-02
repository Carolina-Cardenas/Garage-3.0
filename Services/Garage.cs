using System;
using System.Collections;
// ↑ Permite utilizar IEnumerator e IEnumerable sin <T>.

using System.Collections.Generic;
// ↑ Permite utilizar List<T>, IEnumerable<T> e IEnumerator<T>.

using GarageApp.Models;

namespace GarageApp.Services;

public class Garage<T> : IEnumerable<T> where T : Vehicle
//           ↑ CLASE GENÉRICA:
//             T representa el tipo de vehículos que elegiremos.
//
//                      ↑ INTERFAZ:
//                        Permite recorrer este garaje con foreach.
//
//                                          ↑ RESTRICCIÓN:
//                                            T debe ser Vehicle
//                                            o una clase que herede de Vehicle.
{
    private readonly List<T> _vehicles;
    // ↑ CAMPO PRIVADO: guarda el acceso a la lista de vehículos.
    // List<T>: la lista acepta elementos del tipo elegido para T.
    // readonly: no podemos reemplazar esta lista después del constructor.
    // Sí podemos añadir o retirar vehículos de ella.

    public int Capacity { get; }
    // ↑ PROPIEDAD: permite consultar la capacidad.
    // Se guarda inicialmente en el constructor.
    // Otro código no puede cambiarla directamente.

    public Garage(int capacity)
    //     ↑ CONSTRUCTOR:
    //       Tiene el mismo nombre que la clase: Garage.
    //       Aquí NO escribimos Garage<T>.
    //       Recibe la cantidad máxima de vehículos.
    {
        if (capacity <= 0)
        {
            throw new ArgumentException(
                "Garage capacity must be greater than zero.");
        }

        Capacity = capacity;

        _vehicles = new List<T>();
        // ↑ Creamos la lista vacía cuando se crea el garaje.
    }

    public T? FindByRegistrationNumber(string registrationNumber)
    //     ↑ Devuelve un vehículo del tipo T.
    //      ? permite devolver null si no encuentra ninguno.
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return null;
        }

        foreach (T vehicle in _vehicles)
        //       ↑ Tipo elegido para este garaje.
        //         vehicle permite acceder al vehículo de esta vuelta.
        //                 in indica qué colección recorremos.
        {
            if (string.Equals(
                vehicle.RegistrationNumber,
                registrationNumber.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                return vehicle;
            }
        }

        return null;
    }

    public bool TryPark(T vehicle)
    // ↑ MÉTODO: intenta estacionar el vehículo recibido.
    // Devuelve true si lo añade y false si no puede.
    {
        if (vehicle == null)
        {
            return false;
        }

        if (_vehicles.Count >= Capacity)
        // ↑ Count: cantidad de vehículos que contiene la lista.
        {
            return false;
        }

        if (FindByRegistrationNumber(vehicle.RegistrationNumber) != null)
        // ↑ Evita estacionar dos vehículos con la misma matrícula.
        {
            return false;
        }

        _vehicles.Add(vehicle);
        // ↑ Añade el vehículo a la lista privada.

        return true;
    }

    public bool TryRemove(string registrationNumber)
    // ↑ MÉTODO: intenta retirar el vehículo con esa matrícula.
    {
        if (string.IsNullOrWhiteSpace(registrationNumber))
        {
            return false;
        }

        for (int i = 0; i < _vehicles.Count; i++)
        {
            T vehicle = _vehicles[i];
            // ↑ Consulta el vehículo situado en la posición i.

            if (string.Equals(
                vehicle.RegistrationNumber,
                registrationNumber.Trim(),
                StringComparison.OrdinalIgnoreCase))
            {
                _vehicles.RemoveAt(i);
                // ↑ Retira el vehículo de esa posición.

                return true;
            }
        }

        return false;
    }

    public List<T> GetVehicles()
    // ↑ MÉTODO: entrega una copia de la lista.
    {
        return new List<T>(_vehicles);
        // Cambiar esta copia no añade ni retira elementos
        // de la lista privada del garaje.
        // Los vehículos contenidos siguen siendo los mismos objetos.
    }

    public int CountByType(string vehicleType)
    // ↑ MÉTODO: cuenta los vehículos del tipo solicitado.
    {
        if (string.IsNullOrWhiteSpace(vehicleType))
        {
            return 0;
        }

        int count = 0;

        foreach (T vehicle in _vehicles)
        {
            string typeName = vehicle.GetType().Name;
            // ↑ Obtiene el nombre de la clase del objeto:
            // Car, Motorcycle, Bus, Boat o Airplane.

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

    public List<T> Search(
        string registrationNumber,
        string color,
        int? numberOfWheels,
        string vehicleType)
    // ↑ MÉTODO: busca vehículos que cumplan todos los filtros elegidos.
    // int? permite recibir un número de ruedas o null.
    // null significa que no se filtra por ruedas.
    {
        List<T> results = new List<T>();

        foreach (T vehicle in _vehicles)
        {
            if (!string.IsNullOrWhiteSpace(registrationNumber))
            {
                if (!string.Equals(
                    vehicle.RegistrationNumber,
                    registrationNumber.Trim(),
                    StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                    // ↑ No coincide: pasa al siguiente vehículo.
                }
            }

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

            if (numberOfWheels.HasValue)
            // ↑ Comprueba si recibimos un número en lugar de null.
            {
                if (vehicle.NumberOfWheels != numberOfWheels.Value)
                {
                    continue;
                }
            }

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

            results.Add(vehicle);
            // ↑ Solo añadimos vehículos que cumplen
       
        }

        return results;
    }

    public IEnumerator<T> GetEnumerator()
    // ↑ MÉTODO EXIGIDO POR IEnumerable<T>.
    //
    // IEnumerator<T> es el encargado de controlar un recorrido:
    // permite avanzar y consultar el vehículo actual.
    //
    // foreach utiliza este método para obtener ese encargado.
    {
        return _vehicles.GetEnumerator();
        // ↑ Aprovechamos el recorrido que ya ofrece List<T>.
        // No creamos otra lista ni copiamos los vehículos.
    }

    IEnumerator IEnumerable.GetEnumerator()
    // ↑ Atiende la versión no genérica de IEnumerable,
    // que también forma parte de la exigencia de IEnumerable<T>.
    //
    // IEnumerable. indica a qué interfaz pertenece este método.
    {
        return GetEnumerator();
        // ↑ Reutiliza el método de arriba.
    }
}