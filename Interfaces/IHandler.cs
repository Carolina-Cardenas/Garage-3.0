using System.Collections.Generic;
using GarageApp.Models;

namespace GarageApp.Interfaces;

public interface IHandler
//     ↑ INTERFAZ: la reconoces por interface.
//       Aquí indicamos qué operaciones debe ofrecer GarageHandler.
//       Las instrucciones de esas operaciones irán en GarageHandler.cs.
{
    int Capacity { get; }
    // ↑ PROPIEDAD: permite consultar el máximo de vehículos.

    int OccupiedSpaces { get; }
    // ↑ PROPIEDAD: permite consultar cuántos vehículos hay.

    bool TryPark(Vehicle vehicle);
    // ↑ MÉTODO: recibe el vehículo que queremos estacionar.
    // bool indica que entrega true o false.

    bool TryRemove(string registrationNumber);
    // ↑ MÉTODO: recibe la matrícula del vehículo que queremos retirar.

    Vehicle? FindByRegistrationNumber(string registrationNumber);
    // ↑ MÉTODO: entrega el vehículo encontrado.
    // ? permite entregar null si no hay coincidencia.

    List<Vehicle> GetVehicles();
    // ↑ MÉTODO: entrega una lista de los vehículos estacionados.

    int CountByType(string vehicleType);
    // ↑ MÉTODO: entrega cuántos vehículos hay del tipo solicitado.

    List<Vehicle> Search(
        string registrationNumber,
        string color,
        int? numberOfWheels,
        string vehicleType);
    // ↑ MÉTODO: entrega los vehículos que cumplen los filtros.
    // int? permite no indicar un número de ruedas: null.
}