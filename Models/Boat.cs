
    using System;

    namespace GarageApp.Models;

    public class Boat : Vehicle
    // ↑ CLASE llamada Boat.
    // : Vehicle indica que hereda matrícula, color y ruedas.
    {
        public decimal Length { get; }
        // ↑ PROPIEDAD: guardará la longitud en metros.
        // decimal permite valores con decimales, como 4.5.
        // get permite consultar la longitud guardada.


        public Boat(
            string registrationNumber,
            string color,
            int numberOfWheels,
            decimal length)
        // ↑ CONSTRUCTOR: se llama Boat, igual que la clase.
        // No lleva un tipo de resultado delante.
        //
        // length es el PARÁMETRO que recibe la longitud.

            : base(registrationNumber, color, numberOfWheels)
        // ↑ Envía matrícula, color y ruedas a Vehicle.
        // Podemos entregar cero ruedas: Vehicle permite cero.
        {
            if (length <= 0)
            // ↑ Comprueba que la longitud sea mayor que cero.
            {
                throw new ArgumentException(
                    "Length must be greater than zero.");
            }

            Length = length;
            // ↑ PROPIEDAD donde se guarda la longitud.
            //       ↑ PARÁMETRO que recibió el número.
        }
    }
