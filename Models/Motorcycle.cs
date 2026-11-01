
    using System;

    namespace GarageApp.Models;

    public class Motorcycle : Vehicle
    // ↑ CLASE llamada Motorcycle.
    // : Vehicle indica que hereda matrícula, color y ruedas.
    {
        public int CylinderVolume { get; }
        // ↑ PROPIEDAD: guardará la cilindrada en centímetros cúbicos.
        // int significa número entero.
        // El constructor recibirá el número; get permitirá consultarlo.


        public Motorcycle(
            string registrationNumber,
            string color,
            int numberOfWheels,
            int cylinderVolume)
        // ↑ CONSTRUCTOR: mismo nombre que la clase, Motorcycle.
        // No lleva un tipo de resultado delante.
        //
        // Los PARÁMETROS reciben los datos de la motocicleta.

            : base(registrationNumber, color, numberOfWheels)
        // ↑ Envía los datos comunes al constructor de Vehicle.
        // Vehicle los comprueba y los guarda.
        {
            if (cylinderVolume <= 0)
            // ↑ La cilindrada debe ser mayor que cero.
            {
                throw new ArgumentException(
                    "Cylinder volume must be greater than zero.");
            }

            CylinderVolume = cylinderVolume;
            // ↑ PROPIEDAD donde guardamos el número.
            //               ↑ PARÁMETRO que recibió ese número.
        }
    }
