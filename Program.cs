
    using GarageApp.UI;
    // ↑ Permite utilizar la clase ConsoleUI.

    namespace GarageApp;

    internal class Program
    {
        private static void Main()
        // ↑ Aquí comienza la ejecución del programa.
        {
            ConsoleUI ui = new ConsoleUI();
            // ↑ Crea el objeto que manejará la consola.

            ui.Run();
            // ↑ Ejecuta su método Run:
            // pregunta la capacidad, crea el garaje y muestra el menú.
        }
    }
