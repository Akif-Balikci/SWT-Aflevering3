using System;
using Microwave.Classes.Boundary;
using Microwave.Classes.Controllers;

namespace Microwave.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Button startCancelButton = new Button();
            Button powerButton = new Button();
            Button timeButton = new Button();

            Door door = new Door();

            Output output = new Output();

            Display display = new Display(output);

            PowerTube powerTube = new PowerTube(output);

            powerTube.SetMaxPower(1000);

            Light light = new Light(output);

            Buzzer buzzer = new Buzzer(output);

            Microwave.Classes.Boundary.Timer timer = new Timer();

            CookController cooker = new CookController(timer, display, powerTube);

            UserInterface ui = new UserInterface(powerButton, timeButton, startCancelButton, door, display, light, cooker, buzzer );

            // Finish the double association
            cooker.UI = ui;

            // Simulate a simple sequence

            powerButton.Press();

            timeButton.Press();

            startCancelButton.Press();

            // The simple sequence should now run

            System.Console.WriteLine("When you press enter, the program will stop \n Pressing 'E' will increment timer, 'R' will decrement");
            ConsoleKeyInfo v;
            do
            {
                v = Console.ReadKey();
                if (v.Key == ConsoleKey.E)
                {
                    timeButton.Press();
                    ui.decrease = false;
                }
                if (v.Key == ConsoleKey.Enter)
                {
                    System.Environment.Exit(0);
                }
                if (v.Key == ConsoleKey.R)
                {
                    ui.decrease = true;
                    timeButton.Press();
                }
            }
            while (v.Key != ConsoleKey.Escape);
            // Wait for input
            System.Console.ReadLine();
        }
    }
}
