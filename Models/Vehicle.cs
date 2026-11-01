
    using System;
    // ↑ Permite utilizar ArgumentException para comunicar datos inválidos.

    namespace GarageApp.Models;
    //  Agrupa esta clase bajo el nombre GarageApp.Models.
    


    public class Vehicle
    
    {
        public string RegistrationNumber { get; }
        // ↑ PROPIEDAD: reconoce { get; }.
        // string significa texto.
        // Guarda la matrícula y permite consultarla.
        // Sin set, otro código no puede reemplazarla directamente.


        public string Color { get; }
        // ↑ PROPIEDAD: guarda el color y permite consultarlo.


        public int NumberOfWheels { get; }
        // ↑ PROPIEDAD: guarda la cantidad de ruedas.
       
        
        // Estas propiedades son automáticas:
        // C# prepara internamente dónde guardar sus valores.
        // No necesitamos escribir campos separados para este paso.


        public Vehicle(
            string registrationNumber,
            string color,
            int numberOfWheels)
        //      CONSTRUCTOR: se llama Vehicle, igual que la clase.
        // No lleva un tipo de resultado delante del nombre.
        //
        // Los tres nombres entre paréntesis son PARÁMETROS:
        // reciben los datos del vehículo que se está creando.
        {
            if (string.IsNullOrWhiteSpace(registrationNumber))
            // ↑ Comprueba si la matrícula es null, está vacía
            // o contiene solamente espacios.
            {
                throw new ArgumentException(
                    "Registration number cannot be empty.");
                // ↑ Comunica el problema e interrumpe la creación.
            }

            if (string.IsNullOrWhiteSpace(color))
            {
                throw new ArgumentException(
                    "Color cannot be empty.");
            }

            if (numberOfWheels < 0)
            {
                throw new ArgumentException(
                    "Number of wheels cannot be negative.");
                // Permitimos cero: un barco puede no tener ruedas.
            }


            RegistrationNumber = registrationNumber.Trim();
            // ↑ Guarda la matrícula recibida en la propiedad.
            // Trim devuelve el texto sin espacios al principio y al final.

            Color = color.Trim();
            // ↑ Guarda el color recibido.

            NumberOfWheels = numberOfWheels;
            // ↑ Guarda la cantidad de ruedas recibida.
        }
    }
