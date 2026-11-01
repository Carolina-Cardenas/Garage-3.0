
    using System;

    namespace GarageApp.Models;

    public class Bus : Vehicle
    // ↑ CLASE llamada Bus.
    // : Vehicle indica que hereda matrícula, color y ruedas.
    {
        public int NumberOfSeats { get; }
        // ↑ PROPIEDAD: guardará la cantidad de asientos.
        // int indica que será un número entero.
        // get permite consultar el número una vez guardado.


        public Bus(
            string registrationNumber,
            string color,
            int numberOfWheels,
            int numberOfSeats)
        // ↑ CONSTRUCTOR: se llama Bus, igual que la clase.
        // No lleva un tipo de resultado delante.
        //
        // numberOfSeats es el PARÁMETRO que recibe
        // la cantidad de asientos al crear el autobús.

            : base(registrationNumber, color, numberOfWheels)
        // ↑ Ejecuta el constructor de Vehicle.
        // Le entrega matrícula, color y ruedas
        // para que los compruebe y los guarde.
        {
            if (numberOfSeats <= 0)
            // ↑ Comprueba que haya al menos un asiento.
            {
                throw new ArgumentException(
                    "Number of seats must be greater than zero.");
            }

            NumberOfSeats = numberOfSeats;
            // ↑ Destino: la PROPIEDAD que guarda la cantidad.
            //              ↑ Origen: el PARÁMETRO recibido.
        }
    }
