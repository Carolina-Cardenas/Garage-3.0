
    using System;

    namespace GarageApp.Models;

    public class Airplane : Vehicle
    // ↑ CLASE llamada Airplane.
    // : Vehicle indica que hereda matrícula, color y ruedas.
    {
        public int NumberOfEngines { get; }
        // ↑ PROPIEDAD: guardará la cantidad de motores.
        // El constructor recibirá ese número.
        // get permitirá consultarlo después.


        public Airplane(
            string registrationNumber,
            string color,
            int numberOfWheels,
            int numberOfEngines)
        // ↑ CONSTRUCTOR: se llama Airplane, igual que la clase.
        // No lleva un tipo de resultado delante.
        //
        // Los PARÁMETROS reciben los datos del avión.

            : base(registrationNumber, color, numberOfWheels)
        // ↑ Envía los tres datos comunes al constructor de Vehicle.
        // Vehicle los comprueba y los guarda.
        {
            if (numberOfEngines <= 0)
            // ↑ En esta versión aceptamos aviones con uno o más motores.
            {
                throw new ArgumentException(
                    "Number of engines must be greater than zero.");
            }

            NumberOfEngines = numberOfEngines;
            // ↑ PROPIEDAD donde se guarda.
            //                ↑ PARÁMETRO que recibió el número.
        }
    }
