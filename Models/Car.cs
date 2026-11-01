
    using System;

    namespace GarageApp.Models;
    // ↑ El mismo grupo de nombres que utilizamos en Vehicle.cs.


    public class Car : Vehicle    
    // Car ya tendrá las propiedades de Vehicle:
    // RegistrationNumber, Color y NumberOfWheels.
    // Por eso no las escribimos otra vez aquí.
    {
        public string FuelType { get; }
        // ↑ PROPIEDAD llamada FuelType: significa tipo de combustible.
        // Todavía no contiene un combustible concreto.
        // El constructor recibirá ese texto y lo guardará aquí.
        // get permite consultarlo después.


        public Car(
            string registrationNumber,
            string color,
            int numberOfWheels,
            string fuelType)
        //     ↑ CONSTRUCTOR: se llama Car, igual que la clase.
        // No tiene un tipo de resultado delante de Car.
        //
        // Los cuatro PARÁMETROS reciben los datos del coche:
        // matrícula, color, cantidad de ruedas y combustible.

            : base(registrationNumber, color, numberOfWheels)
        //    ↑ BASE llama al constructor de Vehicle.
        //
        // Envía los tres datos comunes a Vehicle.
        // El constructor de Vehicle los comprueba y los guarda.
        // Después se ejecutan las instrucciones de abajo.
        {
            if (string.IsNullOrWhiteSpace(fuelType))
            // ↑ Comprueba si el combustible está vacío,
            // es null o contiene solamente espacios.
            {
                throw new ArgumentException(
                    "Fuel type cannot be empty.");
                // ↑ Comunica el error e interrumpe la creación.
            }

            FuelType = fuelType.Trim();
            // ↑ PROPIEDAD donde guardamos el combustible.
            //         ↑ PARÁMETRO que recibió el texto.
            //                  ↑ Quita espacios de los extremos.
        }
    }
